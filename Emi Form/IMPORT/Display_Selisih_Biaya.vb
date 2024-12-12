Public Class Display_Selisih_Biaya
    Private currentRow, currentCell As Integer
    Private resetRow As Boolean = False



    Dim DGVNoFak As String
    Dim DGVSisa As String
    Dim DGVPakai As String
    Dim DGVJenis As String
    Dim DGVUrut As String
    Private Function CekNothing(ByVal str As String) As String
        Dim hasil As String = ""

        If str Is Nothing Then
            hasil = ""
        Else
            hasil = str
        End If

        Return hasil
    End Function

    Public Sub Get_Isi_DataGridView(ByVal index As Integer)
        DGVNoFak = DataGridView1.Rows.Item(index).Cells(0).Value.ToString
        DGVJenis = DataGridView1.Rows.Item(index).Cells(1).Value.ToString
        DGVSisa = CekNothing(DataGridView1.Rows.Item(index).Cells(2).Value.ToString)
        DGVPakai = CekNothing(DataGridView1.Rows.Item(index).Cells(3).Value.ToString)
        DGVUrut = DataGridView1.Rows.Item(index).Cells(4).Value.ToString

    End Sub

    Private Sub Display_Selisih_PO_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            DataGridView1.Rows.Clear()

            OpenConn()
            Dim Kode_sup As String = ""

            SQL = "select cast(RV as bigint) as rvx, biaya_form_e, b.Flag_Average, b.Metode_Selisih_Declare, b.Kode_Supplier from rencana_order a, Suppliers b where "
            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Supplier = b.Kode_Supplier "
            SQL = SQL & "and a.id_rencana = '" & Hitung_HPP_Import.TxtId_Rencana.Text & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Kode_sup = Dr("Kode_Supplier")
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Id Rencana tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            SQL = "select a.no_val,a.id_rencana ,a.sisa,a.urut from  Selisih_Kurs_Biaya_Import_By_Perusahaan a, rencana_order b "
            SQL = SQL & "where a.Kode_Perusahaan=b.Kode_Perusahaan and a.Id_Rencana=b.ID_rencana and a.kode_perusahaan = '" & KodePerusahaan & "'  "
            SQL = SQL & "and a.flag_pakai is null and round(a.sisa,0) <> 0  and a.status is null "
            SQL = SQL & "and b.Lokasi='" & Hitung_HPP_Import.CmbLokasi.Text & "' and b.Kode_Supplier='" & Kode_sup & "' "
            SQL = SQL & "order by no_val asc"
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    For index As Integer = 0 To .Rows.Count - 1

                        DataGridView1.Rows.Add(1)
                        DataGridView1.Rows.Item(index).Cells(0).Value = .Rows(index).Item("id_rencana")
                        If .Rows(index).Item("sisa") < 0 Then
                            DataGridView1.Rows.Item(index).Cells(1).Value = "Deposit"
                        ElseIf .Rows(index).Item("sisa") > 0 Then
                            DataGridView1.Rows.Item(index).Cells(1).Value = "Hutang"
                        End If
                        DataGridView1.Rows.Item(index).Cells(2).Value = Format(Math.Abs(.Rows(index).Item("sisa")), "N0")
                        DataGridView1.Rows.Item(index).Cells(3).Value = ""
                        DataGridView1.Rows.Item(index).Cells(4).Value = .Rows(index).Item("urut")

                    Next
                End With
            End Using
            CloseConn()
            DataGridView1.Columns(0).ReadOnly = True
            DataGridView1.Columns(1).ReadOnly = True
            DataGridView1.Columns(2).ReadOnly = True
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

   

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Hitung_HPP_Import.arrNoUrutBiaya.Clear()
        Hitung_HPP_Import.arrNilaiPakaiBiaya.Clear()
        Hitung_HPP_Import.txtJmlPakaiBiaya.Clear()

        Dim total As Double = 0
        Dim jmlHutang As Double = 0
        Dim jmlDeposit As Double = 0


        For index As Integer = 0 To DataGridView1.Rows.Count - 1

            Get_Isi_DataGridView(index)

            'total += Val(HilangkanTanda(DGVPakai))


            If DGVPakai <> "" And DGVSisa <> 0 Then
                Hitung_HPP_Import.arrNoUrutBiaya.Add(DGVUrut)



                If DGVJenis = "Deposit" Then
                    jmlDeposit += Val(HilangkanTanda(DGVPakai))
                    Hitung_HPP_Import.arrNilaiPakaiBiaya.Add(DGVPakai * -1)
                ElseIf DGVJenis = "Hutang" Then
                    jmlHutang += Val(HilangkanTanda(DGVPakai))
                    Hitung_HPP_Import.arrNilaiPakaiBiaya.Add(DGVPakai)
                End If
            End If


        Next

        Hitung_HPP_Import.txtJmlPakaiBiaya.Text = Format(jmlHutang - jmlDeposit, "N0")
        Hitung_HPP_Import.TxtId_Rencana_Leave(Button1, e)
        Me.Close()

    End Sub

    
    Private Sub DataGridView1_CellEndEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellEndEdit
        'MessageBox.Show(e.ColumnIndex & " and " & e.RowIndex)

        Get_Isi_DataGridView(DataGridView1.CurrentRow.Index)

        If IsNumeric(DGVPakai) = False Or Val(DGVPakai) < 0 Then
            DataGridView1.CurrentRow.Cells(3).Value = ""
        End If

        Get_Isi_DataGridView(DataGridView1.CurrentRow.Index)

        Dim pakai As Double = 0


        If DGVPakai = "" Then
            DataGridView1.CurrentRow.Cells(3).Value = 0
            resetRow = True
            currentRow = e.RowIndex
            currentCell = e.ColumnIndex
            Exit Sub
        Else
            DGVPakai = Val(HilangkanTanda(DGVPakai))
        End If


        If Val(HilangkanTanda(DGVPakai)) > Val(HilangkanTanda(DGVSisa)) Then
            MessageBox.Show("Nilai pakai tidak boleh lebih dari sisa")
            DataGridView1.CurrentRow.Cells(3).Value = 0

            resetRow = True
            currentRow = e.RowIndex
            currentCell = e.ColumnIndex
            Exit Sub
        End If


        

       


    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If resetRow Then
            resetRow = False
            DataGridView1.CurrentCell = DataGridView1.Rows(currentRow).Cells(currentCell)
        End If
    End Sub



   
    Private Sub DataGridView1_TabIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DataGridView1.TabIndexChanged
        If resetRow Then
            resetRow = False
            DataGridView1.CurrentCell = DataGridView1.Rows(currentRow).Cells(currentCell)
        End If
    End Sub


   

    
End Class
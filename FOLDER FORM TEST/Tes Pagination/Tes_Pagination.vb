Public Class Tes_Pagination


    Dim PageSize As Integer = 5

    Private Sub Tes_Pagination_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        ListView1.Columns.Clear()
        ListView1.Columns.Add("Kode Barang", 150, HorizontalAlignment.Left)
        ListView1.Columns.Add("Nama Barang", 300, HorizontalAlignment.Left)
        ListView1.Columns.Add("Jumlah", 150, HorizontalAlignment.Left)
        ListView1.View = View.Details

        Try
            OpenConn()

            Dim TotalData As Integer = 0
            SQL = "select COUNT(*) as Total FROM barang where good_stock <> 0 "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    TotalData = Dr("Total")
                End If
            End Using
            Pagination1.Inisialisasi(TotalData, PageSize)

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        TampilData()
    End Sub


    Private Sub TampilData()
        Try
            OpenConn()

            Dim HalamanAktif As Integer = Pagination1.CurrentPage
            Dim offset As Integer = (HalamanAktif - 1) * PageSize



            ListView1.Items.Clear()
            SQL = "select Kode_Barang, Nama, Good_Stock FROM barang where good_stock <> 0 ORDER BY Kode_Barang "
            SQL = SQL & "OFFSET " & offset & " ROWS FETCH NEXT " & PageSize & " ROWS ONLY "

            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = ListView1.Items.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add(Dr("Nama"))
                    Lv.SubItems.Add(Dr("Good_Stock"))
                Loop
            End Using



            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub Pagination1_PageChanged(NewPage As Integer) Handles Pagination1.PageChanged
        ' Saat tombol ditekan, refresh data
        TampilData()
    End Sub


End Class
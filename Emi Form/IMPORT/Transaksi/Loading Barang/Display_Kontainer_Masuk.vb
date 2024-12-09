Imports Microsoft.VisualBasic.PowerPacks.Printing.Compatibility.VB6

Public Class Display_Kontainer_Masuk
    Dim Arr1, Arr2, Arr3, arrbatal As New ArrayList
    Dim Clr_Selesai As Color = Color.LightBlue
    Dim Clr_Batal As Color = Color.Black
    Dim Clr_Default As Color = Color.Blue

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Display_Data_Pembelian_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        LvRencanaOrder.Columns.Clear()
        LvRencanaOrder.Columns.Add("No Kontainer", 100, HorizontalAlignment.Center)
        LvRencanaOrder.Columns.Add("NO Seal", 100, HorizontalAlignment.Center)
        LvRencanaOrder.Columns.Add("Lokasi", 100, HorizontalAlignment.Left)
        LvRencanaOrder.Columns.Add("Kode Barang", 100, HorizontalAlignment.Left)
        LvRencanaOrder.Columns.Add("Nama Barang", 200, HorizontalAlignment.Left)
        LvRencanaOrder.Columns.Add("Tanggal Muat", 100, HorizontalAlignment.Center)
        LvRencanaOrder.Columns.Add("Qty", 100, HorizontalAlignment.Center)
        LvRencanaOrder.Columns.Add("Urut_Oto", 0, HorizontalAlignment.Left)
        LvRencanaOrder.View = View.Details

        kosong()
        
    End Sub

    Private Sub LvRencanaOrder_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LvRencanaOrder.SelectedIndexChanged

    End Sub

    Private Sub kosong()

        If TextBoxFaktur.Text.Trim.Length = 0 Then
            MessageBox.Show("No Faktur tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Try
            OpenConn()

            SQL = "SELECT No_Container, No_Seal, a.Kode_Stock_Owner, a.Kode_Barang, b.nama, Tgl_Muat, Qty, Urut_Oto "
            SQL = SQL & "FROM Kontainer_Masuk a, Barang b "
            SQL = SQL & "WHERE a.kode_perusahaan = b.kode_perusahaan AND a.kode_barang = b.kode_barang AND a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.kode_perusahaan = '" & KodePerusahaan & "' and a.No_Faktur = '" & TextBoxFaktur.Text & "' "
            SQL = SQL & " Order BY a.Kode_Stock_Owner, a.kode_Barang "

            LvRencanaOrder.Items.Clear()
            Dim Lvw As ListViewItem
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    For i As Integer = 0 To .Rows.Count - 1
                        Lvw = LvRencanaOrder.Items.Add(.Rows(i).Item("No_Container"))
                        Lvw.SubItems.Add(.Rows(i).Item("No_Seal"))
                        Lvw.SubItems.Add(.Rows(i).Item("Kode_Stock_Owner"))
                        Lvw.SubItems.Add(.Rows(i).Item("Kode_Barang"))
                        Lvw.SubItems.Add(.Rows(i).Item("nama"))
                        Lvw.SubItems.Add(Format(.Rows(i).Item("Tgl_Muat"), "dd-MMM-yyyy"))
                        Lvw.SubItems.Add(.Rows(i).Item("Qty"))
                        Lvw.SubItems.Add(.Rows(i).Item("Urut_Oto"))
                    Next
                End With
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub
    Private Sub LvRencanaOrder_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles LvRencanaOrder.DoubleClick
        Dim Hapus1 As String = MessageBox.Show("Anda yakin data ini akan dihapus?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If Hapus1 = vbYes Then
            Try
                If LvRencanaOrder.Items.Count = 0 Then Exit Sub

                OpenConn()
                Cmd.Transaction = Cn.BeginTransaction
                SQL = "Delete from Kontainer_Masuk where Urut_Oto ='" & LvRencanaOrder.FocusedItem.SubItems(7).Text & "' "
                ExecuteTrans(SQL)
                Cmd.Transaction.Commit()
                CloseConn()
            Catch ex As Exception
                CloseTrans()
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        Else
            MessageBox.Show("Penghapusan dibatalkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If

        Kosong()
    End Sub

    Private Sub Display_Kontainer_Masuk_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SizeChanged
        Label1.Size = New Point(Me.Width, 33)
    End Sub
End Class
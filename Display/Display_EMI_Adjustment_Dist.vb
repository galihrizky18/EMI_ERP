Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class Display_EMI_Adjustment_Dist

    Dim arrFilterKodeStock As New ArrayList

    Dim kodePerusahaan As String = "001"

    Private Sub Display_EMI_Adjustment_Dist_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Lv_Adj.Columns.Add("Kode Adjustment", 120, HorizontalAlignment.Center)
        Lv_Adj.Columns.Add("Kode Stock Owner", 130, HorizontalAlignment.Center)
        Lv_Adj.Columns.Add("Tanggal", 100, HorizontalAlignment.Center)
        Lv_Adj.Columns.Add("Jam", 100, HorizontalAlignment.Center)
        Lv_Adj.Columns.Add("Nama Barang", 150, HorizontalAlignment.Center)
        Lv_Adj.Columns.Add("Keterangan", 200, HorizontalAlignment.Center)
        Lv_Adj.Columns.Add("Jumlah", 110, HorizontalAlignment.Center)
        Lv_Adj.View = View.Details

        kosong()

        ComboBox3.Items.Add("Tgl Adjust")
    End Sub

    Private Sub kosong()
        Cmb_StockOwner.Items.Clear()
        Cmb_StockOwner.Text = ""

        Chk_FilterTanggal.Checked = False
        Load_Adjustment()
        Load_Stock_Owner_Filter()
    End Sub

    Private Sub Load_Adjustment()
        Try
            OpenConn()
            Lv_Adj.Items.Clear()

            SQL = "select a.Kode_Adjustment, a.kode_stock_owner, a.Tanggal, a.Jam, b.Nama, a.Keterangan, a.Jumlah as Jmlh_Adj "
            SQL = SQL & "from EMI_Adjustment a, Barang b where a.Kode_Perusahaan=b.Kode_Perusahaan "
            SQL = SQL & "and a.Kode_Stock_Owner=b.Kode_Stock_Owner and a.Kode_Barang=b.Kode_Barang "
            SQL = SQL & "and a.Kode_Perusahaan='" & kodePerusahaan & "'"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Adj.Items.Add(Dr("Kode_Adjustment"))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("kode_stock_owner")))
                    Lv.SubItems.Add(Format(Dr("Tanggal"), "yyyy-MM-dd"))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("Jam")))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("Nama")))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("Keterangan")))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("Jmlh_Adj")))

                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Load_Stock_Owner_Filter()
        Try
            OpenConn()

            SQL = "select kode_stock_owner, keterangan from Stock_Owner_Gudang "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read

                    Cmb_StockOwner.Items.Add(Dr("keterangan")) : arrFilterKodeStock.Add(Dr("kode_stock_owner"))

                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Btn_FilterCari_Click(sender As Object, e As EventArgs) Handles Btn_FilterCari.Click

        Try
            OpenConn()
            Lv_Adj.Items.Clear()

            SQL = "select a.Kode_Adjustment, a.kode_stock_owner, a.Tanggal, a.Jam, b.Nama, a.Keterangan, a.Jumlah as Jmlh_Adj "
            SQL = SQL & "from EMI_Adjustment a, Barang b where a.Kode_Perusahaan=b.Kode_Perusahaan "
            SQL = SQL & "and a.Kode_Stock_Owner=b.Kode_Stock_Owner and a.Kode_Barang=b.Kode_Barang "
            SQL = SQL & "and a.Kode_Perusahaan='" & kodePerusahaan & "' "

            If Not Cmb_StockOwner.SelectedIndex = -1 Then
                SQL = SQL & " and a.kode_stock_owner='" & arrFilterKodeStock(Cmb_StockOwner.SelectedIndex) & "' "
            End If

            If Chk_FilterTanggal.Checked Then
                If ComboBox3.SelectedIndex = 0 Then
                    SQL = SQL & " and a.Tanggal between '"
                    SQL = SQL & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' and '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' "
                End If

            End If

            If CheckBox3.Checked Then
                SQL = SQL & " and a.Tanggal between '"
                SQL = SQL & Format(Now, "yyyy-MM-dd") & "' and '" & Format(Now, "yyyy-MM-dd") & "' "
            End If

            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Adj.Items.Add(Dr("Kode_Adjustment"))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("kode_stock_owner")))
                    Lv.SubItems.Add(Format(Dr("Tanggal"), "yyyy-MM-dd"))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("Jam")))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("Nama")))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("Keterangan")))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("Jmlh_Adj")))

                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        kosong()
    End Sub
End Class
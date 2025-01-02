Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button

Public Class EMI_Produksi_Display
    Dim Jenis = "Transaksi_Produksi"
    Public asal As String
    Dim arrcari As New ArrayList
    Public filter_tambahan As String
    Private Sub SD_Produksi_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        kosong3()
    End Sub

    Private Sub kosong3()
        Try
            OpenConn()
            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)

            Label1.Text = Base_Language.Lang_Transaksi_Produksi_Judul_SD
            Label3.Text = Base_Language.Lang_Global_Jenis
            Btn_Cari.Text = Base_Language.Lang_Global_Cari

            ComboBox3.Items.Clear() : arrcari.Clear()
            ComboBox3.Items.Add(Base_Language.Lang_Global_NoFaktur) : arrcari.Add("No_Faktur")
            ComboBox3.Items.Add("Line") : arrcari.Add("Line")
            ComboBox3.SelectedIndex = -1
            TextBox3.Text = ""

            ListView1.Columns.Clear()
            ListView1.Columns.Add(Base_Language.Lang_Global_NoFaktur, 250, HorizontalAlignment.Left)
            ListView1.Columns.Add(Base_Language.Lang_Global_Tanggal_Produksi, 210, HorizontalAlignment.Center)
            ListView1.Columns.Add(Base_Language.Lang_Global_Jam, 210, HorizontalAlignment.Center)
            ListView1.Columns.Add("Line", 250, HorizontalAlignment.Center)
            ListView1.View = View.Details

            ListView2.Columns.Clear()
            ListView2.Columns.Add(Base_Language.Lang_Global_No_PO, 130, HorizontalAlignment.Left)
            ListView2.Columns.Add(Base_Language.Lang_Global_Lokasi, 130, HorizontalAlignment.Left)
            ListView2.Columns.Add(Base_Language.Lang_Global_KodeCustomer, 0, HorizontalAlignment.Left)
            ListView2.Columns.Add(Base_Language.Lang_Global_NamaCustomer, 240, HorizontalAlignment.Left)
            ListView2.Columns.Add(Base_Language.Lang_Global_KodeBarang, 0, HorizontalAlignment.Left)
            ListView2.Columns.Add(Base_Language.Lang_Global_NamaBarang, 250, HorizontalAlignment.Left)
            ListView2.Columns.Add(Base_Language.Lang_Global_Jumlah, 100, HorizontalAlignment.Center)
            ListView2.Columns.Add(Base_Language.Lang_Global_Satuan, 120, HorizontalAlignment.Center)
            ListView2.View = View.Details

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        isi_LV()
    End Sub

    Private Sub isi_LV()
        Try
            OpenConn()

            ListView1.Items.Clear()
            ListView2.Items.Clear()
            SQL = "select No_Faktur,Tanggal_Produksi,Jam_Produksi,Line from emi_rencana_produksi "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "Status is null "
            SQL = SQL & " " & filter_tambahan & " "
            SQL = SQL & "order by Tanggal_Produksi,Jam_Produksi"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim lvw As ListViewItem
                    lvw = ListView1.Items.Add(dr("No_Faktur"))
                    lvw.SubItems.Add(Format(dr("Tanggal_Produksi"), "dd MMMM yyyy"))
                    lvw.SubItems.Add(dr("Jam_Produksi"))
                    lvw.SubItems.Add(dr("Line"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
        Try
            OpenConn()

            ListView1.Items.Clear()
            SQL = "select No_Faktur,Tanggal_Produksi,Jam_Produksi,Line from emi_rencana_produksi "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "Status is null and Selesai is null "
            SQL = SQL & " " & filter_tambahan & " "
            If ComboBox3.SelectedIndex <> -1 Then
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "
                SQL = SQL & arrcari.Item(ComboBox3.SelectedIndex) & "  like  '%" & Trim(TextBox3.Text) & "%' "
            End If
            SQL = SQL & "order by Tanggal_Produksi,Jam_Produksi"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim lvw As ListViewItem
                    lvw = ListView1.Items.Add(dr("No_Faktur"))
                    lvw.SubItems.Add(Format(dr("Tanggal_Produksi"), "dd MMMM yyyy"))
                    lvw.SubItems.Add(dr("Jam_Produksi"))
                    lvw.SubItems.Add(dr("Line"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged
        Try
            OpenConn()

            ListView2.Items.Clear()
            SQL = "select a.No_PO,a.Kode_Stock_Owner,a.Kode_Customer,b.Nama as nama_cus,"
            SQL = SQL & "a.Kode_Barang,c.Nama as nama_brg,a.Jumlah,a.Satuan from "
            SQL = SQL & "emi_rencana_produksi_detail a,Customers b, Barang c where "
            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and "
            SQL = SQL & "a.Kode_Perusahaan = c.Kode_Perusahaan and "
            SQL = SQL & "a.Kode_Customer = b.Kode_Customer and "
            SQL = SQL & "a.Kode_Stock_Owner = c.Kode_Stock_Owner and "
            SQL = SQL & "a.Kode_Barang = c.Kode_Barang and "
            SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "a.No_Faktur = '" & ListView1.FocusedItem.Text & "'"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim lvw As ListViewItem
                    lvw = ListView2.Items.Add(dr("No_PO"))
                    lvw.SubItems.Add(dr("Kode_Stock_Owner"))
                    lvw.SubItems.Add(dr("Kode_Customer"))
                    lvw.SubItems.Add(dr("nama_cus"))
                    lvw.SubItems.Add(dr("Kode_Barang"))
                    lvw.SubItems.Add(dr("nama_brg"))
                    lvw.SubItems.Add(Format(dr("Jumlah"), "N0"))
                    lvw.SubItems.Add(dr("Satuan"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub ListView1_DoubleClick(sender As Object, e As EventArgs) Handles ListView1.DoubleClick
        If ListView1.Items.Count = 0 Or ListView1.SelectedItems.Count = 0 Then
            MessageBox.Show(Base_Language.Lang_Transaksi_Produksi_Error_Pilih, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        If asal = "Transaksi_Produksi" Then
            'EMI_Produksi.TextBox4.Text = ListView1.FocusedItem.Text
            EMI_Produksi.ShowDialog()
        ElseIf asal = "Transaksi_Hasil_Produksi" Then
            EMI_Hasil_Produksi.no_ro = ListView1.FocusedItem.Text
            EMI_Hasil_Produksi.ShowDialog()
        ElseIf asal = "Compare_HPP" Then
            EMI_Compare_HPP.no_ro = ListView1.FocusedItem.Text
            EMI_Compare_HPP.ShowDialog()
        End If

    End Sub

    Public Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        isi_LV()
    End Sub
End Class
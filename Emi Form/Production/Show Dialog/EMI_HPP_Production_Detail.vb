
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button

Public Class EMI_HPP_Production_Detail
    Public asal As String
    Public nno_transaksi, nbulan, ntahun, nkd_so, nkd_brg As String
    Private Sub EMI_HPP_Production_Detail_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
        kosong()
    End Sub

    Private Sub kosong()

        Try
            OpenConn()
            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            'Base_Language.Get_Languages(Bahasa_Pilihan, "QC_Formula")

            If asal = "Biaya Bahan Baku" Then
                Label1.Text = "Detail HPP Biaya Bahan Baku"

                ListView1.Columns.Clear() : ListView1.Items.Clear()
                ListView1.Columns.Add("Kode Stock Owner", 150, HorizontalAlignment.Left)
                ListView1.Columns.Add("Kode Barang", 150, HorizontalAlignment.Left)
                ListView1.Columns.Add("Nama Barang", 250, HorizontalAlignment.Left)
                ListView1.Columns.Add("Batch Number", 150, HorizontalAlignment.Left)
                ListView1.Columns.Add("Jumlah", 150, HorizontalAlignment.Right)
                ListView1.Columns.Add("HPP", 150, HorizontalAlignment.Right)
                ListView1.Columns.Add("Total", 150, HorizontalAlignment.Right)


                SQL = "select  a.No_Transaksi,a.Kode_Stock_Owner,a.Kode_Barang,"
                SQL = SQL & "b.Nama,a.Nilai,a.Serial_Number,c.Batch_Number,"
                SQL = SQL & "dbo.get_hpp(a.Serial_Number) as hpp "
                SQL = SQL & "from Emi_Production_Results_Det a,Barang b,Barang_SN c "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
                SQL = SQL & "and a.Kode_Stock_Owner = b.Kode_Stock_Owner "
                SQL = SQL & "and a.Kode_Barang = b.Kode_Barang "
                SQL = SQL & "and a.Kode_Perusahaan = c.Kode_Perusahaan "
                SQL = SQL & "and a.Kode_Stock_Owner = c.Kode_Stock_Owner "
                SQL = SQL & "and a.Kode_Barang = c.Kode_Barang "
                SQL = SQL & "and a.Serial_Number = c.Serial_Number "
                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and a.No_Transaksi = '" & nno_transaksi & "' "
                Using dr = OpenTrans(SQL)
                    Do While dr.Read
                        Dim lvw As ListViewItem
                        lvw = ListView1.Items.Add(dr("Kode_Stock_Owner"))
                        lvw.SubItems.Add(dr("Kode_Barang"))
                        lvw.SubItems.Add(dr("Nama"))
                        lvw.SubItems.Add(dr("Batch_Number"))
                        lvw.SubItems.Add(Format(dr("Nilai"), "N2"))
                        lvw.SubItems.Add(Format(dr("hpp"), "N2"))
                        Dim ftotal As Double = dr("Nilai") * dr("hpp")
                        lvw.SubItems.Add(Format(ftotal, "N2"))
                    Loop
                End Using
            ElseIf asal = "Biaya Packaging" Then
                Label1.Text = "Detail HPP Biaya Packaging"

                ListView1.Columns.Clear() : ListView1.Items.Clear()
                ListView1.Columns.Add("Kode Stock Owner", 150, HorizontalAlignment.Left)
                ListView1.Columns.Add("Kode Barang", 150, HorizontalAlignment.Left)
                ListView1.Columns.Add("Nama Barang", 250, HorizontalAlignment.Left)
                ListView1.Columns.Add("Batch Number", 150, HorizontalAlignment.Left)
                ListView1.Columns.Add("Jumlah", 150, HorizontalAlignment.Right)
                ListView1.Columns.Add("HPP", 150, HorizontalAlignment.Right)
                ListView1.Columns.Add("Total", 150, HorizontalAlignment.Right)

                SQL = "select  a.No_Transaksi,a.Kode_Stock_Owner,a.Kode_Barang,"
                SQL = SQL & "b.Nama,a.Nilai,a.Serial_Number,c.Batch_Number,"
                SQL = SQL & "dbo.get_hpp(a.Serial_Number) as hpp "
                SQL = SQL & "from Emi_Production_Results_packaging_det a,Barang b,Barang_SN c "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
                SQL = SQL & "and a.Kode_Stock_Owner = b.Kode_Stock_Owner "
                SQL = SQL & "and a.Kode_Barang = b.Kode_Barang "
                SQL = SQL & "and a.Kode_Perusahaan = c.Kode_Perusahaan "
                SQL = SQL & "and a.Kode_Stock_Owner = c.Kode_Stock_Owner "
                SQL = SQL & "and a.Kode_Barang = c.Kode_Barang "
                SQL = SQL & "and a.Serial_Number = c.Serial_Number "
                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and a.No_Transaksi = '" & nno_transaksi & "' "
                Using dr = OpenTrans(SQL)
                    Do While dr.Read
                        Dim lvw As ListViewItem
                        lvw = ListView1.Items.Add(dr("Kode_Stock_Owner"))
                        lvw.SubItems.Add(dr("Kode_Barang"))
                        lvw.SubItems.Add(dr("Nama"))
                        lvw.SubItems.Add(dr("Batch_Number"))
                        lvw.SubItems.Add(Format(dr("Nilai"), "N2"))
                        lvw.SubItems.Add(Format(dr("hpp"), "N2"))
                        Dim ftotal As Double = dr("Nilai") * dr("hpp")
                        lvw.SubItems.Add(Format(ftotal, "N2"))
                    Loop
                End Using
            ElseIf asal = "Biaya Produksi" Then
                Label1.Text = "Detail HPP Biaya Produksi"

                ListView1.Columns.Clear() : ListView1.Items.Clear()
                ListView1.Columns.Add("id Work center", 0, HorizontalAlignment.Left)
                ListView1.Columns.Add("Kode Work Center", 150, HorizontalAlignment.Left)
                ListView1.Columns.Add("Nama Work Center", 250, HorizontalAlignment.Left)
                ListView1.Columns.Add("Nilai Per Pcs", 150, HorizontalAlignment.Right)

                SQL = "select b.Id_Work_Center,c.Kode_Work_Center,c.Keterangan,b.Nilai_Per_Pcs "
                SQL = SQL & "from Emi_Transaksi_Work_Center a,Emi_Transaksi_Work_Center_Detail b, EMI_Master_Work_Center c "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur and a.Status is null "
                SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Work_Center = c.Id_Work_Center "
                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Bulan = '" & nbulan & "' and a.Tahun = '" & ntahun & "' "
                SQL = SQL & "and b.Kode_Barang = '" & nkd_brg & "' "
                Using dr = OpenTrans(SQL)
                    Do While dr.Read
                        Dim lvw As ListViewItem
                        lvw = ListView1.Items.Add(dr("Id_Work_Center"))
                        lvw.SubItems.Add(dr("Kode_Work_Center"))
                        lvw.SubItems.Add(dr("Keterangan"))
                        lvw.SubItems.Add(Format(dr("Nilai_Per_Pcs"), "N2"))
                    Loop
                End Using
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub EMI_HPP_Production_Detail_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub
End Class
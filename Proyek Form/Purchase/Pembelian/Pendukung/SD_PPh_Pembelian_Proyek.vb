
Public Class SD_PPh_Pembelian_Proyek
    Public kd_sup As String
    Private Sub SD_PPh_PO_Pembelian_Proyek_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            OpenConn()

            Dim totalpph As Double = 0
            Dim xpph As Double = 0
            Listview1.Items.Clear()
            SQL = "select b.Kode_Tarif,d.Keterangan,b.Persentase from "
            SQL = SQL & "Barang_Masuk_Proyek a, Detail_PO_Pembelian_Proyek_PPH b, PO_Pembelian_Proyek c, EMI_Master_Pajak d "
            SQL = SQL & "where a.No_PO = c.No_Faktur and a.Status is null and c.Status is null and a.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.No_Faktur = c.No_Faktur "
            SQL = SQL & "and b.Kode_Perusahaan = d.Kode_Perusahaan and b.Kode_Tarif = d.Kode_Tarif and b.Flag_PPN is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Faktur = '" & kd_sup & "' "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    xpph = Val(HilangkanTanda(TxtSub.Text)) * dr("Persentase") / 100
                    totalpph = totalpph + xpph

                    Dim lvw As New ListViewItem
                    lvw = Listview1.Items.Add(dr("Kode_Tarif"))
                    lvw.SubItems.Add(dr("Keterangan"))
                    lvw.SubItems.Add(Format(dr("Persentase"), "N0"))
                    lvw.SubItems.Add(Format(xpph, "N0"))
                Loop
            End Using

            TextBox2.Text = Format(totalpph, "N0")

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub
End Class
Public Class SD_Detail_PajakPO_Pelunasan

    Public NoPO, KdPerusahaanBiayaImport As String

    Private Sub SD_Detail_PajakPO_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Kosong()
    End Sub

    Private Sub Kosong()

        Lv_Data.Columns.Clear() : Lv_Data.Items.Clear()
        Lv_Data.Columns.Add("Pajak", 150, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Persen", 120, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Nilai", 250, HorizontalAlignment.Right)
        Lv_Data.View = View.Details

        GetData()
    End Sub

    Private Sub GetData()

        Dim TotalPPH As Double = 0

        Try
            OpenConn()

            Lv_Data.Items.Clear()
            SQL = "select Persentase, Kode_Tarif from Display_Biaya_Import_PPH_Detail where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Perusahaan_Biaya_Import = '" & KdPerusahaanBiayaImport & "' and No_BiayaImportPPH = '" & NoPO & "' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Data.Items.Add(Dr("Kode_Tarif"))
                    Lv.SubItems.Add(Dr("Persentase") & " %")

                    Dim NilaiPajak As Double = (Val(HilangkanTanda(Dr("Persentase"))) / 100) * HilangkanTanda(TxtPO_GrandTotal.Text)
                    Lv.SubItems.Add(Format(NilaiPajak, "N2"))

                    TotalPPH += NilaiPajak
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Txt_TotalPPH.Text = Format(TotalPPH, "N2")

    End Sub
End Class
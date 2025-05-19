Public Class SD_Detail_PajakPO_Sub

    Public asal As String

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
            If asal = "SUBPO" Then
                SQL = "select Persentase, Kode_Tarif from EMI_Detail_PPH_PO_Induk where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & Txt_NoFakInduk.Text & "' and flag_ppn is null"
            ElseIf asal = "PEMBELIAN" Then
                SQL = "select Persentase, Kode_Tarif from EMI_Detail_PPH_PO where kode_perusahaan = '" & KodePerusahaan & "' and No_Faktur_Induk = '" & Txt_NoFakInduk.Text & "' and No_Faktur = '" & No_FakturSub.Text & "' and flag_ppn is null"
            End If
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
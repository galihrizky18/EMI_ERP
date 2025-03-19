Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button

Public Class EMI_Actual_Budgeting_Detail_Display

    Public tanggalAwal As String
    Public tanggalAkhir As String
    Public jenisBiaya As String

    Private subTotalJumlah As Double


    Private Sub SD_Formulator_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        kosong()
    End Sub





    Private Sub kosong()

        Try
            OpenConn()
            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            Base_Language.Get_Languages(Bahasa_Pilihan, "QC_Formula")
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


        subTotalJumlah = 0


        ListView1.Columns.Clear()
        ListView1.Columns.Add("Meteran", 150, HorizontalAlignment.Left)
        ListView1.Columns.Add("Tarif Per Satuan", 110, HorizontalAlignment.Right)
        ListView1.Columns.Add("Jumlah", 110, HorizontalAlignment.Right)
        ListView1.Columns.Add("Total", 130, HorizontalAlignment.Right)
        ListView1.Columns.Add("Kode Barang", 110, HorizontalAlignment.Right)
        ListView1.Columns.Add("Nama", 120, HorizontalAlignment.Right)
        ListView1.Columns.Add("Tanggal Awal", 110, HorizontalAlignment.Center)
        ListView1.Columns.Add("Tanggal Akhir", 110, HorizontalAlignment.Center)

        ListView1.View = View.Details

        DtpPeriodeAwal.Value = tanggalAwal
        DtpPeriodeAkhir.Value = tanggalAkhir

        get_detail_actual_budgeting()

        Label6.Text = Format(subTotalJumlah, "N2")



    End Sub


    Private Sub get_detail_actual_budgeting()
        Try
            OpenConn()

            ListView1.Items.Clear()
            SQL = "select Kode_Jenis_Biaya_Produksi,total,kode_meteran, Satuan As satuan_Produksi, kode_barang,nama,Tarif_Per_Satuan,Jumlah, Tanggal_Awal,Tanggal_Akhir "
            SQL = SQL & "From Vw_Aktualisasi_Budgeting_Aktualisasi "
            SQL = SQL & " Where  tanggal_awal between '" & tanggalAwal & "' and '" & tanggalAkhir & "' "
            SQL = SQL & "and tanggal_akhir between '" & tanggalAwal & "' and '" & tanggalAkhir & "' and Kode_Jenis_Biaya_Produksi = '" & jenisBiaya & "' "

            SQL = SQL & "order by  Kode_Jenis_Biaya_Produksi,kode_meteran asc "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lvw As ListViewItem
                    lvw = ListView1.Items.Add(Dr("kode_meteran"))

                    lvw.SubItems.Add(Format(Dr("Tarif_Per_Satuan"), "N2"))
                    lvw.SubItems.Add(Format(Dr("jumlah"), "N2") & " " & Dr("satuan_Produksi"))

                    lvw.SubItems.Add(Format(Dr("total"), "N2"))
                    If General_Class.CekNULL(Dr("kode_barang")) = "" Then
                        lvw.SubItems.Add("-")
                    Else
                        lvw.SubItems.Add(Dr("kode_barang"))
                    End If

                    If General_Class.CekNULL(Dr("nama")) = "" Then
                        lvw.SubItems.Add("-")
                    Else
                        lvw.SubItems.Add(Dr("nama"))
                    End If
                    lvw.SubItems.Add(Format(Dr("tanggal_awal"), "dd MMM yyyy"))
                    lvw.SubItems.Add(Format(Dr("tanggal_akhir"), "dd MMM yyyy"))

                    subTotalJumlah = subTotalJumlah + Dr("total")
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub






End Class
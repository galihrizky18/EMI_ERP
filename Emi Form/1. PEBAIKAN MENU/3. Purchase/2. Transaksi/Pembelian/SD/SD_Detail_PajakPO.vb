Public Class SD_Detail_PajakPO

    Dim JudulForm As String = "Detail Pajak PO"
    Public KdSupplier, No_Fak As String

    Public tempDataPajak As New List(Of (Pajak As String, Nilai As String, akun As String, isPPN As Boolean, KodeTarif As String))

    Private Sub SD_Detail_PajakPO_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Kosong()
    End Sub

    Private Sub Kosong()

        Lv_Data.Columns.Clear() : Lv_Data.Items.Clear()
        Lv_Data.Columns.Add("Pajak", 150, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Persen", 120, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Nilai", 250, HorizontalAlignment.Right)
        Lv_Data.View = View.Details

        If String.IsNullOrEmpty(KdSupplier) Then
            MessageBox.Show("Kode SUpplier Tidak ditemukan", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Me.Close()
        End If

        GetData()
    End Sub

    Private Sub GetData()
        Try
            OpenConn()

            Dim TotalPPH As Double = 0

            Dim hasData As Boolean = False

            '=======================================
            '=     CEK APAKAH ADA SUDAH SIMPAN     =
            '=======================================
            SQL = "select Kode_Perusahaan from EMI_Pembelian_PO_Induk where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & No_Fak & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then

                        SQL = "select a.No_Faktur, a.Persentase, b.Keterangan "
                        SQL = SQL & "from EMI_Detail_PPH_PO_Induk a, EMI_Master_Pajak b "
                        SQL = SQL & "where  a.Kode_Perusahaan = b.Kode_Perusahaan "
                        SQL = SQL & "and a.Kode_Tarif = b.Kode_Tarif "
                        SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "'  "
                        SQL = SQL & "and a.No_Faktur = '" & No_Fak & "' "
                        SQL = SQL & "and a.Flag_PPN is null"
                        Using Dr = OpenTrans(SQL)

                            Do While Dr.Read
                                Dim Lv As ListViewItem
                                Lv = Lv_Data.Items.Add(Dr("Keterangan"))
                                Lv.SubItems.Add(Dr("Persentase") & " %")

                                Dim NilaiPajak As Double = (Val(Dr("Persentase")) / 100) * HilangkanTanda(TxtPO_GrandTotal.Text)
                                Lv.SubItems.Add(Format(NilaiPajak, "N2"))

                                TotalPPH += NilaiPajak
                            Loop

                        End Using


                        hasData = True
                    End If
                End With
            End Using


            If Not hasData Then

                If tempDataPajak.Count = 0 Then
                    CloseConn()
                    MessageBox.Show("Terjadi Kesahlahan pada PPH", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                '==========================
                '=     GET DATA PAJAK     =
                '==========================
                Lv_Data.Items.Clear()
                For i As Integer = 0 To tempDataPajak.Count - 1
                    If tempDataPajak(i).isPPN = True Then
                        Continue For
                    End If

                    Dim Lv As ListViewItem
                    Lv = Lv_Data.Items.Add(tempDataPajak(i).Pajak)
                    Lv.SubItems.Add(tempDataPajak(i).Nilai & " %")

                    Dim NilaiPajak As Double = (Val(tempDataPajak(i).Nilai) / 100) * HilangkanTanda(TxtPO_GrandTotal.Text)
                    Lv.SubItems.Add(Format(NilaiPajak, "N2"))

                    TotalPPH += NilaiPajak
                Next

            End If

            Txt_TotalPPH.Text = Format(TotalPPH, "N2")

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try







    End Sub
End Class
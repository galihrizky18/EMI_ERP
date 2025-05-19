Public Class SD_Detail_PajakPO

    Dim JudulForm As String = "Detail Pajak PO"
    Public KdSupplier As String

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

            '==========================
            '=     GET DATA PAJAK     =
            '==========================
            Lv_Data.Items.Clear()
            SQL = "select Kode_Supplier, Kode_Jenis_Supplier, Kode_Jasa, Kode_Sub_Jasa  from Suppliers where Kode_Perusahaan = '" & KodePerusahaan & "' and  Kode_Supplier = '" & KdSupplier & "'"
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then

                        SQL = "select Keterangan, Tarif "
                        SQL = SQL & "from EMI_Master_Pajak "
                        SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                        SQL = SQL & "and Kode_Tarif in ( "
                        SQL = SQL & "select Kode_Tarif from EMI_Tarif_PPH where Kode_Perusahaan = '" & KodePerusahaan & "'  "
                        SQL = SQL & "and Kode_Jenis_Supplier = '" & .Rows(0).Item("Kode_Jenis_Supplier") & "' and Kode_Jasa = '" & .Rows(0).Item("Kode_Jasa") & "' "
                        SQL = SQL & " and Kode_Sub_Jasa = '" & .Rows(0).Item("Kode_Sub_Jasa") & "' )"
                        Using Ds1 = BindingTrans(SQL)
                            If Ds1.Tables("MyTable").Rows.Count <> 0 Then
                                For i As Integer = 0 To Ds1.Tables("MyTable").Rows().Count - 1

                                    Dim Lv As ListViewItem
                                    Lv = Lv_Data.Items.Add(Ds1.Tables("MyTable").Rows(i).Item("Keterangan"))
                                    Lv.SubItems.Add(Ds1.Tables("MyTable").Rows(i).Item("Tarif") & " %")

                                    Dim NilaiPajak As Double = (Val(Ds1.Tables("MyTable").Rows(i).Item("Tarif")) / 100) * HilangkanTanda(TxtPO_GrandTotal.Text)
                                    Lv.SubItems.Add(Format(NilaiPajak, "N2"))

                                    TotalPPH += NilaiPajak

                                Next
                            Else
                                CloseConn()
                                MessageBox.Show("Data Pajak Tidak Ditemukan", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        End Using

                    Else
                        CloseConn()
                        MessageBox.Show("Suppplier Tidak Ditermukan", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End With
            End Using

            Txt_TotalPPH.Text = Format(TotalPPH, "N2")

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try







    End Sub
End Class
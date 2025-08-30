Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button

Public Class SD_Ubah_Tanggal_PR_Barang_Lain

    Dim arrIndex, arrNoPenawaran, arrSatuanPenawaran, arrHargaPenawaran As New ArrayList
    Dim no_formula, no_inquiry, kode_customer, kode_barang As String

    Public kodeSupplier As String
    Public kodeBarang As String
    Public rowDgv As Integer
    Public cellDgv As Integer

    Private Sub SD_Formulator_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        kosong()
    End Sub

    Private Sub kosong()

    End Sub
    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
        Purchase_Requisition_Barang_Lain.Dgv_DataBarang.Rows(rowDgv).Cells(cellDgv).Value = Format(DTP_Delivery.Value, "dd MMM yyyy")

        Me.Close()
    End Sub

End Class
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button

Public Class SD_Ubah_Tanggal_PR_Barang_Lain

    Dim arrIndex, arrNoPenawaran, arrSatuanPenawaran, arrHargaPenawaran As New ArrayList
    Dim no_formula, no_inquiry, kode_customer, kode_barang As String

    Public kodeSupplier As String
    Public EstTiba As Integer

    Private Sub DTP_Delivery_ValueChanged(sender As Object, e As EventArgs) Handles DTP_Delivery.ValueChanged
        Dim Diff As Integer = DateDiff(DateInterval.Day, tgl_skg, DTP_Delivery.Value)


        If Diff < EstTiba Then
            Dim Msg As String = MessageBox.Show("Tanggal Dibutuhkan Kurang dari Estimasi Tiba,  Tetap Lanjutkan ? ", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If Msg = vbNo Then
                DTP_Delivery.Value = DateAdd(DateInterval.Day, EstTiba + 1, tgl_skg)
            End If
        End If
    End Sub

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
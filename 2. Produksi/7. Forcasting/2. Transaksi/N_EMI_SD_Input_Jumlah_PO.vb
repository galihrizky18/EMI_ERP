Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button




Public Class N_EMI_SD_Input_Jumlah_PO
    Public txtKodeStockOwner1 As String
    Public txtKdBrng1 As String
    Public txtNamaBarang1 As String
    Public txtSatuan1 As String
    Public txtKetJenisProduk1 As String
    Public txt_NoUrut1 As String
    Public txtIdJenisProduk1 As String
    Public txtKet1 As String
    Public txtNoRvSch1 As String
    Public txtJmlhSdhPO1 As String
    Public txtJmlhSchedule1 As String
    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
        If txtJmlHInginPO.Text < 1 Then
            MessageBox.Show("Jumlah PO minimal 1 untuk bisa melanjutkan proses!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Dim nilaiSdhPO As Double = Val(HilangkanTanda(txtSdhPO.Text))
        Dim jmlhSchedule As Double = Val(HilangkanTanda(txtJumlahSchedule.Text))

        Dim jmlhInginPO As Double = Val(txtJmlHInginPO.Text)

        If jmlhSchedule < (Val(nilaiSdhPO) + jmlhInginPO) Then
            MessageBox.Show("Jumlah yang ingin di PO melebihi jumlah schedule!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        EMI_Production_Order.parentForm = "schedule"

            EMI_Production_Order.LvData.Columns.Clear()
            EMI_Production_Order.LvData.Clear()

            EMI_Production_Order.LvOrder.Columns.Clear()
            EMI_Production_Order.LvOrder.Clear()

            EMI_Production_Order.LvBahan.Columns.Clear()
            EMI_Production_Order.LvBahan.Clear()

            EMI_Production_Order.LvBahanNew.Columns.Clear()
            EMI_Production_Order.LvBahanNew.Clear()


            EMI_Production_Order.LvPackaging.Columns.Clear()
            EMI_Production_Order.LvPackaging.Clear()



            EMI_Production_Order.LvPackagingNew.Columns.Clear()
            EMI_Production_Order.LvPackagingNew.Clear()




            Dim lvw As New ListViewItem("PS")
            lvw.SubItems.Add(txtKodeStockOwner1) '1
            lvw.SubItems.Add("-") '2
            lvw.SubItems.Add("-") '3
            lvw.SubItems.Add(txtKdBrng1) '4
            lvw.SubItems.Add(txtNamaBarang.Text) '5
            lvw.SubItems.Add(txtJmlHInginPO.Text) '6 Barang
            lvw.SubItems.Add(Format(0, "N2")) '7 Barang
            lvw.SubItems.Add(txtSatuan1) '8
            lvw.SubItems.Add(txtKetJenisProduk1) '9
            lvw.SubItems.Add(txt_NoUrut1) '10
            lvw.SubItems.Add(txtIdJenisProduk1) '11
            lvw.SubItems.Add(txtKet1) '12
            lvw.SubItems.Add("Produksi")
            lvw.SubItems.Add("")




            EMI_Production_Order.LvData.Items.Add(lvw)

            EMI_Production_Order.RV_Schedule = txtNoRvSch1
            EMI_Production_Order.UrutOtoSchedule = txt_NoUrut1

            EMI_Production_Order.NoFakturFromSchedule = Nothing
            EMI_Production_Order.flagSudahPOSchedule = "T"
            EMI_Production_Order.CekPackingSetFromSchedule(txtKdBrng.Text)
            EMI_Production_Order.ShowDialog()
            Me.Close()

    End Sub

    Private Sub N_EMI_SD_Input_Jumlah_PO_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtJmlHInginPO.Text = 0

        txtKdBrng.Text = txtKdBrng1
        txtNamaBarang.Text = txtNamaBarang1
        txtSatuan.Text = txtSatuan1
        txtSdhPO.Text = Format(Val(txtJmlhSdhPO1), "N2")
        txtJumlahSchedule.Text = Format(Val(txtJmlhSchedule1), "N2")
    End Sub

    Private Sub txtJmlhInginDiubah_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtJmlHInginPO.KeyPress
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(Asc("."))) Then e.KeyChar = Chr(0)
    End Sub
End Class
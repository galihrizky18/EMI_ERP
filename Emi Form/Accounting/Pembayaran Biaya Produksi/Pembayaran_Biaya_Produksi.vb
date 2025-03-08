Public Class Pembayaran_Biaya_Produksi

    Dim JudulForm As String = "Pembayaran Biaya Produksi"

    Dim arrBiaya As New ArrayList From {"Biaya Air", "Biaya Listrik"}
    Dim arrMesin As New ArrayList From {"Robotic Assembly", "Grinding Machine", "Water Jet Cutter", "Hydraulic Press", "MIG Welding Machine", "Polishing Machine", "Shrink Wrap Machine", "Tes 2", "Laser Cutter"}

    Dim arrNoTagihan As New ArrayList From {
    New ArrayList From {"1012345678", "541234567890"}, ' Robotic Assembly
    New ArrayList From {"1023456789", "542345678901"}, ' Grinding Machine
    New ArrayList From {"1034567890", "543456789012"}, ' Water Jet Cutter
    New ArrayList From {"1045678901", "544567890123"}, ' Hydraulic Press
    New ArrayList From {"1056789012", "545678901234"}, ' MIG Welding Machine
    New ArrayList From {"1067890123", "546789012345"}, ' Polishing Machine
    New ArrayList From {"1078901234", "547890123456"}, ' Shrink Wrap Machine
    New ArrayList From {"1089012345", "548901234567"}, ' Tes 2
    New ArrayList From {"1090123456", "549012345678"}  ' Laser Cutter
}

    Dim item_Tgl As Integer = 0
    Dim item_JnsBiaya As Integer = 1
    Dim item_Mesin As Integer = 2
    Dim item_NoTagihan As Integer = 3
    Dim item_Biaya As Integer = 4

    Private Sub Pembayaran_Biaya_Produksi_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        intialListview()
        Kosong()

    End Sub

    Private Sub Kosong()

        Dtp_Tgl.Value = Date.Now

        Txt_Keterangan.Text = ""
        Txt_NoTagihan.Text = ""
        Txt_Biaya.Text = "AS"

        Cmb_JenisBiaya.Items.Clear()

        For i As Integer = 0 To arrBiaya.Count - 1
            Cmb_JenisBiaya.Items.Add(arrBiaya(i))
        Next
        Cmb_JenisBiaya.SelectedIndex = -1

        Cmb_Mesin.Items.Clear()

        For i As Integer = 0 To arrMesin.Count - 1
            Cmb_Mesin.Items.Add(arrMesin(i))
        Next
        Cmb_Mesin.SelectedIndex = -1

        Cmb_Filter.Items.Clear()
        Cmb_Filter.Items.Add("Tanggal")
        Cmb_Filter.Items.Add("Jenis Biaya")
        Cmb_Filter.Items.Add("Mesin")
        Cmb_Filter.Items.Add("No Tagihan")
        Cmb_Filter.SelectedIndex = -1

        LoadLv()

    End Sub

    Private Sub KosongSebagian()
        Dtp_Tgl.Value = Date.Now

        Txt_Keterangan.Text = ""
        Txt_NoTagihan.Text = ""
        Txt_Biaya.Text = ""

        Cmb_JenisBiaya.Items.Clear()

        For i As Integer = 0 To arrBiaya.Count - 1
            Cmb_JenisBiaya.Items.Add(arrBiaya(i))
        Next
        Cmb_JenisBiaya.SelectedIndex = -1

        Cmb_Mesin.Items.Clear()

        For i As Integer = 0 To arrMesin.Count - 1
            Cmb_Mesin.Items.Add(arrMesin(i))
        Next
        Cmb_Mesin.SelectedIndex = -1

        Cmb_Filter.Items.Clear()
        Cmb_Filter.Items.Add("Tanggal")
        Cmb_Filter.Items.Add("Jenis Biaya")
        Cmb_Filter.Items.Add("Mesin")
        Cmb_Filter.Items.Add("No Tagihan")
        Cmb_Filter.SelectedIndex = -1
    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        KosongSebagian()
    End Sub

    Private Sub intialListview()

        Lv_data.Columns.Clear()
        Lv_data.Columns.Add("Tanggal", 100, HorizontalAlignment.Center)
        Lv_data.Columns.Add("Jenis Biaya", 100, HorizontalAlignment.Left)
        Lv_data.Columns.Add("Mesin", 100, HorizontalAlignment.Left)
        Lv_data.Columns.Add("No Tagihan", 100, HorizontalAlignment.Left)
        Lv_data.Columns.Add("Biaya", 100, HorizontalAlignment.Right)
        Lv_data.View = View.Details

    End Sub

    Private Sub Cmb_Mesin_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Mesin.SelectedIndexChanged
        If Cmb_Mesin.SelectedIndex = -1 Or Cmb_JenisBiaya.SelectedIndex = -1 Then Exit Sub

        Dim indexBiaya As Integer = Cmb_JenisBiaya.SelectedIndex
        Dim indexMesin As Integer = Cmb_Mesin.SelectedIndex

        Txt_NoTagihan.Text = arrNoTagihan(indexMesin)(indexBiaya)

    End Sub

    Private Sub Txt_Biaya_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Biaya.KeyPress
        If Not (Char.IsDigit(e.KeyChar) OrElse e.KeyChar = "."c OrElse e.KeyChar = ControlChars.Back) Then
            e.Handled = True
        End If

        If e.KeyChar = "."c AndAlso Txt_Biaya.Text.Contains(".") Then
            e.Handled = True
        End If
    End Sub

    Private Sub LoadLv()

        Dim Lv As ListViewItem
        Lv = Lv_data.Items.Add("13 Feb 2025")
        Lv.SubItems.Add("Listrik")
        Lv.SubItems.Add("Hydraulic Press")
        Lv.SubItems.Add("1056789012")
        Lv.SubItems.Add("2.300.000,00")

    End Sub

    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click

        If Txt_Keterangan.Text = "" Then
            MessageBox.Show("Keterangan Tidak Boleh Kosong", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_Keterangan.Focus()
            Exit Sub

        ElseIf Cmb_JenisBiaya.SelectedIndex = -1 Then
            MessageBox.Show("Pilih Biaya Dahulu", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_JenisBiaya.Focus()
            Exit Sub

        ElseIf Cmb_Mesin.SelectedIndex = -1 Then
            MessageBox.Show("Pilih Mesin Dahulu", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Mesin.Focus()
            Exit Sub

        ElseIf Txt_NoTagihan.Text = "" Then
            MessageBox.Show("No Tagihan Tidak Ditemukan", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_NoTagihan.Focus()
            Exit Sub

        ElseIf Txt_Biaya.Text = "" Then
            MessageBox.Show("Biaya Tidak Boleh Kosnog", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_Biaya.Focus()
            Exit Sub
        End If

        Dim Lv As ListViewItem
        Lv = Lv_data.Items.Add(Format(Dtp_Tgl.Value, "dd MMM yyyy"))
        Lv.SubItems.Add(Cmb_JenisBiaya.Text)
        Lv.SubItems.Add(Cmb_Mesin.Text)
        Lv.SubItems.Add(Txt_NoTagihan.Text)
        Lv.SubItems.Add(Format(Val(HilangkanTanda(Txt_Biaya.Text)), "N2"))

        MessageBox.Show("Data Berhasil Di simpan", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Information)
        KosongSebagian()
    End Sub

    Private Sub Txt_Biaya_Leave(sender As Object, e As EventArgs) Handles Txt_Biaya.Leave
        If Txt_Biaya.Text.Trim.Length = 0 Then Exit Sub

        Txt_Biaya.Text = Format(Val(HilangkanTanda(Txt_Biaya.Text)), "N2")
    End Sub

    Private Sub Txt_Biaya_Enter(sender As Object, e As EventArgs) Handles Txt_Biaya.Enter
        If Txt_Biaya.Text.Trim.Length = 0 Then Exit Sub

        Txt_Biaya.Text = Format(Val(HilangkanTanda(Txt_Biaya.Text)), "N0")
    End Sub



End Class
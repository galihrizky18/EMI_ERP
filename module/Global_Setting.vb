
Imports System.Drawing.Printing
Imports System.IO.Ports
Public Class Global_Setting
    Dim arrKd As New ArrayList

    Private Sub Get_Printer_List(Nama_Combo As ComboBox)
        For Each Printer_Name_List As String In PrinterSettings.InstalledPrinters
            Nama_Combo.Items.Add(Printer_Name_List)
        Next
    End Sub

    Public Sub SyncComboBoxWithSettings(comboBox As ComboBox, settingValue As String)
        If String.IsNullOrWhiteSpace(settingValue) Then
            ' Jika nilai pengaturan kosong, tidak ada yang dipilih
            comboBox.SelectedIndex = -1
        Else
            Dim found As Boolean = False

            ' Cari nilai dalam ComboBox
            For i As Integer = 0 To comboBox.Items.Count - 1
                If settingValue.ToUpper() = comboBox.Items(i).ToString().ToUpper() Then
                    comboBox.SelectedIndex = i
                    found = True
                    Exit For
                End If
            Next

            ' Jika nilai tidak ditemukan, tambahkan ke ComboBox dan pilih
            If Not found Then
                comboBox.Items.Add(settingValue)
                comboBox.SelectedIndex = comboBox.Items.Count - 1
            End If
        End If
    End Sub


    Private Sub Printer_Setting_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        Get_Printer_List(ComboBox3)
        SyncComboBoxWithSettings(ComboBox3, My.Settings.Prt_Name)

        Get_Printer_List(ComboBox4)
        SyncComboBoxWithSettings(ComboBox4, My.Settings.Prt_Name_TS)

        Get_Printer_List(ComboBox5)
        SyncComboBoxWithSettings(ComboBox5, My.Settings.Prt_Name_SPB)

        Get_Printer_List(ComboBox6)
        SyncComboBoxWithSettings(ComboBox6, My.Settings.Prt_Name_BPB)

        Get_Printer_List(ComboBox7)
        SyncComboBoxWithSettings(ComboBox7, My.Settings.Prt_Name_Bukti_Timbang)

        Get_Printer_List(ComboBox8)
        SyncComboBoxWithSettings(ComboBox8, My.Settings.Prt_Name_2)

        Get_Printer_List(cmbBarcode)
        SyncComboBoxWithSettings(cmbBarcode, My.Settings.Prt_Barcode)

        Get_Printer_List(cmbQC)
        SyncComboBoxWithSettings(cmbQC, My.Settings.Prt_QC)

        Get_Printer_List(Cmb_BarcodeQC)
        SyncComboBoxWithSettings(Cmb_BarcodeQC, My.Settings.Prt_Barcode_QC)


        cmbCOMFloorScale.Items.Clear()
        cmbCOMFloorScale.Items.Add("COM1")
        cmbCOMFloorScale.Items.Add("COM2")
        cmbCOMFloorScale.Items.Add("COM3")
        cmbCOMFloorScale.Items.Add("COM4")
        cmbCOMFloorScale.Items.Add("COM5")
        cmbCOMFloorScale.Items.Add("COM6")
        cmbCOMFloorScale.Items.Add("COM7")
        cmbCOMFloorScale.Items.Add("COM8")
        cmbCOMFloorScale.Items.Add("COM9")

        cmbCOMFloorScale.Text = My.Settings.Port_Timbangan


        Cb_Tb1.Items.Clear()
        Cb_Tb1.Items.Add("COM1")
        Cb_Tb1.Items.Add("COM2")
        Cb_Tb1.Items.Add("COM3")
        Cb_Tb1.Items.Add("COM4")
        Cb_Tb1.Items.Add("COM5")
        Cb_Tb1.Items.Add("COM6")
        Cb_Tb1.Items.Add("COM7")
        Cb_Tb1.Items.Add("COM8")
        Cb_Tb1.Items.Add("COM9")
        Cb_Tb1.Items.Add("COM10")

        Cb_Tb1.Text = My.Settings.Port_Timbangan1

        Cb_Tb2.Items.Clear()
        Cb_Tb2.Items.Add("COM1")
        Cb_Tb2.Items.Add("COM2")
        Cb_Tb2.Items.Add("COM3")
        Cb_Tb2.Items.Add("COM4")
        Cb_Tb2.Items.Add("COM5")
        Cb_Tb2.Items.Add("COM6")
        Cb_Tb2.Items.Add("COM7")
        Cb_Tb2.Items.Add("COM8")
        Cb_Tb2.Items.Add("COM9")
        Cb_Tb2.Items.Add("COM10")

        Cb_Tb2.Text = My.Settings.Port_Timbangan2

        Cb_Tb3.Items.Clear()
        Cb_Tb3.Items.Add("COM1")
        Cb_Tb3.Items.Add("COM2")
        Cb_Tb3.Items.Add("COM3")
        Cb_Tb3.Items.Add("COM4")
        Cb_Tb3.Items.Add("COM5")
        Cb_Tb3.Items.Add("COM6")
        Cb_Tb3.Items.Add("COM7")
        Cb_Tb3.Items.Add("COM8")
        Cb_Tb3.Items.Add("COM9")
        Cb_Tb3.Items.Add("COM10")

        Cb_Tb3.Text = My.Settings.Port_Timbangan3
    End Sub

    Private Sub SetComboBoxWithCheck(cb As ComboBox, savedPort As String, lbl As Label)
        If Not String.IsNullOrEmpty(savedPort) Then
            cb.SelectedItem = savedPort
            If cb.Items.Contains(savedPort) Then
                lbl.Text = "Port aktif"
                lbl.ForeColor = Color.Green
            Else
                lbl.Text = "Port tidak aktif"
                lbl.ForeColor = Color.Red
            End If
        Else
            lbl.Text = "Tidak ada port diset"
            lbl.ForeColor = Color.Black
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If ComboBox3.SelectedIndex = -1 Then
            MessageBox.Show("Printer penjualan harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox3.Focus()
            Exit Sub
        ElseIf ComboBox4.SelectedIndex = -1 Then
            MessageBox.Show("Printer TS harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox4.Focus()
            Exit Sub
        ElseIf ComboBox5.SelectedIndex = -1 Then
            MessageBox.Show("Printer SPB harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox5.Focus()
            Exit Sub
        ElseIf ComboBox6.SelectedIndex = -1 Then
            MessageBox.Show("Printer BPB harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox6.Focus()
            Exit Sub
        ElseIf ComboBox7.SelectedIndex = -1 Then
            MessageBox.Show("Printer 1 harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox7.Focus()
            Exit Sub
        ElseIf ComboBox8.SelectedIndex = -1 Then
            MessageBox.Show("Printer 2 harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox8.Focus()
            Exit Sub
        ElseIf cmbBarcode.SelectedIndex = -1 Then
            MessageBox.Show("Printer Barcode harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            cmbBarcode.Focus()
            Exit Sub
        ElseIf cmbQC.SelectedIndex = -1 Then
            MessageBox.Show("Printer QC harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            cmbQC.Focus()
            Exit Sub
        ElseIf cmbCOMFloorScale.SelectedIndex = -1 Then
            MessageBox.Show("COM Floor scale harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            cmbCOMFloorScale.Focus()
        ElseIf Cmb_BarcodeQC.SelectedIndex = -1 Then
            MessageBox.Show("Printer Barcode harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            cmbCOMFloorScale.Focus()
            Exit Sub
        End If

        My.Settings.Prt_Name = ComboBox3.Text.Trim
        My.Settings.Prt_Name_TS = ComboBox4.Text
        My.Settings.Prt_Name_SPB = ComboBox5.Text
        My.Settings.Prt_Name_BPB = ComboBox6.Text
        My.Settings.Prt_Name_Bukti_Timbang = ComboBox7.Text
        My.Settings.Prt_Name_2 = ComboBox8.Text
        My.Settings.Prt_Barcode = cmbBarcode.Text
        My.Settings.Prt_QC = cmbQC.Text
        My.Settings.Port_Timbangan = cmbCOMFloorScale.Text
        My.Settings.Prt_Barcode_QC = Cmb_BarcodeQC.Text
        My.Settings.Port_Timbangan1 = Cb_Tb1.Text
        My.Settings.Port_Timbangan2 = Cb_Tb2.Text
        My.Settings.Port_Timbangan3 = Cb_Tb3.Text


        PrinterName = My.Settings.Prt_Name
        PrinterNameTS = My.Settings.Prt_Name_TS
        PrinterNameSPB = My.Settings.Prt_Name_SPB
        PrinterNameBPB = My.Settings.Prt_Name_BPB
        PrinterNameBuktiTimbang = My.Settings.Prt_Name_Bukti_Timbang
        PrinterName2 = My.Settings.Prt_Name_2
        PrinterBarcode = My.Settings.Prt_Barcode
        PrinterQC = My.Settings.Prt_QC
        Port_Timbangan = My.Settings.Port_Timbangan
        Port_Timbangan1 = My.Settings.Port_Timbangan1
        Port_Timbangan2 = My.Settings.Port_Timbangan2
        Port_Timbangan3 = My.Settings.Port_Timbangan3
        PrinterBarcodeQC = My.Settings.Prt_Barcode_QC

        My.Settings.Save()

        MessageBox.Show("Berhasil disimpan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End
    End Sub

    'Private Sub Printer_Setting_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SizeChanged
    '    Label2.Size = New Point(Me.Width, 33)
    'End Sub


    Private Sub ComboBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox3.KeyPress
        If e.KeyChar = Chr(13) Then ComboBox4.Focus()
    End Sub

    Private Sub ComboBox4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox4.KeyPress
        If e.KeyChar = Chr(13) Then ComboBox5.Focus()
    End Sub

    Private Sub ComboBox5_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox5.KeyPress
        If e.KeyChar = Chr(13) Then ComboBox6.Focus()
    End Sub

    Private Sub ComboBox6_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox6.KeyPress
        If e.KeyChar = Chr(13) Then ComboBox7.Focus()
    End Sub

    Private Sub ComboBox7_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox7.KeyPress
        If e.KeyChar = Chr(13) Then ComboBox8.Focus()
    End Sub

    Private Sub ComboBox8_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox8.KeyPress
        If e.KeyChar = Chr(13) Then cmbBarcode.Focus()
    End Sub

    Private Sub Cmb_BarcodeQC_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_BarcodeQC.KeyPress
        If e.KeyChar = Chr(13) Then Button1.Focus()
    End Sub
End Class
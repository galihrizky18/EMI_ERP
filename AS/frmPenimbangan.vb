Imports System
Imports System.ComponentModel
Imports System.Threading
Imports System.IO.Ports
Imports System.Text.RegularExpressions
Imports System.Net
Imports System.Globalization
Imports Newtonsoft.Json
Public Class frmPenimbangan


    Dim myPort As Array
    Delegate Sub SetTextCallback(ByVal [text] As String)


    Private Sub frmPenimbangan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'getPicture()
        waktu()
        tgl()

        'txtpcname.Text = Form1.lblpcname.Text
        'txtip.Text = Form1.lblip.Text


        'MsgBox(NumToRoman(Year))
        'MsgBox(NumToRoman(Month))

        'fillConfig()


        kosongCombo()

        idtransaksi.Text = "0"
        TextBox8.Visible = False
        TextBox9.Visible = True
        'Form1.FrmDaftarTimbang1.refreshDataTimbang()
        fillCombo()
        getDefaultComCOnfig()
        noTransaksi()
        Panel5.Visible = False
    End Sub

    Private Sub fillCombo()
        cboPerangkat.DataSource = Nothing
        cboProduk.DataSource = Nothing
        'cboTransporters.DataSource = Nothing
        cboSopir.DataSource = Nothing
        'CBOADDJENISTIMBANG(ComboBox1)
        'fillcbo("SELECT * FROM `MItem` WHERE `MBussiness_ID`=" & Form1.Label15.Text & "", cboProduk, "ItemName", "MItem_ID", "-- Pilih Produk --")
        ''fillcbo("SELECT * FROM `MVehicleData` WHERE `MBussiness_ID`=" & Form1.Label15.Text & "", cboTransporters, "vehiclePoliceNo", "MVehicleData_ID", "-- Pilih Transporter --")
        'fillcbo("SELECT * FROM `MWeightConfig` WHERE `MBussiness_ID`=" & Form1.Label15.Text & "", cboPerangkat, "pcName", "MWeightConfig_ID", "-- Pilih Perangkat --")
        'fillcbo("SELECT * FROM `MDriver` WHERE `MBussiness_ID`=" & Form1.Label15.Text & "", cboSopir, "driverName", "MDriver_ID", "-- Pilih Sopir --")

    End Sub

    Public Sub fillComboSpoir()

        cboSopir.DataSource = Nothing
        'fillcbo("SELECT * FROM `MDriver` WHERE `MBussiness_ID`=" & Form1.Label15.Text & "", cboSopir, "driverName", "MDriver_ID", "-- Pilih Sopir --")

    End Sub

    Public Sub fillComboPerangkat()

        cboPerangkat.DataSource = Nothing
        'fillcbo("SELECT * FROM `MWeightConfig` WHERE `MBussiness_ID`=" & Form1.Label15.Text & "", cboPerangkat, "pcName", "MWeightConfig_ID", "-- Pilih Perangkat --")

    End Sub
    Private Sub noTransaksi()
        Dim Year As Integer
        Dim Month As Integer
        Year = Convert.ToInt32(Now.ToString("yyyy"))
        Month = Convert.ToInt32(Now.ToString("MM"))
        'Dim urut As String = getAutoNumber()
        'Dim timbangan As String = UCase(cboPerangkat.Text)
        'Dim timbangan As String = UCase(Form1.Label18.Text)
        'Dim a As String = Microsoft.VisualBasic.Right(timbangan, 5)
        'Dim Hasil As String = Strings.Mid(a, 2, 3)
        'Dim namacostumer As String = UCase(cboCustomer.Text)
        'Dim bulan As String = NumToRoman(Month)
        Dim tahun As String = Year

        'TextBox9.Text = urut & "/" & Hasil & "-" & namacostumer & "/" & bulan & "/" & tahun
    End Sub
    Sub OpenPort()
        Try

            If SerialPort1.IsOpen = True Then
                SerialPort1.Close()
            End If

            If txtcomport.Text = "" Then
                MsgBox("Silahkan Lakukan Setting Konfigurasi")
                Exit Sub
            Else

                SerialPort1.PortName = txtcomport.Text
                SerialPort1.BaudRate = txtbaudrate.Text

                SerialPort1.Parity = IO.Ports.Parity.None
                SerialPort1.StopBits = IO.Ports.StopBits.One
                SerialPort1.DataBits = 7
                SerialPort1.Open()
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Sub tgl()
        Dim CurrentDateTime As String = DateTime.Now.ToString("yyyy-MM-dd")
        'Dim CurrentDateTime As String = Format(Now, "dddd, dd – MMMM – yyyy")
        Label16.Text = CurrentDateTime

    End Sub
    Sub waktu()

        'HR'
        Dim hr As String = TimeOfDay.Hour
        If hr = 1 Then
            hr = "0" + hr
        End If
        If hr = 2 Then
            hr = "0" + hr
        End If
        If hr = 3 Then
            hr = "0" + hr
        End If
        If hr = 4 Then
            hr = "0" + hr
        End If
        If hr = 5 Then
            hr = "0" + hr
        End If
        If hr = 6 Then
            hr = "0" + hr
        End If
        If hr = 7 Then
            hr = "0" + hr
        End If
        If hr = 8 Then
            hr = "0" + hr
        End If
        If hr = 9 Then
            hr = "0" + hr
        End If
        If hr = 0 Then
            hr = "0" + hr
        End If
        'MN'
        Dim mn As String = TimeOfDay.Minute
        If mn = 1 Then
            mn = "0" + mn
        End If
        If mn = 2 Then
            mn = "0" + mn
        End If
        If mn = 3 Then
            mn = "0" + mn
        End If
        If mn = 4 Then
            mn = "0" + mn
        End If
        If mn = 5 Then
            mn = "0" + mn
        End If
        If mn = 6 Then
            mn = "0" + mn
        End If
        If mn = 7 Then
            mn = "0" + mn
        End If
        If mn = 8 Then
            mn = "0" + mn
        End If
        If mn = 9 Then
            mn = "0" + mn
        End If
        If mn = 0 Then
            mn = "0" + mn
        End If
        'SC'
        Dim sc As String = TimeOfDay.Second
        If sc = 1 Then
            sc = "0" + sc
        End If
        If sc = 2 Then
            sc = "0" + sc
        End If
        If sc = 3 Then
            sc = "0" + sc
        End If
        If sc = 4 Then
            sc = "0" + sc
        End If
        If sc = 5 Then
            sc = "0" + sc
        End If
        If sc = 6 Then
            sc = "0" + sc
        End If
        If sc = 7 Then
            sc = "0" + sc
        End If
        If sc = 8 Then
            sc = "0" + sc
        End If
        If sc = 9 Then
            sc = "0" + sc
        End If
        If sc = 0 Then
            sc = "0" + sc
        End If
        Label9.Text = hr + ":" + mn + ":" + sc

    End Sub



    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        waktu()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        updateVendorSupplier()
    End Sub
    Private Sub updateVendorSupplier()
        'Dim batchId As Integer
        Dim costype As String
        'batchId = ComboBox1.SelectedIndex
        'If ComboBox1.Text = "IN" Then
        '    costype = "VN"
        '    cboCustomer.DataSource = Nothing
        '    'fillcbo("SELECT * FROM `tblmstvendor` WHERE MSTVENDORTYPE='SUPPLIER' ", cboCustomer, "MSTVENDORNAMA", "MSTVENDORID", "-- Pilih Vendor --")
        '    fillcbo("SELECT * FROM `MVendor` WHERE `vendorType`='VN' AND `MBussiness_ID`=" & Form1.Label15.Text & "", cboCustomer, "vendorName", "MVendor_ID", "-- Pilih Vendor --")
        '    Panel5.Visible = False
        'ElseIf ComboBox1.Text = "OUT" Then
        '    cboCustomer.DataSource = Nothing
        '    costype = "CL"
        '    'fillcbo("SELECT * FROM `tblmstvendor` WHERE MSTVENDORTYPE='CUSTOMER' ", cboCustomer, "MSTVENDORNAMA", "MSTVENDORID", "-- Pilih Vendor --")
        '    fillcbo("SELECT * FROM `MVendor` WHERE `vendorType`='CL' AND `MBussiness_ID`=" & Form1.Label15.Text & "", cboCustomer, "vendorName", "MVendor_ID", "-- Pilih Vendor --")
        '    Panel5.Visible = True
        'End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        kosongCombo()
        noTransaksi()
        idtransaksi.Text = "0"
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'Try

        '    If (ComboBox1.Text = "" Or cboProduk.Text = "" Or cboCustomer.Text = "" Or cboTransporters.Text = "" Or TextBox2.Text = "") Then
        '        MessageBox.Show("Silahkan isi data dengan benar", "No entry", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        Exit Sub
        '    End If


        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'End Try

        'OpenPort()
        'Try
        '    If SerialPort1.IsOpen = True Then
        '        PictureBox2.Image = Image.FromFile(Application.StartupPath + "\picture\disconnected.png")
        '        PictureBox2.SizeMode = PictureBoxSizeMode.StretchImage
        '        Button1.Text = "&Connect"
        '        SerialPort1.Close()
        '    Else
        '        If txtcomport.Text = "" Then
        '            MsgBox("Silahkan Pilih Perangkat")
        '            Button1.Text = "&Connect"
        '            Exit Sub
        '        Else

        '            SerialPort1.PortName = txtcomport.Text
        '            SerialPort1.BaudRate = txtbaudrate.Text

        '            SerialPort1.Parity = IO.Ports.Parity.None
        '            SerialPort1.StopBits = IO.Ports.StopBits.One
        '            SerialPort1.Handshake = IO.Ports.Handshake.None
        '            SerialPort1.DataBits = txtparity.Text
        '            SerialPort1.Encoding = System.Text.Encoding.Default
        '            SerialPort1.Open()
        '            'If SerialPort1.IsOpen = True Then
        '            PictureBox2.Image = Image.FromFile(Application.StartupPath + "\picture\connected.png")
        '            PictureBox2.SizeMode = PictureBoxSizeMode.StretchImage
        '            Button1.Text = "&Disconnect"
        '            'End If

        '        End If

        '    End If

        'Catch ex As Exception
        '    'If SerialPort1.IsOpen = True Then
        '    SerialPort1.Close()
        '    PictureBox2.Image = Image.FromFile(Application.StartupPath + "\picture\disconnected.png")
        '    PictureBox2.SizeMode = PictureBoxSizeMode.StretchImage
        '    Button1.Text = "&Connect"
        '    'End If
        '    MsgBox(ex.Message)
        'End Try


        If (Button1.Text = "Connect") Then
            If (txtcomport.Text = "") Then
                MessageBox.Show("Silahkan pilih perangkat timbang", "Perangkat", MessageBoxButtons.OK, MessageBoxIcon.Error)

            Else
                Try
                    SerialPort1.PortName = txtcomport.Text
                    SerialPort1.BaudRate = txtbaudrate.Text
                    SerialPort1.Parity = IO.Ports.Parity.None
                    SerialPort1.StopBits = IO.Ports.StopBits.One
                    SerialPort1.Handshake = IO.Ports.Handshake.None
                    SerialPort1.DataBits = txtparity.Text
                    SerialPort1.Encoding = System.Text.Encoding.Default
                    SerialPort1.Open()
                    Button1.Text = "Dis-connect"
                    PictureBox2.Image = Image.FromFile(Application.StartupPath + "\picture\connected.png")
                    PictureBox2.SizeMode = PictureBoxSizeMode.StretchImage
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            End If
        Else
            SerialPort1.Close()
            Button1.Text = "Connect"
            PictureBox2.Image = Image.FromFile(Application.StartupPath + "\picture\disconnected.png")
            PictureBox2.SizeMode = PictureBoxSizeMode.StretchImage
        End If

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        kosongCombo()
    End Sub

    Private Sub kosongCombo()
        ComboBox1.Text = ""
        cboProduk.Text = ""
        'cboTransporters.Text = ""
        cboCustomer.Text = ""
        'cboPerangkat.Text = ""
        cboSopir.Text = ""
        'CBOADDJENISTIMBANG(ComboBox1)
    End Sub

    Public Sub fillConfig()
        Try

            'SQL = "SELECT * FROM `MWeightConfig` WHERE `ip`= '" & txtip.Text & "' AND `MBussiness_ID`=" & Form1.Label15.Text & ""
            'sql = "SELECT * FROM `tblconfig` WHERE `pcname` = '" & txtpcname.Text & "' AND `ip`= '" & txtip.Text & "'"

            'reloadtxt(SQL)
            'txtbaudrate.Text = dt.Rows(0).Item(4).ToString
            'txtcomport.Text = dt.Rows(0).Item(3).ToString
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        SerialPort1.Close()             'Close our Serial Port
    End Sub

    Private Sub ReceivedText(ByVal [text] As String)
        If Me.TextBox3.InvokeRequired Then
            Dim x As New SetTextCallback(AddressOf ReceivedText)
            Me.Invoke(x, New Object() {(text)})
        Else

            If text.EndsWith("kg") Then
                lblWeight.Invoke(New MethodInvoker(Sub()
                                                       lblWeight.Text = Integer.Parse(Regex.Replace(text, "[^\d]", ""))
                                                       lblWeight.Refresh()
                                                   End Sub))

                Label10.Invoke(New MethodInvoker(Sub()
                                                     Label10.Text = Integer.Parse(Regex.Replace(text, "[^\d]", ""))
                                                     Label10.Refresh()
                                                 End Sub))
            End If
        End If
    End Sub

    Private Sub ReceivedTextNew(ByVal [text] As String)

        Dim reg As New Regex("[^0-9]")
        If Me.TextBox3.InvokeRequired Then
            Dim x As New SetTextCallback(AddressOf ReceivedTextNew)
            Me.Invoke(x, New Object() {(text)})
        Else

            Dim tempData As String = text
            If (text IsNot Nothing) AndAlso (text.Trim().Length <> 0) Then
                Me.TextBox3.Text = text
                If text.StartsWith("ST") Then
                Else
                    If TextBox3.Text = Nothing Or TextBox3.Text = "" Or TextBox3.Text = "0" Then
                        TextBox4.Text = "0"
                    Else
                        TextBox4.Text = Format(Val(TextBox3.Text), "###,###")
                        TextBox4.SelectionStart = Len(TextBox4.Text)
                    End If
                End If



            End If
        End If
    End Sub

    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click
        'frmShow(frmPopKendaraan)
    End Sub

    'Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
    '    Dim nopolPolisi = Me.txtID.Text
    '    sql = "SELECT * FROM `MGoodsFlow` WHERE `MVehicleData_ID` = " & nopolPolisi & "' AND `FINISH`= 0 ORDER BY `MVehicleData_ID` DESC LIMIT 1"
    '    result = RETRIEVESINGLE_DIS_MSG(sql)
    '    MsgBox(nopolPolisi.ToString)
    '    'jika ada maka update timbangan ke-2
    '    If result = True Then
    '        Try

    '            MsgBox("ada")
    '            cboPerangkat.DataSource = Nothing
    '            cboProduk.DataSource = Nothing
    '            cboTransporters.DataSource = Nothing
    '            cboSopir.DataSource = Nothing
    '            CBOADDJENISTIMBANG(ComboBox1)
    '            fillcbo("SELECT * FROM `MItem`", cboProduk, "ItemName", "MItem_ID", "-- Pilih Produk --")
    '            fillcbo("SELECT * FROM `MVehicleData`", cboTransporters, "vehiclePoliceNo", "MVehicleData_ID", "-- Pilih Transporter --")
    '            fillcbo("SELECT * FROM `MWeightConfig`", cboPerangkat, "pcName", "MWeightConfig_ID", "-- Pilih Perangkat --")
    '            fillcbo("SELECT * FROM `MDriver`  ", cboSopir, "driverName", "MDriver_ID", "-- Pilih Sopir --")

    '            'MGoodsFlow_ID 
    '            'FlowType()
    '            'MBussiness_ID()
    '            'Date 
    '            'Bruto(Tarra)
    '            'Netto()
    '            'createdBy()
    '            'createdAt()
    '            'updatedAt()
    '            'percentageDeduction()
    '            'qtyDeduction()
    '            'MDriver_ID()
    '            'MVendor_ID()
    '            'MVehicleData_ID()
    '            'NOTRANS()
    '            'FINISH()

    '            sql = "SELECT * FROM `MGoodsFlow` WHERE `MVehicleData_ID` = '" & nopolPolisi & "' AND `FINISH`= 0 ORDER BY `MVehicleData_ID` DESC LIMIT 1"
    '            reloadtxt(sql)
    '            idtransaksi.Text = dt.Rows(0).Item(0).ToString
    '            'TextBox9.Visible = False
    '            'TextBox8.Visible = True
    '            TextBox8.Text = dt.Rows(0).Item(14).ToString



    '            cboProduk.SelectedValue = dt.Rows(0).Item(15).ToString
    '            TextBox2.Text = dt.Rows(0).Item(7).ToString
    '            TextBox5.Text = dt.Rows(0).Item(8).ToString
    '            tgl1.Text = dt.Rows(0).Item(10).ToString
    '            jam1.Text = dt.Rows(0).Item(11).ToString
    '            ComboBox1.Text = dt.Rows(0).Item(28).ToString


    '            'txtcomport.Text = dt.Rows(0).Item(4).ToString
    '        Catch ex As Exception
    '            MsgBox(ex.Message)
    '        End Try
    '    Else
    '        'jika tidak ada maka insert baru
    '        idtransaksi.Text = "0"
    '    End If


    'End Sub

    Public Sub getDetailTransaction(ByVal idnopolPolisi As Integer)
        'MsgBox(nopolPolisi.ToString)
        'SQL = "SELECT * FROM `MGoodsFlow` WHERE `MVehicleData_ID` = " & idnopolPolisi & " AND `MBussiness_ID` = " & CInt(Form1.Label15.Text) & " AND `FINISH`= 0 ORDER BY `MVehicleData_ID` DESC LIMIT 1"
        'result = RETRIEVESINGLE_DIS_MSG(SQL)
        'MsgBox(result.ToString)
        'jika ada maka update timbangan ke-2
        'If result = True Then
        '    Try

        '        'MsgBox("ada")
        '        'cboPerangkat.DataSource = Nothing
        '        cboProduk.DataSource = Nothing
        '        'cboTransporters.DataSource = Nothing
        '        cboSopir.DataSource = Nothing
        '        CBOADDJENISTIMBANG(ComboBox1)
        '        fillcbo("SELECT * FROM `MItem` WHERE `MBussiness_ID`=" & Form1.Label15.Text & "", cboProduk, "ItemName", "MItem_ID", "-- Pilih Produk --")
        '        'fillcbo("SELECT * FROM `MVehicleData` WHERE `MBussiness_ID`=" & Form1.Label15.Text & "", cboTransporters, "vehiclePoliceNo", "MVehicleData_ID", "-- Pilih Transporter --")
        '        'fillcbo("SELECT * FROM `MWeightConfig`", cboPerangkat, "pcName", "MWeightConfig_ID", "-- Pilih Perangkat --")
        '        fillcbo("SELECT * FROM `MDriver` WHERE `MBussiness_ID`=" & Form1.Label15.Text & "", cboSopir, "driverName", "MDriver_ID", "-- Pilih Sopir --")

        '        'MGoodsFlow_ID 
        '        'FlowType()
        '        'MBussiness_ID()
        '        'Date 
        '        'Bruto(Tarra)
        '        'Netto()
        '        'createdBy()
        '        'createdAt()
        '        'updatedAt()
        '        'percentageDeduction()
        '        'qtyDeduction()
        '        'MDriver_ID()
        '        'MVendor_ID()
        '        'MVehicleData_ID()
        '        'NOTRANS()
        '        'FINISH()

        '        sql2 = "SELECT * FROM `MGoodsFlow` WHERE `MVehicleData_ID` = " & idnopolPolisi & " AND `MBussiness_ID` = " & CInt(Form1.Label15.Text) & " AND `FINISH`= 0 ORDER BY `MVehicleData_ID` DESC LIMIT 1"
        '        reloadtxt2(sql2)
        '        TextBox8.Visible = True
        '        TextBox9.Visible = False
        '        TextBox5.Text = dt4.Rows(0).Item(4).ToString
        '        idtransaksi.Text = dt4.Rows(0).Item(0).ToString
        '        ComboBox1.Text = dt4.Rows(0).Item(1).ToString

        '        lblwaktu.Text = dt4.Rows(0).Item(8).ToString
        '        TextBox11.Text = dt4.Rows(0).Item(10).ToString
        '        cboSopir.SelectedValue = dt4.Rows(0).Item(12)
        '        cboCustomer.SelectedValue = dt4.Rows(0).Item(13)
        '        'cboTransporters.SelectedValue = dt4.Rows(0).Item(14)
        '        TextBox8.Text = dt4.Rows(0).Item(16).ToString
        '        cboProduk.SelectedValue = dt4.Rows(0).Item(15)
        '        TextBox2.Text = dt4.Rows(0).Item(18).ToString
        '        Exit Sub




        '    Catch ex As Exception
        '        MsgBox(ex.Message)
        '    End Try
        'Else
        '    'jika tidak ada maka insert baru
        '    idtransaksi.Text = "0"
        'End If
    End Sub
    Private Sub getPicture()
        On Error Resume Next
        PictureBox2.Image = Image.FromFile(Application.StartupPath + "\picture\disconnected.png")
        PictureBox2.SizeMode = PictureBoxSizeMode.StretchImage

    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Dim nama As String = Me.ComboBox2.Text
        'Try
        '    sql = "SELECT * FROM `tblpks` WHERE `pksname`= '" & nama & "'"

        '    reloadtxt(sql)
        '    txtbaudrate.Text = dt.Rows(0).Item(4).ToString
        '    txtcomport.Text = dt.Rows(0).Item(5).ToString
        '    txtparity.Text = dt.Rows(0).Item(6).ToString
        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'End Try

        getDefaultComCOnfig()
    End Sub

    Private Sub getDefaultComCOnfig()
        Dim nama As Integer = Me.cboPerangkat.SelectedValue
        'MsgBox(nama.ToString)
        'Try
        '    'sql = "SELECT * FROM `tblpks` WHERE `pksname`= '" & nama & "'"
        '    SQL = "SELECT * FROM `MWeightConfig` WHERE `MWeightConfig_ID`= " & nama & " AND `MBussiness_ID`=" & CInt(Form1.Label15.Text)

        '    reloadtxt(SQL)
        '    'txtbaudrate.Text = dt.Rows(0).Item(4).ToString
        '    'txtcomport.Text = dt.Rows(0).Item(5).ToString
        '    'txtparity.Text = dt.Rows(0).Item(6).ToString

        '    txtbaudrate.Text = dt.Rows(0).Item(4).ToString
        '    txtcomport.Text = dt.Rows(0).Item(3).ToString
        '    txtparity.Text = dt.Rows(0).Item(5).ToString
        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'End Try
    End Sub

    Private Sub PictureBox2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox2.Click

        Try

            If SerialPort1.IsOpen = True Then
                SerialPort1.Close()
                PictureBox2.Image = Image.FromFile(Application.StartupPath + "\picture\disconnected.png")
                PictureBox2.SizeMode = PictureBoxSizeMode.StretchImage

            Else
                If txtcomport.Text = "" Then
                    MsgBox("Silahkan Lakukan Setting Konfigurasi")
                    Exit Sub
                Else

                    SerialPort1.PortName = txtcomport.Text
                    SerialPort1.BaudRate = txtbaudrate.Text

                    SerialPort1.Parity = IO.Ports.Parity.None
                    SerialPort1.StopBits = IO.Ports.StopBits.One
                    SerialPort1.DataBits = txtparity.Text
                    SerialPort1.Open()
                    If SerialPort1.IsOpen = True Then
                        PictureBox2.Image = Image.FromFile(Application.StartupPath + "\picture\connected.png")
                        PictureBox2.SizeMode = PictureBoxSizeMode.StretchImage
                    End If

                End If

            End If



        Catch ex As Exception
            If SerialPort1.IsOpen = True Then
                SerialPort1.Close()
                PictureBox2.Image = Image.FromFile(Application.StartupPath + "\picture\disconnected.png")
                PictureBox2.SizeMode = PictureBoxSizeMode.StretchImage
            End If
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub cboCustomer_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboCustomer.SelectedIndexChanged
        noTransaksi()
    End Sub

    Private Sub SerialPort1_DataReceived(ByVal sender As System.Object, ByVal e As System.IO.Ports.SerialDataReceivedEventArgs) Handles SerialPort1.DataReceived
        ReceivedText(SerialPort1.ReadExisting())
        'ReceivedTextNew(SerialPort1.ReadExisting())
    End Sub

    Private Sub btnReserve_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReserve.Click
        Dim nomor As String = ""
        Dim bruto As Double = CDbl(lblWeight.Text)
        Dim reflaksi As Double = 0
        Dim netto As Double
        Dim harga As Double = 0
        Dim jumlah As Double = 0
        Dim sopir As String = ""
        Dim idtrans As Integer
        Dim jenis_timbang As String
        'Dim outtrans As String = "OUT"
        Dim sqltimbang As String
        Dim sqltimbangout As String
        idtrans = Val(idtransaksi.Text)
        jenis_timbang = ComboBox1.Text



        Try
            If idtrans > 0 Then

                If TextBox11.Text = "" Then
                    MessageBox.Show("Silahkan isi data persen deduction benar", "No entry", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If

                If txtID.Text = "" Then
                    MessageBox.Show("Silahkan Pilih No Kendaraan", "No entry", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If

                If jenis_timbang = "IN" Then
                    netto = Val(TextBox5.Text) - Val(lblWeight.Text)

                ElseIf jenis_timbang = "OUT" Then
                    netto = Val(lblWeight.Text) - Val(TextBox5.Text)
                End If
                '
                ' & "`FlowType` ='" & outtrans & "', " _
                'SQL = "UPDATE `MGoodsFlow` " &
                '             "SET `tarra` =" & lblWeight.Text & ", " _
                '           & "`netto` =" & netto - (netto * (Val(TextBox11.Text) / 100)) & ", " _
                '           & "`percentageDeduction` =" & TextBox11.Text & ", " _
                '           & "`qtyDeduction` =" & (netto * (Val(TextBox11.Text) / 100)) & ", " _
                '           & "`netto1` =" & netto & ", " _
                '           & "`updatedAt` ='" & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "', " _
                '           & "`FINISH` = " & 1 & "" &
                '" WHERE `MBussiness_ID` = " & CInt(Form1.Label15.Text) & " AND `MGoodsFlow_ID` =" & idtransaksi.Text
                'result = CUD(SQL)



                'If result = True Then

                '    'insert di MStock
                '    sql4 = "SELECT * FROM `MGoodsFlow` WHERE `MGoodsFlow_ID` = " & idtransaksi.Text & " AND `MBussiness_ID` =" & CInt(Form1.Label15.Text)
                '    reloadtxt3(sql4)

                '    Dim MGoodsFlow_ID As Integer = dt5.Rows(0).Item(0)
                '    Dim StockType As String = dt5.Rows(0).Item(1).ToString
                '    Dim MBussiness_ID As Integer = dt5.Rows(0).Item(2)
                '    Dim Qty As Integer = dt5.Rows(0).Item(6)
                '    Dim createdBy As Integer = dt5.Rows(0).Item(7)
                '    Dim MItem_ID As Integer = dt5.Rows(0).Item(15)
                '    sql2 = "INSERT INTO `MStock` (`Date`,`MItem_ID`,`MGoodsFlow_ID`, `StockType`, `MBussiness_ID`, `Qty`, `createdBy`, `createdAt`, `updatedAt`) " &
                '        " VALUES ('" & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "', " _
                '                      & "'" & MItem_ID & "', " _
                '                      & "'" & CInt(idtransaksi.Text) & "', " _
                '                      & "'" & StockType & "', " _
                '                      & "'" & MBussiness_ID & "', " _
                '                      & "'" & Qty & "', " _
                '                      & "'" & CInt(Form1.Label10.Text) & "', " _
                '                      & "'" & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "', " _
                '                      & "'" & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "')"
                '    result3 = CUD(sql2)

                '    If jenis_timbang = "OUT" Then
                '        sqlMGoodsflowdetail = "INSERT INTO `MGoodsFlowDetails` (`MGoodsFlow_ID`,`ffa`,`impurities`, `moisture`, `locisNo`, `createdAt`, `updatedAt`) " &
                '        " VALUES ('" & CInt(idtransaksi.Text) & "', " _
                '                      & "'" & TextBox12.Text & "', " _
                '                      & "'" & TextBox14.Text & "', " _
                '                      & "'" & TextBox13.Text & "', " _
                '                      & "'" & TextBox15.Text & "', " _
                '                      & "'" & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "', " _
                '                      & "'" & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "')"
                '        CUD(sqlMGoodsflowdetail)
                '    End If

                '    If result3 = True Then
                '        MsgBox("Data Penimbangan sudah terupdate, mohon tunggu sebentar untuk cetak faktur.")

                '        Form1.FrmDaftarTimbang1.refreshDataTimbang()

                '        If jenis_timbang = "OUT" Then

                '            sqltimbangout = "SELECT `MGoodsFlow`.*, MGoodsFlowDetails.*, date_format(`MGoodsFlow`.`createdAt`, '%d/%m/%Y') as `tgl_timbang_1`, date_format(`MGoodsFlow`.`createdAt`, '%H:%i:%s') as `jam_timbang_1`, date_format(`MGoodsFlow`.`updatedAt`, '%d/%m/%Y') as `tgl_timbang_2`, date_format(`MGoodsFlow`.`updatedAt`, '%H:%i:%s') as `jam_timbang_2`,`MItem`.`ItemName` as `Nama Barang`, `MUser`.`name` as `Nama Operator`, `MVehicleData`.`vehiclePoliceNo` AS `Nopol Kendaraan`, `MVehicleData`.`vehicleName` AS `Nama Kendaraan`, `MVendor`.`vendorName` AS `Nama Konsumen`, `MDriver`.`driverName` AS `Nama Sopir` FROM MGoodsFlow LEFT JOIN `MItem` on `MItem`.`MItem_ID` = `MGoodsFlow`.`MItem_ID` LEFT JOIN `MUser` on `MUser`.`MUser_ID` = `MGoodsFlow`.`createdBy` LEFT JOIN `MVehicleData` on `MVehicleData`.`MVehicleData_ID` = `MGoodsFlow`.`MVehicleData_ID` LEFT JOIN `MVendor` on `MVendor`.`MVendor_ID` = `MGoodsFlow`.`MVendor_ID` LEFT JOIN `MDriver` on `MDriver`.`MDriver_ID` = `MGoodsFlow`.`MDriver_ID`  JOIN `MGoodsFlowDetails` ON `MGoodsFlowDetails`.`MGoodsFlow_ID` = `MGoodsFlow`.`MGoodsFlow_ID` where `MGoodsFlow`.`MGoodsFlow_ID`= " & idtrans & "  AND `MGoodsFlow`.`MBussiness_ID` =" & CInt(Form1.Label15.Text) & " ORDER BY `MGoodsFlow`.`NOTRANS` "
                '            'reports2(sqltimbang, "receipt", Form1.FrmReports1.CrystalReportViewer1, Form1.Label9.Text, Form1.Label18.Text)
                '            'Form1.addContent(Form1.FrmReports1, Form1.btnInclusiveReport)
                '            reports2(sqltimbangout, "receiptOut", frmPopReport.CrystalReportViewer1, Form1.Label9.Text, Form1.Label18.Text)
                '            frmPopReport.ShowDialog()

                '        Else

                '            sqltimbang = "SELECT `MGoodsFlow`.*, date_format(`MGoodsFlow`.`createdAt`, '%d/%m/%Y') as `tgl_timbang_1`, date_format(`MGoodsFlow`.`createdAt`, '%H:%i:%s') as `jam_timbang_1`, date_format(`MGoodsFlow`.`updatedAt`, '%d/%m/%Y') as `tgl_timbang_2`, date_format(`MGoodsFlow`.`updatedAt`, '%H:%i:%s') as `jam_timbang_2`,`MItem`.`ItemName` as `Nama Barang`, `MUser`.`name` as `Nama Operator`, `MVehicleData`.`vehiclePoliceNo` AS `Nopol Kendaraan` , `MVehicleData`.`vehicleName` AS `Nama Kendaraan`, `MVendor`.`vendorName` AS `Nama Konsumen`, `MDriver`.`driverName` AS `Nama Sopir` FROM MGoodsFlow LEFT JOIN `MItem` on `MItem`.`MItem_ID` = `MGoodsFlow`.`MItem_ID` LEFT JOIN `MUser` on `MUser`.`MUser_ID` = `MGoodsFlow`.`createdBy` LEFT JOIN `MVehicleData` on `MVehicleData`.`MVehicleData_ID` = `MGoodsFlow`.`MVehicleData_ID` LEFT JOIN `MVendor` on `MVendor`.`MVendor_ID` = `MGoodsFlow`.`MVendor_ID` LEFT JOIN `MDriver` on `MDriver`.`MDriver_ID` = `MGoodsFlow`.`MDriver_ID` where `MGoodsFlow_ID`= " & idtrans & "  AND `MGoodsFlow`.`MBussiness_ID` =" & CInt(Form1.Label15.Text) & " ORDER BY `MGoodsFlow`.`NOTRANS` "
                '            'reports2(sqltimbang, "receipt", Form1.FrmReports1.CrystalReportViewer1, Form1.Label9.Text, Form1.Label18.Text)
                '            'Form1.addContent(Form1.FrmReports1, Form1.btnInclusiveReport)
                '            reports2(sqltimbang, "receipt", frmPopReport.CrystalReportViewer1, Form1.Label9.Text, Form1.Label18.Text)
                '            frmPopReport.ShowDialog()
                '        End If



                '        kosongCombo()
                '        noTransaksi()

                '        TextBox8.Visible = False
                '        TextBox9.Visible = True
                '        TextBox2.Clear()
                '        TextBox1.Clear()
                '        txtID.Clear()
                '        TextBox5.Text = "0"
                '        TextBox6.Text = "0"
                '        TextBox7.Text = "0"
                '        TextBox10.Text = "0"
                '        idtransaksi.Text = "0"

                '        TextBox12.Text = "0"
                '        TextBox13.Text = "0"
                '        TextBox14.Text = "0"
                '        TextBox14.Text = ""
                '        Panel5.Visible = False
                '    End If

                'Else
                '    MsgBox("Error query")
                'End If

            Else

                'If ComboBox1.Text = "OUT" Then
                '    MessageBox.Show("Silahkan Pilih Jenis Trans Timbang Dengan Benar", "No entry", MessageBoxButtons.OK, MessageBoxIcon.Error)
                '    Exit Sub
                'End If

                If ComboBox1.Text = "" Or TextBox11.Text = "" Or TextBox2.Text = "" Or cboCustomer.Text = "" Or cboProduk.Text = "" Or cboSopir.Text = "" Then
                    MessageBox.Show("Silahkan isi data dengan benar", "No entry", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If

                If txtID.Text = "" Then
                    MessageBox.Show("Silahkan Pilih No Kendaraan", "No entry", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If

                'MGoodsFlow_ID	FlowType	MBussiness_ID	Date	Bruto	Tarra	Netto	createdBy	createdAt	updatedAt	percentageDeduction	qtyDeduction	MDriver_ID	MVendor_ID	MVehicleData_ID	NOTRANS	FINISH	
                'jika belum maka insert penimbangan ke-1
                '& "'" & cboTransporters.SelectedValue & "', " _ diganti txtID
                'sql2 = "INSERT INTO `MGoodsFlow` (`FlowType`,`MBussiness_ID`,`Date`,`Bruto`, `Tarra`, `Netto`, `createdBy`, `createdAt`, `updatedAt`, `percentageDeduction`, `qtyDeduction`, `MDriver_ID`, `MVendor_ID`, `MVehicleData_ID`, `NOTRANS`,`MItem_ID`,`FINISH`,`PODONO`,`netto1`) " &
                '      " VALUES ('" & ComboBox1.Text & "', " _
                '                    & "'" & CInt(Form1.Label15.Text) & "', " _
                '                    & "'" & DateTime.Now.ToString("yyyy-MM-dd") & "', " _
                '                    & "'" & lblWeight.Text & "', " _
                '                    & "'" & 0 & "', " _
                '                    & "'" & 0 & "', " _
                '                    & "'" & CInt(Form1.Label10.Text) & "', " _
                '                    & "'" & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "', " _
                '                    & "'" & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "', " _
                '                    & "'" & CInt(TextBox11.Text) & "', " _
                '                    & "'" & 0 & "', " _
                '                    & "'" & cboSopir.SelectedValue & "', " _
                '                    & "'" & cboCustomer.SelectedValue & "', " _
                '                    & "'" & txtID.Text & "', " _
                '                    & "'" & TextBox9.Text & "', " _
                '                    & "'" & cboProduk.SelectedValue & "', " _
                '                    & "'" & 0 & "', " _
                '                    & "'" & TextBox2.Text & "', " _
                '                    & "'" & 0 & "')"
                'result = CUD(sql2)
                'If result = True Then

                '    MsgBox("Data Produk Sudah Tersimpan.")
                '    kosongCombo()
                '    noTransaksi()
                '    idtransaksi.Text = "0"
                '    TextBox8.Visible = False
                '    TextBox9.Visible = True
                '    TextBox2.Clear()
                '    TextBox1.Clear()
                '    txtID.Clear()
                '    TextBox5.Text = "0"
                '    TextBox6.Text = "0"
                '    TextBox7.Text = "0"
                '    TextBox10.Text = "0"
                '    Form1.FrmDaftarTimbang1.refreshDataTimbang()
                'Else
                '    MsgBox("Error query")
                'End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub TextBox11_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox11.KeyPress
        Dim DecimalSeparator As String = Application.CurrentCulture.NumberFormat.NumberDecimalSeparator
        e.Handled = Not (Char.IsDigit(e.KeyChar) Or
                         Asc(e.KeyChar) = 8 Or
                         (e.KeyChar = DecimalSeparator And sender.Text.IndexOf(DecimalSeparator) = -1))
    End Sub

    Private Sub TextBox12_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox12.KeyPress
        Dim DecimalSeparator As String = Application.CurrentCulture.NumberFormat.NumberDecimalSeparator
        e.Handled = Not (Char.IsDigit(e.KeyChar) Or
                         Asc(e.KeyChar) = 8 Or
                         (e.KeyChar = DecimalSeparator And sender.Text.IndexOf(DecimalSeparator) = -1))

    End Sub

    Private Sub TextBox13_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox13.KeyPress
        Dim DecimalSeparator As String = Application.CurrentCulture.NumberFormat.NumberDecimalSeparator
        e.Handled = Not (Char.IsDigit(e.KeyChar) Or
                         Asc(e.KeyChar) = 8 Or
                         (e.KeyChar = DecimalSeparator And sender.Text.IndexOf(DecimalSeparator) = -1))

    End Sub

    Private Sub TextBox14_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox14.KeyPress
        Dim DecimalSeparator As String = Application.CurrentCulture.NumberFormat.NumberDecimalSeparator
        e.Handled = Not (Char.IsDigit(e.KeyChar) Or
                         Asc(e.KeyChar) = 8 Or
                         (e.KeyChar = DecimalSeparator And sender.Text.IndexOf(DecimalSeparator) = -1))

    End Sub

End Class

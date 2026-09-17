Imports System.ComponentModel
Imports System.Net.Sockets
Imports ERP_EMI.Devices.RFID.HW_VX6346KL

Public Class N_EMI_SD_Pairing_RFID_Tags_Timbang_Floor_Scale
    Private RFIDReader As HW_VX6346KL_Reader
    Private RFIDReaderIP As String = "0.0.0.0"
    Public Property SelectedRFIDTags As List(Of String)

    Public Property SelectedSplit As String
    Public Property SelectedBatch As String

    Dim TempLabel As String = ""

    Dim isRFIDActive As Boolean = True
    Dim TAGDEFAULTRFID As String = ""

    Private Sub N_EMI_SD_Pairing_RFID_Tags_Timbang_Floor_Scale_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        With Lv_RFID_Tags
            .Clear()
            .View = View.Details
            .FullRowSelect = True
            .GridLines = True
            .CheckBoxes = False

            .Columns.Add("RFID Tag", 250, HorizontalAlignment.Left)
            .Columns.Add("Label Tag", 250, HorizontalAlignment.Left)
        End With

        Lv_RFID_Tags.Columns(1).DisplayIndex = 0



        Try
            OpenConn()

            SQL = "SELECT IP_Address From N_EMI_Master_Data_RFID_Readers WHERE Kode_Perangkat='COLD_STORAGE_PAIRING' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read() Then
                    RFIDReaderIP = Dr("IP_Address").ToString()
                End If
            End Using

            SQL = $"select Flag_Cold_Storage_RFID_Mati, Tag_RFID_Default from init where kode_perusahaan = '{KodePerusahaan}' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    If General_Class.CekNULL(Dr("Flag_Cold_Storage_RFID_Mati")) = "Y" Then
                        isRFIDActive = False
                    Else
                        isRFIDActive = True
                    End If

                    TAGDEFAULTRFID = Dr("Tag_RFID_Default")

                End If
            End Using




            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        RFIDReader = New HW_VX6346KL_Reader(RFIDReaderIP, 6000)
        AddHandler RFIDReader.Connected, AddressOf RFID_Connected
        AddHandler RFIDReader.Disconnected, AddressOf RFID_Disconnected
        AddHandler RFIDReader.TagDetected, AddressOf RFID_TagDetected



        '======================================
        '=     JIKA FLAG RFID TIDAK AKTIF     =
        '======================================
        If Not isRFIDActive Then
            If Not IsRFIDTagCanBeUsed(TAGDEFAULTRFID) Then Return

            Try
                OpenConn()

                Dim sql As String = "
                    SELECT RFID_Label
                    FROM N_EMI_Master_Data_RFID_Tags
                    WHERE RFID_Tag = '" & TAGDEFAULTRFID & "'
                        AND Status IS NULL
                        AND (
                            (No_Production_Order IS NULL and Batch is null)
                            OR (No_Production_Order = '" & SelectedSplit & "' and Batch = '" & SelectedBatch & "')
                        )
                "
                Using Dr = OpenTrans(sql)
                    If Dr.Read Then
                        TempLabel = Dr("RFID_Label")
                    End If
                End Using

                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

            Dim Lv As ListViewItem
            Lv = Lv_RFID_Tags.Items.Add(TAGDEFAULTRFID)
            Lv.SubItems.Add(TempLabel)

        Else


            'TODO:   JANGAN LIUPA DI UNCOMMENT
            If IsPortOpen(RFIDReaderIP, 6000, 1000) Then
                If Not RFIDReader.Connect(500, 2) Then
                    MsgBox("Gagal terhubung ke reader RFID.", MsgBoxStyle.Critical, "Error")
                End If
            Else
                MsgBox("Reader RFID tidak dapat dijangkau melalui jaringan.", MsgBoxStyle.Critical, "Error")
            End If

            'If PingHost(RFIDReaderIP) Then
            '    If Not RFIDReader.Connect(500) Then
            '        MsgBox("Gagal terhubung ke reader RFID.", MsgBoxStyle.Critical, "Error")
            '    End If
            'Else
            '    MsgBox("Reader RFID tidak dapat dijangkau melalui jaringan.", MsgBoxStyle.Critical, "Error")
            'End If
        End If






    End Sub

    Function PingHost(ip As String) As Boolean
        Try
            Dim ping As New Net.NetworkInformation.Ping()
            Dim reply = ping.Send(ip, 1000)
            Return reply.Status = Net.NetworkInformation.IPStatus.Success
        Catch
            Return False
        End Try
    End Function

    Private Sub RFID_Connected()
    End Sub

    Private Sub RFID_Disconnected()
    End Sub

    Private Sub RFID_TagDetected(tag As String)
        If Lv_RFID_Tags.InvokeRequired Then
            Lv_RFID_Tags.Invoke(New Action(Of String)(AddressOf RFID_TagDetected), tag)
            Return
        End If
        For Each itm As ListViewItem In Lv_RFID_Tags.Items
            If itm.Text = tag Then Return
        Next



        TempLabel = ""

        If Not IsRFIDTagCanBeUsed(tag) Then Return
        'Lv_RFID_Tags.Items.Add(New ListViewItem(tag))

        Try
            OpenConn()

            Dim sql As String = "
                SELECT RFID_Label
                FROM N_EMI_Master_Data_RFID_Tags
                WHERE RFID_Tag = '" & tag & "'
                    AND Status IS NULL
                    AND (
                        (No_Production_Order IS NULL and Batch is null)
                        OR (No_Production_Order = '" & SelectedSplit & "' and Batch = '" & SelectedBatch & "')
                    )
            "
            Using Dr = OpenTrans(sql)
                If Dr.Read Then
                    TempLabel = Dr("RFID_Label")
                End If
            End Using

            'sql = "
            '    select RFID_Label
            '    from N_EMI_Master_Data_RFID_Tags
            '    where status ='Y'
            '    and RFID_Tag = @RFID_Tag
            '    and status is null
            '"
            'Cmd.Parameters.Clear()
            'Cmd.Parameters.AddWithValue("@RFID_Tag", rfidTag)
            'Using Dr = OpenTrans(sql)
            '    TempLabel = If(General_Class.CekNULL(Dr("RFID_Label")) = "", "-", Dr("RFID_Label"))
            '    Return True
            'End Using
            CloseConn()

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            CloseConn()
        End Try

        Dim Lv As ListViewItem
        Lv = Lv_RFID_Tags.Items.Add(tag)
        Lv.SubItems.Add(TempLabel)

        TempLabel = ""


    End Sub

    Private Function IsRFIDTagCanBeUsed(rfidTag As String) As Boolean

        If String.IsNullOrWhiteSpace(SelectedSplit) Or String.IsNullOrWhiteSpace(SelectedBatch) Then
            MessageBox.Show("No Split Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
        End If

        Try
            OpenConn()

            Dim sql As String = "
                SELECT 1
                WHERE EXISTS (
                    SELECT 1
                    FROM N_EMI_Master_Data_RFID_Tags
                    WHERE RFID_Tag = @RFID_Tag
                        AND Status IS NULL
                        AND (
                            --No_Production_Order IS NULL
                            --OR No_Production_Order = @NoSplit
                            (No_Production_Order IS NULL and Batch is null)
                            OR (No_Production_Order = '" & SelectedSplit & "' and Batch = '" & SelectedBatch & "')
                        )
                )
            "
            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@RFID_Tag", rfidTag)
            Cmd.Parameters.AddWithValue("@NoSplit", SelectedSplit)
            Using Dr = OpenTrans(sql)
                Return Dr.Read()
            End Using

            'sql = "
            '    select RFID_Label
            '    from N_EMI_Master_Data_RFID_Tags
            '    where status ='Y'
            '    and RFID_Tag = @RFID_Tag
            '    and status is null
            '"
            'Cmd.Parameters.Clear()
            'Cmd.Parameters.AddWithValue("@RFID_Tag", rfidTag)
            'Using Dr = OpenTrans(sql)
            '    TempLabel = If(General_Class.CekNULL(Dr("RFID_Label")) = "", "-", Dr("RFID_Label"))
            '    Return True
            'End Using


        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return True
        Finally
            CloseConn()
        End Try
    End Function

    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
        If Lv_RFID_Tags.Items.Count = 0 Then
            MessageBox.Show("Belum ada RFID yang dipilih.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        SelectedRFIDTags = New List(Of String)

        For Each itm As ListViewItem In Lv_RFID_Tags.Items
            SelectedRFIDTags.Add(itm.Text)
        Next

        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub N_EMI_SD_Pairing_RFID_Tags_Timbang_Floor_Scale_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        If RFIDReader.IsConnected Then
            RFIDReader.Disconnect()
        End If
    End Sub

    Public Function IsPortOpen(ip As String, port As Integer, Optional timeout As Integer = 1000) As Boolean

        Try
            Using client As New TcpClient()

                Dim result = client.BeginConnect(ip, port, Nothing, Nothing)
                Dim success = result.AsyncWaitHandle.WaitOne(timeout)

                If Not success Then Return False

                client.EndConnect(result)

                Return True

            End Using

        Catch
            Return False
        End Try

    End Function
End Class
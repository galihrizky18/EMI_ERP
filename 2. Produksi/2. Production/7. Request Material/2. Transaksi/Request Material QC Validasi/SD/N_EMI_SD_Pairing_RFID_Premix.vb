Imports System.ComponentModel
Imports System.Net.Sockets
Imports ERP_EMI.Devices.RFID.HW_VX6346KL

Public Class N_EMI_SD_Pairing_RFID_Premix
    Property SelectedSplit As String = ""
    Property SelectedBatch As String = ""

    Private RFIDReader As HW_VX6346KL_Reader
    Private RFIDReaderIP As String = "0.0.0.0"
    Private RFIDPower As Integer = 3
    Dim isRFIDActive As Boolean = True
    Dim TAGDEFAULTRFID As String = ""
    Dim TempLabel As String = ""

    Private Sub N_EMI_SD_Pairing_RFID_Premix_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MsgBox(SelectedBatch)
        MsgBox(SelectedSplit)


        With LvRFIDTags
            .Clear()
            .View = View.Details
            .FullRowSelect = True
            .GridLines = True

            .Columns.Add("RFID Tag", 0, HorizontalAlignment.Left)
            .Columns.Add("RFID Label", 350, HorizontalAlignment.Left)
        End With

        Try
            OpenConn()

            SQL = "SELECT IP_Address, Power From N_EMI_Master_Data_RFID_Readers WHERE Kode_Perangkat='PREMIX_PAIRING' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read() Then
                    RFIDReaderIP = Dr("IP_Address").ToString()
                    RFIDPower = Dr("Power").ToString()
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
            Lv = LvRFIDTags.Items.Add(TAGDEFAULTRFID)
            Lv.SubItems.Add(TempLabel)

        Else
            If IsPortOpen(RFIDReaderIP, 6000, 1000) Then
                If Not RFIDReader.Connect(500, RFIDPower) Then
                    MsgBox("Gagal terhubung ke reader RFID.", MsgBoxStyle.Critical, "Error")
                End If
            Else
                MsgBox("Reader RFID tidak dapat dijangkau melalui jaringan.", MsgBoxStyle.Critical, "Error")
            End If
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

    Private Sub RFID_Connected()
    End Sub

    Private Sub RFID_Disconnected()
    End Sub

    Private Sub RFID_TagDetected(tag As String)
        If LvRFIDTags.InvokeRequired Then
            LvRFIDTags.Invoke(New Action(Of String)(AddressOf RFID_TagDetected), tag)
            Return
        End If
        For Each itm As ListViewItem In LvRFIDTags.Items
            If itm.Text = tag Then Return
        Next

        TempLabel = ""

        If Not IsRFIDTagCanBeUsed(tag) Then Return

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

            CloseConn()

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            CloseConn()
        End Try

        Dim Lv As ListViewItem
        Lv = LvRFIDTags.Items.Add(tag)
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

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return True
        Finally
            CloseConn()
        End Try
    End Function

    Private Sub N_EMI_SD_Pairing_RFID_Premix_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        If RFIDReader.IsConnected Then
            RFIDReader.Disconnect()
        End If
    End Sub

    Private Sub BtnSimpan_Click(sender As Object, e As EventArgs) Handles BtnSimpan.Click
        get_jam()

        Try
            OpenConn()

            For i As Integer = 0 To LvRFIDTags.Items.Count - 1

                Dim rfid_tag As String = LvRFIDTags.Items(i).SubItems(0).Text

                SQL = $"
                    INSERT INTO N_EMI_Pairing_RFID
                    (Kode_Perusahaan, No_Split_Production_Order, Kode_Stock_Owner, RFID_Tag,
                     Tanggal_Pairing, Jam_Pairing, UserID_Pairing, Lokasi_Pairing, batch)
                    VALUES
                    ('{KodePerusahaan}', '{SelectedSplit}', '{Lokasi}', '{rfid_tag}',
                     '{Format(tgl_skg, "yyyy-MM-dd")}', '{Format(tgl_skg, "HH:mm:ss")}', '{UserID}', 'PREMIX', '{SelectedBatch}')
                "

                ExecuteTrans(SQL)
            Next

            CloseConn()

            MessageBox.Show("Data pairing RFID berhasil disimpan.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Close()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class
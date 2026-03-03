Imports System.Management
Imports ReaderB
Imports System.Net.Sockets
Imports System.Threading

Public Module UHF18ReaderHelper

    Private ComAdr As Byte = &HFF
    Private Handle As Integer = 0
    Private NetHandle As Integer = 0
    Public Event TagDetected(tag As String)
    Private Listening As Boolean = False
    Private ListenThread As Thread

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


    Public Function FindCH340Port() As Integer

        Dim searcher As New ManagementObjectSearcher(
            "SELECT * FROM Win32_PnPEntity WHERE Name LIKE '%(COM%'")

        For Each obj As ManagementObject In searcher.Get()

            Dim name As String = obj("Name").ToString().ToUpper()

            If name.Contains("CH340") OrElse name.Contains("USB-SERIAL") Then

                Dim start = name.LastIndexOf("COM") + 3
                Dim endPos = name.IndexOf(")", start)

                Return Integer.Parse(name.Substring(start, endPos - start))
            End If
        Next

        Return -1
    End Function

    Public Function ConnectCH340Port() As Boolean

        Dim port = FindCH340Port()

        If port = -1 Then Return False

        Dim baud As Byte = 5
        Dim err As Integer = 0

        Dim ret = StaticClassReaderB.OpenComPort(port, ComAdr, baud, Handle)
        If ret <> 0 Then Return False

        StaticClassReaderB.SetPowerDbm(ComAdr, 4, Handle)

        Return True
    End Function

    Public Function ReadSingleTag() As String

        Dim epcBuf As Byte() = New Byte(5000) {}

        Dim totalLen As Integer = 0
        Dim tagCount As Integer = 0

        Dim ret = StaticClassReaderB.Inventory_G2(
        ComAdr,
        CType(0, Byte),
        CType(0, Byte),
        CType(0, Byte),
        epcBuf,
        totalLen,
        tagCount,
        Handle)

        If tagCount = 0 Then Return ""

        Dim epcLen As Integer = epcBuf(0)

        Dim epcHex As String = ""

        For i = 1 To epcLen
            epcHex &= epcBuf(i).ToString("X2")
        Next

        Return epcHex

    End Function

    Public Function ReadSingleTagTCP() As String

        If NetHandle = 0 Then Return ""

        Dim epcBuf(5000) As Byte
        Dim totalLen As Integer = 0
        Dim tagCount As Integer = 0

        Dim ret = StaticClassReaderB.Inventory_G2(
        ComAdr,
        0, 0, 0,
        epcBuf,
        totalLen,
        tagCount,
        NetHandle)

        If tagCount = 0 Then Return ""

        Dim epcLen As Integer = epcBuf(0)

        If epcLen <= 0 Or epcLen > 64 Then Return ""

        Dim epcHex As String = ""

        For i = 1 To epcLen
            epcHex &= epcBuf(i).ToString("X2")
        Next

        Return epcHex

    End Function

    Public Sub StartListening()

        If Listening Then Exit Sub

        Listening = True

        ListenThread = New Thread(Sub()

                                      While Listening

                                          Dim tag = ReadSingleTagTCP()

                                          If tag <> "" Then
                                              RaiseEvent TagDetected(tag)
                                          End If

                                          Thread.Sleep(100)
                                      End While

                                  End Sub)

        ListenThread.IsBackground = True
        ListenThread.Start()

    End Sub

    Public Sub StopListening()

        Listening = False

        If ListenThread IsNot Nothing AndAlso ListenThread.IsAlive Then
            ListenThread.Join(1000)
        End If

    End Sub


    Public Function ConnectTCP(ip As String, port As Integer, power As Integer) As Boolean

        Dim err As Integer = 0

        Dim ret = StaticClassReaderB.OpenNetPort(port, ip, ComAdr, NetHandle)

        If ret <> 0 Then Return False

        Return True

    End Function

    Public Function SetPowerTCP(power As Byte) As Boolean

        Dim ret = StaticClassReaderB.SetPowerDbm(ComAdr, power, NetHandle)

        Return ret = 0

    End Function


    Public Sub DisconnectTCP()

        If NetHandle <> 0 Then
            StaticClassReaderB.CloseNetPort(NetHandle)
            NetHandle = 0
        End If

    End Sub


    Public Sub CloseComPort()
        StaticClassReaderB.CloseComPort()
    End Sub
End Module

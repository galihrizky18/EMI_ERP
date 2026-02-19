Imports System.Text
Imports System.Threading
Imports System.Collections.Concurrent

Namespace Devices.RFID.HW_VX6336
    Public Class HW_VX6336_Reader
        Private _comAddr As Byte = &HFF
        Private _portHandle As Integer = -1
        Private _connected As Boolean = False
        Private _reading As Boolean = False
        Private _readThread As Thread

        Private _uniqueTags As New HashSet(Of String)
        Private _tagCounts As New ConcurrentDictionary(Of String, Integer)
        Private _tagLastSeen As New ConcurrentDictionary(Of String, DateTime)

        Private _port As Integer
        Private _baud As Byte

        Public Enum ReadMode
            AllTags
            UniqueTags
            UniqueWithCount
        End Enum

        Public Property Mode As ReadMode = ReadMode.UniqueTags

        Public Event TagDetected(tag As String)
        Public Event TagCountUpdated(tag As String, count As Integer)
        Public Event TagTimeUpdated(tag As String, lastSeen As DateTime)
        Public Event Connected()
        Public Event Disconnected()

        Public ReadOnly Property IsConnected As Boolean
            Get
                Return _connected
            End Get
        End Property

        Public Sub New(port As Integer, baud As Byte)
            _port = port
            _baud = baud
        End Sub

        Public Function Connect() As Boolean
            Dim ret = HW_VX6336_Native.AutoOpenComPort(_port, _comAddr, _baud, _portHandle)
            _connected = (ret = 0)

            If _connected Then
                ResetTagData()
                StartReading()
                RaiseEvent Connected()
            End If

            Return _connected
        End Function

        Public Sub Disconnect()
            StopReading()
            If _connected Then
                HW_VX6336_Native.CloseComPort()
                _connected = False
                Thread.Sleep(300)
                RaiseEvent Disconnected()
            End If
        End Sub

        Public Function Inventory() As List(Of String)
            If Not _connected Then Throw New InvalidOperationException("Reader belum terkoneksi.")

            Dim tags As New List(Of String)
            Dim EPC(5000) As Byte
            Dim totalLen As Integer = 0
            Dim cardNum As Integer = 0

            Dim ret = HW_VX6336_Native.Inventory_G2(_comAddr, 0, 0, 0, EPC, totalLen, cardNum, _portHandle)

            If ret = 1 AndAlso cardNum > 0 AndAlso totalLen > 0 Then
                Dim daw(totalLen - 1) As Byte
                Array.Copy(EPC, daw, totalLen)

                Dim temps As String = BitConverter.ToString(daw).Replace("-", "")
                Dim m As Integer = 0

                For i As Integer = 1 To cardNum
                    Dim epcLen As Integer = daw(m)
                    Dim sEPC As String = temps.Substring(m * 2 + 2, epcLen * 2).ToUpper()
                    tags.Add(sEPC)
                    m += epcLen + 1
                Next
            End If

            Return tags
        End Function

        Public Sub StartReading(Optional intervalMs As Integer = 250)
            If Not _connected Then Throw New InvalidOperationException("Reader belum terkoneksi.")
            If _reading Then Exit Sub

            _reading = True
            _readThread = New Thread(
                Sub()
                    While _reading
                        Try
                            Dim tags = Inventory()
                            If tags IsNot Nothing AndAlso tags.Count > 0 Then
                                For Each epc In tags
                                    Dim now = DateTime.Now
                                    _tagLastSeen(epc) = now

                                    Select Case Mode
                                        Case ReadMode.AllTags
                                            RaiseEvent TagDetected(epc)
                                            RaiseEvent TagTimeUpdated(epc, now)

                                        Case ReadMode.UniqueTags
                                            SyncLock _uniqueTags
                                                If _uniqueTags.Add(epc) Then
                                                    RaiseEvent TagDetected(epc)
                                                End If
                                            End SyncLock
                                            RaiseEvent TagTimeUpdated(epc, now)

                                        Case ReadMode.UniqueWithCount
                                            Dim newCount = _tagCounts.AddOrUpdate(epc, 1, Function(key, oldVal) oldVal + 1)
                                            If newCount = 1 Then
                                                RaiseEvent TagDetected(epc)
                                            End If
                                            RaiseEvent TagCountUpdated(epc, newCount)
                                            RaiseEvent TagTimeUpdated(epc, now)
                                    End Select
                                Next
                            End If
                        Catch ex As Exception
                            Debug.WriteLine("Thread RFID error: " & ex.Message)
                        End Try
                        Thread.Sleep(intervalMs)
                    End While
                End Sub)
            _readThread.IsBackground = True
            _readThread.Start()
        End Sub

        Public Sub StopReading()
            _reading = False
            If _readThread IsNot Nothing AndAlso _readThread.IsAlive Then
                _readThread.Join(200)
            End If
        End Sub

        Public Function GetUniqueTags() As List(Of String)
            SyncLock _uniqueTags
                Return _uniqueTags.ToList()
            End SyncLock
        End Function

        Public Function GetTagCounts() As Dictionary(Of String, Integer)
            Return _tagCounts.ToDictionary(Function(kv) kv.Key, Function(kv) kv.Value)
        End Function

        Public Function GetAllLastSeen() As Dictionary(Of String, DateTime)
            Return _tagLastSeen.ToDictionary(Function(kv) kv.Key, Function(kv) kv.Value)
        End Function

        Public Function GetLastSeen(epc As String) As DateTime?
            Dim dt As DateTime
            If _tagLastSeen.TryGetValue(epc, dt) Then Return dt
            Return Nothing
        End Function

        Public Sub ResetTagData()
            SyncLock _uniqueTags
                _uniqueTags.Clear()
            End SyncLock
            _tagCounts.Clear()
            _tagLastSeen.Clear()
        End Sub

        Public Sub ToggleConnection(reader As HW_VX6336_Reader, mode As HW_VX6336_Reader.ReadMode, Optional intervalMs As Integer = 200)
            Try
                If Not reader.IsConnected Then
                    If reader.Connect() Then
                        reader.Mode = mode
                        reader.StartReading(intervalMs)
                    Else
                        MessageBox.Show("Gagal membuka koneksi ke RFID Reader.", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                Else
                    reader.Disconnect()
                End If
            Catch ex As Exception
                MessageBox.Show("Error pada koneksi RFID: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

    End Class
End Namespace

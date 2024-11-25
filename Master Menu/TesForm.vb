Imports System.IO
Imports System.IO.Ports
Imports Microsoft.VisualBasic.Devices

Public Class TesForm
    Private Sub TesForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Show()
        Get_Data_Timbangan()
        'TestConnection()
    End Sub

    Private Function IsSerialPortAvailable(portName As String) As Boolean
        Try
            Using sp As New IO.Ports.SerialPort(portName)
                sp.Open()
                sp.Close()
            End Using
            Return True
        Catch ex As Exception
            MessageBox.Show("Port tidak tersedia: " & ex.Message)
            Return False
        End Try
    End Function

    Private Sub TestConnection()
        Dim portName As String = "COM7"
        If IsSerialPortAvailable(portName) Then
            MessageBox.Show("Koneksi ke " & portName & " berhasil!")
        Else
            MessageBox.Show("Koneksi ke " & portName & " gagal.")
        End If
    End Sub


    Private Sub Get_Data_Timbangan()
        Try
            SerialPort1.PortName = "COM7"
            SerialPort1.BaudRate = 9600
            SerialPort1.Parity = IO.Ports.Parity.None
            SerialPort1.StopBits = IO.Ports.StopBits.One
            SerialPort1.Handshake = IO.Ports.Handshake.None
            'SerialPort1.DataBits = IO.Ports.DataB
            SerialPort1.Encoding = System.Text.Encoding.Default
            SerialPort1.Open()

            SerialPort1.WriteLine(111)

            MessageBox.Show("Data berhasil dikirim: " & 111)

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    'Private Async Sub Get_Data_Timbangan()

    '    Dim PORT As String = "COM7"
    '    Try
    '        Await Task.Run(Sub()
    '                           Dim sp = New SerialPort(PORT, 9600, Parity.None, 8, StopBits.One)
    '                           If Not (sp Is Nothing) Then
    '                               sp.Open()


    '                               'sp.WriteLine("cobaaaa")

    '                               Dim data As String = sp.ReadLine()


    '                               Me.Invoke(Sub()
    '                                             TextBox1.Text = data
    '                                         End Sub)


    '                               'Lb_1.Text = data

    '                               'Me.Invoke(Sub()
    '                               '              Lb_1.Text = data
    '                               '          End Sub)

    '                               sp.Close()
    '                               sp.Dispose()
    '                               sp = Nothing
    '                           End If
    '                       End Sub)
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '        Exit Sub
    '    End Try
    'End Sub


    'Private Sub Get_Data_Timbangan()

    '    Dim PORT As String = "COM7"
    '    Try
    '        Dim sp = New SerialPort(PORT, 9600, Parity.None, 8, StopBits.One)
    '        If Not (sp Is Nothing) Then
    '            sp.Open()
    '            'sp.ReadLine()

    '            sp.WriteLine("cobaaaa")

    '            sp.Close()
    '            sp.Dispose()
    '            sp = Nothing
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '        Exit Sub
    '    End Try
    'End Sub



End Class
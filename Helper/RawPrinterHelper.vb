Imports System.Runtime.InteropServices

Public Class RawPrinterHelper

    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Ansi)>
    Public Class DOCINFOA
        <MarshalAs(UnmanagedType.LPStr)>
        Public pDocName As String
        <MarshalAs(UnmanagedType.LPStr)>
        Public pOutputFile As String
        <MarshalAs(UnmanagedType.LPStr)>
        Public pDataType As String
    End Class

    <DllImport("winspool.Drv", EntryPoint:="OpenPrinterA", SetLastError:=True, CharSet:=CharSet.Ansi)>
    Public Shared Function OpenPrinter(szPrinter As String, ByRef hPrinter As IntPtr, pd As IntPtr) As Boolean
    End Function

    <DllImport("winspool.Drv", EntryPoint:="ClosePrinter", SetLastError:=True)>
    Public Shared Function ClosePrinter(hPrinter As IntPtr) As Boolean
    End Function

    <DllImport("winspool.Drv", EntryPoint:="StartDocPrinterA", SetLastError:=True, CharSet:=CharSet.Ansi)>
    Public Shared Function StartDocPrinter(hPrinter As IntPtr, level As Integer, di As DOCINFOA) As Boolean
    End Function

    <DllImport("winspool.Drv", EntryPoint:="EndDocPrinter", SetLastError:=True)>
    Public Shared Function EndDocPrinter(hPrinter As IntPtr) As Boolean
    End Function

    <DllImport("winspool.Drv", EntryPoint:="StartPagePrinter", SetLastError:=True)>
    Public Shared Function StartPagePrinter(hPrinter As IntPtr) As Boolean
    End Function

    <DllImport("winspool.Drv", EntryPoint:="EndPagePrinter", SetLastError:=True)>
    Public Shared Function EndPagePrinter(hPrinter As IntPtr) As Boolean
    End Function

    <DllImport("winspool.Drv", EntryPoint:="WritePrinter", SetLastError:=True)>
    Public Shared Function WritePrinter(hPrinter As IntPtr, pBytes As IntPtr, dwCount As Integer, ByRef dwWritten As Integer) As Boolean
    End Function

    Public Shared Function SendStringToPrinter(printerName As String, data As String) As Boolean

        Dim pBytes As IntPtr
        Dim dwCount As Integer = data.Length
        pBytes = Marshal.StringToCoTaskMemAnsi(data)

        Dim hPrinter As IntPtr
        Dim di As New DOCINFOA()

        di.pDocName = "TSPL Barcode"
        di.pDataType = "RAW"

        If OpenPrinter(printerName, hPrinter, IntPtr.Zero) Then

            If StartDocPrinter(hPrinter, 1, di) Then

                If StartPagePrinter(hPrinter) Then

                    Dim dwWritten As Integer
                    WritePrinter(hPrinter, pBytes, dwCount, dwWritten)

                    EndPagePrinter(hPrinter)

                End If

                EndDocPrinter(hPrinter)

            End If

            ClosePrinter(hPrinter)

        End If

        Marshal.FreeCoTaskMem(pBytes)

        Return True

    End Function

End Class
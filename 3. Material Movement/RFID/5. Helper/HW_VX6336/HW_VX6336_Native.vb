Imports System.Runtime.InteropServices

Namespace Devices.RFID.HW_VX6336
    Friend NotInheritable Class HW_VX6336_Native
        Private Const DLLNAME As String = "Basic.dll"

        <DllImport(DLLNAME, EntryPoint:="AutoOpenComPort", CallingConvention:=CallingConvention.StdCall)>
        Public Shared Function AutoOpenComPort(
            ByRef port As Integer,
            ByRef ComAdr As Byte,
            ByRef baud As Byte,
            ByRef FrmHandle As Integer
        ) As Integer
        End Function

        <DllImport(DLLNAME, CallingConvention:=CallingConvention.StdCall)>
        Public Shared Function CloseComPort() As Integer
        End Function

        <DllImport(DLLNAME, CallingConvention:=CallingConvention.StdCall)>
        Public Shared Function Inventory_G2(
            ByRef ComAddr As Byte,
            ByVal AdrTID As Byte,
            ByVal LenTID As Byte,
            ByVal TIDFlag As Byte,
            <Out> EPC As Byte(),
            ByRef Totallen As Integer,
            ByRef CardNum As Integer,
            ByVal PortHandle As Integer
        ) As Integer
        End Function
    End Class
End Namespace

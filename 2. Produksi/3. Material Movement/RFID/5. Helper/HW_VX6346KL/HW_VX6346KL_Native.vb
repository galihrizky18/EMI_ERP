Imports System.Runtime.InteropServices

Namespace Devices.RFID.HW_VX6346KL
    Public NotInheritable Class HW_VX6346KL_Native
        Private Const DLLNAME As String = "Basic.dll"

        <DllImport(DLLNAME, CallingConvention:=CallingConvention.StdCall)>
        Public Shared Function OpenNetPort(
            ByVal Port As Integer,
            ByVal IPaddr As String,
            ByRef ComAddr As Byte,
            ByRef PortHandle As Integer
        ) As Integer
        End Function

        <DllImport(DLLNAME, CallingConvention:=CallingConvention.StdCall)>
        Public Shared Function CloseNetPort(
            ByVal PortHandle As Integer
        ) As Integer
        End Function

        <DllImport(DLLNAME, CallingConvention:=CallingConvention.StdCall)>
        Public Shared Function SetPowerDbm(
            ByRef ComAddr As Byte,
            ByVal PowerDbm As Byte,
            ByVal PortHandle As Integer
        ) As Integer
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

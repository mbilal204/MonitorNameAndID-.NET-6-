Imports System.Runtime.InteropServices
Imports System.Threading

Public Class MainWindow
    Inherits Window

    Public Sub New()
        InitializeComponent()
        GetMonitorInfoForActiveWindow()
    End Sub

    Private Sub GetMonitorInfoForActiveWindow()
        Dim foregroundWindow As IntPtr = NativeMethods.GetForegroundWindow()
        Dim monitorHandle As IntPtr = NativeMethods.MonitorFromWindow(foregroundWindow, NativeMethods.MONITOR_DEFAULTTONEAREST)

        Dim monitorInfo As MONITORINFOEX
        monitorInfo.cbSize = Marshal.SizeOf(GetType(MONITORINFOEX))

        If NativeMethods.GetMonitorInfo(monitorHandle, monitorInfo) Then
            Console.WriteLine("Monitor DeviceName: " & monitorInfo.szDevice)
            Console.WriteLine("Monitor Name: " & monitorInfo.szDevice)
            monitorIdLabel.Content = "Monitor ID: " & monitorInfo.szDevice
            monitorNameLabel.Content = "Monitor Name: " & monitorInfo.szDevice
        End If
    End Sub

    Public Class NativeMethods
        Public Const MONITOR_DEFAULTTONEAREST As Integer = 2

        <DllImport("user32.dll")>
        Public Shared Function GetForegroundWindow() As IntPtr
        End Function

        <DllImport("user32.dll")>
        Public Shared Function MonitorFromWindow(hwnd As IntPtr, dwFlags As Integer) As IntPtr
        End Function

        <DllImport("user32.dll", CharSet:=CharSet.Auto)>
        Public Shared Function GetMonitorInfo(hMonitor As IntPtr, ByRef lpmi As MONITORINFOEX) As Boolean
        End Function
    End Class

    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Auto)>
    Public Structure MONITORINFOEX
        Public cbSize As Integer
        Public rcMonitor As RECT
        Public rcWork As RECT
        Public dwFlags As Integer
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=32)>
        Public szDevice As String
    End Structure

    <StructLayout(LayoutKind.Sequential)>
    Public Structure RECT
        Public left As Integer
        Public top As Integer
        Public right As Integer
        Public bottom As Integer
    End Structure
End Class

Imports System.Data.OracleClient
Imports System.Configuration
Imports System.Data
Imports System.Runtime.InteropServices
Imports System.Net.NetworkInformation
Module Module1
    Public Structure NETRESOURCE
        Public dwScope As Integer
        Public dwType As Integer
        Public dwDisplayType As Integer
        Public dwUsage As Integer
        Public lpLocalName As String
        Public lpRemoteName As String
        Public lpComment As String
        Public lpProvider As String
    End Structure

    <DllImport("mpr.dll", CharSet:=CharSet.Unicode)>
    Public Function WNetAddConnection2(ByRef lpNetResource As NETRESOURCE, ByVal lpPassword As String, ByVal lpUsername As String, ByVal dwFlags As Integer) As Integer
    End Function
    Public Sub CheckConnection()
        AddHandler NetworkChange.NetworkAvailabilityChanged, AddressOf NetworkAvailabilityChangedHandler

        If Not NetworkInterface.GetIsNetworkAvailable() Then
            MessageBox.Show("Không có kết nối mạng vui lòng thử lại!", "Connection Status", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Application.Exit()
        End If
    End Sub
    Public Sub NetworkAvailabilityChangedHandler(sender As Object, e As NetworkAvailabilityEventArgs)
        If Not e.IsAvailable Then
            MessageBox.Show("Không có kết nối mạng vui lòng thử lại!", "Connection Status", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub
End Module

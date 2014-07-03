﻿Public Class Dashy

    Private Sub QuitDashyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles tsmiQuit.Click
        Application.Exit()
    End Sub

    Private Sub MinimizeToTrayToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles tsmiMinimizeToTray.Click
        Me.Hide()
    End Sub

    Private Sub Monitor_Performance_Tick(sender As Object, e As EventArgs) Handles Monitor_Performance.Tick
        lblavailvirtualmem.Text = "Available virtual memory: " & My.Computer.Info.AvailableVirtualMemory
        lblavailphysicalmem.Text = "Available physical memory: " & My.Computer.Info.AvailablePhysicalMemory
    End Sub

    Private Sub Dashy_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Visible = My.Settings.Dashy_UI_HideOnStartup
        GetStaticData()
        Monitor_FileSystem.Start()
        Monitor_Performance.Start()
        Monitor_Network.Start()
        KeyChecker.Start()
    End Sub

    Sub GetStaticData()
        'FileSystem
        For Each drive In My.Computer.FileSystem.Drives
            cbListofDrivesValue.Items.Add(drive)
        Next
        lblProgramFilesDirectoryValue.Text = Environment.GetEnvironmentVariable("PROGRAMFILES")
        'Performance
        lbltotalvirtualmem.Text = "Total virtual memory: " & My.Computer.Info.TotalVirtualMemory
        lbltotalphysicalmem.Text = "Total physical memory: " & My.Computer.Info.TotalPhysicalMemory
        'Hardware
        If Environment.Is64BitOperatingSystem = True Then
            lblArch.Text = "Architecture: 64-Bit"
        Else
            lblArch.Text = "Architecture: 32-Bit"
        End If
        lblProcessors.Text = "Processors: " & Environment.ProcessorCount
        'System
        lblOperatingSystem.Text = "OS:" & My.Computer.Info.OSFullName
        lblOSVersion.Text = "OS Version: " & My.Computer.Info.OSVersion
        lblLoggedInUser.Text = "Logged in user: " & Environment.UserName
    End Sub

    Private Sub Monitor_FileSystem_Tick(sender As Object, e As EventArgs) Handles Monitor_FileSystem.Tick
        lblNumberofDrives.Text = "No. of drives: " & My.Computer.FileSystem.Drives.Count
    End Sub

    Private Sub Monitor_Network_Tick(sender As Object, e As EventArgs) Handles Monitor_Network.Tick
        If My.Computer.Network.IsAvailable = True Then
            lblNetworkConnected.Text = "Connected to network: Yes"
        Else
            lblNetworkConnected.Text = "Connected to network: No"
        End If
        Try
            My.Computer.Network.Ping(My.Settings.Dashy_SiteToPing)
            lblInternetConnection.Text = "Internet connection: Yes"
        Catch ex As Exception
            lblInternetConnection.Text = "Internet connection: No"
        End Try
    End Sub

    Private Sub KeyChecker_Tick(sender As Object, e As EventArgs) Handles KeyChecker.Tick
        If My.Computer.Keyboard.CtrlKeyDown And My.Computer.Keyboard.ShiftKeyDown Then
            If Me.Visible = True Then
                Me.Visible = False
            Else
                Me.Visible = True
            End If
        End If
    End Sub
End Class

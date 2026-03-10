Imports System.Collections.ObjectModel
Imports System.Console
Imports System.Data.OleDb
Imports System.Data.SqlTypes
Imports System.DateTime
Imports System.Timers
Imports LJB_NEA_25_26.cube
Imports Microsoft.Office.Interop

Public Class Timer
    Dim cube As New cube
    Dim sboo As Boolean = False
    Dim swatch As New Stopwatch
    Const DatabaseProvider As String = "Provider=Microsoft.ACE.OLEDB.16.0;"
    Dim MyDatabasePath As String = ""
    Dim MyDatabaseLoaded As Boolean = False
    Dim currentscramble As String
    Dim previousscramble As String
    Dim Conection As OleDbConnection
    Dim Adapter As OleDbDataAdapter
    Private Sub scramgenbutton_Click(sender As Object, e As EventArgs) Handles scramgenbutton.Click
        'generates a scramble for the cube

        Dim times As Integer
        Dim base18(35) As Integer
        Dim scramble(35, 1) As String
        Dim scramblestr As String = ""
        Dim scramblerawstr As String = ""
        Dim change As Boolean
        Dim num1 As Integer
        Dim num2 As Integer
        Dim num3 As Integer = 0

        cube.blank()

        Randomize()
        'randomly generates numbers from and including 0 to 17
        For count = 0 To 35
            base18(count) = vbNull
        Next

        times = 25 + (10 * Rnd())

        For count = 0 To 35

            base18(count) = 17 * Rnd()

        Next

        'adds letters to the base 18 for esier processing
        For count1 = 0 To 35

            Select Case base18(count1)
                Case < 10
                    scramble(count1, 0) = CStr((base18(count1)))
                Case base18(count1) = 10
                    scramble(count1, 0) = ("A")
                Case = 11
                    scramble(count1, 0) = ("B")
                Case = 12
                    scramble(count1, 0) = ("C")
                Case = 13
                    scramble(count1, 0) = ("D")
                Case = 14
                    scramble(count1, 0) = ("E")
                Case = 15
                    scramble(count1, 0) = ("F")
                Case = 16
                    scramble(count1, 0) = ("G")
                Case = 17
                    scramble(count1, 0) = ("H")


            End Select

        Next

        'dose conversion from numbers to the standard notasion 
        For count = 0 To 35

            Select Case scramble(count, 0)
                Case "0"
                    scramble(count, 1) = "R"
                Case "1"
                    scramble(count, 1) = "R'"
                Case "2"
                    scramble(count, 1) = "R2"
                Case "3"
                    scramble(count, 1) = "L"
                Case "4"
                    scramble(count, 1) = "L'"
                Case "5"
                    scramble(count, 1) = "L2"
                Case "6"
                    scramble(count, 1) = "U"
                Case "7"
                    scramble(count, 1) = "U'"
                Case "8"
                    scramble(count, 1) = "U2"
                Case "9"
                    scramble(count, 1) = "D"
                Case "A"
                    scramble(count, 1) = "D'"
                Case "B"
                    scramble(count, 1) = "D2"
                Case "C"
                    scramble(count, 1) = "F"
                Case "D"
                    scramble(count, 1) = "F'"
                Case "E"
                    scramble(count, 1) = "F2"
                Case "F"
                    scramble(count, 1) = "B"
                Case "G"
                    scramble(count, 1) = "B'"
                Case "H"
                    scramble(count, 1) = "B2"
            End Select
        Next


        'cheaks for duplicates and conflicts  e.g D D , or 2D 2D , or D' D  
        num3 = 0
        Do

            change = False
            num3 += 1
            'removes duplicates
            For count = 0 To 34

                If scramble(count, 0) = scramble(count + 1, 0) And scramble(count, 0) <> "" Then
                    change = True
                    scramble(count + 1, 0) = ""
                    scramble(count + 1, 1) = ""
                End If

            Next

            'removes conflicts
            For count = 0 To 34

                If Mid(scramble(count, 1), 1, 1) = Mid(scramble(count + 1, 1), 1, 1) Then
                    If Mid(scramble(count, 1), 1, 1) = "" Then

                    Else
                        change = True
                        scramble(count + 1, 0) = ""
                        scramble(count + 1, 1) = ""
                    End If
                End If

            Next


            'removes spaces in the array
            num1 = 0
            For count = 0 To 35
                If scramble(count, 1) <> "" Then
                    scramble(num1, 0) = scramble(count, 0)
                    scramble(num1, 1) = scramble(count, 1)

                    If count > num1 Then
                        scramble(count, 0) = ""
                        scramble(count, 1) = ""
                    End If

                    num1 += 1
                End If
            Next
            'scramble = scramble2

        Loop Until change = False ' Or num3 = 50

        'outputs what is in the array to text
        num2 = 0
        Do Until scramble(num2, 0) = "" Or num2 = 35
            scramblestr = scramblestr + scramble(num2, 1) + " "
            scramblerawstr = scramblerawstr + scramble(num2, 0) 'for testing
            num2 += 1
        Loop

        'outputs the scramble to the screen
        scramtext.Text = scramblestr
        '     testingoutputtext.Text = scramblerawstr 'for testing

        'stores current for use when entering it into the data base
        currentscramble = scramblestr

        'scrambols the cube and shows it to the user
        For count1 = 0 To 35
            Select Case scramble(count1, 0)
                Case "0"
                    cube.R()
                Case "1"
                    cube.Rprime()
                Case "2"
                    cube.R2()
                Case "3"
                    cube.L()
                Case "4"
                    cube.Lprime()
                Case "5"
                    cube.L2()
                Case "6"
                    cube.U()
                Case "7"
                    cube.Uprime()
                Case "8"
                    cube.U2()
                Case "9"
                    cube.D()
                Case "A"
                    cube.Dprime()
                Case "B"
                    cube.D2()
                Case "C"
                    cube.F()
                Case "D"
                    cube.Fprime()
                Case "E"
                    cube.F2()
                Case "F"
                    cube.B()
                Case "G"
                    cube.Bprime()
                Case "H"
                    cube.B2()
            End Select
            showcube()

        Next

        'testing purpous only
        Dim array(,) As Char = cube.output
        Dim text As String = ""
        For x = 0 To 8
            For y = 0 To 11
                If array(x, y) <> vbNullChar Then
                    text = text + array(x, y)
                Else
                    text = text + "x"
                End If

            Next
        Next
        globals.statemove = text

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        scramtext.Text = ""
        timingtext.Text = ""
        globals.Show()
        globals.Hide()
    End Sub

    Private Sub controlbutton(ByVal buttonname As String, ByVal peicecolour As Char)
        'outputs the correct colour by the coresponding carcter for the colour
        Dim button As Control = Controls(buttonname)

        Select Case peicecolour
            Case "O"
                button.BackColor = Color.DarkOrange
            Case "R"
                button.BackColor = Color.Red
            Case "Y"
                button.BackColor = Color.Yellow
            Case "W"
                button.BackColor = Color.White
            Case "B"
                button.BackColor = Color.Blue
            Case "G"
                button.BackColor = Color.Lime
        End Select
    End Sub

    Private Sub showcube()
        'outputs what the cube looks like to the 2d display
        Dim faces(8, 11) As Char
        Dim buttonname As String
        faces = cube.output()
        faces(0, 0) = "x"
        For x = 0 To 8
            For y = 0 To 11
                If globals.buttonsnames(x, y) = vbNullString Then ' if blanck then output nothing
                Else
                    buttonname = (globals.buttonsnames(x, y) + "Button")
                    controlbutton(buttonname, faces(x, y))
                End If

            Next
        Next
    End Sub

    Private Sub SolverButton_Click(sender As Object, e As EventArgs) Handles SolverButton.Click
        'gose over to the solver function of the program
        Solver.Show()
        Me.Hide()
    End Sub

    Private Sub timerbutton_Click(sender As Object, e As EventArgs) Handles timerbutton.Click
        Dim timeelapsed As Integer
        Dim Minutes As Integer
        Dim Seconds As Integer
        Dim miliiSeconds As Integer
        Dim datetime_ As DateTime = DateTime.Now
        '  Dim MyDataSet As DataSet

        If sboo = False Then
            'starts the stopwatch
            swatch.Start()
            TimerTimer.Start()
            sboo = True
        Else
            TimerTimer.Stop()
            'stops stopwatch
            swatch.Stop()

            'takes the elapesed time in ms and coverts it to minutes seconds and ms
            timeelapsed = swatch.ElapsedMilliseconds.ToString
            sboo = False

            Minutes = timeelapsed \ 60000

            Seconds = (timeelapsed Mod 60000) \ 1000

            miliiSeconds = (timeelapsed Mod 60000) Mod 1000

            'opens the conection to the db
            opendb()

            Conection.Open()
            Adapter = New OleDbDataAdapter("SELECT DateAndTime FROM Times", Conection)


            Dim Time As String = CStr((Format((Minutes), "00") + ":" + Format((Seconds), "00") + "." + Format(miliiSeconds, "000")))
            timingtext.Text = Time
            'enters the timing results into the data base and only enters the scrambole if there is a new one used
            If previousscramble = currentscramble Then
                Dim cmd As New OleDbCommand("INSERT INTO Times (DateAndTime,Scramble,SolveTime,Timems) VALUES (#" & DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") & "#,""" & "Custom" & """,""" & CStr(Time) & """," & CInt(timeelapsed) & ")", Conection)
                If timeelapsed > 1000 Then
                    cmd.ExecuteNonQuery()
                Else
                    MsgBox("Invalid time, to fast")
                End If
            Else
                Dim cmd As New OleDbCommand("INSERT INTO Times (DateAndTime,Scramble,SolveTime,Timems) VALUES (#" & DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") & "#,""" & currentscramble & """,""" & Time & """," & timeelapsed & ")", Conection)
                cmd.ExecuteNonQuery()
                previousscramble = currentscramble
            End If

            ' Dim cmd As New OleDbCommand("INSERT INTO Times (DateAndTime,SolveTime) VALUES (#" & DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") & "#,""" & a & """)", Conection)



            Conection.Close()

            swatch.Reset()
        End If



    End Sub

    Sub opendb()
        'opens db link
        Dim MyOpenFileDialog As OpenFileDialog
        MyOpenFileDialog = New OpenFileDialog
        MyOpenFileDialog.Filter = "MS Access Files (*.accdb)|*.accdb|ALLFiles(*.*)|*.*"
        MyOpenFileDialog.FileName = ""
        MyDatabasePath = ".\CubeDB.accdb"
        Conection = New OleDbConnection(DatabaseProvider & "Data Source=" & MyDatabasePath)
        Conection.Close()
        MyDatabaseLoaded = True

    End Sub
    Private Function input2dstate()
        'takes the state of the 2d output and puts it into an array
        Dim state As String = ""

        For x = 0 To 8
            For y = 0 To 11
                If globals.buttonsnames(x, y) = "" Then
                Else
                    state = state + takecolour(globals.buttonsnames(x, y) + "Button")
                End If
            Next
        Next
        Return state
    End Function
    Private Function takecolour(name As String)
        'takes the colour and returns the correct carcter for the colour
        Dim buttoncontrol As Control = Controls(name)
        If buttoncontrol.BackColor = Color.DarkOrange Then
            Return "O"
        ElseIf buttoncontrol.BackColor = Color.Red Then
            Return "R"
        ElseIf buttoncontrol.BackColor = Color.Yellow Then
            Return "Y"
        ElseIf buttoncontrol.BackColor = Color.White Then
            Return "W"
        ElseIf buttoncontrol.BackColor = Color.Blue Then
            Return "B"
        ElseIf buttoncontrol.BackColor = Color.Lime Then
            Return "G"
        Else
            Return ""
        End If
    End Function

    Private Sub ExeclButton_Click(sender As Object, e As EventArgs) Handles ExeclButton.Click
        'takes the solve times for the database and putsthem into a spreadsheet for the user
        Dim MyDataSet As DataSet
        Dim excelapp As New Excel.Application
        Dim TheTime
        opendb()

        Conection.Open()
        Adapter = New OleDbDataAdapter("SELECT * FROM Times ORDER BY Timems", Conection)
        MyDataSet = New DataSet

        Adapter.Fill(MyDataSet, "Times")

        Conection.Close()

        excelapp.SheetsInNewWorkbook = 1
        excelapp.Workbooks.Add()
        excelapp.Worksheets.Select()
        excelapp.Cells(1, 1).value = CStr(MyDataSet.Tables(0).Columns(0).ColumnName)
        excelapp.Cells(1, 2).value = CStr(MyDataSet.Tables(0).Columns(1).ColumnName)
        excelapp.Cells(1, 3).value = CStr(MyDataSet.Tables(0).Columns(2).ColumnName)
        excelapp.Cells(1, 4).value = CStr(MyDataSet.Tables(0).Columns(3).ColumnName)
        For i = 0 To MyDataSet.Tables(0).Rows.Count - 1
            For j = 0 To MyDataSet.Tables(0).Columns.Count - 1

                If j = 2 Then
                    excelapp.Cells(i + 2, j + 1).value = """" & CStr(MyDataSet.Tables(0).Rows(i)(j)) & """"
                Else
                    excelapp.Cells(i + 2, j + 1).value = CStr(MyDataSet.Tables(0).Rows(i)(j))
                End If

            Next

        Next

        TheTime = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss")
        excelapp.ActiveCell.Worksheet.SaveAs(TheTime & "_times.xlsx")
        excelapp.Visible = True
    End Sub

    Private Sub CloseButton_Click(sender As Object, e As EventArgs) Handles CloseButton.Click
        'closes all forms
        globals.Close()
        Solver.Close()
        Me.Close()
    End Sub

    Private Sub TimerTimer_Tick(sender As Object, e As EventArgs) Handles TimerTimer.Tick
        'shows the time to the user every milisecond when running
        Dim timeelapsed As Integer
        Dim Minutes As Integer
        Dim Seconds As Integer
        Dim miliiSeconds As Integer

        timeelapsed = swatch.ElapsedMilliseconds.ToString

        Minutes = timeelapsed \ 60000

        Seconds = (timeelapsed Mod 60000) \ 1000

        miliiSeconds = (timeelapsed Mod 60000) Mod 1000

        timingtext.Text = (Format((Minutes), "00") + ":" + Format((Seconds), "00") + ":" + Format(miliiSeconds, "000"))
    End Sub
End Class
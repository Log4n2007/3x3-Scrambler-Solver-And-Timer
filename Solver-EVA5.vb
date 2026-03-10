Imports LJB_NEA_25_26.cube
Imports System.Data.OleDb
Public Class Solver
    Const DatabaseProvider As String = "Provider=Microsoft.ACE.OLEDB.16.0;"
    Dim MyDatabasePath As String = ""
    Dim MyDatabaseLoaded As Boolean = False

    Dim Conection As OleDbConnection
    Dim Adapter As OleDbDataAdapter
    Dim MyDataSet As DataSet


    Dim cube As New cube
    Private Sub Button_Click(sender As Object, e As EventArgs) Handles ULYButton.Click, UCYButton.Click, URYButton.Click, CLYButton.Click, CRYButton.Click, BLYButton.Click, BCYButton.Click, BRYButton.Click, ULBButton.Click, UCBButton.Click, URBButton.Click, CLBButton.Click, CRBButton.Click, BLBButton.Click, BCBButton.Click, BRBButton.Click, ULOButton.Click, UCOButton.Click, UROButton.Click, CLOButton.Click, CROButton.Click, BLOButton.Click, BCOButton.Click, BROButton.Click, ULRButton.Click, UCRButton.Click, URRButton.Click, CLRButton.Click, CRRButton.Click, BLRButton.Click, BCRButton.Click, BRRButton.Click, ULGButton.Click, UCGbutton.Click, URGbutton.Click, CLGbutton.Click, CRGbutton.Click, BLGbutton.Click, BCGButton.Click, BRGButton.Click, ULWButton.Click, UCWButton.Click, URWButton.Click, CLWButton.Click, CRWButton.Click, BLWButton.Click, BCWButton.Click, BRWButton.Click
        'handles the clicking of thebuttons on the 2d display and sends them to change there colour
        changecolour(sender.name)
    End Sub

    Private Sub changecolour(name As String)
        'changes the colour of the peice to the next one in the sequence
        Dim buttoncontrol As Control = Controls(name)

        If buttoncontrol.BackColor = Color.DarkOrange Then
            buttoncontrol.BackColor = Color.Red
        ElseIf buttoncontrol.BackColor = Color.Red Then
            buttoncontrol.BackColor = Color.Yellow
        ElseIf buttoncontrol.BackColor = Color.Yellow Then
            buttoncontrol.BackColor = Color.White
        ElseIf buttoncontrol.BackColor = Color.White Then
            buttoncontrol.BackColor = Color.Blue
        ElseIf buttoncontrol.BackColor = Color.Blue Then
            buttoncontrol.BackColor = Color.Lime
        ElseIf buttoncontrol.BackColor = Color.Lime Then
            buttoncontrol.BackColor = Color.DarkOrange
        End If
    End Sub

    Private Sub Solver_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        testtext.Text = ""
        algrithmnametext.Text = ""
        Movestext.Text = ""
    End Sub

    Private Sub timerButton_Click(sender As Object, e As EventArgs) Handles timerButton.Click
        'gose to the timing and scrambling function of the program
        Form1.Show()
        Me.Hide()
    End Sub

    Private Function input2dstate()
        'takes the state of the 2d output and puts it into an array
        Dim state(8, 11) As Char

        For x = 0 To 8
            For y = 0 To 11
                If globals.buttonsnames(x, y) = "" Then
                Else
                    state(x, y) = takecolour(globals.buttonsnames(x, y) + "Button")
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

    Private Sub Solve_Button_Click(sender As Object, e As EventArgs) Handles Solve_Button.Click
        'starts the solving process
        If PresumedsSolvableState() = True Then
            solve()
        End If

    End Sub
    Private Function PresumedsSolvableState()
        'cheaks if the cube is in a solvable state and outputs an error if not
        'cannot cheak if it is in an unsolvable state but dosnt have conflicts or the wrong number of colours
        Dim solvable As Boolean = True
        If colourcheck(input2dstate) = False Then
            MsgBox("Too many of a colour")
            solvable = False
        End If
        If conflictcheck() = False Then
            MsgBox("conflicting colours on a peice")
            solvable = False
        End If
        Return solvable
    End Function
    Function colourcheck(ByVal state(,) As Char)
        Dim solvable As Boolean = True
        Dim colourcount(5) As Integer ' in order Y,W,O,R,B,G

        For x = 0 To 8
            For y = 0 To 11
                Select Case state(x, y)
                    Case = "Y"
                        colourcount(0) += 1
                    Case = "W"
                        colourcount(1) += 1
                    Case = "O"
                        colourcount(2) += 1
                    Case = "R"
                        colourcount(3) += 1
                    Case = "B"
                        colourcount(4) += 1
                    Case = "G"
                        colourcount(5) += 1
                End Select
            Next
        Next

        For count = 0 To 5
            If colourcount(count) <> 9 Then
                solvable = False
            End If
        Next

        Return solvable
    End Function

    Function conflictcheck()
        Dim solvable As Boolean = True
        cube.inputstate(input2dstate())
        solvable = cube.conflictcheck

        'need to cheack on each peice that there are no oposite colours on each peice and there are no same colours on a peice
        'will need to access each peice indervidualy and compare 
        'think this should be done as part of the object
        'might move the other one to part of the object as well 
        'if done make out put an arr ay of 1d 2 as boo for colour then conflict

        Return solvable
    End Function

    Sub solve()
        Dim text As String

        opendb()

        ''
        Conection.Open()
        Adapter = New OleDbDataAdapter("SELECT * FROM CFOP", Conection)  'SELECT Mask FROM CFOP WHERE Step = ""Cross""
        MyDataSet = New DataSet

        Adapter.Fill(MyDataSet, "CFOP")

        ' DataGridView1.DataSource = MyDataSet.Tables("CFOP")

        Conection.Close()
        ''

        testtext.Text = Adapter.ToString()

        patternmatch(texttoarray(text), cube.output)


        '   = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxWW11xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx"
        '     "xxxxxxOOOxxxxxxxxxOOOxxxxxxxxxOOOxxxGGGWWWBBBYYYGGGWWWBBBYYYGGGWWWBBBYYYxxxxxxRRRxxxxxxxxxRRRxxxxxxxxxRRRxxx"  ' up and to the left
        '           '   

        ' wether this is own module or not this dose the partern matching and the database
        'this should be hevily split

        'PATERN MATCHING
        'uppercase of colour then can only bethat colour
        'X means that it is not needed for that partern
        'how to do it for when can match multible     number if the numbered ones on partern are the same on cube then fine    dont bohther with specifics
        'how to store these as

        'if none found after going through everything then output not valid state

    End Sub

    Function texttoarray(ByVal Rawmask As String)
        'the mask of the pattern is stored as a long string so this converts it into an array that can be used for the parttern matching
        Dim mask(8, 11) As Char
        Dim counter As Integer = 1

        For x = 0 To 8
            For y = 0 To 11

                mask(x, y) = Mid(Rawmask, counter, 1)
                counter += 1

            Next
        Next
        Return mask
    End Function

    Function patternmatch(ByVal Mask(,) As Char, ByVal cube2d(,) As Char)
        Dim match As Boolean = True
        Dim one As Char = vbNullChar
        Dim two As Char = vbNullChar
        Dim three As Char = vbNullChar
        Dim four As Char = vbNullChar

        For x = 0 To 8
            For y = 0 To 11

                If Mask(x, y) = "x" Then
                    ' no need to cheack as x means not needed

                ElseIf Mask(x, y) = "Y" Or Mask(x, y) = "W" Or Mask(x, y) = "O" Or Mask(x, y) = "R" Or Mask(x, y) = "G" Or Mask(x, y) = "B" Then
                    'if it is one of the colours letters then the cube must match it

                    If Mask(x, y) <> cube2d(x, y) Then
                        match = False
                    End If

                    'for a number it can be any colour but all numbers on mask must match to the same colour on cube
                    'there can be up to 4 of theses in a mask this is the first one
                ElseIf Mask(x, y) = "1" Then

                    If one = vbNullChar Then
                        one = cube2d(x, y)

                    Else
                        If one = cube2d(x, y) Then
                        Else
                            match = False
                        End If


                    End If

                    'there can be up to 4 of theses in a mask this is the second one
                ElseIf Mask(x, y) = "2" Then

                    If two = vbNullChar Then
                        two = cube2d(x, y)

                    Else
                        If two = cube2d(x, y) Then
                        Else
                            match = False
                        End If


                    End If


                    'there can be up to 4 of theses in a mask this is the third one
                ElseIf Mask(x, y) = "3" Then

                    If three = vbNullChar Then
                        three = cube2d(x, y)

                    Else
                        If three = cube2d(x, y) Then
                        Else
                            match = False
                        End If



                    End If

                    'there can be up to 4 of theses in a mask this is the forth one
                ElseIf Mask(x, y) = "4" Then

                    If four = vbNullChar Then
                        four = cube2d(x, y)

                    Else
                        If four = cube2d(x, y) Then
                        Else
                            match = False
                        End If



                    End If
                End If



            Next
        Next

        Return match
    End Function

    Sub opendb()
        'opens db link
        Dim MyOpenFileDialog As OpenFileDialog
        MyOpenFileDialog = New OpenFileDialog
        MyOpenFileDialog.Filter = "MS Access Files (*.accdb)|*.accdb|ALLFiles(*.*)|*.*"
        MyOpenFileDialog.FileName = ""

        MyDatabasePath = "C:\Users\logan.INT\OneDrive - Maidstone Grammar School\NEA 25-26\CubeDB.accdb" '"C:\Users\19lblount\OneDrive - Maidstone Grammar School\NEA 25-26\CubeDB.accdb"
        Conection = New OleDbConnection(DatabaseProvider & "Data Source=" & MyDatabasePath)
        Conection.Close()
        MyDatabaseLoaded = True
    End Sub
End Class
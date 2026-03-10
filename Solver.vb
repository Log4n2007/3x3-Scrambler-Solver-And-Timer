Imports LJB_NEA_25_26.cube
Imports System.Data.OleDb
Imports System.DateTime
Imports System.Timers
Public Class Solver
    Const DatabaseProvider As String = "Provider=Microsoft.ACE.OLEDB.16.0;"
    Dim MyDatabasePath As String = ""
    Dim MyDatabaseLoaded As Boolean = False
    Dim solved As Boolean
    Enum Colour
        Unslected = 0
        Red = 1
        Green = 2
        Blue = 3
        Orange = 4
        Yellow = 5
        White = 6
    End Enum
    Dim selectedcolour As Colour = 0

    Dim Conection As OleDbConnection
    Dim Adapter As OleDbDataAdapter

    Dim _cube As New cube

    Private Sub Button_Click(sender As Object, e As EventArgs) Handles ULYButton.Click, UCYButton.Click, URYButton.Click, CLYButton.Click, CRYButton.Click, BLYButton.Click, BCYButton.Click, BRYButton.Click, ULBButton.Click, UCBButton.Click, URBButton.Click, CLBButton.Click, CRBButton.Click, BLBButton.Click, BCBButton.Click, BRBButton.Click, ULOButton.Click, UCOButton.Click, UROButton.Click, CLOButton.Click, CROButton.Click, BLOButton.Click, BCOButton.Click, BROButton.Click, ULRButton.Click, UCRButton.Click, URRButton.Click, CLRButton.Click, CRRButton.Click, BLRButton.Click, BCRButton.Click, BRRButton.Click, ULGButton.Click, UCGbutton.Click, URGbutton.Click, CLGbutton.Click, CRGbutton.Click, BLGbutton.Click, BCGButton.Click, BRGButton.Click, ULWButton.Click, UCWButton.Click, URWButton.Click, CLWButton.Click, CRWButton.Click, BLWButton.Click, BCWButton.Click, BRWButton.Click
        'handles the clicking of thebuttons on the 2d display and sends them to change there colour
        changecolour(sender.name)
    End Sub

    Private Sub changecolour(name As String)
        'changes the colour of the peice based on what is selected
        Dim buttoncontrol As Control = Controls(name)

        Select Case selectedcolour
            Case = Colour.Red
                buttoncontrol.BackColor = Color.Red
            Case = Colour.Blue
                buttoncontrol.BackColor = Color.Blue
            Case = Colour.Green
                buttoncontrol.BackColor = Color.Lime
            Case = Colour.Orange
                buttoncontrol.BackColor = Color.DarkOrange
            Case = Colour.Yellow
                buttoncontrol.BackColor = Color.Yellow
            Case = Colour.White
                buttoncontrol.BackColor = Color.White
        End Select

    End Sub

    Private Sub Solver_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        testtext.Text = ""
        algrithmnametext.Text = ""
        Movestext.Text = ""
        globals.Show()
        globals.Hide()

        'testing purpouses
        '"xxxxxxRGWxxxxxxxxxWORxxxxxxxxxWYYxxxGRYBOGRRRGWOWGGYWWBBBYYGOYWBOBORBWBYxxxxxxYBOxxxxxxxxxGROxxxxxxxxxROGxxx"
        '_cube.inputstate(texttoarray("xxxxxxRGWxxxxxxxxxWORxxxxxxxxxWYYxxxGRYBOGRRRGWOWGGYWWBBBYYGOYWBOBORBWBYxxxxxxYBOxxxxxxxxxGROxxxxxxxxxROGxxx"))
        '    _cube.inputstate(texttoarray(globals.statemove))

        showcube(_cube)
    End Sub

    Private Sub timerButton_Click(sender As Object, e As EventArgs) Handles timerButton.Click
        'gose to the timing and scrambling function of the program
        Timer.Show()
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
        _cube.inputstate(input2dstate())
        If colourcheck() = False Then
            MsgBox("Too many of a colour")
            solvable = False
        End If
        If conflictcheck() = False Then
            MsgBox("conflicting colours on a peice")
            solvable = False
        End If
        Return solvable
    End Function
    Function colourcheck()
        'calls the check for more or less colours htan needed
        Return _cube.colourcheck
    End Function

    Function conflictcheck()
        'calls the check for conflicting colours on a peice
        Dim solvable As Boolean = True

        solvable = _cube.conflictcheck

        Return solvable
    End Function
    Sub solve()
        ' Dim text As String
        Dim scramblestate(,) As Char
        Dim dummycube As New cube
        Dim dummycube2 As New cube
        Dim outputcube As New cube
        Dim i As Integer
        Dim solvesequenceC(,) As String
        Dim solvesequenceF(,) As String
        Dim solvesequenceO(,) As String
        Dim solvesequenceP(,) As String
        solved = False
        ' Dim mydatareader As DataTableReader

        scramblestate = input2dstate()

        'takes the input of what the user enered on screen and putsit into the objects to be solved
        dummycube.inputstate(scramblestate)
        outputcube.inputstate(scramblestate)
        If patternmatch(texttoarray("xxxxxx111xxxxxxxxx111xxxxxxxxx111xxx222555333666222555333666222555333666xxxxxx444xxxxxxxxx444xxxxxxxxx444xxx"), dummycube.output) = True Then
            MsgBox("Already Solved")
        Else

            'solves the first step 
            solvesequenceC = Cross(dummycube)
            '   showcube(dummycube)
            '   MsgBox("aaa")

            'dose the second solving step
            solvesequenceF = F2L(dummycube)

            'dose the third solving step
            solvesequenceO = OLL(dummycube)

            'dose the final solving step
            solvesequenceP = PLL(dummycube)

            'only out puts to the user the solving sequence if the cube can realy be solved  by it
            If solved = True Then

                'dose and outputs each step in turn
                i = 0
                Do Until solvesequenceC(i, 2) = "1"
                    If solvesequenceC(i, 1) <> "" Then
                        move(outputcube, solvesequenceC(i, 1))
                        algrithmnametext.Text = solvesequenceC(i, 0)
                        Movestext.Text = solvesequenceC(i, 1)
                        showcube(outputcube)
                        MsgBox("Next Step")
                    End If
                    i = i + 1
                Loop

                i = 0
                Do Until solvesequenceF(i, 2) = "1"
                    If solvesequenceF(i, 1) <> "" Then
                        move(outputcube, solvesequenceF(i, 1))
                        algrithmnametext.Text = solvesequenceF(i, 0)
                        Movestext.Text = solvesequenceF(i, 1)
                        showcube(outputcube)
                        MsgBox("Next Step")
                    End If
                    i = i + 1
                Loop

                i = 0
                Do Until solvesequenceO(i, 2) = "1"
                    If solvesequenceO(i, 1) <> "" Then
                        move(outputcube, solvesequenceO(i, 1))
                        algrithmnametext.Text = solvesequenceO(i, 0)
                        Movestext.Text = solvesequenceO(i, 1)
                        showcube(outputcube)
                        MsgBox("Next Step")
                    End If
                    i = i + 1
                Loop

                i = 0
                Do Until solvesequenceP(i, 2) = "1"
                    If solvesequenceP(i, 1) <> "" Then
                        move(outputcube, solvesequenceP(i, 1))
                        algrithmnametext.Text = solvesequenceP(i, 0)
                        Movestext.Text = solvesequenceP(i, 1)
                        showcube(outputcube)
                        MsgBox("Next Step")
                    End If
                    i = i + 1
                Loop

            ElseIf solved = False Then
                'tells the user if it carnt be solved
                showcube(outputcube)
                algrithmnametext.Text = ""
                Movestext.Text = ""
                MsgBox("Cube not in a valid state")
            End If

        End If
        ' MsgBox("aaaaa")

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
        'patermatching between two 2D nets of cube with the first one being the patern and second being the cube it is being done against
        Dim match As Boolean = True
        Dim one As Char = vbNullChar
        Dim two As Char = vbNullChar
        Dim three As Char = vbNullChar
        Dim four As Char = vbNullChar
        Dim five As Char = vbNullChar
        Dim six As Char = vbNullChar

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
                    'this is the first one
                ElseIf Mask(x, y) = "1" Then

                    If one = vbNullChar Then
                        one = cube2d(x, y)

                    Else
                        If one = cube2d(x, y) Then
                        Else
                            match = False
                        End If


                    End If

                    'this is the second one
                ElseIf Mask(x, y) = "2" Then

                    If two = vbNullChar Then
                        two = cube2d(x, y)

                    Else
                        If two = cube2d(x, y) Then
                        Else
                            match = False
                        End If


                    End If


                    ' this is the third one
                ElseIf Mask(x, y) = "3" Then

                    If three = vbNullChar Then
                        three = cube2d(x, y)

                    Else
                        If three = cube2d(x, y) Then
                        Else
                            match = False
                        End If



                    End If

                    'this is the forth one
                ElseIf Mask(x, y) = "4" Then

                    If four = vbNullChar Then
                        four = cube2d(x, y)

                    Else
                        If four = cube2d(x, y) Then
                        Else
                            match = False
                        End If

                    End If

                    'this is the fith one
                ElseIf Mask(x, y) = "5" Then

                    If five = vbNullChar Then
                        five = cube2d(x, y)

                    Else
                        If five = cube2d(x, y) Then
                        Else
                            match = False
                        End If

                    End If

                    'this is the sixth one
                ElseIf Mask(x, y) = "6" Then

                    If six = vbNullChar Then
                        six = cube2d(x, y)

                    Else
                        If six = cube2d(x, y) Then
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


        MyDatabasePath = ".\CubeDB.accdb"
        Conection = New OleDbConnection(DatabaseProvider & "Data Source=" & MyDatabasePath)
        Conection.Close()
        MyDatabaseLoaded = True
    End Sub
    Function Cross(ByVal cube As cube)
        Dim MyDataSet As DataSet
        Dim text As String
        Dim j As Integer
        Dim i As Integer
        Dim sequence(50, 2) As String
        opendb()


        'takes the mask's , sequenc and name's  for the algrithms used in this step
        Conection.Open()
        Adapter = New OleDbDataAdapter("SELECT Mask,AlgrithmSequence,AlgorithmName FROM CFOP WHERE Step = ""Cross""", Conection)  'SELECT Mask FROM CFOP WHERE Step = ""Cross"" SELECT * FROM CFOP
        MyDataSet = New DataSet

        Adapter.Fill(MyDataSet, "CFOP")


        Conection.Close()
        j = 0


        'gose through and solves each cross peice
        For k = 0 To 3
            i = 0

            Do Until i = MyDataSet.Tables(0).Rows.Count Or i = -1

                text = MyDataSet.Tables(0).Rows(i)(0).ToString()

                'sees if the partern matches the cube
                If patternmatch(texttoarray(text), cube.output) = True Then
                    ' if the pattern matches the it dose the pattern and to the cube before moving on as well as storing what id did
                    sequence(j, 0) = MyDataSet.Tables(0).Rows(i)(2)
                    sequence(j, 1) = MyDataSet.Tables(0).Rows(i)(1).ToString
                    sequence(j, 2) = "0"
                    If MyDataSet.Tables(0).Rows(i)(1).ToString <> "" Then
                        cube = move(cube, MyDataSet.Tables(0).Rows(i)(1))
                        '  showcube(cube)
                        '  MsgBox("aa")
                    End If
                    j = j + 1
                    i = -2
                End If
                i = i + 1
            Loop

            'turns for the next side
            cube.YRotation()
            sequence(j, 0) = "Turn"
            sequence(j, 1) = "Y"
            sequence(j, 2) = "0"
            j = j + 1
        Next
        ' showcube(cube)
        ' MsgBox("aa")

        '1 indicates end of that steps algrithms
        sequence(j, 2) = "1"
        Return sequence
    End Function
    Function F2L(cube As cube)
        Dim sequence(50, 2) As String
        Dim text As String
        Dim MyDataSet As DataSet
        '   Dim cuberaw As blankcube
        Dim fcolour As Char
        Dim scolour As Char
        Dim CInPlace As Boolean
        Dim bottomc As Boolean
        Dim EInPlace As Boolean
        Dim x As Integer
        Dim y As Integer
        Dim j As Integer = 0
        Dim i As Integer
        Dim xc As Char
        Dim zc As Char
        Dim xzy As String
        Const white As Char = "W"
        ' showcube(cube)
        ' MsgBox("aa")
        opendb()
        Conection.Open()
        Adapter = New OleDbDataAdapter("SELECT Mask,AlgrithmSequence,AlgorithmName FROM CFOP WHERE Step = ""F2L""", Conection)  'SELECT Mask FROM CFOP WHERE Step = ""Cross"" SELECT * FROM CFOP
        MyDataSet = New DataSet

        Adapter.Fill(MyDataSet, "CFOP")

        'code for F2l step multi stage will be very long

        For k = 0 To 3
            '   showcube(cube)
            '    MsgBox("aa")
            CInPlace = False
            bottomc = False
            EInPlace = False

            fcolour = cube.cube(1, 1, 0).center.colour
            scolour = cube.cube(2, 1, 1).center.colour
            If fcolour = "O" Or fcolour = "R" Then
                xc = fcolour
            ElseIf fcolour = "B" Or fcolour = "G" Then
                zc = fcolour
            End If
            If scolour = "O" Or scolour = "R" Then
                xc = scolour
            ElseIf scolour = "B" Or scolour = "G" Then
                zc = scolour
            End If
            xzy = xc & zc & white
            'See where corresponding corner Is if in correct place do nothing 

            ' MsgBox(cube.cube(2, 0, 0).corner.identifier)
            If cube.cube(2, 0, 0).corner.identifier = xzy Then
                CInPlace = True
                bottomc = True
                ' showcube(cube)
                ' MsgBox("200")'
            End If


            If CInPlace = False Then

                '(0,2,0) (2,2,0) (2,2,2) (0,2,2)
                x = 0
                y = 0
                ' MsgBox(cube.cube(x, 2, y).corner.identifier)
                If cube.cube(x, 2, y).corner.identifier = xzy Then
                    cube.Uprime()
                    sequence(j, 0) = "Turn"
                    sequence(j, 1) = "U'"
                    sequence(j, 2) = "0"
                    j = j + 1
                    CInPlace = True
                    ' showcube(cube)
                    '  MsgBox("020")'
                End If
            End If

            x = 2
            y = 0
            If CInPlace = False Then
                '   MsgBox(cube.cube(x, 2, y).corner.identifier)
                If cube.cube(x, 2, y).corner.identifier = xzy Then
                    CInPlace = True

                    ' showcube(cube)
                    ' MsgBox("220")
                    '  showcube(cube)
                End If
            End If

            x = 2
            y = 2
            If CInPlace = False Then
                '  MsgBox(cube.cube(x, 2, y).corner.identifier)
                If cube.cube(x, 2, y).corner.identifier = xzy Then
                    cube.U()


                    sequence(j, 0) = "Turn"
                    sequence(j, 1) = "U"
                    sequence(j, 2) = "0"
                    j = j + 1
                    CInPlace = True
                    '  showcube(cube)
                    '  MsgBox("222")
                End If
            End If

            x = 0
            y = 2
            If CInPlace = False Then
                If cube.cube(x, 2, y).corner.identifier = xzy Then
                    cube.U2()

                    sequence(j, 0) = "Turn"
                    sequence(j, 1) = "U2"
                    sequence(j, 2) = "0"
                    j = j + 1
                    CInPlace = True
                    ' showcube(cube)
                    ' MsgBox("022")
                End If
            End If


            'if Not see if in bottom layers
            'then it looks for where it could be in the top layer and“ejects” to be in the correct place  in the top layer
            'bottom layer
            If CInPlace = False Then

                '(0,0,0) (2,0,2) (0,0,2)
                x = 0
                y = 0
                If cube.cube(x, 0, y).corner.identifier = xzy Then
                    'L',U',L
                    cube.Lprime()
                    cube.Uprime()
                    cube.L()
                    sequence(j, 0) = "Move corner Into Place"
                    sequence(j, 1) = "L',U',L"
                    sequence(j, 2) = "0"
                    j = j + 1
                    CInPlace = True
                    ' showcube(cube)
                    ' MsgBox("000")
                End If
            End If

            x = 2
                y = 2
                If CInPlace = False Then
                If cube.cube(x, 0, y).corner.identifier = xzy Then
                    'R',U',R,U2
                    cube.Rprime()
                    cube.Uprime()
                    cube.R()
                    cube.U2()
                    sequence(j, 0) = "Move corner Into Place"
                    sequence(j, 1) = "R',U',R,U2"
                    sequence(j, 2) = "0"
                    j = j + 1
                    CInPlace = True
                    '  showcube(cube)
                    '  MsgBox("202")
                End If
            End If


            x = 0
                y = 2
            If CInPlace = False Then
                If cube.cube(x, 0, y).corner.identifier = xzy Then
                    'L,U2,L'
                    cube.L()
                    cube.U2()
                    cube.Lprime()
                    sequence(j, 0) = "Move corner Into Place"
                    sequence(j, 1) = "L,U2,L'"
                    sequence(j, 2) = "0"
                    j = j + 1
                    CInPlace = True
                    '  showcube(cube)
                    '  MsgBox("002")
                End If
            End If


            'testing
            ' showcube(cube)
            '  MsgBox("aa")

            xzy = xc & zc
            'Then look for the correct edge using same method as for corner
            'sees if it is in most optimal place for it to be
            If cube.cube(2, 1, 0).edge.identifier = xzy Then

                EInPlace = True
            End If


            'if edge Is in top layer but Not in a place for an algorithm then enter the 

            ' If it is “ejected” for an un optimal place then needs to Not disturbed the edge 
            'for edge in top layer
            If EInPlace = False Then

                '(1,2,0) (2,2,1) (1,2,2) ( 0,2,1)

                x = 1
                y = 0

                If cube.cube(x, 2, y).edge.identifier = xzy Then
                    EInPlace = True
                    '   showcube(cube)
                    '   MsgBox("120")
                End If
            End If

            x = 2
                y = 1
            If EInPlace = False Then
                If cube.cube(x, 2, y).edge.identifier = xzy Then
                    EInPlace = True
                    '   showcube(cube)
                    '   MsgBox("221")
                End If
            End If

            x = 1
                y = 2
            If EInPlace = False Then
                If cube.cube(x, 2, y).edge.identifier = xzy Then
                    EInPlace = True
                    If bottomc = True Then
                        'U2
                        cube.U2()
                        sequence(j, 0) = "Move edge Into Place"
                        sequence(j, 1) = "U2"
                        sequence(j, 2) = "0"
                        j = j + 1
                    End If
                    '  showcube(cube)
                    ' MsgBox("122")
                End If
            End If

            x = 0
                y = 1
                If EInPlace = False Then
                If cube.cube(x, 2, y).edge.identifier = xzy Then

                    EInPlace = True
                    If bottomc = True Then
                        'U2
                        cube.U2()
                        sequence(j, 0) = "Move edge Into Place"
                        sequence(j, 1) = "U2"
                        sequence(j, 2) = "0"
                        j = j + 1

                    End If
                    '  showcube(cube)
                    '  MsgBox("021")
                End If
            End If

            'for edge in middle layers
            If EInPlace = False Then

                '(0,1,0) (2,1,2) (0,1,2)
                x = 0
                y = 0
                If cube.cube(x, 1, y).edge.identifier = xzy Then
                    If bottomc = False Then
                        cube.R()
                        cube.U()
                        cube.Rprime()
                        sequence(j, 0) = "Move corner Into Place"
                        sequence(j, 1) = "R,U,R'"
                        sequence(j, 2) = "0"
                        j = j + 1
                    End If
                    'L',U',L
                    cube.Lprime()
                    cube.Uprime()
                    cube.L()
                    sequence(j, 0) = "Move edge Into Place"
                    sequence(j, 1) = "L',U',L"
                    sequence(j, 2) = "0"
                    j = j + 1
                    EInPlace = True
                    '   showcube(cube)
                    '   MsgBox("010")
                End If
            End If

            x = 2
                y = 2
                If EInPlace = False Then
                If cube.cube(x, 1, y).edge.identifier = xzy Then
                    If bottomc = False Then
                        cube.R()
                        cube.U()
                        cube.Rprime()
                        sequence(j, 0) = "Move corner Into Place"
                        sequence(j, 1) = "R,U,R'"
                        sequence(j, 2) = "0"
                        j = j + 1
                    End If
                    'B,U,B'
                    cube.B()
                    cube.U()
                    cube.Bprime()
                    sequence(j, 0) = "Move edge Into Place"
                    sequence(j, 1) = "B,U,B'"
                    sequence(j, 2) = "0"
                    j = j + 1
                    EInPlace = True
                    '  showcube(cube)
                    '   MsgBox("212")
                End If
            End If

            x = 0
                y = 2
            If EInPlace = False Then
                If cube.cube(x, 1, y).edge.identifier = xzy Then
                    If bottomc = False Then
                        cube.R()
                        cube.U()
                        cube.Rprime()
                        sequence(j, 0) = "Move corner Into Place"
                        sequence(j, 1) = "R,U,R'"
                        sequence(j, 2) = "0"
                        j = j + 1
                    End If
                    'L,U',L'
                    cube.L()
                    cube.Uprime()
                    cube.Lprime()
                    sequence(j, 0) = "Move edge Into Place"
                    sequence(j, 1) = "L,U',L'"
                    sequence(j, 2) = "0"
                    j = j + 1
                    EInPlace = True
                    '   showcube(cube)
                    '   MsgBox("012")
                End If
            End If

            'sees if the partern matches the cube
            i = 0
            Do Until i = MyDataSet.Tables(0).Rows.Count Or i = -1

                text = MyDataSet.Tables(0).Rows(i)(0).ToString()

                If patternmatch(texttoarray(text), cube.output) = True Then

                    sequence(j, 0) = MyDataSet.Tables(0).Rows(i)(2)
                    sequence(j, 1) = MyDataSet.Tables(0).Rows(i)(1).ToString
                    sequence(j, 2) = "0"
                    If MyDataSet.Tables(0).Rows(i)(1).ToString <> "" Then
                        cube = move(cube, MyDataSet.Tables(0).Rows(i)(1))
                        '  showcube(cube)
                        '  MsgBox("aa")
                    End If
                    j = j + 1
                    i = -2
                End If
                i = i + 1
            Loop

            'turns for the next side
            cube.YRotation()
            sequence(j, 0) = "Turn"
            sequence(j, 1) = "Y"
            sequence(j, 2) = "0"
            j = j + 1


            'corner And position the edge near before running the algorithm check 

            '  showcube(cube)
            '  MsgBox("aa")
        Next
        'Repeats for all f2l's

        '  showcube(cube)
        '   MsgBox("a")
        sequence(j, 2) = "1"
        Return sequence
    End Function
    Function OLL(cube As cube)
        Dim MyDataSet As DataSet

        Dim text As String
        Dim j As Integer
        Dim i As Integer
        Dim Correct As Boolean = False
        Dim sequence(50, 2) As String
        'showcube(cube)
        'MsgBox("aa")
        opendb()
        Conection.Open()
        Adapter = New OleDbDataAdapter("SELECT Mask,AlgrithmSequence,AlgorithmName FROM CFOP WHERE Step = ""OLL""", Conection)  'SELECT Mask FROM CFOP WHERE Step = ""Cross"" SELECT * FROM CFOP
        MyDataSet = New DataSet

        Adapter.Fill(MyDataSet, "CFOP")



        Conection.Close()
        j = 0


        i = 0
        'sees if the partern matches the cube 
        ' if the pattern matches the it dose the pattern and to the cube before moving on as well as storing what id did
        Do Until i = MyDataSet.Tables(0).Rows.Count Or i = -1

                text = MyDataSet.Tables(0).Rows(i)(0).ToString()

            If patternmatch(texttoarray(text), cube.output) = True Then
                'showcube(cube)
                'MsgBox("aa")
                Correct = True
                sequence(j, 0) = MyDataSet.Tables(0).Rows(i)(2)
                sequence(j, 1) = MyDataSet.Tables(0).Rows(i)(1).ToString
                sequence(j, 2) = "0"
                If MyDataSet.Tables(0).Rows(i)(1).ToString <> "" Then
                    cube = move(cube, MyDataSet.Tables(0).Rows(i)(1))
                End If
                j = j + 1
                i = -2
            End If
            i = i + 1
            Loop

        'uses because  the cube needs rotating for some paters to match
        If Correct = False Then
            cube.U()


            i = 0
            'sees if the partern matches the cube 
            ' if the pattern matches the it dose the pattern and to the cube before moving on as well as storing what id did
            Do Until i = MyDataSet.Tables(0).Rows.Count Or i = -1

                text = MyDataSet.Tables(0).Rows(i)(0).ToString()

                If patternmatch(texttoarray(text), cube.output) = True Then
                    'showcube(cube)
                    'MsgBox("aa")
                    Correct = True
                    sequence(j, 0) = "Turn"
                    sequence(j, 1) = "U"
                    sequence(j, 2) = "0"
                    j = j + 1
                    sequence(j, 0) = MyDataSet.Tables(0).Rows(i)(2)
                    sequence(j, 1) = MyDataSet.Tables(0).Rows(i)(1).ToString
                    sequence(j, 2) = "0"
                    If MyDataSet.Tables(0).Rows(i)(1).ToString <> "" Then
                        cube = move(cube, MyDataSet.Tables(0).Rows(i)(1))
                    End If
                    j = j + 1
                    i = -2
                End If
                i = i + 1
            Loop
        End If

        If Correct = False Then
            i = 0
            cube.U()
            'sees if the partern matches the cube 
            ' if the pattern matches the it dose the pattern and to the cube before moving on as well as storing what id did
            Do Until i = MyDataSet.Tables(0).Rows.Count Or i = -1

                text = MyDataSet.Tables(0).Rows(i)(0).ToString()

                If patternmatch(texttoarray(text), cube.output) = True Then
                    'showcube(cube)
                    'MsgBox("aa")
                    Correct = True
                    sequence(j, 0) = "Turn"
                    sequence(j, 1) = "U2"
                    sequence(j, 2) = "0"
                    j = j + 1
                    sequence(j, 0) = MyDataSet.Tables(0).Rows(i)(2)
                    sequence(j, 1) = MyDataSet.Tables(0).Rows(i)(1).ToString
                    sequence(j, 2) = "0"
                    If MyDataSet.Tables(0).Rows(i)(1).ToString <> "" Then
                        cube = move(cube, MyDataSet.Tables(0).Rows(i)(1))
                    End If
                    j = j + 1
                    i = -2
                End If
                i = i + 1
            Loop
        End If

        If Correct = False Then
            i = 0
            cube.U()
            'sees if the partern matches the cube 
            ' if the pattern matches the it dose the pattern and to the cube before moving on as well as storing what id did
            Do Until i = MyDataSet.Tables(0).Rows.Count Or i = -1

                text = MyDataSet.Tables(0).Rows(i)(0).ToString()

                If patternmatch(texttoarray(text), cube.output) = True Then
                    'showcube(cube)
                    'MsgBox("aa")
                    Correct = True
                    sequence(j, 0) = "Turn"
                    sequence(j, 1) = "U'"
                    sequence(j, 2) = "0"
                    j = j + 1
                    sequence(j, 0) = MyDataSet.Tables(0).Rows(i)(2)
                    sequence(j, 1) = MyDataSet.Tables(0).Rows(i)(1).ToString
                    sequence(j, 2) = "0"
                    If MyDataSet.Tables(0).Rows(i)(1).ToString <> "" Then
                        cube = move(cube, MyDataSet.Tables(0).Rows(i)(1))
                    End If
                    j = j + 1
                    i = -2
                End If
                i = i + 1
            Loop
        End If
        sequence(j, 2) = "1"
        Return sequence
    End Function
    Function PLL(cube As cube)
        Dim MyDataSet As DataSet
        ' Dim endof As Integer
        Dim text As String
        Dim j As Integer
        Dim i As Integer
        Dim correct As Boolean = False
        Dim sequence(50, 2) As String
        opendb()
        Conection.Open()
        Adapter = New OleDbDataAdapter("SELECT Mask,AlgrithmSequence,AlgorithmName FROM CFOP WHERE Step = ""PLL""", Conection)  'SELECT Mask FROM CFOP WHERE Step = ""Cross"" SELECT * FROM CFOP
        MyDataSet = New DataSet

        Adapter.Fill(MyDataSet, "CFOP")

        Conection.Close()

        j = 0

        i = 0
        'sees if the partern matches the cube 
        ' if the pattern matches the it dose the pattern and to the cube before moving on as well as storing what id did
        Do Until i = MyDataSet.Tables(0).Rows.Count Or i = -1

            text = MyDataSet.Tables(0).Rows(i)(0).ToString()

            If patternmatch(texttoarray(text), cube.output) = True Then
                'showcube(cube)
                'MsgBox("aa")
                Correct = True
                sequence(j, 0) = MyDataSet.Tables(0).Rows(i)(2)
                sequence(j, 1) = MyDataSet.Tables(0).Rows(i)(1).ToString
                sequence(j, 2) = "0"
                If MyDataSet.Tables(0).Rows(i)(1).ToString <> "" Then
                    cube = move(cube, MyDataSet.Tables(0).Rows(i)(1))
                End If
                j = j + 1
                i = -2
            End If
            i = i + 1
        Loop


        'uses because  the cube needs rotating for some paters to match
        If Correct = False Then
            cube.U()


            i = 0
            'sees if the partern matches the cube 
            ' if the pattern matches the it dose the pattern and to the cube before moving on as well as storing what id did
            Do Until i = MyDataSet.Tables(0).Rows.Count Or i = -1

                text = MyDataSet.Tables(0).Rows(i)(0).ToString()

                If patternmatch(texttoarray(text), cube.output) = True Then
                    'showcube(cube)
                    'MsgBox("aa")
                    Correct = True
                    sequence(j, 0) = "Turn"
                    sequence(j, 1) = "U"
                    sequence(j, 2) = "0"
                    j = j + 1
                    sequence(j, 0) = MyDataSet.Tables(0).Rows(i)(2)
                    sequence(j, 1) = MyDataSet.Tables(0).Rows(i)(1).ToString
                    sequence(j, 2) = "0"
                    If MyDataSet.Tables(0).Rows(i)(1).ToString <> "" Then
                        cube = move(cube, MyDataSet.Tables(0).Rows(i)(1))
                    End If
                    j = j + 1
                    i = -2
                End If
                i = i + 1
            Loop
        End If

        If Correct = False Then
            i = 0
            cube.U()
            'sees if the partern matches the cube 
            ' if the pattern matches the it dose the pattern and to the cube before moving on as well as storing what id did
            Do Until i = MyDataSet.Tables(0).Rows.Count Or i = -1

                text = MyDataSet.Tables(0).Rows(i)(0).ToString()

                If patternmatch(texttoarray(text), cube.output) = True Then
                    'showcube(cube)
                    'MsgBox("aa")
                    Correct = True
                    sequence(j, 0) = "Turn"
                    sequence(j, 1) = "U2"
                    sequence(j, 2) = "0"
                    j = j + 1
                    sequence(j, 0) = MyDataSet.Tables(0).Rows(i)(2)
                    sequence(j, 1) = MyDataSet.Tables(0).Rows(i)(1).ToString
                    sequence(j, 2) = "0"
                    If MyDataSet.Tables(0).Rows(i)(1).ToString <> "" Then
                        cube = move(cube, MyDataSet.Tables(0).Rows(i)(1))
                    End If
                    j = j + 1
                    i = -2
                End If
                i = i + 1
            Loop
        End If

        If Correct = False Then
            i = 0
            cube.U()
            'sees if the partern matches the cube 
            ' if the pattern matches the it dose the pattern and to the cube before moving on as well as storing what id did
            Do Until i = MyDataSet.Tables(0).Rows.Count Or i = -1

                text = MyDataSet.Tables(0).Rows(i)(0).ToString()

                If patternmatch(texttoarray(text), cube.output) = True Then
                    'showcube(cube)
                    'MsgBox("aa")
                    Correct = True
                    sequence(j, 0) = "Turn"
                    sequence(j, 1) = "U'"
                    sequence(j, 2) = "0"
                    j = j + 1
                    sequence(j, 0) = MyDataSet.Tables(0).Rows(i)(2)
                    sequence(j, 1) = MyDataSet.Tables(0).Rows(i)(1).ToString
                    sequence(j, 2) = "0"
                    If MyDataSet.Tables(0).Rows(i)(1).ToString <> "" Then
                        cube = move(cube, MyDataSet.Tables(0).Rows(i)(1))
                    End If
                    j = j + 1
                    i = -2
                End If
                i = i + 1
            Loop
        End If

        'loop to put top layer into solved posision 
        showcube(cube)
        text = maskfill()
        '                            xxxxxx111xxxxxxxxx111xxxxxxxxx111xxx222555333666222555333666222555333666xxxxxx444xxxxxxxxx444xxxxxxxxx444xxx
        If patternmatch(texttoarray("xxxxxx111xxxxxxxxx111xxxxxxxxx111xxx222555333666222555333666222555333666xxxxxx444xxxxxxxxx444xxxxxxxxx444xxx"), cube.output) Then
            'solved is used and set because sometimes an unsolvable state can get through 
            'even with the checking as it can only noticed in the last step
            solved = True
        Else
            cube.U()
            showcube(cube)
            text = maskfill()
            If patternmatch(texttoarray("xxxxxx111xxxxxxxxx111xxxxxxxxx111xxx222555333666222555333666222555333666xxxxxx444xxxxxxxxx444xxxxxxxxx444xxx"), cube.output) Then
                solved = True
                sequence(j, 0) = "Turn"
                sequence(j, 1) = "U"
                sequence(j, 2) = "0"
                j = j + 1
            Else
                cube.U()
                showcube(cube)
                text = maskfill()
                If patternmatch(texttoarray("xxxxxx111xxxxxxxxx111xxxxxxxxx111xxx222555333666222555333666222555333666xxxxxx444xxxxxxxxx444xxxxxxxxx444xxx"), cube.output) Then
                    solved = True
                    sequence(j, 0) = "Turn"
                    sequence(j, 1) = "U2"
                    sequence(j, 2) = "0"
                    j = j + 1
                Else
                    cube.U()
                    showcube(cube)
                    text = maskfill()
                    If patternmatch(texttoarray("xxxxxx111xxxxxxxxx111xxxxxxxxx111xxx222555333666222555333666222555333666xxxxxx444xxxxxxxxx444xxxxxxxxx444xxx"), cube.output) Then
                        solved = True
                        sequence(j, 0) = "Turn"
                        sequence(j, 1) = "U'"
                        sequence(j, 2) = "0"
                        j = j + 1
                    End If
                End If
            End If

        End If

        sequence(j, 2) = "1"
        Return sequence
    End Function
    Function move(ByRef cube As cube, sequence As String)
        'dose move sequence to the given cube
        Dim seq(50) As String
        Dim temp As String
        Dim i As Integer
        'takes the stringg of moves and breaks them down and puts them in an array
        Do Until sequence = ""
            If Mid(sequence, 2, 1) = "," Then
                seq(i) = Mid(sequence, 1, 1)
                temp = sequence.Remove(0, 2)
                sequence = temp

            ElseIf Mid(sequence, 3, 1) = "," Then
                seq(i) = Mid(sequence, 1, 2)
                temp = sequence.Remove(0, 3)
                sequence = temp
            ElseIf Mid(sequence, 4, 1) = "," Then
                seq(i) = Mid(sequence, 1, 3)
                temp = sequence.Remove(0, 4)
                sequence = temp
            Else
                seq(i) = sequence
                sequence = ""
            End If
            i = i + 1
        Loop
        'gose all the moves on the cube in order
        i = 0
        Do Until seq(i) = "" Or i = 50
            Select Case seq(i)
                Case = ""
                    i = 50
                Case = "R"
                    cube.R()
                Case = "R'"
                    cube.Rprime()
                Case = "R2"
                    cube.R2()
                Case = "Rw"
                    cube.Rw()
                Case = "L"
                    cube.L()
                Case = "L'"
                    cube.Lprime()
                Case = "L2"
                    cube.L2()
                Case = "Lw"
                    cube.Lw()
                Case = "U"
                    cube.U()
                Case = "U'"
                    cube.Uprime()
                Case = "U2"
                    cube.U2()
                Case = "Uw"
                    cube.Uw()
                Case = "D"
                    cube.D()
                Case = "D'"
                    cube.Dprime()
                Case = "D2"
                    cube.D2()
                Case = "Dw"
                    cube.Dw()
                Case = "F"
                    cube.F()
                Case = "F'"
                    cube.Fprime()
                Case = "F2"
                    cube.F2()
                Case = "Fw"
                    cube.Fw()
                Case = "B"
                    cube.B()
                Case = "B'"
                    cube.Bprime()
                Case = "B2"
                    cube.B2()
                Case = "Bw"
                    cube.Bw()
                Case = "E"
                    cube.E()
                Case = "E'"
                    cube.EPrime()
                Case = "E2"
                    cube.E2()
                Case = "S"
                    cube.S()
                Case = "S'"
                    cube.SPrime()
                Case = "S2"
                    cube.S2()
                Case = "M"
                    cube.M()
                Case = "M'"
                    cube.Mprime()
                Case = "M2"
                    cube.M2()
                Case = "X"
                    cube.XRotation()
                Case = "X'"
                    cube.XPrime()
                Case = "Y"
                    cube.YRotation()
                Case = "Y'"
                    cube.YPrime()
                Case = "Z"
                    cube.ZRotation()
                Case = "Z'"
                    cube.ZPrime()
                Case = "Rw'"
                    cube.RwPrime()
                Case = "Lw'"
                    cube.LwPrime()
                Case = "Uw'"
                    cube.UwPrime()
                Case = "Dw'"
                    cube.DwPrime()
                Case = "Fw'"
                    cube.FwPrime()
                Case = "Bw'"
                    cube.BwPrime()
                Case = "Rw2"
                    cube.Rw2()
                Case = "Lw2"
                    cube.Lw2()
                Case = "Uw2"
                    cube.Uw2()
                Case = "Dw2"
                    cube.Dw2()
                Case = "Fw2"
                    cube.Fw2()
                Case = "Bw2"
                    cube.Bw2()
                Case = "X2"
                    cube.X2()
                Case = "Y2"
                    cube.Y2()
                Case = "Z2"
                    cube.Z2()

            End Select
            i = i + 1
        Loop
        Return cube
    End Function
    Private Sub showcube(ByVal cube As cube)
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

    Function maskfill()
        'converts the cube on screen into a string representasion
        Dim mask As String
        mask = ""

        For x = 0 To 8
            For y = 0 To 11

                If globals.buttonsnames(x, y) <> "" Then
                    mask = mask + takecolour(globals.buttonsnames(x, y) & "Button")
                Else
                    mask = mask + "x"
                End If

            Next
        Next
        Return mask
        '  MsgBox(mask)
    End Function

    Private Sub CloseButton_Click(sender As Object, e As EventArgs) Handles CloseButton.Click
        'used to close the form properly
        globals.Close()
        Timer.Close()
        Me.Close()
    End Sub

    'used for selecting the colour you want to change peices to 
    Private Sub RedButton_Click(sender As Object, e As EventArgs) Handles RedButton.Click
        selectedcolour = Colour.Red
    End Sub
    Private Sub BlueButton_Click(sender As Object, e As EventArgs) Handles BlueButton.Click
        selectedcolour = Colour.Blue
    End Sub
    Private Sub GreenButton_Click(sender As Object, e As EventArgs) Handles GreenButton.Click
        selectedcolour = Colour.Green
    End Sub
    Private Sub OrangeButton_Click(sender As Object, e As EventArgs) Handles OrangeButton.Click
        selectedcolour = Colour.Orange
    End Sub
    Private Sub YellowButton_Click(sender As Object, e As EventArgs) Handles YellowButton.Click
        selectedcolour = Colour.Yellow
    End Sub
    Private Sub WhiteButton_Click(sender As Object, e As EventArgs) Handles WhiteButton.Click
        selectedcolour = Colour.White
    End Sub
End Class
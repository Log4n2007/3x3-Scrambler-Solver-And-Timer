
Public Class cube

    Public _cube(2, 2, 2) As blankcube
    Public Sub New()
        blank()

    End Sub

    Property cube As blankcube(,,)
        'propert to get the underlying cube for deeper analasis
        Get
            Return _cube
        End Get
        Set(value(,,) As blankcube)
            '  _cube = value
        End Set

    End Property

    Public Sub blank()
        Dim xc As Char
        Dim yc As Char
        Dim zc As Char
        'sets each colour on each peice based on where it should be on a solved cube
        For x = 0 To 2
            For y = 0 To 2
                For z = 0 To 2

                    If x = 0 Then
                        xc = "O"
                    ElseIf x = 2 Then
                        xc = "R"
                    Else
                        xc = vbNullChar
                    End If

                    If y = 0 Then
                        yc = "W"
                    ElseIf y = 2 Then
                        yc = "Y"
                    Else
                        yc = vbNullChar
                    End If

                    If z = 0 Then
                        zc = "B"
                    ElseIf z = 2 Then
                        zc = "G"
                    Else
                        zc = vbNullChar
                    End If

                    If x = 1 Or y = 1 Or z = 1 Then


                        _cube(x, y, z) = New blankcube '(xc, yc, zc)
                        _cube(x, y, z).edge.colourx = xc
                        _cube(x, y, z).edge.coloury = yc
                        _cube(x, y, z).edge.colourz = zc
                    Else
                        _cube(x, y, z) = New blankcube '(xc, yc, zc)
                        _cube(x, y, z).corner.colourx = xc
                        _cube(x, y, z).corner.coloury = yc
                        _cube(x, y, z).corner.colourz = zc
                    End If
                    xc = Nothing
                    yc = Nothing
                    zc = Nothing
                Next
            Next
        Next


        'sets centers to correct colour
        _cube(1, 1, 0) = New blankcube '("B")
        _cube(1, 1, 0).center.colour = "B"
        _cube(1, 1, 2) = New blankcube '("G")
        _cube(1, 1, 2).center.colour = "G"
        _cube(1, 2, 1) = New blankcube '("Y")
        _cube(1, 2, 1).center.colour = "Y"
        _cube(1, 0, 1) = New blankcube '("W")
        _cube(1, 0, 1).center.colour = "W"
        _cube(0, 1, 1) = New blankcube '("O")
        _cube(0, 1, 1).center.colour = "O"
        _cube(2, 1, 1) = New blankcube '("R")
        _cube(2, 1, 1).center.colour = "R"

        setidentifiers()

    End Sub

    Public Sub U()
        'turns upper face clockwise
        Dim tempc As New corner
        Dim tempe As New edge
        tempc = _cube(2, 2, 2).corner
        _cube(0, 2, 2).corner.x()
        _cube(2, 2, 2).corner = _cube(0, 2, 2).corner
        _cube(0, 2, 0).corner.x()
        _cube(0, 2, 2).corner = _cube(0, 2, 0).corner
        _cube(2, 2, 0).corner.x()
        _cube(0, 2, 0).corner = _cube(2, 2, 0).corner
        tempc.x()
        _cube(2, 2, 0).corner = tempc
        tempe = _cube(1, 2, 2).edge
        _cube(0, 2, 1).edge.x()
        _cube(1, 2, 2).edge = _cube(0, 2, 1).edge
        _cube(1, 2, 0).edge.x()
        _cube(0, 2, 1).edge = _cube(1, 2, 0).edge
        _cube(2, 2, 1).edge.x()
        _cube(1, 2, 0).edge = _cube(2, 2, 1).edge
        tempe.x()
        _cube(2, 2, 1).edge = tempe

        setidentifiers()
    End Sub

    Public Sub U2()
        'turns upper face twice
        U()
        U()
    End Sub

    Public Sub Uprime()
        'turns upper face anti clockwise
        U()
        U()
        U()
    End Sub

    Public Sub Uw()
        'turns the upper 2 layers clockwise
        YRotation()
        D()
    End Sub
    Public Sub UwPrime()
        'turns the upper 2 layers anti-clockwise
        Uw()
        Uw()
        Uw()
    End Sub
    Public Sub Uw2()
        'turns the upper 2 layers twice
        Uw()
        Uw()
    End Sub
    Public Sub F()
        'turns front face clockwise
        Dim tempc As New corner
        Dim tempe As New edge

        tempc = _cube(2, 2, 0).corner
        _cube(0, 2, 0).corner.z()
        _cube(2, 2, 0).corner = _cube(0, 2, 0).corner
        _cube(0, 0, 0).corner.z()
        _cube(0, 2, 0).corner = _cube(0, 0, 0).corner
        _cube(2, 0, 0).corner.z()
        _cube(0, 0, 0).corner = _cube(2, 0, 0).corner
        tempc.z()
        _cube(2, 0, 0).corner = tempc

        tempe = _cube(2, 1, 0).edge
        _cube(1, 2, 0).edge.z()
        _cube(2, 1, 0).edge = _cube(1, 2, 0).edge
        _cube(0, 1, 0).edge.z()
        _cube(1, 2, 0).edge = _cube(0, 1, 0).edge
        _cube(1, 0, 0).edge.z()
        _cube(0, 1, 0).edge = _cube(1, 0, 0).edge
        tempe.z()
        _cube(1, 0, 0).edge = tempe

        setidentifiers()
    End Sub

    Public Sub F2()
        'turns front face twice
        F()
        F()

    End Sub

    Public Sub Fprime()
        'turns front face anti-clockwise
        F()
        F()
        F()

    End Sub

    Public Sub Fw()
        'turns the front 2 layers clockwise
        ZPrime()
        B()

    End Sub
    Public Sub FwPrime()
        'turns the front 2 layers anti-clockwise
        Fw()
        Fw()
        Fw()

    End Sub
    Public Sub Fw2()
        'turns the front 2 layers twice
        Fw()
        Fw()

    End Sub
    Public Sub L()
        'turns left face clockwise
        Dim tempc As New corner
        Dim tempe As New edge
        tempc = _cube(0, 2, 0).corner
        _cube(0, 2, 2).corner.y()
        _cube(0, 2, 0).corner = _cube(0, 2, 2).corner
        _cube(0, 0, 2).corner.y()
        _cube(0, 2, 2).corner = _cube(0, 0, 2).corner
        _cube(0, 0, 0).corner.y()
        _cube(0, 0, 2).corner = _cube(0, 0, 0).corner
        tempc.y()
        _cube(0, 0, 0).corner = tempc

        tempe = _cube(0, 2, 1).edge
        _cube(0, 1, 2).edge.y()
        _cube(0, 2, 1).edge = _cube(0, 1, 2).edge
        _cube(0, 0, 1).edge.y()
        _cube(0, 1, 2).edge = _cube(0, 0, 1).edge
        _cube(0, 1, 0).edge.y()
        _cube(0, 0, 1).edge = _cube(0, 1, 0).edge
        tempe.y()
        _cube(0, 1, 0).edge = tempe

        setidentifiers()
    End Sub

    Public Sub L2()
        'turns left face twice
        L()
        L()

    End Sub

    Public Sub Lprime()
        'turns left face anti-clockwise
        L()
        L()
        L()

    End Sub

    Public Sub Lw()
        'turns the left 2 layers clockwise
        XPrime()
        R()

    End Sub
    Public Sub LwPrime()
        'turns the left 2 layers anti-clockwise
        Lw()
        Lw()
        Lw()

    End Sub
    Public Sub Lw2()
        'turns the left 2 layers twice
        Lw()
        Lw()

    End Sub
    Public Sub R()
        'turns right face clockwise
        Rprime()
        Rprime()
        Rprime()
    End Sub

    Public Sub R2()
        'turns right face twice
        Rprime()
        Rprime()

    End Sub

    Public Sub Rprime()
        'turns right face anti-clockwise
        Dim tempc As New corner
        Dim tempe As New edge
        tempc = _cube(2, 2, 0).corner
        _cube(2, 2, 2).corner.y()
        _cube(2, 2, 0).corner = _cube(2, 2, 2).corner
        _cube(2, 0, 2).corner.y()
        _cube(2, 2, 2).corner = _cube(2, 0, 2).corner
        _cube(2, 0, 0).corner.y()
        _cube(2, 0, 2).corner = _cube(2, 0, 0).corner
        tempc.y()
        _cube(2, 0, 0).corner = tempc

        tempe = _cube(2, 2, 1).edge
        _cube(2, 1, 2).edge.y()
        _cube(2, 2, 1).edge = _cube(2, 1, 2).edge
        _cube(2, 0, 1).edge.y()
        _cube(2, 1, 2).edge = _cube(2, 0, 1).edge
        _cube(2, 1, 0).edge.y()
        _cube(2, 0, 1).edge = _cube(2, 1, 0).edge
        tempe.y()
        _cube(2, 1, 0).edge = tempe

        setidentifiers()
    End Sub

    Public Sub Rw()
        'turns the right 2 layers clockwise
        XRotation()
        L()

    End Sub
    Public Sub RwPrime()
        'turns the right 2 layers anti-clockwise
        Rw()
        Rw()
        Rw()

    End Sub
    Public Sub Rw2()
        'turns the right 2 layers twice
        Rw()
        Rw()

    End Sub
    Public Sub B()
        'turns back face clockwise
        Bprime()
        Bprime()
        Bprime()
    End Sub

    Public Sub B2()
        'turns back face twice
        Bprime()
        Bprime()
    End Sub

    Public Sub Bprime()
        'turns back face anti-clockwise
        Dim tempc As New corner
        Dim tempe As New edge

        tempc = _cube(2, 2, 2).corner
        _cube(0, 2, 2).corner.z()
        _cube(2, 2, 2).corner = _cube(0, 2, 2).corner
        _cube(0, 0, 2).corner.z()
        _cube(0, 2, 2).corner = _cube(0, 0, 2).corner
        _cube(2, 0, 2).corner.z()
        _cube(0, 0, 2).corner = _cube(2, 0, 2).corner
        tempc.z()
        _cube(2, 0, 2).corner = tempc

        tempe = _cube(2, 1, 2).edge
        _cube(1, 2, 2).edge.z()
        _cube(2, 1, 2).edge = _cube(1, 2, 2).edge
        _cube(0, 1, 2).edge.z()
        _cube(1, 2, 2).edge = _cube(0, 1, 2).edge
        _cube(1, 0, 2).edge.z()
        _cube(0, 1, 2).edge = _cube(1, 0, 2).edge
        tempe.z()
        _cube(1, 0, 2).edge = tempe

        setidentifiers()
    End Sub

    Public Sub Bw()
        'turns the back 2 layers clockwise
        ZPrime()
        F()

    End Sub
    Public Sub BwPrime()
        'turns the back 2 layers anti-clockwise
        Bw()
        Bw()
        Bw()

    End Sub
    Public Sub Bw2()
        'turns the back 2 layers twice
        Bw()
        Bw()

    End Sub
    Public Sub D()
        'turns bottom face clockwise
        Dprime()
        Dprime()
        Dprime()
    End Sub

    Public Sub D2()
        'turns bottom face twice
        Dprime()
        Dprime()
    End Sub

    Public Sub Dprime()
        'turns bottom face anti-clockwise

        Dim tempc As New corner
        Dim tempe As New edge
        tempc = _cube(2, 0, 2).corner
        _cube(0, 0, 2).corner.x()
        _cube(2, 0, 2).corner = _cube(0, 0, 2).corner
        _cube(0, 0, 0).corner.x()
        _cube(0, 0, 2).corner = _cube(0, 0, 0).corner
        _cube(2, 0, 0).corner.x()
        _cube(0, 0, 0).corner = _cube(2, 0, 0).corner
        tempc.x()
        _cube(2, 0, 0).corner = tempc
        tempe = _cube(1, 0, 2).edge
        _cube(0, 0, 1).edge.x()
        _cube(1, 0, 2).edge = _cube(0, 0, 1).edge
        _cube(1, 0, 0).edge.x()
        _cube(0, 0, 1).edge = _cube(1, 0, 0).edge
        _cube(2, 0, 1).edge.x()
        _cube(1, 0, 0).edge = _cube(2, 0, 1).edge
        tempe.x()
        _cube(2, 0, 1).edge = tempe

        setidentifiers()
    End Sub


    Public Sub Dw()
        'turns the bottom 2 layers clockwise
        YPrime()
        U()

    End Sub
    Public Sub DwPrime()
        'turns the bottom 2 layers anti-clockwise
        Dw()
        Dw()
        Dw()

    End Sub
    Public Sub Dw2()
        'turns the bottom 2 layers twice
        Dw()
        Dw()
    End Sub

    Public Sub E()
        'turns the centeral layer paralel to D face clockwise
        EPrime()
        EPrime()
        EPrime()

    End Sub

    Public Sub E2()
        'turns the centeral layer paralel to D face twice
        EPrime()
        EPrime()

    End Sub

    Public Sub EPrime()
        'turns the centeral layer paralel to D face anti-clockwise
        Dim tempc As New center
        Dim tempe As New edge
        tempe = _cube(2, 1, 2).edge
        _cube(0, 1, 2).edge.x()
        _cube(2, 1, 2).edge = _cube(0, 1, 2).edge
        _cube(0, 1, 0).edge.x()
        _cube(0, 1, 2).edge = _cube(0, 1, 0).edge
        _cube(2, 1, 0).edge.x()
        _cube(0, 1, 0).edge = _cube(2, 1, 0).edge
        tempe.x()
        _cube(2, 1, 0).edge = tempe

        tempc = _cube(1, 1, 2).center
        _cube(1, 1, 2).center = _cube(0, 1, 1).center
        _cube(0, 1, 1).center = _cube(1, 1, 0).center
        _cube(1, 1, 0).center = _cube(2, 1, 1).center
        _cube(2, 1, 1).center = tempc

        setidentifiers()
    End Sub

    Public Sub S()
        'turns the centeral layer paralel to F face clockwise
        Dim tempc As New center
        Dim tempe As New edge

        tempe = _cube(2, 2, 1).edge
        _cube(0, 2, 1).edge.z()
        _cube(2, 2, 1).edge = _cube(0, 2, 1).edge
        _cube(0, 0, 1).edge.z()
        _cube(0, 2, 1).edge = _cube(0, 0, 1).edge
        _cube(2, 0, 1).edge.z()
        _cube(0, 0, 1).edge = _cube(2, 0, 1).edge
        tempe.z()
        _cube(2, 0, 1).edge = tempe

        tempc = _cube(2, 1, 1).center
        _cube(2, 1, 1).center = _cube(1, 2, 1).center
        _cube(1, 2, 1).center = _cube(0, 1, 1).center
        _cube(0, 1, 1).center = _cube(1, 0, 1).center
        _cube(1, 0, 1).center = tempc

        setidentifiers()
    End Sub

    Public Sub S2()
        'turns the centeral layer paralel to F face twice
        S()
        S()

    End Sub

    Public Sub SPrime()
        'turns the centeral layer paralel to F face anti-clockwise
        S()
        S()
        S()

    End Sub

    Public Sub M()
        'turns the centeral layer paralel to L face clockwise
        Dim tempe As New edge
        Dim tempc As New center


        tempe = _cube(1, 2, 0).edge
        _cube(1, 2, 2).edge.y()
        _cube(1, 2, 0).edge = _cube(1, 2, 2).edge
        _cube(1, 0, 2).edge.y()
        _cube(1, 2, 2).edge = _cube(1, 0, 2).edge
        _cube(1, 0, 0).edge.y()
        _cube(1, 0, 2).edge = _cube(1, 0, 0).edge
        tempe.y()
        _cube(1, 0, 0).edge = tempe

        tempc = _cube(1, 2, 1).center
        _cube(1, 2, 1).center = _cube(1, 1, 2).center
        _cube(1, 1, 2).center = _cube(1, 0, 1).center
        _cube(1, 0, 1).center = _cube(1, 1, 0).center
        _cube(1, 1, 0).center = tempc

        setidentifiers()

    End Sub

    Public Sub M2()
        'turns the centeral layer paralel to L face twice
        M()
        M()

    End Sub

    Public Sub Mprime()
        'turns the centeral layer paralel to L face anti-clockwise
        M()
        M()
        M()

    End Sub

    Public Sub XRotation()
        'turs the entire cube from top to bottom forward
        R()
        Mprime()
        Lprime()

    End Sub

    Public Sub XPrime()
        'turs the entire cube from bottom to top forward
        Rprime()
        M()
        L()

    End Sub
    Public Sub X2()
        'turs the entire cube from bottom to top forward twice
        R2()
        M2()
        L2()
    End Sub

    Public Sub YRotation()
        'turs the entire cube from right to left round the front
        U()
        EPrime()
        Dprime()
    End Sub

    Public Sub YPrime()
        'turs the entire cube from left to right found the front
        Uprime()
        E()
        D()

    End Sub
    Public Sub Y2()
        'turs the entire cube from left to right found the front twice
        U2()
        E2()
        D2()

    End Sub
    Public Sub ZRotation()
        'turs the entire cube from left to right over the top
        F()
        S()
        Bprime()

    End Sub

    Public Sub ZPrime()
        'turs the entire cube from right to left over the top
        Fprime()
        SPrime()
        B()

    End Sub
    Public Sub Z2()
        'turs the entire cube from right to left over the top twice
        F2()
        S2()
        B2()

    End Sub

    Public Function output()
        'outputs all faces of the cube as a 2d array
        Dim faces(8, 11) As Char
        ' in order Y,W,O,R,B,G
        For x = 0 To 2
            For y = 0 To 2
                If x = 1 And y = 1 Then
                    faces(3 + x, 9 + y) = _cube(0 + x, 2, 0 + y).center.colour
                    faces(3 + x, 3 + y) = _cube(0 + x, 0, 2 - y).center.colour
                    faces(0 + x, 6 + y) = _cube(0, 0 + y, 2 - x).center.colour
                    faces(6 + x, 6 + y) = _cube(2, 0 + y, 0 + x).center.colour
                    faces(3 + x, 6 + y) = _cube(0 + x, 0 + y, 0).center.colour
                    faces(3 + x, 0 + y) = _cube(0 + x, 2 - y, 2).center.colour
                ElseIf x = 1 Xor y = 1 Then
                    faces(3 + x, 9 + y) = _cube(0 + x, 2, 0 + y).edge.coloury
                    faces(3 + x, 3 + y) = _cube(0 + x, 0, 2 - y).edge.coloury
                    faces(0 + x, 6 + y) = _cube(0, 0 + y, 2 - x).edge.colourx
                    faces(6 + x, 6 + y) = _cube(2, 0 + y, 0 + x).edge.colourx
                    faces(3 + x, 6 + y) = _cube(0 + x, 0 + y, 0).edge.colourz
                    faces(3 + x, 0 + y) = _cube(0 + x, 2 - y, 2).edge.colourz
                ElseIf x <> 1 And y <> 1 Then
                    faces(3 + x, 9 + y) = _cube(0 + x, 2, 0 + y).corner.coloury
                    faces(3 + x, 3 + y) = _cube(0 + x, 0, 2 - y).corner.coloury
                    faces(0 + x, 6 + y) = _cube(0, 0 + y, 2 - x).corner.colourx
                    faces(6 + x, 6 + y) = _cube(2, 0 + y, 0 + x).corner.colourx
                    faces(3 + x, 6 + y) = _cube(0 + x, 0 + y, 0).corner.colourz
                    faces(3 + x, 0 + y) = _cube(0 + x, 2 - y, 2).corner.colourz
                End If
            Next
        Next

        Return faces
    End Function
    Public Sub inputstate(ByVal faces(,) As Char)
        'takes an input of the cubes state as  a 2d array
        ' in order Y,W,O,R,B,G

        For x = 0 To 2
            For y = 0 To 2
                If x = 1 And y = 1 Then
                    _cube(0 + x, 2, 0 + y).center.colour = (faces(3 + x, 9 + y))
                    _cube(0 + x, 0, 2 - y).center.colour = (faces(3 + x, 3 + y))
                    _cube(0, 0 + y, 2 - x).center.colour = (faces(0 + x, 6 + y))
                    _cube(2, 0 + y, 0 + x).center.colour = (faces(6 + x, 6 + y))
                    _cube(0 + x, 0 + y, 0).center.colour = (faces(3 + x, 6 + y))
                    _cube(0 + x, 2 - y, 2).center.colour = (faces(3 + x, 0 + y))
                ElseIf x = 1 Xor y = 1 Then
                    _cube(0 + x, 2, 0 + y).edge.coloury = (faces(3 + x, 9 + y))
                    _cube(0 + x, 0, 2 - y).edge.coloury = (faces(3 + x, 3 + y))
                    _cube(0, 0 + y, 2 - x).edge.colourx = (faces(0 + x, 6 + y))
                    _cube(2, 0 + y, 0 + x).edge.colourx = (faces(6 + x, 6 + y))
                    _cube(0 + x, 0 + y, 0).edge.colourz = (faces(3 + x, 6 + y))
                    _cube(0 + x, 2 - y, 2).edge.colourz = (faces(3 + x, 0 + y))
                ElseIf x <> 1 And y <> 1 Then
                    _cube(0 + x, 2, 0 + y).corner.coloury = (faces(3 + x, 9 + y))
                    _cube(0 + x, 0, 2 - y).corner.coloury = (faces(3 + x, 3 + y))
                    _cube(0, 0 + y, 2 - x).corner.colourx = (faces(0 + x, 6 + y))
                    _cube(2, 0 + y, 0 + x).corner.colourx = (faces(6 + x, 6 + y))
                    _cube(0 + x, 0 + y, 0).corner.colourz = (faces(3 + x, 6 + y))
                    _cube(0 + x, 2 - y, 2).corner.colourz = (faces(3 + x, 0 + y))

                End If
            Next
        Next

        setidentifiers()
    End Sub

    Public Function conflictcheck()
        'cheacks for conflicts on each peice
        Dim solvable As Boolean = True
        Dim counter As Integer
        Dim colours As New Dictionary(Of Char, Integer)
        colours.Add("W", 0)
        colours.Add("Y", 0)
        colours.Add("R", 1)
        colours.Add("O", 1)
        colours.Add("B", 2)
        colours.Add("G", 2)

        For x = 0 To 2
            For y = 0 To 2
                For z = 0 To 2
                    counter = vbNull

                    'gose through all corners
                    If (x = 0 Or x = 2) And (y = 0 Or y = 2) And (z = 0 Or z = 2) Then
                        'checks that each face of the corners is a difrent colour

                        If (colours(_cube(x, y, z).corner.colourx) + colours(_cube(x, y, z).corner.coloury) + colours(_cube(x, y, z).corner.colourz)) <> 3 Then
                            solvable = False
                        ElseIf (colours(_cube(x, y, z).corner.colourx) = colours(_cube(x, y, z).corner.coloury) And colours(_cube(x, y, z).corner.coloury) = colours(_cube(x, y, z).corner.colourz)) Then
                            solvable = False
                        End If

                    End If

                    'checks that each face of the edge is a difrent colour
                    If (x = 1) And (z <> 1) And y <> 1 Then

                        If colours(_cube(x, y, z).edge.colourz) = colours(_cube(x, y, z).edge.coloury) Then

                            solvable = False

                        End If

                    End If
                    'checks that each face of the edge is a difrent colour
                    If (y = 1) And (z <> 1) And x <> 1 Then

                        If colours(_cube(x, y, z).edge.colourz) = colours(_cube(x, y, z).edge.colourx) Then

                            solvable = False

                        End If

                        'checks that each face of the edge is a difrent colour
                        If (z = z) And (x <> 1) And y <> 1 Then

                            If colours(_cube(x, y, z).edge.colourx) = colours(_cube(x, y, z).edge.coloury) Then

                                solvable = False

                            End If

                        End If


                    End If

                Next
            Next
        Next


        Return solvable
    End Function
    Private Sub setidentifiers()
        Dim xc As Char
        Dim zc As Char
        Dim yc As Char
        Dim xzy As String
        'sets all identifiers x,z,y
        For x = 0 To 2
            For y = 0 To 2
                For z = 0 To 2
                    xc = ""
                    yc = ""
                    zc = ""
                    xzy = ""
                    If x = 1 Xor y = 1 Xor z = 1 Then
                        'generates the identifier for each peice in the form x,c,y from where the peice would orignaly be
                        If _cube(x, y, z).edge.colourx = "O" Or _cube(x, y, z).edge.colourx = "R" Then
                            xc = _cube(x, y, z).edge.colourx
                        ElseIf _cube(x, y, z).edge.colourx = "Y" Or _cube(x, y, z).edge.colourx = "W" Then
                            yc = _cube(x, y, z).edge.colourx
                        ElseIf _cube(x, y, z).edge.colourx = "B" Or _cube(x, y, z).edge.colourx = "G" Then
                            zc = _cube(x, y, z).edge.colourx
                        End If
                        If _cube(x, y, z).edge.coloury = "O" Or _cube(x, y, z).edge.coloury = "R" Then
                            xc = _cube(x, y, z).edge.coloury
                        ElseIf _cube(x, y, z).edge.coloury = "Y" Or _cube(x, y, z).edge.coloury = "W" Then
                            yc = _cube(x, y, z).edge.coloury
                        ElseIf _cube(x, y, z).edge.coloury = "B" Or _cube(x, y, z).edge.coloury = "G" Then
                            zc = _cube(x, y, z).edge.coloury
                        End If
                        If _cube(x, y, z).edge.colourz = "O" Or _cube(x, y, z).edge.colourz = "R" Then
                            xc = _cube(x, y, z).edge.colourz
                        ElseIf _cube(x, y, z).edge.colourz = "Y" Or _cube(x, y, z).edge.colourz = "W" Then
                            yc = _cube(x, y, z).edge.colourz
                        ElseIf _cube(x, y, z).edge.colourz = "B" Or _cube(x, y, z).edge.colourz = "G" Then
                            zc = _cube(x, y, z).edge.colourz
                        End If
                        'removes null charcter
                        If xc <> Nothing Then
                            xzy = xc
                        End If
                        If zc <> Nothing Then
                            xzy = xzy & zc
                        End If
                        If yc <> Nothing Then
                            xzy = xzy & yc
                        End If
                        _cube(x, y, z).edge.identifier = xzy
                    ElseIf x <> 1 And y <> 1 And z <> 1 Then
                        'generates the identifier for each peice in the form x,c,y from where the peice would orignaly be
                        If _cube(x, y, z).corner.colourx = "O" Or _cube(x, y, z).corner.colourx = "R" Then
                            xc = _cube(x, y, z).corner.colourx
                        ElseIf _cube(x, y, z).corner.colourx = "Y" Or _cube(x, y, z).corner.colourx = "W" Then
                            yc = _cube(x, y, z).corner.colourx
                        ElseIf _cube(x, y, z).corner.colourx = "B" Or _cube(x, y, z).corner.colourx = "G" Then
                            zc = _cube(x, y, z).corner.colourx
                        End If
                        If _cube(x, y, z).corner.coloury = "O" Or _cube(x, y, z).corner.coloury = "R" Then
                            xc = _cube(x, y, z).corner.coloury
                        ElseIf _cube(x, y, z).corner.coloury = "Y" Or _cube(x, y, z).corner.coloury = "W" Then
                            yc = _cube(x, y, z).corner.coloury
                        ElseIf _cube(x, y, z).corner.coloury = "B" Or _cube(x, y, z).corner.coloury = "G" Then
                            zc = _cube(x, y, z).corner.coloury
                        End If
                        If _cube(x, y, z).corner.colourz = "O" Or _cube(x, y, z).corner.colourz = "R" Then
                            xc = _cube(x, y, z).corner.colourz
                        ElseIf _cube(x, y, z).corner.colourz = "Y" Or _cube(x, y, z).corner.colourz = "W" Then
                            yc = _cube(x, y, z).corner.colourz
                        ElseIf _cube(x, y, z).corner.colourz = "B" Or _cube(x, y, z).corner.colourz = "G" Then
                            zc = _cube(x, y, z).corner.colourz
                        End If
                        _cube(x, y, z).corner.identifier = CStr(xc & zc & yc)
                    End If
                Next
            Next
        Next
        'sets centre identifiers
        _cube(1, 1, 0).center.identifier = _cube(1, 1, 0).center.colour
        _cube(1, 1, 2).center.identifier = _cube(1, 1, 2).center.colour
        _cube(0, 1, 1).center.identifier = _cube(0, 1, 1).center.colour
        _cube(2, 1, 1).center.identifier = _cube(2, 1, 1).center.colour
        _cube(1, 0, 1).center.identifier = _cube(1, 0, 1).center.colour
        _cube(1, 2, 1).center.identifier = _cube(1, 2, 1).center.colour
    End Sub

    Function colourcheck()
        'checks that there only 9 of each colour
        Dim state(,) As Char = output()
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
End Class
Public Class blankcube
    'agrigates all the objects that make up the cube
    Public center As New center
    Public edge As New edge
    Public corner As New corner

End Class
Public Class center
    ' an object to do the center peice witch has only one colour per peice
    Protected _colour As Char
    Protected _identifier As String
    'unique identifier for each peice
    Public Property identifier As String
        Get
            Return _identifier
        End Get
        Set(value As String)
            _identifier = value
        End Set
    End Property


    Public Property colour As Char
        Set(value As Char)
            _colour = value
        End Set
        Get
            Return _colour
        End Get

    End Property
End Class
Public Class edge
    ' an abject to do the edge peice witch has only two colour per peice
    Protected _colourx As Char
    Protected _coloury As Char
    Protected _colourz As Char
    Private _identifier As String
    'unique identifier for each peice
    Public Property identifier As String
        Get
            Return _identifier
        End Get
        Set(value As String)
            _identifier = value
        End Set
    End Property

    'inputs the colour for the axis it is on
    'and cheacks that it has only two colours on it
    Public Property colourx As Char
        Set(value As Char)

            Dim count As Integer = 0
            If _coloury <> Nothing Then
                count = count + 1
            End If
            If _colourz <> Nothing Then
                count = count + 1
            End If
            If count = 2 Then

            Else
                _colourx = value
            End If
        End Set
        Get
            Return _colourx
        End Get
    End Property
    Public Property coloury As Char
        Set(value As Char)

            Dim count As Integer = 0
            If _colourx <> Nothing Then
                count = count + 1
            End If
            If _colourz <> Nothing Then
                count = count + 1
            End If
            If count = 2 Then

            Else
                _coloury = value
            End If
            '  End If
        End Set
        Get
            Return _coloury
        End Get

    End Property
    Public Property colourz As Char
        Set(value As Char)

            Dim count As Integer = 0
            If _coloury <> Nothing Then
                count = count + 1
            End If
            If _colourx <> Nothing Then
                count = count + 1
            End If
            If count = 2 Then

            Else
                _colourz = value
            End If
            '  End If
        End Set
        Get
            Return _colourz
        End Get

    End Property

    'swaps the colours as they should be when the peice moves when the face turns
    Public Sub x()
        Dim temp As Char
        temp = _colourx
        _colourx = _colourz
        _colourz = temp
    End Sub
    Public Sub y()
        Dim temp As Char
        temp = _colourz
        _colourz = _coloury
        _coloury = temp
    End Sub
    Public Sub z()
        Dim temp As Char
        temp = _colourx
        _colourx = _coloury
        _coloury = temp
    End Sub
End Class
Public Class corner
    ' an abject to do the corner peice witch has only three colour per peice
    Inherits edge

    'inputs the colour for the axis it is on
    Public Overloads Property colourz As Char
        Set(value As Char)
            _colourz = value
        End Set
        Get
            Return _colourz
        End Get

    End Property
    Public Overloads Property coloury As Char
        Set(value As Char)
            _coloury = value
        End Set
        Get
            Return _coloury
        End Get

    End Property
    Public Overloads Property colourx As Char
        Set(value As Char)
            _colourx = value
        End Set
        Get
            Return _colourx
        End Get

    End Property
End Class
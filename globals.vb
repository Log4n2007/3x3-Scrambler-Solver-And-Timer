Imports System.Data.OleDb

Public Class globals
    'holds the global varbles
    Public buttonsnames(8, 11) As String
    Public statemove As String
    Private Sub globals_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        statemove = ""
        'global for the name and possision of each button
        buttonsnames(3, 11) = "ULY" '(3,11) = "ULY"
        buttonsnames(4, 11) = "UCY"
        buttonsnames(5, 11) = "URY"
        buttonsnames(3, 10) = "CLY"
        buttonsnames(4, 10) = "CCY"
        buttonsnames(5, 10) = "CRY"
        buttonsnames(3, 9) = "BLY"
        buttonsnames(4, 9) = "BCY"
        buttonsnames(5, 9) = "BRY"

        buttonsnames(0, 8) = "ULO"
        buttonsnames(1, 8) = "UCO"
        buttonsnames(2, 8) = "URO"
        buttonsnames(0, 7) = "CLO"
        buttonsnames(1, 7) = "CCO"
        buttonsnames(2, 7) = "CRO"
        buttonsnames(0, 6) = "BLO"
        buttonsnames(1, 6) = "BCO"
        buttonsnames(2, 6) = "BRO"

        buttonsnames(3, 8) = "ULB"
        buttonsnames(4, 8) = "UCB"
        buttonsnames(5, 8) = "URB"
        buttonsnames(3, 7) = "CLB"
        buttonsnames(4, 7) = "CCB"
        buttonsnames(5, 7) = "CRB"
        buttonsnames(3, 6) = "BLB"
        buttonsnames(4, 6) = "BCB"
        buttonsnames(5, 6) = "BRB"

        buttonsnames(6, 8) = "ULR"
        buttonsnames(7, 8) = "UCR"
        buttonsnames(8, 8) = "URR"
        buttonsnames(6, 7) = "CLR"
        buttonsnames(7, 7) = "CCR"
        buttonsnames(8, 7) = "CRR"
        buttonsnames(6, 6) = "BLR"
        buttonsnames(7, 6) = "BCR"
        buttonsnames(8, 6) = "BRR"

        buttonsnames(3, 5) = "ULW"
        buttonsnames(4, 5) = "UCW"
        buttonsnames(5, 5) = "URW"
        buttonsnames(3, 4) = "CLW"
        buttonsnames(4, 4) = "CCW"
        buttonsnames(5, 4) = "CRW"
        buttonsnames(3, 3) = "BLW"
        buttonsnames(4, 3) = "BCW"
        buttonsnames(5, 3) = "BRW"

        buttonsnames(3, 2) = "ULG"
        buttonsnames(4, 2) = "UCG"
        buttonsnames(5, 2) = "URG"
        buttonsnames(3, 1) = "CLG"
        buttonsnames(4, 1) = "CCG"
        buttonsnames(5, 1) = "CRG"
        buttonsnames(3, 0) = "BLG"
        buttonsnames(4, 0) = "BCG"
        buttonsnames(5, 0) = "BRG"

    End Sub
End Class
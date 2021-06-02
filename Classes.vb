Imports System.IO

Public Enum StateFM
    Intro
    Stand
    Roar
    JumpStart
    MidAir
    FallDown
    JumpEnd
    FireShot
    OilGlob
    GetHit
    Dead
End Enum

Public Enum StateFire
    Create
    Shoot
    Ground
End Enum

Public Enum StateOil
    Create
    Shoot
    Ground
    Ground1
    OilFire
End Enum

Public Enum StateMegaman
    Stand
    JumpUp1
    JumpUp2
    JumpUp3
    JumpDown1
    JumpDown2
    Run
    GetHit
End Enum

Public Enum StateConveyor
    Create
    MoveLeft
    MoveRight
End Enum

Public Enum FaceDir
    Left
    Right
End Enum

Public Class CImage
    Public Width As Integer
    Public Height As Integer
    Public Elmt(,) As System.Drawing.Color 'store the pixel of coordinate
    Public ColorMode As Integer 'not used

    Sub OpenImage(ByVal FName As String)
        Dim s As String
        Dim L As Long
        Dim BR As BinaryReader
        Dim h, w, pos As Integer
        Dim r, g, b As Integer
        Dim pad As Integer

        BR = New BinaryReader(File.Open(FName, FileMode.Open))

        Try
            BlockRead(BR, 2, s)

            If s <> "BM" Then
                MsgBox("Not a BMP file")
            Else 'BMP file
                BlockReadInt(BR, 4, L) 'size
                'MsgBox("Size = " + CStr(L))
                BlankRead(BR, 4) 'reserved
                BlockReadInt(BR, 4, pos) 'start of data
                BlankRead(BR, 4) 'size of header
                BlockReadInt(BR, 4, Width) 'width
                'MsgBox("Width = " + CStr(I.Width))
                BlockReadInt(BR, 4, Height) 'height
                'MsgBox("Height = " + CStr(I.Height))
                BlankRead(BR, 2) 'color panels
                BlockReadInt(BR, 2, ColorMode) 'colormode
                If ColorMode <> 24 Then
                    MsgBox("Not a 24-bit color BMP")
                Else

                    BlankRead(BR, pos - 30)

                    ReDim Elmt(Width - 1, Height - 1)
                    pad = (4 - (Width * 3 Mod 4)) Mod 4

                    For h = Height - 1 To 0 Step -1
                        For w = 0 To Width - 1
                            BlockReadInt(BR, 1, b)
                            BlockReadInt(BR, 1, g)
                            BlockReadInt(BR, 1, r)
                            Elmt(w, h) = Color.FromArgb(r, g, b)

                        Next
                        BlankRead(BR, pad)

                    Next

                End If

            End If

        Catch ex As Exception
            MsgBox("Error")

        End Try

        BR.Close()


    End Sub


    Sub CreateMask(ByRef Mask As CImage)
        Dim i, j As Integer

        Mask = New CImage
        Mask.Width = Width
        Mask.Height = Height

        'size of the mask
        '-1, because start from 0
        ReDim Mask.Elmt(Mask.Width - 1, Mask.Height - 1)

        For i = 0 To Width - 1
            For j = 0 To Height - 1
                'if elmt(i,j) is black 
                If Elmt(i, j).R = 0 And Elmt(i, j).G = 0 And Elmt(i, j).B = 0 Then
                    Mask.Elmt(i, j) = Color.FromArgb(255, 255, 255) 'white
                Else
                    Mask.Elmt(i, j) = Color.FromArgb(0, 0, 0) 'black
                End If
            Next
        Next

    End Sub

    Sub CopyImg(ByRef Img As CImage)
        'copies image to Img
        Img = New CImage
        Img.Width = Width
        Img.Height = Height
        ReDim Img.Elmt(Width - 1, Height - 1)

        For i = 0 To Width - 1
            For j = 0 To Height - 1
                Img.Elmt(i, j) = Elmt(i, j)
            Next
        Next

    End Sub

End Class

Public Class CCharacter
    Public PosX, PosY As Double
    Public Vx, Vy As Double
    Public CurrState As StateFM
    Public FrameIdx As Integer
    Public CurrFrame As Integer
    Public ArrSprites() As CArrFrame
    Public IdxArrSprites As Integer
    Public FDir As FaceDir
    Public destroy As Boolean = False
    Public lock As Boolean = False
    Public lock1 As Boolean = False
    Public left, right, top, bottom As Integer

    'Public Const gravity = 1

    Public Sub GetNextFrame()
        CurrFrame = CurrFrame + 1
        If CurrFrame = ArrSprites(IdxArrSprites).Elmt(FrameIdx).MaxFrameTime Then
            FrameIdx = FrameIdx + 1
            If FrameIdx = ArrSprites(IdxArrSprites).N Then
                FrameIdx = 0
            End If
            CurrFrame = 0

        End If

    End Sub

    Public Overridable Sub Update()

    End Sub
End Class

Public Class CCharFM
    Inherits CCharacter

    Public CurrState As StateFM

    Public Sub State(state As StateFM, idxspr As Integer)
        CurrState = state
        IdxArrSprites = idxspr
        CurrFrame = 0
        FrameIdx = 0

    End Sub

    Public Overrides Sub Update()

        Select Case CurrState
            Case StateFM.Intro
                'lock = True
                GetNextFrame()
                If FrameIdx = 0 And CurrFrame = 0 Then
                    State(StateFM.JumpStart, 2)
                End If

            Case StateFM.Stand
                'lock = False
                GetNextFrame()

            Case StateFM.JumpStart
                'lock = True
                GetNextFrame()
                If FrameIdx = 0 And CurrFrame = 0 And FDir = FaceDir.Right Then
                    State(StateFM.MidAir, 3)
                    Vx = 3
                    Vy = -6
                ElseIf FrameIdx = 0 And CurrFrame = 0 And FDir = FaceDir.Left Then
                    State(StateFM.MidAir, 3)
                    Vx = -3
                    Vy = -6
                End If

            Case StateFM.MidAir
                PosX = PosX + Vx
                PosY = PosY + Vy
                Vy = Vy + 0.2
                GetNextFrame()
                If PosX > 540 Then
                    PosX = 540
                    Vx = 0
                    Vy = 0
                    State(StateFM.FallDown, 4)
                ElseIf PosX < 50 Then
                    PosX = 50
                    Vy = 0
                    Vx = 0
                    State(StateFM.FallDown, 4)
                ElseIf Vy >= 0 Then
                    State(StateFM.FallDown, 4)
                End If

            Case StateFM.FallDown
                If PosX > 540 Then
                    PosX = 540
                    Vy = 5
                    Vx = 0
                    PosY = PosY + Vy
                ElseIf PosX < 50 Then
                    PosX = 50
                    Vy = 5
                    Vx = 0
                    PosY = PosY + Vy
                Else
                    PosX = PosX + Vx
                    PosY = PosY + Vy
                    Vy = Vy + 0.2
                End If
                GetNextFrame()
                If PosY >= 290 Then
                    State(StateFM.JumpEnd, 5)
                    PosY = 290
                    Vx = 0
                    Vy = 0
                End If

            Case StateFM.JumpEnd
                GetNextFrame()
                If FrameIdx = 0 And CurrFrame = 0 Then
                    State(StateFM.Stand, 1)
                End If

            Case StateFM.OilGlob
                'lock = True
                GetNextFrame()
                If FrameIdx = 0 And CurrFrame = 0 Then
                    State(StateFM.Stand, 1)
                End If

            Case StateFM.FireShot
                ' lock = True
                GetNextFrame()
                If FrameIdx = 0 And CurrFrame = 0 Then
                    State(StateFM.Stand, 1)
                End If

            Case StateFM.Roar
                ' lock = True
                GetNextFrame()
                If FrameIdx = 0 And CurrFrame = 0 Then
                    State(StateFM.Stand, 1)
                End If

            Case StateFM.GetHit
                'lock = True
                GetNextFrame()
                If FrameIdx = 0 And CurrFrame = 0 Then
                    State(StateFM.Dead, 10)
                End If

            Case StateFM.Dead
                'lock = True
                GetNextFrame()

        End Select

    End Sub

End Class

Public Class CCharFire
    Inherits CCharacter

    Public CurrState As StateFire

    Public Sub State(state As StateFire, idxspr As Integer)
        CurrState = state
        IdxArrSprites = idxspr
        CurrFrame = 0
        FrameIdx = 0
    End Sub

    Public Overrides Sub Update()

        Select Case CurrState
            Case StateFire.Create
                GetNextFrame()
                If FrameIdx = 0 And CurrFrame = 0 Then
                    State(StateFire.Shoot, 1)
                End If

            Case StateFire.Shoot
                GetNextFrame()
                PosX = PosX + Vx
                PosY = PosY + Vy
                If Vy < 1 Then
                    Vy = Vy + 0.1
                ElseIf Vy > 1 Then
                    Vy = Vy + 0.3
                End If
                If PosY >= 325 Or PosX > 565 Or PosX < 30 Then
                    'PosY = 325
                    State(StateFire.Ground, 2)
                End If

            Case StateFire.Ground
                GetNextFrame()
                destroy = True
                PosX = 600
                PosY = 600
        End Select

    End Sub

End Class

Public Class CCharOil
    Inherits CCharacter

    Public CurrState As StateOil

    Public Sub State(state As StateOil, idxspr As Integer)
        CurrState = state
        IdxArrSprites = idxspr
        CurrFrame = 0
        FrameIdx = 0

    End Sub

    Public Overrides Sub Update()

        Select Case CurrState
            Case StateOil.Create
                GetNextFrame()
                If FrameIdx = 0 And CurrFrame = 0 Then
                    State(StateOil.Shoot, 1)
                End If

            Case StateOil.Shoot
                GetNextFrame()
                PosX = PosX + Vx
                PosY = PosY + Vy
                Vy = Vy + 0.5
                If PosY >= 325 Then
                    PosY = 325
                    State(StateOil.Ground, 2)
                End If

            Case StateOil.Ground
                GetNextFrame()
                If FrameIdx = 0 And CurrFrame = 0 Then
                    State(StateOil.Ground1, 3)
                End If

            Case StateOil.Ground1
                GetNextFrame()
                If PosX < 0 Or PosX > 610 Then
                    destroy = True
                End If

            Case StateOil.OilFire
                'lock1 = True
                GetNextFrame()
                If PosX < 0 Or PosX > 610 Then
                    destroy = True
                End If

        End Select

    End Sub

End Class

Public Class CCharMegaman
    Inherits CCharacter

    Dim random As New Random()
    Public CurrState As StateMegaman

    Public Sub State(state As StateMegaman, idxspr As Integer)
        CurrState = state
        IdxArrSprites = idxspr
        CurrFrame = 0
        FrameIdx = 0

    End Sub

    Public Overrides Sub Update()
        Select Case CurrState
            Case StateMegaman.Stand
                lock = False
                GetNextFrame()

            Case StateMegaman.JumpUp1
                PosX = PosX + Vx
                PosY = PosY + Vy
                Vy = Vy + 0.2
                'lock = True
                GetNextFrame()
                If FrameIdx = 0 And CurrFrame = 0 Then
                    State(StateMegaman.JumpUp2, 2)
                End If

            Case StateMegaman.JumpUp2
                PosX = PosX + Vx
                PosY = PosY + Vy
                Vy = Vy + 0.2
                GetNextFrame()
                If FrameIdx = 0 And CurrFrame = 0 Then
                    State(StateMegaman.JumpUp3, 3)
                End If

            Case StateMegaman.JumpUp3
                PosX = PosX + Vx
                PosY = PosY + Vy
                Vy = Vy + 0.2
                GetNextFrame()
                If PosX >= 555 Then
                    PosX = 555
                    Vx = 0
                    Vy = 0
                    State(StateMegaman.JumpDown1, 4)
                ElseIf PosX <= 35 Then
                    PosX = 35
                    Vy = 0
                    Vx = 0
                    State(StateMegaman.JumpDown1, 4)
                ElseIf Vy >= 0 Then
                    State(StateMegaman.JumpDown1, 4)
                End If

            Case StateMegaman.JumpDown1
                'lock = False
                PosX = PosX + Vx
                PosY = PosY + Vy
                Vy = Vy + 0.2
                GetNextFrame()
                If FrameIdx = 0 And CurrFrame = 0 Then
                    State(StateMegaman.JumpDown2, 5)
                End If

            Case StateMegaman.JumpDown2
                If PosX >= 555 Then
                    PosX = 555
                    Vy = 5
                    Vx = 0
                    PosY = PosY + Vy
                ElseIf PosX <= 35 Then
                    PosX = 35
                    Vy = 5
                    Vx = 0
                    PosY = PosY + Vy
                Else
                    PosX = PosX + Vx
                    PosY = PosY + Vy
                    Vy = Vy + 0.2
                End If
                GetNextFrame()
                If PosY >= 300 Then
                    State(StateMegaman.Stand, 0)
                    PosY = 300
                    Vx = 0
                    Vy = 0
                End If

            Case StateMegaman.Run
                GetNextFrame()
                If FDir = FaceDir.Left Then
                    PosX = PosX - 3
                ElseIf FDir = FaceDir.Right Then
                    PosX = PosX + 3
                End If
                If CurrFrame = 0 And FrameIdx = 0 Then
                    State(StateMegaman.Stand, 0)
                End If

            Case StateMegaman.GetHit
                'lock = True
                GetNextFrame()
                If CurrFrame = 0 And FrameIdx = 0 Then
                    PosY = 150
                    Vy = 1
                    State(StateMegaman.JumpDown2, 5)
                End If

        End Select

    End Sub

End Class

Public Class CCharConveyor
    Inherits CCharacter

    Public CurrState As StateConveyor

    Public Sub State(state As StateConveyor, idxspr As Integer)
        CurrState = state
        IdxArrSprites = idxspr
        CurrFrame = 0
        FrameIdx = 0

    End Sub

    Public Overrides Sub Update()
        Select Case CurrState
            Case StateConveyor.Create
                GetNextFrame()

            Case StateConveyor.MoveLeft
                GetNextFrame()

            Case StateConveyor.MoveRight
                GetNextFrame()

        End Select

    End Sub
End Class

Public Class CElmtFrame
    Public CtrPoint As TPoint
    Public Top, Bottom, Left, Right As Integer
    Public Idx As Integer
    Public MaxFrameTime As Integer

    Public Sub New(ctrx As Integer, ctry As Integer, l As Integer, t As Integer, r As Integer, b As Integer, mft As Integer)
        CtrPoint.x = ctrx
        CtrPoint.y = ctry
        Top = t
        Bottom = b
        Left = l
        Right = r
        MaxFrameTime = mft

    End Sub
End Class

Public Class CArrFrame
    'N = number of frames
    'Elmt = array of frame
    Public N As Integer
    Public Elmt As CElmtFrame()

    'Public Sub New()
    '    N = 0
    '    ReDim Elmt(-1)
    'End Sub

    'Public Overloads Sub Insert(EF As CElmtFrame)
    '    ReDim Preserve Elmt(N)
    '    Elmt(N) = EF
    '    N = N + 1
    'End Sub

    Public Overloads Sub Insert(ctrx As Integer, ctry As Integer, l As Integer, t As Integer, r As Integer, b As Integer, mft As Integer)
        Dim EF As CElmtFrame
        EF = New CElmtFrame(ctrx, ctry, l, t, r, b, mft)
        ReDim Preserve Elmt(N)
        Elmt(N) = EF
        N = N + 1

    End Sub

End Class

Public Structure TPoint
    Dim x As Integer
    Dim y As Integer

End Structure


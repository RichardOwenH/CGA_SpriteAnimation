Imports System.IO

Public Class Form1

    Dim bmp As Bitmap
    Dim Bg, Img As CImage
    Dim SpriteMap As CImage
    Dim SpriteMask As CImage
    Dim FMRoar, FMJumpStart, FMMidAir, FMJumpEnd, FMFallDown, FMOilGlob, FMFireShot, FMGetHit, FMDead, FMIntro, FMStand As CArrFrame
    Dim MGStand, MGJumpUp1, MGJumpUp2, MGJumpUp3, MGJumpDown1, MGJumpDown2, MGRun, MGGetHit As CArrFrame
    Dim CBLeft, CBRight, CBCreate As CArrFrame
    Dim FireCreate, FireShoot, FireGround As CArrFrame
    Dim OilCreate, OilShoot, OilGround, OilGround1, OilFire As CArrFrame
    Dim ListChar As New List(Of CCharacter)
    Public FM As CCharFM
    Dim MG As CCharMegaman
    Dim CB As CCharConveyor
    Dim FS As CCharFire
    Dim O As CCharOil

    Public Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'backsound effect
        My.Computer.Audio.Play("backsound.wav")

        'open image for background, assign to bg

        Bg = New CImage
        Bg.OpenImage("BgFlameMSmall.bmp")

        Bg.CopyImg(Img)

        SpriteMap = New CImage
        SpriteMap.OpenImage("FMammoth_MegaMm.bmp")

        SpriteMap.CreateMask(SpriteMask)

        'initialize sprites

        CBCreate = New CArrFrame
        CBCreate.Insert(353, 1673, 68, 1669, 625, 1679, 2)

        CBLeft = New CArrFrame
        CBLeft.Insert(353, 1596, 68, 1588, 625, 1603, 2)
        CBLeft.Insert(353, 1622, 68, 1616, 625, 1629, 2)
        CBLeft.Insert(353, 1648, 68, 1642, 625, 1655, 2)
        CBLeft.Insert(353, 1673, 68, 1669, 625, 1679, 2)

        CBRight = New CArrFrame

        CBRight.Insert(353, 1673, 68, 1669, 625, 1679, 2)
        CBRight.Insert(353, 1648, 68, 1642, 625, 1655, 2)
        CBRight.Insert(353, 1622, 68, 1616, 625, 1629, 2)
        CBRight.Insert(353, 1596, 68, 1588, 625, 1603, 2)

        CB = New CCharConveyor
        ReDim CB.ArrSprites(2)
        CB.ArrSprites(0) = CBCreate
        CB.ArrSprites(1) = CBRight
        CB.ArrSprites(2) = CBLeft

        CB.PosX = 295
        CB.PosY = 330
        CB.Vx = 0
        CB.Vy = 0

        ListChar.Add(CB)

        CB.State(StateConveyor.Create, 0)
        CB.FDir = FaceDir.Right

        FMIntro = New CArrFrame
        FMIntro.Insert(113, 130, 67, 72, 170, 170, 10)
        FMIntro.Insert(248, 128, 203, 70, 302, 164, 3)
        FMIntro.Insert(403, 126, 339, 66, 460, 166, 3)
        FMIntro.Insert(546, 130, 505, 52, 612, 168, 3)
        FMIntro.Insert(693, 126, 654, 52, 760, 167, 20)
        'FMIntro.Insert(106, 298, 61, 243, 171, 335, 7)
        'FMIntro.Insert(270, 297, 222, 235, 333, 336, 2)
        'FMIntro.Insert(657, 302, 610, 224, 720, 341, 1)
        'FMIntro.Insert(1043, 299, 996, 234, 1106, 342, 1)
        'FMIntro.Insert(1410, 300, 1360, 235, 1475, 341, 2)
        'FMIntro.Insert(1570, 299, 1523, 248, 1634, 339, 3)

        FMRoar = New CArrFrame
        FMRoar.Insert(113, 130, 67, 72, 170, 170, 3)
        FMRoar.Insert(248, 128, 203, 70, 302, 164, 3)
        FMRoar.Insert(403, 126, 339, 66, 460, 166, 3)
        FMRoar.Insert(546, 130, 505, 52, 612, 168, 3)
        FMRoar.Insert(693, 126, 654, 52, 760, 167, 20)

        FMJumpStart = New CArrFrame
        FMJumpStart.Insert(106, 298, 61, 243, 171, 335, 7)
        FMJumpStart.Insert(270, 297, 222, 235, 333, 336, 2)

        FMMidAir = New CArrFrame
        FMMidAir.Insert(657, 302, 610, 224, 720, 341, 1)

        FMFallDown = New CArrFrame
        FMFallDown.Insert(1043, 299, 996, 234, 1106, 342, 1)

        FMJumpEnd = New CArrFrame
        FMJumpEnd.Insert(1410, 300, 1360, 235, 1475, 341, 2)
        FMJumpEnd.Insert(1570, 299, 1523, 248, 1634, 339, 3)

        FMOilGlob = New CArrFrame
        FMOilGlob.Insert(108, 486, 61, 431, 169, 520, 3)
        FMOilGlob.Insert(260, 485, 220, 425, 325, 525, 2)
        FMOilGlob.Insert(412, 485, 369, 429, 475, 522, 2)
        FMOilGlob.Insert(562, 484, 519, 432, 625, 525, 2)
        FMOilGlob.Insert(710, 486, 664, 432, 773, 525, 3)
        FMOilGlob.Insert(862, 486, 814, 430, 923, 525, 2)
        FMOilGlob.Insert(1029, 486, 970, 430, 1090, 528, 2)
        FMOilGlob.Insert(1186, 487, 1137, 430, 1250, 528, 2)
        FMOilGlob.Insert(1349, 485, 1299, 430, 1409, 528, 2)
        FMOilGlob.Insert(102, 618, 60, 563, 168, 658, 2)
        FMOilGlob.Insert(260, 618, 219, 562, 323, 656, 5) 'gk ada di spritemap
        FMOilGlob.Insert(414, 617, 372, 561, 480, 656, 3) 'gk ada di spritemap

        FMFireShot = New CArrFrame
        FMFireShot.Insert(116, 804, 60, 743, 182, 843, 6)
        FMFireShot.Insert(271, 804, 217, 743, 335, 842, 2)
        FMFireShot.Insert(430, 803, 373, 743, 495, 841, 6)

        FMGetHit = New CArrFrame
        FMGetHit.Insert(1100, 989, 1048, 928, 1167, 1030, 1)

        FMDead = New CArrFrame
        FMDead.Insert(99, 978, 60, 914, 166, 1031, 1) '60 meledak 90 layar putih 200 hilang
        FMDead.Insert(258, 979, 213, 917, 325, 1031, 1)

        FMStand = New CArrFrame
        FMStand.Insert(682, 985, 636, 923, 747, 1024, 1)

        FM = New CCharFM
        ReDim FM.ArrSprites(10)
        FM.ArrSprites(0) = FMIntro
        FM.ArrSprites(1) = FMStand
        FM.ArrSprites(2) = FMJumpStart
        FM.ArrSprites(3) = FMMidAir
        FM.ArrSprites(4) = FMFallDown
        FM.ArrSprites(5) = FMJumpEnd
        FM.ArrSprites(6) = FMOilGlob
        FM.ArrSprites(7) = FMFireShot
        FM.ArrSprites(8) = FMRoar
        FM.ArrSprites(9) = FMGetHit
        FM.ArrSprites(10) = FMDead

        FM.PosX = 490
        FM.PosY = 290
        FM.Vx = 0
        FM.Vy = 0
        FM.State(StateFM.Intro, 0)
        FM.FDir = FaceDir.Left

        ListChar.Add(FM)

        MGStand = New CArrFrame
        MGStand.Insert(88, 1472, 61, 1437, 116, 1502, 5)
        MGStand.Insert(186, 1472, 156, 1437, 215, 1502, 5)

        MGJumpUp1 = New CArrFrame
        MGJumpUp1.Insert(991, 1629, 962, 1587, 1012, 1662, 4)

        MGJumpUp2 = New CArrFrame
        MGJumpUp2.Insert(1068, 1623, 1043, 1581, 1093, 1666, 4)

        MGJumpUp3 = New CArrFrame
        MGJumpUp3.Insert(1146, 1623, 1122, 1582, 1178, 1667, 2)

        MGJumpDown1 = New CArrFrame
        MGJumpDown1.Insert(1464, 1624, 1437, 1578, 1495, 1665, 7)

        MGJumpDown2 = New CArrFrame
        MGJumpDown2.Insert(1552, 1618, 1528, 1578, 1581, 1663, 2)

        MGRun = New CArrFrame
        MGRun.Insert(92, 1348, 64, 1311, 120, 1377, 2)
        MGRun.Insert(185, 1348, 159, 1311, 200, 1378, 2)
        MGRun.Insert(263, 1349, 242, 1312, 287, 1380, 2)
        MGRun.Insert(353, 1346, 325, 1311, 388, 1379, 2)
        MGRun.Insert(450, 1348, 420, 1310, 481, 1380, 2)
        MGRun.Insert(547, 1346, 520, 1313, 576, 1378, 2)
        MGRun.Insert(635, 1348, 602, 1313, 666, 1381, 2)

        MGGetHit = New CArrFrame
        MGGetHit.Insert(756, 1471, 735, 1439, 789, 1508, 4)
        MGGetHit.Insert(836, 1473, 812, 1423, 871, 1511, 4)

        FS = New CCharFire
        FireCreate = New CArrFrame
        FireCreate.Insert(624, 804, 609, 784, 636, 811, 1)

        FireShoot = New CArrFrame
        FireShoot.Insert(624, 808, 609, 784, 636, 811, 1)
        FireShoot.Insert(666, 806, 654, 786, 675, 809, 1)
        FireShoot.Insert(707, 808, 694, 789, 719, 807, 2)

        FireGround = New CArrFrame
        FireGround.Insert(746, 808, 733, 793, 757, 810, 3)
        FireGround.Insert(787, 812, 773, 800, 801, 813, 3)

        O = New CCharOil
        OilCreate = New CArrFrame
        OilCreate.Insert(575, 628, 549, 613, 581, 640, 1)

        OilShoot = New CArrFrame
        OilShoot.Insert(566, 628, 549, 613, 581, 640, 1)

        OilGround = New CArrFrame
        OilGround.Insert(622, 632, 596, 610, 648, 638, 1)
        OilGround.Insert(681, 631, 656, 610, 706, 639, 1)
        OilGround.Insert(747, 628, 720, 611, 774, 639, 1)
        OilGround.Insert(814, 629, 786, 611, 840, 639, 1)

        OilGround1 = New CArrFrame
        OilGround1.Insert(880, 628, 854, 611, 907, 639, 3)

        OilFire = New CArrFrame
        OilFire.Insert(967, 636, 936, 601, 990, 639, 1)
        OilFire.Insert(1034, 638, 1007, 601, 1058, 642, 1)
        OilFire.Insert(1099, 640, 1074, 593, 1125, 643, 3)

        bmp = New Bitmap(Img.Width, Img.Height)


        DisplayImg()
        ResizeImg()
        CreateMegaman()


        Timer1.Enabled = True
    End Sub

    Sub PutSprites()
        Dim cc As CCharacter

        'set background
        'Dim w, h As Integer
        'w = Img.Width - 1
        'h = Img.Height - 1

        'Parallel.For(0, w - 1, _
        '   Sub(i)
        '     Parallel.For(0, h - 1, _
        '       Sub(j)
        '         For j = 0 To h - 1
        '           Img.Elmt(i, j) = Bg.Elmt(i, j)
        '         Next

        '       End Sub)
        '   End Sub)

        For i = 0 To Img.Width - 1
            For j = 0 To Img.Height - 1
                Img.Elmt(i, j) = Bg.Elmt(i, j)
            Next
        Next


        For Each cc In ListChar
            Dim EF As CElmtFrame = cc.ArrSprites(cc.IdxArrSprites).Elmt(cc.FrameIdx)
            Dim spritewidth = EF.Right - EF.Left
            Dim spriteheight = EF.Bottom - EF.Top
            Dim ImgX, ImgY As Integer
            If cc.FDir = FaceDir.Left Then
                'obj can out of bounds
                Dim spriteleft As Integer = cc.PosX - EF.CtrPoint.x + EF.Left
                Dim spritetop As Integer = cc.PosY - EF.CtrPoint.y + EF.Top

                'set mask
                For i = 0 To spritewidth
                    For j = 0 To spriteheight
                        ImgX = spriteleft + i
                        ImgY = spritetop + j
                        If ImgX >= 0 And ImgX <= Img.Width - 1 And ImgY >= 0 And ImgY <= Img.Height - 1 Then
                            Img.Elmt(spriteleft + i, spritetop + j) = OpAnd(Img.Elmt(spriteleft + i, spritetop + j), SpriteMask.Elmt(EF.Left + i, EF.Top + j))
                        End If
                    Next
                Next

                'set sprite
                For i = 0 To spritewidth
                    For j = 0 To spriteheight
                        ImgX = spriteleft + i
                        ImgY = spritetop + j
                        If ImgX >= 0 And ImgX <= Img.Width - 1 And ImgY >= 0 And ImgY <= Img.Height - 1 Then
                            Img.Elmt(spriteleft + i, spritetop + j) = OpOr(Img.Elmt(spriteleft + i, spritetop + j), SpriteMap.Elmt(EF.Left + i, EF.Top + j))
                        End If
                    Next
                Next
            Else 'facing right
                Dim spriteleft = cc.PosX + EF.CtrPoint.x - EF.Right
                Dim spritetop = cc.PosY - EF.CtrPoint.y + EF.Top
                'set mask
                For i = 0 To spritewidth
                    For j = 0 To spriteheight
                        ImgX = spriteleft + i
                        ImgY = spritetop + j
                        If ImgX >= 0 And ImgX <= Img.Width - 1 And ImgY >= 0 And ImgY <= Img.Height - 1 Then
                            Img.Elmt(spriteleft + i, spritetop + j) = OpAnd(Img.Elmt(spriteleft + i, spritetop + j), SpriteMask.Elmt(EF.Right - i, EF.Top + j))
                        End If
                    Next
                Next

                'set sprite
                For i = 0 To spritewidth
                    For j = 0 To spriteheight
                        ImgX = spriteleft + i
                        ImgY = spritetop + j
                        If ImgX >= 0 And ImgX <= Img.Width - 1 And ImgY >= 0 And ImgY <= Img.Height - 1 Then
                            Img.Elmt(spriteleft + i, spritetop + j) = OpOr(Img.Elmt(spriteleft + i, spritetop + j), SpriteMap.Elmt(EF.Right - i, EF.Top + j))
                        End If
                    Next
                Next
            End If
        Next

    End Sub

    Sub DisplayImg()
        'display bg and sprite on picturebox
        Dim i, j As Integer
        'Dim sw As New System.Diagnostics.Stopwatch

        'sw.Start()

        PutSprites()

        Dim rect As New Rectangle(0, 0, bmp.Width, bmp.Height)
        Dim bmpdata As System.Drawing.Imaging.BitmapData = bmp.LockBits(rect,
     System.Drawing.Imaging.ImageLockMode.ReadWrite, bmp.PixelFormat)

        'Get the address of the first line.
        Dim ptr As IntPtr = bmpdata.Scan0


        'Declare an array to hold the bytes of the bitmap.
        Dim bytes As Integer = Math.Abs(bmpdata.Stride) * bmp.Height
        Dim rgbvalues(bytes) As Byte


        'Copy the RGB values into the array.
        System.Runtime.InteropServices.Marshal.Copy(ptr, rgbvalues, 0, bytes)

        Dim n As Integer = 0
        Dim col As System.Drawing.Color

        'Set every third value to 255. A 24bpp bitmap will look red.  
        For j = 0 To Img.Height - 1
            For i = 0 To Img.Width - 1
                col = Img.Elmt(i, j)
                rgbvalues(n) = col.B
                rgbvalues(n + 1) = col.G
                rgbvalues(n + 2) = col.R
                rgbvalues(n + 3) = col.A

                n = n + 4
            Next
        Next

        'Copy the RGB values back to the bitmap
        System.Runtime.InteropServices.Marshal.Copy(rgbvalues, 0, ptr, bytes)


        'Unlock the bits.
        bmp.UnlockBits(bmpdata)

        'Timer1.Enabled = False

        'MsgBox(CStr(bmp.GetPixel(0, 0).A) + " " + CStr(bmp.GetPixel(0, 0).R) + " " + CStr(bmp.GetPixel(0, 0).G) + " " + CStr(bmp.GetPixel(0, 0).B))
        'MsgBox(CStr(bmp.GetPixel(1, 0).A) + " " + CStr(bmp.GetPixel(1, 0).R) + " " + CStr(bmp.GetPixel(1, 0).G) + " " + CStr(bmp.GetPixel(1, 0).B))

        PictureBox1.Refresh()

        PictureBox1.Image = bmp
        PictureBox1.Width = bmp.Width
        PictureBox1.Height = bmp.Height
        PictureBox1.Top = 0
        PictureBox1.Left = 0


    End Sub

    Sub Displaypause()
        'display bg and sprite on picturebox
        Dim i, j As Integer

        For i = 0 To Img.Width - 1
            For j = 0 To Img.Height - 1
                Img.Elmt(i, j) = Bg.Elmt(i, j)
            Next

        Next
        Dim rect As New Rectangle(0, 0, bmp.Width, bmp.Height)
        Dim bmpdata As System.Drawing.Imaging.BitmapData = bmp.LockBits(rect,
     System.Drawing.Imaging.ImageLockMode.ReadWrite, bmp.PixelFormat)

        'Get the address of the first line.
        Dim ptr As IntPtr = bmpdata.Scan0


        'Declare an array to hold the bytes of the bitmap.
        Dim bytes As Integer = Math.Abs(bmpdata.Stride) * bmp.Height
        Dim rgbvalues(bytes) As Byte


        'Copy the RGB values into the array.
        System.Runtime.InteropServices.Marshal.Copy(ptr, rgbvalues, 0, bytes)

        Dim n As Integer = 0
        Dim col As System.Drawing.Color

        'Set every third value to 255. A 24bpp bitmap will look red.  
        For j = 0 To Img.Height - 1
            For i = 0 To Img.Width - 1
                col = Img.Elmt(i, j)
                rgbvalues(n) = col.B
                rgbvalues(n + 1) = col.G
                rgbvalues(n + 2) = col.R
                rgbvalues(n + 3) = col.A

                n = n + 4
            Next
        Next

        'Copy the RGB values back to the bitmap
        System.Runtime.InteropServices.Marshal.Copy(rgbvalues, 0, ptr, bytes)


        'Unlock the bits.
        bmp.UnlockBits(bmpdata)

        PictureBox1.Refresh()

        PictureBox1.Image = bmp
        PictureBox1.Width = bmp.Width
        PictureBox1.Height = bmp.Height
        PictureBox1.Top = 0
        PictureBox1.Left = 0

    End Sub
    Sub ResizeImg()
        Dim w, h As Integer

        w = PictureBox1.Width
        h = PictureBox1.Height

        Me.ClientSize = New Size(w, h)

    End Sub

    Dim i As Integer = 0

    Public Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Dim cc As CCharacter
        Dim random As New Random()

        PictureBox1.Refresh()

        For Each cc In ListChar
            cc.Update()
        Next


        'create fire and oil 
        If FM.CurrState = StateFM.OilGlob And FM.FrameIdx = 6 And FM.CurrFrame = 0 Then
            CreateOil()
        ElseIf FM.CurrState = StateFM.FireShot And FM.FrameIdx = 1 And FM.CurrFrame = 0 Then
            CreateFire()
        End If


        'conveyor change direction
        If FM.CurrState = StateFM.Intro And FM.FrameIdx = 4 And FM.CurrFrame = 19 And CB.CurrState = StateConveyor.Create Then
            CB.State(StateConveyor.MoveRight, 1)
        ElseIf FM.CurrState = StateFM.Roar And FM.FrameIdx = 4 And FM.CurrFrame = 19 And CB.CurrState = StateConveyor.MoveRight Then
            CB.State(StateConveyor.MoveLeft, 2)
        ElseIf FM.CurrState = StateFM.Roar And FM.FrameIdx = 4 And FM.CurrFrame = 19 And CB.CurrState = StateConveyor.MoveLeft Then
            CB.State(StateConveyor.MoveRight, 1)
        End If

        'random respawn
        If MG.CurrState = StateMegaman.JumpDown2 And MG.PosY = 150 Then
            MG.PosX = random.Next(50, 560)
            Do While MG.PosX < FM.PosX + 70 And MG.PosX > FM.PosX - 70
                MG.PosX = random.Next(50, 560)
            Loop
        End If

        'conveyor flame mammoth
        If FM.FDir = FaceDir.Left Then
            If FM.PosX > 520 Then
                FM.PosX = 520
            ElseIf FM.PosX < 50 Then
                FM.PosX = 50
            ElseIf FM.PosX < 520 AndAlso FM.PosX > 50 AndAlso FM.PosY = 290 Then
                If CB.CurrState = StateConveyor.MoveRight Then
                    FM.PosX = FM.PosX + 1
                ElseIf CB.CurrState = StateConveyor.MoveLeft Then
                    FM.PosX = FM.PosX - 1
                End If
            End If
        ElseIf FM.FDir = FaceDir.Right Then
            If FM.PosX > 540 Then
                FM.PosX = 540
            ElseIf FM.PosX < 70 Then
                FM.PosX = 70
            ElseIf FM.PosX < 540 AndAlso FM.PosX > 70 AndAlso FM.PosY = 290 Then
                If CB.CurrState = StateConveyor.MoveRight Then
                    FM.PosX = FM.PosX + 1
                ElseIf CB.CurrState = StateConveyor.MoveLeft Then
                    FM.PosX = FM.PosX - 1
                End If
            End If
        End If

        'conveyor megaman
        If MG.PosX > 555 Then
            MG.PosX = 555
        ElseIf MG.PosX < 35 Then
            MG.PosX = 35
        ElseIf MG.PosX < 555 AndAlso MG.PosX > 35 AndAlso MG.PosY = 300 Then
            If CB.CurrState = StateConveyor.MoveRight Then
                MG.PosX = MG.PosX + 1
            ElseIf CB.CurrState = StateConveyor.MoveLeft Then
                MG.PosX = MG.PosX - 1
            End If
        End If

        'conveyor oil
        If CB.CurrState = StateConveyor.MoveRight Then
            O.PosX = O.PosX + 1
        ElseIf CB.CurrState = StateConveyor.MoveLeft Then
            O.PosX = O.PosX - 1
        End If

        Dim listchar1 As New List(Of CCharacter)

        For Each cc In ListChar
            If Not cc.destroy Then
                listchar1.Add(cc)
            End If
        Next

        ListChar = listchar1

        DisplayImg()

        'collision
        If MG.lock = False Then
            collision()
        End If


    End Sub

    Public Sub CreateFire()
        FS.destroy = False
        If FM.FDir = FaceDir.Left Then
            FS.PosX = FM.PosX - 60
            FS.FDir = FaceDir.Left
            FS.Vx = -5
            FS.Vy = 0
        Else
            FS.PosX = FM.PosX + 60
            FS.FDir = FaceDir.Right
            FS.Vx = 5
            FS.Vy = 0
        End If

        FS.PosY = FM.PosY - 10

        ReDim FS.ArrSprites(2)
        FS.ArrSprites(0) = FireCreate
        FS.ArrSprites(1) = FireShoot
        FS.ArrSprites(2) = FireGround

        FS.State(StateFire.Create, 0)
        ListChar.Add(FS)
    End Sub

    Public Sub CreateOil()
        O.destroy = False
        If FM.FDir = FaceDir.Left Then
            O.PosX = FM.PosX - 40
            O.FDir = FaceDir.Left
            O.Vx = -5
            O.Vy = -5
        Else
            O.PosX = FM.PosX + 40
            O.FDir = FaceDir.Right
            O.Vx = 5
            O.Vy = -5
        End If

        O.PosY = FM.PosY - 15

        ReDim O.ArrSprites(4)
        O.ArrSprites(0) = OilCreate

        O.ArrSprites(1) = OilShoot
        O.ArrSprites(2) = OilGround
        O.ArrSprites(3) = OilGround1
        O.ArrSprites(4) = OilFire

        O.State(StateOil.Create, 0)
        ListChar.Add(O)
    End Sub

    Public Sub CreateMegaman()
        MG = New CCharMegaman
        ReDim MG.ArrSprites(8)
        MG.ArrSprites(0) = MGStand
        MG.ArrSprites(1) = MGJumpUp1
        MG.ArrSprites(2) = MGJumpUp2
        MG.ArrSprites(3) = MGJumpUp3
        MG.ArrSprites(4) = MGJumpDown1
        MG.ArrSprites(5) = MGJumpDown2
        MG.ArrSprites(6) = MGRun
        MG.ArrSprites(7) = MGGetHit

        MG.PosX = 100
        MG.PosY = 300
        MG.Vx = 0
        MG.Vy = 0
        MG.State(StateMegaman.Stand, 0)
        MG.FDir = FaceDir.Right

        ListChar.Add(MG)
    End Sub

    Public Sub collision()
        Dim obj1 = FM.ArrSprites(FM.IdxArrSprites).Elmt(FM.FrameIdx)
        Dim obj2 = MG.ArrSprites(MG.IdxArrSprites).Elmt(MG.FrameIdx)

        Dim x1, y1, w1, h1 As Int32
        Dim x2, y2, w2, h2 As Int32
        Dim x3, y3, w3, h3 As Int32
        Dim x4, y4, w4, h4 As Int32
        Dim x5, y5, w5, h5 As Int32

        'FM
        x1 = FM.PosX
        y1 = FM.PosY
        w1 = obj1.Right - obj1.Left - 10
        h1 = obj1.Bottom - obj1.Top - 5
        'dummy
        x2 = MG.PosX
        y2 = MG.PosY
        w2 = obj2.Right - obj2.Left - 5
        h2 = obj2.Bottom - obj2.Top - 5

        'condition for MG and FM collide
        If (x1 < (x2 + w2) And (x1 + w1) > x2 And y1 < (y2 + h2) And (h1 + y1) > y2) Then
            MG.State(StateMegaman.GetHit, 7)
            MG.Vx = 0
            MG.lock = True
        End If

        If FS.CurrState = StateFire.Shoot Then
            obj1 = FS.ArrSprites(FS.IdxArrSprites).Elmt(FS.FrameIdx)
            'fire
            x3 = FS.PosX
            y3 = FS.PosY
            w3 = obj1.Right - obj1.Left - 10
            h3 = obj1.Bottom - obj1.Top - 5
            'condition for fire and MG collide
            If (x3 < (x2 + w2) And (x3 + w3) > x2 And y3 < (y2 + h2) And (h3 + y3) > y2) Then
                MG.State(StateMegaman.GetHit, 7)
                MG.Vx = 0
                FS.State(StateFire.Ground, 2)
                MG.lock = True
            End If
        End If

        'condition for oil and fire collide
        If O.CurrState = StateOil.Ground1 And FS.CurrState = StateFire.Shoot Then
            obj1 = FS.ArrSprites(FS.IdxArrSprites).Elmt(FS.FrameIdx)
            obj2 = O.ArrSprites(O.IdxArrSprites).Elmt(O.FrameIdx)
            'fire
            x3 = FS.PosX
            y3 = FS.PosY
            w3 = obj1.Right - obj1.Left
            h3 = obj1.Bottom - obj1.Top
            'oil
            x4 = O.PosX
            y4 = O.PosY
            w4 = obj2.Right - obj2.Left
            h4 = obj2.Bottom - obj2.Top

            If (x3 < (x4 + w4) And (x3 + w3) > x4 And y3 < (y4 + h4) And (h3 + y3) > y4) Then
                O.State(StateOil.OilFire, 4)
                FS.State(StateFire.Ground, 2)
            End If
        End If

        If O.CurrState = StateOil.OilFire Then
            'condition for oilfire and MG collide
            obj1 = O.ArrSprites(O.IdxArrSprites).Elmt(O.FrameIdx)
            'oilfire
            x5 = O.PosX
            y5 = O.PosY
            w5 = obj1.Right - obj1.Left - 60
            h5 = obj1.Bottom - obj1.Top - 10
            If (x5 < (x2 + w2) And (x5 + w5) > x2 And y5 < (y2 + h2) And (h5 + y5) > y2 And O.CurrState = StateOil.OilFire) Then
                MG.State(StateMegaman.GetHit, 7)
                MG.Vx = 0
                O.destroy = True
                O.PosX = 600
                O.PosY = 600
            End If
        End If
    End Sub

    Public Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        'atur pergerakan
        If FM.CurrState = StateFM.Stand Then
            If e.KeyCode = Keys.O Then
                FM.State(StateFM.JumpStart, 2)
            ElseIf e.KeyCode = Keys.K Then
                FM.State(StateFM.OilGlob, 6)
            ElseIf e.KeyCode = Keys.L Then
                FM.State(StateFM.FireShot, 7)
            ElseIf e.KeyCode = Keys.Enter Then
                FM.State(StateFM.Roar, 8)
            ElseIf e.KeyCode = Keys.I Then
                FM.FDir = FaceDir.Left
                'FM.PosX = FM.PosX - 3
            ElseIf e.KeyCode = Keys.P Then
                FM.FDir = FaceDir.Right
                'FM.PosX = FM.PosX + 3
            ElseIf e.KeyCode = Keys.Escape Then
                FM.State(StateFM.GetHit, 9)
            End If
        End If

        If MG.CurrState = StateMegaman.Stand Then
            If e.KeyCode = Keys.Q Then
                MG.FDir = FaceDir.Left
                MG.State(StateMegaman.Run, 6)
            ElseIf e.KeyCode = Keys.E Then
                MG.FDir = FaceDir.Right
                MG.State(StateMegaman.Run, 6)
            ElseIf e.KeyCode = Keys.W Then
                If MG.FDir = FaceDir.Right Then
                    MG.Vx = 2
                    MG.Vy = -7
                    MG.State(StateMegaman.JumpUp1, 1)
                ElseIf MG.FDir = FaceDir.Left Then
                    MG.Vx = -2
                    MG.Vy = -7
                    MG.State(StateMegaman.JumpUp1, 1)
                End If
            End If
        End If


        'pause play
        If e.KeyCode = Keys.V Then
            Timer1.Enabled = False
            Bg.OpenImage("pause.bmp")
            Bg.CopyImg(Img)
            Displaypause()

        ElseIf e.KeyCode = Keys.B Then
            Timer1.Enabled = True
            Bg.OpenImage("BgFlameMSmall.bmp")
            Bg.CopyImg(Img)
            DisplayImg()
        End If
    End Sub
End Class

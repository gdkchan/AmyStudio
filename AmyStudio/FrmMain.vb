'Sonic 2 Tile Viewer by gdkchan

Imports System.IO
Imports System.Drawing.Imaging
Imports System.Runtime.InteropServices

Public Class FrmMain
    Const Fast_Mode As Boolean = False

    Dim Sapphire As Color = Color.FromArgb(15, 82, 186)

    '---

    Const Emerald_Hill_Tiles_Offset As Integer = &H95C24
    Const Chemical_Plant_Tiles_Offset As Integer = &HB6174
    Const Mystic_Cave_Tiles_Offset As Integer = &HA9D74

    Const Emerald_Hill_Tiles_16x16_Offset As Integer = &H94E74
    Const Chemical_Plant_Tiles_16x16_Offset As Integer = &HB5234
    Const Mystic_Cave_Tiles_16x16_Offset As Integer = &HA8D04

    Const Emerald_Hill_Tiles_128x128_Offset As Integer = &H99D34
    Const Chemical_Plant_Tiles_128x128_Offset As Integer = &HB90F4
    Const Mystic_Cave_Tiles_128x128_Offset As Integer = &HAD454

    Const Emerald_Hill_Palette_Offset As Integer = &H2A02
    Const Chemical_Plant_Palette_Offset As Integer = &H2DE2
    Const Mystic_Cave_Palette_Offset As Integer = &H2D22

    Const Emerald_Hill_Level_Act_1_Offset As Integer = &H45AC4
    Const Emerald_Hill_Level_Act_2_Offset As Integer = &H45C84
    Const Chemical_Plant_Level_Act_1_Offset As Integer = &H48774
    Const Chemical_Plant_Level_Act_2_Offset As Integer = &H48A84
    Const Mystic_Cave_Level_Act_1_Offset As Integer = &H47B24
    Const Mystic_Cave_Level_Act_2_Offset As Integer = &H47D24

    Dim Tiles_Offset, Tiles_16x16_Offset, Blocks_Offset, Palette_Offset, Level_Offset As Integer
    Dim Bg_Index As Integer
    Dim Bg_Scroll_Y As Boolean

    '---

    Dim ROM() As Byte

    Dim Palette(63) As Color

    Private Structure TileData
        Dim Data() As Byte
    End Structure
    Dim Tiles() As TileData
    Dim Tiles_16x16() As Bitmap
    Dim Blocks() As Bitmap

    Dim Map_Background As Image
    Dim Map_Foreground As Image
    Dim Load_Lock As Boolean

    Dim Mouse_Position As Point

#Region "GUI"
    Private Sub BtnOpen_Click(sender As Object, e As EventArgs) Handles BtnOpen.Click
        Dim OpenDlg As New OpenFileDialog
        OpenDlg.Title = "Abrir ROM de Mega Drive"
        OpenDlg.Filter = "Sega Genesis ROMs|*.bin"
        If OpenDlg.ShowDialog = DialogResult.OK Then
            ROM = File.ReadAllBytes(OpenDlg.FileName)
            Load_Lock = True
            CmbAct.SelectedIndex = 0
            CmbZone.SelectedIndex = 0
            Load_Lock = False
            Load_Data()
        End If
    End Sub

    Private Sub TilesScroll_Scroll(sender As Object, e As ScrollEventArgs) Handles TilesScroll.Scroll
        PicTiles.Top = TilesScroll.Value * -1
    End Sub
    Private Sub MapScroll(sender As Object, e As ScrollEventArgs) Handles MapScrollX.Scroll, MapScrollY.Scroll
        PicMap.Refresh()
    End Sub

    'Carrega offsets do mapa selecionado
    Private Sub Cmb_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbZone.SelectedIndexChanged, CmbAct.SelectedIndexChanged
        Dim Cmb As ComboBox = sender
        If Cmb.SelectedIndex < 0 Then Exit Sub

        Select Case CmbZone.SelectedIndex
            Case 0 'Emerald Hill
                Tiles_Offset = Emerald_Hill_Tiles_Offset
                Tiles_16x16_Offset = Emerald_Hill_Tiles_16x16_Offset
                Blocks_Offset = Emerald_Hill_Tiles_128x128_Offset
                Palette_Offset = Emerald_Hill_Palette_Offset
                If CmbAct.SelectedIndex = 1 Then
                    Level_Offset = Emerald_Hill_Level_Act_2_Offset
                Else
                    Level_Offset = Emerald_Hill_Level_Act_1_Offset
                End If
                Bg_Index = 0
                Bg_Scroll_Y = False
            Case 1 'Chemical Plant
                Tiles_Offset = Chemical_Plant_Tiles_Offset
                Tiles_16x16_Offset = Chemical_Plant_Tiles_16x16_Offset
                Blocks_Offset = Chemical_Plant_Tiles_128x128_Offset
                Palette_Offset = Chemical_Plant_Palette_Offset
                If CmbAct.SelectedIndex = 1 Then
                    Level_Offset = Chemical_Plant_Level_Act_2_Offset
                Else
                    Level_Offset = Chemical_Plant_Level_Act_1_Offset
                End If
                Bg_Index = 0
                Bg_Scroll_Y = False
            Case 2 'Mystic Cave
                Tiles_Offset = Mystic_Cave_Tiles_Offset
                Tiles_16x16_Offset = Mystic_Cave_Tiles_16x16_Offset
                Blocks_Offset = Mystic_Cave_Tiles_128x128_Offset
                Palette_Offset = Mystic_Cave_Palette_Offset
                If CmbAct.SelectedIndex = 1 Then
                    Level_Offset = Mystic_Cave_Level_Act_2_Offset
                Else
                    Level_Offset = Mystic_Cave_Level_Act_1_Offset
                End If
                Bg_Index = 2
                Bg_Scroll_Y = True
        End Select

        If Not Load_Lock Then Load_Data()
    End Sub

    'Botões de fechar/minimizar e efeitos dos botões
    Private Sub BtnClose_Click(sender As Object, e As EventArgs) Handles BtnClose.Click
        End
    End Sub
    Private Sub BtnMinimize_Click(sender As Object, e As EventArgs) Handles BtnMinimize.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub
    Private Sub Button_MouseEnter(sender As Object, e As EventArgs) Handles BtnOpen.MouseEnter, BtnMinimize.MouseEnter
        Dim Lbl As Label = sender
        Lbl.BackColor = Sapphire
        Lbl.ForeColor = Color.White
    End Sub
    Private Sub BtnClose_MouseEnter(sender As Object, e As EventArgs) Handles BtnClose.MouseEnter
        Dim Lbl As Label = sender
        Lbl.BackColor = Color.Crimson
        Lbl.ForeColor = Color.White
    End Sub
    Private Sub Button_MouseLeave(sender As Object, e As EventArgs) Handles BtnOpen.MouseLeave, BtnClose.MouseLeave, BtnMinimize.MouseLeave
        Dim Lbl As Label = sender
        Lbl.BackColor = Color.Transparent
        Lbl.ForeColor = Color.Black
    End Sub

    'Move a janela
    Private Sub TitleGrab_MouseDown(sender As Object, e As MouseEventArgs) Handles TitleGrab.MouseDown
        If e.Button = MouseButtons.Left Then
            Mouse_Position.X = Windows.Forms.Cursor.Position.X - Me.Left
            Mouse_Position.Y = Windows.Forms.Cursor.Position.Y - Me.Top
        End If
    End Sub
    Private Sub TitleGrab_MouseMove(sender As Object, e As MouseEventArgs) Handles TitleGrab.MouseMove
        If e.Button = MouseButtons.Left Then
            Me.Top = Windows.Forms.Cursor.Position.Y - Mouse_Position.Y
            Me.Left = Windows.Forms.Cursor.Position.X - Mouse_Position.X
        End If
    End Sub
#End Region

    Private Sub Load_Tiles(Data() As Byte)
        Dim Tile_Count As Integer = Data.Length / 32
        ReDim Tiles(Tile_Count - 1)
        Dim Offset As Integer
        For Tile As Integer = 0 To Tile_Count - 1
            ReDim Tiles(Tile).Data(63)
            For Y As Integer = 0 To 7
                For X As Integer = 0 To 7 Step 2 'Pula 2 pq 1 byte já vai ter Info de 2 pixels
                    Dim Index_1 As Integer = Data(Offset) >> 4 'Lê os 4 bits (XXXX)XXXX e da o shift para direita
                    Dim Index_2 As Integer = Data(Offset) And &HF 'Lê os 4 bits XXXX(XXXX)

                    Tiles(Tile).Data(X + (Y * 8)) = Index_1
                    Tiles(Tile).Data(X + (Y * 8) + 1) = Index_2

                    Offset += 1 'Avança posição de leitura no arquivo
                Next
            Next
        Next
    End Sub
    Private Sub Load_Tiles_16x16(Data() As Byte)
        Dim Tile_Count As Integer = Data.Length / 8
        ReDim Tiles_16x16(Tile_Count - 1)
        Dim Offset As Integer
        For Tile As Integer = 0 To Tile_Count - 1
            Tiles_16x16(Tile) = New Bitmap(16, 16, Imaging.PixelFormat.Format32bppArgb)
            Dim ImgData As BitmapData = Tiles_16x16(Tile).LockBits(New Rectangle(0, 0, 16, 16), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb)
            Dim Image_Buffer((16 * 16 * 4) - 1) As Byte 'Cria um buffer onde vamos armazenar o tile

            For TY As Integer = 0 To 1
                For TX As Integer = 0 To 1
                    Dim Current_Data As Integer = (Data(Offset) * &H100) + Data(Offset + 1)
                    Dim Priority As Boolean = Current_Data And &H8000
                    Dim Palette_Line As Integer = (Current_Data And &H6000) >> 13
                    Dim V_Flip As Boolean = Current_Data And &H1000
                    Dim H_Flip As Boolean = Current_Data And &H800
                    Dim Tile_Index As Integer = Current_Data And &H7FF

                    Dim Tile_Offset As Integer = 0
                    For Y As Integer = 0 To 7
                        For X As Integer = 0 To 7
                            Dim Index As Integer = (Palette_Line * 16) + Tiles(Tile_Index).Data(Tile_Offset)

                            Dim Image_Offset As Integer = ((TX * 8) + If(H_Flip, 7 - X, X) + (((TY * 8) + If(V_Flip, 7 - Y, Y)) * 16)) * 4

                            Image_Buffer(Image_Offset) = Palette(Index).B
                            Image_Buffer(Image_Offset + 1) = Palette(Index).G
                            Image_Buffer(Image_Offset + 2) = Palette(Index).R
                            If Index Mod 16 <> 0 Then Image_Buffer(Image_Offset + 3) = &HFF 'Opaco/Transparente

                            Tile_Offset += 1
                        Next
                    Next

                    Offset += 2
                Next
            Next

            Marshal.Copy(Image_Buffer, 0, ImgData.Scan0, Image_Buffer.Length) 'Copia o buffer para a imagem
            Tiles_16x16(Tile).UnlockBits(ImgData)
        Next
    End Sub
    Private Sub Load_Tiles_128x128(Data() As Byte)
        Dim Tile_Count As Integer = Data.Length / 128
        ReDim Blocks(Tile_Count - 1)
        Dim Offset As Integer
        For Tile As Integer = 0 To Tile_Count - 1
            Blocks(Tile) = New Bitmap(128, 128)
            Dim Gfx As Graphics = Graphics.FromImage(Blocks(Tile))

            For Y As Integer = 0 To 127 Step 16
                For X As Integer = 0 To 127 Step 16
                    Dim Current_Data As Integer = (Data(Offset) * &H100) + Data(Offset + 1)
                    Dim Alternate_Collision As Integer = (Current_Data And &HC000) >> 14
                    Dim Normal_Collision As Integer = (Current_Data And &H3000) >> 12
                    Dim V_Flip As Boolean = Current_Data And &H800
                    Dim H_Flip As Boolean = Current_Data And &H400
                    Dim Tile_Index As Integer = Current_Data And &H3FF

                    If Tile_Index < Tiles_16x16.Length Then
                        'Flip
                        If H_Flip Or V_Flip Then
                            Dim Img As New Bitmap(Tiles_16x16(Tile_Index))
                            If H_Flip Then Img.RotateFlip(RotateFlipType.RotateNoneFlipX)
                            If V_Flip Then Img.RotateFlip(RotateFlipType.RotateNoneFlipY)

                            Gfx.DrawImage(Img, New Point(X, Y))
                        Else
                            Gfx.DrawImage(Tiles_16x16(Tile_Index), New Point(X, Y))
                        End If
                    End If

                    Offset += 2
                Next
            Next
        Next
    End Sub
    Private Sub Load_Palette(Data() As Byte, Offset As Integer, Optional Start_Index As Integer = 0)
        For Index As Integer = Start_Index To 63
            Dim Current_Data As Integer = (Data(Offset) * &H100) + Data(Offset + 1)
            Dim R As Byte = (Current_Data And &HF) * &H11
            Dim G As Byte = ((Current_Data And &HF0) >> 4) * &H11
            Dim B As Byte = ((Current_Data >> 8) And &HF) * &H11
            Palette(Index) = Color.FromArgb(R, G, B)
            Offset += 2
        Next
    End Sub

    'Desenha o mapa em um Buffer completo
    Private Sub Draw_Map(Data() As Byte)
        Map_Background = New Bitmap(128 * 128, 128 * 16)
        Map_Foreground = New Bitmap(128 * 128, 128 * 16)
        Dim Background As Graphics = Graphics.FromImage(Map_Background)
        Dim Foreground As Graphics = Graphics.FromImage(Map_Foreground)

        Dim Offset As Integer
        For Y As Integer = 0 To 15
            For X As Integer = 0 To 127
                Background.DrawImage(Blocks(Data(Offset + 128)), New Point(X * 128, Y * 128)) 'Background
                Foreground.DrawImage(Blocks(Data(Offset)), New Point(X * 128, Y * 128)) 'Foreground
                Offset += 1
            Next
            Offset += 128 'Pula o Background, pois já o desenhamos (ver o +128)
        Next
    End Sub
    Private Sub PicMap_Click(sender As Object, e As PaintEventArgs) Handles PicMap.Paint
        If Map_Foreground IsNot Nothing Then
            With e.Graphics
                .Clear(Palette(Bg_Index * 16))
                .DrawImage(Map_Foreground, New Rectangle(0, 0, PicMap.Width, PicMap.Height), New Rectangle(MapScrollX.Value, MapScrollY.Value, PicMap.Width, PicMap.Height), GraphicsUnit.Pixel)
            End With
        End If
    End Sub
    Private Sub Alpha_Copy(SrcArray() As Byte, SrcOffset As Integer, DstArray() As Byte, DstOffset As Integer, Count As Integer)
        For Offset As Integer = SrcOffset To (SrcOffset + Count) - 1 Step 4
            If SrcArray(Offset + 3) = &HFF Then
                DstArray(DstOffset) = SrcArray(Offset)
                DstArray(DstOffset + 1) = SrcArray(Offset + 1)
                DstArray(DstOffset + 2) = SrcArray(Offset + 2)
                DstArray(DstOffset + 3) = SrcArray(Offset + 3)
            End If
            DstOffset += 4
        Next
    End Sub

    'Carrega um novo mapa de ROM
    Private Sub Load_Data()
        'Carrega todos os dados necessários da ROM
        Dim Tile_Data() As Byte = LZSega.Decompress(ROM, Tiles_Offset)
        Load_Tiles(Tile_Data)
        Tile_Data = LZSega.Decompress(ROM, Tiles_16x16_Offset)
        Load_Palette(ROM, Palette_Offset)
        Load_Tiles_16x16(Tile_Data)
        Tile_Data = LZSega.Decompress(ROM, Blocks_Offset)
        Load_Tiles_128x128(Tile_Data)

        Dim Img As New Bitmap(128, Blocks.Length * 128)
        Dim Gfx As Graphics = Graphics.FromImage(Img)
        For Y As Integer = 0 To Blocks.Length - 1
            Gfx.DrawImage(Blocks(Y), New Point(0, Y * 128))
        Next
        PicTiles.Image = Img
        TilesScroll.Maximum = PicTiles.Height - TilesPanel.Height
        TilesScroll.Enabled = True

        Tile_Data = LZSega.Decompress(ROM, Level_Offset)
        Draw_Map(Tile_Data)
        MapScrollX.Maximum = (128 * 128) - PicMap.Width
        MapScrollY.Maximum = (128 * 16) - PicMap.Height
        MapScrollX.Enabled = True
        MapScrollY.Enabled = True
        PicMap.Refresh()
    End Sub
End Class

Public Class LZSega
    Public Shared Function Decompress(Input() As Byte, ByVal Offset As Integer) As Byte()
        Dim Max_Indicators As Integer = (Input.Length / 8) * 2
        Dim Output((((Input.Length - Max_Indicators) * 3) + Max_Indicators) - 1) As Byte

        Dim Bit_To_Test As Integer
        Dim Indicator As Integer

        Dim Description As Integer = Input(Offset) + (Input(Offset + 1) * &H100)
        Dim Input_Offset As Integer = Offset + 2
        Dim Output_Offset As Integer

        Do
            Indicator = (Description And (2 ^ Bit_To_Test))
            Bit_To_Test += 1

            If Bit_To_Test > 15 Then
                Bit_To_Test = 0
                Description = Input(Input_Offset) + (Input(Input_Offset + 1) * &H100)
                Input_Offset += 2
            End If

            If Indicator > 0 Then '*** 1
                Output(Output_Offset) = Input(Input_Offset)
                Input_Offset += 1
                Output_Offset += 1
            End If

            If Indicator = 0 Then
                Dim Back As Integer = 0, Length As Integer = 0, Info_Bytes As Integer

                Indicator = (Description And (2 ^ Bit_To_Test))
                Bit_To_Test += 1
                If Bit_To_Test > 15 Then
                    Bit_To_Test = 0
                    Description = Input(Input_Offset) + (Input(Input_Offset + 1) * &H100)
                    Input_Offset += 2
                End If

                If Indicator > 0 Then '*** 01
                    Info_Bytes = (Input(Input_Offset) * &H100) + Input(Input_Offset + 1)
                    Input_Offset += 2

                    If (Info_Bytes And 7) = 0 Then '*** 01 3b
                        If Input(Input_Offset) = 0 Then Exit Do

                        If Input(Input_Offset) = 1 Then
                            Bit_To_Test = 0
                            Description = Input(Input_Offset) + (Input(Input_Offset + 1) * &H100)
                            Input_Offset += 2
                        End If

                        Info_Bytes = (Info_Bytes << 8) Or Input(Input_Offset)
                        Input_Offset += 1

                        Back = -8192 + ((Info_Bytes >> 11) And &H1F) * 256 + (Info_Bytes >> 16)
                        Length = (Info_Bytes And &HFF) + 1
                    Else '*** 01 2b
                        Back = -8192 + ((Info_Bytes >> 3) And &H1F) * 256 + (Info_Bytes >> 8)
                        Length = (Info_Bytes And &H7) + 2
                    End If
                Else '*** 00xx
                    For Length_Bit As Integer = 2 To 1 Step -1
                        Indicator = (Description And (2 ^ Bit_To_Test))
                        If Indicator > 0 Then Length += Length_Bit
                        Bit_To_Test += 1
                        If Bit_To_Test > 15 Then
                            Bit_To_Test = 0
                            Description = Input(Input_Offset) + (Input(Input_Offset + 1) * &H100)
                            Input_Offset += 2
                        End If
                    Next

                    Length += 2

                    Info_Bytes = Input(Input_Offset)
                    Input_Offset += 1

                    Back = -256 + Info_Bytes
                End If

                Dim Temp_Back As Integer = Back
                Dim Current_Offset As Integer = Output_Offset
                For Byte_To_Copy As Integer = 0 To Length - 1
                    Output(Output_Offset) = Output(Current_Offset + Temp_Back)
                    Output_Offset += 1
                    If Temp_Back < -1 Then Temp_Back += 1 Else Temp_Back = Back
                Next
            End If
        Loop

        ReDim Preserve Output(Output_Offset - 1)
        Return Output
    End Function
End Class

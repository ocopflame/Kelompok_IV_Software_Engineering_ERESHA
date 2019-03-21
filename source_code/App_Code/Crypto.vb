Imports Microsoft.VisualBasic
Imports System.Text
Imports System.Windows.Forms

Public Module Crypto

    Private Const INT_lens As Integer = 1
    Public str As StringBuilder
    Public searchStr As String
    Dim b As Integer = 6
    Dim p() As Integer = {2, 4, 7, 9, 3, INT_lens}
    Dim i As Integer
    Dim j As Integer
    Dim k As Integer
    Dim c As Integer
    Dim lens As Integer

    Public Function Encrypt(ByVal inputstr As String)

        str = New StringBuilder(inputstr)
        lens = str.Length
        While (lens < b) OrElse (lens Mod b)
            str.Append(" ")
            lens += INT_lens
        End While
        For i = 0 To ((lens / b) - INT_lens)
            For j = 0 To (b - INT_lens)
                k = p(j) + 100
                c = (6 * i + j)
                str.Replace(str.Chars(c), Chr(Asc(str.Chars(c)) + k), c, INT_lens)
            Next
        Next
        Return str.ToString
        str = Nothing
    End Function

    Public Function Decrypt(ByVal inputstr As String)

        str = New StringBuilder(inputstr)
        lens = str.Length
        While (lens < b) OrElse (lens Mod b)
            str.Append(" ")
            lens += INT_lens
        End While

        For i = 0 To ((lens / b) - INT_lens)
            For j = 0 To (b - INT_lens)
                k = p(j) + 100
                c = (6 * i + j)
                str.Replace(str.Chars(c), Chr(Asc(str.Chars(c)) - k), c, INT_lens)
            Next
        Next
        Return str.ToString
        str = Nothing
    End Function

End Module

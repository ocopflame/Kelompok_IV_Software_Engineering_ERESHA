Imports Microsoft.VisualBasic

Public Module GlobalClass

    Public EmailManagedCare As String = "mppk.pertamedika@gmail.com"
    Public EmailAdmin As String = "parera.jumano@gmail.com"
    Public PassEmailAdmin As String = "annisa2008"

    Public Function AdminMenu() As String
        Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder
        AdminMenu = sb.ToString
    End Function

    Public Function SuperUserMenu() As String
        Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder
        SuperUserMenu = sb.ToString
    End Function

    Public Function UserMenu() As String
        Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder
        sb.AppendLine("<li><a href=""m_pekerja.aspx"">Master Pekerja</a></li>")
        sb.AppendLine("<li><a href=""m_mapcoa.aspx"">Mapping WT-COA</a></li>")
        sb.AppendLine("<li><a href=""m_rekanan.aspx"">Master Vendor</a></li>")
        'sb.AppendLine("<li><a href=""m_bank.aspx"">Rekening Pekerja</a></li>")
        sb.AppendLine("<li><a href=""posting.aspx"">Posting DME</a></li>")
        'sb.AppendLine("<li><a href=""bk.aspx"">Pembuatan BK Gaji</a></li>")
        sb.AppendLine("<li><a href=""sp3.aspx"">Pembuatan SP3</a></li>")
        UserMenu = sb.ToString
    End Function

    Public Function Terbilang(ByVal nilai As Long) As String

        Dim bilangan As String() = {" ", "Satu", "Dua", "Tiga", "Empat", "Lima", "Enam", "Tujuh", "Delapan", "Sembilan", "Sepuluh", "Sebelas"}

        If nilai < 12 Then
            Return " " & bilangan(nilai)
        ElseIf nilai < 20 Then
            Return Terbilang(nilai - 10) & " Belas"
        ElseIf nilai < 100 Then
            Return (Terbilang(CInt((nilai \ 10))) & " Puluh") + Terbilang(nilai Mod 10)
        ElseIf nilai < 200 Then
            Return " Seratus" & Terbilang(nilai - 100)
        ElseIf nilai < 1000 Then
            Return (Terbilang(CInt((nilai \ 100))) & " Ratus") + Terbilang(nilai Mod 100)
        ElseIf nilai < 2000 Then
            Return " Seribu" & Terbilang(nilai - 1000)
        ElseIf nilai < 1000000 Then
            Return (Terbilang(CInt((nilai \ 1000))) & " Ribu") + Terbilang(nilai Mod 1000)
        ElseIf nilai < 1000000000 Then
            Return (Terbilang(CInt((nilai \ 1000000))) & " Juta") + Terbilang(nilai Mod 1000000)
        ElseIf nilai < 1000000000000 Then
            Return (Terbilang(CInt((nilai \ 1000000000))) & " Milyar") + Terbilang(nilai Mod 1000000000)
        ElseIf nilai < 1000000000000000 Then
            Return (Terbilang(CInt((nilai \ 1000000000000))) & " Trilyun") + Terbilang(nilai Mod 1000000000000)
        Else
            Return ""
        End If

    End Function

    Public Function DataSaved() As String
        Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder
        sb.Append("<div id=""alertsaved"" class=""alert alert-success"">")
        sb.Append("<i class=""mdi mdi-content-save""></i><span>Data berhasil disimpan!</span>")
        sb.Append("</div>")
        DataSaved = sb.ToString
    End Function

    Public Function DataDeleted() As String
        Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder
        sb.Append("<div id=""alertdeleted"" class=""alert alert-danger"">")
        sb.Append("<i class=""mdi mdi-delete""></i><span>Data berhasil dihapus!</span>")
        sb.Append("</div>")
        DataDeleted = sb.ToString
    End Function

End Module

Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports System.Data.Odbc

Imports System.Globalization
Imports System.Threading
Imports System.Xml
Imports System.Net.Mail
Imports GlobalClass
Imports Crypto

Partial Class cpass
    Inherits System.Web.UI.Page
    Private StrConn As String = ConfigurationManager.ConnectionStrings("MyConn").ConnectionString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ltAlert.Text = ""
        lblErrMessage.Text = ""

        If Not Page.IsPostBack Then
            If GetCookies("e_filing_UserName").ToString = "" Then
                Response.Redirect("login.aspx")
            End If

            lblNamaUser.Text = "Hi, " + GetCookies("e_filing_NamaUser").ToString

            txtPasswordLama.Focus()
        End If
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        lblErrMessage.Text = ""

        If Encrypt(Trim(txtPasswordLama.Text)) <> GetCookies("e_filing_Password").ToString Then
            lblErrMessage.Text = "Password saat ini salah." : Exit Sub
        End If

        If Trim(txtPasswordLama.Text) = Trim(txtPasswordBaru.Text) Then
            lblErrMessage.Text = "Password baru tidak boleh sama dengan password saat ini." : Exit Sub
        End If

        If Not txtPasswordBaru.Text.ToString = "" Then
            If Not System.Text.RegularExpressions.Regex.IsMatch(txtPasswordBaru.Text.ToString, "^[a-zA-Z0-9_]{3,16}$") Then
                lblErrMessage.Text = "Password Baru mengandung karakter yang tidak diperbolehkan."
                Exit Sub
            End If
        End If

        If txtPasswordBaru.Text <> txtConfirmPasswordBaru.Text Then
            lblErrMessage.Text = "Password dengan Confirm Password tidak sama." : Exit Sub
        End If

        Dim ODBCConn As OdbcConnection = New OdbcConnection(StrConn)
        Dim ODBCCmd As OdbcCommand
        Dim StrSQL As String

        Try
            ODBCConn.Open()
            StrSQL = " UPDATE user SET password='" + Encrypt(Trim(txtPasswordBaru.Text)) + "'"
            StrSQL = StrSQL + " WHERE UserID='" + GetCookies("UserID") + "'"

            ODBCCmd = New OdbcCommand(StrSQL, ODBCConn)
            ODBCCmd.ExecuteNonQuery()
            ODBCCmd.Dispose()
            ODBCConn.Close()

            ltAlert.Text = DataSaved()
            SetCookies("Password", Encrypt(Trim(txtPasswordBaru.Text)))

            txtPasswordLama.Text = ""
            txtPasswordBaru.Text = ""
            txtConfirmPasswordBaru.Text = ""

        Catch ex As Exception
            lblErrMessage.Text = "Error: " + ex.Message
        End Try
    End Sub

    '################ GENERAL ################

    Protected Function GetCookies(ByVal CookieName As String) As String
        Dim Cookie As HttpCookie = Request.Cookies(CookieName)
        If Not Cookie Is Nothing Then
            Return Cookie.Value
        Else
            Return ""
        End If
    End Function

    Protected Sub SetCookies(ByVal CookieName As String, ByVal CookieValue As String)
        Dim Cookie As HttpCookie = New HttpCookie(CookieName)
        Cookie.Value = CookieValue
        Response.Cookies.Add(Cookie)
    End Sub

    Sub ShowAlert(ByVal TextMessage As String)
        'If Not ClientScript.IsStartupScriptRegistered("Alert") Then
        '    Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder
        '    sb.Append("<script language=JavaScript>")
        '    sb.Append("alert('" + TextMessage + "');")
        '    sb.Append("</script>")
        '    ClientScript.RegisterStartupScript(Me.GetType(), "Alert", sb.ToString())
        'End If

        If (Not ClientScript.IsStartupScriptRegistered("alert")) Then
            Page.ClientScript.RegisterStartupScript _
            (Me.GetType(), "alert", "alertMe();", True)
        End If
    End Sub

End Class

Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.Odbc
Imports System.IO


Partial Class login
    Inherits System.Web.UI.Page
    Private StrConn As String = ConfigurationManager.ConnectionStrings("MyConn").ConnectionString

    Protected Sub cmdLogin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdLogin.Click
        Dim ODBCConn As OdbcConnection = New OdbcConnection(StrConn)
        Dim ODBCCmd As OdbcCommand
        Dim Reader As OdbcDataReader

        If Not txtUserID.Text.ToString = "" Then
            If Not System.Text.RegularExpressions.Regex.IsMatch(txtUserID.Text.ToString, "^[a-zA-Z0-9_]{3,16}$") Then
                lblErrMessage.Text = "User ID tidak diperbolehkan."
                Exit Sub
            End If
        Else
            lblErrMessage.Text = "User ID tidak diperbolehkan."
            Exit Sub
        End If

        Try
            ODBCConn.Open()
            Dim strSQL As String
            strSQL = " SELECT * FROM user "
            strSQL += " WHERE UserName=? AND Password=? "

            ODBCCmd = New OdbcCommand(strSQL, ODBCConn)
            ODBCCmd.Parameters.AddWithValue("@UserName", txtUserID.Text)
            ODBCCmd.Parameters.AddWithValue("@Password", txtPassword.Text)
            Reader = ODBCCmd.ExecuteReader

            If Reader.HasRows Then
                Reader.Read()
                With Reader
                    SetCookies("UserID", .Item("UserID").ToString)
                    SetCookies("UserName", .Item("UserName").ToString)
                    SetCookies("NamaUser", .Item("NamaUser").ToString)
                    SetCookies("Password", .Item("Password").ToString)
                End With
                Reader.Close()
                ODBCCmd.Dispose()
                ODBCConn.Close()
                Response.Redirect("agendain.aspx")
            Else
                lblErrMessage.Text = "User ID atau Password tidak benar"
                txtUserID.Focus()
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            ODBCConn.Close()
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtUserID.Focus()
    End Sub

    Protected Sub SetCookies(ByVal CookieName As String, ByVal CookieValue As String)
        Dim Cookie As HttpCookie = New HttpCookie(CookieName)
        Cookie.Value = CookieValue
        Response.Cookies.Add(Cookie)
    End Sub
End Class

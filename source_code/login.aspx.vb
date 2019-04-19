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

        If Not System.Text.RegularExpressions.Regex.IsMatch(txtUserID.Text.ToString, "^[a-zA-Z0-9_]{3,16}$") Then
            lblErrMessage.Text = "ID Pengguna tidak valid."
            Exit Sub
        End If

        If Not System.Text.RegularExpressions.Regex.IsMatch(txtPassword.Text.ToString, "^[a-zA-Z0-9_]{3,16}$") Then
            lblErrMessage.Text = "Password tidak valid."
            Exit Sub
        End If

        Try
            ODBCConn.Open()
            Dim strSQL As String
            strSQL = " SELECT * FROM user "
            strSQL += " WHERE UserName=? AND Password=? AND StatusID=1"

            ODBCCmd = New OdbcCommand(strSQL, ODBCConn)
            ODBCCmd.Parameters.AddWithValue("@UserName", txtUserID.Text)
            ODBCCmd.Parameters.AddWithValue("@Password", Encrypt(txtPassword.Text))

            Reader = ODBCCmd.ExecuteReader

            If Reader.HasRows Then
                Reader.Read()
                With Reader
                    SetCookies("e_filing_DivisiID", .Item("DivisiID").ToString)
                    SetCookies("e_filing_UserID", .Item("UserID").ToString)
                    SetCookies("e_filing_UserName", .Item("UserName").ToString)
                    SetCookies("e_filing_NamaUser", .Item("NamaUser").ToString)
                    SetCookies("e_filing_Password", .Item("Password").ToString)
                    SetCookies("e_filing_OtorisasiID", .Item("OtorisasiID").ToString)
                    SetCookies("e_filing_StatusID", .Item("StatusID").ToString)

                    Select Case .Item("OtorisasiID").ToString
                        Case "1" : Response.Redirect("divisi.aspx")
                        Case "2" : Response.Redirect("agendain.aspx")
                    End Select
                End With

                Reader.Close()
                ODBCCmd.Dispose()
                ODBCConn.Close()
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
        If Not Page.IsPostBack Then
            SetCookies("e_filing_DivisiID", "")
            SetCookies("e_filing_UserID", "")
            SetCookies("e_filing_UserName", "")
            SetCookies("e_filing_NamaUser", "")
            SetCookies("e_filing_Password", "")
            SetCookies("e_filing_OtorisasiID", "")
            SetCookies("e_filing_StatusID", "")
            txtUserID.Focus()
        End If
    End Sub

    Protected Sub SetCookies(ByVal CookieName As String, ByVal CookieValue As String)
        Dim Cookie As HttpCookie = New HttpCookie(CookieName)
        Cookie.Value = CookieValue
        Response.Cookies.Add(Cookie)
    End Sub

    Protected Function GetCookies(ByVal CookieName As String) As String
        Dim Cookie As HttpCookie = Request.Cookies(CookieName)
        If Not Cookie Is Nothing Then
            Return Cookie.Value
        Else
            Return ""
        End If
    End Function

End Class

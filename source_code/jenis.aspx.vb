﻿Imports System.IO
Imports System.Data
Imports System.Data.Odbc
Imports System.Net.Mail
Imports System.Globalization
Imports System.Threading
Imports System.Xml
Imports GlobalClass
Imports Crypto

Partial Class Jenis
    Inherits System.Web.UI.Page
    Private StrConn As String = ConfigurationManager.ConnectionStrings("MyConn").ConnectionString

    Dim dsMasterCOA As New DataSet
    Dim dsMasterPekerja As New DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ltAlert.Text = ""
        lblErrMessage.Text = ""

        If Not Page.IsPostBack Then
            If GetCookies("e_filing_UserName").ToString = "" Then
                Response.Redirect("login.aspx")
            End If

            If GetCookies("e_filing_OtorisasiID").ToString <> "1" Then
                Response.Redirect("forbidden.aspx")
            End If

            lblNamaUser.Text = "Hi, " + GetCookies("e_filing_NamaUser").ToString

            ShowGridData()
        End If
    End Sub

    Protected Sub gv_Sorting(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Dim Sorting As String = Split(Convert.ToString(ViewState("Sorting")), " ")(1)

        If Sorting = "Asc" Then
            ViewState("Sorting") = e.SortExpression & " Desc"
        Else
            ViewState("Sorting") = e.SortExpression & " Asc"
        End If

        ShowGridData()
    End Sub

    Protected Sub gvData_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvData.PageIndexChanging
        gvData.PageIndex = e.NewPageIndex
        ShowGridData()
    End Sub

    Protected Sub gvData_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvData.RowCommand
        Dim ODBCConn As OdbcConnection = New OdbcConnection(StrConn)
        Dim ODBCCmd As OdbcCommand
        Dim Reader As OdbcDataReader
        Dim strSQL As String

        Try
            ODBCConn.Open()

            Select Case e.CommandName
                Case "Ubah"
                    lblHeader.Text = "Edit Data Jenis Dokumen"

                    Dim IdData As Integer = Convert.ToInt32(e.CommandArgument)
                    hfIdData.Value = IdData.ToString
                    strSQL = " SELECT * FROM Jenis WHERE JenisID=" & IdData
                    ODBCCmd = New OdbcCommand(strSQL, ODBCConn)
                    Reader = ODBCCmd.ExecuteReader()
                    Reader.Read()

                    txtJenis.Text = Reader.Item("Jenis").ToString
                    ddlStatus.SelectedValue = CInt(Reader.Item("StatusID"))

                    pnlInputData.Visible = True
                    pnlGridData.Visible = False

                Case Else
                    Exit Sub

            End Select

            ODBCConn.Close()

        Catch ex As Exception
            Response.Write("Rowcommand: " + ex.Message)
        Finally
            ODBCConn.Close()
        End Try

    End Sub


    Protected Sub gvData_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvData.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                'CType(e.Row.FindControl("BtnDelete"), Image).Attributes.Add("onclick", "javascript:return confirm('Anda yakin akan menghapus data pada baris ini?');")

                If e.Row.Cells(2).Text.ToString = "&nbsp;" Then
                    CType(e.Row.FindControl("BtnEdit"), ImageButton).Visible = False
                    CType(e.Row.FindControl("lblNomor"), Label).Visible = False
                End If
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub

    Sub ShowGridData()
        Dim dt As New DataTable

        Dim ODBCConn As OdbcConnection = New OdbcConnection(StrConn)
        Dim ODBCCmd As OdbcCommand

        Try
            ODBCConn.Open()
            Dim strSQL As String

            strSQL = " SELECT *, "
            strSQL += " CASE WHEN StatusID=1 THEN 'Aktif' ELSE 'Tidak Aktif' END AS Status "
            strSQL += " FROM Jenis "

            ODBCCmd = New OdbcCommand(strSQL, ODBCConn)
            Dim daData As New OdbcDataAdapter(ODBCCmd)
            dt.Clear()
            daData.Fill(dt)
            ODBCConn.Close()

            If dt.Rows.Count = 0 Then
                dt.Rows.Add(dt.NewRow())
            End If

            If ViewState("Sorting") = "" Then
                ViewState("Sorting") = "Jenis ASC"
            End If

            dt.DefaultView.Sort = ViewState("Sorting")
            gvData.DataSource = dt
            gvData.DataBind()

        Catch ex As Exception
            lblErrMessage.Text = ex.Message.ToString
        Finally
            ODBCConn.Close()
        End Try

    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        lblHeader.Text = "Daftar Jenis Dokumen"
        pnlInputData.Visible = False
        pnlGridData.Visible = True
    End Sub


    '################ GENERAL ################

    Sub ShowMessage(ByVal TextMessage As String)
        If Not ClientScript.IsStartupScriptRegistered("Alert") Then
            Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder
            sb.Append("<script language=JavaScript>")
            sb.Append("alert('" + TextMessage + "');")
            sb.Append("</script>")
            ClientScript.RegisterStartupScript(Me.GetType(), "Alert", sb.ToString())
        End If
    End Sub

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

    Protected Sub lnkAddNew_Click(sender As Object, e As System.EventArgs) Handles lnkAddNew.Click
        lblHeader.Text = "Tambah Data Jenis Dokumen"
        hfIdData.Value = ""
        txtJenis.Text = ""
        ddlStatus.SelectedValue = 1

        txtJenis.Focus()
        pnlGridData.Visible = False
        pnlInputData.Visible = True
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As System.EventArgs) Handles btnSave.Click
        lblErrMessage.Text = ""

        If txtJenis.Text = "" Then
            lblErrMessage.Text = "Jenis dokumen harus diisi."
            Exit Sub
        End If

        Dim ODBCConn As OdbcConnection = New OdbcConnection(StrConn)
        Dim ODBCCmd As OdbcCommand
        Dim StrSQL As String

        Try
            ODBCConn.Open()

            If hfIdData.Value.ToString = "" Then
                StrSQL = " INSERT INTO Jenis (Jenis, StatusID, CreateBy, CreateDate, UpdateBy, UpdateDate) "
                StrSQL += " VALUES (?,?,?,?,?,?) "
                ODBCCmd = New OdbcCommand(StrSQL, ODBCConn)
                With ODBCCmd
                    .Parameters.AddWithValue("@Jenis", Trim(txtJenis.Text))
                    .Parameters.AddWithValue("@StatusID", ddlStatus.SelectedValue)
                    .Parameters.AddWithValue("@CreateBy", GetCookies("e_filing_UserName").ToString)
                    .Parameters.Add("@CreateDate", OdbcType.DateTime).Value = Now
                    .Parameters.AddWithValue("@UpdateBy", GetCookies("e_filing_UserName").ToString)
                    .Parameters.Add("@UpdateDate", OdbcType.DateTime).Value = Now
                    .ExecuteNonQuery()
                End With
                ODBCCmd.Dispose()
            Else
                StrSQL = " UPDATE Jenis SET Jenis=?, StatusID=?, UpdateBy=?, UpdateDate=? "
                StrSQL += " WHERE JenisID=" + hfIdData.Value.ToString
                ODBCCmd = New OdbcCommand(StrSQL, ODBCConn)
                With ODBCCmd
                    .Parameters.AddWithValue("@Jenis", Trim(txtJenis.Text))
                    .Parameters.AddWithValue("@StatusID", ddlStatus.SelectedValue)
                    .Parameters.AddWithValue("@UpdateBy", GetCookies("e_filing_UserName").ToString)
                    .Parameters.Add("@UpdateDate", OdbcType.DateTime).Value = Now
                    .ExecuteNonQuery()
                End With
                ODBCCmd.Dispose()
            End If

            StrSQL = " INSERT INTO Log (Modul, Deskripsi, UserName, Tanggal) VALUES (?,?,?,?) "
            ODBCCmd = New OdbcCommand(StrSQL, ODBCConn)
            With ODBCCmd
                .Parameters.AddWithValue("@Modul", "Jenis")
                If hfIdData.Value.ToString = "" Then
                    .Parameters.AddWithValue("@Deskripsi", "Tambah Data Jenis=" + txtJenis.Text)
                Else
                    .Parameters.AddWithValue("@Deskripsi", "Edit Data ID=" + hfIdData.Value.ToString)
                End If
                .Parameters.AddWithValue("@UserName", GetCookies("e_filing_UserName"))
                .Parameters.Add("@Tanggal", OdbcType.DateTime).Value = Now
                .ExecuteNonQuery()
            End With
            ODBCCmd.Dispose()
            ODBCConn.Close()

            ltAlert.Text = DataSaved()
            ShowGridData()

            lblHeader.Text = "Daftar Jenis Dokumen"
            pnlInputData.Visible = False
            pnlGridData.Visible = True

        Catch ex As Exception
            lblErrMessage.Text = "Error: " + ex.Message
        End Try
    End Sub

End Class
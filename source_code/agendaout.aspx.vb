Imports System.IO
Imports System.Data
Imports System.Data.Odbc
Imports System.Net.Mail
Imports System.Globalization
Imports System.Threading
Imports System.Xml
Imports GlobalClass
Imports Crypto

Partial Class agendaout
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

            If GetCookies("e_filing_OtorisasiID").ToString <> "2" Then
                Response.Redirect("forbidden.aspx")
            End If

            lblNamaUser.Text = "Hi, " + GetCookies("e_filing_NamaUser").ToString

            ShowJenis()
            ShowTingkat()
            ShowGridData()

            ddlJenis.Focus()

        End If
    End Sub

    Sub ShowJenis()

        Dim ODBCConn As OdbcConnection = New OdbcConnection(StrConn)
        Dim ODBCCmd As OdbcCommand

        Try
            ODBCConn.Open()
            Dim strSQL As String

            strSQL = " SELECT * FROM jenis "

            ODBCCmd = New OdbcCommand(strSQL, ODBCConn)
            Dim dt As New DataTable
            Dim daData As New OdbcDataAdapter(ODBCCmd)
            dt.Clear()
            daData.Fill(dt)
            ODBCConn.Close()

            With ddlJenis
                .DataSource = dt
                .DataTextField = "Jenis"
                .DataValueField = "JenisID"
                .DataBind()
                .SelectedIndex = 0
            End With

            With ddlFilterJenis
                .DataSource = dt
                .DataTextField = "Jenis"
                .DataValueField = "JenisID"
                .DataBind()
                .SelectedIndex = 0
            End With

        Catch ex As Exception
            lblErrMessage.Text = ex.Message.ToString
        Finally
            ODBCConn.Close()
        End Try
    End Sub

    Sub ShowTingkat()

        Dim ODBCConn As OdbcConnection = New OdbcConnection(StrConn)
        Dim ODBCCmd As OdbcCommand

        Try
            ODBCConn.Open()
            Dim strSQL As String

            strSQL = " SELECT * FROM tingkat "

            ODBCCmd = New OdbcCommand(strSQL, ODBCConn)
            Dim dt As New DataTable
            Dim daData As New OdbcDataAdapter(ODBCCmd)
            dt.Clear()
            daData.Fill(dt)
            ODBCConn.Close()

            With ddlTingkat
                .DataSource = dt
                .DataTextField = "Tingkat"
                .DataValueField = "TingkatID"
                .DataBind()
                .SelectedIndex = 0
            End With

        Catch ex As Exception
            lblErrMessage.Text = ex.Message.ToString
        Finally
            ODBCConn.Close()
        End Try
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
                    lblHeader.Text = "Edit Data Agenda Keluar"

                    Dim IdData As Integer = Convert.ToInt32(e.CommandArgument)
                    hfIdData.Value = IdData.ToString
                    strSQL = " SELECT * FROM outgoing WHERE OutgoingID=" & IdData
                    ODBCCmd = New OdbcCommand(strSQL, ODBCConn)
                    Reader = ODBCCmd.ExecuteReader()
                    Reader.Read()

                    txtTanggal.Text = String.Format("{0:d}", Reader.Item("Tanggal"))
                    txtTglSurat.Text = String.Format("{0:d}", Reader.Item("TanggalSurat"))
                    ddlJenis.SelectedValue = Reader.Item("JenisID")
                    ddlTingkat.SelectedValue = Reader.Item("TingkatID")
                    txtNomor.Text = Reader.Item("NoSurat").ToString
                    txtNoAgenda.Text = Reader.Item("NoAgenda").ToString
                    txtKepada.Text = Reader.Item("Kepada").ToString
                    txtPerihal.Text = Reader.Item("Perihal").ToString
                    txtRingkasan.Text = Reader.Item("Ringkasan").ToString

                    pnlInputData.Visible = True
                    pnlGridData.Visible = False

                Case "Hapus"
                    Dim IdData As Integer = Convert.ToInt32(e.CommandArgument)

                    strSQL = " DELETE FROM outgoing WHERE outgoingID=" + IdData.ToString
                    ODBCCmd = New OdbcCommand(strSQL, ODBCConn)
                    ODBCCmd.ExecuteNonQuery()

                    ShowGridData()
                    ltAlert.Text = DataDeleted()

                Case "Lampiran1"
                    Dim IdData As Integer = Convert.ToInt32(e.CommandArgument)
                    strSQL = " SELECT Lampiran1 FROM outgoing WHERE OutgoingID=" & IdData
                    ODBCCmd = New OdbcCommand(strSQL, ODBCConn)
                    Reader = ODBCCmd.ExecuteReader()
                    Reader.Read()
                    If Reader.Item("Lampiran1").ToString <> "" Then
                        Dim FileName As String = Reader.Item("Lampiran1").ToString
                        Response.ClearContent()
                        Response.Clear()
                        If Path.GetExtension("../attachment/" + Reader.Item("Lampiran1").ToString) = ".jpg" Then
                            Response.ContentType = "image/jpeg"
                        Else
                            Response.ContentType = "Application/pdf"
                        End If
                        Response.AddHeader("Content-Disposition", "attachment; filename=" + FileName + ";")
                        Response.TransmitFile(Server.MapPath("../attachment/" + Reader.Item("Lampiran1").ToString))
                        Response.Flush()
                        Response.End()
                    End If

                Case "Lampiran2"
                    Dim IdData As Integer = Convert.ToInt32(e.CommandArgument)
                    strSQL = " SELECT Lampiran2 FROM outgoing WHERE OutgoingID=" & IdData
                    ODBCCmd = New OdbcCommand(strSQL, ODBCConn)
                    Reader = ODBCCmd.ExecuteReader()
                    Reader.Read()
                    If Reader.Item("Lampiran2").ToString <> "" Then
                        Dim FileName As String = Reader.Item("Lampiran2").ToString
                        Response.ClearContent()
                        Response.Clear()
                        If Path.GetExtension("../attachment/" + Reader.Item("Lampiran2").ToString) = ".jpg" Then
                            Response.ContentType = "image/jpeg"
                        Else
                            Response.ContentType = "Application/pdf"
                        End If
                        Response.AddHeader("Content-Disposition", "attachment; filename=" + FileName + ";")
                        Response.TransmitFile(Server.MapPath("../attachment/" + Reader.Item("Lampiran2").ToString))
                        Response.Flush()
                        Response.End()
                    End If

                Case "Lampiran3"
                    Dim IdData As Integer = Convert.ToInt32(e.CommandArgument)
                    strSQL = " SELECT Lampiran3 FROM outgoing WHERE OutgoingID=" & IdData
                    ODBCCmd = New OdbcCommand(strSQL, ODBCConn)
                    Reader = ODBCCmd.ExecuteReader()
                    Reader.Read()
                    If Reader.Item("Lampiran3").ToString <> "" Then
                        Dim FileName As String = Reader.Item("Lampiran3").ToString
                        Response.ClearContent()
                        Response.Clear()
                        If Path.GetExtension("../attachment/" + Reader.Item("Lampiran3").ToString) = ".jpg" Then
                            Response.ContentType = "image/jpeg"
                        Else
                            Response.ContentType = "Application/pdf"
                        End If
                        Response.AddHeader("Content-Disposition", "attachment; filename=" + FileName + ";")
                        Response.TransmitFile(Server.MapPath("../attachment/" + Reader.Item("Lampiran3").ToString))
                        Response.Flush()
                        Response.End()
                    End If

                Case Else
                    Exit Sub

            End Select

            'ODBCCmd.Dispose()
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
                CType(e.Row.FindControl("BtnDelete"), Image).Attributes.Add("onclick", "javascript:return confirm('Anda yakin akan menghapus data pada baris ini?');")

                If e.Row.Cells(6).Text.ToString = "&nbsp;" Then
                    CType(e.Row.FindControl("BtnEdit"), ImageButton).Visible = False
                    CType(e.Row.FindControl("BtnDelete"), ImageButton).Visible = False
                    CType(e.Row.FindControl("BtnLamp1"), ImageButton).Visible = False
                    CType(e.Row.FindControl("BtnLamp2"), ImageButton).Visible = False
                    CType(e.Row.FindControl("BtnLamp3"), ImageButton).Visible = False
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

            strSQL = " SELECT * FROM outgoing "
            strSQL += " WHERE DivisiID=" + GetCookies("e_filing_DivisiID").ToString

            If ddlFilterJenis.SelectedValue.ToString <> "0" Then
                strSQL += " AND JenisID=" + ddlFilterJenis.SelectedValue.ToString
            End If

            If txtFilterPerihal.Text <> "" Then
                strSQL += " AND Perihal LIKE '%" + Replace(txtFilterPerihal.Text, "'", "''") + "%'"
            End If

            If txtFilterRingkasan.Text <> "" Then
                strSQL += " AND Ringkasan LIKE '%" + Replace(txtFilterRingkasan.Text, "'", "''") + "%'"
            End If

            ODBCCmd = New OdbcCommand(strSQL, ODBCConn)
            Dim daData As New OdbcDataAdapter(ODBCCmd)
            dt.Clear()
            daData.Fill(dt)
            ODBCConn.Close()

            If dt.Rows.Count = 0 Then
                dt.Rows.Add(dt.NewRow())
            End If

            If ViewState("Sorting") = "" Then
                ViewState("Sorting") = "Tanggal DESC"
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
        lblHeader.Text = "Daftar Agenda Keluar"

        pnlInputData.Visible = False
        pnlGridData.Visible = True
    End Sub


    Protected Sub btnRetrieve_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRetrieve.Click
        ShowGridData()
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

    Protected Sub ddlFilterBentuk_DataBound(sender As Object, e As System.EventArgs) Handles ddlFilterJenis.DataBound
        ddlFilterJenis.Items.Insert(0, New ListItem("", "0"))
    End Sub

    Protected Sub lnkAddNew_Click(sender As Object, e As System.EventArgs) Handles lnkAddNew.Click

        hfIdData.Value = ""
        txtTanggal.Text = ""
        txtTglSurat.Text = ""
        ddlJenis.SelectedIndex = 0
        ddlTingkat.SelectedIndex = 0
        txtNomor.Text = ""
        txtNoAgenda.Text = ""
        txtKepada.Text = ""
        txtPerihal.Text = ""
        txtRingkasan.Text = ""
        txtTanggal.Text = Now.Date
        txtTanggal.Focus()

        lblHeader.Text = "Tambah Data Agenda Keluar"

        pnlGridData.Visible = False
        pnlInputData.Visible = True
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As System.EventArgs) Handles btnSave.Click
        lblErrMessage.Text = ""

        Dim StrDBLamp1 As String = ""
        If fuLamp1.HasFile Then
            Dim FillStatus As Boolean
            Dim fileExtension As String
            fileExtension = Path.GetExtension(fuLamp1.FileName).ToLower()
            Dim allowedExtensions As String() = {".pdf", ".jpg"}
            For i As Integer = 0 To allowedExtensions.Length - 1
                If fileExtension = allowedExtensions(i) Then
                    FillStatus = True
                End If
            Next

            If FillStatus Then
                Dim Waktu As String = Format(Now, "yyyyMMdd hhmmss")
                Dim NamaFile As String = "Lamp_" + Waktu + "_#1" + fileExtension

                StrDBLamp1 = NamaFile
                fuLamp1.PostedFile.SaveAs(Server.MapPath("~/attachment/") + NamaFile)
            Else
                lblErrMessage.Text = "Lampiran harus menggunakan format pdf atau jpg."
                Exit Sub
            End If
        End If

        Dim StrDBLamp2 As String = ""
        If fuLamp2.HasFile Then
            Dim FillStatus As Boolean
            Dim fileExtension As String
            fileExtension = Path.GetExtension(fuLamp2.FileName).ToLower()
            Dim allowedExtensions As String() = {".pdf", ".jpg"}
            For i As Integer = 0 To allowedExtensions.Length - 1
                If fileExtension = allowedExtensions(i) Then
                    FillStatus = True
                End If
            Next

            If FillStatus Then
                Dim Waktu As String = Format(Now, "yyyyMMdd hhmmss")
                Dim NamaFile As String = "Lamp_" + Waktu + "_#2" + fileExtension

                StrDBLamp2 = NamaFile
                fuLamp2.PostedFile.SaveAs(Server.MapPath("~/attachment/") + NamaFile)
            Else
                lblErrMessage.Text = "Lampiran harus menggunakan format pdf atau jpg."
                Exit Sub
            End If
        End If

        Dim StrDBLamp3 As String = ""
        If fuLamp3.HasFile Then
            Dim FillStatus As Boolean
            Dim fileExtension As String
            fileExtension = Path.GetExtension(fuLamp3.FileName).ToLower()
            Dim allowedExtensions As String() = {".pdf", ".jpg"}
            For i As Integer = 0 To allowedExtensions.Length - 1
                If fileExtension = allowedExtensions(i) Then
                    FillStatus = True
                End If
            Next

            If FillStatus Then
                Dim Waktu As String = Format(Now, "yyyyMMdd hhmmss")
                Dim NamaFile As String = "Lamp_" + Waktu + "_#3" + fileExtension

                StrDBLamp3 = NamaFile
                fuLamp3.PostedFile.SaveAs(Server.MapPath("~/attachment/") + NamaFile)
            Else
                lblErrMessage.Text = "Lampiran harus menggunakan format pdf atau jpg."
                Exit Sub
            End If
        End If


        Dim ODBCConn As OdbcConnection = New OdbcConnection(StrConn)
        Dim ODBCCmd As OdbcCommand
        Dim StrSQL As String

        Try
            ODBCConn.Open()

            If hfIdData.Value.ToString = "" Then
                StrSQL = " INSERT INTO outgoing (Tanggal, TanggalSurat, JenisID, TingkatID, Kepada, NoSurat, NoAgenda, "
                StrSQL += " Perihal, Ringkasan, "
                StrSQL += " Lampiran1, Lampiran2, Lampiran3, UserName, DivisiID)  "
                StrSQL += " VALUES (?,?,?,?,?,?,?,?,?,?,?,?,'" + GetCookies("e_filing_UserName").ToString + "'," + GetCookies("e_filing_DivisiID").ToString + ")"
            Else
                StrSQL = " UPDATE outgoing SET Tanggal=?, TanggalSurat=?, JenisID=?, TingkatID=?, Kepada=?, NoSurat=?, NoAgenda=?, "
                StrSQL += " Perihal=?, Ringkasan=?, "
                If Trim(StrDBLamp1) <> "" Then
                    StrSQL += " Lampiran1='" + Trim(StrDBLamp1).ToString + "',"
                End If
                If Trim(StrDBLamp2) <> "" Then
                    StrSQL += " Lampiran2='" + Trim(StrDBLamp2).ToString + "',"
                End If
                If Trim(StrDBLamp3) <> "" Then
                    StrSQL += " Lampiran3='" + Trim(StrDBLamp3).ToString + "',"
                End If
                StrSQL += " Username='" + GetCookies("e_filing_UserName").ToString + "'"
                StrSQL += " WHERE outgoingID=" + hfIdData.Value.ToString
            End If

            ODBCCmd = New OdbcCommand(StrSQL, ODBCConn)
            With ODBCCmd
                .Parameters.Add("@Tanggal", OdbcType.Date).Value = Trim(txtTanggal.Text)
                .Parameters.Add("@TanggalSurat", OdbcType.Date).Value = Trim(txtTglSurat.Text)
                .Parameters.AddWithValue("@JenisID", ddlJenis.SelectedValue)
                .Parameters.AddWithValue("@TingkatID", ddlTingkat.SelectedValue)
                .Parameters.AddWithValue("@Dari", Trim(txtKepada.Text))
                .Parameters.AddWithValue("@NoSurat", Trim(txtNomor.Text))
                .Parameters.AddWithValue("@NoAgenda", Trim(txtNoAgenda.Text))
                .Parameters.AddWithValue("@Perihal", Trim(txtPerihal.Text))
                .Parameters.AddWithValue("@Ringkasan", Trim(txtRingkasan.Text))
                .Parameters.AddWithValue("@Lampiran1", Trim(StrDBLamp1))
                .Parameters.AddWithValue("@Lampiran2", Trim(StrDBLamp2))
                .Parameters.AddWithValue("@Lampiran3", Trim(StrDBLamp3))
                .ExecuteNonQuery()
            End With
            ODBCCmd.Dispose()
            ODBCConn.Close()

            ltAlert.Text = DataSaved()
            ShowGridData()
            lblHeader.Text = "Daftar Agenda Keluar"
            pnlInputData.Visible = False
            pnlGridData.Visible = True

        Catch ex As Exception
            lblErrMessage.Text = "Error: " + ex.Message
        End Try
    End Sub

End Class

Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Configuration
Imports System.Text
Imports System.IO
Imports System.Xml
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.html.simpleparser
Imports System.Collections
Imports System.Net

'Namespace myApp.ns.pages

Public Class MyHeaderFooter
    Inherits iTextSharp.text.pdf.PdfPageEventHelper

    Public Overrides Sub OnEndPage(ByVal PDFWrite As iTextSharp.text.pdf.PdfWriter, ByVal PDFDoc As iTextSharp.text.Document)

        Dim ReportFont As BaseFont = BaseFont.CreateFont(HttpContext.Current.Server.MapPath("~/fonts/opensans/OpenSans-Regular.ttf"), BaseFont.WINANSI, BaseFont.EMBEDDED)
        Dim FooterFont As New Font(ReportFont, 7, Font.NORMAL)

        'Header / Footer
        Dim Page As Rectangle = PDFDoc.PageSize

        'Page Header
        Dim PDFImage As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath("~/images/logo.jpg"))

        Dim HeaderTable As PdfPTable = New PdfPTable(1)
        HeaderTable.TotalWidth = 100

        Dim HeaderTableCell As PdfPCell = New PdfPCell(New Phrase(""))
        PDFImage.ScaleToFit(100, 100)
        HeaderTableCell.AddElement(PDFImage)
        HeaderTableCell.Border = Rectangle.NO_BORDER
        HeaderTableCell.VerticalAlignment = Element.ALIGN_TOP
        HeaderTableCell.HorizontalAlignment = Element.ALIGN_LEFT
        HeaderTable.AddCell(HeaderTableCell)
        HeaderTable.WriteSelectedRows(0, -1, PDFDoc.PageSize.Width - PDFDoc.RightMargin - HeaderTable.TotalWidth, (PDFDoc.PageSize.Height - PDFDoc.TopMargin + 5), PDFWrite.DirectContent)

        'Page Footer
        Dim FooterTable As PdfPTable = New PdfPTable(2)
        FooterTable.TotalWidth = Page.Width - PDFDoc.LeftMargin - PDFDoc.RightMargin

        Dim FooterTableCellWidth(1) As Single
        FooterTableCellWidth(0) = 80.0F
        FooterTableCellWidth(1) = 20.0F
        FooterTable.SetWidths(FooterTableCellWidth)

        Dim FooterTableCell As PdfPCell
        FooterTableCell = New PdfPCell(New Phrase("Managed Care - PT Pertamina Bina Medika", FooterFont))
        FooterTableCell.Border = Rectangle.TOP_BORDER
        FooterTableCell.VerticalAlignment = Element.ALIGN_TOP
        FooterTableCell.HorizontalAlignment = Element.ALIGN_LEFT
        FooterTable.AddCell(FooterTableCell)

        'FooterTableCell = New PdfPCell(New Phrase(PDFWrite.PageNumber.ToString, FooterFont))
        FooterTableCell = New PdfPCell(New Phrase("", FooterFont))
        FooterTableCell.Border = Rectangle.TOP_BORDER
        FooterTableCell.VerticalAlignment = Element.ALIGN_TOP
        FooterTableCell.HorizontalAlignment = Element.ALIGN_RIGHT
        FooterTable.AddCell(FooterTableCell)

        FooterTable.WriteSelectedRows(0, -1, PDFDoc.LeftMargin, PDFDoc.BottomMargin, PDFWrite.DirectContent)

    End Sub

End Class


Public Class MyHeaderFooterOnlyPageOne
    Inherits iTextSharp.text.pdf.PdfPageEventHelper

    Public Overrides Sub OnEndPage(ByVal PDFWrite As iTextSharp.text.pdf.PdfWriter, ByVal PDFDoc As iTextSharp.text.Document)

        Dim ReportFont As BaseFont = BaseFont.CreateFont(HttpContext.Current.Server.MapPath("~/fonts/opensans/OpenSans-Regular.ttf"), BaseFont.WINANSI, BaseFont.EMBEDDED)
        Dim FooterFont As New Font(ReportFont, 7, Font.NORMAL)

        'Header / Footer
        Dim Page As Rectangle = PDFDoc.PageSize

        'Page Header
        Dim PDFImage As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath("~/images/logo.jpg"))

        If PDFWrite.PageNumber = 1 Then
            Dim HeaderTable As PdfPTable = New PdfPTable(1)
            HeaderTable.TotalWidth = 100

            Dim HeaderTableCell As PdfPCell = New PdfPCell(New Phrase(""))
            PDFImage.ScaleToFit(100, 100)
            HeaderTableCell.AddElement(PDFImage)
            HeaderTableCell.Border = Rectangle.NO_BORDER
            HeaderTableCell.VerticalAlignment = Element.ALIGN_TOP
            HeaderTableCell.HorizontalAlignment = Element.ALIGN_RIGHT
            HeaderTable.AddCell(HeaderTableCell)
            HeaderTable.WriteSelectedRows(0, -1, PDFDoc.PageSize.Width - PDFDoc.RightMargin - HeaderTable.TotalWidth, (PDFDoc.PageSize.Height - PDFDoc.TopMargin + 5), PDFWrite.DirectContent)
            'HeaderTable.WriteSelectedRows(0, -1, PDFDoc.LeftMargin, PDFDoc.TopMargin, PDFWrite.DirectContent)

        End If

        'Page Footer
        Dim FooterTable As PdfPTable = New PdfPTable(2)
        FooterTable.TotalWidth = Page.Width - PDFDoc.LeftMargin - PDFDoc.RightMargin

        Dim FooterTableCellWidth(1) As Single
        FooterTableCellWidth(0) = 80.0F
        FooterTableCellWidth(1) = 20.0F
        FooterTable.SetWidths(FooterTableCellWidth)

        Dim FooterTableCell As PdfPCell
        FooterTableCell = New PdfPCell(New Phrase("Managed Care - PT Pertamina Bina Medika", FooterFont))
        FooterTableCell.Border = Rectangle.TOP_BORDER
        FooterTableCell.VerticalAlignment = Element.ALIGN_TOP
        FooterTableCell.HorizontalAlignment = Element.ALIGN_LEFT
        FooterTable.AddCell(FooterTableCell)

        FooterTableCell = New PdfPCell(New Phrase("", FooterFont))
        'FooterTableCell = New PdfPCell(New Phrase(PDFWrite.PageNumber.ToString, FooterFont))
        FooterTableCell.Border = Rectangle.TOP_BORDER
        FooterTableCell.VerticalAlignment = Element.ALIGN_TOP
        FooterTableCell.HorizontalAlignment = Element.ALIGN_RIGHT
        FooterTable.AddCell(FooterTableCell)

        FooterTable.WriteSelectedRows(0, -1, PDFDoc.LeftMargin, PDFDoc.BottomMargin, PDFWrite.DirectContent)

    End Sub

End Class






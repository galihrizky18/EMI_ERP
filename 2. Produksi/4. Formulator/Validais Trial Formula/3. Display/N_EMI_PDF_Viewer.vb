Imports System.IO
Imports PdfiumViewer

Public Class N_EMI_PDF_Viewer
    Private _reportTitle As String
    Private _tempFilePath As String
    Private _pdfDocument As PdfDocument

    Public Sub New(tempFilePath As String, Optional reportTitle As String = "Laporan")
        InitializeComponent()
        _tempFilePath = tempFilePath
        _reportTitle = reportTitle

        Me.Text = reportTitle
    End Sub
    Private Sub N_EMI_FrmPdfViewer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.WindowState = FormWindowState.Maximized

            _pdfDocument = PdfDocument.Load(_tempFilePath)
            PdfViewer1.Document = _pdfDocument
            PdfViewer1.ZoomMode = PdfViewerZoomMode.FitWidth
            PdfViewer1.Renderer.Zoom = 0.75
        Catch ex As Exception
            MessageBox.Show("Gagal membuka PDF: " & ex.Message)
        End Try
    End Sub

    Private Sub N_EMI_FrmPdfViewer_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        Try
            If _pdfDocument IsNot Nothing Then
                _pdfDocument.Dispose()
            End If

            If File.Exists(_tempFilePath) Then
                File.Delete(_tempFilePath)
            End If
        Catch ex As Exception
        End Try
    End Sub
End Class
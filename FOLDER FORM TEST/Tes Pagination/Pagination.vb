Public Class Pagination
    Public Event PageChanged(ByVal NewPage As Integer)
    Private _CurrentPage As Integer = 1
    Private _TotalPages As Integer = 1

    Public ReadOnly Property CurrentPage As Integer
        Get
            Return _CurrentPage
        End Get
    End Property

    Public Sub Inisialisasi(TotalData As Integer, PageSize As Integer)

        If PageSize > 0 Then
            _TotalPages = Math.Ceiling(TotalData / PageSize)
        Else
            _TotalPages = 1
        End If

        If _TotalPages < 1 Then _TotalPages = 1

        _CurrentPage = 1
        UpdateUI()
    End Sub

    Private Sub UpdateUI()
        lblInfo.Text = $"Page {_CurrentPage} of {_TotalPages}"

        Dim isFirstPage As Boolean = (_CurrentPage = 1)
        Dim isLastPage As Boolean = (_CurrentPage = _TotalPages)

        ' Tombol Kiri (First & Prev) mati jika di halaman 1
        btnFirst.Enabled = Not isFirstPage
        btnPrev.Enabled = Not isFirstPage

        ' Tombol Kanan (Next & Last) mati jika di halaman terakhir
        btnNext.Enabled = Not isLastPage
        btnLast.Enabled = Not isLastPage
    End Sub


    '===============================
    '=     HANDLE EVENT TOMBOL     =
    '===============================
    Private Sub btnFirst_Click(sender As Object, e As EventArgs) Handles btnFirst.Click
        _CurrentPage = 1
        UpdateUI()
        RaiseEvent PageChanged(_CurrentPage)
    End Sub

    Private Sub btnPrev_Click(sender As Object, e As EventArgs) Handles btnPrev.Click
        If _CurrentPage > 1 Then
            _CurrentPage -= 1
            UpdateUI()
            RaiseEvent PageChanged(_CurrentPage)
        End If
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        If _CurrentPage < _TotalPages Then
            _CurrentPage += 1
            UpdateUI()
            RaiseEvent PageChanged(_CurrentPage)
        End If
    End Sub

    Private Sub btnLast_Click(sender As Object, e As EventArgs) Handles btnLast.Click
        _CurrentPage = _TotalPages
        UpdateUI()
        RaiseEvent PageChanged(_CurrentPage)
    End Sub

End Class

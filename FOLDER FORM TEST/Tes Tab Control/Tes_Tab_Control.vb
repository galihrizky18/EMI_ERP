

Public Class Tes_Tab_Control


    Private hoverIndex As Integer = -1

    Private Sub Tes_Tab_Control_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TabControl1.Padding = New Point(20, 5)

        TabControl1.SizeMode = TabSizeMode.Normal
        TabControl1.DrawMode = TabDrawMode.OwnerDrawFixed
    End Sub


    Private Sub TabControl1_DrawItem(sender As Object, e As DrawItemEventArgs) Handles TabControl1.DrawItem
        Dim g As Graphics = e.Graphics
        Dim bounds As Rectangle = e.Bounds
        Dim isSelected As Boolean = (e.Index = TabControl1.SelectedIndex)

        ' --- PENTING: BUKA KUNCI BATAS AREA (UNLOCK CLIPPING) ---
        ' Ini mengizinkan kita menggambar MELEWATI batas kotak tab asli.
        ' Tanpa ini, cat melebar kita akan terpotong dan garis hitam tetap muncul.
        g.SetClip(TabControl1.ClientRectangle)

        ' --- WARNA ---
        Dim bgHeader As Color = Color.White               ' Putih Bersih
        Dim activeColor As Color = Color.FromArgb(0, 102, 204) ' Biru
        Dim textColor As Color = If(isSelected, activeColor, Color.Gray)

        ' 1. TEKNIK CAT MELEBAR (Sekarang pasti berhasil karena Clip sudah dibuka)
        ' Kita buat area hapus yang sangat lebar ke kanan dan kiri (Inflate 15)
        ' Ini akan menimpa garis hitam/abu-abu di kanan tab yang aktif.
        Dim eraserRect As Rectangle = bounds
        eraserRect.Inflate(15, 5)

        Using bBrush As New SolidBrush(bgHeader)
            g.FillRectangle(bBrush, eraserRect)
        End Using

        ' 2. GAMBAR TEKS
        Dim sf As New StringFormat()
        sf.Alignment = StringAlignment.Center
        sf.LineAlignment = StringAlignment.Center

        Using font As New Font("Segoe UI", 10, FontStyle.Regular)
            Using brush As New SolidBrush(textColor)
                g.DrawString(TabControl1.TabPages(e.Index).Text, font, brush, bounds, sf)
            End Using
        End Using

        ' 3. GARIS BIRU BAWAH (Hanya Tab Aktif)
        If isSelected Then
            ' Gambar garis biru setebal 3px di bagian bawah
            Dim lineRect As New Rectangle(bounds.X, bounds.Bottom - 3, bounds.Width, 3)

            Using lineBrush As New SolidBrush(activeColor)
                g.FillRectangle(lineBrush, lineRect)
            End Using
        End If

        ' Reset Clip (Opsional, tapi praktik yang baik)
        g.ResetClip()

    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        If Me.Controls.Count > 0 Then
            Me.ActiveControl = Nothing ' Melepas fokus dari TabControl
        End If
    End Sub
End Class



Public Class TabControlNoBorder
    Inherits TabControl

    Public Sub New()
        Me.SetStyle(ControlStyles.UserPaint, True)
        Me.DrawMode = TabDrawMode.OwnerDrawFixed
        Me.ItemSize = New Size(120, 40)
        Me.SizeMode = TabSizeMode.Fixed
    End Sub

    Protected Overrides Sub WndProc(ByRef m As Message)
        Const WM_NCPAINT As Integer = &H85
        Const WM_PAINT As Integer = &HF

        ' Cegah Windows menggambar border bawaan
        If m.Msg = WM_NCPAINT Then
            Return
        End If

        MyBase.WndProc(m)
    End Sub
End Class

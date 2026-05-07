Public Class BadgeLabel
    Inherits Label

    Sub New()
        Me.DoubleBuffered = True
        Me.SetStyle(ControlStyles.SupportsTransparentBackColor, True)
        Me.SetStyle(ControlStyles.Opaque, True)
        Me.AutoSize = False
    End Sub

    Protected Overrides Sub OnPaintBackground(e As PaintEventArgs)
        If Me.Parent IsNot Nothing Then
            Dim g = e.Graphics
            g.TranslateTransform(-Me.Left, -Me.Top)
            g.TranslateTransform(Me.Left, Me.Top)
        End If
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        Dim rect As New Rectangle(0, 0, Width - 1, Height - 1)
        Dim path As New System.Drawing.Drawing2D.GraphicsPath()
        Dim radius As Integer = 10

        path.AddArc(rect.X, rect.Y, radius, radius, 180, 90)
        path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90)
        path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90)
        path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90)
        path.CloseFigure()

        e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

        ' Gambar rounded background
        e.Graphics.FillPath(New SolidBrush(BackColor), path)

        ' Gambar border tipis (opsional, bisa dihapus)
        e.Graphics.DrawPath(New Pen(Color.FromArgb(30, 0, 0, 0), 1), path)

        ' Gambar teks
        Dim sf As New StringFormat With {
            .Alignment = StringAlignment.Center,
            .LineAlignment = StringAlignment.Center
        }
        e.Graphics.DrawString(Text, Font, New SolidBrush(ForeColor),
            New RectangleF(0, 0, Width, Height), sf)
    End Sub
End Class

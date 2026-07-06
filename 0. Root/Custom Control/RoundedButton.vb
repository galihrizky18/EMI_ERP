Imports System.ComponentModel
Imports System.Drawing.Drawing2D

Public Class RoundedButton
	Inherits Button

	' Parameter untuk mengatur radius di panel design
	Private _borderRadius As Integer = 20

	' 1. Tambahkan Constructor (Sub New)
	Public Sub New()
		' Aktifkan Double Buffered untuk mengurangi flickering (berkedip)
		Me.DoubleBuffered = True

		' Opsional: Atur gaya default agar lebih modern
		Me.FlatStyle = FlatStyle.Flat
		Me.FlatAppearance.BorderSize = 0
	End Sub

	<Category("Appearance Custom")>
	Public Property BorderRadius As Integer
		Get
			Return _borderRadius
		End Get
		Set(value As Integer)
			If value >= 0 Then
				_borderRadius = value
				Me.Invalidate() ' Memaksa kontrol gambar ulang saat angka diubah
			End If
		End Set
	End Property

	' Event untuk menggambar ulang bentuk tombol
	Protected Overrides Sub OnPaint(pevent As PaintEventArgs)
		MyBase.OnPaint(pevent)

		Dim g As Graphics = pevent.Graphics
		g.SmoothingMode = SmoothingMode.AntiAlias ' Agar pinggiran halus/tidak pecah

		Dim rect As New Rectangle(0, 0, Me.Width, Me.Height)
		Dim diameter As Integer = _borderRadius * 2

		' Membuat jalur lengkungan
		Using path As New GraphicsPath()
			If diameter > 0 Then
				' Menggambar 4 sudut melengkung
				path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90)
				path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90)
				path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90)
				path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90)
				path.CloseFigure()

				' Potong tombol mengikuti jalur path
				Me.Region = New Region(path)

				' Gambar Border (Opsional)
				Using pen As New Pen(Me.FlatAppearance.BorderColor, 1)
					g.DrawPath(pen, path)
				End Using
			Else
				' Jika radius 0, kembali jadi kotak
				Me.Region = New Region(rect)
			End If
		End Using
	End Sub

	' Pastikan tombol tetap proporsional saat di-resize di design
	Protected Overrides Sub OnResize(e As EventArgs)
		MyBase.OnResize(e)
		Me.Invalidate()
	End Sub

End Class
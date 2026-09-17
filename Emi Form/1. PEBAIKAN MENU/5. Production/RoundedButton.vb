Imports System.ComponentModel
Imports System.Drawing.Drawing2D

Public Class RoundedButton
	Inherits Button

	' --- PROPERTI BORDER & RADIUS ---
	Private _borderRadius As Integer = 6

	Private _borderSize As Integer = 1

	' --- WARNA NORMAL (DASAR) ---
	Private _normalBackColor As Color = Color.White

	Private _normalForeColor As Color = Color.Black
	Private _normalBorderColor As Color = Color.FromArgb(215, 215, 215)
	Private _currentBorderColor As Color = _normalBorderColor

	' --- WARNA HOVER (SAAT DISOROT) ---
	Private _hoverBackColor As Color = Color.FromArgb(245, 245, 245)

	Private _hoverForeColor As Color = Color.Empty
	Private _hoverBorderColor As Color = Color.FromArgb(200, 200, 200)

	' --- WARNA PRESSED (SAAT DIKLIK) ---
	Private _pressedBackColor As Color = Color.FromArgb(230, 230, 230)

	Private _pressedForeColor As Color = Color.Empty
	Private _pressedBorderColor As Color = Color.FromArgb(180, 180, 180)

	' --- WARNA DISABLED (TIDAK AKTIF) ---
	Private _disableBackColor As Color = Color.FromArgb(235, 235, 235)

	Private _disableForeColor As Color = Color.DarkGray
	Private _disableBorderColor As Color = Color.FromArgb(215, 215, 215)

	' --- STATE TRACKER ---
	Private _isHovered As Boolean = False

	Private _isPressed As Boolean = False

	Public Sub New()
		Me.DoubleBuffered = True
		Me.FlatStyle = FlatStyle.Flat
		Me.FlatAppearance.BorderSize = 0
		Me.Cursor = Cursors.Hand
		MyBase.BackColor = _normalBackColor
		MyBase.ForeColor = _normalForeColor
	End Sub

#Region "PROPERTI SHADOWS (MENANGKAP PERUBAHAN DARI KODE/UI)"

	<Category("Appearance Custom"), Description("Warna latar belakang utama.")>
	Public Shadows Property BackColor As Color
		Get
			Return _normalBackColor
		End Get
		Set(value As Color)
			_normalBackColor = value
			UpdateButtonState()
		End Set
	End Property

	<Category("Appearance Custom"), Description("Warna teks utama.")>
	Public Shadows Property ForeColor As Color
		Get
			Return _normalForeColor
		End Get
		Set(value As Color)
			_normalForeColor = value
			UpdateButtonState()
		End Set
	End Property

	<Category("Appearance Custom"), Description("Warna garis border utama.")>
	Public Property BorderColor As Color
		Get
			Return _normalBorderColor
		End Get
		Set(value As Color)
			_normalBorderColor = value
			UpdateButtonState()
		End Set
	End Property

#End Region

#Region "PROPERTI KUSTOM ERP (HOVER, PRESSED, DISABLED, BORDER)"

	<Category("Appearance Custom")> Public Property BorderRadius As Integer
		Get
			Return _borderRadius
		End Get
		Set(value As Integer)
			_borderRadius = If(value < 0, 0, value)
			Me.Invalidate()
		End Set
	End Property

	<Category("Appearance Custom")> Public Property BorderSize As Integer
		Get
			Return _borderSize
		End Get
		Set(value As Integer)
			_borderSize = If(value < 0, 0, value)
			Me.Invalidate()
		End Set
	End Property

	<Category("Appearance Hover")> Public Property HoverBackColor As Color
		Get
			Return _hoverBackColor
		End Get
		Set(value As Color)
			_hoverBackColor = value
		End Set
	End Property

	<Category("Appearance Hover")> Public Property HoverForeColor As Color
		Get
			Return _hoverForeColor
		End Get
		Set(value As Color)
			_hoverForeColor = value
		End Set
	End Property

	<Category("Appearance Hover")> Public Property HoverBorderColor As Color
		Get
			Return _hoverBorderColor
		End Get
		Set(value As Color)
			_hoverBorderColor = value
		End Set
	End Property

	<Category("Appearance Pressed")> Public Property PressedBackColor As Color
		Get
			Return _pressedBackColor
		End Get
		Set(value As Color)
			_pressedBackColor = value
		End Set
	End Property

	<Category("Appearance Pressed")> Public Property PressedForeColor As Color
		Get
			Return _pressedForeColor
		End Get
		Set(value As Color)
			_pressedForeColor = value
		End Set
	End Property

	<Category("Appearance Pressed")> Public Property PressedBorderColor As Color
		Get
			Return _pressedBorderColor
		End Get
		Set(value As Color)
			_pressedBorderColor = value
		End Set
	End Property

	<Category("Appearance Disabled")> Public Property DisableBackColor As Color
		Get
			Return _disableBackColor
		End Get
		Set(value As Color)
			_disableBackColor = value
		End Set
	End Property

	<Category("Appearance Disabled")> Public Property DisableForeColor As Color
		Get
			Return _disableForeColor
		End Get
		Set(value As Color)
			_disableForeColor = value
		End Set
	End Property

	<Category("Appearance Disabled")> Public Property DisableBorderColor As Color
		Get
			Return _disableBorderColor
		End Get
		Set(value As Color)
			_disableBorderColor = value
		End Set
	End Property

#End Region

#Region "STATE MANAGEMENT LENGKAP"

	Private Sub UpdateButtonState()
		If Not Me.Enabled Then
			' State Disabled
			MyBase.BackColor = If(_disableBackColor = Color.Empty, _normalBackColor, _disableBackColor)
			MyBase.ForeColor = If(_disableForeColor = Color.Empty, _normalForeColor, _disableForeColor)
			_currentBorderColor = If(_disableBorderColor = Color.Empty, _normalBorderColor, _disableBorderColor)

		ElseIf _isPressed Then
			' State Click/Pressed
			MyBase.BackColor = If(_pressedBackColor = Color.Empty, _normalBackColor, _pressedBackColor)
			MyBase.ForeColor = If(_pressedForeColor = Color.Empty, _normalForeColor, _pressedForeColor)
			_currentBorderColor = If(_pressedBorderColor = Color.Empty, _normalBorderColor, _pressedBorderColor)

		ElseIf _isHovered Then
			' State Hover
			MyBase.BackColor = If(_hoverBackColor = Color.Empty, _normalBackColor, _hoverBackColor)
			MyBase.ForeColor = If(_hoverForeColor = Color.Empty, _normalForeColor, _hoverForeColor)
			_currentBorderColor = If(_hoverBorderColor = Color.Empty, _normalBorderColor, _hoverBorderColor)
		Else
			' State Normal
			MyBase.BackColor = _normalBackColor
			MyBase.ForeColor = _normalForeColor
			_currentBorderColor = _normalBorderColor
		End If

		Me.Invalidate()
	End Sub

	Protected Overrides Sub OnMouseEnter(e As EventArgs)
		MyBase.OnMouseEnter(e)
		_isHovered = True
		UpdateButtonState()
	End Sub

	Protected Overrides Sub OnMouseLeave(e As EventArgs)
		MyBase.OnMouseLeave(e)
		_isHovered = False
		_isPressed = False
		UpdateButtonState()
	End Sub

	Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
		MyBase.OnMouseDown(e)
		If e.Button = MouseButtons.Left Then
			_isPressed = True
			UpdateButtonState()
		End If
	End Sub

	Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
		MyBase.OnMouseUp(e)
		If e.Button = MouseButtons.Left Then
			_isPressed = False
			UpdateButtonState()
		End If
	End Sub

	Protected Overrides Sub OnEnabledChanged(e As EventArgs)
		MyBase.OnEnabledChanged(e)
		UpdateButtonState()
	End Sub

#End Region

#Region "CUSTOM PAINTING ENGINE"

	Private Function GetFigurePath(rect As RectangleF, radius As Single) As GraphicsPath
		Dim path As New GraphicsPath()
		Dim diameter As Single = radius * 2.0F

		If diameter <= 0 Then
			path.AddRectangle(rect)
			path.CloseFigure()
			Return path
		End If

		If diameter > rect.Width Then diameter = rect.Width
		If diameter > rect.Height Then diameter = rect.Height

		path.StartFigure()
		path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90)
		path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90)
		path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90)
		path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90)
		path.CloseFigure()
		Return path
	End Function

	Protected Overrides Sub OnPaint(pevent As PaintEventArgs)
		MyBase.OnPaint(pevent)

		Dim g As Graphics = pevent.Graphics
		g.SmoothingMode = SmoothingMode.AntiAlias

		If _borderRadius > 0 Then
			' 1. Set Region tombol melengkung
			Dim rectSurface As New RectangleF(0, 0, Me.Width, Me.Height)
			Using pathSurface As GraphicsPath = GetFigurePath(rectSurface, _borderRadius)
				Me.Region = New Region(pathSurface)
			End Using

			' 2. Gambar Border
			If _borderSize > 0 AndAlso _currentBorderColor <> Color.Transparent AndAlso _currentBorderColor <> Color.Empty Then
				Dim halfBorder As Single = _borderSize / 2.0F
				Dim rectBorder As New RectangleF(halfBorder, halfBorder, Me.Width - _borderSize, Me.Height - _borderSize)
				Dim radiusBorder As Single = Math.Max(0.1F, _borderRadius - halfBorder)

				Using pathBorder As GraphicsPath = GetFigurePath(rectBorder, radiusBorder)
					Using penBorder As New Pen(_currentBorderColor, _borderSize)
						g.DrawPath(penBorder, pathBorder)
					End Using
				End Using
			End If
		Else
			Me.Region = New Region(New RectangleF(0, 0, Me.Width, Me.Height))
			If _borderSize > 0 AndAlso _currentBorderColor <> Color.Transparent AndAlso _currentBorderColor <> Color.Empty Then
				Using penBorder As New Pen(_currentBorderColor, _borderSize)
					g.DrawRectangle(penBorder, 0, 0, Me.Width - _borderSize, Me.Height - _borderSize)
				End Using
			End If
		End If
	End Sub

	Protected Overrides Sub OnResize(e As EventArgs)
		MyBase.OnResize(e)
		Me.Invalidate()
	End Sub

#End Region

End Class
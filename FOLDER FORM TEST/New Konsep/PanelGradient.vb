Imports System.ComponentModel
Imports System.Drawing.Drawing2D

Public Class PanelGradient
    Inherits Panel

    Private _colorTop As Color = Color.White
    Private _colorBottom As Color = Color.Blue

    <Category("Gradient Colors")>
    Public Property ColorTop() As Color
        Get
            Return _colorTop
        End Get
        Set(ByVal value As Color)
            _colorTop = value
            Me.Invalidate()
        End Set
    End Property

    <Category("Gradient Colors")>
    Public Property ColorBottom() As Color
        Get
            Return _colorBottom
        End Get
        Set(ByVal value As Color)
            _colorBottom = value
            Me.Invalidate()
        End Set
    End Property

    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        Dim brush As LinearGradientBrush = New LinearGradientBrush(Me.ClientRectangle, Me.ColorTop, Me.ColorBottom, 90.0F)
        e.Graphics.FillRectangle(brush, Me.ClientRectangle)
        MyBase.OnPaint(e)
    End Sub
End Class


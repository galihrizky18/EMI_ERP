Imports System
Imports System.Collections
Imports System.ComponentModel
Imports System.Drawing
Imports System.Data
Imports System.Windows.Forms


Namespace CustomControl


    Public Class PanelGradient
        Inherits System.Windows.Forms.Panel

        Private m_color1 As Color = Color.LightGreen
        Private m_color2 As Color = Color.DarkBlue
        Private m_color1Transparent As Integer = 64
        Private m_color2Transparent As Integer = 64

        Public Property cuteColor1() As Color
            Get
                Return m_color1
            End Get
            Set(ByVal value As Color)
                m_color1 = value
                Invalidate()
            End Set
        End Property

        Public Property cuteColor2() As Color
            Get
                Return m_color2
            End Get
            Set(ByVal value As Color)
                m_color2 = value
                Invalidate()
            End Set
        End Property

        Public Property cuteTransparent1() As Integer
            Get
                Return m_color1Transparent
            End Get
            Set(ByVal value As Integer)
                m_color1Transparent = value
                Invalidate()
            End Set
        End Property

        Public Property cuteTransparent2() As Integer
            Get
                Return m_color2Transparent
            End Get
            Set(ByVal value As Integer)
                m_color2Transparent = value
                Invalidate()
            End Set
        End Property

        Public Sub New()
        End Sub

        Protected Overrides Sub OnPaint(ByVal pe As PaintEventArgs)
            MyBase.OnPaint(pe)
            Dim c1 As Color = Color.FromArgb(m_color1Transparent, m_color1)
            Dim c2 As Color = Color.FromArgb(m_color2Transparent, m_color2)
            Dim b As Brush = New System.Drawing.Drawing2D.LinearGradientBrush(ClientRectangle, c1, c2, 10)
            pe.Graphics.FillRectangle(b, ClientRectangle)
            b.Dispose()
        End Sub

        Private Sub InitializeComponent()
            Me.SuspendLayout()
            Me.ResumeLayout(False)

        End Sub
    End Class

End Namespace


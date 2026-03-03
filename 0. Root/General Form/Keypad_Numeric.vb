Imports System.Media

Public Class Keypad_Numeric
    Inherits UserControl

    Private _value As String = "0"
    Private ReadOnly clickSound As SoundPlayer

    Private ReadOnly MinFontSize As Single = 8
    Private ReadOnly MaxFontSize As Single = 24

    Public Event ValueChanged(sender As Object, e As EventArgs)

    Public Sub New()
        InitializeComponent()
        clickSound = New SoundPlayer(IO.Path.Combine(Application.StartupPath, "click.wav"))
    End Sub

    Private Sub Keypad_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Size = Me.MinimumSize
        Me.BorderStyle = BorderStyle.FixedSingle

        Lb_Value.AutoSize = False
        Lb_Value.TextAlign = ContentAlignment.MiddleRight
        Lb_Value.Text = _value
        UpdateLabelFontSize()
    End Sub

    Public Property Value As String
        Get
            Return _value
        End Get
        Set(ByVal val As String)
            Dim newValue As String = "0"

            If Not String.IsNullOrEmpty(val) Then
                Dim number As Decimal
                If Decimal.TryParse(val, number) Then
                    If number = 0D Then
                        newValue = "0"
                    Else
                        newValue = number.ToString("0.####")
                    End If
                Else
                    newValue = val
                End If
            End If

            _value = newValue
            Lb_Value.Text = _value
            UpdateLabelFontSize()
            RaiseEvent ValueChanged(Me, EventArgs.Empty)
        End Set
    End Property


    Public Sub ClearInput()
        _value = "0"
        Lb_Value.Text = _value
        UpdateLabelFontSize()
        RaiseEvent ValueChanged(Me, EventArgs.Empty)
    End Sub

    Private Sub BtnNumber_Click(sender As Object, e As EventArgs) _
    Handles Btn_Zero.Click, Btn_ZeroDouble.Click, Btn_One.Click, Btn_Two.Click, Btn_Three.Click,
            Btn_Four.Click, Btn_Five.Click, Btn_Six.Click, Btn_Seven.Click,
            Btn_Eight.Click, Btn_Nine.Click

        'clickSound.Play()

        Dim btn As Button = CType(sender, Button)
        Dim digit As String = btn.Text

        If btn Is Btn_ZeroDouble Then
            If _value = "0" Then Return

            If _value.Contains(".") Then
                Dim afterDot As String = _value.Split("."c)(1)
                If afterDot.Length >= 4 Then Return
                If afterDot.Length = 3 Then
                    _value &= "0"
                    GoTo UpdateLabel
                End If
            End If

            _value &= "00"
            GoTo UpdateLabel
        End If

        If _value.Contains(".") Then
            Dim afterDot As String = _value.Split("."c)(1)
            If afterDot.Length >= 4 Then Return
        End If

        If _value = "0" AndAlso Not _value.Contains(".") Then
            _value = digit
        Else
            _value &= digit
        End If

UpdateLabel:
        Lb_Value.Text = _value
        UpdateLabelFontSize()
        RaiseEvent ValueChanged(Me, EventArgs.Empty)
    End Sub


    Private Sub Btn_Dot_Click(sender As Object, e As EventArgs) Handles Btn_Dot.Click
        'clickSound.Play()

        If _value.Contains(".") Then Return

        If String.IsNullOrEmpty(_value) OrElse _value = "0" Then
            _value = "0."
        Else
            _value &= "."
        End If

        Lb_Value.Text = _value
        UpdateLabelFontSize()
        RaiseEvent ValueChanged(Me, EventArgs.Empty)
    End Sub

    Private Sub Btn_Backspace_Click(sender As Object, e As EventArgs) Handles Btn_Backspace.Click
        'clickSound.Play()

        If _value.Length > 0 Then
            _value = _value.Substring(0, _value.Length - 1)
        End If
        If String.IsNullOrEmpty(_value) Then _value = "0"

        Lb_Value.Text = _value
        UpdateLabelFontSize()
        RaiseEvent ValueChanged(Me, EventArgs.Empty)
    End Sub

    Private Sub UpdateLabelFontSize()
        If Lb_Value.Text = "" Then Exit Sub

        Dim g As Graphics = Lb_Value.CreateGraphics()
        Dim fontSize As Single = MaxFontSize

        Do
            Dim testFont As New Font(Lb_Value.Font.FontFamily, fontSize, Lb_Value.Font.Style)
            Dim size As SizeF = g.MeasureString(Lb_Value.Text, testFont)

            If size.Width <= Lb_Value.Width Or fontSize <= MinFontSize Then
                Lb_Value.Font = testFont
                Exit Do
            End If

            fontSize -= 0.5F
        Loop
    End Sub

    Private Sub Btn_Clear_Click(sender As Object, e As EventArgs) Handles Btn_Clear.Click
        ClearInput()
    End Sub
End Class

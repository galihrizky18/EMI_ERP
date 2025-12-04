Public Class Keypad_PIN
    Private _pinValue As String = ""
    Private _isValueVisible As Boolean = False
    Private Const MaxLength As Integer = 6

    Public Event ValueChanged(ByVal newValue As String)
    Public Event VisibilityToggled(ByVal isVisible As Boolean)

    Public ReadOnly Property Value As String
        Get
            Return _pinValue
        End Get
    End Property

    Public Property IsPasswordVisible As Boolean
        Get
            Return _isValueVisible
        End Get
        Set(value As Boolean)
            _isValueVisible = value
            UpdateVisibilityButton()
        End Set
    End Property

    Private Sub UpdateLabel()
        RaiseEvent ValueChanged(_pinValue)
    End Sub

    Private Sub KeypadButton_Click(sender As Object, e As EventArgs) _
        Handles Btn_0.Click, Btn_1.Click, Btn_2.Click, Btn_3.Click, Btn_4.Click,
                Btn_5.Click, Btn_6.Click, Btn_7.Click, Btn_8.Click, Btn_9.Click
        If _pinValue.Length >= MaxLength Then
            Exit Sub
        End If
        Dim btn As Button = CType(sender, Button)
        _pinValue &= btn.Text
        UpdateLabel()
    End Sub

    Private Sub Btn_Backspace_Click(sender As Object, e As EventArgs) Handles Btn_Backspace.Click
        If _pinValue.Length > 0 Then
            _pinValue = _pinValue.Substring(0, _pinValue.Length - 1)
            UpdateLabel()
        End If
    End Sub

    Public Sub Reset()
        _pinValue = ""
        UpdateLabel()
    End Sub

    Private Sub Btn_Toggle_Visibility_Click(sender As Object, e As EventArgs) Handles Btn_Toggle_Visibility.Click
        _isValueVisible = Not _isValueVisible
        UpdateVisibilityButton()
        RaiseEvent VisibilityToggled(_isValueVisible)
    End Sub

    Private Sub UpdateVisibilityButton()
        Btn_Toggle_Visibility.Text = If(_isValueVisible, "🔓", "🔒")

        Btn_Toggle_Visibility.BackColor = If(_isValueVisible, Color.LightGreen, Color.LightGray)
    End Sub

    Private Sub Keypad_PIN_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _pinValue = ""
        _isValueVisible = False

        Me.TabStop = False
        For Each ctrl As Control In Me.Controls
            If TypeOf ctrl Is Button Then
                CType(ctrl, Button).TabStop = False
            End If
        Next

        UpdateVisibilityButton()
        UpdateLabel()
    End Sub
End Class
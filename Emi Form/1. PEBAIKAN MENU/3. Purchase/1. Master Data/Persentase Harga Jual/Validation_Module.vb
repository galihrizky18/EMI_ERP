Module Validation_Module

    '====================================================================
    ' Validation Module Documentation
    '====================================================================
    '
    ' 1. Function Return Convention
    '    - All validation functions return:
    '        ""  → validation passed (no error)
    '        "…" → error message string (validation failed)
    '
    ' 2. Naming Conventions
    '    - KPR_[FX] : Key Press Rule
    '        • Used in KeyPress event handlers
    '        • Validates keystrokes in real-time while typing
    '
    '    - IVR_[FX] : Input Value Rule
    '        • Used in Validating events or before processing input
    '        • Validates the final value of the control
    '
    '    - IVP_[FX] : Input Value Parser
    '        • Used to parse and validate input values
    '
    '    - VIV : Validate Input Value
    '        • Helper to display a message box if validation fails
    '        • Returns True if validation fails (error present), otherwise False
    '
    ' 3. Usage Examples
    '    a) KPR_[FX] — KeyPress validation
    '       Private Sub [COMPONENT]_KeyPress(sender As Object, e As KeyPressEventArgs) Handles [COMPONENT].KeyPress
    '           KPR_Percentage(CType(sender, TextBox), e)
    '       End Sub
    '
    '    b) IVR_[FX] — Final value validation
    '       If VIV(IVR_Email(TxtEmail.Text)) Then
    '           Return  ' stop processing if validation failed
    '       End If
    '
    ' 4. IVR_Combine
    '    - Purpose:
    '        • Chains multiple validation rules together for the same input
    '        • Runs rules in order and returns the first error found
    '    - Signature:
    '        Function IVR_Combine(value As String, ParamArray rules() As Func(Of String, String)) As String
    '    - Behavior:
    '        • Iterates through each validation rule
    '        • If a rule returns an error (non-empty string), stop and return that error
    '        • If all rules pass, return ""
    '    - Example Usage:
    '        If VIV(IVR_Combine(TxtPersentase.Text,
    '                           Function(v) IVR_Required(v, "Persentase"),
    '                           Function(v) IVR_Percentage(v, "Persentase"))) Then Exit Sub
    '
    '====================================================================


    'For percentage input (0-100 only)
    Public Sub KPR_Percentage(txt As TextBox, e As KeyPressEventArgs)
        If Char.IsControl(e.KeyChar) Then
            Return
        End If

        If Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True
            Return
        End If

        Dim futureText As String = txt.Text.Substring(0, txt.SelectionStart) & e.KeyChar & txt.Text.Substring(txt.SelectionStart + txt.SelectionLength)

        If futureText.Length > 1 AndAlso futureText.StartsWith("0") Then
            e.Handled = True
            Return
        End If

        Dim value As Integer
        If Integer.TryParse(futureText, value) Then
            If value < 0 OrElse value > 100 Then
                e.Handled = True
            End If
        End If
    End Sub

    'For email input (letters, digits, and specific special characters)
    Public Sub KPR_Email(txt As TextBox, e As KeyPressEventArgs)
        If Char.IsControl(e.KeyChar) Then
            Return
        End If

        Dim allowed As String = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789@._-"
        If Not allowed.Contains(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    'For integer input (whole numbers only)
    Public Sub KPR_Integer(txt As TextBox, e As KeyPressEventArgs)
        If Char.IsControl(e.KeyChar) Then
            Return
        End If

        If Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    'For decimal input (numbers with a single decimal point)
    Public Sub KPR_Decimal(txt As TextBox, e As KeyPressEventArgs)
        If Char.IsControl(e.KeyChar) Then
            Return
        End If

        If Not (Char.IsDigit(e.KeyChar) OrElse e.KeyChar = "."c) Then
            e.Handled = True
        End If

        If e.KeyChar = "."c AndAlso txt.Text.Contains(".") Then
            e.Handled = True
        End If
    End Sub

    'For alphabetic input (letters and spaces only)
    Public Sub KPR_Alphabet(txt As TextBox, e As KeyPressEventArgs)
        If Char.IsControl(e.KeyChar) Then
            Return
        End If

        If Not (Char.IsLetter(e.KeyChar) OrElse e.KeyChar = " "c) Then
            e.Handled = True
        End If
    End Sub

    'For percentage input validation (0-100)
    Public Function IVR_Percentage(value As String, fieldName As String) As String
        Dim num As Integer

        If Not Integer.TryParse(value, num) Then
            Return fieldName & " harus berupa angka."
        End If

        If num < 0 OrElse num > 100 Then
            Return fieldName & " harus di antara 0 - 100."
        End If

        Return ""
    End Function

    'For required field validation
    Public Function IVR_Required(value As String, fieldName As String) As String
        If String.IsNullOrWhiteSpace(value) Then
            Return fieldName & " tidak boleh kosong."
        End If
        Return ""
    End Function

    'For optional field validation with custom validator
    Public Function IVR_Optional(value As String, validator As Func(Of String, String)) As String
        If String.IsNullOrWhiteSpace(value) Then
            Return ""
        End If
        Return validator(value)
    End Function

    'For combine multiple validators
    Public Function IVR_Combine(value As String, ParamArray validators() As Func(Of String, String)) As String
        For Each validator In validators
            Dim msg = validator(value)
            If msg <> "" Then
                Return msg
            End If
        Next
        Return ""
    End Function

    'For percentage input parser
    Public Function IVP_Percentage(value As String, delta As Integer) As Integer
        Dim num As Integer
        If Not Integer.TryParse(value, num) Then
            num = 0
        End If
        num += delta
        If num < 0 Then num = 0
        If num > 100 Then num = 100
        Return num
    End Function

    'For check if validation rules
    Public Function VIV(msg As String) As Boolean
        If msg <> "" Then
            MessageBox.Show(msg)
            Return True
        End If
        Return False
    End Function
End Module

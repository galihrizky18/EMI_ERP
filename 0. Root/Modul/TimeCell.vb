Imports System.ComponentModel

Public Class TimeCell
    Inherits DataGridViewTextBoxCell

    Public Overrides Sub InitializeEditingControl(rowIndex As Integer, initialFormattedValue As Object, dataGridViewCellStyle As DataGridViewCellStyle)
        MyBase.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle)

        Dim ctl As TimeEditingControl = CType(DataGridView.EditingControl, TimeEditingControl)

        Try
            If Me.Value IsNot Nothing AndAlso Not Convert.IsDBNull(Me.Value) Then
                ctl.Value = Convert.ToDateTime(Me.Value)
            Else
                ctl.Value = DateTime.Now
            End If
        Catch
            ctl.Value = DateTime.Now
        End Try
    End Sub

    '================================
    '=     Setting Format Waktu     = 
    '================================
    Protected Overrides Function GetFormattedValue(
        value As Object,
        rowIndex As Integer,
        ByRef cellStyle As DataGridViewCellStyle,
        valueTypeConverter As TypeConverter,
        formattedValueTypeConverter As TypeConverter,
        context As DataGridViewDataErrorContexts
    ) As Object
        If TypeOf value Is DateTime Then
            Dim waktu As DateTime = CType(value, DateTime)
            Return waktu.ToString("HH:mm")
        End If

        Return MyBase.GetFormattedValue(value, rowIndex, cellStyle, valueTypeConverter, formattedValueTypeConverter, context)
    End Function


    Public Overrides ReadOnly Property EditType As Type
        Get
            Return GetType(TimeEditingControl)
        End Get
    End Property

    Public Overrides ReadOnly Property ValueType As Type
        Get
            Return GetType(DateTime)
        End Get
    End Property

    Public Overrides ReadOnly Property DefaultNewRowValue As Object
        Get
            Return DateTime.Now
        End Get
    End Property
End Class

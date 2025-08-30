Imports System.Windows.Forms

Public Class CalendarCell
    Inherits DataGridViewTextBoxCell

    Public Overrides Sub InitializeEditingControl(rowIndex As Integer, initialFormattedValue As Object, dataGridViewCellStyle As DataGridViewCellStyle)
        MyBase.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle)
        Dim ctl As CalendarEditingControl = CType(DataGridView.EditingControl, CalendarEditingControl)
        Try
            If Me.Value IsNot Nothing Then
                ctl.Value = DateTime.Parse(Me.Value.ToString())
            Else
                ctl.Value = DateTime.Now
            End If
        Catch
            ctl.Value = DateTime.Now
        End Try

    End Sub

    Public Overrides ReadOnly Property EditType As Type
        Get
            Return GetType(CalendarEditingControl)
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

Public Class CalendarEditingControl
    Inherits DateTimePicker
    Implements IDataGridViewEditingControl

    Private dataGridViewControl As DataGridView
    Private valueChanged As Boolean = False
    Private rowIndexNum As Integer

    Public Property EditingControlFormattedValue As Object Implements IDataGridViewEditingControl.EditingControlFormattedValue
        Get
            Return Me.Value.ToShortDateString()
        End Get
        Set(value As Object)
            Me.Value = DateTime.Parse(value.ToString())
        End Set
    End Property

    Public Function GetEditingControlFormattedValue(context As DataGridViewDataErrorContexts) As Object Implements IDataGridViewEditingControl.GetEditingControlFormattedValue
        Return Me.Value.ToShortDateString()
    End Function

    Public Sub ApplyCellStyleToEditingControl(cellStyle As DataGridViewCellStyle) Implements IDataGridViewEditingControl.ApplyCellStyleToEditingControl
        Me.Font = cellStyle.Font
        Me.CalendarForeColor = cellStyle.ForeColor
        Me.CalendarMonthBackground = cellStyle.BackColor
    End Sub

    Public Property EditingControlRowIndex As Integer Implements IDataGridViewEditingControl.EditingControlRowIndex
        Get
            Return rowIndexNum
        End Get
        Set(value As Integer)
            rowIndexNum = value
        End Set
    End Property

    Public Function EditingControlWantsInputKey(key As Keys, dataGridViewWantsInputKey As Boolean) As Boolean Implements IDataGridViewEditingControl.EditingControlWantsInputKey
        Return True
    End Function

    Public Sub PrepareEditingControlForEdit(selectAll As Boolean) Implements IDataGridViewEditingControl.PrepareEditingControlForEdit
    End Sub

    Public ReadOnly Property RepositionEditingControlOnValueChange As Boolean Implements IDataGridViewEditingControl.RepositionEditingControlOnValueChange
        Get
            Return False
        End Get
    End Property

    Public Property EditingControlDataGridView As DataGridView Implements IDataGridViewEditingControl.EditingControlDataGridView
        Get
            Return dataGridViewControl
        End Get
        Set(value As DataGridView)
            dataGridViewControl = value
        End Set
    End Property

    Public Property EditingControlValueChanged As Boolean Implements IDataGridViewEditingControl.EditingControlValueChanged
        Get
            Return valueChanged
        End Get
        Set(value As Boolean)
            valueChanged = value
        End Set
    End Property

    Public ReadOnly Property EditingControlCursor As Cursor Implements IDataGridViewEditingControl.EditingPanelCursor
        Get
            Return MyBase.Cursor
        End Get
    End Property

    Protected Overrides Sub OnValueChanged(eventargs As EventArgs)
        valueChanged = True
        Me.EditingControlDataGridView.NotifyCurrentCellDirty(True)
        MyBase.OnValueChanged(eventargs)
    End Sub
End Class

Public Class Binding_Akun_WorkCenter
    Private Sub Binding_Akun_WorkCenter_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Initial_ListView()
        Isi_ListView()
    End Sub


    Private Sub Initial_ListView()

        Lv_DetailAkun.Columns.Add("", 30, HorizontalAlignment.Center)
        Lv_DetailAkun.Columns.Add("Kode Akun", 150, HorizontalAlignment.Center)
        Lv_DetailAkun.Columns.Add("Nama Akun", 150, HorizontalAlignment.Center)
        Lv_DetailAkun.View = View.Details

    End Sub

    Private Sub Isi_ListView()
        Dim LV As New ListViewItem()
        LV = Lv_DetailAkun.Items.Add("")
        LV.SubItems.Add("Kolom 1")
        LV.SubItems.Add("Kolom 2")

    End Sub

    Private Sub Lv_DetailAkun_DrawColumnHeader(sender As Object, e As DrawListViewColumnHeaderEventArgs) Handles Lv_DetailAkun.DrawColumnHeader
        If e.ColumnIndex = 0 Then
            e.DrawBackground()

            Dim value As Boolean = False

            Try
                value = Convert.ToBoolean(e.Header.Tag)
            Catch ex As Exception
                ' Optional: Handle or log the error
            End Try

            ' Draw the checkbox in the header
            CheckBoxRenderer.DrawCheckBox(e.Graphics, New Point(e.Bounds.Left + 4, e.Bounds.Top + 4),
                                      If(value, System.Windows.Forms.VisualStyles.CheckBoxState.CheckedNormal,
                                         System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal))
        Else
            ' Draw default for other columns
            e.DrawDefault = True
        End If
    End Sub

    Private Sub Lv_DetailAkun_DrawItem(sender As Object, e As DrawListViewItemEventArgs) Handles Lv_DetailAkun.DrawItem
        e.DrawDefault = True
    End Sub

    Private Sub Lv_DetailAkun_DrawSubItem(sender As Object, e As DrawListViewSubItemEventArgs) Handles Lv_DetailAkun.DrawSubItem
        e.DrawDefault = True
    End Sub

    Private Sub Lv_DetailAkun_ColumnClick(sender As Object, e As ColumnClickEventArgs) Handles Lv_DetailAkun.ColumnClick
        If e.Column = 0 Then
            Dim value As Boolean = False

            Try
                value = Convert.ToBoolean(Lv_DetailAkun.Columns(e.Column).Tag)
            Catch ex As Exception
            End Try

            Dim newValue As Boolean = Not value
            Lv_DetailAkun.Columns(e.Column).Tag = newValue

            For Each item As ListViewItem In Lv_DetailAkun.Items
                item.Checked = newValue
            Next

            Lv_DetailAkun.Invalidate()
        End If
    End Sub


End Class
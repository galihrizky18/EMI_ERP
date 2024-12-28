Imports System.Reflection

Public Class Form_Awal

    Dim allForms As New List(Of Form)

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click


        Dim formToOpen As Form

        Dim Menu As String = "Form_Ke2"



        '' Menutup semua form dengan nama sesuai Menu
        'For Each frm As Form In Application.OpenForms
        '    If frm.Name = Menu Then
        '        frm.Close()  ' Menutup form yang terbuka dengan nama yang sama
        '    End If
        'Next

        '' Menghapus semua referensi ke form lama
        'GC.Collect()  ' Memaksa garbage collector untuk membersihkan form yang sudah ditutup
        'GC.WaitForPendingFinalizers()  '

        'Dim formType As Type = Type.GetType($"{Me.GetType().Namespace}.{Menu}")
        'formToOpen = CType(Activator.CreateInstance(formType), Form)

        'formToOpen.Show()

        'Form_Ke2.Show()

        ' Mendapatkan semua tipe form yang ada dalam proyek

        Dim formTypes = Assembly.GetExecutingAssembly().GetTypes().Where(Function(t) t.IsSubclassOf(GetType(Form)))

        For Each formType In formTypes
            Dim form As Form = CType(Activator.CreateInstance(formType), Form)
            allForms.Add(form)
        Next

        ' Menampilkan semua form yang ditemukan
        For Each form As Form In allForms
            MessageBox.Show("Form ditemukan: " & form.Name)
        Next

        Me.Close()
    End Sub

    Private Sub Form_Awal_Load(sender As Object, e As EventArgs) Handles Me.Load
        allForms.Clear()
    End Sub
End Class
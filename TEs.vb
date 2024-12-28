Imports System.Globalization
Imports System.Text.RegularExpressions

Public Class Tes


    Private Sub Tes_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub Tes_Load(sender As Object, e As EventArgs) Handles Me.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub


    Private Sub TextBox2_Leave(sender As Object, e As EventArgs) Handles TextBox2.Leave
        If Not TextBox2.Text.Length = 0 Then
            Try

                Dim culture As CultureInfo = CultureInfo.CurrentCulture
                Dim input As String = TextBox2.Text.Replace(culture.NumberFormat.CurrencySymbol, "").Replace(",", "").Trim()

                If IsNumeric(input) Then
                    Dim value As Decimal = Convert.ToDouble(input)
                    TextBox2.Text = culture.NumberFormat.CurrencySymbol & " " & value.ToString("N2", culture) ' Tambahkan jarak dengan format
                Else
                    MessageBox.Show("Invalid input!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    TextBox2.Text = ""
                End If

            Catch ex As Exception
                MessageBox.Show("Terjadi Kesalahan saat Convert", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                TextBox2.Text = ""
                Exit Sub
            End Try
        End If
    End Sub

    Private Sub TextBox2_Enter(sender As Object, e As EventArgs) Handles TextBox2.Enter
        If Not TextBox2.Text.Length = 0 Then
            Try
                Dim culture As CultureInfo = CultureInfo.CurrentCulture
                TextBox2.Text = TextBox2.Text.Replace(culture.NumberFormat.CurrencySymbol, "").Trim()

                Dim cleanedStr As String = HilangkanTanda(TextBox2.Text) ' Menghapus titik
                Dim nilai As Decimal = Decimal.Parse(Val(cleanedStr))
                TextBox2.Text = nilai
            Catch ex As Exception
                MessageBox.Show("Terjadi Kesalahan saat Convert", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                TextBox2.Text = ""
                Exit Sub
            End Try
        End If
    End Sub


    '=========================================
    Private Sub Txt_Fix_Leave(sender As Object, e As EventArgs) Handles Txt_Fix.Leave
        If Not Txt_Fix.Text.Length = 0 Then

            Try

                Dim culture As CultureInfo = CultureInfo.CurrentCulture
                'Dim input As String = Txt_Fix.Text.Replace(culture.NumberFormat.CurrencySymbol, "").Replace(",", "").Trim()
                Dim input As String = HilangkanTanda(Txt_Fix.Text)

                If Txt_Fix.Text.Contains(",") Then
                    MessageBox.Show("Kuantity Tidak Boleh Koma, Ganti dengan Titik", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Txt_Fix.Text = ""
                    Exit Sub
                End If

                If IsNumeric(input) Then

                    Dim value As Decimal = Convert.ToDouble(input)
                    'Txt_Fix.Text = culture.NumberFormat.CurrencySymbol & " " & value.ToString("N2", culture) ' Jika Dengan Simbol Mata Uang
                    Txt_Fix.Text = value.ToString("N2", culture) ' Jika Dengan Simbol Mata Uang
                Else
                    MessageBox.Show("Kuantity Harus Berupa Angka!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Txt_Fix.Text = ""
                End If

            Catch ex As Exception
                MessageBox.Show("Terjadi Kesalahan saat Convert", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Txt_Fix.Text = ""
                Exit Sub
            End Try
        End If
    End Sub

    Private Sub Txt_Fix_Enter(sender As Object, e As EventArgs) Handles Txt_Fix.Enter
        If Not Txt_Fix.Text.Length = 0 Then
            Try

                Dim culture As CultureInfo = CultureInfo.CurrentCulture
                'Txt_Fix.Text = Txt_Fix.Text.Replace(culture.NumberFormat.CurrencySymbol, "").Trim() ' Jika Dengan Simbol Mata Uang

                Dim cleanedStr As String = HilangkanTanda(Txt_Fix.Text).Trim() ' Menghapus titik
                Dim nilai As Decimal = Decimal.Parse(Val(cleanedStr))
                Txt_Fix.Text = nilai

            Catch ex As Exception
                MessageBox.Show("Terjadi Kesalahan saat Convert", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Txt_Fix.Text = ""
                Exit Sub
            End Try
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        'Dim formType As Type = Type.GetType("ERP_EMI.TesLoading")
        'Dim instanceDefault As Form = CType(Activator.CreateInstance(formType), Form)

        'instanceDefault.Show()

        TesLoading.Show()

    End Sub
End Class

Public Class FMenuDevFix

	Private Sub FMenuDevFix_Activated(sender As Object, e As EventArgs) Handles Me.Activated
		My.Application.ChangeCulture("en-us")
		My.Application.ChangeUICulture("en-us")
	End Sub

	Private Sub FMenuDevFix_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		My.Application.ChangeCulture("en-us")
		My.Application.ChangeUICulture("en-us")

		'Automation_Forecast_Release()

		Try
			OpenConn()

			Using Dr = OpenTrans("select dateadd(hh, " & selisihjam & ", getdate()) as Jam")
				If Dr.Read Then
					ToolStripStatusLabel3.Text = Format(Dr("jam"), "yyyy-MM-dd HH:mm:ss")
				End If
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		ToolStripStatusLabel1.Text = "Login : " & UserID
		ToolStripStatusLabel4.Text = "Lokasi : " & Lokasi

		Timer1_Tick(Me, Nothing)

		Dim C As Control

		For Each C In Me.Controls
			If TypeOf C Is MdiClient Then
				C.BackColor = Color.LightGray
				Exit For
			End If
		Next

		C = Nothing
	End Sub

	Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
		ToolStripStatusLabel3.Text = Format(DateAdd(DateInterval.Second, 1, CDate(ToolStripStatusLabel3.Text)), "yyyy-MM-dd HH:mm:ss")

	End Sub

	Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
		Try
			OpenConn()

			Using Dr = OpenTrans("select dateadd(hh, " & selisihjam & ", getdate()) as Jam")
				If Dr.Read Then
					'ToolStripStatusLabel3.Text = Format(Dr("jam"), "dd MMM yyyy HH:mm:ss")
					ToolStripStatusLabel3.Text = Format(Dr("jam"), "yyyy-MM-dd HH:mm:ss")
				End If
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	'=====================
	'=     LOAD MENU     =
	'=====================
	Private Sub ProductionResultToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ProductionResultToolStripMenuItem.Click
		EMI_Display_Production_Result.StartPosition = FormStartPosition.CenterScreen

		EMI_Display_Production_Result.MdiParent = Me
		EMI_Display_Production_Result.Show()
		EMI_Display_Production_Result.Focus()
	End Sub

	Private Sub PRDeptToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PRDeptToolStripMenuItem.Click
		N_EMI_Purchase_Requisition_Barang_Lain_Departement.StartPosition = FormStartPosition.CenterScreen

		N_EMI_Purchase_Requisition_Barang_Lain_Departement.MdiParent = Me
		N_EMI_Purchase_Requisition_Barang_Lain_Departement.Show()
		N_EMI_Purchase_Requisition_Barang_Lain_Departement.Focus()
	End Sub

	Private Sub GIToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GIToolStripMenuItem.Click
		EMI_Hasil_Pengeluaran_Bahan_Baku_Baru.StartPosition = FormStartPosition.CenterScreen

		EMI_Hasil_Pengeluaran_Bahan_Baku_Baru.asal = "INDEPENDENT"
		EMI_Hasil_Pengeluaran_Bahan_Baku_Baru.MdiParent = Me
		EMI_Hasil_Pengeluaran_Bahan_Baku_Baru.Show()
		EMI_Hasil_Pengeluaran_Bahan_Baku_Baru.Focus()
	End Sub

End Class
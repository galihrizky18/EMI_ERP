Public Class Master_Klasifikasi_Bahan
	Dim arrcariKlasifikasiBahan As New ArrayList

	Private Sub Master_Klasifikasi_Bahan_Activated(sender As Object, e As EventArgs) Handles Me.Activated
		My.Application.ChangeCulture("en-us")
		My.Application.ChangeUICulture("en-us")
	End Sub

	Private Sub Master_Klasifikasi_Bahan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		My.Application.ChangeCulture("en-us")
		My.Application.ChangeUICulture("en-us")

		Try
			OpenConn()

			Base_Language.Get_Languages(Bahasa_Pilihan, "Master_Klasifikasi_Bahan")

			Label1.Text = Base_Language.Lang_Klasifikasi_Bahan_Judul

			'lblKlasifikasiBahan_Judul.Text = Base_Language.Lang_Klasifikasi_Bahan_Judul
			LblKlasifikasiBahan_Kode.Text = Base_Language.Lang_Klasifikasi_Bahan_Kode
			LblKlasifikasiBahan_Ket.Text = Base_Language.Lang_Klasifikasi_Bahan_Keterangan
			LblKlasifikasiBahan_Prefix.Text = Base_Language.Lang_Klasifikasi_Bahan_Prefix
			LblKlasifikasiBahan_Kolom.Text = Base_Language.Lang_Klasifikasi_Bahan_Kolom

			LvwKlasifikasiBahan_Data.Columns.Add("Id Jenis", 0, HorizontalAlignment.Left)
			LvwKlasifikasiBahan_Data.Columns.Add(Base_Language.Lang_Klasifikasi_Bahan_Kode, 150, HorizontalAlignment.Left)
			LvwKlasifikasiBahan_Data.Columns.Add(Base_Language.Lang_Klasifikasi_Bahan_Keterangan, 660, HorizontalAlignment.Left)
			LvwKlasifikasiBahan_Data.Columns.Add(Base_Language.Lang_Klasifikasi_Bahan_Prefix, 100, HorizontalAlignment.Left)

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub

		End Try
		kosongKlasifikasiBahan()
	End Sub

	'================================================================================================================
	'KLASIFIKASI BAHAN
	'================================================================================================================

	Private Sub kosongKlasifikasiBahan()
		TxtKlasifikasiBahan_Kode.Text = ""
		TxtKlasifikasiBahan_Kode.Enabled = True
		TxtKlasifikasiBahan_Ket.Text = ""
		TxtKlasifikasiBahan_Prefix.Text = ""

		Try

			OpenConn()

			Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")

			CmbKlasifikasiBahan_Kolom.Items.Clear() : arrcariKlasifikasiBahan.Clear()
			CmbKlasifikasiBahan_Kolom.Items.Add(Base_Language.Lang_Klasifikasi_Bahan_Kode) : arrcariKlasifikasiBahan.Add("kode_Klasifikasi_Bahan")
			CmbKlasifikasiBahan_Kolom.Items.Add(Base_Language.Lang_Klasifikasi_Bahan_Keterangan) : arrcariKlasifikasiBahan.Add("keterangan")
			TxtKlasifikasiBahan_Value.Text = ""

			BtnKlasifikasiBahan_Simpan.Text = Base_Language.Lang_Global_Simpan
			BtnKlasifikasiBahan_Hapus.Text = Base_Language.Lang_Global_Hapus
			BtnKlasifikasiBahan_Cari.Text = Base_Language.Lang_Global_Cari
			BtnKlasifikasiBahan_Refresh.Text = Base_Language.Lang_Global_Refresh
			BtnKlasifikasiBahan_Simpan.Tag = "&Simpan"
			BtnKlasifikasiBahan_Hapus.Enabled = False

			LvwKlasifikasiBahan_Data.Items.Clear()
			SQL = "Select id_Klasifikasi_Bahan,kode_Klasifikasi_Bahan, keterangan, prefix_Klasifikasi_Bahan From emi_Klasifikasi_Bahan where kode_perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "order by keterangan "
			Using dr = OpenTrans(SQL)
				Do While dr.Read
					Dim Lvw As ListViewItem
					Lvw = LvwKlasifikasiBahan_Data.Items.Add(dr("id_Klasifikasi_Bahan"))
					Lvw.SubItems.Add(dr("kode_Klasifikasi_Bahan"))
					Lvw.SubItems.Add(dr("keterangan"))
					Lvw.SubItems.Add(dr("prefix_Klasifikasi_Bahan"))
				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub CariKlasifikasiBahan(ByVal semua As String)
		Try

			OpenConn()

			LvwKlasifikasiBahan_Data.Items.Clear()
			SQL = "Select id_Klasifikasi_Bahan,Kode_Klasifikasi_Bahan, keterangan, prefix_Klasifikasi_Bahan From emi_klasifikasi_bahan where kode_perusahaan = '" & KodePerusahaan & "' "
			If semua = "T" Then
				SQL = SQL & "and " & arrcariKlasifikasiBahan.Item(CmbKlasifikasiBahan_Kolom.SelectedIndex) & " like '%" & TxtKlasifikasiBahan_Value.Text & "%' "
				SQL = SQL & "order by " & arrcariKlasifikasiBahan.Item(CmbKlasifikasiBahan_Kolom.SelectedIndex) & " "
			Else
				SQL = SQL & "order by keterangan "
			End If
			Using dr = OpenTrans(SQL)
				Do While dr.Read
					Dim Lvw As ListViewItem
					Lvw = LvwKlasifikasiBahan_Data.Items.Add(dr("id_Klasifikasi_Bahan"))
					Lvw.SubItems.Add(dr("Kode_Klasifikasi_Bahan"))
					Lvw.SubItems.Add(dr("keterangan"))
					Lvw.SubItems.Add(dr("prefix_Klasifikasi_Bahan"))
				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub TxtKlasifikasiBahan_Kode_Leave(sender As Object, e As EventArgs) Handles TxtKlasifikasiBahan_Kode.Leave
		If TxtKlasifikasiBahan_Kode.Text.Trim.Length = 0 Then Exit Sub

		Try

			OpenConn()

			SQL = "Select Kode_Klasifikasi_Bahan, keterangan, Prefix_Klasifikasi_Bahan From emi_klasifikasi_bahan Where "
			SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
			SQL = SQL & "Kode_Klasifikasi_Bahan = '" & TxtKlasifikasiBahan_Kode.Text.Trim & "'"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					TxtKlasifikasiBahan_Ket.Text = Dr("keterangan")
					TxtKlasifikasiBahan_Prefix.Text = Dr("Prefix_Klasifikasi_Bahan")

					TxtKlasifikasiBahan_Kode.Enabled = False
					BtnKlasifikasiBahan_Simpan.Text = Base_Language.Lang_Global_Update : BtnKlasifikasiBahan_Hapus.Enabled = True
					BtnKlasifikasiBahan_Simpan.Tag = "&Update"
				Else
					TxtKlasifikasiBahan_Ket.Text = ""

					BtnKlasifikasiBahan_Simpan.Text = Base_Language.Lang_Global_Simpan : BtnKlasifikasiBahan_Hapus.Enabled = False
					BtnKlasifikasiBahan_Simpan.Tag = "&Simpan"
				End If
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub BtnKlasifikasiBahan_Simpan_Click(sender As Object, e As EventArgs) Handles BtnKlasifikasiBahan_Simpan.Click
		If TxtKlasifikasiBahan_Kode.Text.Trim.Length = 0 Then
			MessageBox.Show(Base_Language.Lang_Klasifikasi_Bahan_Error_Kode, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			TxtKlasifikasiBahan_Kode.Focus() : Exit Sub
		ElseIf TxtKlasifikasiBahan_Ket.Text.Trim.Length = 0 Then
			MessageBox.Show(Base_Language.Lang_Klasifikasi_Bahan_Error_Nama, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			TxtKlasifikasiBahan_Ket.Focus() : Exit Sub
		ElseIf TxtKlasifikasiBahan_Prefix.Text.Trim.Length <> 2 Then
			MessageBox.Show(Base_Language.Lang_Klasifikasi_Bahan_Error_Prefix, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			TxtKlasifikasiBahan_Prefix.Focus() : Exit Sub
		ElseIf IsNumeric(TxtKlasifikasiBahan_Prefix.Text) = False Then
			MessageBox.Show(Base_Language.Lang_Klasifikasi_Bahan_Error_Prefix_Numeric, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			TxtKlasifikasiBahan_Prefix.Focus() : Exit Sub
		End If

		Try

			OpenConn()
			SQL = "Select ID_Klasifikasi_Bahan From EMI_Klasifikasi_Bahan Where Prefix_Klasifikasi_Bahan = '" & TxtKlasifikasiBahan_Prefix.Text & "' And Kode_Klasifikasi_Bahan <> '" & TxtKlasifikasiBahan_Kode.Text & "'"
			Using Ds = Binding(SQL)
				If Ds.Tables("MyTable").Rows.Count = 0 Then
				Else
					MessageBox.Show(Base_Language.Lang_Klasifikasi_Bahan_Error_Prefix_Sudah_Ada, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					TxtKlasifikasiBahan_Prefix.Focus()
					'Cmd.Transaction.Rollback()
					CloseConn()
					Exit Sub
				End If
			End Using
			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Try

			OpenConn()

			Cmd.Transaction = Cn.BeginTransaction

			If BtnKlasifikasiBahan_Simpan.Tag = "&Simpan" Then
				SQL = "Insert Into emi_klasifikasi_bahan(Kode_Perusahaan, Kode_Klasifikasi_Bahan, keterangan, Prefix_Klasifikasi_Bahan) "
				SQL = SQL & "Values('" & KodePerusahaan & "', "
				SQL = SQL & "'" & TxtKlasifikasiBahan_Kode.Text.Trim & "', '" & TxtKlasifikasiBahan_Ket.Text.Trim & "', '" & TxtKlasifikasiBahan_Prefix.Text.Trim & "')"
				ExecuteTrans(SQL)
			Else
				SQL = "Update emi_klasifikasi_bahan Set keterangan = '" & TxtKlasifikasiBahan_Ket.Text.Trim & "', Prefix_Klasifikasi_Bahan = '" & TxtKlasifikasiBahan_Prefix.Text.Trim & "' "
				SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and Kode_Klasifikasi_Bahan = '" & TxtKlasifikasiBahan_Kode.Text.Trim & "'"
				ExecuteTrans(SQL)
			End If

			Cmd.Transaction.Commit()

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
		kosongKlasifikasiBahan()
		TxtKlasifikasiBahan_Kode.Focus()
	End Sub

	Private Sub BtnKlasifikasiBahan_Hapus_Click(sender As Object, e As EventArgs) Handles BtnKlasifikasiBahan_Hapus.Click
		Dim Hapus1 As String = MessageBox.Show(Base_Language.Lang_Global_Tanya_Hapus, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
		If Hapus1 = vbYes Then

			Try

				OpenConn()

				Cmd.Transaction = Cn.BeginTransaction

				SQL = "Delete From emi_klasifikasi_bahan where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Klasifikasi_Bahan = '" & TxtKlasifikasiBahan_Kode.Text.Trim & "'"
				ExecuteTrans(SQL)

				Cmd.Transaction.Commit()

				CloseConn()
			Catch ex As Exception
				CloseTrans()
				CloseConn()
				MessageBox.Show(ex.Message)
				Exit Sub
			End Try
		Else
			MessageBox.Show(Base_Language.Lang_Global_Hapus_No, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		End If
		kosongKlasifikasiBahan()
		TxtKlasifikasiBahan_Kode.Focus()
	End Sub

	Private Sub BtnKlasifikasiBahan_Refresh_Click(sender As Object, e As EventArgs) Handles BtnKlasifikasiBahan_Refresh.Click
		kosongKlasifikasiBahan()
	End Sub

	Private Sub BtnKlasifikasiBahan_Cari_Click(sender As Object, e As EventArgs) Handles BtnKlasifikasiBahan_Cari.Click
		If CmbKlasifikasiBahan_Kolom.Text.Trim.Length = 0 Then Exit Sub
		If TxtKlasifikasiBahan_Value.Text.Trim.Length = 0 Then Exit Sub

		CariKlasifikasiBahan("T")
	End Sub

	Private Sub LvwKlasifikasiBahan_Data_DoubleClick(sender As Object, e As EventArgs) Handles LvwKlasifikasiBahan_Data.DoubleClick

		TxtKlasifikasiBahan_Kode.Text = LvwKlasifikasiBahan_Data.FocusedItem.SubItems(1).Text
		TxtKlasifikasiBahan_Kode_Leave(LvwKlasifikasiBahan_Data, e)
	End Sub

	Private Sub TxtKlasifikasiBahan_Kode_TextChanged(sender As Object, e As EventArgs) Handles TxtKlasifikasiBahan_Kode.TextChanged

	End Sub

	Private Sub TxtKlasifikasiBahan_Kode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtKlasifikasiBahan_Kode.KeyPress
		If e.KeyChar = Chr(13) Then TxtKlasifikasiBahan_Ket.Focus()
	End Sub

	Private Sub TxtKlasifikasiBahan_Ket_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtKlasifikasiBahan_Ket.KeyPress
		If e.KeyChar = Chr(13) Then TxtKlasifikasiBahan_Prefix.Focus()
	End Sub

	Private Sub TxtKlasifikasiBahan_Prefix_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtKlasifikasiBahan_Prefix.KeyPress
		If e.KeyChar = Chr(13) Then
			BtnKlasifikasiBahan_Simpan.Focus()
		Else
			If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(Asc("."))) Then e.KeyChar = Chr(0)
		End If
	End Sub

	Private Sub CmbKlasifikasiBahan_Kolom_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CmbKlasifikasiBahan_Kolom.KeyPress
		If e.KeyChar = Chr(13) Then TxtKlasifikasiBahan_Value.Focus()
	End Sub

	Private Sub TxtKlasifikasiBahan_Value_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtKlasifikasiBahan_Value.KeyPress
		If e.KeyChar = Chr(13) Then BtnKlasifikasiBahan_Cari_Click(TxtKlasifikasiBahan_Value, e)
	End Sub

	Private Sub TxtKlasifikasiBahan_Prefix_TextChanged(sender As Object, e As EventArgs) Handles TxtKlasifikasiBahan_Prefix.TextChanged

	End Sub

	Private Sub LvwKlasifikasiBahan_Data_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LvwKlasifikasiBahan_Data.SelectedIndexChanged

	End Sub

End Class
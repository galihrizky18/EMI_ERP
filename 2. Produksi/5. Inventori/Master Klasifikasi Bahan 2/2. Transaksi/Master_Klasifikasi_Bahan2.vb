Public Class Master_Klasifikasi_Bahan2

	Dim arrKlasifikasiBahan1, arrKlasifikasiBahan1Prefix As New ArrayList
	Dim selectedIdKlasifikasiBahan1, idKlasifikasiAwal, prefixAwal As String

	Dim lv_IdKlasifikasi2, lv_IdKlasifikasi1, lv_Kode, lv_Keterangan, lv_prefix As String

	Dim itemKlasifikasi2 As Integer = 0
	Dim itemKlasifikasi1 As Integer = 1
	Dim itemKode As Integer = 3
	Dim itemKeterangan As Integer = 4
	Dim itemPrefix As Integer = 5

	Private Sub Master_Klasifikasi_Bahan2_Load(sender As Object, e As EventArgs) Handles MyBase.Load

		Lv_Klasifikasi_Bahan2.Columns.Add("id_Klasifikasi_Bahan_2", 0, HorizontalAlignment.Center)
		Lv_Klasifikasi_Bahan2.Columns.Add("id_Klasifikasi_Bahan_1", 0, HorizontalAlignment.Center)
		Lv_Klasifikasi_Bahan2.Columns.Add("Klasifikasi Bahan 1", 250, HorizontalAlignment.Center)
		Lv_Klasifikasi_Bahan2.Columns.Add("Kode", 250, HorizontalAlignment.Center)
		Lv_Klasifikasi_Bahan2.Columns.Add("Keterangan", 300, HorizontalAlignment.Center)
		Lv_Klasifikasi_Bahan2.Columns.Add("Prefix", 120, HorizontalAlignment.Center)

		Lv_Klasifikasi_Bahan2.View = View.Details

		kosongKlasifikasi2()
		Load_Klasifikasi2_Lv()

	End Sub

	Private Sub Cmb_Kolom_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Klasifikasi2_Kolom.SelectedIndexChanged
		Txt_Klasifikasi2_Value.Enabled = True
	End Sub

	Private Sub kosongKlasifikasi2()

		Cmb_Klasifikasi2_Kategori1.Items.Clear()
		Cmb_Klasifikasi2_Kolom.Items.Clear()
		Txt_Klasifikasi2_Kode.Text = String.Empty
		Txt_Klasifikasi2_Keterangan.Text = String.Empty
		Txt_Klasifikasi2_Prefix.Text = String.Empty
		Txt_Klasifikasi2_PrefixKategori.Text = String.Empty
		Txt_Klasifikasi2_Value.Text = String.Empty
		selectedIdKlasifikasiBahan1 = String.Empty
		idKlasifikasiAwal = String.Empty
		prefixAwal = String.Empty

		Lv_Klasifikasi_Bahan2.Items.Clear()

		Btn_Klasifikasi2_Simpan.Tag = "SIMPAN"
		Btn_Klasifikasi2_Simpan.Text = "&Simpan"

		Cmb_Klasifikasi2_Kolom.Items.Add("Kode")
		Cmb_Klasifikasi2_Kolom.Items.Add("Keterangan")

		arrKlasifikasiBahan1.Clear()
		arrKlasifikasiBahan1Prefix.Clear()

		Try
			OpenConn()

			SQL = "Select Id_Klasifikasi_Bahan, Kode_Klasifikasi_Bahan, Keterangan, Prefix_Klasifikasi_Bahan from EMI_Klasifikasi_Bahan order by Keterangan "
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Cmb_Klasifikasi2_Kategori1.Items.Add(Dr("Keterangan")) : arrKlasifikasiBahan1.Add(Dr("Id_Klasifikasi_Bahan"))
					arrKlasifikasiBahan1Prefix.Add(Dr("Prefix_Klasifikasi_Bahan"))
				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

	End Sub

	Private Sub Load_Klasifikasi2_Lv(ByVal Optional filter As String = "")
		Try
			OpenConn()

			Lv_Klasifikasi_Bahan2.Items.Clear()

			SQL = "Select b.Id_Klasifikasi_Bahan ,b.Keterangan ,a.Id_Klasifikasi_Bahan2, a.Kode_Klasifikasi_Bahan, a.Keterangan as keterangan2, a.Prefix_Klasifikasi_Bahan "
			SQL = SQL & "from EMI_Klasifikasi_Bahan2 a, EMI_Klasifikasi_Bahan b "
			SQL = SQL & "where a.Kode_Perusahaan=b.Kode_Perusahaan and a.Id_Klasifikasi_Bahan1=b.Id_Klasifikasi_Bahan "
			If Not filter = "" Then
				SQL = SQL & "and " & filter & " "
			End If
			SQL = SQL & "group by b.Keterangan ,a.Id_Klasifikasi_Bahan2, a.Kode_Klasifikasi_Bahan, a.Keterangan, a.Prefix_Klasifikasi_Bahan, b.Id_Klasifikasi_Bahan "
			SQL = SQL & "order by a.Id_Klasifikasi_Bahan2"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Dim Lv As ListViewItem
					Lv = Lv_Klasifikasi_Bahan2.Items.Add(Dr("Id_Klasifikasi_Bahan2"))
					Lv.SubItems.Add(Dr("Id_Klasifikasi_Bahan"))
					Lv.SubItems.Add(Dr("Keterangan"))
					Lv.SubItems.Add(Dr("Kode_Klasifikasi_Bahan"))
					Lv.SubItems.Add(Dr("keterangan2"))
					Lv.SubItems.Add(Dr("Prefix_Klasifikasi_Bahan"))

					Lv.SubItems(1).BackColor = Color.LightBlue
				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Klasifikasi2_Refresh.Click
		kosongKlasifikasi2()
		Load_Klasifikasi2_Lv()
	End Sub

	Private Sub Get_Data_Lv(ByVal index As Integer)

		lv_IdKlasifikasi2 = Lv_Klasifikasi_Bahan2.Items(index).SubItems(itemKlasifikasi2).Text
		lv_IdKlasifikasi1 = Lv_Klasifikasi_Bahan2.Items(index).SubItems(itemKlasifikasi1).Text
		lv_Kode = Lv_Klasifikasi_Bahan2.Items(index).SubItems(itemKode).Text
		lv_Keterangan = Lv_Klasifikasi_Bahan2.Items(index).SubItems(itemKeterangan).Text
		lv_prefix = Lv_Klasifikasi_Bahan2.Items(index).SubItems(itemPrefix).Text

	End Sub

	Private Sub Lv_KlasifikasiBahan_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Klasifikasi_Bahan2.DoubleClick
		If Lv_Klasifikasi_Bahan2.Items.Count = 0 Then Exit Sub

		Get_Data_Lv(Lv_Klasifikasi_Bahan2.FocusedItem.Index)

		Txt_Klasifikasi2_Kode.Text = lv_Kode
		Txt_Klasifikasi2_Keterangan.Text = lv_Keterangan
		Txt_Klasifikasi2_PrefixKategori.Text = lv_prefix

		idKlasifikasiAwal = lv_IdKlasifikasi2
		prefixAwal = lv_prefix

		Try
			OpenConn()

			SQL = "Select Prefix_Klasifikasi_Bahan, keterangan from EMI_Klasifikasi_Bahan where Id_Klasifikasi_Bahan='" & lv_IdKlasifikasi1 & "'"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Txt_Klasifikasi2_Prefix.Text = Dr("Prefix_Klasifikasi_Bahan")
					Cmb_Klasifikasi2_Kategori1.SelectedItem = Dr("keterangan")
				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Btn_Klasifikasi2_Simpan.Tag = "UPDATE"
		Btn_Klasifikasi2_Simpan.Text = "&Update"

	End Sub

	Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Klasifikasi2_Simpan.Click
		If Cmb_Klasifikasi2_Kategori1.SelectedIndex = -1 Or selectedIdKlasifikasiBahan1.Trim.Length = 0 Then
			MessageBox.Show("Kategori 1 Harus Dipilih", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmb_Klasifikasi2_Kategori1.Focus() : Exit Sub
		ElseIf Txt_Klasifikasi2_Kode.Text.Trim.Length = 0 Then
			MessageBox.Show("Kode Harus Di Isi", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_Klasifikasi2_Kode.Focus() : Exit Sub
		ElseIf Txt_Klasifikasi2_Keterangan.Text.Trim.Length = 0 Then
			MessageBox.Show("Keterangan Harus Di Isi", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_Klasifikasi2_Keterangan.Focus() : Exit Sub
		ElseIf Txt_Klasifikasi2_PrefixKategori.Text.Trim.Length <> 2 Then
			MessageBox.Show("Prefix Harus 2 Angka", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_Klasifikasi2_PrefixKategori.Focus() : Exit Sub
		ElseIf IsNumeric(Txt_Klasifikasi2_PrefixKategori.Text) = False Then
			MessageBox.Show("Prefix Harus Angka", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_Klasifikasi2_PrefixKategori.Focus() : Exit Sub
		End If

		'======= INSERT =======
		Try
			OpenConn()

			If Btn_Klasifikasi2_Simpan.Tag = "SIMPAN" Then

				SQL = "Select Id_Klasifikasi_Bahan2 from EMI_Klasifikasi_Bahan2 "
				SQL = SQL & "where Prefix_Klasifikasi_Bahan='" & Txt_Klasifikasi2_PrefixKategori.Text & "'"
				Using dr = OpenTrans(SQL)
					If dr.HasRows Then
						MessageBox.Show(Base_Language.Lang_Klasifikasi_Bahan_Error_Prefix_Sudah_Ada, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Txt_Klasifikasi2_PrefixKategori.Focus()
						CloseConn()
						Exit Sub
					End If
				End Using

				SQL = "insert into EMI_Klasifikasi_Bahan2 ( Kode_Perusahaan, Id_Klasifikasi_Bahan1, "
				SQL = SQL & "Kode_Klasifikasi_Bahan, Keterangan, Prefix_Klasifikasi_Bahan) "
				SQL = SQL & "values ('" & KodePerusahaan & "', '" & selectedIdKlasifikasiBahan1 & "', '" & Txt_Klasifikasi2_Kode.Text.Trim & "', "
				SQL = SQL & "'" & Txt_Klasifikasi2_Keterangan.Text.Trim & "', '" & Txt_Klasifikasi2_PrefixKategori.Text.Trim & "')"
				ExecuteTrans(SQL)

				MessageBox.Show("Beerhasil Disimpan", Base_Language.Lang_Global_Simpan, MessageBoxButtons.OK, MessageBoxIcon.Information)

			ElseIf Btn_Klasifikasi2_Simpan.Tag = "UPDATE" Then

				SQL = "UPDATE EMI_Klasifikasi_Bahan2 set Id_Klasifikasi_Bahan1='" & selectedIdKlasifikasiBahan1 & "', "
				SQL = SQL & "Kode_Klasifikasi_Bahan='" & Txt_Klasifikasi2_Kode.Text.Trim & "', Keterangan='" & Txt_Klasifikasi2_Keterangan.Text.Trim & "', "
				SQL = SQL & "Prefix_Klasifikasi_Bahan='" & Txt_Klasifikasi2_PrefixKategori.Text.Trim & "'"
				SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' and Id_Klasifikasi_Bahan2 = '" & idKlasifikasiAwal & "' "
				SQL = SQL & " and Prefix_Klasifikasi_Bahan='" & prefixAwal & "'"
				ExecuteTrans(SQL)

				MessageBox.Show("Beerhasil DiUpdate", Base_Language.Lang_Global_Update, MessageBoxButtons.OK, MessageBoxIcon.Information)

			End If

			kosongKlasifikasi2()
			Load_Klasifikasi2_Lv()

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

	End Sub

	Private Sub Cmb_Kategori1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Klasifikasi2_Kategori1.SelectedIndexChanged
		If Cmb_Klasifikasi2_Kategori1.Items.Count = 0 AndAlso Cmb_Klasifikasi2_Kategori1.SelectedIndex = -1 Then Exit Sub

		Txt_Klasifikasi2_Prefix.Text = arrKlasifikasiBahan1Prefix(Cmb_Klasifikasi2_Kategori1.SelectedIndex)
		selectedIdKlasifikasiBahan1 = arrKlasifikasiBahan1(Cmb_Klasifikasi2_Kategori1.SelectedIndex)

	End Sub

	Private Sub Tb_PrefixKategori2_Leave(sender As Object, e As EventArgs) Handles Txt_Klasifikasi2_PrefixKategori.Leave
		If Not IsNumeric(Txt_Klasifikasi2_PrefixKategori.Text) Then Txt_Klasifikasi2_PrefixKategori.Text = ""
	End Sub

	Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Klasifikasi2_Cari.Click
		If Cmb_Klasifikasi2_Kolom.SelectedIndex = -1 Then Exit Sub

		Dim filter As String = ""

		If Cmb_Klasifikasi2_Kolom.SelectedIndex = 0 Then

			filter = "a.Kode_Klasifikasi_Bahan like '" & Txt_Klasifikasi2_Value.Text & "%'"

		ElseIf Cmb_Klasifikasi2_Kolom.SelectedIndex = 1 Then

			filter = "a.Keterangan like '" & Txt_Klasifikasi2_Value.Text & "%'"

		End If

		Load_Klasifikasi2_Lv(filter)
	End Sub

	Private Sub Btn_Hapus_Click(sender As Object, e As EventArgs) Handles Btn_Klasifikasi2_Hapus.Click
		If Cmb_Klasifikasi2_Kategori1.SelectedIndex = -1 Or selectedIdKlasifikasiBahan1.Trim.Length = 0 Then Exit Sub

		Dim Hapus1 As String = MessageBox.Show("Yakin Ingin Hapus?", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
		If Hapus1 = vbYes Then
			Try

				OpenConn()

				Cmd.Transaction = Cn.BeginTransaction

				SQL = "delete from EMI_Klasifikasi_Bahan2 where kode_perusahaan='" & KodePerusahaan & "' and Id_Klasifikasi_Bahan2='" & idKlasifikasiAwal & "' "
				SQL = SQL & "and Prefix_Klasifikasi_Bahan='" & prefixAwal & "'"
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

		kosongKlasifikasi2()
		Load_Klasifikasi2_Lv()

	End Sub

End Class
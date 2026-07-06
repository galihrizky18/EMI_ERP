Public Class N_EMI_Master_Klasifikasi_Bahan

	Dim menuItems As String() = {
		"Klasifikasi Bahan", "Klasifikasi Bahan 2", "Klasifikasi Bahan 3"
	}

	Private activeLine As Panel = Nothing

	Dim arrcariKlasifikasiBahan As New ArrayList
	Dim arrKlasifikasiBahan1, arrKlasifikasiBahan1Prefix As New ArrayList
	Dim selectedIdKlasifikasiBahan1, idKlasifikasiAwal, prefixAwal As String

	Dim arrFilterKlasifikasi3 As New ArrayList

	Dim lv_IdKlasifikasi2, lv_IdKlasifikasi1, lv_Kode, lv_Keterangan, lv_prefix As String

	Dim itemKlasifikasi2 As Integer = 0
	Dim itemKlasifikasi1 As Integer = 1
	Dim itemKode As Integer = 3
	Dim itemKeterangan As Integer = 4
	Dim itemPrefix As Integer = 5

	Dim Lv_Klasifikasi3_ID, Lv_Klasifikasi3_Prefix, Lv_Klasifikasi3_KodeKlasifikasi, Lv_Klasifikasi3_Keterangan, Lv_Klasifikasi3_Toleransi_Min, Lv_Klasifikasi3_Toleransi_Max As String

	Dim item_Klasifikasi3_ID As Integer = 0
	Dim item_Klasifikasi3_Prefix As Integer = 1
	Dim item_Klasifikasi3_KodeKlasifikasi As Integer = 2
	Dim item_Klasifikasi3_Keterangan As Integer = 3
	Dim item_Klasifikasi3_ToleransiMin As Integer = 4
	Dim item_Klasifikasi3_ToleransiMax As Integer = 5

	Private Sub N_EMI_Master_Klasifikasi_Bahan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		EnableDoubleBuffer(LvwKlasifikasiBahan_Data)
		EnableDoubleBuffer(Lv_Klasifikasi_Bahan2)
		EnableDoubleBuffer(Lv_Klasifikasi_Bahan_3)

		Try
			OpenConn()

			'=========================================================================================================================
			'=     MENU KLASIFIKASI BAHAN 1
			'=========================================================================================================================

			Base_Language.Get_Languages(Bahasa_Pilihan, "Master_Klasifikasi_Bahan")

			'lblKlasifikasiBahan_Judul.Text = Base_Language.Lang_Klasifikasi_Bahan_Judul
			LblKlasifikasiBahan_Kode.Text = Base_Language.Lang_Klasifikasi_Bahan_Kode
			LblKlasifikasiBahan_Ket.Text = Base_Language.Lang_Klasifikasi_Bahan_Keterangan
			LblKlasifikasiBahan_Prefix.Text = Base_Language.Lang_Klasifikasi_Bahan_Prefix
			LblKlasifikasiBahan_Kolom.Text = Base_Language.Lang_Klasifikasi_Bahan_Kolom

			LvwKlasifikasiBahan_Data.Columns.Add("Id Jenis", 0, HorizontalAlignment.Left)
			LvwKlasifikasiBahan_Data.Columns.Add(Base_Language.Lang_Klasifikasi_Bahan_Kode, 180, HorizontalAlignment.Left)
			LvwKlasifikasiBahan_Data.Columns.Add(Base_Language.Lang_Klasifikasi_Bahan_Keterangan, 630, HorizontalAlignment.Left)
			LvwKlasifikasiBahan_Data.Columns.Add(Base_Language.Lang_Klasifikasi_Bahan_Prefix, 100, HorizontalAlignment.Left)

			'=========================================================================================================================
			'=     MENU KLASIFIKASI BAHAN 2
			'=========================================================================================================================

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

			Lv_Klasifikasi_Bahan2.Columns.Add("id_Klasifikasi_Bahan_2", 0, HorizontalAlignment.Center)
			Lv_Klasifikasi_Bahan2.Columns.Add("id_Klasifikasi_Bahan_1", 0, HorizontalAlignment.Center)
			Lv_Klasifikasi_Bahan2.Columns.Add("Klasifikasi Bahan 1", 250, HorizontalAlignment.Left)
			Lv_Klasifikasi_Bahan2.Columns.Add("Kode", 250, HorizontalAlignment.Left)
			Lv_Klasifikasi_Bahan2.Columns.Add("Keterangan", 300, HorizontalAlignment.Left)
			Lv_Klasifikasi_Bahan2.Columns.Add("Prefix", 90, HorizontalAlignment.Center).DisplayIndex = 2
			Lv_Klasifikasi_Bahan2.View = View.Details

			'=========================================================================================================================
			'=     MENU KLASIFIKASI BAHAN 3
			'=========================================================================================================================

			Lv_Klasifikasi_Bahan_3.Columns.Clear()
			Lv_Klasifikasi_Bahan_3.Columns.Add("Id", 0, HorizontalAlignment.Left)
			Lv_Klasifikasi_Bahan_3.Columns.Add("Prefix", 90, HorizontalAlignment.Center)
			Lv_Klasifikasi_Bahan_3.Columns.Add("Kode Klasifikasi", 200, HorizontalAlignment.Left)
			Lv_Klasifikasi_Bahan_3.Columns.Add("Keterangan", 420, HorizontalAlignment.Left)
			Lv_Klasifikasi_Bahan_3.Columns.Add("Persentase Min", 100, HorizontalAlignment.Center)
			Lv_Klasifikasi_Bahan_3.Columns.Add("Persentase Max", 100, HorizontalAlignment.Center)
			Lv_Klasifikasi_Bahan_3.View = View.Details

			Cmb_Klasifikasi3_Kolom.Items.Clear() : arrFilterKlasifikasi3.Clear()
			Cmb_Klasifikasi3_Kolom.Items.Add("Prefix") : arrFilterKlasifikasi3.Add("Prefix_Klasifikasi_Bahan")
			Cmb_Klasifikasi3_Kolom.Items.Add("Kode Klasifikasi") : arrFilterKlasifikasi3.Add("Kode_Klasifikasi_Bahan")
			Cmb_Klasifikasi3_Kolom.Items.Add("Keterangan") : arrFilterKlasifikasi3.Add("Keterangan")

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub

		End Try

		LoadTabMenu()
		Kosong()
	End Sub

	Private Sub Kosong()

	End Sub

	Private Sub HandleClickMenu(ByVal MenuTag As String)
		If MenuTag Is Nothing Then Exit Sub

		Select Case MenuTag
			Case "Klasifikasi Bahan"
				Tab_Control.SelectedIndex = 0
				kosongKlasifikasiBahan()
			Case "Klasifikasi Bahan 2"
				Tab_Control.SelectedIndex = 1
				kosongKlasifikasi2()
				Load_Klasifikasi2_Lv()
			Case "Klasifikasi Bahan 3"
				Tab_Control.SelectedIndex = 2
				Kosong_Klasifikasi_Bahan_3()
		End Select
	End Sub

	'=========================================================================================================================
	'=     MENU KLASIFIKASI BAHAN 1
	'=========================================================================================================================

#Region "KLASIFIKASI BAHAN 1"

	Private Sub kosongKlasifikasiBahan()
		TxtKlasifikasiBahan_Kode.Text = ""
		TxtKlasifikasiBahan_Kode.Enabled = True
		TxtKlasifikasiBahan_Ket.Text = ""
		TxtKlasifikasiBahan_Prefix.Text = ""

		Try

			OpenConn()

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

		TxtKlasifikasiBahan_Kode.Focus()
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

#End Region

	'=========================================================================================================================
	'=     MENU KLASIFIKASI BAHAN 2
	'=========================================================================================================================

#Region "KLASIFIKASI BAHAN 2"

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

		Cmb_Klasifikasi2_Kategori1.Focus()

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

#End Region

	'=========================================================================================================================
	'=     MENU KLASIFIKASI BAHAN 3
	'=========================================================================================================================

#Region "KLASIFIKASI BAHAN 3"

	Private Sub Kosong_Klasifikasi_Bahan_3()

		Txt_Klasifikasi3_Kode.Text = ""
		Txt_Klasifikasi3_Keterangan.Text = ""
		Txt_Klasifikasi3_Prefix.Text = ""

		Txt_Klasifikasi3_SelectedID.Text = ""

		Cmb_Klasifikasi3_Kolom.SelectedIndex = -1
		Txt_Klasifikasi3_Value.Text = ""

		Txt_Klasifikasi3_Kode.Enabled = True

		Txt_Toleransi_Min.Text = ""
		Txt_Toleransi_Max.Text = ""

		Btn_Klasifikasi3_Simpan.Tag = "SIMPAN"
		Btn_Klasifikasi3_Simpan.Text = "&Simpan"

		Load_Data_Lv_Klasifikasi_Bahan_3()

		Try
			OpenConn()

			GetCurrentPrefixKlasifikasi3()

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Txt_Klasifikasi3_Kode.Focus()
	End Sub

	Private Sub GetDataLvKlasifikasi3(ByVal index As Integer)

		Lv_Klasifikasi3_ID = Lv_Klasifikasi_Bahan_3.Items(index).SubItems(item_Klasifikasi3_ID).Text
		Lv_Klasifikasi3_Prefix = Lv_Klasifikasi_Bahan_3.Items(index).SubItems(item_Klasifikasi3_Prefix).Text
		Lv_Klasifikasi3_KodeKlasifikasi = Lv_Klasifikasi_Bahan_3.Items(index).SubItems(item_Klasifikasi3_KodeKlasifikasi).Text
		Lv_Klasifikasi3_Keterangan = Lv_Klasifikasi_Bahan_3.Items(index).SubItems(item_Klasifikasi3_Keterangan).Text
		Lv_Klasifikasi3_Toleransi_Min = Lv_Klasifikasi_Bahan_3.Items(index).SubItems(item_Klasifikasi3_ToleransiMin).Text
		Lv_Klasifikasi3_Toleransi_Max = Lv_Klasifikasi_Bahan_3.Items(index).SubItems(item_Klasifikasi3_ToleransiMax).Text
	End Sub

	Private Sub Load_Data_Lv_Klasifikasi_Bahan_3()
		Try
			OpenConn()

			Dim Filter As String = ""
			If Cmb_Klasifikasi3_Kolom.SelectedIndex <> -1 Then
				Filter &= $"and {arrFilterKlasifikasi3(Cmb_Klasifikasi3_Kolom.SelectedIndex)} like '%{Txt_Klasifikasi3_Value.Text.Trim}%' "
			End If

			Lv_Klasifikasi_Bahan_3.Items.Clear()
			SQL = $"
				select Id_Klasifikasi_Bahan3, Kode_Klasifikasi_Bahan, Prefix_Klasifikasi_Bahan, Keterangan, Toleransi_Formula_GI_Min, Toleransi_Formula_GI_Max
				from N_EMI_Master_Klasifikasi_Bahan_3
				where Kode_Perusahaan = '{KodePerusahaan}'
				{Filter}
				order by Id_Klasifikasi_Bahan3
			"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Dim Lv As ListViewItem
					Lv = Lv_Klasifikasi_Bahan_3.Items.Add(Dr("Id_Klasifikasi_Bahan3"))
					Lv.SubItems.Add(Dr("Prefix_Klasifikasi_Bahan"))
					Lv.SubItems.Add(Dr("Kode_Klasifikasi_Bahan"))
					Lv.SubItems.Add(Dr("Keterangan"))
					If General_Class.CekNULL(Dr("Toleransi_Formula_GI_Min")) = "" Then
						Lv.SubItems.Add($"-")
					Else
						Lv.SubItems.Add($"{Format(Val(HilangkanTanda(Dr("Toleransi_Formula_GI_Min"))), "N2")} %")
					End If
					If General_Class.CekNULL(Dr("Toleransi_Formula_GI_Max")) = "" Then
						Lv.SubItems.Add($"-")
					Else
						Lv.SubItems.Add($"{Format(Val(HilangkanTanda(Dr("Toleransi_Formula_GI_Max"))), "N2")} %")
					End If
				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub GetCurrentPrefixKlasifikasi3()
		Txt_Klasifikasi3_Prefix.Text = ""
		SQL = $"
				select isnull(max(Prefix_Klasifikasi_Bahan), 0) as Prefix
				from N_EMI_Master_Klasifikasi_Bahan_3
				where Kode_Perusahaan = '{KodePerusahaan}'
			"
		Using Dr = OpenTrans(SQL)
			If Dr.Read Then
				Txt_Klasifikasi3_Prefix.Text = Val(HilangkanTanda(Dr("Prefix"))) + 1
			End If
		End Using
	End Sub

	Private Sub Btn_Klasifikasi3_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Klasifikasi3_Simpan.Click
		If Txt_Klasifikasi3_Kode.Text.Trim.Length = 0 Then
			MessageBox.Show("Kode Klasifikasi Tidak Boleh Kosong", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_Klasifikasi3_Kode.Focus()
			Exit Sub
		ElseIf Txt_Klasifikasi3_Keterangan.Text.Trim.Length = 0 Then
			MessageBox.Show("Keterangan Harus Diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_Klasifikasi3_Keterangan.Focus()
			Exit Sub
		ElseIf Txt_Klasifikasi3_Prefix.Text.Trim.Length = 0 Then
			MessageBox.Show("Terjadi Kesalaham, Prefix Tidak Boleh Kosong", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_Klasifikasi3_Prefix.Focus()
			Exit Sub
		ElseIf Txt_Toleransi_Min.Text.Trim.Length = 0 Then
			MessageBox.Show("Terjadi Kesalaham, Persentase Formula Min Tidak Boleh Kosong", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_Toleransi_Min.Focus()
			Exit Sub
		ElseIf Txt_Toleransi_Max.Text.Trim.Length = 0 Then
			MessageBox.Show("Terjadi Kesalaham,  Persentase Formula Max Tidak Boleh Kosong", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_Toleransi_Max.Focus()
			Exit Sub
		End If

		If (MessageBox.Show("Yakin Ingin Melakuakn Simpan Data Ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)) = vbNo Then Exit Sub

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			Dim Action As String = ""

			If Btn_Klasifikasi3_Simpan.Tag = "SIMPAN" Then

				GetCurrentPrefixKlasifikasi3()

				'===========================================
				'=     CEK APAKAH ADA PREFIX YANG SAMA     =
				'===========================================
				SQL = $"
					select 1
					from N_EMI_Master_Klasifikasi_Bahan_3
					where Kode_Perusahaan = '{KodePerusahaan}'
					and Prefix_Klasifikasi_Bahan = '{Txt_Klasifikasi3_Prefix.Text.Trim}'
				"
				Using Dr = OpenTrans(SQL)
					If Dr.Read Then
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show($"Data Klasifikasi Bahan 3 Dengan Prefix {Txt_Klasifikasi3_Prefix.Text.Trim} Sudah Ada", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				End Using

				'=======================
				'=     INSERT DATA     =
				'=======================
				SQL = $"
					insert into N_EMI_Master_Klasifikasi_Bahan_3(Kode_Perusahaan, Kode_Klasifikasi_Bahan, Prefix_Klasifikasi_Bahan, Keterangan, Toleransi_Formula_GI_Min, Toleransi_Formula_GI_Max)
					values ('{KodePerusahaan}', '{Txt_Klasifikasi3_Kode.Text.Trim}', '{Txt_Klasifikasi3_Prefix.Text.Trim}', '{Txt_Klasifikasi3_Keterangan.Text.Trim}',
					'{Val(HilangkanTanda(Txt_Toleransi_Min.Text.Trim))}', '{Val(HilangkanTanda(Txt_Toleransi_Max.Text.Trim))}')
				"
				ExecuteTrans(SQL)

				Action = "simpan"

			ElseIf Btn_Klasifikasi3_Simpan.Tag = "UPDATE" Then

				If Txt_Klasifikasi3_SelectedID.Text.Trim.Length = 0 Then
					CloseTrans()
					CloseConn()
					MessageBox.Show($"Terjadi Kesalahan, Harap Pilih Data Dahulu yang Ingin Diupdate", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If

				'===============================
				'=     CEK APAKAH ADA DATA     =
				'===============================
				SQL = $"
					select 1
					from N_EMI_Master_Klasifikasi_Bahan_3
					where Kode_Perusahaan = '{KodePerusahaan}'
					and Id_Klasifikasi_Bahan3 = '{Txt_Klasifikasi3_SelectedID.Text.Trim}'
				"
				Using Dr = OpenTrans(SQL)
					If Not Dr.Read Then
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show($"Terjadi Kesalahan, Data Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				End Using

				'=======================
				'=     UDPATE DATA     =
				'=======================
				SQL = $"
					update N_EMI_Master_Klasifikasi_Bahan_3
						set Keterangan = '{Txt_Klasifikasi3_Keterangan.Text.Trim}',
						Toleransi_Formula_GI_Min = '{Val(HilangkanTanda(Txt_Toleransi_Min.Text.Trim))}',
						Toleransi_Formula_GI_Max = '{Val(HilangkanTanda(Txt_Toleransi_Max.Text.Trim))}'
					where Kode_Perusahaan = '{KodePerusahaan}'
					and Id_Klasifikasi_Bahan3 = '{Txt_Klasifikasi3_SelectedID.Text.Trim}'
				"
				ExecuteTrans(SQL)

				Action = "update"
			End If

			Cmd.Transaction.Commit()
			CloseTrans()
			CloseConn()
			MessageBox.Show($"Data Berhasil Di{Action}", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Kosong_Klasifikasi_Bahan_3()
	End Sub

	Private Sub Btn_Klasifikasi3_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Klasifikasi3_Refresh.Click
		Kosong_Klasifikasi_Bahan_3()
	End Sub

	Private Sub Btn_Klasifikasi3_Hapus_Click(sender As Object, e As EventArgs) Handles Btn_Klasifikasi3_Hapus.Click
		If Txt_Klasifikasi3_SelectedID.Text.Trim.Length = 0 Then
			MessageBox.Show("Pilih Dahulu Data yang Ingin Dihapus", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If

		If (MessageBox.Show("Yakin Ingin Menghapus Data Ini???", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)) = vbNo Then Exit Sub

		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction

			'===============================
			'=     CEK APAKAH DATA ADA     =
			'===============================
			SQL = $"
					select 1
					from N_EMI_Master_Klasifikasi_Bahan_3
					where Kode_Perusahaan = '{KodePerusahaan}'
					and Id_Klasifikasi_Bahan3 = '{Txt_Klasifikasi3_SelectedID.Text.Trim}'
				"
			Using Dr = OpenTrans(SQL)
				If Not Dr.Read Then
					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show($"Terjadi Kesalahan, Data Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			'===================================================
			'=     CEK APAKAH DATA SUDAH DI PAKAI DIBARANG     =
			'===================================================
			SQL = $"
				select top 1 1
				from Barang
				where Kode_Perusahaan = '{KodePerusahaan}'
				and Id_Klasifikasi_Bahan3 = '{Txt_Klasifikasi3_SelectedID.Text.Trim}'
			"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show($"Terjadi Kesalahan, Data Tidak Bisa Dihapus Karena Sudah Dipakai pada Data Barang", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			'======================
			'=     HAPUS DATA     =
			'======================
			SQL = $"
				delete from N_EMI_Master_Klasifikasi_Bahan_3
				where Kode_Perusahaan = '{KodePerusahaan}'
				and Id_Klasifikasi_Bahan3 = '{Txt_Klasifikasi3_SelectedID.Text.Trim}'
			"
			ExecuteTrans(SQL)

			Cmd.Transaction.Commit()
			CloseTrans()
			CloseConn()
			MessageBox.Show("Data Berhasil Dihapus", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Kosong_Klasifikasi_Bahan_3()

	End Sub

	Private Sub Lv_Klasifikasi_Bahan_3_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Klasifikasi_Bahan_3.DoubleClick
		If Lv_Klasifikasi_Bahan_3.Items.Count = 0 Then Exit Sub

		GetDataLvKlasifikasi3(Lv_Klasifikasi_Bahan_3.FocusedItem.Index)

		Txt_Klasifikasi3_Kode.Text = Lv_Klasifikasi3_KodeKlasifikasi.Trim
		Txt_Klasifikasi3_Keterangan.Text = Lv_Klasifikasi3_Keterangan.Trim
		Txt_Klasifikasi3_Prefix.Text = Lv_Klasifikasi3_Prefix.Trim
		Txt_Klasifikasi3_SelectedID.Text = Lv_Klasifikasi3_ID.Trim

		If Lv_Klasifikasi3_Toleransi_Min.Trim = "-" Then
			Txt_Toleransi_Min.Text = ""
		Else
			Txt_Toleransi_Min.Text = Val(HilangkanTanda(Lv_Klasifikasi3_Toleransi_Min.Replace("%", "").Trim()))
		End If

		If Lv_Klasifikasi3_Toleransi_Min.Trim = "-" Then
			Txt_Toleransi_Max.Text = ""
		Else
			Txt_Toleransi_Max.Text = Val(HilangkanTanda(Lv_Klasifikasi3_Toleransi_Max.Replace("%", "").Trim))
		End If


		Txt_Klasifikasi3_Kode.Enabled = False

		Btn_Klasifikasi3_Simpan.Tag = "UPDATE"
		Btn_Klasifikasi3_Simpan.Text = "&Update"
	End Sub

	Private Sub Cmb_Klasifikasi3_Kolom_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Klasifikasi3_Kolom.SelectedIndexChanged
		If Cmb_Klasifikasi3_Kolom.SelectedIndex = -1 Then
			Txt_Klasifikasi3_Value.Enabled = False
		Else
			Txt_Klasifikasi3_Value.Enabled = True
		End If

		Txt_Klasifikasi3_Value.Text = ""
	End Sub

	Private Sub Btn_Klasifikasi3_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Klasifikasi3_Cari.Click
		Load_Data_Lv_Klasifikasi_Bahan_3()
	End Sub

	Private Sub Txt_Klasifikasi3_Kode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Klasifikasi3_Kode.KeyPress
		If e.KeyChar = Chr(13) Then Txt_Klasifikasi3_Keterangan.Focus()
	End Sub

	Private Sub Txt_Klasifikasi3_Keterangan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Klasifikasi3_Keterangan.KeyPress
		If e.KeyChar = Chr(13) Then Txt_Toleransi_Min.Focus()
	End Sub

	Private Sub Txt_Klasifikasi3_Value_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Klasifikasi3_Value.KeyPress
		If e.KeyChar = Chr(13) Then Btn_Klasifikasi3_Cari.Focus()
	End Sub

	Private Sub Txt_Toleransi_Min_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Toleransi_Min.KeyPress
		HandleFutureText(sender, e)
		If e.KeyChar = Chr(13) Then Txt_Toleransi_Max.Focus()
	End Sub

	Private Sub Txt_Toleransi_Max_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Toleransi_Max.KeyPress
		HandleFutureText(sender, e)
		If e.KeyChar = Chr(13) Then Btn_Klasifikasi3_Simpan.Focus()
	End Sub

#End Region

	'=========================================================================================================================
	'=     HELPER
	'=========================================================================================================================

#Region "HELPER"

	Private Sub LoadTabMenu()

		For Each tabName In menuItems

			Dim container As New FlowLayoutPanel With {
				.Tag = tabName,
				.FlowDirection = FlowDirection.TopDown,
				.AutoSize = True,
				.Margin = New Padding(2, 5, 2, 0),
				.Cursor = Cursors.Hand
			}

			Dim lbl As New Label With {
				.Text = tabName,
				.AutoSize = True,
				.Font = New Font("Work Sans SemiBold", 10, FontStyle.Bold),
				.ForeColor = Color.DimGray,
				.Padding = New Padding(0, 0, 0, 2)
			}

			Dim line As New Panel With {
				.Tag = lbl,
				.Height = 3,
				.Dock = DockStyle.Bottom,
				.BackColor = Color.Transparent
			}

			AddHandler lbl.Click, Sub(s, ev) SetActiveTab(line, lbl, tabName)
			AddHandler line.Click, Sub(s, ev) SetActiveTab(line, lbl, tabName)
			AddHandler container.Click, Sub(s, ev) SetActiveTab(line, lbl, tabName)

			container.Controls.Add(lbl)
			container.Controls.Add(line)
			FLPanel_Tab_Menu.Controls.Add(container)

			'Set Default Tab
			If tabName = "Klasifikasi Bahan" Then SetActiveTab(line, lbl, tabName)
		Next

	End Sub

	Private Sub SetActiveTab(line As Panel, lbl As Label, tag As String)

		If activeLine IsNot Nothing Then
			activeLine.BackColor = Color.Transparent
			DirectCast(activeLine.Tag, Label).ForeColor = Color.DimGray
		End If

		line.BackColor = Color.FromArgb(40, 80, 145)
		lbl.ForeColor = Color.FromArgb(40, 80, 145)

		activeLine = line
		HandleClickMenu(tag)

	End Sub

	Private Sub HandleFutureText(sender As Object, e As KeyPressEventArgs)
		If Char.IsControl(e.KeyChar) Then Return

		Dim txt As TextBox = DirectCast(sender, TextBox)

		If Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> "."c Then
			e.Handled = True
			Return
		End If

		If e.KeyChar = "."c AndAlso txt.Text.Contains(".") Then
			e.Handled = True
			Return
		End If

		Dim futureText As String = txt.Text.Remove(txt.SelectionStart, txt.SelectionLength).Insert(txt.SelectionStart, e.KeyChar)

		If futureText.Contains(".") Then
			Dim parts() As String = futureText.Split("."c)
			If parts.Length > 1 AndAlso parts(1).Length > 2 Then
				e.Handled = True
				Return
			End If
		End If

		Dim value As Double
		If Double.TryParse(futureText, value) OrElse futureText = "." Then

			If futureText.Length > 1 AndAlso futureText.StartsWith("0") AndAlso Not futureText.StartsWith("0.") Then
				e.Handled = True
				Return
			End If

			If value > 10000 Then
				e.Handled = True
			End If
		Else
			e.Handled = True
		End If
	End Sub

#End Region

	'=========================================================================================================================
	'=     UTILITY
	'=========================================================================================================================

#Region "UTILITY"

	Protected Overrides Sub WndProc(ByRef m As Message)
		' WM_NCLBUTTONDBLCLK = 0xA3 (double click di title bar)
		If m.Msg = &HA3 Then
			Return  ' Abaikan pesan, sehingga form tidak maximize
		End If

		MyBase.WndProc(m)
	End Sub

	Private Sub LvwKlasifikasiBahan_Data_MouseMove(sender As Object, e As MouseEventArgs) Handles LvwKlasifikasiBahan_Data.MouseMove
		HandleListViewHover(LvwKlasifikasiBahan_Data, e)
	End Sub

	Private Sub Lv_Klasifikasi_Bahan2_MouseMove(sender As Object, e As MouseEventArgs) Handles Lv_Klasifikasi_Bahan2.MouseMove
		HandleListViewHover(Lv_Klasifikasi_Bahan2, e)
	End Sub

	Private Sub Lv_Klasifikasi_Bahan_3_MouseMove(sender As Object, e As MouseEventArgs) Handles Lv_Klasifikasi_Bahan_3.MouseMove
		HandleListViewHover(Lv_Klasifikasi_Bahan_3, e)
	End Sub

	Private Sub HandleListViewHover(lvw As ListView, e As MouseEventArgs)

		Dim hit As ListViewHitTestInfo = lvw.HitTest(e.Location)
		Dim lastItem As ListViewItem = TryCast(lvw.Tag, ListViewItem)

		If hit.Item IsNot Nothing Then
			lvw.Cursor = Cursors.Hand
		Else
			lvw.Cursor = Cursors.Default
		End If

		If hit.Item Is Nothing Then

			If lastItem IsNot Nothing Then
				If lastItem.BackColor = Color.FromArgb(235, 235, 235) Then
					lastItem.BackColor = Color.White
				End If

				lvw.Tag = Nothing
			End If

			Exit Sub
		End If

		Dim currentItem = hit.Item

		If currentItem.Tag IsNot Nothing Then
			Exit Sub
		End If

		If lastItem IsNot currentItem Then

			lvw.BeginUpdate()

			If lastItem IsNot Nothing Then
				If lastItem.BackColor = Color.FromArgb(235, 235, 235) Then
					lastItem.BackColor = Color.White
				End If
			End If

			currentItem.BackColor = Color.FromArgb(235, 235, 235)

			lvw.Tag = currentItem

			lvw.EndUpdate()
		End If
	End Sub

	Private Sub EnableDoubleBuffer(lvw As ListView)
		Dim t As Type = lvw.GetType()
		Dim prop = t.GetProperty("DoubleBuffered", Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance)
		prop.SetValue(lvw, True, Nothing)
	End Sub

#End Region

End Class
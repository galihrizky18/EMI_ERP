Public Class SD_Pilih_Produk_Forecasting
	Public filter_tambahan, filter_kdSupplier As String
	Public asal As String
	Dim arrcari As New ArrayList
	Dim Jenis = "Tampil_Barang"
	Public urutcmb As Integer

	Dim arrKdBarang, arrIdKategoriGudang As New ArrayList

	Dim selectedKdBarang As New List(Of Object)()

	Private Sub kosong()
		Try
			OpenConn()

			'Lv_Barang.Items.Clear()
			'SQL = "select a.Kode_Barang,a.Nama,a.Kode_Kategori_Besar,a.Kode_Kategori_Kecil,a.Id_Kategori_Gudang "
			'SQL = SQL & "from Barang a,EMI_Group_Jenis b where "
			'SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Group_Jenis = b.Id_Group_Jenis "
			'SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and b.Flag_Finished_Good = 'Y' "
			'SQL = SQL & "group by a.Kode_Barang,a.Nama,a.Kode_Kategori_Besar,a.Kode_Kategori_Kecil,a.Id_Kategori_Gudang order by a.Nama "
			'Using dr = OpenTrans(SQL)
			'    Do While dr.Read
			'        Dim LV As New ListViewItem
			'        LV = Lv_Barang.Items.Add(dr("Kode_Barang"))
			'        LV.SubItems.Add(dr("Nama"))
			'        LV.SubItems.Add(dr("Id_Kategori_Gudang"))
			'    Loop
			'End Using

			selectedKdBarang.Clear() : arrKdBarang.Clear() : arrIdKategoriGudang.Clear()

			ComboBox1.Items.Clear()
			ComboBox2.Items.Clear()
			ComboBox1.Items.Add(Base_Language.Lang_Global_SeluruhCombobox)
			SQL = "select Kode_Kategori_Besar from Kategori_Besar where Kode_Perusahaan = '" & KodePerusahaan & "' order by Kode_Kategori_Besar "
			Using dr = OpenTrans(SQL)
				Do While dr.Read
					ComboBox1.Items.Add(dr("Kode_Kategori_Besar"))
				Loop
			End Using
			ComboBox1.SelectedIndex = 0
			CheckBox1.Checked = False
			'ComboBox2.SelectedIndex = -1

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub SD_Pilih_Barang_Activated(sender As Object, e As EventArgs) Handles Me.Activated
		My.Application.ChangeCulture("en-us")
		My.Application.ChangeUICulture("en-us")
	End Sub

	Private Sub SD_Pilih_Barang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		My.Application.ChangeCulture("en-us")
		My.Application.ChangeUICulture("en-us")

		Try
			OpenConn()

			'Base_Language.Get_Languages_Global(Bahasa_Pilihan)
			'Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)

			'LblPilihBarang_Judul.Text = Base_Language.Lang_TampilBarang_Judul

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub

		End Try
		Lv_Barang.Visible = True
		kosong()

	End Sub

	Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
		kosong()
	End Sub

	Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
		Try
			OpenConn()

			If ComboBox1.SelectedIndex = -1 Then
				ComboBox2.Items.Clear()
				Exit Sub
			End If

			ComboBox2.Items.Clear()
			ComboBox2.Items.Add(Base_Language.Lang_Global_SeluruhCombobox)
			SQL = "select Kode_Kategori_Kecil from Kategori_Kecil where Kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and Kode_Kategori_Besar = '" & ComboBox1.Text & "' order by Kode_Kategori_Kecil "
			Using dr = OpenTrans(SQL)
				Do While dr.Read
					ComboBox2.Items.Add(dr("Kode_Kategori_Kecil"))
				Loop
			End Using
			ComboBox2.SelectedIndex = 0
			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub

		End Try
		filter_data()
	End Sub

	Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
		''Forecast
		'Dim fboleh As Boolean = False
		'For a As Integer = 0 To Lv_Barang.Items.Count - 1
		'    If Lv_Barang.Items(a).Checked = True Then
		'        fboleh = True
		'        Exit For
		'    Else
		'        fboleh = False
		'    End If
		'Next

		'If fboleh = False Then
		'    MessageBox.Show("bahan belum di pilih.....!! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		'    ComboBox1.Focus()
		'    Exit Sub
		'End If

		'If EMI_Transaksi_ForecastOrder.DataGridView1.Rows.Count <> 0 Then
		'    For a As Integer = 0 To Lv_Barang.Items.Count - 1
		'        If Lv_Barang.Items(a).Checked = True Then
		'            For b As Integer = 0 To EMI_Transaksi_ForecastOrder.DataGridView1.Rows.Count - 1
		'                If Lv_Barang.Items(a).Text = EMI_Transaksi_ForecastOrder.DataGridView1.Rows(b).Cells(1).Value.ToString Then
		'                    MessageBox.Show("bahan sudah di pilih sebelumnya.....!! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		'                    Exit Sub
		'                End If
		'            Next
		'        End If
		'    Next
		'End If

		'Try
		'    OpenConn()

		'    EMI_Transaksi_ForecastOrder.Arrbarang.Clear()
		'    EMI_Transaksi_ForecastOrder.Arrlokasi.Clear()
		'    EMI_Transaksi_ForecastOrder.ArrNama.Clear()

		'    For z As Integer = 0 To Lv_Barang.Items.Count - 1
		'        If Lv_Barang.Items(z).Checked = True Then

		'            Dim fSO As String = ""
		'            SQL = "select Top(1)a.Lokasi_Gudang from EMI_Kategori_Gudang_PerLokasi a,Barang b where a.Kode_Perusahaan = b.Kode_Perusahaan "
		'            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.ID_Kategori_Gudang = b.Id_Kategori_Gudang and "
		'            SQL = SQL & "a.ID_Kategori_Gudang = '" & Lv_Barang.Items(z).SubItems(2).Text & "' and b.Kode_Barang = '" & Lv_Barang.Items(z).Text & "'"
		'            Using Ds = BindingTrans(SQL)
		'                With Ds.Tables("MyTable")
		'                    If .Rows.Count <> 0 Then
		'                        For i As Integer = 0 To .Rows.Count - 1
		'                            fSO = .Rows(i).Item("Lokasi_Gudang")
		'                        Next
		'                    End If
		'                End With
		'            End Using

		'            EMI_Transaksi_ForecastOrder.Arrbarang.Add(Lv_Barang.Items(z).SubItems(0).Text)
		'            EMI_Transaksi_ForecastOrder.Arrlokasi.Add(fSO)
		'            EMI_Transaksi_ForecastOrder.ArrNama.Add(Lv_Barang.Items(z).SubItems(1).Text)
		'        End If
		'    Next
		'    CloseConn()
		'Catch ex As Exception
		'    CloseConn()
		'    MessageBox.Show(ex.Message)
		'    Exit Sub
		'End Try

		Dim fboleh As Boolean = False
		'For i As Integer = 0 To selectedKdBarang.Count - 1
		'    If Lv_Barang.Items(i).Checked = True Then
		'        fboleh = True
		'        Exit For
		'    Else
		'        fboleh = False
		'    End If
		'Next

		If selectedKdBarang.Count > 0 Then
			fboleh = True
		Else
			fboleh = False
		End If

		If fboleh = False Then
			MessageBox.Show("bahan belum di pilih.....!! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			ComboBox1.Focus()
			Exit Sub
		End If

		'If EMI_Transaksi_ForecastOrder.DataGridView1.Rows.Count <> 0 Then
		'    For a As Integer = 0 To selectedKdBarang.Count - 1
		'        For b As Integer = 0 To EMI_Transaksi_ForecastOrder.DataGridView1.Rows.Count - 1
		'            Dim itemData As Object() = CType(selectedKdBarang(a), Object())
		'            If itemData(0) = EMI_Transaksi_ForecastOrder.DataGridView1.Rows(b).Cells(1).Value.ToString Then
		'                MessageBox.Show("bahan sudah di pilih sebelumnya.....!! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
		'                Exit Sub
		'            End If
		'        Next
		'    Next
		'End If

		Try
			OpenConn()

			'EMI_Transaksi_ForecastOrder.Arrbarang.Clear()
			'EMI_Transaksi_ForecastOrder.Arrlokasi.Clear()
			'EMI_Transaksi_ForecastOrder.ArrNama.Clear()

			For z As Integer = 0 To selectedKdBarang.Count - 1
				Dim itemData As Object() = CType(selectedKdBarang(z), Object())

				Dim fSO As String = ""
				SQL = "select Top(1)a.Lokasi_Gudang from EMI_Kategori_Gudang_PerLokasi a,Barang b where a.Kode_Perusahaan = b.Kode_Perusahaan "
				SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.ID_Kategori_Gudang = b.Id_Kategori_Gudang and "
				SQL = SQL & "a.ID_Kategori_Gudang = '" & itemData(1).ToString & "' and b.Kode_Barang = '" & itemData(0).ToString & "'"
				Using Ds = BindingTrans(SQL)
					With Ds.Tables("MyTable")
						If .Rows.Count <> 0 Then
							For i As Integer = 0 To .Rows.Count - 1
								fSO = .Rows(i).Item("Lokasi_Gudang")
							Next
						End If
					End With
				End Using

				'EMI_Transaksi_ForecastOrder.Arrbarang.Add(itemData(0).ToString)
				'EMI_Transaksi_ForecastOrder.Arrlokasi.Add(fSO)
				'EMI_Transaksi_ForecastOrder.ArrNama.Add(itemData(2).ToString)

			Next
			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		'EMI_Transaksi_ForecastOrder.Get_Barang()
		Me.Close()
	End Sub

	Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
		'UserID
		If ComboBox1.SelectedIndex = -1 Then
			MessageBox.Show("kategori besar " & Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			ComboBox1.Focus()
			Exit Sub
		ElseIf ComboBox2.SelectedIndex = -1 Then
			MessageBox.Show("kategori kecil " & Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			ComboBox2.Focus()
			Exit Sub
		End If
		filter_data()
	End Sub

	Private Sub filter_data()
		If ComboBox1.SelectedIndex = -1 Or ComboBox2.SelectedIndex = -1 Then
			Lv_Barang.Items.Clear()
			Exit Sub
		End If

		Try
			OpenConn()

			Lv_Barang.Items.Clear() : arrKdBarang.Clear()
			SQL = "select a.Kode_Barang,a.Nama,a.Kode_Kategori_Besar,a.Kode_Kategori_Kecil,a.Id_Kategori_Gudang "
			SQL = SQL & "from Barang a,EMI_Group_Jenis b where "
			SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Group_Jenis = b.Id_Group_Jenis "
			SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and b.Flag_Finished_Good = 'Y' "
			If ComboBox1.SelectedIndex <> 0 Then
				SQL = SQL & "and a.Kode_Kategori_Besar = '" & ComboBox1.Text & "' "
			End If

			If ComboBox2.SelectedIndex <> 0 Then
				SQL = SQL & "And a.Kode_Kategori_Kecil = '" & ComboBox2.Text & "' "
			End If
			SQL = SQL & "group by a.Kode_Barang,a.Nama,a.Kode_Kategori_Besar,a.Kode_Kategori_Kecil,a.Id_Kategori_Gudang order by a.Nama "
			Using dr = OpenTrans(SQL)
				Do While dr.Read
					Dim LV As New ListViewItem
					LV = Lv_Barang.Items.Add(dr("Kode_Barang"))
					LV.SubItems.Add(dr("Nama"))
					LV.SubItems.Add(dr("Id_Kategori_Gudang"))

					For i As Integer = 0 To selectedKdBarang.Count - 1
						'convert menjadi object agar bisa menggunakn contain
						Dim itemData As Object() = CType(selectedKdBarang(i), Object())

						If itemData.Contains(dr("Kode_Barang").ToString()) Then
							LV.Checked = True
							Exit For
						End If
					Next

					arrKdBarang.Add(dr("Kode_Barang"))
				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
		If CheckBox1.Checked = True Then
			For a As Integer = 0 To Lv_Barang.Items.Count - 1
				Lv_Barang.Items(a).Checked = True
			Next
		Else
			For a As Integer = 0 To Lv_Barang.Items.Count - 1
				Lv_Barang.Items(a).Checked = False
			Next
		End If
	End Sub

	Private Sub Txt_Nama_TextChanged(sender As Object, e As EventArgs) Handles Txt_Nama.TextChanged

		If ComboBox1.SelectedIndex = -1 Then
			MessageBox.Show("Pilih Dahulu Kategori Besar", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			kosong()
			Exit Sub
		ElseIf ComboBox2.SelectedIndex = -1 Then
			MessageBox.Show("Pilih Dahulu Kategori Besar", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			kosong()
			Exit Sub
		End If

		Try
			OpenConn()

			Lv_Barang.Items.Clear() : arrKdBarang.Clear()
			SQL = "select a.Kode_Barang,a.Nama,a.Kode_Kategori_Besar,a.Kode_Kategori_Kecil,a.Id_Kategori_Gudang "
			SQL = SQL & "from Barang a,EMI_Group_Jenis b where "
			SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Group_Jenis = b.Id_Group_Jenis "
			SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and b.Flag_Finished_Good = 'Y' "
			If ComboBox1.SelectedIndex <> 0 Then
				SQL = SQL & "and a.Kode_Kategori_Besar = '" & ComboBox1.Text & "' "
			End If

			If ComboBox2.SelectedIndex <> 0 Then
				SQL = SQL & "And a.Kode_Kategori_Kecil = '" & ComboBox2.Text & "' "
			End If
			SQL = SQL & "and a.Nama like '" & Txt_Nama.Text & "%' "
			SQL = SQL & "group by a.Kode_Barang,a.Nama,a.Kode_Kategori_Besar,a.Kode_Kategori_Kecil,a.Id_Kategori_Gudang order by a.Nama "
			Using dr = OpenTrans(SQL)
				Do While dr.Read
					Dim LV As New ListViewItem
					LV = Lv_Barang.Items.Add(dr("Kode_Barang"))
					LV.SubItems.Add(dr("Nama"))
					LV.SubItems.Add(dr("Id_Kategori_Gudang"))

					For i As Integer = 0 To selectedKdBarang.Count - 1
						Dim itemData As Object() = CType(selectedKdBarang(i), Object())

						If itemData.Contains(dr("Kode_Barang").ToString()) Then
							LV.Checked = True
							Exit For
						End If
					Next

					arrKdBarang.Add(dr("Kode_Barang"))
				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

	End Sub

	Private Sub Lv_Barang_Leave(sender As Object, e As EventArgs) Handles Lv_Barang.Leave
		If Lv_Barang.Items.Count = 0 Then Exit Sub

		selectedKdBarang.Clear()
		arrIdKategoriGudang.Clear()

		'For i As Integer = 0 To Lv_Barang.Items.Count - 1
		'    If Lv_Barang.Items(i).Checked = True Then
		'        selectedKdBarang.Add(Lv_Barang.Items(i).SubItems(0).Text)
		'        arrIdKategoriGudang.Add(Lv_Barang.Items(i).SubItems(2).Text)
		'    Else
		'        selectedKdBarang.Remove(Lv_Barang.Items(i).SubItems(0).Text)
		'    End If
		'Next

		selectedKdBarang.Clear()

		For i As Integer = 0 To Lv_Barang.Items.Count - 1
			If Lv_Barang.Items(i).Checked = True Then
				Dim itemData As Object() = {Lv_Barang.Items(i).SubItems(0).Text, Lv_Barang.Items(i).SubItems(2).Text, Lv_Barang.Items(i).SubItems(1).Text}

				selectedKdBarang.Add(itemData)
			End If

		Next

	End Sub

End Class
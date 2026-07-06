Public Class Form_Coding_Standar

	Dim Lv_Kolom_1, Lv_Kolom_2, Lv_Kolom_3, Lv_Kolom_4, Lv_Kolom_5, Lv_Kolom_6 As String

	Dim Item_Lv_Kolom_1 As Integer = 0
	Dim Item_Lv_Kolom_2 As Integer = 1
	Dim Item_Lv_Kolom_3 As Integer = 2
	Dim Item_Lv_Kolom_4 As Integer = 3
	Dim Item_Lv_Kolom_5 As Integer = 4
	Dim Item_Lv_Kolom_6 As Integer = 5

	Dim Dgv_KdBarang, Dgv_NmBarang, Dgv_Jumlah, Dgv_Satuan As String

	Dim Cell_KdBarang As Integer = 0
	Dim Cell_NmBarang As Integer = 1
	Dim Cell_Jumlah As Integer = 2
	Dim Cell_Satuan As Integer = 3

	Private Sub Form_Coding_Standar_Activated(sender As Object, e As EventArgs) Handles Me.Activated
		My.Application.ChangeCulture("en-us")
		My.Application.ChangeUICulture("en-us")
	End Sub

	Private Sub Form_Coding_Standar_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		My.Application.ChangeCulture("en-us")
		My.Application.ChangeUICulture("en-us")

		Listview.Columns.Clear()
		Listview.Columns.Add("Kode Barang", 120, HorizontalAlignment.Left)
		Listview.Columns.Add("Nama Barang", 250, HorizontalAlignment.Left)
		Listview.Columns.Add("Kolom Angka 1", 130, HorizontalAlignment.Right)
		Listview.Columns.Add("Kolom Angka 2", 130, HorizontalAlignment.Right)
		Listview.Columns.Add("Kolom Angka 3", 130, HorizontalAlignment.Right)
		Listview.Columns.Add("Kolom Flag", 0, HorizontalAlignment.Center)
		Listview.View = View.Details

		ComboBox1.Items.Clear()
		ComboBox1.Items.Add("Value 1")
		ComboBox1.Items.Add("Value 2")
		ComboBox1.Items.Add("Value 3")

		Kosong()
	End Sub

	Private Sub Kosong()

		TextBox1.Text = ""
		ComboBox1.SelectedIndex = -1

		LoadDataParent()
	End Sub

	Private Sub Get_Data_Lv(ByVal index As Integer)
		Lv_Kolom_1 = Listview.Items(index).SubItems(Item_Lv_Kolom_1).Text
		Lv_Kolom_2 = Listview.Items(index).SubItems(Item_Lv_Kolom_2).Text
		Lv_Kolom_3 = Listview.Items(index).SubItems(Item_Lv_Kolom_3).Text
		Lv_Kolom_4 = Listview.Items(index).SubItems(Item_Lv_Kolom_4).Text
		Lv_Kolom_5 = Listview.Items(index).SubItems(Item_Lv_Kolom_5).Text
		Lv_Kolom_6 = Listview.Items(index).SubItems(Item_Lv_Kolom_6).Text
	End Sub

	Private Sub Get_Data_Dgv(ByVal index As Integer)
		Dgv_KdBarang = DGV.Rows(index).Cells(Cell_KdBarang).Value
		Dgv_NmBarang = DGV.Rows(index).Cells(Cell_NmBarang).Value
		Dgv_Jumlah = DGV.Rows(index).Cells(Cell_Jumlah).Value
		Dgv_Satuan = DGV.Rows(index).Cells(Cell_Satuan).Value

	End Sub

	Private Sub LoadDataParent()
		Try
			OpenConn()

			'BUAT MASING MASING MENJADI SNIPPET
			'=====================
			'=     SHOW DATA     =
			'=====================
			Listview.BeginUpdate()     '--> AGAR MENGHENTIKAN PROSES REPAINTING SAAT DATA DI LOAD (MENINGKATKAN PERFORMA)
			Listview.Items.Clear()
			SQL = $"
				select Kode_Barang, Nama, Good_Stock, Flag_Non_Barcode from barang
			"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Do
						Dim Lv As ListViewItem
						Lv = Listview.Items.Add(Dr("Kode_Barang"))
						Lv.SubItems.Add(Dr("Nama"))
						Lv.SubItems.Add(Format(Dr("Good_Stock"), "N4")) 'Jika Stock, Gunakan Format N4
						Lv.SubItems.Add(Format(Dr("Good_Stock"), "N2")) 'Jika Uang, Gunakan Format N2
						Lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy")) 'Format Tanggal
						If General_Class.CekNULL(Dr("Flag_Non_Barcode")) = "" Then
							Lv.SubItems.Add("-")
						Else
							Lv.SubItems.Add(Dr("Flag_Non_Barcode"))
						End If
					Loop While Dr.Read
				Else
					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show("Terjadi Kesalahan Data Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If

			End Using
			Listview.EndUpdate()

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
		Try
			OpenConn()

			'=====================
			'=     SHOW DATA     =
			'=====================
			Listview.BeginUpdate()     '--> AGAR MENGHENTIKAN PROSES REPAINTING SAAT DATA DI LOAD (MENINGKATKAN PERFORMA)
			Listview.Items.Clear()
			SQL = $"
				select Kode_Barang, Nama, Good_Stock, Flag_Non_Barcode from barang
			"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Do
						Dim Lv As ListViewItem
						Lv = Listview.Items.Add(Dr("Kode_Barang"))
						Lv.SubItems.Add(Dr("Nama"))
						Lv.SubItems.Add(Format(Dr("Good_Stock"), "N4")) 'Jika Stock, Gunakan Format N4
						Lv.SubItems.Add(Format(Dr("Good_Stock"), "N2")) 'Jika Uang, Gunakan Format N2
						Lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy")) 'Format Tanggal
						If General_Class.CekNULL(Dr("Flag_Non_Barcode")) = "" Then
							Lv.SubItems.Add("-")
						Else
							Lv.SubItems.Add(Dr("Flag_Non_Barcode"))
						End If
					Loop While Dr.Read
				Else
					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show("Terjadi Kesalahan Data Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using
			Listview.EndUpdate()

			'===================================
			'=     SHOW DATA DENGAN FILTER     =
			'===================================
			Dim KdBarang As String = "1111006"
			Dim Filter As String = ""

			If True Then
				Filter &= $"and Kode_Barang = '{KdBarang}' "
			Else
				Filter &= $""
			End If

			Listview.BeginUpdate()     '--> AGAR MENGHENTIKAN PROSES REPAINTING SAAT DATA DI LOAD (MENINGKATKAN PERFORMA)
			Listview.Items.Clear()
			SQL = $"
				select Kode_Barang, Nama, Good_Stock, Flag_Non_Barcode from barang
				where kode_perusahaan = '{KodePerusahaan}'
				{Filter}
			"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Do
						Dim Lv As ListViewItem
						Lv = Listview.Items.Add(Dr("Kode_Barang"))
						Lv.SubItems.Add(Dr("Nama"))
						Lv.SubItems.Add(Format(Dr("Good_Stock"), "N4")) 'Jika Stock, Gunakan Format N4
						Lv.SubItems.Add(Format(Dr("Good_Stock"), "N2")) 'Jika Uang, Gunakan Format N2
						Lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy")) 'Format Tanggal
						If General_Class.CekNULL(Dr("Flag_Non_Barcode")) = "" Then
							Lv.SubItems.Add("-")
						Else
							Lv.SubItems.Add(Dr("Flag_Non_Barcode"))
						End If
					Loop While Dr.Read
				Else
					Dr.Close()
					CloseTrans()
					CloseConn()
					MessageBox.Show("Terjadi Kesalahan Data Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using
			Listview.EndUpdate()

			'====================
			'=     CEK DATA     =
			'====================
			Dim KdBarang1 As String = "1111006"
			SQL = $"
				select Kode_Barang, Nama, Good_Stock, Flag_Non_Barcode from barang
				where kode_perusahaan = '{KodePerusahaan}'
				and Kode_Barang = '{KdBarang1}'
			"
			Using Dr = OpenTrans(SQL)
				If Dr.Read Then
					Dr.Close()
					CloseConn()
					MessageBox.Show($"Data Pada Kode Barang {KdBarang1} Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				Else
					Dr.Close()
					CloseConn()
					MessageBox.Show($"Data Pada Kode Barang {KdBarang1} Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
					Exit Sub
				End If
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
		Try
			OpenConn()

			'=====================
			'=     SHOW DATA     =
			'=====================
			Dim KdBarang1 As String = "1111006"

			DGV.SuspendLayout()
			DGV.Rows.Clear()
			SQL = $"
				select Kode_Barang, Nama, Good_Stock, Satuan from barang
				where Kode_Perusahaan = '{KodePerusahaan}'
				and Kode_Barang = '{KdBarang1}'
			"
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count <> 0 Then
						For i As Integer = 0 To .Rows.Count - 1

							DGV.Rows.Add()
							DGV.Rows(i).Cells(0).Value = .Rows(i).Item("Kode_Barang")
							DGV.Rows(i).Cells(1).Value = .Rows(i).Item("Nama")
							DGV.Rows(i).Cells(2).Value = Format(.Rows(i).Item("Good_Stock"), "N4")
							DGV.Rows(i).Cells(3).Value = Format(.Rows(i).Item("Tanggal"), "dd MMM yyyy")
							DGV.Rows(i).Cells(4).Value = .Rows(i).Item("Satuan")

						Next
					Else
						CloseConn()
						MessageBox.Show($"Terjadi Kesalahan, Data Kode Barang {KdBarang1} Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				End With
			End Using
			DGV.ResumeLayout()

			'===================================
			'=     SHOW DATA DENGAN FILTER     =
			'===================================
			Dim KdBarang As String = "1111006"
			Dim Filter As String = ""

			If True Then
				Filter &= $"and Kode_Barang = '{KdBarang}' "
			End If
			SQL = $"
				select Kode_Barang, Nama, Good_Stock, Flag_Non_Barcode from barang
				where kode_perusahaan = '{KodePerusahaan}'
				{Filter}
			"
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count <> 0 Then
						For i As Integer = 0 To .Rows.Count - 1

							DGV.Rows.Add()
							DGV.Rows(i).Cells(0).Value = .Rows(i).Item("Kode_Barang")
							DGV.Rows(i).Cells(1).Value = .Rows(i).Item("Nama")
							DGV.Rows(i).Cells(2).Value = Format(.Rows(i).Item("Good_Stock"), "N4")
							If General_Class.CekNULL(.Rows(i).Item("Flag_Non_Barcode")) = "" Then
								DGV.Rows(i).Cells(3).Value = "-"
							Else
								DGV.Rows(i).Cells(3).Value = .Rows(i).Item("Flag_Non_Barcode")
							End If

						Next
					Else
						CloseConn()
						MessageBox.Show($"Terjadi Kesalahan, Data Kode Barang {KdBarang} Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				End With
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
		If TextBox1.Text.Trim.Length = 0 Then
			MessageBox.Show("Value 1 Harus Diisi Terlebih Dahulu", jumlah_tampil_barang, MessageBoxButtons.OK, MessageBoxIcon.Warning)
			TextBox1.Focus()
			Exit Sub
		ElseIf ComboBox1.SelectedIndex = -1 Then
			MessageBox.Show("Combobox Harus Dipilih Terlebih Dahulu", jumlah_tampil_barang, MessageBoxButtons.OK, MessageBoxIcon.Warning)
			ComboBox1.DroppedDown = True
			ComboBox1.Focus()
			Exit Sub
		End If

		If MessageBox.Show("Yakin Ingin Melakukan Simpan Data Ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = vbNo Then Exit Sub

		get_jam()
		Try
			OpenConn()
			Cmd.Transaction = Cn.BeginTransaction
			Get_No_Faktur()

			Dim Action As String = ""
			If Button3.Tag = "SIMPAN" Then

				If CekButtonRole("CEK_BUTTON_ROLE_SIMPAN") = "T" Then
					CloseTrans()
					CloseConn()
					MessageBox.Show("Anda Tidak Memiliki Akses Untuk Melakukan Simpan Data Ini", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
					Exit Sub
				End If

				'============================================
				'=     CEK APAKAH KODE BARANG SUDAH ADA     =
				'============================================
				SQL = $"
					select top 1 1 from barang
					where Kode_Perusahaan = '{KodePerusahaan}' and Kode_Barang = '{TextBox1.Text.Trim}'
				"
				Using Dr = OpenTrans(SQL)
					If Dr.Read Then
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show($"Terjadi Kesalahan. Kode Barang {TextBox1.Text.Trim} Sudah Ada. Harap Ulangi Transaksi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				End Using

				'=======================
				'=     INSERT DATA     =
				'=======================
				Dim Value1 As String = ""
				Dim Value2 As String = ""
				Dim Value3 As Double = ""
				SQL = $"
					insert into barangss (Kode_Perusahaan, Kode_Barang, Nama, Good_Stocks)
					like ('{KodePerusahaan}', '{Value1.Trim}', '{Value2.Trim}', {Val(HilangkanTanda(Value3))})
				"
				ExecuteTrans(SQL)

				Action = "Disimpan"

			ElseIf Button3.Tag = "UPDATE" Then
				If CekButtonRole("CEK_BUTTON_ROLE_UPDATE") = "T" Then
					CloseTrans()
					CloseConn()
					MessageBox.Show("Anda Tidak Memiliki Akses Untuk Melakukan Update Data Ini", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
					Exit Sub
				End If

				'======================================
				'=     CEK APAKAH KODE BARANG ADA     =
				'======================================
				SQL = $"
					select top 1 1 from barang
					where Kode_Perusahaan = '{KodePerusahaan}' and Kode_Barang = '{TextBox1.Text.Trim}'
				"
				Using Dr = OpenTrans(SQL)
					If Not Dr.Read Then
						Dr.Close()
						CloseTrans()
						CloseConn()
						MessageBox.Show($"Terjadi Kesalahan. Kode Barang {TextBox1.Text.Trim} Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Exit Sub
					End If
				End Using

				'=======================
				'=     UPDATE DATA     =
				'=======================
				Dim Value1 As String = ""
				Dim Value2 As String = ""
				Dim Value3 As Double = ""
				SQL = $"
					Update barangss Set Nama = '{Value2.Trim}', Good_Stocks = {Val(HilangkanTanda(Value3))}
					where Kode_Perusahaan = '{KodePerusahaan}' and Kode_Barang = '{TextBox1.Text.Trim}'
				"
				ExecuteTrans(SQL)

				Action = "Diupdate"
			End If

			Cmd.Transaction.Commit()
			CloseTrans()
			CloseConn()
			MessageBox.Show($"Data Berhasil {Action}", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Kosong()
	End Sub

	Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
		Listview_DoubleClick(sender, e)
	End Sub

	Private Sub Listview_DoubleClick(sender As Object, e As EventArgs) Handles Listview.DoubleClick
		If Listview.Items.Count = 0 Or Listview.FocusedItem Is Nothing Then Exit Sub

		Dim SelectedIndex As Integer = Listview.FocusedItem.Index
		Get_Data_Lv(SelectedIndex)

		Txt_Faktur.Text = Lv_Kolom_2.Trim
		TextBox1.Text = Val(HilangkanTanda(Lv_Kolom_1.Trim))
		ComboBox1.Text = Lv_Kolom_3.Trim

	End Sub

	Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
		DGV_CellDoubleClick(sender, e)
	End Sub

	Private Sub DGV_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGV.CellDoubleClick
		If DGV.Rows.Count = 0 Or e.RowIndex < 0 Then Exit Sub

		Dim SelectedIndex As Integer = DGV.CurrentRow.Index
		Get_Data_Dgv(SelectedIndex)

		Txt_Faktur.Text = Dgv_KdBarang.Trim
		ComboBox1.Text = Dgv_NmBarang.Trim
		TextBox1.Text = Val(HilangkanTanda(Dgv_Jumlah.Trim))

	End Sub

	Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
		Listview_SelectedIndexChanged(sender, e)
	End Sub

	Private Sub Listview_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Listview.SelectedIndexChanged
		If Listview.Items.Count = 0 Or Listview.FocusedItem Is Nothing Then Exit Sub

		Dim SelectedIndex As Integer = Listview.FocusedItem.Index
		Get_Data_Lv(SelectedIndex)

		Try
			OpenConn()

			'=======================================
			'=     HANDLE SHOW LISTVIEW DETAIL     =
			'=======================================

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

	End Sub

	Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
		DGV_CellClick(sender, e)
	End Sub

	Private Sub DGV_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGV.CellClick
		If DGV.Rows.Count = 0 Or e.RowIndex < 0 Then Exit Sub

		Dim SelectedIndex As Integer = DGV.CurrentRow.Index
		Get_Data_Dgv(SelectedIndex)

		Try
			OpenConn()

			'===========================================
			'=     HANDLE SHOW DATAGRIDVIEW DETAIL     =
			'===========================================

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	'============================================================================================================================================================
	'=     UTILITY
	'============================================================================================================================================================

	Private Sub Get_No_Faktur()
		Dim IntialFaktur As String = "3HURUFINITIAL-"
		Txt_Faktur.Text = IntialFaktur & Format(tgl_skg, "MMyy") & "-" &
							General_Class.Get_Last_Number2("NAMA_TABEL", "KOLOM_TRANSAKSI/FAKTUR", 5,
							"Kode_perusahaan", KodePerusahaan,
							"And", "substring(KOLOM_TRANSAKSI/FAKTUR, 1, " & Len(IntialFaktur) + 4 & ")", IntialFaktur & Format(tgl_skg, "MMyy"))
	End Sub

	Private Sub sadasda()

		Dim JumlahBudget As Double = 10000
		Dim Jumlah As Double = Val(HilangkanTanda(JumlahBudget))

	End Sub

End Class
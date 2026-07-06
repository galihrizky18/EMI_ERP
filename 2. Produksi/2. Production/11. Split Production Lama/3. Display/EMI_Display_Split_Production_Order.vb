Public Class EMI_Display_Split_Production_Order

	Dim Arr1, Arr2, Arr3, Arr4, ArrValueParamLain As New ArrayList
	Dim pertama As Integer = 1
	Dim T As Color = Color.Blue
	Dim KT As Color = Color.Red
	Dim KY As Color = Color.Green
	Dim Batal As Color = Color.Black

	Private Sub Display_Pembelian_Barang_Masuk_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		kosong()
	End Sub

	Private Sub kosong()

		Try
			OpenConn()
			Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
			Base_Language.Get_Languages(Bahasa_Pilihan, "Display_Barang_Masuk")
			Base_Language.Get_Languages(Bahasa_Pilihan, "Pembelian_Barang_Masuk")
			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Lv_SplitProdOrder.Items.Clear()
		Lv_SplitProdOrder.Columns.Add(Base_Language.Lang_Global_No_Transaksi, 125, HorizontalAlignment.Left)
		Lv_SplitProdOrder.Columns.Add(Base_Language.Lang_Global_No_PO, 125, HorizontalAlignment.Left)
		Lv_SplitProdOrder.Columns.Add(Base_Language.Lang_Global_Lokasi, 0, HorizontalAlignment.Center)
		Lv_SplitProdOrder.Columns.Add("UserID", 100, HorizontalAlignment.Center)
		Lv_SplitProdOrder.Columns.Add("Kode Stock Owner", 0, HorizontalAlignment.Center)
		Lv_SplitProdOrder.Columns.Add(Base_Language.Lang_Global_KodeBarang, 100, HorizontalAlignment.Left)
		Lv_SplitProdOrder.Columns.Add(Base_Language.Lang_Global_NamaBarang, 250, HorizontalAlignment.Left)
		Lv_SplitProdOrder.Columns.Add(Base_Language.Lang_Global_Jumlah, 100, HorizontalAlignment.Left)
		Lv_SplitProdOrder.Columns.Add(Base_Language.Lang_Global_Satuan, 80, HorizontalAlignment.Left)
		Lv_SplitProdOrder.Columns.Add(Base_Language.Lang_Global_Catatan, 0, HorizontalAlignment.Left)
		Lv_SplitProdOrder.Columns.Add("Flag Produksi", 0, HorizontalAlignment.Center) 'NULLable
		Lv_SplitProdOrder.Columns.Add(Base_Language.Lang_Global_Tanggal_Produksi, 120, HorizontalAlignment.Center) 'NULLable
		Lv_SplitProdOrder.Columns.Add(Base_Language.Lang_Jam_Produksi, 120, HorizontalAlignment.Center) 'NULLable
		Lv_SplitProdOrder.Columns.Add("No Batch", 150, HorizontalAlignment.Left) 'NULLable
		Lv_SplitProdOrder.Columns.Add("Operator", 150, HorizontalAlignment.Left) 'NULLable
		Lv_SplitProdOrder.Columns.Add("Flag Hasil Produksi", 0, HorizontalAlignment.Center) 'NULLable
		Lv_SplitProdOrder.Columns.Add(Base_Language.Lang_Tgl_Hasil_Produksi, 150, HorizontalAlignment.Center) 'NULLable
		Lv_SplitProdOrder.Columns.Add(Base_Language.Lang_Jam_Hasil_Produksi, 150, HorizontalAlignment.Center) 'NULLable
		Lv_SplitProdOrder.View = View.Details

		Try
			OpenConn()

			Cmb_Lokasi.Items.Clear()
			Cmb_Lokasi.Items.Add(Base_Language.Lang_Global_SeluruhCombobox)

			SQL = "Select kode_stock_owner From "
			SQL = SQL & "stock_owner where kode_perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "order by kode_stock_owner"
			Using dr = OpenTrans(SQL)
				Do While dr.Read
					Cmb_Lokasi.Items.Add(dr("kode_stock_owner"))
				Loop
			End Using

			Cmb_Lokasi.Text = Lokasi

			Cmb_ParamTgl.Items.Clear() : Arr1.Clear()
			Cmb_ParamTgl.Items.Add(Base_Language.Lang_Global_Tanggal) : Arr1.Add("Tanggal")

			Cmb_ParamTgl.Enabled = False : Cmb_ParamLain.Enabled = False
			DateTimePicker1.Enabled = False : DateTimePicker2.Enabled = False
			TextBox4.Enabled = False

			Cmb_ParamLain.Items.Clear() : Cmb_ParamLain.Text = "" : Arr2.Clear()
			Cmb_ParamLain.Items.Add(Base_Language.Lang_Global_No_Transaksi) : Arr2.Add("a.no_transaksi")
			Cmb_ParamLain.Items.Add(Base_Language.Lang_Global_Mulai_Produksi) : Arr2.Add("a.flag_produksi")
			Cmb_ParamLain.Items.Add(Base_Language.Lang_Global_Selesai_Produksi) : Arr2.Add("a.Flag_Selesai_Produksi")

			Cmb_ValueParamLain.Items.Add(Base_Language.Lang_Global_Error_Ya) : ArrValueParamLain.Add(" = 'Y' ")
			Cmb_ValueParamLain.Items.Add(Base_Language.Lang_Global_Error_Tidak) : ArrValueParamLain.Add(" is null ")

			Label1.Text = "Display - Split Production Order"
			Cb_TransaksiHrIni.Text = Base_Language.Lang_Global_Hari_ini
			Cb_ParamTgl.Text = Base_Language.Lang_Global_Para_Tbl
			Cb_ParamLain.Text = Base_Language.Lang_Global_Para_lain
			Btn_Cari.Text = Base_Language.Lang_Global_Cari
			CloseConn()
		Catch ex As Exception
			Cmb_Lokasi.Items.Clear()
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

	End Sub

	Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles Cb_TransaksiHrIni.CheckedChanged
		If Cb_TransaksiHrIni.Checked = True Then
			Cb_ParamTgl.Checked = False
			BtnBarangMasuk_Cari_Click(Cb_TransaksiHrIni, e)
		End If
	End Sub

	Private Sub BtnBarangMasuk_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
		Try
			pertama = 1

			If Cb_ParamTgl.Checked = False And Cb_ParamLain.Checked = False And Cb_TransaksiHrIni.Checked = False Then
				MessageBox.Show(Base_Language.Lang_Global_Error_Paramater, Judul)
				Cb_ParamTgl.Focus() : Exit Sub
			End If

			If Cb_ParamTgl.Checked Then

				If Cmb_ParamTgl.SelectedIndex = -1 Then
					MessageBox.Show(Base_Language.Lang_Global_Error_Paramater_Tgl, Judul)
					Cmb_ParamTgl.Focus() : Exit Sub
				ElseIf DateTimePicker1.Value > DateTimePicker2.Value Then
					MessageBox.Show("Periode I " & Base_Language.Lang_Global_TidakBolehLebihDari & " Periode II!", Judul)
					DateTimePicker1.Value = Now.Date : DateTimePicker2.Value = Now.Date
					Exit Sub
				End If

			ElseIf Cb_ParamLain.Checked Then

				If Cmb_ParamLain.SelectedIndex = -1 Then
					MessageBox.Show(Base_Language.Lang_Global_Error_Paramater_Lain, Judul)
					Cmb_ParamLain.Focus() : Exit Sub
				ElseIf Cmb_ParamLain.SelectedIndex = 0 Then
					If TextBox4.Text.Trim.Length = 0 Then
						MessageBox.Show(Base_Language.Lang_Global_Error_Paramater_Lain2, Judul)
						TextBox4.Focus() : Exit Sub
					End If
				ElseIf Cmb_ParamLain.SelectedIndex = 1 Then
					If Cmb_ValueParamLain.SelectedIndex = -1 Then
						MessageBox.Show(Base_Language.Lang_Global_Error_Paramater_Lain2, Judul)
						Cmb_ValueParamLain.Focus() : Exit Sub
					End If
				ElseIf Cmb_ParamLain.SelectedIndex = 2 Then
					If Cmb_ValueParamLain.SelectedIndex = -1 Then
						MessageBox.Show(Base_Language.Lang_Global_Error_Paramater_Lain2, Judul)
						Cmb_ValueParamLain.Focus() : Exit Sub
					End If
				End If

			End If

			OpenConn()

			Lv_SplitProdOrder.Items.Clear()

			SQL = "select a.No_Transaksi,a.No_PO,a.Lokasi,a.Tanggal,a.Jam,a.UserID, "
			SQL = SQL & "a.Kode_Stock_Owner,a.Kode_Barang,b.Nama as Nama_Barang,a.Jumlah,a.Satuan, "
			SQL = SQL & "a.Catatan,a.Flag_Produksi,a.Tgl_Produksi,a.Jam_Produksi,a.No_Batch,a.Operator, "
			SQL = SQL & "a.Flag_Selesai_Produksi,a.Tgl_Selesai_Produksi,a.Jam_Selesai_Produksi,a.UserID_Selesai_Produksi, "
			SQL = SQL & "a.Flag_Hasil_Produksi,a.Tgl_Hasil_Produksi,a.Jam_Hasil_Produksi, a.Status "
			SQL = SQL & "from Emi_Split_Production_Order a, barang b "
			SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Barang = b.Kode_Barang "
			SQL = SQL & "and a.Kode_Stock_Owner = b.Kode_Stock_Owner "

			If Cb_TransaksiHrIni.Checked Then
				If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

				SQL = SQL & " tanggal between '"
				SQL = SQL & Format(Now, "yyyy-MM-dd") & "' and '" & Format(Now, "yyyy-MM-dd") & "' "
			End If

			If Cb_ParamTgl.Checked Then
				If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "and "

				SQL = SQL & Arr1.Item(Cmb_ParamTgl.SelectedIndex) & " between '"
				SQL = SQL & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' and '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' "
			End If

			If Cb_ParamLain.Checked Then

				If Cmb_ParamLain.SelectedIndex = 0 Then 'No Transaksi
					If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "
					SQL = SQL & Arr2.Item(Cmb_ParamLain.SelectedIndex) & " like '%" & Trim(TextBox4.Text) & "%' "
				ElseIf Cmb_ParamLain.SelectedIndex = 1 Then 'Mulai Produksi
					If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "
					SQL = SQL & Arr2.Item(Cmb_ParamLain.SelectedIndex) & ArrValueParamLain.Item(Cmb_ValueParamLain.SelectedIndex)
				ElseIf Cmb_ParamLain.SelectedIndex = 2 Then 'Selesai Produksi
					If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "
					SQL = SQL & Arr2.Item(Cmb_ParamLain.SelectedIndex) & ArrValueParamLain.Item(Cmb_ValueParamLain.SelectedIndex)
				End If

			End If

			SQL = SQL & "order by a.tanggal , a.jam"

			Dim Lvw As ListViewItem

			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count <> 0 Then
						For i As Integer = 0 To .Rows.Count - 1
							Lvw = Lv_SplitProdOrder.Items.Add(.Rows(i).Item("No_Transaksi"))
							Lvw.SubItems.Add(.Rows(i).Item("No_PO"))
							Lvw.SubItems.Add(.Rows(i).Item("Lokasi"))
							Lvw.SubItems.Add(.Rows(i).Item("UserID"))
							Lvw.SubItems.Add(.Rows(i).Item("Kode_Stock_Owner"))
							Lvw.SubItems.Add(.Rows(i).Item("Kode_Barang"))
							Lvw.SubItems.Add(.Rows(i).Item("Nama_Barang"))
							Lvw.SubItems.Add(.Rows(i).Item("Jumlah"))
							Lvw.SubItems.Add(.Rows(i).Item("Satuan"))

							If General_Class.CekNULL(.Rows(i).Item("Catatan")) = "" Then
								Lvw.SubItems.Add("-")
							Else
								Lvw.SubItems.Add(.Rows(i).Item("Catatan"))
							End If

							If General_Class.CekNULL(.Rows(i).Item("Flag_Produksi")) = "" Then
								Lvw.SubItems.Add("-")
							Else
								Lvw.SubItems.Add(.Rows(i).Item("Flag_Produksi"))
							End If

							If General_Class.CekNULL(.Rows(i).Item("Tgl_Produksi")) = "" Then
								Lvw.SubItems.Add("-")
							Else
								Lvw.SubItems.Add(Format(.Rows(i).Item("Tgl_Produksi"), "dd MMM yyyy"))
							End If

							If General_Class.CekNULL(.Rows(i).Item("Jam_Produksi")) = "" Then
								Lvw.SubItems.Add("-")
							Else
								Lvw.SubItems.Add(.Rows(i).Item("Jam_Produksi"))
							End If

							If General_Class.CekNULL(.Rows(i).Item("No_Batch")) = "" Then
								Lvw.SubItems.Add("-")
							Else
								Lvw.SubItems.Add(.Rows(i).Item("No_Batch"))
							End If

							If General_Class.CekNULL(.Rows(i).Item("Operator")) = "" Then
								Lvw.SubItems.Add("-")
							Else
								Lvw.SubItems.Add(.Rows(i).Item("Operator"))
							End If

							If General_Class.CekNULL(.Rows(i).Item("Flag_Hasil_Produksi")) = "" Then
								Lvw.SubItems.Add("-")
							Else
								Lvw.SubItems.Add(.Rows(i).Item("Flag_Hasil_Produksi"))
							End If

							If General_Class.CekNULL(.Rows(i).Item("Tgl_Hasil_Produksi")) = "" Then
								Lvw.SubItems.Add("-")
							Else
								Lvw.SubItems.Add(Format(.Rows(i).Item("Tgl_Hasil_Produksi"), "dd MMM yyyy"))
							End If

							If General_Class.CekNULL(.Rows(i).Item("Jam_Hasil_Produksi")) = "" Then
								Lvw.SubItems.Add("-")
							Else
								Lvw.SubItems.Add(.Rows(i).Item("Jam_Hasil_Produksi"))
							End If

							If Not General_Class.CekNULL(.Rows(i).Item("Status")) = "" Then
								Lvw.BackColor = Color.FromArgb(242, 139, 130)
							End If

						Next
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

	Private Sub CopyNoTransaksiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyNoTransaksiToolStripMenuItem.Click
		If Lv_SplitProdOrder.Items.Count = 0 Or Lv_SplitProdOrder.SelectedItems.Count = 0 Then
			MessageBox.Show(Base_Language.Lang_Pilih_Dahulu_No_Transaksi, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Exit Sub
		End If

		Clipboard.SetText(Lv_SplitProdOrder.FocusedItem.Text)
	End Sub

	Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_ParamLain.SelectedIndexChanged
		If Cmb_ParamLain.SelectedIndex = 0 Then
			TextBox4.Visible = True
			Cmb_ValueParamLain.Visible = False
			Cmb_ValueParamLain.SelectedIndex = -1
			TextBox4.Text = ""
		ElseIf Cmb_ParamLain.SelectedIndex = 1 Then
			TextBox4.Visible = False
			Cmb_ValueParamLain.Visible = True
			Cmb_ValueParamLain.SelectedIndex = -1
			TextBox4.Text = ""
		ElseIf Cmb_ParamLain.SelectedIndex = 2 Then
			TextBox4.Visible = False
			Cmb_ValueParamLain.Visible = True
			Cmb_ValueParamLain.SelectedIndex = -1
			TextBox4.Text = ""
		End If
	End Sub

	Private Sub DisplayRakToolStripMenuItem_Click(sender As Object, e As EventArgs)
		If Lv_SplitProdOrder.Items.Count = 0 Or Lv_SplitProdOrder.SelectedItems.Count = 0 Then
			Exit Sub
		End If
		EMI_Barang_Masuk_Display_Rak.TxtNoBM.Text = Lv_SplitProdOrder.FocusedItem.Text
		EMI_Barang_Masuk_Display_Rak.ShowDialog()
	End Sub

	Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles Cb_ParamTgl.CheckedChanged
		If Cb_ParamTgl.Checked Then
			Cmb_ParamTgl.Enabled = True : DateTimePicker1.Enabled = True : DateTimePicker2.Enabled = True
			Cb_TransaksiHrIni.Checked = False
		Else
			Cmb_ParamTgl.Enabled = False : DateTimePicker1.Enabled = False : DateTimePicker2.Enabled = False
			Cmb_ParamTgl.SelectedIndex = -1 : DateTimePicker1.Value = Now.Date : DateTimePicker2.Value = Now.Date
		End If
	End Sub

	Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles Cb_ParamLain.CheckedChanged
		If Cb_ParamLain.Checked Then
			Cmb_ParamLain.Enabled = True : TextBox4.Enabled = True
		Else
			Cmb_ParamLain.Enabled = False : TextBox4.Enabled = False
			Cmb_ParamLain.SelectedIndex = -1 : TextBox4.Text = ""
		End If
	End Sub

	Private Sub BatalToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BatalToolStripMenuItem.Click
		If Lv_SplitProdOrder.Items.Count = 0 Then Exit Sub

		Try
			OpenConn()

			Dim SelectedFaktur As String = Lv_SplitProdOrder.FocusedItem.SubItems(0).Text

			'========================================
			'=     CEK APAKAH PO SUDAH BERJALAN     =
			'========================================
			SQL = "select Kode_Perusahaan from Emi_Production_Results where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Production_Order = '" & SelectedFaktur & "' and Status is null "
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count <> 0 Then
						CloseConn()
						MessageBox.Show("Tidak Bisa Membatalkan PO yang sudah Mulai Produksi")
						Exit Sub
					Else
						SQL = "update Emi_Split_Production_Order set Status='Y' where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Transaksi = '" & SelectedFaktur & "'"
						ExecuteTrans(SQL)
					End If
				End With
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		BtnBarangMasuk_Cari_Click(sender, e)
	End Sub

End Class
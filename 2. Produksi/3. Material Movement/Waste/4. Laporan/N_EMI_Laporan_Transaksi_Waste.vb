Public Class N_EMI_Laporan_Transaksi_Waste

	Dim ArrJenisBarang, ArrJenisTransaksi As New ArrayList

	Dim Switch_Auto_Complete As Boolean = False

	Dim arrIndexRekap As New ArrayList From {0, 2}

	Dim ArrFilterJenis As New List(Of (JenisLaporan As String, ValueCombobox As String, JenisSql As String)) From {
		("PEMUSNAHAN_PRODUCTION", "Goods Received 1", "Goods Received 1"),
		("PEMUSNAHAN_PRODUCTION", "Sampel", "Waste Sampel"),
		("PEMUSNAHAN_WASTE_STORAGE", "Pemindahan Waste", "Pemindahan Waste"),
		("PEMINDAHAN", "Goods Received 2", "Goods Received 2"),
		("PEMINDAHAN", "Goods Received 3", "Goods Received 3"),
		("PEMINDAHAN", "Packaging Waste", "Packaging Waste")
	}

	Dim ArrFilterStatusTransaksi As New List(Of (JenisLaporan As String, ValueCombobox As String, Sql As String)) From {
		(OpsiSeluruh, OpsiSeluruh, OpsiSeluruh),
		("PEMUSNAHAN_PRODUCTION", "Status Approval", "isCompleted"),
		("PEMUSNAHAN_PRODUCTION", "Status Accounting", "Status_Validasi_Acc"),
		("PEMINDAHAN", "Status Approval", "isCompleted"),
		("PEMINDAHAN", "Status GA", "Status_Validasi_GA")
	}

	Dim ArrFilterValueStatusTransaksi As New List(Of (ValueCombobox As String, Sql As String)) From {
		(OpsiSeluruh, OpsiSeluruh),
		("Sudah Validasi", "Sudah Validasi"),
		("Belum Validasi", "Belum Validasi")
	}

	Dim IsRekap As Boolean = False

	Private Sub N_EMI_Laporan_Transaksi_Waste_Product_Transfer_Load(sender As Object, e As EventArgs) Handles MyBase.Load

		Lv_Transaksi.Columns.Clear()
		Lv_Transaksi.Columns.Add("No Transaksi", 150, HorizontalAlignment.Left)
		Lv_Transaksi.Columns.Add("No Split", 150, HorizontalAlignment.Left)
		Lv_Transaksi.Columns.Add("Tanggal", 130, HorizontalAlignment.Center)
		Lv_Transaksi.Columns.Add("Jam", 110, HorizontalAlignment.Center)
		Lv_Transaksi.View = View.Details

		Lv_Split.Columns.Clear()
		Lv_Split.Columns.Add("No Split", 150, HorizontalAlignment.Left)
		Lv_Split.Columns.Add("No PO", 150, HorizontalAlignment.Left)
		Lv_Split.Columns.Add("Tanggal", 130, HorizontalAlignment.Center)
		Lv_Split.Columns.Add("Jam", 110, HorizontalAlignment.Center)
		Lv_Split.Columns.Add("Keterangan", 278, HorizontalAlignment.Center)
		Lv_Split.View = View.Details

		Lv_Barang.Columns.Clear()
		Lv_Barang.Columns.Add("Kode Barang", 150, HorizontalAlignment.Left)
		Lv_Barang.Columns.Add("Nama Barang", 330, HorizontalAlignment.Left)
		Lv_Barang.View = View.Details

		Cmb_Jenis_Barang.Items.Clear() : ArrJenisBarang.Clear()
		Cmb_Jenis_Barang.Items.Add(OpsiSeluruh) : ArrJenisBarang.Add(OpsiSeluruh)
		Cmb_Jenis_Barang.Items.Add("Barang") : ArrJenisBarang.Add("Kode_Barang")
		'Cmb_Jenis_Barang.Items.Add("Barang Awal") : ArrJenisBarang.Add("Kode_Barang_Awal")

		Cmb_Jenis.Items.Clear()
		Cmb_Jenis.Items.Add(OpsiSeluruh)

		Cmb_Jenis_Transaksi.Items.Clear() : ArrJenisTransaksi.Clear()
		Cmb_Jenis_Transaksi.Items.Add(OpsiSeluruh) : ArrJenisTransaksi.Add(OpsiSeluruh)
		Cmb_Jenis_Transaksi.Items.Add("Faktur Waste") : ArrJenisTransaksi.Add("No_Faktur")
		'Cmb_Jenis_Transaksi.Items.Add("Faktur Asal") : ArrJenisTransaksi.Add("No_Faktur_Asal")

		Cmb_Jenis_Laporan.Items.Clear()
		Cmb_Jenis_Laporan.Items.Add("Pemusnahan Waste Rekap")
		Cmb_Jenis_Laporan.Items.Add("Pemusnahan Waste")
		Cmb_Jenis_Laporan.Items.Add("Pemindahan Waste Rekap")
		Cmb_Jenis_Laporan.Items.Add("Pemindahan Waste")

		Cmb_Lokasi_Asal_Pemusnahan.Items.Clear()
		Cmb_Lokasi_Asal_Pemusnahan.Items.Add(OpsiSeluruh)
		Cmb_Lokasi_Asal_Pemusnahan.Items.Add("Production")
		Cmb_Lokasi_Asal_Pemusnahan.Items.Add("Waste Storage")

		Cmb_Status_Transaksi.Items.Add(OpsiSeluruh)

		Cmb_Value_Status_Transaksi.Items.Clear()
		For Each item In ArrFilterValueStatusTransaksi
			Cmb_Value_Status_Transaksi.Items.Add(item.ValueCombobox)
		Next

		Kosong()

	End Sub

	Private Sub Kosong()
		Tgl1.Value = DateTime.Today
		Tgl2.Value = DateTime.Today

		Switch_Auto_Complete = True
		Cmb_Jenis.SelectedIndex = 0
		Txt_No_Transaksi.Text = OpsiSeluruh
		Txt_No_Split.Text = OpsiSeluruh : Txt_Keterangan_Split.Text = OpsiSeluruh
		Txt_Kd_Barang.Text = OpsiSeluruh : Txt_Nm_Barang.Text = OpsiSeluruh
		Cmb_Jenis_Barang.SelectedIndex = 0
		Cmb_Jenis_Transaksi.SelectedIndex = 0
		Cmb_Jenis_Laporan.SelectedIndex = 0
		Cmb_Lokasi_Asal_Pemusnahan.SelectedIndex = 0
		Switch_Auto_Complete = False

		Txt_Kd_Barang.Enabled = False
		Txt_Nm_Barang.Enabled = False

		Cmb_Status_Transaksi.Enabled = False
		Cmb_Value_Status_Transaksi.Enabled = False

		Cmb_Status_Transaksi.SelectedIndex = 0
		Cmb_Value_Status_Transaksi.SelectedIndex = 0

		ResetEnabledComponents()
		If arrIndexRekap.Contains(Cmb_Jenis_Laporan.SelectedIndex) Then
			HandleSelectedRekap()
		Else
			HandleSelectedDetail()
		End If

		Me.Size = New Size(808, 407)
		Tgl1.Focus()
	End Sub

	Private Sub ResetEnabledComponents()

		Cmb_Jenis.SelectedIndex = 0
		Cmb_Jenis_Transaksi.SelectedIndex = 0
		Cmb_Jenis_Barang.SelectedIndex = 0

		Switch_Auto_Complete = True
		Txt_No_Transaksi.Text = OpsiSeluruh
		Txt_No_Split.Text = OpsiSeluruh
		Txt_Keterangan_Split.Text = OpsiSeluruh
		Txt_Kd_Barang.Text = OpsiSeluruh
		Txt_Nm_Barang.Text = OpsiSeluruh
		Switch_Auto_Complete = False

		Cmb_Jenis.Enabled = False
		Cmb_Jenis_Transaksi.Enabled = False
		Txt_No_Transaksi.Enabled = False
		Txt_No_Split.Enabled = False
		Txt_Keterangan_Split.Enabled = False
		Cmb_Jenis_Barang.Enabled = False
		Txt_Kd_Barang.Enabled = False
		Txt_Nm_Barang.Enabled = False

	End Sub

	'====================================================================================================================
	'=     HANDLE TEXT CHANGED
	'====================================================================================================================

	Private Sub Txt_No_Transaksi_TextChanged(sender As Object, e As EventArgs) Handles Txt_No_Transaksi.TextChanged
		If Switch_Auto_Complete Then Exit Sub

		If Txt_No_Transaksi.Text.Trim.Length = 0 Then
			Me.Size = New Size(808, 407)
			Lv_Transaksi.Visible = False
			Lv_Transaksi.Location = New Point(800, 251)
			Txt_No_Transaksi.Text = ""
			Exit Sub
		Else
			Me.Size = New Size(808, 513)
			Lv_Transaksi.Location = New Point(150, 251)
			Lv_Transaksi.Visible = True
		End If

		Try
			OpenConn()

			Lv_Transaksi.Items.Clear()

			Dim Lv As ListViewItem
			Lv = Lv_Transaksi.Items.Add(OpsiSeluruh)
			Lv.SubItems.Add(OpsiSeluruh)
			Lv.SubItems.Add(OpsiSeluruh)
			Lv.SubItems.Add(OpsiSeluruh)

			If Cmb_Jenis_Laporan.SelectedIndex = 0 Or Cmb_Jenis_Laporan.SelectedIndex = 1 Then
				If Cmb_Jenis_Transaksi.SelectedIndex = 1 Then
					SQL = ""
					SQL = "select No_Faktur as No_Transaksi, '-' as No_Split, Tanggal, Jam "
					SQL &= $"from N_EMI_Transaksi_Transfer_Waste "
					SQL &= $"where status is NULL "
					SQL &= $"and kode_perusahaan = '{KodePerusahaan}' "
					SQL &= $"and No_Faktur like '%{Txt_No_Transaksi.Text.Trim}%' "
					SQL &= $"order by No_Faktur "

				ElseIf Cmb_Jenis_Transaksi.SelectedIndex = 2 Then
					SQL = ""
					Dim queries As New List(Of String)
					Dim filterNoTransaksi As String = Txt_No_Transaksi.Text.Trim

					If Cmb_Jenis.SelectedIndex = 0 OrElse Cmb_Jenis.SelectedIndex = 1 Then
						queries.Add($"
                        select No_Transaksi,
                            No_Production_Order as No_Split, Tanggal, Jam
                            from Emi_Production_Results
                            where Kode_Perusahaan = '{KodePerusahaan}'
                                and Status is null
                                and No_Transaksi like '%{filterNoTransaksi}%'
                        ")
					End If

					If Cmb_Jenis.SelectedIndex = 0 OrElse Cmb_Jenis.SelectedIndex = 2 Then
						queries.Add($"
                            select No_Transaksi, No_Production_Order as No_Split, Tanggal, Jam
                            from Emi_Production_Results_Validation
                            where Kode_Perusahaan = '{KodePerusahaan}'
                                and Status is null
                                and No_Transaksi like '%{filterNoTransaksi}%'
                        ")
					End If

					If Cmb_Jenis.SelectedIndex = 0 OrElse Cmb_Jenis.SelectedIndex = 4 Then
						queries.Add($"
                            select distinct a.No_Faktur as No_Transaksi, c.No_Split, a.Tanggal, a.Jam
                            from N_EMI_Transaksi_Transfer_Waste a
                            inner join N_EMI_Transaksi_Transfer_Waste_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur
                            inner join N_EMI_Transaksi_Transfer_Waste_Det c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.Urut_TF
                            where a.Kode_Perusahaan = '{KodePerusahaan}' and a.Status is null and a.No_Faktur like '%{filterNoTransaksi}%'
                        ")
					End If

					SQL = String.Join(" UNION ALL ", queries)
					SQL &= $" order by No_Transaksi "
				Else
					CloseConn()
					Exit Sub
				End If

			ElseIf Cmb_Jenis_Laporan.SelectedIndex = 2 Or Cmb_Jenis_Laporan.SelectedIndex = 3 Then
				If Cmb_Jenis_Transaksi.SelectedIndex = 1 Then
					SQL = ""

					SQL = "select No_Faktur as No_Transaksi, '-' as No_Split, Tanggal, Jam "
					SQL &= $"from N_EMI_Transaksi_Transfer_Waste_Produk "
					SQL &= $"where status is NULL "
					SQL &= $"and kode_perusahaan = '{KodePerusahaan}' "
					SQL &= $"and No_Faktur like '%{Txt_No_Transaksi.Text.Trim}%' "
					SQL &= $"order by No_Faktur "

				ElseIf Cmb_Jenis_Transaksi.SelectedIndex = 2 Then
					SQL = ""
					Dim queries As New List(Of String)
					Dim filterNoTransaksi As String = Txt_No_Transaksi.Text.Trim

					If Cmb_Jenis.SelectedIndex = 0 OrElse Cmb_Jenis.SelectedIndex = 1 Then
						queries.Add($"
                        select No_Transaksi,
                            No_Production_Order as No_Split, Tanggal, Jam
                            from Emi_Production_Results
                            where Kode_Perusahaan = '{KodePerusahaan}'
                                and Status is null
                                and No_Transaksi like '%{filterNoTransaksi}%'
                        ")
					End If

					If Cmb_Jenis.SelectedIndex = 0 OrElse Cmb_Jenis.SelectedIndex = 2 Then
						queries.Add($"
                        select No_Transaksi, No_Production_Order as No_Split, Tanggal, Jam
                        from Emi_Production_Results_Validation
                        where Kode_Perusahaan = '{KodePerusahaan}'
                            and Status is null
                            and No_Transaksi like '%{filterNoTransaksi}%'
                    ")
					End If

					If Cmb_Jenis.SelectedIndex = 0 OrElse Cmb_Jenis.SelectedIndex = 3 Then
						queries.Add($"
                        select No_Transaksi, No_Split, Tanggal, Jam
                        from EMI_Production_Results_Detail_Change_Packaging
                        where Kode_Perusahaan = '{KodePerusahaan}'
                            and Status is null
                            and No_Transaksi like '%{filterNoTransaksi}%'
                    ")
					End If

					If Cmb_Jenis.SelectedIndex = 0 OrElse Cmb_Jenis.SelectedIndex = 4 Then
						queries.Add($"
                        select distinct a.No_Faktur as No_Transaksi, d.No_Split, a.Tanggal, a.Jam
                        from N_EMI_Transaksi_Transfer_Waste_Produk a
                        inner join N_EMI_Transaksi_Transfer_Waste_Produk_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur
                        inner join N_EMI_Transaksi_Transfer_Waste_Produk_Det c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.Urut_TF
                        inner join N_EMI_Binding_Transaksi_Transfer_Waste_Produk d on c.Kode_Perusahaan = d.Kode_Perusahaan and c.No_Faktur = d.No_Faktur and c.Urut_Oto = d.Urut_det
                        where a.Kode_Perusahaan = '{KodePerusahaan}' and a.Status is null and a.No_Faktur like '%{filterNoTransaksi}%'
                    ")
					End If

					SQL = String.Join(" UNION ALL ", queries)
					SQL &= $" order by No_Transaksi "
				Else
					CloseConn()
					Exit Sub
				End If
			End If

			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Lv = Lv_Transaksi.Items.Add(Dr("No_Transaksi"))
					Lv.SubItems.Add(Dr("No_Split"))
					Lv.SubItems.Add(If(General_Class.CekNULL(Dr("tanggal")) = "", "-", Format(Dr("tanggal"), "dd MMM yyyy")))
					Lv.SubItems.Add(If(General_Class.CekNULL(Dr("jam")) = "", "-", Dr("jam")))
				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Txt_No_Transaksi_Leave(sender As Object, e As EventArgs) Handles Txt_No_Transaksi.Leave
		If Txt_No_Transaksi.Text.Trim.Length = 0 Then Exit Sub
		If Lv_Transaksi.Focused = True Then Exit Sub

		Try
			OpenConn()

			If Not Txt_No_Transaksi.Text.Trim.ToUpper = OpsiSeluruh.Trim.ToUpper Then

				If Cmb_Jenis_Laporan.SelectedIndex = 0 Then

					If Cmb_Jenis_Transaksi.SelectedIndex = 1 Then
						SQL = ""
						SQL = "select No_Faktur as No_Transaksi, '-' as No_Split, Tanggal, Jam "
						SQL &= $"from N_EMI_Transaksi_Transfer_Waste "
						SQL &= $"where status is NULL "
						SQL &= $"and kode_perusahaan = '{KodePerusahaan}' "
						SQL &= $"and No_Faktur = '{Txt_No_Transaksi.Text.Trim}' "
						SQL &= $"order by No_Faktur "

					ElseIf Cmb_Jenis_Transaksi.SelectedIndex = 2 Then
						SQL = ""
						Dim queries As New List(Of String)
						Dim filterNoTransaksi As String = Txt_No_Transaksi.Text.Trim

						If Cmb_Jenis.SelectedIndex = 0 OrElse Cmb_Jenis.SelectedIndex = 1 Then
							queries.Add($"
                        select No_Transaksi,
                            No_Production_Order as No_Split, Tanggal, Jam
                            from Emi_Production_Results
                            where Kode_Perusahaan = '{KodePerusahaan}'
                                and Status is null
                                and No_Transaksi = '{filterNoTransaksi}'
                        ")
						End If

						If Cmb_Jenis.SelectedIndex = 0 OrElse Cmb_Jenis.SelectedIndex = 2 Then
							queries.Add($"
                            select No_Transaksi, No_Production_Order as No_Split, Tanggal, Jam
                            from Emi_Production_Results_Validation
                            where Kode_Perusahaan = '{KodePerusahaan}'
                                and Status is null
                                and No_Transaksi = '{filterNoTransaksi}'
                        ")
						End If

						If Cmb_Jenis.SelectedIndex = 0 OrElse Cmb_Jenis.SelectedIndex = 4 Then
							queries.Add($"
                            select distinct a.No_Faktur as No_Transaksi, c.No_Split, a.Tanggal, a.Jam
                            from N_EMI_Transaksi_Transfer_Waste a
                            inner join N_EMI_Transaksi_Transfer_Waste_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur
                            inner join N_EMI_Transaksi_Transfer_Waste_Det c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.Urut_TF
                            where a.Kode_Perusahaan = '{KodePerusahaan}' and a.Status is null and a.No_Faktur = '{filterNoTransaksi}'
                        ")
						End If

						SQL = String.Join(" UNION ALL ", queries)
						SQL &= $" order by No_Transaksi "
					Else
						CloseConn()
						Exit Sub
					End If

				ElseIf Cmb_Jenis_Laporan.SelectedIndex = 1 Then
					If Cmb_Jenis_Transaksi.SelectedIndex = 1 Then
						SQL = "select No_Faktur as No_Transaksi, '-' as No_Split, Tanggal, Jam "
						SQL &= $"from N_EMI_Transaksi_Transfer_Waste_Produk "
						SQL &= $"where status is NULL "
						SQL &= $"and kode_perusahaan = '{KodePerusahaan}' "
						SQL &= $"and No_Faktur like '%{Txt_No_Transaksi.Text.Trim}%' "
						SQL &= $"order by No_Faktur "
					ElseIf Cmb_Jenis_Transaksi.SelectedIndex = 2 Then

						Dim queries As New List(Of String)
						Dim filterNoTransaksi As String = Txt_No_Transaksi.Text.Trim
						If Cmb_Jenis.SelectedIndex = 0 OrElse Cmb_Jenis.SelectedIndex = 1 Then
							queries.Add($"
                            select No_Transaksi,
                                No_Production_Order as No_Split, Tanggal, Jam
                                from Emi_Production_Results
                                where Kode_Perusahaan = '{KodePerusahaan}'
                                    and Status is null
                                    and No_Transaksi = '{filterNoTransaksi}'
                            ")
						End If

						If Cmb_Jenis.SelectedIndex = 0 OrElse Cmb_Jenis.SelectedIndex = 2 Then
							queries.Add($"
                            select No_Transaksi, No_Production_Order as No_Split, Tanggal, Jam
                            from Emi_Production_Results_Validation
                            where Kode_Perusahaan = '{KodePerusahaan}'
                                and Status is null
                                and No_Transaksi = '{filterNoTransaksi}'
                        ")
						End If

						If Cmb_Jenis.SelectedIndex = 0 OrElse Cmb_Jenis.SelectedIndex = 3 Then
							queries.Add($"
                            select No_Transaksi, No_Split, Tanggal, Jam
                            from EMI_Production_Results_Detail_Change_Packaging
                            where Kode_Perusahaan = '{KodePerusahaan}'
                                and Status is null
                                and No_Transaksi = '{filterNoTransaksi}'
                        ")
						End If

						If Cmb_Jenis.SelectedIndex = 0 OrElse Cmb_Jenis.SelectedIndex = 4 Then
							queries.Add($"
                            select distinct a.No_Faktur as No_Transaksi, d.No_Split, a.Tanggal, a.Jam
                            from N_EMI_Transaksi_Transfer_Waste_Produk a
                            inner join N_EMI_Transaksi_Transfer_Waste_Produk_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur
                            inner join N_EMI_Transaksi_Transfer_Waste_Produk_Det c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.Urut_TF
                            inner join N_EMI_Binding_Transaksi_Transfer_Waste_Produk d on c.Kode_Perusahaan = d.Kode_Perusahaan and c.No_Faktur = d.No_Faktur and c.Urut_Oto = d.Urut_det
                            where a.Kode_Perusahaan = '{KodePerusahaan}' and a.Status is null and a.No_Faktur = '{filterNoTransaksi}'
                        ")
						End If

						Dim SQL As String = String.Join(" UNION ALL ", queries)
					Else
						CloseConn()
						Exit Sub
					End If

				End If
				Using Dr = Open(SQL)
					If Dr.Read Then
						Txt_No_Transaksi.Text = Dr("No_Transaksi")
						Txt_No_Split.Focus()
					Else
						MessageBox.Show("No Transaksi Tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Txt_No_Transaksi.Text = ""
						Txt_No_Transaksi.Focus()
					End If

					Me.Size = New Size(808, 407)
					Lv_Transaksi.Visible = False
					Lv_Transaksi.Location = New Point(800, 251)
				End Using
			Else
				Txt_No_Split.Focus()
			End If

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Txt_No_Split_TextChanged(sender As Object, e As EventArgs) Handles Txt_No_Split.TextChanged
		If Switch_Auto_Complete Then Exit Sub

		If Txt_No_Split.Text.Trim.Length = 0 Then
			Me.Size = New Size(808, 407)
			Lv_Split.Visible = False
			Lv_Split.Location = New Point(800, 278)
			Txt_No_Split.Text = ""
			Txt_Keterangan_Split.Text = ""
			Exit Sub
		Else
			Me.Size = New Size(808, 544)
			Lv_Split.Location = New Point(150, 278)
			Lv_Split.Visible = True
		End If

		Try
			OpenConn()

			Lv_Split.Items.Clear()

			Dim Lv As ListViewItem
			Lv = Lv_Split.Items.Add(OpsiSeluruh)
			Lv.SubItems.Add(OpsiSeluruh)
			Lv.SubItems.Add(OpsiSeluruh)
			Lv.SubItems.Add(OpsiSeluruh)
			Lv.SubItems.Add(OpsiSeluruh)

			SQL = "select No_Transaksi, No_PO, Tanggal, Jam, No_Batch "
			SQL &= $"from Emi_Split_Production_Order "
			SQL &= $"where kode_perusahaan = '{KodePerusahaan}' "
			SQL &= $"and status is NULL "
			SQL &= $"and No_Transaksi like '%{Txt_No_Split.Text.Trim}%' "
			SQL &= $"order by No_Transaksi"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Lv = Lv_Split.Items.Add(Dr("No_Transaksi"))
					Lv.SubItems.Add(Dr("No_PO"))
					Lv.SubItems.Add(If(General_Class.CekNULL(Dr("tanggal")) = "", "-", Format(Dr("tanggal"), "dd MMM yyyy")))
					Lv.SubItems.Add(If(General_Class.CekNULL(Dr("jam")) = "", "-", Dr("jam")))
					Lv.SubItems.Add(Dr("No_Batch"))
				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Txt_No_Split_Leave(sender As Object, e As EventArgs) Handles Txt_No_Split.Leave
		If Txt_No_Split.Text.Trim.Length = 0 Then Exit Sub
		If Lv_Split.Focused = True Then Exit Sub

		Try
			OpenConn()

			If Not Txt_No_Split.Text.Trim.ToUpper = OpsiSeluruh.Trim.ToUpper Then

				SQL = "select No_Transaksi, No_PO, Tanggal, Jam, No_Batch "
				SQL &= $"from Emi_Split_Production_Order "
				SQL &= $"where kode_perusahaan = '{KodePerusahaan}' "
				SQL &= $"and status is NULL "
				SQL &= $"and No_Transaksi = '{Txt_No_Split.Text.Trim}' "
				SQL &= $"order by No_Transaksi"
				Using Dr = Open(SQL)
					If Dr.Read Then
						Txt_No_Split.Text = Dr("No_Transaksi")
						Txt_Keterangan_Split.Text = Dr("No_Batch")
						Cmb_Jenis_Barang.DroppedDown = True
						Cmb_Jenis_Barang.Focus()
					Else
						MessageBox.Show("No Split Tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Txt_No_Split.Text = ""
						Txt_Keterangan_Split.Text = ""
						Txt_No_Split.Focus()
					End If

					Me.Size = New Size(808, 407)
					Lv_Split.Visible = False
					Lv_Split.Location = New Point(800, 278)
				End Using
			Else
				Cmb_Jenis_Barang.DroppedDown = True
				Cmb_Jenis_Barang.Focus()
			End If

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Cmb_Jenis_Barang_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Jenis_Barang.SelectedIndexChanged
		If Cmb_Jenis_Barang.SelectedIndex <> 0 Then
			Txt_Kd_Barang.Enabled = True
			Txt_Nm_Barang.Enabled = True
			Switch_Auto_Complete = True
			Txt_Kd_Barang.Text = ""
			Txt_Nm_Barang.Text = ""
			Switch_Auto_Complete = False
			Txt_Kd_Barang.Focus()
		Else
			Txt_Kd_Barang.Enabled = False
			Txt_Nm_Barang.Enabled = False
			Switch_Auto_Complete = True
			Txt_Kd_Barang.Text = OpsiSeluruh
			Txt_Nm_Barang.Text = OpsiSeluruh
			Switch_Auto_Complete = False
		End If

	End Sub

	Private Sub Txt_Kd_Barang_TextChanged(sender As Object, e As EventArgs) Handles Txt_Kd_Barang.TextChanged
		If Switch_Auto_Complete Then Exit Sub

		If Txt_Kd_Barang.Text.Trim.Length = 0 Then
			Me.Size = New Size(808, 407)
			Lv_Barang.Visible = False
			Lv_Barang.Location = New Point(800, 306)
			Txt_Kd_Barang.Text = ""
			Txt_Nm_Barang.Text = ""
			Exit Sub
		Else
			Me.Size = New Size(808, 567)
			Lv_Barang.Location = New Point(150, 306)
			Lv_Barang.Visible = True
		End If

		Try
			OpenConn()

			Lv_Barang.Items.Clear()

			Dim Lv As ListViewItem
			Lv = Lv_Barang.Items.Add(OpsiSeluruh)
			Lv.SubItems.Add(OpsiSeluruh)

			SQL = "select Distinct a.Kode_Barang, a.Nama "
			SQL = SQL & "from barang a "
			SQL = SQL & "Where a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and a.Kode_Barang like '%" & Txt_Kd_Barang.Text & "%' "
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Lv = Lv_Barang.Items.Add(Dr("Kode_Barang"))
					Lv.SubItems.Add(Dr("Nama"))
				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Txt_Kd_Barang_Leave(sender As Object, e As EventArgs) Handles Txt_Kd_Barang.Leave
		If Txt_Kd_Barang.Text.Trim.Length = 0 Then Exit Sub
		If Lv_Barang.Focused = True Then Exit Sub

		Try
			OpenConn()

			If Not Txt_Kd_Barang.Text.Trim.ToUpper = OpsiSeluruh.Trim.ToUpper Then

				SQL = "select Distinct a.Kode_Barang, a.Nama "
				SQL = SQL & "from barang a "
				SQL = SQL & "Where a.Kode_Perusahaan = '" & KodePerusahaan & "' "
				SQL = SQL & "and a.Kode_Barang = '" & Txt_Kd_Barang.Text & "' "
				Using Dr = Open(SQL)
					If Dr.Read Then
						Txt_Kd_Barang.Text = Dr("Kode_Barang")
						Txt_Nm_Barang.Text = Dr("Nama")
						BtnCetak.Focus()
					Else
						MessageBox.Show("Barang Tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
						Txt_Kd_Barang.Text = ""
						Txt_Nm_Barang.Text = ""
						Txt_Kd_Barang.Focus()
					End If

					Me.Size = New Size(808, 407)
					Lv_Barang.Visible = False
					Lv_Barang.Location = New Point(800, 306)
				End Using
			Else
				BtnCetak.Focus()
			End If

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Txt_Nm_Barang_TextChanged(sender As Object, e As EventArgs) Handles Txt_Nm_Barang.TextChanged
		If Switch_Auto_Complete Then Exit Sub

		If Txt_Nm_Barang.Text.Trim.Length = 0 Then
			Me.Size = New Size(808, 407)
			Lv_Barang.Visible = False
			Lv_Barang.Location = New Point(800, 306)
			Txt_Kd_Barang.Text = ""
			Txt_Nm_Barang.Text = ""
			Exit Sub
		Else
			Me.Size = New Size(808, 567)
			Lv_Barang.Location = New Point(150, 306)
			Lv_Barang.Visible = True
		End If

		Try
			OpenConn()

			Lv_Barang.Items.Clear()

			Dim Lv As ListViewItem
			Lv = Lv_Barang.Items.Add(OpsiSeluruh)
			Lv.SubItems.Add(OpsiSeluruh)

			SQL = "select Distinct a.Kode_Barang, a.Nama "
			SQL = SQL & "from barang a "
			SQL = SQL & "Where a.Kode_Perusahaan = '" & KodePerusahaan & "' "
			SQL = SQL & "and a.Nama like '%" & Txt_Nm_Barang.Text & "%' "
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Lv = Lv_Barang.Items.Add(Dr("Kode_Barang"))
					Lv.SubItems.Add(Dr("Nama"))
				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	'====================================================================================================================
	'=     HANDLE LISTVIEW
	'====================================================================================================================
	Private Sub Lv_Transaksi_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Transaksi.DoubleClick
		If Lv_Transaksi.Items.Count = 0 Or Lv_Transaksi.FocusedItem.Index = -1 Then Exit Sub

		Dim NoFaktur As String = Lv_Transaksi.FocusedItem.SubItems(0).Text

		Switch_Auto_Complete = True
		Txt_No_Transaksi.Text = NoFaktur
		Switch_Auto_Complete = False

		Me.Size = New Size(808, 407)
		Lv_Transaksi.Visible = False
		Lv_Transaksi.Location = New Point(800, 251)

		Txt_No_Split.Focus()
	End Sub

	Private Sub Lv_Transaksi_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Transaksi.KeyDown
		If e.KeyCode = Keys.Enter Then
			Lv_Transaksi_DoubleClick(Lv_Transaksi, e)
		End If
	End Sub

	Private Sub Lv_Split_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Split.DoubleClick
		If Lv_Split.Items.Count = 0 Or Lv_Split.FocusedItem.Index = -1 Then Exit Sub

		Dim NoFaktur As String = Lv_Split.FocusedItem.SubItems(0).Text
		Dim Ket As String = Lv_Split.FocusedItem.SubItems(4).Text

		Switch_Auto_Complete = True
		Txt_No_Split.Text = NoFaktur
		Txt_Keterangan_Split.Text = Ket
		Switch_Auto_Complete = False

		Me.Size = New Size(808, 407)
		Lv_Split.Visible = False
		Lv_Split.Location = New Point(800, 278)

		Cmb_Jenis_Barang.DroppedDown = True
		Cmb_Jenis_Barang.Focus()
	End Sub

	Private Sub Lv_Split_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Split.KeyDown
		If e.KeyCode = Keys.Enter Then
			Lv_Split_DoubleClick(Lv_Split, e)
		End If
	End Sub

	Private Sub Lv_Barang_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Barang.DoubleClick
		If Lv_Barang.Items.Count = 0 Or Lv_Barang.FocusedItem.Index = -1 Then Exit Sub

		Dim NoFaktur As String = Lv_Barang.FocusedItem.SubItems(0).Text
		Dim Ket As String = Lv_Barang.FocusedItem.SubItems(1).Text

		Switch_Auto_Complete = True
		Txt_Kd_Barang.Text = NoFaktur
		Txt_Nm_Barang.Text = Ket
		Switch_Auto_Complete = False

		Me.Size = New Size(808, 407)
		Lv_Barang.Visible = False
		Lv_Barang.Location = New Point(800, 306)

		BtnCetak.Focus()
	End Sub

	Private Sub Lv_Barang_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Barang.KeyDown
		If e.KeyCode = Keys.Enter Then
			Lv_Barang_DoubleClick(Lv_Barang, e)
		End If
	End Sub

	'====================================================================================================================
	'=     HANDLE BUTTON
	'====================================================================================================================
	Private Sub BtnCetak_Click(sender As Object, e As EventArgs) Handles BtnCetak.Click
		If Tgl1.Value > Tgl2.Value Then
			MessageBox.Show("Periode I tidak boleh lebih dari periode II!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Tgl1.Value = Now.Date : Tgl2.Value = Now.Date
			Tgl1.Focus() : Exit Sub
		ElseIf Cmb_Jenis_Laporan.SelectedIndex = -1 Then
			MessageBox.Show("Jenis Laporan Harus Di Pilih!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmb_Jenis_Laporan.Focus() : Exit Sub
		ElseIf Cmb_Jenis.SelectedIndex = -1 Then
			MessageBox.Show("Jenis Harus Di Pilih!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Cmb_Jenis.Focus() : Exit Sub
		ElseIf Txt_No_Transaksi.Text.Trim.Length = 0 Then
			MessageBox.Show("No Transaksi harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_No_Transaksi.Focus() : Exit Sub
		ElseIf Txt_No_Split.Text.Trim.Length = 0 Then
			MessageBox.Show("No Split harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Txt_No_Split.Focus() : Exit Sub
		ElseIf Cmb_Jenis_Barang.SelectedIndex <> 0 Then
			If Txt_Kd_Barang.Text.Trim.Length = 0 Then
				MessageBox.Show("Barang harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				Txt_Kd_Barang.Focus() : Exit Sub
			End If
		End If

		Try
			OpenConn()

			Dim SF As String = ""

			If Cmb_Jenis_Laporan.SelectedIndex = 0 Then

				SQL = "select Kode_Perusahaan from N_EMI_View_Laporan_Transaksi_Waste_Process_Rekap "
				SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
				SQL = SQL & "and Tanggal between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "

				SF = "{N_EMI_View_Laporan_Transaksi_Waste_Process_Rekap.Kode_Perusahaan} = '" & KodePerusahaan & "' "
				SF = SF & "and {N_EMI_View_Laporan_Transaksi_Waste_Process_Rekap.Tanggal} >= #" & Format(Tgl1.Value, "yyyy-MM-dd") & "# and "
				SF = SF & "{N_EMI_View_Laporan_Transaksi_Waste_Process_Rekap.Tanggal} <= #" & Format(Tgl2.Value, "yyyy-MM-dd") & "# "

				'If Cmb_Jenis.SelectedIndex <> 0 Then
				'	SQL = SQL & "and jenis = '" & ArrJenis(Cmb_Jenis.SelectedIndex) & "' "
				'	SF = SF & "And {N_EMI_View_Laporan_Transaksi_Waste_Process_Rekap.jenis} = '" & ArrJenis(Cmb_Jenis.SelectedIndex) & "'"

				'End If

				If Cmb_Status_Transaksi.SelectedIndex > 0 And Cmb_Value_Status_Transaksi.SelectedIndex > 0 Then

					Dim listFilterStatus = ArrFilterStatusTransaksi.Where(Function(x) x.JenisLaporan = "PEMUSNAHAN_PRODUCTION").ToList()

					SQL = SQL & "and " & listFilterStatus(Cmb_Status_Transaksi.SelectedIndex - 1).Sql & " = '" & ArrFilterValueStatusTransaksi(Cmb_Value_Status_Transaksi.SelectedIndex).Sql & "' "
					SF = SF & "And {N_EMI_View_Laporan_Transaksi_Waste_Process_Rekap." & listFilterStatus(Cmb_Status_Transaksi.SelectedIndex - 1).Sql & "} = '" & ArrFilterValueStatusTransaksi(Cmb_Value_Status_Transaksi.SelectedIndex).Sql.Trim & "'"
				End If

				If Not Txt_No_Transaksi.Text.ToUpper.Trim = OpsiSeluruh.ToUpper.Trim Then
					Dim Kolom As String = ArrJenisTransaksi(Cmb_Jenis_Transaksi.SelectedIndex).ToString()
					SQL = SQL & "and " & Kolom & " = '" & Txt_No_Transaksi.Text.Trim & "' "
					SF = SF & "And {N_EMI_View_Laporan_Transaksi_Waste_Process_Rekap." & Kolom & "} = '" & Txt_No_Transaksi.Text.Trim & "'"
				End If

				'If Not Txt_No_Split.Text.ToUpper.Trim = OpsiSeluruh.ToUpper.Trim Then
				'	SQL = SQL & "and No_Production_Order = '" & Txt_No_Split.Text.Trim & "' "
				'	SF = SF & "And {N_EMI_View_Laporan_Transaksi_Waste_Process_Rekap.No_Production_Order} = '" & Txt_No_Split.Text.Trim & "'"
				'End If

				If Cmb_Jenis_Barang.SelectedIndex <> 0 Then
					If Txt_Kd_Barang.Text.ToUpper.Trim <> OpsiSeluruh.ToUpper.Trim Then
						Dim Kolom As String = ArrJenisBarang(Cmb_Jenis_Barang.SelectedIndex).ToString()
						SQL = SQL & "and " & Kolom & " = '" & Txt_Kd_Barang.Text & "' "
						SF = SF & "And {N_EMI_View_Laporan_Transaksi_Waste_Process_Rekap." & Kolom & "} = '" & Txt_Kd_Barang.Text & "'"
					End If
				End If
				Using DS = BindingTrans(SQL)
					With DS.Tables("MyTable")
						If .Rows.Count <> 0 Then

							Dim CrDoc As New N_EMI_CR_Laporan_Transaksi_Waste_Process_Rekap

							CrDoc.SetDataSource(DS)
							CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
							CrDoc.SummaryInfo.ReportTitle = "Periode : " & Format(Tgl1.Value, "dd/MMM/yyyy") & " s/d " &
																			Format(Tgl2.Value, "dd/MMM/yyyy")
							CrDoc.RecordSelectionFormula = SF

							With A_Place_For_Printing2
								.Text = "Laporan Pemusnahan Waste Rekap"
								.CrystalReportViewer1.ReportSource = CrDoc
								.CrystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
								.Refresh()
								.Show()
							End With
						Else

							CloseConn()
							MessageBox.Show("Data Pemusnahan Waste Rekap Items Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							Exit Sub

						End If
					End With
				End Using

			ElseIf Cmb_Jenis_Laporan.SelectedIndex = 1 Then

				SQL = "select Kode_Perusahaan from N_EMI_View_Laporan_Transaksi_Waste_Process "
				SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
				SQL = SQL & "and Tanggal between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "

				SF = "{N_EMI_View_Laporan_Transaksi_Waste_Process.Kode_Perusahaan} = '" & KodePerusahaan & "' "
				SF = SF & "and {N_EMI_View_Laporan_Transaksi_Waste_Process.Tanggal} >= #" & Format(Tgl1.Value, "yyyy-MM-dd") & "# and "
				SF = SF & "{N_EMI_View_Laporan_Transaksi_Waste_Process.Tanggal} <= #" & Format(Tgl2.Value, "yyyy-MM-dd") & "# "

				If Cmb_Status_Transaksi.SelectedIndex > 0 And Cmb_Value_Status_Transaksi.SelectedIndex > 0 Then

					Dim listFilterStatus = ArrFilterStatusTransaksi.Where(Function(x) x.JenisLaporan = "PEMUSNAHAN_PRODUCTION").ToList()

					SQL = SQL & "and " & listFilterStatus(Cmb_Status_Transaksi.SelectedIndex - 1).Sql & " = '" & ArrFilterValueStatusTransaksi(Cmb_Value_Status_Transaksi.SelectedIndex).Sql & "' "
					SF = SF & "And {N_EMI_View_Laporan_Transaksi_Waste_Process." & listFilterStatus(Cmb_Status_Transaksi.SelectedIndex - 1).Sql & "} = '" & ArrFilterValueStatusTransaksi(Cmb_Value_Status_Transaksi.SelectedIndex).Sql.Trim & "'"
				End If

				If Not Txt_No_Transaksi.Text.ToUpper.Trim = OpsiSeluruh.ToUpper.Trim Then
					Dim Kolom As String = ArrJenisTransaksi(Cmb_Jenis_Transaksi.SelectedIndex).ToString()
					SQL = SQL & "and " & Kolom & " = '" & Txt_No_Transaksi.Text.Trim & "' "
					SF = SF & "And {N_EMI_View_Laporan_Transaksi_Waste_Process." & Kolom & "} = '" & Txt_No_Transaksi.Text.Trim & "'"
				End If

				If Not Txt_No_Split.Text.ToUpper.Trim = OpsiSeluruh.ToUpper.Trim Then
					SQL = SQL & "and No_Production_Order = '" & Txt_No_Split.Text.Trim & "' "
					SF = SF & "And {N_EMI_View_Laporan_Transaksi_Waste_Process.No_Production_Order} = '" & Txt_No_Split.Text.Trim & "'"
				End If

				If Cmb_Jenis_Barang.SelectedIndex <> 0 Then
					If Txt_Kd_Barang.Text.ToUpper.Trim <> OpsiSeluruh.ToUpper.Trim Then
						Dim Kolom As String = ArrJenisBarang(Cmb_Jenis_Barang.SelectedIndex).ToString()
						SQL = SQL & "and " & Kolom & " = '" & Txt_Kd_Barang.Text & "' "
						SF = SF & "And {N_EMI_View_Laporan_Transaksi_Waste_Process." & Kolom & "} = '" & Txt_Kd_Barang.Text & "'"
					End If
				End If

				If Cmb_Lokasi_Asal_Pemusnahan.SelectedIndex <> 0 Then

					If Cmb_Lokasi_Asal_Pemusnahan.SelectedIndex = 2 Then
						Dim Kolom As String = "Jenis"
						SQL = SQL & "and " & Kolom & " = 'Pemindahan Waste' "
						SF = SF & "And {N_EMI_View_Laporan_Transaksi_Waste_Process." & Kolom & "} = 'Pemindahan Waste'"
					Else
						Dim Kolom As String = "Jenis"
						SQL = SQL & "and " & Kolom & " <> 'Pemindahan Waste' "
						SF = SF & "And {N_EMI_View_Laporan_Transaksi_Waste_Process." & Kolom & "} <> 'Pemindahan Waste'"
					End If

					If Cmb_Jenis.SelectedIndex <> 0 Then
						Dim value As String = ArrFilterJenis.FirstOrDefault(Function(x) x.ValueCombobox.Trim = Cmb_Jenis.Text.Trim).JenisSql

						SQL = SQL & "and jenis = '" & value & "' "
						SF = SF & "And {N_EMI_View_Laporan_Transaksi_Waste_Process.jenis} = '" & value & "'"

					End If

				End If

				Using DS = BindingTrans(SQL)
					With DS.Tables("MyTable")
						If .Rows.Count <> 0 Then

							Dim CrDoc As New N_EMI_CR_Laporan_Transaksi_Waste_Process

							CrDoc.SetDataSource(DS)
							CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
							CrDoc.SummaryInfo.ReportTitle = "Periode : " & Format(Tgl1.Value, "dd/MMM/yyyy") & " s/d " &
																			Format(Tgl2.Value, "dd/MMM/yyyy")
							CrDoc.RecordSelectionFormula = SF

							With A_Place_For_Printing2
								.Text = "Laporan Pemusnahan Waste"
								.CrystalReportViewer1.ReportSource = CrDoc
								.CrystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
								.Refresh()
								.Show()
							End With
						Else

							CloseConn()
							MessageBox.Show("Data Pemusnahan Waste Items Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							Exit Sub

						End If
					End With
				End Using

			ElseIf Cmb_Jenis_Laporan.SelectedIndex = 2 Then

				SQL = "select Kode_Perusahaan from N_EMI_View_Laporan_Transaksi_Waste_Product_Transfer_Rekap "
				SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
				SQL = SQL & "and Tanggal between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "

				SF = "{N_EMI_View_Laporan_Transaksi_Waste_Product_Transfer_Rekap.Kode_Perusahaan} = '" & KodePerusahaan & "' "
				SF = SF & "and {N_EMI_View_Laporan_Transaksi_Waste_Product_Transfer_Rekap.Tanggal} >= #" & Format(Tgl1.Value, "yyyy-MM-dd") & "# and "
				SF = SF & "{N_EMI_View_Laporan_Transaksi_Waste_Product_Transfer_Rekap.Tanggal} <= #" & Format(Tgl2.Value, "yyyy-MM-dd") & "# "

				'If Cmb_Jenis.SelectedIndex <> 0 Then
				'	SQL = SQL & "and jenis = '" & ArrJenis(Cmb_Jenis.SelectedIndex) & "' "
				'	SF = SF & "And {N_EMI_View_Laporan_Transaksi_Waste_Product_Transfer_Rekap.jenis} = '" & ArrJenis(Cmb_Jenis.SelectedIndex) & "'"

				'End If

				If Cmb_Status_Transaksi.SelectedIndex > 0 And Cmb_Value_Status_Transaksi.SelectedIndex > 0 Then

					Dim listFilterStatus = ArrFilterStatusTransaksi.Where(Function(x) x.JenisLaporan = "PEMINDAHAN").ToList()

					SQL = SQL & "and " & listFilterStatus(Cmb_Status_Transaksi.SelectedIndex - 1).Sql & " = '" & ArrFilterValueStatusTransaksi(Cmb_Value_Status_Transaksi.SelectedIndex).Sql & "' "
					SF = SF & "And {N_EMI_View_Laporan_Transaksi_Waste_Product_Transfer_Rekap." & listFilterStatus(Cmb_Status_Transaksi.SelectedIndex - 1).Sql & "} = '" & ArrFilterValueStatusTransaksi(Cmb_Value_Status_Transaksi.SelectedIndex).Sql.Trim & "'"
				End If

				If Not Txt_No_Transaksi.Text.ToUpper.Trim = OpsiSeluruh.ToUpper.Trim Then
					Dim Kolom As String = ArrJenisTransaksi(Cmb_Jenis_Transaksi.SelectedIndex).ToString()
					SQL = SQL & "and " & Kolom & " = '" & Txt_No_Transaksi.Text.Trim & "' "
					SF = SF & "And {N_EMI_View_Laporan_Transaksi_Waste_Product_Transfer_Rekap." & Kolom & "} = '" & Txt_No_Transaksi.Text.Trim & "'"
				End If

				'If Not Txt_No_Split.Text.ToUpper.Trim = OpsiSeluruh.ToUpper.Trim Then
				'	SQL = SQL & "and No_Production_Order = '" & Txt_No_Split.Text.Trim & "' "
				'	SF = SF & "And {N_EMI_View_Laporan_Transaksi_Waste_Product_Transfer_Rekap.No_Production_Order} = '" & Txt_No_Split.Text.Trim & "'"
				'End If

				If Cmb_Jenis_Barang.SelectedIndex <> 0 Then
					If Txt_Kd_Barang.Text.ToUpper.Trim <> OpsiSeluruh.ToUpper.Trim Then
						Dim Kolom As String = ArrJenisBarang(Cmb_Jenis_Barang.SelectedIndex).ToString()
						SQL = SQL & "and " & Kolom & " = '" & Txt_Kd_Barang.Text & "' "
						SF = SF & "And {N_EMI_View_Laporan_Transaksi_Waste_Product_Transfer_Rekap." & Kolom & "} = '" & Txt_Kd_Barang.Text & "'"
					End If
				End If

				Using DS = BindingTrans(SQL)
					With DS.Tables("MyTable")
						If .Rows.Count <> 0 Then

							Dim CrDoc As New N_EMI_CR_Laporan_Transaksi_Waste_Product_Transfer_Rekap

							CrDoc.SetDataSource(DS)
							CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
							CrDoc.SummaryInfo.ReportTitle = "Periode : " & Format(Tgl1.Value, "dd/MMM/yyyy") & " s/d " &
																			Format(Tgl2.Value, "dd/MMM/yyyy")
							CrDoc.RecordSelectionFormula = SF

							With A_Place_For_Printing2
								.Text = "Laporan Pemindahan Waste"
								.CrystalReportViewer1.ReportSource = CrDoc
								.CrystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
								.Refresh()
								.Show()
							End With
						Else

							CloseConn()
							MessageBox.Show("Data Pemindahan Waste Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							Exit Sub

						End If
					End With
				End Using

			ElseIf Cmb_Jenis_Laporan.SelectedIndex = 3 Then

				SQL = "select Kode_Perusahaan from N_EMI_View_Laporan_Transaksi_Waste_Product_Transfer "
				SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
				SQL = SQL & "and Tanggal between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "

				SF = "{N_EMI_View_Laporan_Transaksi_Waste_Product_Transfer.Kode_Perusahaan} = '" & KodePerusahaan & "' "
				SF = SF & "and {N_EMI_View_Laporan_Transaksi_Waste_Product_Transfer.Tanggal} >= #" & Format(Tgl1.Value, "yyyy-MM-dd") & "# and "
				SF = SF & "{N_EMI_View_Laporan_Transaksi_Waste_Product_Transfer.Tanggal} <= #" & Format(Tgl2.Value, "yyyy-MM-dd") & "# "

				If Cmb_Jenis.SelectedIndex <> 0 Then

					Dim value As String = ArrFilterJenis.FirstOrDefault(Function(x) x.ValueCombobox.Trim = Cmb_Jenis.Text.Trim).JenisSql

					SQL = SQL & "and jenis = '" & value & "' "
					SF = SF & "And {N_EMI_View_Laporan_Transaksi_Waste_Product_Transfer.jenis} = '" & value & "'"

				End If

				If Cmb_Status_Transaksi.SelectedIndex > 0 And Cmb_Value_Status_Transaksi.SelectedIndex > 0 Then
					Dim listFilterStatus = ArrFilterStatusTransaksi.Where(Function(x) x.JenisLaporan = "PEMINDAHAN").ToList()

					SQL = SQL & "and " & listFilterStatus(Cmb_Status_Transaksi.SelectedIndex - 1).Sql & " = '" & ArrFilterValueStatusTransaksi(Cmb_Value_Status_Transaksi.SelectedIndex).Sql & "' "
					SF = SF & "And {N_EMI_View_Laporan_Transaksi_Waste_Product_Transfer." & listFilterStatus(Cmb_Status_Transaksi.SelectedIndex - 1).Sql & "} = '" & ArrFilterValueStatusTransaksi(Cmb_Value_Status_Transaksi.SelectedIndex).Sql.Trim & "'"
				End If

				If Not Txt_No_Transaksi.Text.ToUpper.Trim = OpsiSeluruh.ToUpper.Trim Then
					Dim Kolom As String = ArrJenisTransaksi(Cmb_Jenis_Transaksi.SelectedIndex).ToString()
					SQL = SQL & "and " & Kolom & " = '" & Txt_No_Transaksi.Text.Trim & "' "
					SF = SF & "And {N_EMI_View_Laporan_Transaksi_Waste_Product_Transfer." & Kolom & "} = '" & Txt_No_Transaksi.Text.Trim & "'"
				End If

				If Not Txt_No_Split.Text.ToUpper.Trim = OpsiSeluruh.ToUpper.Trim Then
					SQL = SQL & "and No_Production_Order = '" & Txt_No_Split.Text.Trim & "' "
					SF = SF & "And {N_EMI_View_Laporan_Transaksi_Waste_Product_Transfer.No_Production_Order} = '" & Txt_No_Split.Text.Trim & "'"
				End If

				If Cmb_Jenis_Barang.SelectedIndex <> 0 Then
					If Txt_Kd_Barang.Text.ToUpper.Trim <> OpsiSeluruh.ToUpper.Trim Then
						Dim Kolom As String = ArrJenisBarang(Cmb_Jenis_Barang.SelectedIndex).ToString()
						SQL = SQL & "and " & Kolom & " = '" & Txt_Kd_Barang.Text & "' "
						SF = SF & "And {N_EMI_View_Laporan_Transaksi_Waste_Product_Transfer." & Kolom & "} = '" & Txt_Kd_Barang.Text & "'"
					End If
				End If

				Using DS = BindingTrans(SQL)
					With DS.Tables("MyTable")
						If .Rows.Count <> 0 Then

							Dim CrDoc As New N_EMI_CR_Laporan_Transaksi_Waste_Product_Transfer

							CrDoc.SetDataSource(DS)
							CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
							CrDoc.SummaryInfo.ReportTitle = "Periode : " & Format(Tgl1.Value, "dd/MMM/yyyy") & " s/d " &
																			Format(Tgl2.Value, "dd/MMM/yyyy")
							CrDoc.RecordSelectionFormula = SF

							With A_Place_For_Printing2
								.Text = "Laporan Pemindahan Waste"
								.CrystalReportViewer1.ReportSource = CrDoc
								.CrystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
								.Refresh()
								.Show()
							End With
						Else

							CloseConn()
							MessageBox.Show("Data Pemindahan Waste Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
							Exit Sub

						End If
					End With
				End Using

			End If

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub BtnExit_Click(sender As Object, e As EventArgs) Handles BtnExit.Click
		Me.Close()
	End Sub

	'====================================================================================================================
	'=     HANDLE KEYPRESS
	'====================================================================================================================
	Private Sub Tgl1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl1.KeyPress
		If e.KeyChar = Chr(13) Then Tgl2.Focus()
	End Sub

	Private Sub Tgl2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl2.KeyPress
		If e.KeyChar = Chr(13) Then
			Cmb_Jenis.DroppedDown = True
			Cmb_Jenis.Focus()
		End If
	End Sub

	Private Sub Cmb_Lokasi_Asal_Pemusnahan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Lokasi_Asal_Pemusnahan.KeyPress
		If e.KeyChar = Chr(13) Then
			Cmb_Jenis_Transaksi.DroppedDown = True
			Cmb_Jenis_Transaksi.Focus()
		End If
	End Sub

	Private Sub Cmb_Jenis_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Jenis.KeyPress
		If e.KeyChar = Chr(13) Then
			Cmb_Jenis_Transaksi.DroppedDown = True
			Cmb_Jenis_Transaksi.Focus()
		End If
	End Sub

	Private Sub Cmb_Jenis_Transaksi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Jenis_Transaksi.SelectedIndexChanged
		If Cmb_Jenis_Transaksi.SelectedIndex <> 0 Then
			If IsRekap Then
				Txt_No_Transaksi.Enabled = True
				Switch_Auto_Complete = True
				Txt_No_Transaksi.Text = ""
				Switch_Auto_Complete = False
			Else
				Txt_No_Transaksi.Enabled = True
				Switch_Auto_Complete = True
				Txt_No_Transaksi.Text = ""
			End If
			Switch_Auto_Complete = False
			Txt_No_Transaksi.Focus()
		Else
			Txt_No_Transaksi.Enabled = False
			Switch_Auto_Complete = True
			Txt_No_Transaksi.Text = OpsiSeluruh
			Switch_Auto_Complete = False
		End If
	End Sub

	Private Sub Txt_No_Transaksi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_No_Transaksi.KeyPress
		If e.KeyChar = Chr(13) Then
			If Txt_No_Transaksi.Text.Trim.Length = 0 Then Txt_No_Transaksi.Focus()
			Txt_No_Transaksi_Leave(Txt_No_Transaksi, e)

			Me.Size = New Size(808, 407)
			Lv_Transaksi.Visible = False
			Lv_Transaksi.Location = New Point(800, 251)

			'Txt_KdKategori.Focus()
		End If
	End Sub

	Private Sub Txt_No_Transaksi_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_No_Transaksi.KeyDown
		If e.KeyCode = Keys.Down Then Lv_Transaksi.Focus()
	End Sub

	Private Sub Txt_No_Split_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_No_Split.KeyPress
		If e.KeyChar = Chr(13) Then
			If Txt_No_Split.Text.Trim.Length = 0 Then Txt_No_Split.Focus()
			Txt_No_Split_Leave(Txt_No_Split, e)

			Me.Size = New Size(808, 407)
			Lv_Split.Visible = False
			Lv_Split.Location = New Point(800, 278)

			'Txt_KdKategori.Focus()
		End If
	End Sub

	Private Sub Txt_No_Split_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_No_Split.KeyDown
		If e.KeyCode = Keys.Down Then Lv_Split.Focus()
	End Sub

	Private Sub Txt_Kd_Barang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Kd_Barang.KeyPress
		If e.KeyChar = Chr(13) Then
			If Txt_Kd_Barang.Text.Trim.Length = 0 Then Txt_Kd_Barang.Focus()
			Txt_Kd_Barang_Leave(Txt_Kd_Barang, e)

			Me.Size = New Size(808, 407)
			Lv_Barang.Visible = False
			Lv_Barang.Location = New Point(800, 306)

			'Txt_KdKategori.Focus()
		End If
	End Sub

	Private Sub Txt_Kd_Barang_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_Kd_Barang.KeyDown
		If e.KeyCode = Keys.Down Then Lv_Barang.Focus()
	End Sub

	Private Sub Txt_Nm_Barang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Nm_Barang.KeyPress
		If e.KeyChar = Chr(13) Then
			If Txt_Nm_Barang.Text.Trim.Length = 0 Then Txt_Nm_Barang.Focus()
			Txt_Kd_Barang_Leave(Txt_Nm_Barang, e)

			Me.Size = New Size(808, 407)
			Lv_Barang.Visible = False
			Lv_Barang.Location = New Point(800, 306)

			'Txt_KdKategori.Focus()
		End If
	End Sub

	Private Sub Txt_Nm_Barang_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_Nm_Barang.KeyDown
		If e.KeyCode = Keys.Down Then Lv_Barang.Focus()
	End Sub

	Private Sub Cmb_Jenis_Laporan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Jenis_Laporan.KeyPress
		If e.KeyChar = Chr(13) Then
			Cmb_Status_Transaksi.DroppedDown = True
			Cmb_Status_Transaksi.Focus()
		End If
	End Sub

	Private Sub Rd_Status_Seluruh_KeyPress(sender As Object, e As KeyPressEventArgs)
		If e.KeyChar = Chr(13) Then
			Cmb_Lokasi_Asal_Pemusnahan.SelectedIndex = 0

			If IsRekap Then
				Cmb_Jenis.Enabled = False
				Cmb_Jenis_Transaksi.DroppedDown = True
				Cmb_Jenis_Transaksi.Focus()
			Else
				If Cmb_Jenis_Laporan.SelectedIndex = 1 Then
					Cmb_Lokasi_Asal_Pemusnahan.Enabled = True
					Cmb_Lokasi_Asal_Pemusnahan.DroppedDown = True
					Cmb_Lokasi_Asal_Pemusnahan.Focus()
				Else
					Cmb_Lokasi_Asal_Pemusnahan.Enabled = False
					Cmb_Jenis.Enabled = True
					Cmb_Jenis.DroppedDown = True
					Cmb_Jenis.Focus()
				End If
			End If

		End If
	End Sub

	Private Sub Cmb_Jenis_Laporan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Jenis_Laporan.SelectedIndexChanged
		If Cmb_Jenis_Laporan.Items.Count = 0 Or Cmb_Jenis_Laporan.SelectedIndex = -1 Then Exit Sub

		Cmb_Lokasi_Asal_Pemusnahan.SelectedIndex = 0
		Cmb_Jenis.Items.Clear()
		Cmb_Jenis.Items.Add(OpsiSeluruh)

		If Cmb_Jenis_Laporan.SelectedIndex = 1 Then
			Cmb_Lokasi_Asal_Pemusnahan.Enabled = True

		ElseIf Cmb_Jenis_Laporan.SelectedIndex = 3 Then
			Cmb_Lokasi_Asal_Pemusnahan.Enabled = False

			For Each item In ArrFilterJenis.FindAll(Function(x) x.JenisLaporan = "PEMINDAHAN").Select(Of String)(Function(x) x.ValueCombobox).ToList
				Cmb_Jenis.Items.Add(item)
			Next
		Else
			Cmb_Lokasi_Asal_Pemusnahan.Enabled = False

		End If

		Cmb_Status_Transaksi.Items.Clear()
		Cmb_Status_Transaksi.Items.Add(OpsiSeluruh)
		If Cmb_Jenis_Laporan.SelectedIndex <= 1 Then
			For Each item In ArrFilterStatusTransaksi.FindAll(Function(x) x.JenisLaporan = "PEMUSNAHAN_PRODUCTION").Select(Of String)(Function(x) x.ValueCombobox).ToList
				Cmb_Status_Transaksi.Items.Add(item)
			Next
		Else
			For Each item In ArrFilterStatusTransaksi.FindAll(Function(x) x.JenisLaporan = "PEMINDAHAN").Select(Of String)(Function(x) x.ValueCombobox).ToList
				Cmb_Status_Transaksi.Items.Add(item)
			Next
		End If

		Cmb_Status_Transaksi.Enabled = True
		Cmb_Status_Transaksi.SelectedIndex = 0

		Cmb_Jenis.SelectedIndex = 0

		If arrIndexRekap.Contains(Cmb_Jenis_Laporan.SelectedIndex) Then
			HandleSelectedRekap()
		Else
			HandleSelectedDetail()
		End If

	End Sub

	'================================================================================================================================================
	'=     HELPER
	'================================================================================================================================================
	Private Sub HandleSelectedRekap()

		Cmb_Jenis_Transaksi.SelectedIndex = 0
		Cmb_Jenis_Barang.SelectedIndex = 0

		Switch_Auto_Complete = True
		Txt_No_Transaksi.Text = OpsiSeluruh
		Txt_No_Split.Text = OpsiSeluruh
		Txt_Keterangan_Split.Text = OpsiSeluruh
		Txt_Kd_Barang.Text = OpsiSeluruh
		Txt_Nm_Barang.Text = OpsiSeluruh
		Switch_Auto_Complete = False

		Cmb_Jenis.Enabled = False
		Cmb_Jenis_Transaksi.Enabled = True
		Txt_No_Transaksi.Enabled = False
		Txt_No_Split.Enabled = False
		Txt_Keterangan_Split.Enabled = False
		Cmb_Jenis_Barang.Enabled = True
		Txt_Kd_Barang.Enabled = False
		Txt_Nm_Barang.Enabled = False

		IsRekap = True

	End Sub

	Private Sub HandleSelectedDetail()

		Cmb_Jenis_Transaksi.SelectedIndex = 0
		Cmb_Jenis_Barang.SelectedIndex = 0

		Switch_Auto_Complete = True
		Txt_No_Transaksi.Text = OpsiSeluruh
		Txt_No_Split.Text = OpsiSeluruh
		Txt_Keterangan_Split.Text = OpsiSeluruh
		Txt_Kd_Barang.Text = OpsiSeluruh
		Txt_Nm_Barang.Text = OpsiSeluruh
		Switch_Auto_Complete = False

		Cmb_Jenis.Enabled = True
		Cmb_Jenis_Transaksi.Enabled = True
		Txt_No_Transaksi.Enabled = False
		Txt_No_Split.Enabled = True
		Txt_Keterangan_Split.Enabled = False
		Cmb_Jenis_Barang.Enabled = True
		Txt_Kd_Barang.Enabled = False
		Txt_Nm_Barang.Enabled = False

		IsRekap = False

	End Sub

	Private Sub Cmb_Lokasi_Asal_Pemusnahan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Lokasi_Asal_Pemusnahan.SelectedIndexChanged
		If Cmb_Lokasi_Asal_Pemusnahan.Items.Count = 0 Then Exit Sub

		Cmb_Jenis.Items.Clear()
		Cmb_Jenis.Items.Add(OpsiSeluruh)
		If Cmb_Lokasi_Asal_Pemusnahan.SelectedIndex = 1 Then
			For Each item In ArrFilterJenis.FindAll(Function(x) x.JenisLaporan = "PEMUSNAHAN_PRODUCTION").Select(Of String)(Function(x) x.ValueCombobox).ToList
				Cmb_Jenis.Items.Add(item)
			Next
		ElseIf Cmb_Lokasi_Asal_Pemusnahan.SelectedIndex = 2 Then
			For Each item In ArrFilterJenis.FindAll(Function(x) x.JenisLaporan = "PEMUSNAHAN_WASTE_STORAGE").Select(Of String)(Function(x) x.ValueCombobox).ToList
				Cmb_Jenis.Items.Add(item)
			Next
		End If
		Cmb_Jenis.SelectedIndex = 0
	End Sub

	Private Sub Cmb_Status_Transaksi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Status_Transaksi.SelectedIndexChanged
		If Cmb_Status_Transaksi.Items.Count = 0 Then Exit Sub

		If Cmb_Status_Transaksi.SelectedIndex = 0 Then
			Cmb_Value_Status_Transaksi.Enabled = False
		Else
			Cmb_Value_Status_Transaksi.Enabled = True
		End If
		Cmb_Value_Status_Transaksi.SelectedIndex = 0

	End Sub

	Private Sub Cmb_Value_Status_Transaksi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Value_Status_Transaksi.KeyPress
		If e.KeyChar = Chr(13) Then
			Cmb_Lokasi_Asal_Pemusnahan.Focus()
		End If
	End Sub

	Private Sub Cmb_Status_Transaksi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Status_Transaksi.KeyPress
		If e.KeyChar = Chr(13) Then
			Cmb_Value_Status_Transaksi.DroppedDown = True
			Cmb_Value_Status_Transaksi.Focus()
		End If
	End Sub

	'================================================================================================================================================
	'=     UTILITY
	'================================================================================================================================================
	Protected Overrides Sub WndProc(ByRef m As Message)
		' WM_NCLBUTTONDBLCLK = 0xA3 (double click di title bar)
		If m.Msg = &HA3 Then
			Return  ' Abaikan pesan, sehingga form tidak maximize
		End If

		MyBase.WndProc(m)
	End Sub

End Class
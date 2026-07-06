Public Class Roles_Button

	Private Sub Kosong()
		Try
			OpenConn()

			ComboBox2.Items.Clear() : ListView1.Items.Clear()
			SQL = "select * from users where kode_perusahaan = '" & KodePerusahaan & "' order by userid"
			Using dr = OpenTrans(SQL)
				Do While dr.Read
					ComboBox2.Items.Add(dr("userid"))
					Dim lv As New ListViewItem
					lv = ListView1.Items.Add(dr("userid"))
					lv.SubItems.Add(dr("username"))
				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		For i As Integer = 0 To ListView2.Items.Count - 1
			ListView2.Items(i).Checked = False
		Next

		CheckBox1.Checked = False
		ListView3.Items.Clear()

		TextBox1.Text = ""
		Button1.Enabled = True : Button2.Enabled = True
	End Sub

	Private Sub Perusahaan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		Kosong()

		ComboBox2.Focus()

		Dim lv As New ListViewItem

		lv = ListView2.Items.Add("Simpan_Purchase_Requisition") : lv.SubItems.Add("PR")
		lv = ListView2.Items.Add("Simpan_Purchase_Requisition_Barang_Lain") : lv.SubItems.Add("PR")

		'
		lv = ListView2.Items.Add("Release_Purchase_Requisition") : lv.SubItems.Add("PR")
		lv = ListView2.Items.Add("Release_Purchase_Requisition_Barang_Lain") : lv.SubItems.Add("PR")

		lv = ListView2.Items.Add("Edit_Pembelian_PO") : lv.SubItems.Add("Purchase")

		lv = ListView2.Items.Add("Validasi_Batal_Detail_Biaya_Import") : lv.SubItems.Add("Import")
		lv = ListView2.Items.Add("Validasi_Batal_Detail_Biaya_Import2") : lv.SubItems.Add("Import")
		lv = ListView2.Items.Add("Validasi_Batal_Detail_Biaya_Import3") : lv.SubItems.Add("Import")
		lv = ListView2.Items.Add("Validasi_Detail_Biaya_Import") : lv.SubItems.Add("Import")
		lv = ListView2.Items.Add("Validasi_Detail_Biaya_Import2") : lv.SubItems.Add("Import")
		lv = ListView2.Items.Add("Validasi_Detail_Biaya_Import3") : lv.SubItems.Add("Import")
		lv = ListView2.Items.Add("Import_Edit_OTW") : lv.SubItems.Add("Import")
		lv = ListView2.Items.Add("Import_Edit_Tarik_Kontainer") : lv.SubItems.Add("Import")
		lv = ListView2.Items.Add("Import_Update_Tracking_Pengiriman") : lv.SubItems.Add("Import")
		lv = ListView2.Items.Add("Batal_Status_Otw") : lv.SubItems.Add("Import")
		lv = ListView2.Items.Add("Batal_Tracking_Dokumen") : lv.SubItems.Add("Import")
		lv = ListView2.Items.Add("Batal_Kapal_Tiba") : lv.SubItems.Add("Import")
		lv = ListView2.Items.Add("Batal_Penjaluran") : lv.SubItems.Add("Import")
		lv = ListView2.Items.Add("Batal_Sppb") : lv.SubItems.Add("Import")
		lv = ListView2.Items.Add("Batal_Bongkar") : lv.SubItems.Add("Import")
		lv = ListView2.Items.Add("Batal_Rencana_Order_Gabungan") : lv.SubItems.Add("Import")
		lv = ListView2.Items.Add("Batal_Tarik_Kontainer") : lv.SubItems.Add("Import")
		lv = ListView2.Items.Add("Ganti_Lokasi_Display_Master_Paket") : lv.SubItems.Add("Import")
		lv = ListView2.Items.Add("Batal_Submit_PO") : lv.SubItems.Add("Import")
		lv = ListView2.Items.Add("Update_Status_Otw") : lv.SubItems.Add("Import")
		lv = ListView2.Items.Add("Batal_Loading_Barang") : lv.SubItems.Add("Import")
		lv = ListView2.Items.Add("Batal_Hpp_Import") : lv.SubItems.Add("Import")
		lv = ListView2.Items.Add("Edit_Tanggal_Tarik") : lv.SubItems.Add("Import")
		lv = ListView2.Items.Add("Edit_Berat_Timbangan") : lv.SubItems.Add("Import")
		lv = ListView2.Items.Add("Batal_Transaksi_Biaya_Import") : lv.SubItems.Add("Import")
		lv = ListView2.Items.Add("Input_Alasan_Terlambat_Bongkar") : lv.SubItems.Add("Import")
		lv = ListView2.Items.Add("Hapus_Biaya_Import_Detail") : lv.SubItems.Add("Import")
		lv = ListView2.Items.Add("Hapus_Biaya_Import_Detail2") : lv.SubItems.Add("Import")
		lv = ListView2.Items.Add("Hapus_Biaya_Import_Detail3") : lv.SubItems.Add("Import")
		lv = ListView2.Items.Add("Validasi_Selisih_Pabrik") : lv.SubItems.Add("Import")
		lv = ListView2.Items.Add("Update_Status_Otw_ETD") : lv.SubItems.Add("Import")

		'
		lv = ListView2.Items.Add("Validasi_Selesai_Pengajuan") : lv.SubItems.Add("Finance")
		lv = ListView2.Items.Add("Batal_Pengajuan_Biaya") : lv.SubItems.Add("Finance")
		lv = ListView2.Items.Add("Validasi_Selesai_Pengajuan_Token") : lv.SubItems.Add("Finance")
		lv = ListView2.Items.Add("Batal_Pengajuan_Token") : lv.SubItems.Add("Finance")
		lv = ListView2.Items.Add("Tanda_Tangan_Sementara_Cheque") : lv.SubItems.Add("Finance")
		lv = ListView2.Items.Add("Batal_Tanda_Tangan_sementara_cheque") : lv.SubItems.Add("Finance")
		lv = ListView2.Items.Add("Batal_Cheque") : lv.SubItems.Add("Finance")
		lv = ListView2.Items.Add("Hapus_Cheque") : lv.SubItems.Add("Finance")
		lv = ListView2.Items.Add("Batal_Jurnal_Sementara") : lv.SubItems.Add("Finance")
		lv = ListView2.Items.Add("Validasi_Jurnal_Sementara") : lv.SubItems.Add("Finance")

		lv = ListView2.Items.Add("Ganti_Lokasi_Display_Penjualan_proyek") : lv.SubItems.Add("Project")
		lv = ListView2.Items.Add("Ganti_Lokasi_Display_Pengeluaran_Barang_proyek") : lv.SubItems.Add("Project")
		lv = ListView2.Items.Add("Cetak_Ulang_Pengeluaran_Barang_proyek") : lv.SubItems.Add("Project")
		lv = ListView2.Items.Add("Batal_Pengeluaran_Barang_Proyek") : lv.SubItems.Add("Project")
		lv = ListView2.Items.Add("Batal_Pelunasan_Pembelian_Proyek") : lv.SubItems.Add("Project")
		lv = ListView2.Items.Add("Batal_Request_Material_Proyek") : lv.SubItems.Add("Project")
		lv = ListView2.Items.Add("Batal_Barang_Masuk_New") : lv.SubItems.Add("Project")
		lv = ListView2.Items.Add("Validasi_Penyelesaian_Selisih_Barang_Masuk") : lv.SubItems.Add("Project")
		lv = ListView2.Items.Add("Batal_PO_Proyek") : lv.SubItems.Add("Project")
		lv = ListView2.Items.Add("Batal_Pembelian_Proyek_New") : lv.SubItems.Add("Project")
		lv = ListView2.Items.Add("Batal_Barang_Masuk_Proyek") : lv.SubItems.Add("Project")
		lv = ListView2.Items.Add("Edit_RAB_Pembelian_Proyek") : lv.SubItems.Add("Project")

		lv = ListView2.Items.Add("Pengeluaran_Bahan_Baku") : lv.SubItems.Add("Production")
		lv = ListView2.Items.Add("Hasil_FG") : lv.SubItems.Add("Production")

		lv = ListView2.Items.Add("Report_Produksi_Biaya") : lv.SubItems.Add("Production")
		lv = ListView2.Items.Add("Report_Produksi_GI_Ada_HPP") : lv.SubItems.Add("Production")
		lv = ListView2.Items.Add("Report_Produksi_Biaya_GI_Tanpa_HPP") : lv.SubItems.Add("Production")
		lv = ListView2.Items.Add("Report_Hasil_Produksi_Ada_HPP") : lv.SubItems.Add("Production")
		lv = ListView2.Items.Add("Report_Hasil_Produksi_Tanpa_HPP") : lv.SubItems.Add("Production")
		lv = ListView2.Items.Add("Report_Validasi_GR") : lv.SubItems.Add("Production")
		lv = ListView2.Items.Add("Report_Validasi_GR_Ada_HPP") : lv.SubItems.Add("Production")

		'

		lv = ListView2.Items.Add("Cari_KB") : lv.SubItems.Add("Sales")
		lv = ListView2.Items.Add("Penjualan_Tampil_Harga") : lv.SubItems.Add("Sales")

		lv = ListView2.Items.Add("Jurnal_Entry") : lv.SubItems.Add("Accounting")
		lv = ListView2.Items.Add("Update_Jurnal") : lv.SubItems.Add("Accounting")
		lv = ListView2.Items.Add("Update_Jurnal_Lewat_10_Menit") : lv.SubItems.Add("Accounting")
		lv = ListView2.Items.Add("Cetak_LR") : lv.SubItems.Add("Accounting")
		lv = ListView2.Items.Add("Akun_Khusus") : lv.SubItems.Add("Accounting")

		lv = ListView2.Items.Add("Laporan_Material_Usage_ACC") : lv.SubItems.Add("HPP Laporan_Material_Usage_ACC")
		lv = ListView2.Items.Add("Tanda_Tangan_Cheque") : lv.SubItems.Add("Tanda_Tangan_Cheque")

		lv = ListView2.Items.Add("UnRelease_Purchase_Requisition") : lv.SubItems.Add("PR")
		lv = ListView2.Items.Add("Pembatalan_PR") : lv.SubItems.Add("PR")
		lv = ListView2.Items.Add("Pengajuan_Batal_PR") : lv.SubItems.Add("PR")
		lv = ListView2.Items.Add("Validasi_Batal_PR") : lv.SubItems.Add("PR")
		lv = ListView2.Items.Add("Pembatalan_POInduk") : lv.SubItems.Add("Purchase")
		lv = ListView2.Items.Add("Penyelesaian_POInduk") : lv.SubItems.Add("Purchase")
		lv = ListView2.Items.Add("Pembatalan_PO") : lv.SubItems.Add("Purchase")
		lv = ListView2.Items.Add("Penyelesaian_SubPO") : lv.SubItems.Add("Purchase")

		lv = ListView2.Items.Add("Release_PO_Produksi") : lv.SubItems.Add("Produksi")
		lv = ListView2.Items.Add("UnRelease_PO_Produksi") : lv.SubItems.Add("Produksi")

		lv = ListView2.Items.Add("update_barang") : lv.SubItems.Add("Barang")
		lv = ListView2.Items.Add("update_barang_lain") : lv.SubItems.Add("Barang")

		lv = ListView2.Items.Add("Ganti_Lokasi_Display_SubPO_Barang_Lain") : lv.SubItems.Add("Asset")
		lv = ListView2.Items.Add("Penyelesaian_SubPO_Barang_Lain") : lv.SubItems.Add("Asset")
		lv = ListView2.Items.Add("Pembatalan_SubPO_Barang_Lain") : lv.SubItems.Add("Asset")

		lv = ListView2.Items.Add("Validasi_Pembelian_Down_Payment") : lv.SubItems.Add("Accounting")
		lv = ListView2.Items.Add("Validasi_Pembelian_Down_Payment_Aset") : lv.SubItems.Add("Accounting")
		lv = ListView2.Items.Add("Validasi_Pembelian_Down_Payment_Proyek") : lv.SubItems.Add("Accounting")

		lv = ListView2.Items.Add("Batal_Bahan_Bakar") : lv.SubItems.Add("Warehouse")
		lv = ListView2.Items.Add("Batal_Pengeluaran_Stock") : lv.SubItems.Add("Warehouse")
		lv = ListView2.Items.Add("Batal_Restock") : lv.SubItems.Add("Warehouse")
		lv = ListView2.Items.Add("Batal_Split_Stock") : lv.SubItems.Add("Warehouse")
		lv = ListView2.Items.Add("Batal_Transfer_Stock2") : lv.SubItems.Add("Warehouse")
		lv = ListView2.Items.Add("Batal_Material_To_Material") : lv.SubItems.Add("Warehouse")

		lv = ListView2.Items.Add("Validasi_Pemusnahan_Barang") : lv.SubItems.Add("Warehouse")

		lv = ListView2.Items.Add("Tampil_Detail_GI") : lv.SubItems.Add("Production")
		lv = ListView2.Items.Add("Validasi_GI") : lv.SubItems.Add("Production")
		lv = ListView2.Items.Add("Validasi_GR") : lv.SubItems.Add("Production")

		'lv = ListView2.Items.Add("Pengajuan_Batal_PR") : lv.SubItems.Add("Purchase")
		lv = ListView2.Items.Add("Validasi_Batal_PR_Barang_Lain") : lv.SubItems.Add("Purchase")
		lv = ListView2.Items.Add("Pembatalan_PR_Barang_Lain") : lv.SubItems.Add("Purchase")
		lv = ListView2.Items.Add("Pembatalan_POInduk_Barang_Lain") : lv.SubItems.Add("Purchase")
		lv = ListView2.Items.Add("Penyelesaian_POInduk_Barang_Lain") : lv.SubItems.Add("Purchase")
		lv = ListView2.Items.Add("Pembatalan_PO_Barang_Lain") : lv.SubItems.Add("Purchase")
		'lv = ListView2.Items.Add("Penyelesaian_SubPO_Barang_Lain") : lv.SubItems.Add("Purchase")

		lv = ListView2.Items.Add("Report_HPPProduksi_Column_GI") : lv.SubItems.Add("Production")
		lv = ListView2.Items.Add("Report_HPPProduksi_Column_Bahan_Baku") : lv.SubItems.Add("Production")
		lv = ListView2.Items.Add("Report_HPPProduksi_Column_Packaging") : lv.SubItems.Add("Production")
		lv = ListView2.Items.Add("Report_HPPProduksi_Column_Cost_Production") : lv.SubItems.Add("Production")
		lv = ListView2.Items.Add("Report_HPPProduksi_Column_HPP") : lv.SubItems.Add("Production")

		lv = ListView2.Items.Add("Report_Packaging_Sekunder") : lv.SubItems.Add("Production")
		lv = ListView2.Items.Add("Report_GR_3_Summary") : lv.SubItems.Add("Production")
		lv = ListView2.Items.Add("Batal_RM_QC") : lv.SubItems.Add("Production")

		lv = ListView2.Items.Add("Flag_Validasi_Tambah") : lv.SubItems.Add("Flag_Validasi_Tambah")

		lv = ListView2.Items.Add("Simpan_Validasi_BM_Selisih") : lv.SubItems.Add("Purchase")
		lv = ListView2.Items.Add("Simpan_Validasi_BM_Tidak_Selisih") : lv.SubItems.Add("Purchase")
		lv = ListView2.Items.Add("Simpan_Validasi_BM_Selisih_Barang_Lain") : lv.SubItems.Add("Purchase")
		lv = ListView2.Items.Add("Simpan_Validasi_BM_Tidak_Selisih_Barang_Lain") : lv.SubItems.Add("Purchase")
		lv = ListView2.Items.Add("Simpan_Purchase_Requisition_Departement") : lv.SubItems.Add("Purchase")
		lv = ListView2.Items.Add("Release_Purchase_Requisition_Departement_Barang_Lain") : lv.SubItems.Add("Purchase")
		lv = ListView2.Items.Add("Simpan_Purchase_Requisition_Barang_Lain_Departement") : lv.SubItems.Add("Purchase")
		lv = ListView2.Items.Add("Release_Purchase_Requisition_Departement") : lv.SubItems.Add("Purchase")

		lv = ListView2.Items.Add("Cancel_GI") : lv.SubItems.Add("Production")

		lv = ListView2.Items.Add("Tampil_Offer_Dashboard_Procurment") : lv.SubItems.Add("Dashboard")
		lv = ListView2.Items.Add("Tampil_Purchase_Value_Dashboard_Procurment") : lv.SubItems.Add("Dashboard")
		lv = ListView2.Items.Add("Tampil_Offer_Dashboard_Procurment_Barang_Lain") : lv.SubItems.Add("Dashboard")
		lv = ListView2.Items.Add("Tampil_Purchase_Value_Dashboard_Procurment_Barang_Lain") : lv.SubItems.Add("Dashboard")
		lv = ListView2.Items.Add("Tampil_Semua_Barang_Lain") : lv.SubItems.Add("Dashboard")

		lv = ListView2.Items.Add("Ubah_ETD_ETA_PO_Induk") : lv.SubItems.Add("Purchase")
		lv = ListView2.Items.Add("Ubah_ETD_ETA_Sub_PO") : lv.SubItems.Add("Purchase")
		lv = ListView2.Items.Add("Ubah_ETD_ETA_PO_Induk_Barang_Lain") : lv.SubItems.Add("Purchase")
		lv = ListView2.Items.Add("Ubah_ETD_ETA_Sub_PO_Barang_Lain") : lv.SubItems.Add("Purchase")

		lv = ListView2.Items.Add("Pembatalan_PR_Barang_Lain_Departement") : lv.SubItems.Add("Purchase")
		lv = ListView2.Items.Add("UnRelease_Purchase_Requisition_Barang_Lain") : lv.SubItems.Add("Purchase")
		lv = ListView2.Items.Add("UnRelease_Purchase_Requisition_Departement") : lv.SubItems.Add("Purchase")
		'Cetak_Faktur_Waste_Produk
		'Cetak_Faktur_Waste_Process
		'
		lv = ListView2.Items.Add("Batal_Transaksi_Barcode_Merge") : lv.SubItems.Add("Production")
		lv = ListView2.Items.Add("Batal_Transaksi_Barcode_Merge_Per_Barcode") : lv.SubItems.Add("Production")
		lv = ListView2.Items.Add("Simpan_Approval_Level_Waste") : lv.SubItems.Add("Production")
		lv = ListView2.Items.Add("Validasi_Transaksi_Waste_Proses") : lv.SubItems.Add("Production")
		lv = ListView2.Items.Add("Transaksi_Waste_Product_Transfer") : lv.SubItems.Add("Production")

		lv = ListView2.Items.Add("Validasi_Adjustment_Stock_Accounting") : lv.SubItems.Add("Accounting")
		lv = ListView2.Items.Add("Cetak_Laporan_Penjualan_Ada_HPP") : lv.SubItems.Add("Sales")

		lv = ListView2.Items.Add("Cetak_Laporan_Penjualan_Ada_Harga") : lv.SubItems.Add("Sales")

		lv = ListView2.Items.Add("Batal_Transfer_Stock_Sementara") : lv.SubItems.Add("Warehouse")

		lv = ListView2.Items.Add("Cetak_Laporan_Pengeluaran_Stock_Proyek_Ada_HPP") : lv.SubItems.Add("Project")
		lv = ListView2.Items.Add("Cetak_Laporan_Pengeluaran_Stock_Ada_HPP") : lv.SubItems.Add("Warehouse")
		lv = ListView2.Items.Add("Cetak_Laporan_Pengeluaran_Stock_Barang_Lain_Ada_HPP") : lv.SubItems.Add("Warehouse")

		lv = ListView2.Items.Add("Lihat_Neraca_Lama") : lv.SubItems.Add("Accounting")

		lv = ListView2.Items.Add("attachment_pr_barang_lain") : lv.SubItems.Add("Purchase")
		lv = ListView2.Items.Add("attachment_pr_dept_barang_lain") : lv.SubItems.Add("Purchase")

		lv = ListView2.Items.Add("Batal_Validasi_Penerimaan_Barang") : lv.SubItems.Add("Production")

		lv = ListView2.Items.Add("Selesai_Barang_PO_Induk") : lv.SubItems.Add("Dashboard")
		lv = ListView2.Items.Add("Selesai_PO_Induk") : lv.SubItems.Add("Dashboard")
		lv = ListView2.Items.Add("Batal_PO_Induk") : lv.SubItems.Add("Dashboard")
		lv = ListView2.Items.Add("Selesai_PO_Induk_Barang_Lain") : lv.SubItems.Add("Dashboard")
		lv = ListView2.Items.Add("Selesai_Barang_PO_Induk_Barang_Lain") : lv.SubItems.Add("Dashboard")
		lv = ListView2.Items.Add("Batal_PO_Induk_Barang_Lain") : lv.SubItems.Add("Dashboard")

		lv = ListView2.Items.Add("Tampil_Supplier_Dashboard_Barang_Lain") : lv.SubItems.Add("Dashboard")
		lv = ListView2.Items.Add("Tampil_Supplier_Dashboard") : lv.SubItems.Add("Dashboard")

		lv = ListView2.Items.Add("Penyelesaian_PR_No_Offer_Barang_Lain") : lv.SubItems.Add("Purchase")
		lv = ListView2.Items.Add("Penyelesaian_PR_Offered_Barang_Lain") : lv.SubItems.Add("Purchase")
		lv = ListView2.Items.Add("Penyelesaian_PR_Waiting_Offer_Barang_Lain") : lv.SubItems.Add("Purchase")
		lv = ListView2.Items.Add("Batal_DO_Barang_Lain") : lv.SubItems.Add("Purchase")
		lv = ListView2.Items.Add("Batal_DO") : lv.SubItems.Add("Purchase")

		lv = ListView2.Items.Add("Release_PO_Produksi_Trial") : lv.SubItems.Add("Production")
		lv = ListView2.Items.Add("UnRelease_PO_Produksi_Trial") : lv.SubItems.Add("Production")
		lv = ListView2.Items.Add("RM_Ubah_Lokasi_Tujuan_Trial") : lv.SubItems.Add("Production")
		lv = ListView2.Items.Add("Validasi_Hpp_Formula") : lv.SubItems.Add("Production")
		lv = ListView2.Items.Add("Input_Trial_Good_Received_1") : lv.SubItems.Add("Production")
		lv = ListView2.Items.Add("Get_Formula_RM_Trial") : lv.SubItems.Add("Production")
		lv = ListView2.Items.Add("Insert_Penyediaan_Bahan_Baku_Trial") : lv.SubItems.Add("Production")
		lv = ListView2.Items.Add("Akses_Validasi_Penyediaan_Bahan_Baku_Trial") : lv.SubItems.Add("Production")

		lv = ListView2.Items.Add("Cetak_Faktur_Waste_Produk") : lv.SubItems.Add("Production")
		lv = ListView2.Items.Add("Cetak_Faktur_Waste_Process") : lv.SubItems.Add("Production")

		lv = ListView2.Items.Add("Akses_Formula_PO") : lv.SubItems.Add("Formula")

		lv = ListView2.Items.Add("Selesai_Production_Order_Outstanding") : lv.SubItems.Add("Production")
		lv = ListView2.Items.Add("Validasi_Formula_Binding") : lv.SubItems.Add("Formula")
		lv = ListView2.Items.Add("Tampil_Stock_Display_Stock_Produksi") : lv.SubItems.Add("Formula")

		lv = ListView2.Items.Add("EMI_Transaksi_ForecastOrder_PPIC") : lv.SubItems.Add("Production Plan")
		lv = ListView2.Items.Add("Realease_Forecast_PPIC") : lv.SubItems.Add("Production Plan")
		lv = ListView2.Items.Add("Unrealease_Forecast_PPIC") : lv.SubItems.Add("Production Plan")
		lv = ListView2.Items.Add("EMI_Transaksi_ForecastOrder_Sales") : lv.SubItems.Add("Production Plan")
		lv = ListView2.Items.Add("Realease_Forecast_Sales") : lv.SubItems.Add("Production Plan")
		lv = ListView2.Items.Add("Unrealease_Forecast_Sales") : lv.SubItems.Add("Production Plan")

		lv = ListView2.Items.Add("Show_Scrap_Packaging_Laporan_GIGR_Final") : lv.SubItems.Add("Production")

		'
	End Sub

	Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
		Me.Close()
	End Sub

	Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
		Kosong()
		ComboBox2.Focus()
	End Sub

	Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
		If ComboBox2.SelectedIndex = -1 Then
			MessageBox.Show("UserID harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			ComboBox2.Focus() : Exit Sub
		ElseIf ListView2.CheckedItems.Count = 0 Then
			MessageBox.Show("Roles harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			ListView2.Focus() : Exit Sub
		End If

		Try
			OpenConn()

			Cmd.Transaction = Cn.BeginTransaction

			If Button1.Text = "&Simpan" Then
				SQL = "delete from role_button where kode_perusahaan = '" & KodePerusahaan & "' and userid = '" & ComboBox2.Text & "'"
				ExecuteTrans(SQL)

				For i As Integer = 0 To ListView2.Items.Count - 1
					If ListView2.Items(i).Checked = True Then
						SQL = "Insert Into role_button(Kode_Perusahaan, userid, buttonname) Values('" & KodePerusahaan & "', "
						SQL = SQL & "'" & ComboBox2.Text & "', '" & ListView2.Items(i).Text & "')"
						ExecuteTrans(SQL)
					End If
				Next
			End If

			Cmd.Transaction.Commit()

			CloseConn()
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Kosong()
		ComboBox2.Focus()
	End Sub

	Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
		If ComboBox2.SelectedIndex = -1 Then
			MessageBox.Show("UserID harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			ComboBox2.Focus() : Exit Sub
		End If

		Try
			OpenConn()

			Cmd.Transaction = Cn.BeginTransaction

			SQL = "delete from role_button where kode_perusahaan = '" & KodePerusahaan & "' and userid = '" & ComboBox2.Text & "'"
			ExecuteTrans(SQL)

			Cmd.Transaction.Commit()

			CloseConn()
		Catch ex As Exception
			CloseTrans()
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

		Kosong()
		ComboBox2.Focus()
	End Sub

	Private Sub Master_Sales_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SizeChanged
		Label1.Size = New Point(Me.Width, 33)
	End Sub

	Private Sub ComboBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox2.KeyPress
		If e.KeyChar = Chr(13) Then TextBox1.Focus()
	End Sub

	Private Sub ComboBox2_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox2.Leave
		Try
			OpenConn()

			SQL = "select * from users where kode_perusahaan = '" & KodePerusahaan & "' and userid = '" & ComboBox2.Text & "'"
			Using dr = OpenTrans(SQL)
				If dr.Read Then
					TextBox1.Text = dr("username")
				Else
					TextBox1.Text = ""
				End If
			End Using

			For i As Integer = 0 To ListView2.Items.Count - 1
				ListView2.Items(i).Checked = False
			Next

			SQL = "select * from role_button where kode_perusahaan = '" & KodePerusahaan & "' and userid = '" & ComboBox2.Text & "'"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					For i As Integer = 0 To ListView2.Items.Count - 1
						If Dr("buttonname") = ListView2.Items(i).Text Then
							ListView2.Items(i).Checked = True
							Exit For
						End If
					Next
				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
		If CheckBox1.Checked = True Then
			For i As Integer = 0 To ListView2.Items.Count - 1
				ListView2.Items(i).Checked = True
			Next
		Else
			For i As Integer = 0 To ListView2.Items.Count - 1
				ListView2.Items(i).Checked = False
			Next
		End If
	End Sub

	Private Sub ListView1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListView1.SelectedIndexChanged
		Try
			OpenConn()

			ListView3.Items.Clear()
			SQL = "select a.kode_trans, a.nama_trans from journal_otomatis a, role_wallet b where "
			SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan and a.kode_trans = b.kode_trans and "
			SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and b.userid = '" & ListView1.FocusedItem.Text & "'"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Dim Lvw As ListViewItem
					Lvw = ListView3.Items.Add(Dr("kode_trans"))
					Lvw.SubItems.Add(Dr("nama_trans"))
				Loop
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click

	End Sub

End Class
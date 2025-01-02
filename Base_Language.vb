Imports System.Security.Cryptography.X509Certificates
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class Base_Language
    'Global 
    Public Shared Lang_Global_Kode_Unik_Asal As String
    Public Shared Lang_Global_Kode_Unik_Berjalan As String
    Public Shared Lang_Global_Nilai_Formula As String
    Public Shared Lang_Global_Nilai_Produksi As String
    Public Shared Lang_Global_Qty_Hasil_Produksi As String

    Public Shared Lang_Global_Simpan As String
    Public Shared Lang_Global_Hapus As String
    Public Shared Lang_Global_Update As String
    Public Shared Lang_Global_Cari As String
    Public Shared Lang_Global_Refresh As String
    Public Shared Lang_Global_Tanya_Hapus As String
    Public Shared Lang_Global_Hapus_No As String
    Public Shared Lang_Global_Error1 As String
    Public Shared Lang_Global_Perhatian As String
    Public Shared Lang_Global_Data_Tdk_Ditemukan As String
    Public Shared Lang_Global_Sukses_Update As String
    Public Shared Lang_Global_Data_Sdh_Ada As String
    Public Shared Lang_Global_Sukses_Simpan As String
    Public Shared Lang_Global_Error_Paramater As String
    Public Shared Lang_Global_Error_Paramater_Tgl As String
    Public Shared Lang_Global_Error_Paramater_Tgl2 As String
    Public Shared Lang_Global_Error_Paramater_Lain As String
    Public Shared Lang_Global_Error_Paramater_Lain2 As String
    Public Shared Lang_Global_Error_Tdk_Ada_Akses As String
    Public Shared Lang_Global_Berhasil_Batal As String
    Public Shared Lang_Global_Customer As String
    Public Shared Lang_Global_NoInquiry As String
    Public Shared Lang_Global_NoFaktur As String
    Public Shared Lang_Global_Tanggal As String
    Public Shared Lang_Global_Belum_Diisi As String
    Public Shared Lang_Global_Belum_Diubah As String
    Public Shared Lang_Global_NoFormula As String
    Public Shared Lang_Global_KodeCustomer As String
    Public Shared Lang_Global_KodeBarang As String
    Public Shared Lang_Global_NamaBarang As String
    Public Shared Lang_Global_NamaCustomer As String
    Public Shared Lang_Global_SeluruhCombobox As String
    Public Shared Lang_Global_Lokasi As String
    Public Shared Lang_Global_LokasiGudang As String
    Public Shared Lang_Global_Hasil As String
    Public Shared Lang_Global_Satuan As String
    Public Shared Lang_Global_FormAsal As String
    Public Shared Lang_Global_Tidak_Bisa_Berbeda As String
    Public Shared Lang_Global_Nilai_Pengali As String
    Public Shared Lang_Global_Satuan_Barang As String
    Public Shared Lang_Global_Nilai_Barang As String
    Public Shared Lang_Global_jumlah_kuantiti As String
    Public Shared Lang_Global_Persentase As String
    Public Shared Lang_Global_Hari_ini As String
    Public Shared Lang_Global_Para_Tbl As String
    Public Shared Lang_Global_Para_lain As String
    Public Shared Lang_Global_Sukses_Hapus As String
    Public Shared Lang_Global_Kolom As String
    Public Shared Lang_Global_Nama As String
    Public Shared Lang_Global_Penawaran As String
    Public Shared Lang_Global_Supplier As String
    Public Shared Lang_Global_MinOrder As String
    Public Shared Lang_Global_HargaSatuan As String
    Public Shared Lang_Global_Bahan As String
    Public Shared Lang_Global_Kandungan As String
    Public Shared Lang_Global_Total As String
    Public Shared Lang_Global_TotalPersen As String
    Public Shared Lang_Global_Persen_harus_100 As String
    Public Shared Lang_Global_Kurs As String
    Public Shared Lang_Global_Lokasi_Tujuan As String
    Public Shared Lang_Global_Lokasi_Awal As String
    Public Shared Lang_Global_Kemasan As String
    Public Shared Lang_Global_Cara_Kirim As String
    Public Shared Lang_Global_Bahan_Baku As String
    Public Shared Lang_Global_TidakBolehLebihDari As String
    Public Shared Lang_Global_NoNota As String
    Public Shared Lang_Global_DataSudahBatal As String
    Public Shared Lang_Global_Jam As String
    Public Shared Lang_Global_JenisPembayaran As String
    Public Shared Lang_Global_MataUang As String
    Public Shared Lang_Global_CaraBayar As String
    Public Shared Lang_Global_GrandSblmPPN As String
    Public Shared Lang_Global_TotalMUA As String
    Public Shared Lang_Global_PPN As String
    Public Shared Lang_Global_Jumlah As String
    Public Shared Lang_Global_Harga As String
    Public Shared Lang_Global_Validasi As String
    Public Shared Lang_Global_Sukses_Validasi As String
    Public Shared Lang_Global_Barang As String
    Public Shared Lang_Global_Gudang As String
    Public Shared Lang_Global_Asal As String
    Public Shared Lang_Global_Tujuan As String
    Public Shared Lang_Global_Kecamatan As String
    Public Shared Lang_Global_Kelurahan As String
    Public Shared Lang_Global_Error_Kd_Brg As String
    Public Shared Lang_Global_Error_Nm_Brg As String
    Public Shared Lang_Global_Error_Lokasi_Tujuan As String
    Public Shared Lang_Global_Error_Satuan As String
    Public Shared Lang_Global_Error_Jumlah As String
    Public Shared Lang_Global_Error_Jns_Pengali As String
    Public Shared Lang_Global_Error_Jns_Hitung As String
    Public Shared Lang_Global_Error_Lv_Kosong As String
    Public Shared Lang_Global_No_PO As String
    Public Shared Lang_Global_Panjang As String
    Public Shared Lang_Global_Lebar As String
    Public Shared Lang_Global_Tinggi As String
    Public Shared Lang_Global_Ukuran As String
    Public Shared Lang_Global_Kode As String
    Public Shared Lang_Global_Media_Kirim As String
    Public Shared Lang_Global_Berat As String
    Public Shared Lang_Global_SatuanVolume As String
    Public Shared Lang_Global_SatuanBerat As String
    Public Shared Lang_Global_HitungVolume As String
    ' Public Shared Lang_Global_Lokasi As String
    Public Shared Lang_Global_Kode_Supplier As String
    'Public Shared Lang_Global_Supplier As String
    Public Shared Lang_Global_Tanggal_Jatuh_Tempo As String
    Public Shared Lang_Global_Nilai_PPN As String
    Public Shared Lang_Global_Diskon_Persen As String
    Public Shared Lang_Global_Nilai_Diskon As String
    Public Shared Lang_Global_Jenis_Satuan As String
    Public Shared Lang_Global_Data_Produk As String
    Public Shared Lang_Global_No_Nota As String
    Public Shared Lang_Global_Produksi As String
    Public Shared Lang_Global_Expired As String
    Public Shared Lang_Global_Error_Update As String
    Public Shared Lang_Global_Alert_Simpan As String
    Public Shared Lang_Global_Label_No_Inquiry_PO As String
    Public Shared Lang_Global_KodeBarangTidakAda As String
    Public Shared lang_global_keterangan As String
    Public Shared lang_global_Nama_Supplier As String
    Public Shared lang_global_Error_LokasiTidakAda As String
    Public Shared Lang_Global_Satuan_Panjang As String
    Public Shared Lang_Global_EstimasiSerapan As String
    Public Shared Lang_Global_hargaJual As String
    Public Shared Lang_Global_HargaTerendah As String
    Public Shared Lang_Global_HargaTertinggi As String
    Public Shared Lang_Global_Jenis As String
    Public Shared Lang_Global_MarkUp As String
    Public Shared Lang_Global_Belum_ada_Penawaran As String
    Public Shared Lang_Global_QtySimulasi As String
    Public Shared Lang_Global_NamaEkspedisi As String
    Public Shared Lang_Global_Kecamatan_awal As String
    Public Shared Lang_Global_Kecamatan_tujuan As String
    Public Shared Lang_Global_Kelurahan_awal As String
    Public Shared Lang_Global_Kelurahan_tujuan As String
    Public Shared Lang_Global_Kota_awal As String
    Public Shared Lang_Global_Kota_tujuan As String
    Public Shared Lang_Global_Provinsi_awal As String
    Public Shared Lang_Global_Provinsi_tujuan As String
    Public Shared Lang_Global_Bruto As String
    Public Shared Lang_Global_TglBruto As String
    Public Shared Lang_Global_JamBruto As String
    Public Shared Lang_Global_FotoBruto As String
    Public Shared Lang_Global_Tara As String
    Public Shared Lang_Global_TglTara As String
    Public Shared Lang_Global_JamTara As String
    Public Shared Lang_Global_UserTara As String
    Public Shared Lang_Global_FotoTara As String
    Public Shared Lang_Global_TimbangKosong As String
    Public Shared Lang_Global_TimbangIsi As String
    Public Shared Lang_Global_Ekspedisi As String
    Public Shared Lang_Global_Supir As String
    Public Shared Lang_Global_PlatNomor As String
    Public Shared Lang_Global_WaktuTimbang As String
    Public Shared Lang_Global_FotoKendaraan As String
    Public Shared Lang_Global_KoneksiKamera As String
    Public Shared Lang_Global_TimbangMasuk As String
    Public Shared Lang_Global_TimbangKeluar As String
    Public Shared Lang_Global_Error_Validasi As String
    Public Shared Lang_Global_Pilih_Batal As String
    Public Shared Lang_Global_Tanya_Batal As String
    Public Shared Lang_Global_ListKendaraan As String
    Public Shared Lang_Global_Timbangan As String
    Public Shared Lang_Global_Tunai As String
    Public Shared Lang_Global_Non_Tunai As String
    Public Shared Lang_Global_Error_No_PO As String
    Public Shared Lang_Global_Error_Jatuh_Tempo As String
    Public Shared Lang_Global_JenisKategoriHarga As String
    Public Shared Lang_Global_KategoriKemasan As String
    Public Shared Lang_Global_KategoriBerat As String
    Public Shared Lang_Global_RangeHarga As String
    Public Shared Lang_Global_sampaidengan As String
    Public Shared Lang_Global_PersenMarkUp As String
    Public Shared Lang_Global_KategoriProduk As String
    Public Shared Lang_Global_Rangetidakbolehlebihrendah As String
    Public Shared Lang_Global_Error_Pernah_Simpan As String
    Public Shared Lang_Global_Pilih_Hapus As String
    Public Shared Lang_Global_Tanggal_PO As String
    Public Shared Lang_Global_NO_BM As String
    Public Shared Lang_Global_NO As String
    Public Shared Lang_Global_Tanggal_Produksi As String
    Public Shared Lang_Global_Tanggal_Expired As String
    Public Shared Lang_GLOBAL_No_Surat_Jalan As String
    Public Shared Lang_Global_Error_Lokasi As String
    Public Shared Lang_Global_Catatan As String
    Public Shared Lang_Global_No_Transaksi As String
    Public Shared Lang_Global_Penangung_Jawab As String
    Public Shared Lang_Global_Error_No_Transaksi As String
    Public Shared Lang_Global_Error_Ya As String
    Public Shared Lang_Global_Error_Tidak As String
    Public Shared Lang_GLOBAL_Volume As String
    Public Shared Lang_Global_Harga_Beli As String
    Public Shared Lang_Global_Barang_Sendiri As String
    Public Shared Lang_Global_Kategori As String
    Public Shared Lang_Global_Aktif As String
    Public Shared Lang_Global_Flag_PPN As String
    Public Shared Lang_Global_Jenis_Gudang As String
    Public Shared Lang_Global_Kode_SO As String
    Public Shared Lang_Global_Penentu_Harga As String
    Public Shared Lang_Global_Stok_Min As String
    Public Shared Lang_Global_Kategori_Kecil As String
    Public Shared Lang_Global_Kategori_Besar As String
    Public Shared Lang_Global_Berat_Bersih As String
    Public Shared Lang_Global_Berat_Kotor As String
    Public Shared Lang_Global_Satuan_Jumlah As String
    Public Shared Lang_Global_Jenis_Produk As String
    Public Shared Lang_Global_Bentuk As String
    Public Shared Lang_Global_Kode_Size As String
    Public Shared Lang_Global_Ketebalan As String
    Public Shared Lang_Global_BeratKemasan As String
    Public Shared Lang_Global_Trah As String
    Public Shared Lang_Global_JudulHewan As String
    Public Shared Lang_Global_JudulProduk As String
    Public Shared Lang_Global_JudulKemasan As String
    Public Shared Lang_Global_JudulPacking As String
    Public Shared Lang_global_Periode_Awal As String
    Public Shared Lang_global_Periode_Akhir As String
    Public Shared Lang_Global_P As String
    Public Shared Lang_Global_L As String
    Public Shared Lang_Global_T As String
    Public Shared Lang_Global_Jumlah_Sisa As String
    Public Shared Lang_Global_List_Inquiry_PO As String
    Public Shared Lang_Global_Kota As String
    Public Shared Lang_Global_No_Produksi As String
    Public Shared Lang_GLOBAL_Masuk_Tny_Val_kurang As String
    Public Shared Lang_Global_Data_Sdh_Val As String
    Public Shared Lang_GLOBAL_Masuk_Error_Minus_Stock As String
    Public Shared Lang_GLOBAL_Error_Brg_SN1 As String
    Public Shared Lang_GLOBAL_Jmlh_Stok_Krg As String
    Public Shared Lang_GLOBAL_SN_untuk_barang As String
    Public Shared Lang_GLOBAL_Tidak_Ditemukan As String
    Public Shared Lang_GLOBAL_Jumlah_Produksi As String
    Public Shared Lang_GLOBAL_Jumlah_PO As String
    Public Shared Lang_GLOBAL_Jumlah_Tambahan As String
    Public Shared Lang_GLOBAL_Jumlah_Stock As String
    Public Shared Lang_GLOBAL_Jumlah_Order As String
    Public Shared Lang_GLOBAL_Terjadi_Kesalahan As String
    Public Shared Lang_GLOBAL_Ulangi_Transaksi As String
    Public Shared Lang_GLOBAL_Kategori_Supplier As String
    Public Shared Lang_GLOBAL_Data_Kontainer As String
    Public Shared Lang_GLOBAL_Id_Rencana As String
    Public Shared Lang_Global_Grand As String
    Public Shared Lang_Global_totalIDR As String
    Public Shared Lang_Global_biaya As String
    Public Shared Lang_Global_ETD As String
    Public Shared Lang_Global_Ras As String
    Public Shared Lang_global_Jenislabel As String
    Public Shared Lang_Global_Jenisstiker As String
    Public Shared Lang_Global_tipeSeal As String
    Public Shared Lang_Global_Jenisfinishing As String
    Public Shared Lang_Global_KapasitasKemasan As String
    Public Shared Lang_Global_Val As String
    Public Shared Lang_GLOBAL_Tanya_Validasi As String
    Public Shared Lang_Global_Batal As String
    Public Shared Lang_Global_Bahan_Penolong As String
    Public Shared Lang_Global_Bahan_Pengiriman As String
    Public Shared Lang_Global_NilaiBiaya As String
    Public Shared Lang_Global_Tambah As String
    Public Shared Lang_Global_SudahAda As String
    Public Shared Lang_MasterBiaya_Judul As String
    Public Shared Lang_MasterKategoriGudang_Judul As String
    Public Shared Lang_Global_KategoriGudang As String
    Public Shared Lang_Quality_Control_Kendaraan_Judul As String
    Public Shared Lang_GLOBAL_QC_Kode_Uji As String
    Public Shared Lang_GLOBAL_QC_Keterangan As String
    Public Shared Lang_GLOBAL_QC_Satuan As String
    Public Shared Lang_GLOBAL_Tidak_Ada_Data As String
    Public Shared Lang_GLOBAL_Pilih_Dahulu_Jenis_Produk As String
    Public Shared Lang_GLOBAL_Data_Sdh_Ditambahkan As String
    Public Shared Lang_GLOBAL_Jenis_Input_Tdk_Ditemukan As String
    Public Shared Lang_Global_KlasifikasiBahan As String
    Public Shared Lang_Global_KategoriPO As String
    Public Shared Lang_GLOBAL_Januari As String
    Public Shared Lang_Global_Februari As String
    Public Shared Lang_Global_Maret As String
    Public Shared Lang_Global_April As String
    Public Shared Lang_Global_Mei As String
    Public Shared Lang_Global_Juni As String
    Public Shared Lang_Global_Juli As String
    Public Shared Lang_Global_Agustus As String
    Public Shared Lang_Global_September As String
    Public Shared Lang_Global_Oktober As String
    Public Shared Lang_Global_November As String
    Public Shared Lang_Global_Desember As String
    Public Shared Lang_Global_Bulan As String
    Public Shared Lang_Global_Tahun As String
    Public Shared Lang_Global_PilihSeluruh As String
    Public Shared Lang_Global_TambahBarang As String
    Public Shared Lang_Global_Metode_Truckscale As String
    Public Shared Lang_Global_Kode_Bahan As String
    Public Shared Lang_Global_Nama_Bahan As String
    Public Shared Lang_Global_Qty_Barang As String
    Public Shared Lang_Global_Qty_Bahan As String
    Public Shared Lang_Global_Level As String
    Public Shared Lang_Global_Data_Barang As String
    Public Shared Lang_Global_Data_Bahan As String
    Public Shared Lang_Global_ValidQty As String
    Public Shared Lang_Global_ValidKbBhn As String
    Public Shared Lang_Global_Metode_Unloading As String

    'Display_Production_Order
    Public Shared Lang_Display_Production_Order_Judul As String
    Public Shared Lang_Display_Production_Order_Error_Pilih As String
    Public Shared Lang_Display_Production_Order_Judul2 As String
    Public Shared Lang_Display_Production_Order_Qty_Produksi As String
    Public Shared Lang_Display_Production_Order_Nilai_Produksi As String
    Public Shared Lang_Display_Production_Order_Hasil_Produksi As String
    Public Shared Lang_Display_Production_Order_Qty_Produksi2 As String
    Public Shared Lang_Display_Production_Order_Error_Qty1 As String
    Public Shared Lang_Display_Production_Order_Error_Qty2 As String
    Public Shared Lang_Display_Production_Order_Error_Qty3 As String
    Public Shared Lang_Display_Production_Order_Error_Qty4 As String

    'Jenis Muatan
    Public Shared Lang_Master_Jenis_Muatan_Judul As String

    'Master Packing
    Public Shared Lang_Master_Packing_Judul As String

    '
    Public Shared Lang_Transaksi_Sales_Forecasting As String

    'display penawaran
    Public Shared Lang_Display_Penawaran_Provinsi_Awal As String
    Public Shared Lang_Display_Penawaran_Provinsi_Tujuan As String
    Public Shared Lang_Display_Penawaran_Kota_Asal As String
    Public Shared Lang_Display_Penawaran_Kota_Tujuan As String
    Public Shared Lang_Display_Penawaran_Kecamatan_Asal As String
    Public Shared Lang_Display_Penawaran_Kecamatan_Tujuan As String
    Public Shared Lang_Display_Penawaran_Kelurahan_Asal As String
    Public Shared Lang_Display_Penawaran_Kelurahan_Tujuan As String
    Public Shared Lang_Display_Penawaran_Judul As String
    Public Shared Lang_Display_Penawaran_Media_Kirim As String

    'master jenis hewan
    Public Shared Lang_Jenis_Hewan_Judul As String
    Public Shared Lang_Jenis_Hewan_Kode As String
    Public Shared Lang_Jenis_Hewan_Keterangan As String
    Public Shared Lang_Jenis_Hewan_Kolom As String
    Public Shared Lang_Jenis_Hewan_Error_Kode As String
    Public Shared Lang_Jenis_Hewan_Error_Nama As String
    Public Shared Lang_Jenis_Hewan_Prefix As String
    Public Shared Lang_Jenis_Hewan_Error_Prefix As String
    'master kategori umur
    Public Shared Lang_Kategori_Umur_Judul As String
    Public Shared Lang_Kategori_Umur_Jenis As String
    Public Shared Lang_Kategori_Umur_Kode As String
    Public Shared Lang_Kategori_Umur_Keterangan As String
    Public Shared Lang_Kategori_Umur_Kolom As String
    Public Shared Lang_Kategori_Umur_Error_Jenis As String
    Public Shared Lang_Kategori_Umur_Error_Kode As String
    Public Shared Lang_Kategori_Umur_Error_Keterangan As String

    'master jenis produk
    Public Shared Lang_Jenis_Produk_Judul As String
    Public Shared Lang_Jenis_Produk_Kode As String
    Public Shared Lang_Jenis_Produk_Keterangan As String
    Public Shared Lang_Jenis_Produk_Protein As String
    Public Shared Lang_Jenis_Produk_Lemak As String
    Public Shared Lang_Jenis_Produk_Min As String
    Public Shared Lang_Jenis_Produk_Max As String
    Public Shared Lang_Jenis_Produk_Catatan As String
    Public Shared Lang_Jenis_Produk_Kolom As String
    Public Shared Lang_Jenis_Produk_Error_Kode As String
    Public Shared Lang_Jenis_Produk_Error_Keterangan As String
    Public Shared Lang_Jenis_Produk_Error_Protein_Min As String
    Public Shared Lang_Jenis_Produk_Error_Protein_Max As String
    Public Shared Lang_Jenis_Produk_Error_Lemak_Min As String
    Public Shared Lang_Jenis_Produk_Error_Lemak_Max As String
    Public Shared Lang_Jenis_Produk_Error_Catatan As String

    'master jenis kemasan utama
    Public Shared Lang_Jenis_Kemasan_Utama_Judul As String
    Public Shared Lang_Jenis_Kemasan_Utama_Jenis_Produk As String
    Public Shared Lang_Jenis_Kemasan_Utama_Kode As String
    Public Shared Lang_Jenis_Kemasan_Utama_Keterangan As String
    Public Shared Lang_Jenis_Kemasan_Utama_Kolom As String
    Public Shared Lang_Jenis_Kemasan_Utama_Error_Jenis_Produk As String
    Public Shared Lang_Jenis_Kemasan_Utama_Error_Kode As String
    Public Shared Lang_Jenis_Kemasan_Utama_Error_Keterangan As String

    Public Shared Lang_Jenis_Kemasan_Utama_Prefix As String
    Public Shared Lang_Jenis_Kemasan_Utama_Error_Prefix As String
    'mastrer bentuk
    Public Shared Lang_Bentuk_Judul As String
    Public Shared Lang_Bentuk_Jenis_Produk As String
    Public Shared Lang_Bentuk_Kode As String
    Public Shared Lang_Bentuk_Keterangan As String
    Public Shared Lang_Bentuk_Kolom As String
    Public Shared Lang_Bentuk_Error_Jenis_Produk As String
    Public Shared Lang_Bentuk_Error_Kode As String
    Public Shared Lang_Bentuk_Error_Keterangan As String

    'master warna
    Public Shared Lang_Warna_Judul As String
    Public Shared Lang_Warna_Jenis_Produk As String
    Public Shared Lang_Warna_Kode As String
    Public Shared Lang_Warna_Keterangan As String
    Public Shared Lang_Warna_Kolom As String
    Public Shared Lang_Warna_Error_Jenis_Produk As String
    Public Shared Lang_Warna_Error_Kode As String
    Public Shared Lang_Warna_Error_Keterangan As String

    'mastrer Varian
    Public Shared Lang_Varian_Judul As String
    Public Shared Lang_Varian_Jenis_Produk As String
    Public Shared Lang_Varian_Kode As String
    Public Shared Lang_Varian_Keterangan As String
    Public Shared Lang_Varian_Kolom As String
    Public Shared Lang_Varian_Error_Jenis_Produk As String
    Public Shared Lang_Varian_Error_Kode As String
    Public Shared Lang_Varian_Error_Keterangan As String
    Public Shared Lang_Master_Varian_Jumlah_Max As String
    Public Shared Lang_Varian_Jumlah_Max As String

    'mastrer Label Kemasan
    Public Shared Lang_Label_Kemasan_Judul As String
    Public Shared Lang_Label_Kemasan_Jenis_Produk As String
    Public Shared Lang_Label_Kemasan_Kode As String
    Public Shared Lang_Label_Kemasan_Keterangan As String
    Public Shared Lang_Label_Kemasan_Kolom As String
    Public Shared Lang_Label_Kemasan_Error_Jenis_Produk As String
    Public Shared Lang_Label_Kemasan_Error_Kode As String
    Public Shared Lang_Label_Kemasan_Error_Keterangan As String
    Public Shared Lang_Label_Kemasan_Jenis_Utama As String

    'master stiker kemasan utama
    Public Shared Lang_Stiker_Kemasan_Utama_Judul As String
    Public Shared Lang_Stiker_Kemasan_Utama_Jenis_Produk As String
    Public Shared Lang_Stiker_Kemasan_Utama_Kode As String
    Public Shared Lang_Stiker_Kemasan_Utama_Keterangan As String
    Public Shared Lang_Stiker_Kemasan_Utama_Kolom As String
    Public Shared Lang_Stiker_Kemasan_Utama_Error_Jenis_Produk As String
    Public Shared Lang_Stiker_Kemasan_Utama_Error_Kode As String
    Public Shared Lang_Stiker_Kemasan_Utama_Error_Keterangan As String
    Public Shared Lang_Stiker_Kemasan_Utama_Jenis_Utama As String
    Public Shared Lang_Stiker_Kemasan_Utama_Stiker_Utama As String

    'master kapasitas kemasan utama
    Public Shared Lang_Kapasitas_Kemasan_Error_Jenis_Produk As String
    Public Shared Lang_Kapasitas_Kemasan_Error_Keterangan As String
    Public Shared Lang_Kapasitas_Kemasan_Error_Kode As String
    Public Shared Lang_Kapasitas_Kemasan_Jenis_Produk As String
    Public Shared Lang_Kapasitas_Kemasan_Jenis_Utama As String
    Public Shared Lang_Kapasitas_Kemasan_Judul As String
    Public Shared Lang_Kapasitas_Kemasan_Keterangan As String
    Public Shared Lang_Kapasitas_Kemasan_Kode As String
    Public Shared Lang_Kapasitas_Kemasan_Kolom As String
    Public Shared Lang_Kapasitas_Kemasan_Error_SatuanTurunan As String

    'master customer
    Public Shared Lang_Customer_Judul As String
    Public Shared Lang_Customer_Customer As String
    Public Shared Lang_Customer_Data_Pic As String
    Public Shared Lang_Customer_Nama_Pic As String
    Public Shared Lang_Customer_Jabatan As String
    Public Shared Lang_Customer_Divisi As String
    Public Shared Lang_Customer_Hp_Pic As String
    Public Shared Lang_Customer_Tipe_Member As String
    Public Shared Lang_Customer_Tipe_Pembayaran As String
    Public Shared Lang_Customer_Cara_Bayar As String
    Public Shared Lang_Customer_Ketentuan_Harga As String
    Public Shared Lang_Customer_Data_Bisnis As String
    Public Shared Lang_Customer_Jenis_Usaha As String
    Public Shared Lang_Customer_Nama_Usaha As String
    Public Shared Lang_Customer_Alamat As String
    Public Shared Lang_Customer_Tlp As String
    Public Shared Lang_Customer_Email As String
    Public Shared Lang_Customer_Status_Pajak As String
    Public Shared Lang_Customer_Identitas_WP As String
    Public Shared Lang_Customer_Nama_WP As String
    Public Shared Lang_Customer_Alamat_WP As String
    Public Shared Lang_Customer_Nama_Bank As String
    Public Shared Lang_Customer_Cabang As String
    Public Shared Lang_Customer_No_Rek As String
    Public Shared Lang_Customer_Nama_Nasabah As String
    Public Shared Lang_Customer_Tipe_Id As String
    Public Shared Lang_Customer_No_Id As String
    Public Shared Lang_Customer_Nama_Leng As String
    Public Shared Lang_Customer_Jenis_Kelamin As String
    Public Shared Lang_Customer_Data_Bank As String
    Public Shared Lang_Customer_Data_Pemilik As String
    Public Shared Lang_Customer_Tambah As String
    Public Shared Lang_Customer_Error_Kd_Customer As String
    Public Shared Lang_Customer_Judul_Display As String
    Public Shared Lang_Customer_Kolom As String
    Public Shared Lang_Customer_Kd_Customer As String
    Public Shared Lang_Customer_Error_Nama_PIC As String
    Public Shared Lang_Customer_Error_Jabatan As String
    Public Shared Lang_Customer_Error_Divisi As String
    Public Shared Lang_Customer_Error_HP_PIC As String
    Public Shared Lang_Customer_Error_Tp_Member As String
    Public Shared Lang_Customer_Error_Tp_Pembayaran As String
    Public Shared Lang_Customer_Error_Cara_Bayar As String
    Public Shared Lang_Customer_Error_Ketentuan As String
    Public Shared Lang_Customer_Nomor_WP As String
    Public Shared Lang_Customer_Error_Jenis_Usaha As String
    Public Shared Lang_Customer_Error_Nama_Usaha As String
    Public Shared Lang_Customer_Error_Alamat As String
    Public Shared Lang_Customer_Error_Tlp As String
    Public Shared Lang_Customer_Error_Email As String
    Public Shared Lang_Customer_Error_Status_Pajak As String
    Public Shared Lang_Customer_Error_Identitas_WP As String
    Public Shared Lang_Customer_Error_Nama_WP As String
    Public Shared Lang_Customer_Error_Alamat_WP As String
    Public Shared Lang_Customer_Error_Nomor_WP As String
    Public Shared Lang_Customer_Error_Tipe_Id As String
    Public Shared Lang_Customer_Error_No_Id As String
    Public Shared Lang_Customer_Error_Jenis_Kelamin As String
    Public Shared Lang_Customer_Error_Hp As String
    Public Shared Lang_Customer_Cara_Kirim As String
    Public Shared Lang_Customer_Media_Kirim As String
    Public Shared Lang_Customer_Penerima As String
    Public Shared Lang_Customer_Alamat_Penerima As String
    Public Shared Lang_Customer_Kontak_Penerima As String
    Public Shared Lang_Customer_Provinsi As String
    Public Shared Lang_Customer_Kabupaten_Kota As String
    Public Shared Lang_Customer_Kecamatan As String
    Public Shared Lang_Customer_Kelurahan As String
    Public Shared Lang_Customer_Prefernsi_Ekspedisi As String
    Public Shared Lang_Customer_Alamat_Ekspedisi As String
    Public Shared Lang_Customer_Telepon_Ekspedisi As String
    Public Shared Lang_Customer_Data_Kirim As String
    Public Shared Lang_Customer_Error_Cara_Kirim As String
    Public Shared Lang_Customer_Error_Media_Kirim As String
    Public Shared Lang_Customer_Error_Penerima As String
    Public Shared Lang_Customer_Error_Alamat_Penerima As String
    Public Shared Lang_Customer_Error_Kontak_Penerima As String
    Public Shared Lang_Customer_Error_Provinsi As String
    Public Shared Lang_Customer_Error_Kabupaten_Kota As String
    Public Shared Lang_Customer_Error_Kecamatan As String
    Public Shared Lang_Customer_Error_Kelurahan As String
    Public Shared Lang_Customer_Error_Prefernsi_Ekspedisi As String
    Public Shared Lang_Customer_Error_Alamat_Ekspedisi As String
    Public Shared Lang_Customer_Error_Telepon_Ekspedisi As String

    'Display Inquiry
    Public Shared Lang_Display_Inquiry_No_Faktur As String
    Public Shared Lang_Display_Inquiry_Kd_Customer As String
    Public Shared Lang_Display_Inquiry_Nama_Usaha As String
    Public Shared Lang_Display_Inquiry_Lokasi As String
    Public Shared Lang_Display_Inquiry_Tanggal As String
    Public Shared Lang_Display_Inquiry_Hari_ini As String
    Public Shared Lang_Display_Inquiry_Para_Tbl As String
    Public Shared Lang_Display_Inquiry_Para_lain As String
    Public Shared Lang_Display_Inquiry_Jns_Hewan As String
    Public Shared Lang_Display_Inquiry_Jns_Umur As String
    Public Shared Lang_Display_Inquiry_Jns_Produk As String
    Public Shared Lang_Display_Inquiry_Jns_Varian As String
    Public Shared Lang_Display_Inquiry_Jns_Komposisi As String
    Public Shared Lang_Display_Inquiry_Protein As String
    Public Shared Lang_Display_Inquiry_Lemak As String
    Public Shared Lang_Display_Inquiry_Kelembapan As String
    Public Shared Lang_Display_Inquiry_Catatan As String
    Public Shared Lang_Display_Inquiry_Kd_barang As String
    Public Shared Lang_Display_Inquiry_Nm_Barang As String
    Public Shared lang_display_inquiry_Judul As String
    Public Shared Lang_Display_Inquiry_Error_Batal As String
    Public Shared Lang_Display_Inquiry_Error_Tolak_Batal As String
    Public Shared Lang_Display_Inquiry_Error_Tolak_Batal2 As String

    'master Karywan
    Public Shared Lang_Karywan_Judul As String
    Public Shared Lang_Karywan_Divisi As String
    Public Shared Lang_Karywan_Kode As String
    Public Shared Lang_Karywan_Nama As String
    Public Shared Lang_Karywan_Jabatan As String
    Public Shared Lang_Karywan_Kolom As String
    Public Shared Lang_Karywan_Error_Divisi As String
    Public Shared Lang_Karywan_Error_Kode As String
    Public Shared Lang_Karywan_Error_Nama As String
    Public Shared Lang_Karywan_Error_Jabatan As String

    'master Transaksi Formula
    Public Shared Lang_TransFormula_Judul As String
    Public Shared Lang_TransFormula_Customer As String
    Public Shared Lang_TransFormula_KdBarang As String
    Public Shared Lang_TransFormula_NmBarang As String
    Public Shared Lang_TransFormula_NoFaktur As String
    Public Shared Lang_TransFormula_NoInquiry As String
    Public Shared Lang_TransFormula_PenanggungJawab As String
    Public Shared Lang_TransFormula_Sample As String
    Public Shared Lang_TransFormula_Tanggal As String
    Public Shared Lang_TransFormula_DgvStepFormula_No As String
    Public Shared Lang_TransFormula_DgvStepFormula_Tipe As String
    Public Shared Lang_TransFormula_DgvStepFormula_KdBarang As String
    Public Shared Lang_TransFormula_DgvStepFormula_Nama As String
    Public Shared Lang_TransFormula_DgvStepFormula_Qty As String
    Public Shared Lang_TransFormula_DgvStepFormula_Satuan As String
    Public Shared Lang_TransFormula_DgvStepFormula_Persentase As String
    Public Shared Lang_TransFormula_DgvStepFormula_Keterangan As String

    'Display Tampil Inquiry
    Public Shared Lang_TampilInquiry_Judul As String
    Public Shared Lang_TampilInquiry_Customer As String
    Public Shared Lang_TampilInquiry_Lokasi As String
    Public Shared Lang_TampilInquiry_DgvDataInquiry_NoInquiry As String
    Public Shared Lang_TampilInquiry_DgvDataInquiry_Lokasi As String
    Public Shared Lang_TampilInquiry_DgvDataInquiry_Tanggal As String
    Public Shared Lang_TampilInquiry_DgvDataInquiry_KdCustomer As String
    Public Shared Lang_TampilInquiry_DgvDataInquiry_NamaCustomer As String

    'Display Tampil Inquiry Barang
    Public Shared Lang_TampilInquiryBarang_Judul As String

    'Display Tampil Barang
    Public Shared Lang_TampilBarang_Judul As String


    ' Transaksi Quality Control Formula
    Public Shared Lang_QC_Formula_Judul As String
    Public Shared Lang_QC_Formula_Tanggal As String
    Public Shared Lang_QC_Formula_No_Transaksi As String
    Public Shared Lang_QC_Formula_Penanggung_Jawab As String
    Public Shared Lang_QC_Formula_Kode_Formula As String
    Public Shared Lang_QC_Formula_Nama_Barang As String
    Public Shared Lang_QC_Formula_Note1 As String
    Public Shared Lang_QC_Formula_Note2 As String
    Public Shared Lang_QC_Formula_Catatan As String
    Public Shared Lang_QC_Formula_Hasil_Uji As String
    Public Shared Lang_QC_Formula_kode_uji_required As String
    Public Shared Lang_QC_Formula_Penanggung_jawab_req As String
    Public Shared Lang_QC_Formula_No_Inquiry_required As String
    Public Shared Lang_QC_Formula_hasil_qc_required As String
    Public Shared Lang_QC_Formula_Standar_Kontrol_req As String
    Public Shared Lang_QC_Formula_Tanggal_Uji_required As String
    Public Shared Lang_QC_Formula_Waktu_Uji_required As String
    Public Shared Lang_QC_Formula_Qc_Hapus As String


    Public Shared Lang_QC_Formula_Kode_Uji_DGV As String
    Public Shared Lang_QC_Formula_Deskripsi_DGV As String
    Public Shared Lang_QC_Formula_Satuan_DGV As String
    Public Shared Lang_QC_Formula_Target_DGV As String
    Public Shared Lang_QC_Formula_Hasil_DGV As String
    Public Shared Lang_QC_Formula_Standar_Kontrol_DGV As String
    Public Shared Lang_QC_Formula_Pass_DGV As String
    Public Shared Lang_QC_Formula_Tanggal_Uji_DGV As String
    Public Shared Lang_QC_Formula_Waktu_Uji_DGV As String
    Public Shared Lang_QC_Formula_Judul_QC As String
    Public Shared Lang_QC_Formula_SD_Jenis As String
    Public Shared Lang_QC_Formula_SD_value As String

    Public Shared Lang_QC_Formula_Qc_required As String
    Public Shared Lang_QC_Formula_required As String

    'Transaksi Binding Formula
    Public Shared Lang_TransFormulaBinding_Judul As String
    Public Shared Lang_TransFormulaBinding_DGV_KodeProduk As String
    Public Shared Lang_TransFormulaBinding_DGV_NamaProduk As String
    Public Shared Lang_TransFormulaBinding_DGV_BindingFormula As String

    'Display Transaksi Formula
    Public Shared Lang_Display_Transaksi_Formula As String
    Public Shared Lang_Display_Transaksi_Formula_No_Faktur As String
    Public Shared Lang_Display_Transaksi_Formula_No_Inquiry As String
    Public Shared Lang_Display_Transaksi_Formula_Kd_Cusotmer As String
    Public Shared Lang_Display_Transaksi_Formula_Nama_Usaha As String
    Public Shared Lang_Display_Transaksi_Formula_Lokasi As String
    Public Shared Lang_Display_Transaksi_Formula_Kd_barang As String
    Public Shared Lang_Display_Transaksi_Formula_Nama_barang As String
    Public Shared Lang_Display_Transaksi_Formula_Tgl As String
    Public Shared Lang_Display_Transaksi_Formula_Penanggung As String
    Public Shared Lang_Display_Transaksi_Formula_Detail_Step As String
    Public Shared Lang_Display_Transaksi_Formula_Komposisi As String
    Public Shared Lang_Display_Transaksi_Formula_No_Step As String
    Public Shared Lang_Display_Transaksi_Formula_Tipe As String
    Public Shared Lang_Display_Transaksi_Formula_Kode As String
    Public Shared Lang_Display_Transaksi_Formula_Deskripsi As String
    Public Shared Lang_Display_Transaksi_Formula_Jumlah As String
    Public Shared Lang_Display_Transaksi_Formula_Satuan As String
    Public Shared Lang_Display_Transaksi_Formula_Presentase As String
    Public Shared Lang_Display_Transaksi_Formula_Error_Tolak_Batal As String
    Public Shared Lang_Display_Transaksi_Formula_Error_Batal As String
    Public Shared Lang_Display_Transaksi_Formula_Error_Tolak_Batal2 As String

    'Master Penawaran
    Public Shared Lang_Penawaran_Judul As String
    Public Shared Lang_Penawaran_NoPenawaran As String
    Public Shared Lang_Penawaran_TglPenawaranHrg As String
    Public Shared Lang_Penawaran_PeriodeAkhir As String

    'Master Ongkir
    Public Shared Lang_Ongkir_Judul As String
    Public Shared Lang_Ongkir_ProvAsal As String
    Public Shared Lang_Ongkir_ProvTujuan As String
    Public Shared Lang_Ongkir_KabKotaAsal As String
    Public Shared Lang_Ongkir_KabKotaTujuan As String
    Public Shared Lang_Ongkir_Ukuran As String
    Public Shared Lang_Ongkir_Hrg As String
    Public Shared Lang_Ongkir_NmEkspedisi As String
    Public Shared Lang_Ongkir_MediaKirim As String

    'Transaksi_Validasi_Harga_Jual
    Public Shared Lang_Transaksi_Validasi_Harga_Jual_Judul As String
    Public Shared Lang_Transaksi_Validasi_Harga_Jual_No_trasaksi As String
    Public Shared Lang_Transaksi_Validasi_Harga_Jual_Tanggal As String
    Public Shared Lang_Transaksi_Validasi_Harga_Jual_No_inquiry As String
    Public Shared Lang_Transaksi_Validasi_Harga_Jual_Customer As String
    Public Shared Lang_Transaksi_Validasi_Harga_Jual_Pilih_Brg As String
    Public Shared Lang_Transaksi_Validasi_Harga_Jual_Kode_Brg As String
    Public Shared Lang_Transaksi_Validasi_Harga_Jual_Nama_Brg As String
    Public Shared Lang_Transaksi_Validasi_Harga_Jual_Gudang As String
    Public Shared Lang_Transaksi_Validasi_Harga_Jual_Harga_Satuan As String
    Public Shared Lang_Transaksi_Validasi_Harga_Jual_Est_Serapan As String
    Public Shared Lang_Transaksi_Validasi_Harga_Jual_Mark_Up As String
    Public Shared Lang_Transaksi_Validasi_Harga_Jual_Harga_Jual As String
    Public Shared Lang_Transaksi_Validasi_Harga_Jual_Total_Jual As String
    Public Shared Lang_Transaksi_Validasi_Harga_Jual_Mark_Up_Fixed As String
    Public Shared Lang_Transaksi_Validasi_Harga_Jual_Hrg_Jual_Fixed As String
    Public Shared Lang_Transaksi_Validasi_Harga_Jual_Ttl_Jual_Fixed As String
    Public Shared Lang_Transaksi_Validasi_Harga_Jual_Gdg_Asal As String
    Public Shared Lang_Transaksi_Validasi_Harga_Jual_Gdg_Tujuan As String
    Public Shared Lang_Transaksi_Validasi_Harga_Jual_Err_Mark_Up As String
    Public Shared Lang_Transaksi_Validasi_Harga_Jual_Hrg_Jual As String
    Public Shared Lang_Transaksi_Validasi_Harga_Jual_Ttl_Jual As String
    Public Shared Lang_Transaksi_Validasi_Harga_Jual_Err_Selisih As String

    'lokasi_PO
    Public Shared Lang_Lokasi_PO_Judul As String
    Public Shared Lang_Lokasi_PO_Cari_PO As String
    Public Shared Lang_Lokasi_PO_Gudang_Tujuan As String
    Public Shared Lang_Lokasi_PO_Judul_Display As String
    Public Shared Lang_Lokasi_PO_Kolom As String
    Public Shared Lang_Lokasi_PO_Error_Banding_Jmlh As String
    Public Shared Lang_Lokasi_PO_Error_Kode_Brg_Lv As String
    Public Shared Lang_Lokasi_PO_Error_Banding_Jmlh2 As String

    'Master Media Kirim 
    Public Shared Lang_MediaKirim_Judul As String
    Public Shared Lang_MediaKirim_KodeMediaKirim As String

    'Master Ekspedisi
    Public Shared Lang_Ekspedisi_Judul As String
    Public Shared Lang_Ekspedisi_KodeEkspedisi As String
    Public Shared Lang_Ekspedisi_NamaEkspedisi As String
    Public Shared Lang_Ekspedisi_AlamatEkspedisi As String
    Public Shared Lang_Ekspedisi_TeleponEkspedisi As String
    Public Shared Lang_Ekspedisi_PenanggungJawab As String
    Public Shared Lang_Ekspedisi_Pembayaran As String
    Public Shared Lang_Ekspedisi_GolonganPPH As String
    Public Shared Lang_Ekspedisi_NilaiPPH As String

    'Display Transaksi PO Pembelian
    Public Shared Lang_Display_Transaksi_PO_Pembelian_Judul As String

    'Display Transaksi Pembelian
    Public Shared Lang_Display_Transaksi_Pembelian As String

    'Display Transaksi PO Desktop
    Public Shared Lang_Display_Transaksi_PO_Desktop_Judul As String

    'Transaksi Prepare Bahan Baku
    Public Shared Lang_Prepare_Bahan_Label_Jumlah_Stok As String
    Public Shared Lang_Prepare_Bahan_Label_Jumlah_Butuh As String
    Public Shared Lang_Prepare_Bahan_Label_Harus_Order As String
    Public Shared Lang_Prepare_Bahan_Label_NoPenawaran As String
    Public Shared Lang_Prepare_Bahan_Error_StokNegatif1 As String
    Public Shared Lang_Prepare_Bahan_Error_StokNegatif2 As String
    Public Shared Lang_Prepare_Bahan_BrgTdkDitemukan As String
    Public Shared Lang_Prepare_Bahan_Judul As String

    'Transaksi Barang Masuk
    Public Shared Lang_Pmb_Barang_Masuk_Judul As String
    Public Shared Lang_Pmb_Barang_Masuk_Tanggal_Bongkar As String
    Public Shared Lang_Pmb_Barang_masuk_No_Plat As String
    Public Shared Lang_Pmb_Barang_Masuk_SD_Judul As String
    Public Shared Lang_Pmb_Barang_Masuk_Tanggal_Expire As String

    Public Shared Lang_Pmb_Barang_Masuk_Tanggal_produksi As String
    Public Shared Lang_Pmb_Barang_Masuk_SD_Judul_Pili_PO As String
    Public Shared Lang_Pmb_Barang_Masuk_Error_Faktur As String

    Public Shared Lang_Pmb_Barang_Masuk_Error_Nota As String
    Public Shared Lang_Pmb_Barang_Masuk_Error_Plat As String
    Public Shared Lang_Pmb_Barang_Masuk_Error_PO As String

    Public Shared Lang_Display_Barang_Masuk_Judul_Display As String

    'Simulasi HPP
    Public Shared Lang_SimulasiHPP_Judul As String
    Public Shared Lang_DetailSimulasiHPP_Judul_BB As String
    Public Shared Lang_DetailSimulasiHPP_Judul_BP As String
    Public Shared Lang_DetailSimulasiHPP_Judul_BK As String
    Public Shared Lang_DetailSimulasiHPP_Judul_BLL As String

    Public Shared Lang_DetailSimulasiHPP_Validasi1 As String
    Public Shared Lang_DetailSimulasiHPP_Validasi2 As String
    Public Shared Lang_DetailSimulasiHPP_Validasi3 As String

    'Validasi Barang Masuk
    Public Shared Lang_Validasi_Barang_Masuk_Judul As String
    Public Shared Lang_Validasi_Barang_Masuk_Nomor_Plat As String
    Public Shared Lang_Validasi_Barang_Masuk_Tgl_bongkar As String
    Public Shared Lang_Validasi_Barang_Masuk_Tgl_Produksi As String
    Public Shared Lang_Validasi_Barang_Masuk_Tgl_Expired As String
    Public Shared Lang_Validasi_Barang_Masuk_Tny_Val As String
    Public Shared Lang_Validasi_Barang_Masuk_Error1 As String
    Public Shared Lang_Validasi_Barang_Masuk_Error2 As String
    Public Shared Lang_Validasi_Barang_Masuk_Error3 As String
    Public Shared Lang_Validasi_Barang_Masuk_Error4 As String
    Public Shared Lang_Validasi_Barang_Masuk_Error5 As String
    Public Shared Lang_Validasi_Barang_Masuk_Error6 As String
    Public Shared Lang_Validasi_Barang_Masuk_Error7 As String
    Public Shared Lang_Validasi_Barang_Masuk_Error8 As String
    Public Shared Lang_Validasi_Barang_Masuk_Error9 As String
    Public Shared Lang_Validasi_Barang_Masuk_Tdk_Ditemu As String
    Public Shared Lang_Validasi_Barang_Masuk_Data_Tdk_Ditemu As String
    Public Shared Lang_Validasi_Barang_Masuk_Berbeda As String
    Public Shared Lang_Validasi_Barang_Masuk_Jadi_Salah As String

    'TIMBANGAN
    Public Shared Lang_TransUnloading_Judul As String
    Public Shared Lang_TransUnloadingPO_Judul As String
    Public Shared Lang_Global_NoTimbangan As String

    'Pembelian
    Public Shared Lang_Pembelian_Judul As String
    Public Shared Lang_Pembelian_No_SO As String
    Public Shared Lang_Pembelian_Cari_PO As String
    Public Shared Lang_Pembelian_Kd_Bahan As String
    Public Shared Lang_Pembelian_Nm_Bahan As String
    Public Shared Lang_Pembelian_No_Seri As String
    Public Shared Lang_Pembelian_Harga As String
    Public Shared Lang_Pembelian_Jumlah As String
    Public Shared Lang_Pembelian_Satuan As String
    Public Shared Lang_Pembelian_Total As String
    Public Shared Lang_Pembelian_Sisa As String
    Public Shared Lang_Pembelian_Total_Harga As String

    'QC BAHAN
    Public Shared Lang_QC_Bahan_Judul_Form2 As String
    Public Shared Lang_QC_Bahan_Judul_Form As String
    Public Shared Lang_QC_Bahan_Nm_Ekspedisi As String
    Public Shared Lang_QC_Bahan_Err_No_SJ As String
    Public Shared Lang_QC_Bahan_Err_Penaggung As String
    Public Shared Lang_QC_Bahan_Err_QC_Bahan As String
    Public Shared Lang_QC_Bahan_Err_Save_QC As String
    Public Shared Lang_QC_Bahan_Judul3 As String
    Public Shared Lang_QC_Bahan_Err_Hasil_Qc As String


    ' selisih barang masuk
    Public Shared Lang_Selisih_BM_Judul As String
    Public Shared Lang_Selisih_BM_Jenis_Selisih As String
    Public Shared Lang_Selisih_BM_Total_Selisih_QTY As String
    Public Shared Lang_Selisih_BM_Total_Selisih_Rp As String
    Public Shared Lang_Selisih_BM_Jumlah_PL As String
    Public Shared Lang_Selisih_BM_Jumlah_BM As String
    Public Shared Lang_Selisih_BM_Selisih As String
    Public Shared Lang_Selisih_BM_Penyelesaian_Plus As String
    Public Shared Lang_Selisih_BM_Penyelesaian_Min As String
    Public Shared Lang_Selisih_BM_Selisih_Fix As String
    Public Shared Lang_Selisih_BM_Selisih_Rp As String
    Public Shared Lang_Selisih_BM_Harga_Per_PCS As String
    Public Shared Lang_Selisih_BM_Err_Jenis As String
    Public Shared Lang_Selisih_BM_Err_KdSupp As String
    Public Shared Lang_Selisih_BM_Err_No_PO As String
    Public Shared Lang_Selisih_BM_Err_Brng_Msk As String
    Public Shared Lang_Selisih_BM_Err_Data_Kosong As String
    Public Shared Lang_Selisih_BM_Err_only_one_cell As String

    'Master Jenis Kategori Harga
    Public Shared Lang_JenisKategoriHarga_Judul As String
    Public Shared Lang_JenisKategoriHarga_Kode As String
    Public Shared Lang_JenisKategoriHarga_Nama As String

    'Master Jenis Member Perbarang
    Public Shared Lang_JenisMemberPerbarang_Judul As String
    Public Shared Lang_JenisMemberPerbarang_Persen As String
    'master gudang

    Public Shared Lang_SetGudang_JenisGudang As String
    Public Shared Lang_SetGudang_KodeArea As String
    Public Shared Lang_SetGudang_KodeKolom As String
    Public Shared Lang_SetGudang_KodeBaris As String
    Public Shared Lang_SetGudang_KodeLevel As String
    Public Shared Lang_SetGudang_KodeUkuran As String
    Public Shared Lang_SetGudang_LevelUkuran As String
    Public Shared Lang_SetGudang_KodePosisi As String
    Public Shared Lang_SetGudang_UkuranPalet As String
    Public Shared Lang_SetGudang_KodePalet As String
    Public Shared Lang_SetGudang_Judul As String
    Public Shared Lang_SetGudang_Susunan As String
    Public Shared Lang_SetGudang_Palet As String
    Public Shared Lang_SetGudang_SetGudang As String
    Public Shared Lang_SetGudang_Position As String
    Public Shared Lang_SetGudang_LevelSize As String
    Public Shared Lang_SetGudang_Level As String
    Public Shared Lang_SetGudang_Baris As String
    Public Shared Lang_SetGudang_Kolom As String
    Public Shared Lang_SetGudang_Area As String
    Public Shared Lang_SetGudang_KodeSusunan As String
    Public Shared Lang_SetGudang_UkSusunan As String

    'Transaksi_Permintaan_BB_Produksi
    Public Shared Lang_Transaksi_Permintaan_BB_Produksi_Judul As String
    Public Shared Lang_Transaksi_Permintaan_BB_Produksi_Tambah As String
    Public Shared Lang_Transaksi_Permintaan_BB_Produksi_Jns_Brg As String
    Public Shared Lang_Transaksi_Permintaan_BB_Produksi_Req_Date As String
    Public Shared Lang_Transaksi_Permintaan_BB_Produksi_Error_Inqu As String
    Public Shared Lang_Transaksi_Permintaan_BB_Produksi_Error_Cus As String
    Public Shared Lang_Transaksi_Permintaan_BB_Produksi_Error1 As String
    Public Shared Lang_Transaksi_Permintaan_BB_Produksi_Error2 As String
    Public Shared Lang_Transaksi_Permintaan_BB_Produksi_Error3 As String
    Public Shared Lang_Transaksi_Permintaan_BB_Produksi_Error4 As String
    Public Shared Lang_Transaksi_Permintaan_BB_Produksi_Error5 As String
    Public Shared Lang_Transaksi_Permintaan_BB_Produksi_Error6 As String
    Public Shared Lang_Transaksi_Permintaan_BB_Produksi_Error7 As String
    Public Shared Lang_Transaksi_Permintaan_BB_Produksi_Error8 As String
    Public Shared Lang_Transaksi_Permintaan_BB_Produksi_Error9 As String
    Public Shared Lang_Transaksi_Permintaan_BB_Produksi_Error10 As String
    Public Shared Lang_Transaksi_Permintaan_BB_Produksi_Error11 As String
    Public Shared Lang_Transaksi_Permintaan_BB_Produksi_Error12 As String
    Public Shared Lang_Transaksi_Permintaan_BB_Produksi_Error13 As String

    'Binding_Barcode
    Public Shared Lang_Binding_Barcode_Judul_SD As String
    Public Shared Lang_Binding_Barcode_Judul As String
    Public Shared Lang_Binding_Barcode_Sampel As String
    Public Shared Lang_Binding_Barcode_Kd_Barcode As String

    'Master_Satuan
    Public Shared Lang_Master_Satuan_Judul As String
    Public Shared Lang_Master_Satuan_Kd_Satuan As String
    Public Shared Lang_Master_Satuan_Default_Insert As String
    Public Shared Lang_Master_Satuan_Tampil_Inq As String
    Public Shared Lang_Master_Satuan_Tampil_PO As String
    Public Shared Lang_Master_Satuan_Pembulatan As String
    Public Shared Lang_Master_Satuan_Tampil_Vol As String
    Public Shared Lang_Master_Satuan_Tampil_Berat As String
    Public Shared Lang_Master_Satuan_Kolom As String
    Public Shared Lang_Master_Satuan_Error_Kd_Satuan As String
    Public Shared Lang_Master_Satuan_Error_Blm_Pilih As String
    Public Shared Lang_Master_Satuan_Error1 As String
    Public Shared Lang_Master_Satuan_Error2 As String
    Public Shared Lang_Master_Satuan_Error3 As String
    Public Shared Lang_Master_Satuan_Tampil_Jumlah As String
    Public Shared Lang_Master_Satuan_Tampil_Panjang As String

    'master quality control
    Public Shared Lang_Quality_Control_Judul As String
    Public Shared Lang_Quality_Control_Kode As String
    Public Shared Lang_Quality_Control_Keterangan As String
    Public Shared Lang_Quality_Control_Target As String
    Public Shared Lang_Quality_Control_Satuan As String
    Public Shared Lang_Quality_Control_Tampil_Formula As String
    Public Shared Lang_Quality_Control_Tampil_Bahan As String
    Public Shared Lang_Quality_Control_Kolom As String
    Public Shared Lang_Quality_Control_Error_Kode As String
    Public Shared Lang_Quality_Control_Error_Keterangan As String
    Public Shared Lang_Quality_Control_Error_Target As String
    Public Shared Lang_Quality_Control_Error_Satuan As String
    Public Shared Lang_Quality_Control_Error_Blm_Pilih As String
    Public Shared Lang_Quality_Control_Error1 As String
    Public Shared Lang_Quality_Control_Error2 As String
    Public Shared Lang_Quality_Control_Judul_Kategori As String
    Public Shared Lang_Quality_Control_Error_Kategori As String
    'Master Mesin
    Public Shared Lang_Mesin_Divisi As String
    Public Shared Lang_Mesin_Judul As String
    Public Shared Lang_Mesin_NmMesin As String
    Public Shared Lang_Mesin_SeriMesin As String

    'master tipe seal
    Public Shared Lang_Tipe_Seal_Judul As String
    Public Shared Lang_Tipe_Seal_Kode As String
    Public Shared Lang_Tipe_Seal_Kode_Kemasan_Utama As String
    Public Shared Lang_Tipe_Seal_Keterangan As String
    Public Shared Lang_Tipe_Seal_Kolom As String
    Public Shared Lang_Tipe_Seal_Jenis_Produk As String
    Public Shared Lang_Tipe_Seal_Error_Kode As String
    Public Shared Lang_Tipe_Seal_Error_Keterangan As String
    Public Shared Lang_Tipe_Seal_Error_Jenis_Produk As String
    Public Shared Lang_Tipe_Seal_Error_Kemasan_Utama As String

    'master komposisi
    Public Shared Lang_Komposisi_Judul As String
    Public Shared Lang_Komposisi_Kode As String
    Public Shared Lang_Komposisi_Keterangan As String
    Public Shared Lang_Komposisi_Kolom As String
    Public Shared Lang_Komposisi_Error_Kode As String
    Public Shared Lang_Komposisi_Error_Nama As String

    'Master_Barang_Susunan
    Public Shared Lang_Barang_Susunan_Judul As String
    Public Shared Lang_Barang_Susunan_Susunan As String
    Public Shared Lang_Barang_Susunan_Jenis_Palet As String
    Public Shared Lang_Barang_Susunan_Lebar As String
    Public Shared Lang_Barang_Susunan_Panjang As String
    Public Shared Lang_Barang_Susunan_Tinggi_Per As String
    Public Shared Lang_Barang_Susunan_Kolom As String
    Public Shared Lang_Barang_Susunan_Error_Susunan As String
    Public Shared Lang_Barang_Susunan_Error_Jenis_Palet As String
    Public Shared Lang_Barang_Susunan_Error_Panjang As String
    Public Shared Lang_Barang_Susunan_Error_Lebar As String
    Public Shared Lang_Barang_Susunan_Error_Total As String
    Public Shared Lang_Barang_Susunan_Error_Jumlah As String
    Public Shared Lang_Barang_Susunan_Error_Ukuran As String
    Public Shared Lang_Barang_Susunan_Error_Tinggi_Per As String
    Public Shared Lang_Barang_Susunan_Error_Ket As String
    Public Shared Lang_Barang_Susunan_Error1 As String

    'Master_Pelapis_Packing_Level
    Public Shared Lang_Pelapis_Packing_Level_Judul As String
    Public Shared Lang_Pelapis_Packing_Level_Jenis As String
    Public Shared Lang_Pelapis_Packing_Level_Jenis_Pro As String
    Public Shared Lang_Pelapis_Packing_Level_Kd_Pelapis As String
    Public Shared Lang_Pelapis_Packing_Level_Kolom As String
    Public Shared Lang_Pelapis_Packing_Level_Kemasan_Pack As String
    Public Shared Lang_Pelapis_Packing_Level_Error_Ket As String
    Public Shared Lang_Pelapis_Packing_Level_Error_Jenis As String
    Public Shared Lang_Pelapis_Packing_Level_Error_Kode As String

    'Master_Bundling_Promo
    Public Shared Lang_Bundling_Promo_Judul As String
    Public Shared Lang_Bundling_Promo_Kolom As String
    Public Shared Lang_Bundling_Promo_Error_Kode As String
    Public Shared Lang_Bundling_Promo_Error_Ket As String

    'Master_Kemasan_Packing_Level
    Public Shared Lang_Master_Kemasan_Packing_Level_Judul As String
    Public Shared Lang_Master_Kemasan_Packing_Level_Jenis As String
    Public Shared Lang_Master_Kemasan_Packing_Level_Jenis_Pro As String
    Public Shared Lang_Master_Kemasan_Packing_Level_Kd_Pack As String
    Public Shared Lang_Master_Kemasan_Packing_Level_Kolom As String
    Public Shared Lang_Master_Kemasan_Packing_Level_Error_Ket As String
    Public Shared Lang_Master_Kemasan_Packing_Level_Error_Jenis As String

    'Master_Finishing_Kemasan
    Public Shared Lang_Finishing_Kemasan_Judul As String
    Public Shared Lang_Finishing_Kemasan_Jenis_Kemasan As String
    Public Shared Lang_Finishing_Kemasan_Kolom As String
    Public Shared Lang_Finishing_Kemasan_Jenis_Produk As String
    Public Shared Lang_Finishing_Kemasan_Kode_Finising As String
    Public Shared Lang_Finishing_Kemasan_Error_Ket As String
    Public Shared Lang_Finishing_Kemasan_Error_Jenis As String

    'barang 
    Public Shared Lang_Barang_Msg_Berat As String
    Public Shared Lang_Barang_Msg_Uk As String
    Public Shared Lang_Barang_Flag_Tampil_Display As String
    Public Shared Lang_Barang_Sd_Satuan_Judul As String
    Public Shared Lang_Barang_Satuan_Belum_Dipilih As String
    Public Shared Lang_Barang_Err_Sudah_Di_List As String
    Public Shared Lang_Barang_Err_Kode_Stock_Owner As String
    Public Shared Lang_Barang_Err_Kode_Barang As String
    Public Shared Lang_Barang_Err_Nama_Barang As String
    Public Shared Lang_Barang_Err_Satuan As String
    Public Shared Lang_Barang_Err_Keterangan As String
    Public Shared Lang_Barang_Err_Harga_Barang As String
    Public Shared Lang_Barang_Err_Stock_Min As String
    Public Shared Lang_Barang_Err_Kategori As String
    Public Shared Lang_Barang_Err_Status_Aktif As String
    Public Shared Lang_Barang_Err_Flag_PPN As String
    Public Shared Lang_Barang_Err_Barang_Sendiri As String
    Public Shared Lang_Barang_Err_Berat_Bersih As String
    Public Shared Lang_Barang_Err_Berat_Kotor As String
    Public Shared Lang_Barang_Err_Panjang As String
    Public Shared Lang_Barang_Err_Lebar As String
    Public Shared Lang_Barang_Err_Tinggi As String
    Public Shared Lang_Barang_Err_Kategori_Besar As String
    Public Shared Lang_Barang_Err_Kategori_Kecil As String
    Public Shared Lang_Barang_Err_Jenis_Gudang As String
    Public Shared Lang_Barang_Err_Nilai_Pengali1 As String
    Public Shared Lang_Barang_Err_Nilai_Pengali2 As String
    Public Shared Lang_Barang_Err_Flag_Tampil_Display As String
    Public Shared Lang_Barang_Err_Lokasi_Sudah_Ada As String
    Public Shared Lang_Barang_Err_Kode_Barang_Sudah_Ada As String
    Public Shared Lang_Barang_Err_Lokasi_Tidak_Ditemukan As String
    Public Shared Lang_Barang_Err_Pilih_Jenis_Barang As String

    'isi pl 
    Public Shared Lang_Isi_PL_Judul As String

    'size
    Public Shared Lang_Size_Judul As String

    'ras
    Public Shared Lang_Ras_Judul As String
    Public Shared Lang_Ras_Kode As String
    Public Shared Lang_Ras_Jenis_Hwan As String
    Public Shared Lang_Ras_Error_Jenis_Hewan As String
    Public Shared Lang_Ras_Error_Kode As String
    Public Shared Lang_Ras_Error_Keterangan As String

    'trah 
    Public Shared Lang_Trah_Judul As String
    Public Shared Lang_Trah_Kode As String

    'Transaksi_Produksi
    Public Shared Lang_Transaksi_Produksi_Judul_SD As String
    Public Shared Lang_Transaksi_Produksi_Error_Pilih As String
    Public Shared Lang_Transaksi_Produksi_Judul As String
    Public Shared Lang_Transaksi_Produksi_No_Batch As String
    Public Shared Lang_Transaksi_Produksi_No_Rencana As String
    Public Shared Lang_Transaksi_Produksi_Error_No_Batch As String
    Public Shared Lang_Transaksi_Produksi_Error_Operator As String

    'Transaksi_Hasil_Produksi
    Public Shared Lang_Hasil_Produksi_Judul As String
    Public Shared Lang_Hasil_Produksi_Tgl_Transaksi As String
    Public Shared Lang_Hasil_Produksi_Operator As String
    Public Shared Lang_Hasil_Produksi_Mesin As String
    Public Shared Lang_Hasil_Produksi_Hasil_Pro As String
    Public Shared Lang_Hasil_Produksi_Gagal_Pro As String
    Public Shared Lang_Hasil_Produksi_Akumulasi As String
    Public Shared Lang_Hasil_Produksi_No_Batch As String
    Public Shared Lang_Hasil_Produksi_Tarik_Data As String
    Public Shared Lang_Hasil_Produksi_Error_Operator As String
    Public Shared Lang_Hasil_Produksi_Error_No_Pro As String
    Public Shared Lang_Hasil_Produksi_Error_Gagal As String
    Public Shared Lang_Hasil_Produksi_Error_Tgl_Pro As String
    Public Shared Lang_Hasil_Produksi_Error_Tgl_Exp As String
    Public Shared Lang_Hasil_Produksi_Error_Tgl_Pro2 As String
    Public Shared Lang_Hasil_Produksi_Error_Gagal2 As String
    Public Shared Lang_Hasil_Produksi_Error_Brg_SN As String
    'cek data pengiriman
    Public Shared Lang_Master_CekPengiriman_Judul As String
    Public Shared Lang_Master_CekPengiriman_Judul_Sd As String
    Public Shared Lang_Master_CekPengiriman_Judul_Sd1 As String
    Public Shared Lang_Master_CekPengiriman_Err_Ekspedisi_Kosong As String
    Public Shared Lang_Master_CekPengiriman_Err_Id_Ekspedisi_Null As String
    Public Shared Lang_Master_CekPengiriman_Err_EkspedisiHrsDipilih As String

    'Rencana Produksi
    Public Shared Lang_Rencana_Produksi_Judul As String
    Public Shared Lang_Rencana_Produksi_Err_Hapus_Temp As String
    Public Shared Lang_Rencana_Produksi_Err_Jmlh_Sisa As String
    Public Shared Lang_Rencana_Produksi_Err_No_Tdk_Ada As String
    Public Shared Lang_Rencana_Produksi_Err_Harus_Sesuai_Antrian As String
    Public Shared Lang_Rencana_Produksi_No_Antrian As String
    Public Shared Lang_Rencana_Produksi_No_PO As String
    Public Shared Lang_Rencana_Produksi_Sisa As String
    Public Shared Lang_Rencana_Produksi_Err_Belum_Pilih As String
    Public Shared Lang_Rencana_Produksi_Err_Line As String
    Public Shared Lang_Rencana_Produksi_Err_Faktur As String
    Public Shared Lang_Rencana_Produksi_Err_Jumlah_Lebih_Besar As String
    Public Shared Lang_Rencana_Produksi_Err_No_PO_Sdh_Ada As String
    Public Shared Lang_Rencana_Produksi_Err_Jumlah_hrs_diisi As String

    Public Shared Lang_Rencana_Produksi_Err_Hrs_Selesai_Semua As String
    Public Shared Lang_Rencana_Produksi_Err_Hrs_Sesuai As String
    Public Shared Lang_Rencana_Produksi_Err_Jns_Produk_Beda As String
    Public Shared Lang_Rencana_Produksi_Judul_Sd As String
    Public Shared Lang_Rencana_Produksi_Err_Tanggal_Awal As String

    'Validasi_Permintaan_BB
    Public Shared Lang_Validasi_Permintaan_BB_Judul As String
    Public Shared Lang_Validasi_Permintaan_BB_No_Rencana As String

    'Po Bahan 
    Public Shared Lang_PO_Bahan_Judul As String

    'Compare_HPP
    Public Shared Lang_Compare_HPP_Judul As String
    Public Shared Lang_Compare_HPP_Real_HPP As String
    Public Shared Lang_Compare_HPP_Simulasi_HPP As String
    Public Shared Lang_Compare_HPP_Selisih_HPP As String
    Public Shared Lang_Compare_HPP_Error_Ket As String
    Public Shared Lang_Compare_HPP_Error As String

    'Display HPP
    Public Shared Lang_Display_HPP_Judul As String

    'kategori QC
    Public Shared Lang_Kategori_QC_Judul As String

    'Master Work Center
    Public Shared Lang_Master_Work_Center_Judul As String

    'Master Routing
    Public Shared Lang_Master_Routing As String

    'master klasifikasi bahan
    Public Shared Lang_Klasifikasi_Bahan_Judul As String
    Public Shared Lang_Klasifikasi_Bahan_Kode As String
    Public Shared Lang_Klasifikasi_Bahan_Keterangan As String
    Public Shared Lang_Klasifikasi_Bahan_Prefix As String
    Public Shared Lang_Klasifikasi_Bahan_Kolom As String
    Public Shared Lang_Klasifikasi_Bahan_Error_Kode As String
    Public Shared Lang_Klasifikasi_Bahan_Error_Nama As String
    Public Shared Lang_Klasifikasi_Bahan_Error_Prefix As String
    Public Shared Lang_Klasifikasi_Bahan_Error_Prefix_Numeric As String
    Public Shared Lang_Klasifikasi_Bahan_Error_Prefix_Sudah_Ada As String

    'master kategori PO
    Public Shared Lang_Kategori_PO_Judul As String
    Public Shared Lang_Kategori_PO_Kode As String
    Public Shared Lang_Kategori_PO_Keterangan As String
    Public Shared Lang_Kategori_PO_Kolom As String
    Public Shared Lang_Kategori_PO_Error_Kode As String
    Public Shared Lang_Kategori_PO_Error_Nama As String

    Public Shared Lang_Global_Mulai_Produksi As String
    Public Shared Lang_Global_Selesai_Produksi As String
    Public Shared Lang_Pilih_Dahulu_No_Transaksi As String
    Public Shared Lang_Jam_Produksi As String
    Public Shared Lang_Tgl_Selesai_Produksi As String
    Public Shared Lang_Jam_Selesai_Produksi As String
    Public Shared Lang_UserID_Selesai_Produksi As String
    Public Shared Lang_Tgl_Hasil_Produksi As String
    Public Shared Lang_Jam_Hasil_Produksi As String


    'tolong Untuk penamaan variable Lang_Nama_Form_xxxx

    Public Shared Sub Get_Languages(lang As String, jenis As String)
        SQL = "exec SP_GET_LANGUAGE '" & jenis & "', '" & Bahasa_Pilihan & "', NULL, NULL"
        Using dr = OpenTrans(SQL)
            If dr.Read Then
                If jenis = "GLOBAL" Then

                    Lang_Global_Simpan = dr("Lang_Global_Kode_Unik_Asal")
                    Lang_Global_Simpan = dr("Lang_Global_Kode_Unik_Berjalan")
                    Lang_Global_Simpan = dr("Lang_Global_Nilai_Formula")
                    Lang_Global_Simpan = dr("Lang_Global_Nilai_Produksi")
                    Lang_Global_Simpan = dr("Lang_Global_Qty_Hasil_Produksi")
                    Lang_Global_Simpan = dr("Lang_Global_Simpan")
                    Lang_Global_Hapus = dr("Lang_Global_Hapus")
                    Lang_Global_Update = dr("Lang_Global_Update")
                    Lang_Global_Cari = dr("Lang_Global_Cari")
                    Lang_Global_Refresh = dr("Lang_Global_Refresh")
                    Lang_Global_Tanya_Hapus = dr("Lang_Global_Tanya_Hapus")
                    Lang_Global_Hapus_No = dr("Lang_Global_Hapus_No")
                    Lang_Global_Error1 = dr("Lang_Global_Error1")
                    Lang_Global_Perhatian = dr("Lang_Global_Perhatian")
                    Lang_Global_Data_Tdk_Ditemukan = dr("Lang_Global_Data_Tdk_Ditemukan")
                    Lang_Global_Sukses_Update = dr("Lang_Global_Sukses_Update")
                    Lang_Global_Data_Sdh_Ada = dr("Lang_Global_Data_Sdh_Ada")
                    Lang_Global_Sukses_Simpan = dr("Lang_Global_Sukses_Simpan")
                    Lang_Global_Error_Paramater = dr("Lang_Global_Error_Paramater")
                    Lang_Global_Error_Paramater_Tgl = dr("Lang_Global_Error_Paramater_Tgl")
                    Lang_Global_Error_Paramater_Tgl2 = dr("Lang_Global_Error_Paramater_Tgl2")
                    Lang_Global_Error_Paramater_Lain = dr("Lang_Global_Error_Paramater_Lain")
                    Lang_Global_Error_Paramater_Lain2 = dr("Lang_Global_Error_Paramater_Lain2")
                    Lang_Global_Batal = dr("Lang_Global_Batal")
                    Lang_Global_Error_Tdk_Ada_Akses = dr("Lang_Global_Error_Tdk_Ada_Akses")
                    Lang_Global_Berhasil_Batal = dr("Lang_Global_Berhasil_Batal")
                    Lang_Global_Customer = dr("Lang_Global_Customer")
                    Lang_Global_NoInquiry = dr("Lang_Global_NoInquiry")
                    Lang_Global_NoFaktur = dr("Lang_Global_NoFaktur")
                    Lang_Global_Tanggal = dr("Lang_Global_Tanggal")
                    Lang_Global_Belum_Diisi = dr("Lang_Global_Belum_Diisi")
                    Lang_Global_Belum_Diubah = dr("Lang_Global_Belum_Diubah")
                    Lang_Global_SeluruhCombobox = dr("Lang_Global_SeluruhCombobox")
                    Lang_Global_NoFormula = dr("Lang_Global_NoFormula")
                    Lang_Global_KodeCustomer = dr("Lang_Global_KodeCustomer")
                    Lang_Global_KodeBarang = dr("Lang_Global_KodeBarang")
                    Lang_Global_NamaBarang = dr("Lang_Global_NamaBarang")
                    Lang_Global_NamaCustomer = dr("Lang_Global_NamaCustomer")
                    Lang_Global_Lokasi = dr("Lang_Global_Lokasi")
                    Lang_Global_LokasiGudang = dr("Lang_Global_LokasiGudang")
                    Lang_Global_Hasil = dr("Lang_Global_Hasil")
                    Lang_Global_Satuan = dr("Lang_Global_satuan")
                    Lang_Global_FormAsal = dr("Lang_Global_FormAsal")
                    Lang_Global_Tidak_Bisa_Berbeda = dr("Lang_Global_Tidak_Bisa_Berbeda")
                    Lang_Global_Nilai_Pengali = dr("Lang_Global_Nilai_Pengali")
                    Lang_Global_Satuan_Barang = dr("Lang_Global_Satuan_Barang")
                    Lang_Global_Nilai_Barang = dr("Lang_Global_Nilai_Barang")
                    Lang_Global_jumlah_kuantiti = dr("Lang_Global_jumlah_kuantiti")
                    Lang_Global_Persentase = dr("Lang_Global_Persentase")
                    Lang_Global_Hari_ini = dr("Lang_Global_Hari_ini")
                    Lang_Global_Para_Tbl = dr("Lang_Global_Para_Tbl")
                    Lang_Global_Para_lain = dr("Lang_Global_Para_lain")
                    Lang_Global_Sukses_Hapus = dr("Lang_Global_Sukses_Hapus")
                    Lang_Global_Kolom = dr("Lang_Global_Kolom")
                    Lang_Global_Nama = dr("Lang_Global_Nama")
                    Lang_Global_Penawaran = dr("Lang_Global_Penawaran")
                    Lang_Global_Supplier = dr("Lang_Global_Supplier")
                    Lang_Global_MinOrder = dr("Lang_Global_MinOrder")
                    Lang_Global_HargaSatuan = dr("Lang_Global_HargaSatuan")
                    Lang_Global_Bahan = dr("Lang_Global_Bahan")
                    Lang_Global_Kandungan = dr("Lang_Global_Kandungan")
                    Lang_Global_Total = dr("Lang_Global_Total")
                    Lang_Global_TotalPersen = dr("Lang_Global_TotalPersen")
                    Lang_Global_Persen_harus_100 = dr("Lang_Global_Persen_harus_100")
                    Lang_Global_Lokasi_Tujuan = dr("Lang_Global_Lokasi_Tujuan")
                    Lang_Global_Lokasi_Awal = dr("Lang_Global_Lokasi_Awal")
                    Lang_Global_Kemasan = dr("Lang_Global_Kemasan")
                    Lang_Global_Cara_Kirim = dr("Lang_Global_Cara_Kirim")
                    Lang_Global_Bahan_Baku = dr("Lang_Global_Bahan_Baku")
                    Lang_Global_Sukses_Hapus = dr("Lang_Global_Sukses_Hapus")
                    Lang_Global_Kolom = dr("Lang_Global_Kolom")
                    Lang_Global_Nama = dr("Lang_Global_Nama")
                    Lang_Global_Penawaran = dr("Lang_Global_Penawaran")
                    Lang_Global_Supplier = dr("Lang_Global_Supplier")
                    Lang_Global_MinOrder = dr("Lang_Global_MinOrder")
                    Lang_Global_HargaSatuan = dr("Lang_Global_HargaSatuan")
                    Lang_Global_Lokasi_Awal = dr("Lang_Global_Lokasi_Awal")
                    Lang_Global_Lokasi_Tujuan = dr("Lang_Global_Lokasi_Tujuan")
                    Lang_Global_Cara_Kirim = dr("Lang_Global_Cara_Kirim")
                    Lang_Global_TidakBolehLebihDari = dr("Lang_Global_TidakBolehLebihDari")
                    Lang_Global_NoNota = dr("Lang_Global_NoNota")
                    Lang_Global_DataSudahBatal = dr("Lang_Global_DataSudahBatal")
                    Lang_Global_Jam = dr("Lang_Global_Jam")
                    Lang_Global_JenisPembayaran = dr("Lang_Global_JenisPembayaran")
                    Lang_Global_MataUang = dr("Lang_Global_MataUang")
                    Lang_Global_Kurs = dr("Lang_Global_Kurs")
                    Lang_Global_CaraBayar = dr("Lang_Global_CaraBayar")
                    Lang_Global_GrandSblmPPN = dr("Lang_Global_GrandSblmPPN")
                    Lang_Global_TotalMUA = dr("Lang_Global_TotalMUA")
                    Lang_Global_PPN = dr("Lang_Global_PPN")
                    Lang_Global_Jumlah = dr("Lang_Global_Jumlah")
                    Lang_Global_Harga = dr("Lang_Global_Harga")
                    Lang_Global_Validasi = dr("Lang_Global_Validasi")
                    Lang_Global_Sukses_Validasi = dr("Lang_Global_Sukses_Validasi")
                    Lang_Global_Barang = dr("Lang_Global_Barang")
                    Lang_Global_Gudang = dr("Lang_Global_Gudang")
                    Lang_Global_Kecamatan = dr("Lang_Global_Kecamatan")
                    Lang_Global_Kelurahan = dr("Lang_Global_Kelurahan")
                    Lang_Global_Asal = dr("Lang_Global_Asal")
                    Lang_Global_Tujuan = dr("Lang_Global_Tujuan")
                    Lang_Global_Error_Kd_Brg = dr("Lang_Global_Error_Kd_Brg")
                    Lang_Global_Error_Nm_Brg = dr("Lang_Global_Error_Nm_Brg")
                    Lang_Global_Error_Lokasi_Tujuan = dr("Lang_Global_Error_Lokasi_Tujuan")
                    Lang_Global_Error_Satuan = dr("Lang_Global_Error_Satuan")
                    Lang_Global_Error_Jumlah = dr("Lang_Global_Error_Jumlah")
                    Lang_Global_Error_Jns_Pengali = dr("Lang_Global_Error_Jns_Pengali")
                    Lang_Global_Error_Jns_Hitung = dr("Lang_Global_Error_Jns_Hitung")
                    Lang_Global_Error_Lv_Kosong = dr("Lang_Global_Error_Lv_Kosong")
                    Lang_Global_No_PO = dr("Lang_Global_No_PO")
                    Lang_Global_Kode_Supplier = dr("Lang_Global_Kode_Supplier")
                    Lang_Global_Panjang = dr("Lang_Global_Panjang")
                    Lang_Global_Lebar = dr("Lang_Global_Lebar")
                    Lang_Global_Tinggi = dr("Lang_Global_Tinggi")
                    Lang_Global_Ukuran = dr("Lang_Global_Ukuran")
                    Lang_Global_Kode = dr("Lang_Global_Kode")
                    Lang_Global_Media_Kirim = dr("Lang_Global_Media_Kirim")
                    Lang_Global_Berat = dr("Lang_Global_Berat")
                    Lang_Global_SatuanVolume = dr("Lang_Global_SatuanVolume")
                    Lang_Global_SatuanBerat = dr("Lang_Global_SatuanBerat")
                    Lang_Global_HitungVolume = dr("Lang_Global_HitungVolume")
                    Lang_Global_Tanggal_Jatuh_Tempo = dr("Lang_Global_Tanggal_Jatuh_Tempo")
                    Lang_Global_Nilai_PPN = dr("Lang_Global_Nilai_PPN")
                    Lang_Global_Diskon_Persen = dr("Lang_Global_Diskon_Persen")
                    Lang_Global_Nilai_Diskon = dr("Lang_Global_Nilai_Diskon")
                    Lang_Global_Jenis_Satuan = dr("Lang_Global_Jenis_Satuan")
                    Lang_Global_Data_Produk = dr("Lang_Global_Data_Produk")
                    Lang_Global_No_Nota = dr("Lang_Global_No_Nota")
                    Lang_Global_Produksi = dr("Lang_Global_Produksi")
                    Lang_Global_Expired = dr("Lang_Global_Expired")
                    Lang_Global_Error_Update = dr("Lang_Global_Error_Update")
                    Lang_Global_Alert_Simpan = dr("Lang_Global_Alert_Simpan")
                    Lang_Global_Label_No_Inquiry_PO = dr("Lang_Global_Label_No_Inquiry_PO")
                    Lang_Global_KodeBarangTidakAda = dr("Lang_Global_KodeBarangTidakAda")
                    lang_global_keterangan = dr("lang_global_keterangan")
                    lang_global_Error_LokasiTidakAda = dr("lang_global_Error_LokasiTidakAda")
                    lang_global_Nama_Supplier = dr("lang_global_Nama_Supplier")
                    Lang_Global_Satuan_Panjang = dr("Lang_Global_Satuan_Panjang")
                    Lang_Global_EstimasiSerapan = dr("Lang_Global_EstimasiSerapan")
                    Lang_Global_hargaJual = dr("Lang_Global_hargaJual")
                    Lang_Global_HargaTerendah = dr("Lang_Global_HargaTerendah")
                    Lang_Global_HargaTertinggi = dr("Lang_Global_HargaTertinggi")
                    Lang_Global_Jenis = dr("Lang_Global_Jenis")
                    Lang_Global_MarkUp = dr("Lang_Global_MarkUp")
                    Lang_Global_Belum_ada_Penawaran = dr("Lang_Global_Belum_ada_Penawaran")
                    Lang_Global_QtySimulasi = dr("Lang_Global_QtySimulasi")
                    Lang_Global_NamaEkspedisi = dr("Lang_Global_NamaEkspedisi")
                    Lang_Global_Kecamatan_awal = dr("Lang_Global_Kecamatan_awal")
                    Lang_Global_Kecamatan_tujuan = dr("Lang_Global_Kecamatan_tujuan")
                    Lang_Global_Kelurahan_awal = dr("Lang_Global_Kelurahan_awal")
                    Lang_Global_Kelurahan_tujuan = dr("Lang_Global_Kelurahan_tujuan")
                    Lang_Global_Kota_awal = dr("Lang_Global_Kota_awal")
                    Lang_Global_Kota_tujuan = dr("Lang_Global_Kota_tujuan")
                    Lang_Global_Provinsi_awal = dr("Lang_Global_Provinsi_awal")
                    Lang_Global_Provinsi_tujuan = dr("Lang_Global_Provinsi_tujuan")
                    Lang_Global_Bruto = dr("Lang_Global_Bruto")
                    Lang_Global_TglBruto = dr("Lang_Global_TglBruto")
                    Lang_Global_JamBruto = dr("Lang_Global_JamBruto")
                    Lang_Global_FotoBruto = dr("Lang_Global_FotoBruto")
                    Lang_Global_Tara = dr("Lang_Global_Tara")
                    Lang_Global_TglTara = dr("Lang_Global_TglTara")
                    Lang_Global_JamTara = dr("Lang_Global_JamTara")
                    Lang_Global_UserTara = dr("Lang_Global_UserTara")
                    Lang_Global_FotoTara = dr("Lang_Global_FotoTara")
                    Lang_Global_TimbangKosong = dr("Lang_Global_TimbangKosong")
                    Lang_Global_TimbangIsi = dr("Lang_Global_TimbangIsi")
                    Lang_Global_Ekspedisi = dr("Lang_Global_Ekspedisi")
                    Lang_Global_Supir = dr("Lang_Global_Supir")
                    Lang_Global_PlatNomor = dr("Lang_Global_PlatNomor")
                    Lang_Global_WaktuTimbang = dr("Lang_Global_WaktuTimbang")
                    Lang_Global_FotoKendaraan = dr("Lang_Global_FotoKendaraan")
                    Lang_Global_KoneksiKamera = dr("Lang_Global_KoneksiKamera")
                    Lang_Global_TimbangMasuk = dr("Lang_Global_TimbangMasuk")
                    Lang_Global_TimbangKeluar = dr("Lang_Global_TimbangKeluar")
                    Lang_Global_Error_Validasi = dr("Lang_Global_Error_Validasi")
                    Lang_Global_Pilih_Batal = dr("Lang_Global_Pilih_Batal")
                    Lang_Global_Tanya_Batal = dr("Lang_Global_Tanya_Batal")
                    Lang_Global_NoTimbangan = dr("Lang_Global_NoTimbangan")
                    Lang_Global_ListKendaraan = dr("Lang_Global_ListKendaraan")
                    Lang_Global_Timbangan = dr("Lang_Global_Timbangan")
                    Lang_Global_Tunai = dr("Lang_Global_Tunai")
                    Lang_Global_Non_Tunai = dr("Lang_Global_Non_Tunai")
                    Lang_Global_Error_No_PO = dr("Lang_Global_Error_No_PO")
                    Lang_Global_Error_Jatuh_Tempo = dr("Lang_Global_Error_Jatuh_Tempo")
                    Lang_Global_Error_Pernah_Simpan = dr("Lang_Global_Error_Pernah_Simpan")
                    Lang_Global_Pilih_Hapus = dr("Lang_Global_Pilih_Hapus")
                    Lang_Global_Tanggal_PO = dr("Lang_Global_Tanggal_PO")
                    Lang_Global_NO_BM = dr("Lang_Global_NO_BM")
                    Lang_Global_NO = dr("Lang_Global_NO")
                    Lang_Global_Tanggal_Produksi = dr("Lang_Global_Tanggal_Produksi")
                    Lang_Global_Tanggal_Expired = dr("Lang_Global_Tanggal_Expired")
                    Lang_GLOBAL_No_Surat_Jalan = dr("Lang_GLOBAL_No_Surat_Jalan")
                    Lang_Global_JenisKategoriHarga = dr("Lang_Global_JenisKategoriHarga")
                    Lang_Global_KategoriKemasan = dr("Lang_Global_KategoriKemasan")
                    Lang_Global_KategoriBerat = dr("Lang_Global_KategoriBerat")
                    Lang_Global_RangeHarga = dr("Lang_Global_RangeHarga")
                    Lang_Global_sampaidengan = dr("Lang_Global_sampaidengan")
                    Lang_Global_PersenMarkUp = dr("Lang_Global_PersenMarkUp")
                    Lang_Global_KategoriProduk = dr("Lang_Global_KategoriProduk")
                    Lang_Global_Rangetidakbolehlebihrendah = dr("Lang_Global_Rangetidakbolehlebihrendah")
                    Lang_Global_Error_Lokasi = dr("Lang_Global_Error_Lokasi")
                    Lang_Global_Catatan = dr("Lang_Global_Catatan")
                    Lang_Global_No_Transaksi = dr("Lang_Global_No_Transaksi")
                    Lang_Global_Error_No_Transaksi = dr("Lang_Global_Error_No_Transaksi")
                    Lang_Global_Penangung_Jawab = dr("Lang_Global_Penangung_Jawab")
                    Lang_Global_Error_Ya = dr("Lang_Global_Error_Ya")
                    Lang_Global_Error_Tidak = dr("Lang_Global_Error_Tidak")
                    Lang_GLOBAL_Volume = dr("Lang_GLOBAL_Volume")
                    Lang_Global_Harga_Beli = dr("Lang_Global_Harga_Beli")
                    Lang_Global_Barang_Sendiri = dr("Lang_Global_Barang_Sendiri")
                    Lang_Global_Kategori = dr("Lang_Global_Kategori")
                    Lang_Global_Aktif = dr("Lang_Global_Aktif")
                    Lang_Global_Flag_PPN = dr("Lang_Global_Flag_PPN")
                    Lang_Global_Jenis_Gudang = dr("Lang_Global_Jenis_Gudang")
                    Lang_Global_Kode_SO = dr("Lang_Global_Kode_SO")
                    Lang_Global_Penentu_Harga = dr("Lang_Global_Penentu_Harga")
                    Lang_Global_Stok_Min = dr("Lang_Global_Stok_Min")
                    Lang_Global_Kategori_Kecil = dr("Lang_Global_Kategori_Kecil")
                    Lang_Global_Kategori_Besar = dr("Lang_Global_Kategori_Besar")
                    Lang_Global_Berat_Bersih = dr("Lang_Global_Berat_Bersih")
                    Lang_Global_Berat_Kotor = dr("Lang_Global_Berat_Kotor")
                    Lang_Global_Satuan_Jumlah = dr("Lang_Global_Satuan_Jumlah")
                    Lang_Global_Jenis_Produk = dr("Lang_Global_Jenis_Produk")
                    Lang_Global_Bentuk = dr("Lang_Global_Bentuk")
                    Lang_Global_Kode_Size = dr("Lang_Global_Kode_Size")
                    Lang_Global_Ketebalan = dr("Lang_Global_Ketebalan")
                    Lang_Global_Trah = dr("Lang_Global_Trah")
                    Lang_Global_BeratKemasan = dr("Lang_Global_BeratKemasan")
                    Lang_Global_JudulHewan = dr("Lang_Global_JudulHewan")
                    Lang_Global_JudulProduk = dr("Lang_Global_JudulProduk")
                    Lang_Global_JudulKemasan = dr("Lang_Global_JudulKemasan")
                    Lang_Global_JudulPacking = dr("Lang_Global_JudulPacking")
                    Lang_global_Periode_Awal = dr("Lang_global_Periode_Awal")
                    Lang_global_Periode_Akhir = dr("Lang_global_Periode_Akhir")
                    Lang_Global_P = dr("Lang_Global_P")
                    Lang_Global_L = dr("Lang_Global_L")
                    Lang_Global_T = dr("Lang_Global_T")
                    Lang_Global_List_Inquiry_PO = dr("Lang_Global_List_Inquiry_PO")
                    Lang_Global_Jumlah_Sisa = dr("Lang_Global_Jumlah_Sisa")
                    Lang_Global_Kota = dr("Lang_Global_Kota")
                    Lang_Global_No_Produksi = dr("Lang_Global_No_Produksi")
                    Lang_GLOBAL_Masuk_Tny_Val_kurang = dr("Lang_GLOBAL_Masuk_Tny_Val_kurang")
                    Lang_Global_Data_Sdh_Val = dr("Lang_Global_Data_Sdh_Val")
                    Lang_GLOBAL_Masuk_Error_Minus_Stock = dr("Lang_GLOBAL_Masuk_Error_Minus_Stock")
                    Lang_GLOBAL_Error_Brg_SN1 = dr("Lang_GLOBAL_Error_Brg_SN1")
                    Lang_GLOBAL_Jmlh_Stok_Krg = dr("Lang_GLOBAL_Jmlh_Stok_Krg")
                    Lang_GLOBAL_SN_untuk_barang = dr("Lang_GLOBAL_SN_untuk_barang")
                    Lang_GLOBAL_Tidak_Ditemukan = dr("Lang_GLOBAL_Tidak_Ditemukan")
                    Lang_GLOBAL_Jumlah_Produksi = dr("Lang_GLOBAL_Jumlah_Produksi")
                    Lang_GLOBAL_Jumlah_PO = dr("Lang_GLOBAL_Jumlah_PO")
                    Lang_GLOBAL_Jumlah_Tambahan = dr("Lang_GLOBAL_Jumlah_Tambahan")
                    Lang_GLOBAL_Jumlah_Stock = dr("Lang_GLOBAL_Jumlah_Stock")
                    Lang_GLOBAL_Jumlah_Order = dr("Lang_GLOBAL_Jumlah_Order")
                    Lang_GLOBAL_Terjadi_Kesalahan = dr("Lang_GLOBAL_Terjadi_Kesalahan")
                    Lang_GLOBAL_Ulangi_Transaksi = dr("Lang_GLOBAL_Ulangi_Transaksi")
                    Lang_GLOBAL_Kategori_Supplier = dr("Lang_GLOBAL_Kategori_Supplier")
                    Lang_GLOBAL_Data_Kontainer = dr("Lang_GLOBAL_Data_Kontainer")
                    Lang_GLOBAL_Id_Rencana = dr("Lang_GLOBAL_Id_Rencana")
                    Lang_Global_Grand = dr("Lang_Global_Grand")
                    Lang_Global_totalIDR = dr("Lang_Global_totalIDR")
                    Lang_Global_biaya = dr("Lang_Global_biaya")
                    Lang_Global_ETD = dr("Lang_Global_ETD")
                    Lang_Global_Ras = dr("Lang_Global_Ras")
                    Lang_global_Jenislabel = dr("Lang_global_Jenislabel")
                    Lang_Global_Jenisstiker = dr("Lang_Global_Jenisstiker")
                    Lang_Global_tipeSeal = dr("Lang_Global_tipeSeal")
                    Lang_Global_Jenisfinishing = dr("Lang_Global_Jenisfinishing")
                    Lang_Global_KapasitasKemasan = dr("Lang_Global_KapasitasKemasan")
                    Lang_Global_Val = dr("Lang_Global_Val")
                    Lang_GLOBAL_Tanya_Validasi = dr("Lang_GLOBAL_Tanya_Validasi")
                    Lang_Global_Bahan_Penolong = dr("Lang_Global_Bahan_Penolong")
                    Lang_Global_Bahan_Pengiriman = dr("Lang_Global_Bahan_Pengiriman")
                    Lang_Global_NilaiBiaya = dr("Lang_Global_NilaiBiaya")
                    Lang_Global_Tambah = dr("Lang_Global_Tambah")
                    Lang_Global_SudahAda = dr("Lang_Global_SudahAda")
                    Lang_MasterBiaya_Judul = dr("Lang_MasterBiaya_Judul")
                    Lang_MasterKategoriGudang_Judul = dr("Lang_MasterKategoriGudang_Judul")
                    Lang_Global_KategoriGudang = dr("Lang_Global_KategoriGudang")
                    Lang_GLOBAL_QC_Kode_Uji = dr("Lang_GLOBAL_QC_Kode_Uji")
                    Lang_GLOBAL_QC_Keterangan = dr("Lang_GLOBAL_QC_Keterangan")
                    Lang_GLOBAL_QC_Satuan = dr("Lang_GLOBAL_QC_Satuan")
                    Lang_GLOBAL_Tidak_Ada_Data = dr("Lang_GLOBAL_Tidak_Ada_Data")
                    Lang_GLOBAL_Pilih_Dahulu_Jenis_Produk = dr("Lang_GLOBAL_Pilih_Dahulu_Jenis_Produk")
                    Lang_GLOBAL_Data_Sdh_Ditambahkan = dr("Lang_GLOBAL_Data_Sdh_Ditambahkan")
                    Lang_GLOBAL_Jenis_Input_Tdk_Ditemukan = dr("Lang_GLOBAL_Jenis_Input_Tdk_Ditemukan")
                    Lang_GLOBAL_Januari = dr("Lang_GLOBAL_Januari")
                    Lang_Global_Februari = dr("Lang_Global_Februari")
                    Lang_Global_Maret = dr("Lang_Global_Maret")
                    Lang_Global_April = dr("Lang_Global_April")
                    Lang_Global_Mei = dr("Lang_Global_Mei")
                    Lang_Global_Juni = dr("Lang_Global_Juni")
                    Lang_Global_Juli = dr("Lang_Global_Juli")
                    Lang_Global_Agustus = dr("Lang_Global_Agustus")
                    Lang_Global_September = dr("Lang_Global_September")
                    Lang_Global_Oktober = dr("Lang_Global_Oktober")
                    Lang_Global_November = dr("Lang_Global_November")
                    Lang_Global_Desember = dr("Lang_Global_Desember")
                    Lang_Global_Bulan = dr("Lang_Global_Bulan")
                    Lang_Global_Tahun = dr("Lang_Global_Tahun")
                    Lang_Global_PilihSeluruh = dr("Lang_Global_PilihSeluruh")
                    Lang_Global_TambahBarang = dr("Lang_Global_TambahBarang")
                    Lang_Global_KlasifikasiBahan = dr("Lang_Global_Klasifikasi_Bahan")
                    Lang_Global_KategoriPO = dr("Lang_Global_Kategori_PO")
                    Lang_Global_Metode_Truckscale = dr("Lang_Global_Metode_Truckscale")
                    Lang_Global_Metode_Unloading = dr("Lang_Global_Metode_Unloading")
                    Lang_Global_Kode_Bahan = dr("Lang_Global_Kode_Bahan")
                    Lang_Global_Nama_Bahan = dr("Lang_Global_Nama_Bahan")
                    Lang_Global_Qty_Barang = dr("Lang_Global_Qty_Barang")
                    Lang_Global_Qty_Bahan = dr("Lang_Global_Qty_Bahan")
                    Lang_Global_Level = dr("Lang_Global_Level")
                    Lang_Global_Data_Barang = dr("Lang_Global_Data_Barang")
                    Lang_Global_Data_Bahan = dr("Lang_Global_Data_Bahan")
                    Lang_Global_ValidQty = dr("Lang_Global_ValidQty")
                    Lang_Global_ValidKbBhn = dr("Lang_Global_ValidKbBhn")
                    Lang_Global_No_Transaksi = dr("Lang_Global_No_Transaksi")
                    Lang_Global_Mulai_Produksi = dr("Lang_Global_Mulai_Produksi")
                    Lang_Global_Selesai_Produksi = dr("Lang_Global_Selesai_Produksi")
                    Lang_Pilih_Dahulu_No_Transaksi = dr("Lang_Pilih_Dahulu_No_Transaksi")
                    Lang_Jam_Produksi = dr("Lang_Jam_Produksi")
                    Lang_Tgl_Selesai_Produksi = dr("Lang_Tgl_Selesai_Produksi")
                    Lang_Jam_Selesai_Produksi = dr("Lang_Jam_Selesai_Produksi")
                    Lang_UserID_Selesai_Produksi = dr("Lang_UserID_Selesai_Produksi")
                    Lang_Tgl_Hasil_Produksi = dr("Lang_Tgl_Hasil_Produksi")
                    Lang_Jam_Hasil_Produksi = dr("Lang_Jam_Hasil_Produksi")
                ElseIf jenis = "Display_Production_Order" Then
                    Lang_Display_Production_Order_Judul = dr("Lang_Display_Production_Order_Judul")
                    Lang_Display_Production_Order_Error_Pilih = dr("Lang_Display_Production_Order_Error_Pilih")
                    Lang_Display_Production_Order_Judul2 = dr("Lang_Display_Production_Order_Judul2")
                    Lang_Display_Production_Order_Qty_Produksi = dr("Lang_Display_Production_Order_Qty_Produksi")
                    Lang_Display_Production_Order_Nilai_Produksi = dr("Lang_Display_Production_Order_Nilai_Produksi")
                    Lang_Display_Production_Order_Hasil_Produksi = dr("Lang_Display_Production_Order_Hasil_Produksi")
                    Lang_Display_Production_Order_Qty_Produksi2 = dr("Lang_Display_Production_Order_Qty_Produksi2")
                    Lang_Display_Production_Order_Error_Qty1 = dr("Lang_Display_Production_Order_Error_Qty1")
                    Lang_Display_Production_Order_Error_Qty2 = dr("Lang_Display_Production_Order_Error_Qty2")
                    Lang_Display_Production_Order_Error_Qty3 = dr("Lang_Display_Production_Order_Error_Qty3")
                    Lang_Display_Production_Order_Error_Qty4 = dr("Lang_Display_Production_Order_Error_Qty4")

                ElseIf jenis = "Transaksi_Sales_Forecasting" Then
                    Lang_Transaksi_Sales_Forecasting = dr("Lang_Transaksi_Sales_Forecasting")
                ElseIf jenis = "Master_Quality_Control_Kendaraan" Then
                    Lang_Quality_Control_Kendaraan_Judul = dr("Lang_Quality_Control_Kendaraan_Judul")
                ElseIf jenis = "Kategori_QC" Then
                    Lang_Kategori_QC_Judul = dr("Lang_Kategori_QC_Judul")
                ElseIf jenis = "Master_Penawaran" Then
                    Lang_Penawaran_Judul = dr("Lang_Penawaran_Judul")
                    Lang_Penawaran_NoPenawaran = dr("Lang_Penawaran_NoPenawaran")
                    Lang_Penawaran_TglPenawaranHrg = dr("Lang_Penawaran_TglPenawaranHrg")
                    Lang_Penawaran_PeriodeAkhir = dr("Lang_Penawaran_PeriodeAkhir")
                ElseIf jenis = "Master_Ongkir" Then
                    Lang_Ongkir_Judul = dr("Lang_Ongkir_Judul")
                    Lang_Ongkir_ProvAsal = dr("Lang_Ongkir_ProvAsal")
                    Lang_Ongkir_ProvTujuan = dr("Lang_Ongkir_ProvTujuan")
                    Lang_Ongkir_KabKotaAsal = dr("Lang_Ongkir_KabKotaAsal")
                    Lang_Ongkir_KabKotaTujuan = dr("Lang_Ongkir_KabKotaTujuan")
                    Lang_Ongkir_Ukuran = dr("Lang_Ongkir_Ukuran")
                    Lang_Ongkir_Hrg = dr("Lang_Ongkir_Hrg")
                    Lang_Ongkir_NmEkspedisi = dr("Lang_Ongkir_NmEkspedisi")
                    Lang_Ongkir_MediaKirim = dr("Lang_Ongkir_MediaKirim")
                ElseIf jenis = "Master_Work_Center" Then
                    Lang_Master_Work_Center_Judul = dr("Lang_Master_Work_Center_Judul")
                ElseIf jenis = "Master_Routing" Then
                    Lang_Master_Routing = dr("Lang_Master_Routing")
                ElseIf jenis = "Master_Jenis_Hewan" Then
                    Lang_Jenis_Hewan_Judul = dr("Lang_Jenis_Hewan_Judul")
                    Lang_Jenis_Hewan_Kode = dr("Lang_Jenis_Hewan_Kode")
                    Lang_Jenis_Hewan_Keterangan = dr("Lang_Jenis_Hewan_Keterangan")
                    Lang_Jenis_Hewan_Kolom = dr("Lang_Jenis_Hewan_Kolom")
                    Lang_Jenis_Hewan_Error_Kode = dr("Lang_Jenis_Hewan_Error_Kode")
                    Lang_Jenis_Hewan_Error_Nama = dr("Lang_Jenis_Hewan_Error_Nama")
                    Lang_Jenis_Hewan_Prefix = dr("Lang_Jenis_Hewan_Prefix")
                    Lang_Jenis_Hewan_Error_Prefix = dr("Lang_Jenis_Hewan_Error_Prefix")
                ElseIf jenis = "Master_Kategori_Umur" Then
                    Lang_Kategori_Umur_Judul = dr("Lang_Kategori_Umur_Judul")
                    Lang_Kategori_Umur_Jenis = dr("Lang_Kategori_Umur_Jenis")
                    Lang_Kategori_Umur_Kode = dr("Lang_Kategori_Umur_Kode")
                    Lang_Kategori_Umur_Keterangan = dr("Lang_Kategori_Umur_Keterangan")
                    Lang_Kategori_Umur_Kolom = dr("Lang_Kategori_Umur_Kolom")
                    Lang_Kategori_Umur_Error_Jenis = dr("Lang_Kategori_Umur_Error_Jenis")
                    Lang_Kategori_Umur_Error_Kode = dr("Lang_Kategori_Umur_Error_Kode")
                    Lang_Kategori_Umur_Error_Keterangan = dr("Lang_Kategori_Umur_Error_Keterangan")
                ElseIf jenis = "Master_Jenis_Produk" Then
                    Lang_Jenis_Produk_Judul = dr("Lang_Jenis_Produk_Judul")
                    Lang_Jenis_Produk_Kode = dr("Lang_Jenis_Produk_Kode")
                    Lang_Jenis_Produk_Keterangan = dr("Lang_Jenis_Produk_Keterangan")
                    Lang_Jenis_Produk_Protein = dr("Lang_Jenis_Produk_Protein")
                    Lang_Jenis_Produk_Lemak = dr("Lang_Jenis_Produk_Lemak")
                    Lang_Jenis_Produk_Min = dr("Lang_Jenis_Produk_Min")
                    Lang_Jenis_Produk_Max = dr("Lang_Jenis_Produk_Max")
                    Lang_Jenis_Produk_Catatan = dr("Lang_Jenis_Produk_Catatan")
                    Lang_Jenis_Produk_Kolom = dr("Lang_Jenis_Produk_Kolom")
                    Lang_Jenis_Produk_Error_Kode = dr("Lang_Jenis_Produk_Error_Kode")
                    Lang_Jenis_Produk_Error_Keterangan = dr("Lang_Jenis_Produk_Error_Keterangan")
                    Lang_Jenis_Produk_Error_Protein_Min = dr("Lang_Jenis_Produk_Error_Protein_Min")
                    Lang_Jenis_Produk_Error_Protein_Max = dr("Lang_Jenis_Produk_Error_Protein_Max")
                    Lang_Jenis_Produk_Error_Lemak_Min = dr("Lang_Jenis_Produk_Error_Lemak_Min")
                    Lang_Jenis_Produk_Error_Lemak_Max = dr("Lang_Jenis_Produk_Error_Lemak_Max")
                    Lang_Jenis_Produk_Error_Catatan = dr("Lang_Jenis_Produk_Error_Catatan")
                ElseIf jenis = "Master_Jenis_Kemasan_Utama" Then
                    Lang_Jenis_Kemasan_Utama_Judul = dr("Lang_Jenis_Kemasan_Utama_Judul")
                    Lang_Jenis_Kemasan_Utama_Jenis_Produk = dr("Lang_Jenis_Kemasan_Utama_Jenis_Produk")
                    Lang_Jenis_Kemasan_Utama_Kode = dr("Lang_Jenis_Kemasan_Utama_Kode")
                    Lang_Jenis_Kemasan_Utama_Keterangan = dr("Lang_Jenis_Kemasan_Utama_Keterangan")
                    Lang_Jenis_Kemasan_Utama_Kolom = dr("Lang_Jenis_Kemasan_Utama_Kolom")
                    Lang_Jenis_Kemasan_Utama_Error_Jenis_Produk = dr("Lang_Jenis_Kemasan_Utama_Error_Jenis_Produk")
                    Lang_Jenis_Kemasan_Utama_Error_Kode = dr("Lang_Jenis_Kemasan_Utama_Error_Kode")
                    Lang_Jenis_Kemasan_Utama_Error_Keterangan = dr("Lang_Jenis_Kemasan_Utama_Error_Keterangan")
                    Lang_Jenis_Kemasan_Utama_Prefix = dr("Lang_Jenis_Kemasan_Utama_Prefix")
                    Lang_Jenis_Kemasan_Utama_Error_Prefix = dr("Lang_Jenis_Kemasan_Utama_Error_Prefix")
                ElseIf jenis = "Master_Bentuk" Then
                    Lang_Bentuk_Judul = dr("Lang_Bentuk_Judul")
                    Lang_Bentuk_Jenis_Produk = dr("Lang_Bentuk_Jenis_Produk")
                    Lang_Bentuk_Kode = dr("Lang_Bentuk_Kode")
                    Lang_Bentuk_Keterangan = dr("Lang_Bentuk_Keterangan")
                    Lang_Bentuk_Kolom = dr("Lang_Bentuk_Kolom")
                    Lang_Bentuk_Error_Jenis_Produk = dr("Lang_Bentuk_Error_Jenis_Produk")
                    Lang_Bentuk_Error_Kode = dr("Lang_Bentuk_Error_Kode")
                    Lang_Bentuk_Error_Keterangan = dr("Lang_Bentuk_Error_Keterangan")
                ElseIf jenis = "Master_Warna" Then
                    Lang_Warna_Judul = dr("Lang_Warna_Judul")
                    Lang_Warna_Jenis_Produk = dr("Lang_Warna_Jenis_Produk")
                    Lang_Warna_Kode = dr("Lang_Warna_Kode")
                    Lang_Warna_Keterangan = dr("Lang_Warna_Keterangan")
                    Lang_Warna_Kolom = dr("Lang_Warna_Kolom")
                    Lang_Warna_Error_Jenis_Produk = dr("Lang_Warna_Error_Jenis_Produk")
                    Lang_Warna_Error_Kode = dr("Lang_Warna_Error_Kode")
                    Lang_Warna_Error_Keterangan = dr("Lang_Warna_Error_Keterangan")
                ElseIf jenis = "Master_Varian" Then
                    Lang_Varian_Judul = dr("Lang_Varian_Judul")
                    Lang_Varian_Jenis_Produk = dr("Lang_Varian_Jenis_Produk")
                    Lang_Varian_Kode = dr("Lang_Varian_Kode")
                    Lang_Varian_Keterangan = dr("Lang_Varian_Keterangan")
                    Lang_Varian_Kolom = dr("Lang_Varian_Kolom")
                    Lang_Varian_Error_Jenis_Produk = dr("Lang_Varian_Error_Jenis_Produk")
                    Lang_Varian_Error_Kode = dr("Lang_Varian_Error_Kode")
                    Lang_Varian_Error_Keterangan = dr("Lang_Varian_Error_Keterangan")
                    Lang_Master_Varian_Jumlah_Max = dr("Lang_Master_Varian_Jumlah_Max")
                    Lang_Varian_Jumlah_Max = dr("Lang_Varian_Jumlah_Max")

                ElseIf jenis = "Master_Label_Kemasan" Then
                    Lang_Label_Kemasan_Judul = dr("Lang_Label_Kemasan_Judul")
                    Lang_Label_Kemasan_Jenis_Produk = dr("Lang_Label_Kemasan_Jenis_Produk")
                    Lang_Label_Kemasan_Kode = dr("Lang_Label_Kemasan_Kode")
                    Lang_Label_Kemasan_Keterangan = dr("Lang_Label_Kemasan_Keterangan")
                    Lang_Label_Kemasan_Kolom = dr("Lang_Label_Kemasan_Kolom")
                    Lang_Label_Kemasan_Error_Jenis_Produk = dr("Lang_Label_Kemasan_Error_Jenis_Produk")
                    Lang_Label_Kemasan_Error_Kode = dr("Lang_Label_Kemasan_Error_Kode")
                    Lang_Label_Kemasan_Error_Keterangan = dr("Lang_Label_Kemasan_Error_Keterangan")
                    Lang_Label_Kemasan_Jenis_Utama = dr("Lang_Label_Kemasan_Jenis_Utama")
                ElseIf jenis = "Master_Stiker_Kemasan_Utama" Then
                    Lang_Stiker_Kemasan_Utama_Judul = dr("Lang_Stiker_Kemasan_Utama_Judul")
                    Lang_Stiker_Kemasan_Utama_Jenis_Produk = dr("Lang_Stiker_Kemasan_Utama_Jenis_Produk")
                    Lang_Stiker_Kemasan_Utama_Kode = dr("Lang_Stiker_Kemasan_Utama_Kode")
                    Lang_Stiker_Kemasan_Utama_Keterangan = dr("Lang_Stiker_Kemasan_Utama_Keterangan")
                    Lang_Stiker_Kemasan_Utama_Kolom = dr("Lang_Stiker_Kemasan_Utama_Kolom")
                    Lang_Stiker_Kemasan_Utama_Error_Jenis_Produk = dr("Lang_Stiker_Kemasan_Utama_Error_Jenis_Produk")
                    Lang_Stiker_Kemasan_Utama_Error_Kode = dr("Lang_Stiker_Kemasan_Utama_Error_Kode")
                    Lang_Stiker_Kemasan_Utama_Error_Keterangan = dr("Lang_Stiker_Kemasan_Utama_Error_Keterangan")
                    Lang_Stiker_Kemasan_Utama_Jenis_Utama = dr("Lang_Stiker_Kemasan_Utama_Jenis_Utama")
                    Lang_Stiker_Kemasan_Utama_Stiker_Utama = dr("Lang_Stiker_Kemasan_Utama_Stiker_Utama")
                ElseIf jenis = "Master_Kapasitas_Kemasan_Utama" Then
                    Lang_Kapasitas_Kemasan_Error_Jenis_Produk = dr("Lang_Kapasitas_Kemasan_Error_Jenis_Produk")
                    Lang_Kapasitas_Kemasan_Error_Keterangan = dr("Lang_Kapasitas_Kemasan_Error_Keterangan")
                    Lang_Kapasitas_Kemasan_Error_Kode = dr("Lang_Kapasitas_Kemasan_Error_Kode")
                    Lang_Kapasitas_Kemasan_Jenis_Produk = dr("Lang_Kapasitas_Kemasan_Jenis_Produk")
                    Lang_Kapasitas_Kemasan_Jenis_Utama = dr("Lang_Kapasitas_Kemasan_Jenis_Utama")
                    Lang_Kapasitas_Kemasan_Judul = dr("Lang_Kapasitas_Kemasan_Judul")
                    Lang_Kapasitas_Kemasan_Keterangan = dr("Lang_Kapasitas_Kemasan_Keterangan")
                    Lang_Kapasitas_Kemasan_Kode = dr("Lang_Kapasitas_Kemasan_Kode")
                    Lang_Kapasitas_Kemasan_Kolom = dr("Lang_Kapasitas_Kemasan_Kolom")
                    Lang_Kapasitas_Kemasan_Error_SatuanTurunan = dr("Lang_Kapasitas_Kemasan_Error_SatuanTurunan")
                ElseIf jenis = "Master_Customer" Then
                    Lang_Customer_Judul = dr("Lang_Customer_Judul")
                    Lang_Customer_Customer = dr("Lang_Customer_Customer")
                    Lang_Customer_Data_Pic = dr("Lang_Customer_Data_Pic")
                    Lang_Customer_Nama_Pic = dr("Lang_Customer_Nama_Pic")
                    Lang_Customer_Jabatan = dr("Lang_Customer_Jabatan")
                    Lang_Customer_Divisi = dr("Lang_Customer_Divisi")
                    Lang_Customer_Hp_Pic = dr("Lang_Customer_Hp_Pic")
                    Lang_Customer_Tipe_Member = dr("Lang_Customer_Tipe_Member")
                    Lang_Customer_Tipe_Pembayaran = dr("Lang_Customer_Tipe_Pembayaran")
                    Lang_Customer_Cara_Bayar = dr("Lang_Customer_Cara_Bayar")
                    Lang_Customer_Ketentuan_Harga = dr("Lang_Customer_Ketentuan_Harga")
                    Lang_Customer_Data_Bisnis = dr("Lang_Customer_Data_Bisnis")
                    Lang_Customer_Jenis_Usaha = dr("Lang_Customer_Jenis_Usaha")
                    Lang_Customer_Nama_Usaha = dr("Lang_Customer_Nama_Usaha")
                    Lang_Customer_Alamat = dr("Lang_Customer_Alamat")
                    Lang_Customer_Tlp = dr("Lang_Customer_Tlp")
                    Lang_Customer_Email = dr("Lang_Customer_Email")
                    Lang_Customer_Status_Pajak = dr("Lang_Customer_Status_Pajak")
                    Lang_Customer_Identitas_WP = dr("Lang_Customer_Identitas_WP")
                    Lang_Customer_Nama_WP = dr("Lang_Customer_Nama_WP")
                    Lang_Customer_Alamat_WP = dr("Lang_Customer_Alamat_WP")
                    Lang_Customer_Nama_Bank = dr("Lang_Customer_Nama_Bank")
                    Lang_Customer_Cabang = dr("Lang_Customer_Cabang")
                    Lang_Customer_No_Rek = dr("Lang_Customer_No_Rek")
                    Lang_Customer_Nama_Nasabah = dr("Lang_Customer_Nama_Nasabah")
                    Lang_Customer_Tipe_Id = dr("Lang_Customer_Tipe_Id")
                    Lang_Customer_No_Id = dr("Lang_Customer_No_Id")
                    Lang_Customer_Nama_Leng = dr("Lang_Customer_Nama_Leng")
                    Lang_Customer_Jenis_Kelamin = dr("Lang_Customer_Jenis_Kelamin")
                    Lang_Customer_Data_Bank = dr("Lang_Customer_Data_Bank")
                    Lang_Customer_Data_Pemilik = dr("Lang_Customer_Data_Pemilik")
                    Lang_Customer_Tambah = dr("Lang_Customer_Tambah")
                    Lang_Customer_Error_Kd_Customer = dr("Lang_Customer_Error_Kd_Customer")
                    Lang_Customer_Judul_Display = dr("Lang_Customer_Judul_Display")
                    Lang_Customer_Kolom = dr("Lang_Customer_Kolom")
                    Lang_Customer_Kd_Customer = dr("Lang_Customer_Kd_Customer")
                    Lang_Customer_Error_Nama_PIC = dr("Lang_Customer_Error_Nama_PIC")
                    Lang_Customer_Error_Jabatan = dr("Lang_Customer_Error_Jabatan")
                    Lang_Customer_Error_Divisi = dr("Lang_Customer_Error_Divisi")
                    Lang_Customer_Error_HP_PIC = dr("Lang_Customer_Error_HP_PIC")
                    Lang_Customer_Error_Tp_Member = dr("Lang_Customer_Error_Tp_Member")
                    Lang_Customer_Error_Tp_Pembayaran = dr("Lang_Customer_Error_Tp_Pembayaran")
                    Lang_Customer_Error_Cara_Bayar = dr("Lang_Customer_Error_Cara_Bayar")
                    Lang_Customer_Error_Ketentuan = dr("Lang_Customer_Error_Ketentuan")
                    Lang_Customer_Nomor_WP = dr("Lang_Customer_Nomor_WP")
                    Lang_Customer_Error_Jenis_Usaha = dr("Lang_Customer_Error_Jenis_Usaha")
                    Lang_Customer_Error_Nama_Usaha = dr("Lang_Customer_Error_Nama_Usaha")
                    Lang_Customer_Error_Alamat = dr("Lang_Customer_Error_Alamat")
                    Lang_Customer_Error_Tlp = dr("Lang_Customer_Error_Tlp")
                    Lang_Customer_Error_Email = dr("Lang_Customer_Error_Email")
                    Lang_Customer_Error_Status_Pajak = dr("Lang_Customer_Error_Status_Pajak")
                    Lang_Customer_Error_Identitas_WP = dr("Lang_Customer_Error_Identitas_WP")
                    Lang_Customer_Error_Nama_WP = dr("Lang_Customer_Error_Nama_WP")
                    Lang_Customer_Error_Alamat_WP = dr("Lang_Customer_Error_Alamat_WP")
                    Lang_Customer_Error_Nomor_WP = dr("Lang_Customer_Error_Nomor_WP")
                    Lang_Customer_Error_Tipe_Id = dr("Lang_Customer_Error_Tipe_Id")
                    Lang_Customer_Error_No_Id = dr("Lang_Customer_Error_No_Id")
                    Lang_Customer_Error_Jenis_Kelamin = dr("Lang_Customer_Error_Jenis_Kelamin")
                    Lang_Customer_Error_Hp = dr("Lang_Customer_Error_Hp")
                    Lang_Customer_Cara_Kirim = dr("Lang_Customer_Cara_Kirim")
                    Lang_Customer_Media_Kirim = dr("Lang_Customer_Media_Kirim")
                    Lang_Customer_Penerima = dr("Lang_Customer_Penerima")
                    Lang_Customer_Alamat_Penerima = dr("Lang_Customer_Alamat_Penerima")
                    Lang_Customer_Kontak_Penerima = dr("Lang_Customer_Kontak_Penerima")
                    Lang_Customer_Provinsi = dr("Lang_Customer_Provinsi")
                    Lang_Customer_Kabupaten_Kota = dr("Lang_Customer_Kabupaten_Kota")
                    Lang_Customer_Kecamatan = dr("Lang_Customer_Kecamatan")
                    Lang_Customer_Kelurahan = dr("Lang_Customer_Kelurahan")
                    Lang_Customer_Prefernsi_Ekspedisi = dr("Lang_Customer_Prefernsi_Ekspedisi")
                    Lang_Customer_Alamat_Ekspedisi = dr("Lang_Customer_Alamat_Ekspedisi")
                    Lang_Customer_Telepon_Ekspedisi = dr("Lang_Customer_Telepon_Ekspedisi")
                    Lang_Customer_Data_Kirim = dr("Lang_Customer_Data_Kirim")
                    Lang_Customer_Error_Cara_Kirim = dr("Lang_Customer_Error_Cara_Kirim")
                    Lang_Customer_Error_Media_Kirim = dr("Lang_Customer_Error_Media_Kirim")
                    Lang_Customer_Error_Penerima = dr("Lang_Customer_Error_Penerima")
                    Lang_Customer_Error_Alamat_Penerima = dr("Lang_Customer_Error_Alamat_Penerima")
                    Lang_Customer_Error_Kontak_Penerima = dr("Lang_Customer_Error_Kontak_Penerima")
                    Lang_Customer_Error_Provinsi = dr("Lang_Customer_Error_Provinsi")
                    Lang_Customer_Error_Kabupaten_Kota = dr("Lang_Customer_Error_Kabupaten_Kota")
                    Lang_Customer_Error_Kecamatan = dr("Lang_Customer_Error_Kecamatan")
                    Lang_Customer_Error_Kelurahan = dr("Lang_Customer_Error_Kelurahan")
                    Lang_Customer_Error_Prefernsi_Ekspedisi = dr("Lang_Customer_Error_Prefernsi_Ekspedisi")
                    Lang_Customer_Error_Alamat_Ekspedisi = dr("Lang_Customer_Error_Alamat_Ekspedisi")
                    Lang_Customer_Error_Telepon_Ekspedisi = dr("Lang_Customer_Error_Telepon_Ekspedisi")
                ElseIf jenis = "Display_Inquiry" Then
                    Lang_Display_Inquiry_No_Faktur = dr("Lang_Display_Inquiry_No_Faktur")
                    Lang_Display_Inquiry_Kd_Customer = dr("Lang_Display_Inquiry_Kd_Customer")
                    Lang_Display_Inquiry_Nama_Usaha = dr("Lang_Display_Inquiry_Nama_Usaha")
                    Lang_Display_Inquiry_Lokasi = dr("Lang_Display_Inquiry_Lokasi")
                    Lang_Display_Inquiry_Tanggal = dr("Lang_Display_Inquiry_Tanggal")
                    Lang_Display_Inquiry_Hari_ini = dr("Lang_Display_Inquiry_Hari_ini")
                    Lang_Display_Inquiry_Para_Tbl = dr("Lang_Display_Inquiry_Para_Tbl")
                    Lang_Display_Inquiry_Para_lain = dr("Lang_Display_Inquiry_Para_lain")
                    Lang_Display_Inquiry_Jns_Hewan = dr("Lang_Display_Inquiry_Jns_Hewan")
                    Lang_Display_Inquiry_Jns_Umur = dr("Lang_Display_Inquiry_Jns_Umur")
                    Lang_Display_Inquiry_Jns_Produk = dr("Lang_Display_Inquiry_Jns_Produk")
                    Lang_Display_Inquiry_Jns_Varian = dr("Lang_Display_Inquiry_Jns_Varian")
                    Lang_Display_Inquiry_Jns_Komposisi = dr("Lang_Display_Inquiry_Jns_Komposisi")
                    Lang_Display_Inquiry_Protein = dr("Lang_Display_Inquiry_Protein")
                    Lang_Display_Inquiry_Lemak = dr("Lang_Display_Inquiry_Lemak")
                    Lang_Display_Inquiry_Kelembapan = dr("Lang_Display_Inquiry_Kelembapan")
                    Lang_Display_Inquiry_Catatan = dr("Lang_Display_Inquiry_Catatan")
                    Lang_Display_Inquiry_Kd_barang = dr("Lang_Display_Inquiry_Kd_barang")
                    Lang_Display_Inquiry_Nm_Barang = dr("Lang_Display_Inquiry_Nm_Barang")
                    Lang_Display_Inquiry_Error_Batal = dr("Lang_Display_Inquiry_Error_Batal")
                    Lang_Display_Inquiry_Error_Tolak_Batal = dr("Lang_Display_Inquiry_Error_Tolak_Batal")
                    Lang_Display_Inquiry_Error_Tolak_Batal2 = dr("Lang_Display_Inquiry_Error_Tolak_Batal2")
                    lang_display_inquiry_Judul = dr("lang_display_inquiry_Judul")
                ElseIf jenis = "Master_Karyawan" Then
                    Lang_Karywan_Judul = dr("Lang_Karywan_Judul")
                    Lang_Karywan_Divisi = dr("Lang_Karywan_Divisi")
                    Lang_Karywan_Kode = dr("Lang_Karywan_Kode")
                    Lang_Karywan_Nama = dr("Lang_Karywan_Nama")
                    Lang_Karywan_Jabatan = dr("Lang_Karywan_Jabatan")
                    Lang_Karywan_Kolom = dr("Lang_Karywan_Kolom")
                    Lang_Karywan_Error_Divisi = dr("Lang_Karywan_Error_Divisi")
                    Lang_Karywan_Error_Kode = dr("Lang_Karywan_Error_Kode")
                    Lang_Karywan_Error_Nama = dr("Lang_Karywan_Error_Nama")
                    Lang_Karywan_Error_Jabatan = dr("Lang_Karywan_Error_Jabatan")
                ElseIf jenis = "Transaksi_Formulator" Then

                    Lang_TransFormula_Judul = dr("Lang_TransFormula_Judul")
                    Lang_TransFormula_Customer = dr("Lang_TransFormula_Customer")
                    Lang_TransFormula_KdBarang = dr("Lang_TransFormula_KdBarang")
                    Lang_TransFormula_NmBarang = dr("Lang_TransFormula_NmBarang")
                    Lang_TransFormula_NoFaktur = dr("Lang_TransFormula_NoFaktur")
                    Lang_TransFormula_NoInquiry = dr("Lang_TransFormula_NoInquiry")
                    Lang_TransFormula_PenanggungJawab = dr("Lang_TransFormula_PenanggungJawab")
                    Lang_TransFormula_Sample = dr("Lang_TransFormula_Sample")
                    Lang_TransFormula_Tanggal = dr("Lang_TransFormula_Tanggal")
                    Lang_TransFormula_DgvStepFormula_No = dr("Lang_TransFormula_DgvStepFormula_No")
                    Lang_TransFormula_DgvStepFormula_Tipe = dr("Lang_TransFormula_DgvStepFormula_Tipe")
                    Lang_TransFormula_DgvStepFormula_KdBarang = dr("Lang_TransFormula_DgvStepFormula_KdBarang")
                    Lang_TransFormula_DgvStepFormula_Nama = dr("Lang_TransFormula_DgvStepFormula_Nama")
                    Lang_TransFormula_DgvStepFormula_Qty = dr("Lang_TransFormula_DgvStepFormula_Qty")
                    Lang_TransFormula_DgvStepFormula_Satuan = dr("Lang_TransFormula_DgvStepFormula_Satuan")
                    Lang_TransFormula_DgvStepFormula_Persentase = dr("Lang_TransFormula_DgvStepFormula_Persentase")
                    Lang_TransFormula_DgvStepFormula_Keterangan = dr("Lang_TransFormula_DgvStepFormula_Keterangan")
                ElseIf jenis = "Tampil_Inquiry" Then


                    Lang_TampilInquiry_Judul = dr("Lang_TampilInquiry_Judul")
                    Lang_TampilInquiry_Customer = dr("Lang_TampilInquiry_Customer")
                    Lang_TampilInquiry_Lokasi = dr("Lang_TampilInquiry_DgvDataInquiry_Lokasi")
                    Lang_TampilInquiry_DgvDataInquiry_NoInquiry = dr("Lang_TampilInquiry_DgvDataInquiry_NoInquiry")
                    Lang_TampilInquiry_DgvDataInquiry_Lokasi = dr("Lang_TampilInquiry_DgvDataInquiry_Lokasi")
                    Lang_TampilInquiry_DgvDataInquiry_Tanggal = dr("Lang_TampilInquiry_DgvDataInquiry_Tanggal")
                    Lang_TampilInquiry_DgvDataInquiry_KdCustomer = dr("Lang_TampilInquiry_DgvDataInquiry_KdCustomer")
                    Lang_TampilInquiry_DgvDataInquiry_NamaCustomer = dr("Lang_TampilInquiry_DgvDataInquiry_NamaCustomer")
                ElseIf jenis = "Tampil_InquiryBarang" Then
                    Lang_TampilInquiryBarang_Judul = dr("Lang_TampilInquiryBarang_Judul")

                ElseIf jenis = "Tampil_Barang" Then
                    Lang_TampilBarang_Judul = dr("Lang_TampilBarang_Judul")

                ElseIf jenis = "QC_Formula" Then
                    Lang_QC_Formula_Judul = dr("Lang_QC_Formula_Judul")
                    Lang_QC_Formula_Judul_QC = dr("Lang_QC_Formula_Judul_QC")
                    Lang_QC_Formula_Tanggal = dr("Lang_QC_Formula_Tanggal")
                    Lang_QC_Formula_No_Transaksi = dr("Lang_QC_Formula_No_Transaksi")
                    Lang_QC_Formula_Penanggung_Jawab = dr("Lang_QC_Formula_Penanggung_Jawab")
                    Lang_QC_Formula_Kode_Formula = dr("Lang_QC_Formula_Kode_Formula")
                    Lang_QC_Formula_Nama_Barang = dr("Lang_QC_Formula_Nama_Barang")
                    Lang_QC_Formula_Note1 = dr("Lang_QC_Formula_Note1")
                    Lang_QC_Formula_Note2 = dr("Lang_QC_Formula_Note2")
                    Lang_QC_Formula_Catatan = dr("Lang_QC_Formula_Catatan")
                    Lang_QC_Formula_Hasil_Uji = dr("Lang_QC_Formula_Hasil_Uji")
                    Lang_QC_Formula_Kode_Uji_DGV = dr("Lang_QC_Formula_Kode_Uji_DGV")
                    Lang_QC_Formula_Deskripsi_DGV = dr("Lang_QC_Formula_Deskripsi_DGV")
                    Lang_QC_Formula_Satuan_DGV = dr("Lang_QC_Formula_Satuan_DGV")
                    Lang_QC_Formula_Target_DGV = dr("Lang_QC_Formula_Target_DGV")
                    Lang_QC_Formula_Hasil_DGV = dr("Lang_QC_Formula_Hasil_DGV")
                    Lang_QC_Formula_Standar_Kontrol_DGV = dr("Lang_QC_Formula_Standar_Kontrol_DGV")
                    Lang_QC_Formula_Pass_DGV = dr("Lang_QC_Formula_Pass_DGV")
                    Lang_QC_Formula_Tanggal_Uji_DGV = dr("Lang_QC_Formula_Tanggal_Uji_DGV")
                    Lang_QC_Formula_Waktu_Uji_DGV = dr("Lang_QC_Formula_Waktu_Uji_DGV")
                    Lang_QC_Formula_Qc_required = dr("Lang_QC_Formula_Qc_required")
                    Lang_QC_Formula_required = dr("Lang_QC_Formula_required")
                    Lang_QC_Formula_kode_uji_required = dr("Lang_QC_Formula_kode_uji_required")
                    Lang_QC_Formula_Penanggung_jawab_req = dr("Lang_QC_Formula_Penanggung_jawab_req")
                    Lang_QC_Formula_No_Inquiry_required = dr("Lang_QC_Formula_No_Inquiry_required")
                    Lang_QC_Formula_hasil_qc_required = dr("Lang_QC_Formula_hasil_qc_required")
                    Lang_QC_Formula_Standar_Kontrol_req = dr("Lang_QC_Formula_Standar_Kontrol_req")
                    Lang_QC_Formula_Tanggal_Uji_required = dr("Lang_QC_Formula_Tanggal_Uji_required")
                    Lang_QC_Formula_Waktu_Uji_required = dr("Lang_QC_Formula_Waktu_Uji_required")
                    Lang_QC_Formula_Qc_Hapus = dr("Lang_QC_Formula_Qc_Hapus")
                ElseIf jenis = "Transaksi_Binding_Formula" Then
                    Lang_TransFormulaBinding_Judul = dr("Lang_TransFormulaBinding_Judul")
                    Lang_TransFormulaBinding_DGV_KodeProduk = dr("Lang_TransFormulaBinding_DGV_KodeProduk")
                    Lang_TransFormulaBinding_DGV_NamaProduk = dr("Lang_TransFormulaBinding_DGV_NamaProduk")
                    Lang_TransFormulaBinding_DGV_BindingFormula = dr("Lang_TransFormulaBinding_DGV_BindingFormula")
                ElseIf jenis = "Display_Transaksi_Formula" Then
                    Lang_Display_Transaksi_Formula = dr("Lang_Display_Transaksi_Formula")
                    Lang_Display_Transaksi_Formula_No_Faktur = dr("Lang_Display_Transaksi_Formula_No_Faktur")
                    Lang_Display_Transaksi_Formula_No_Inquiry = dr("Lang_Display_Transaksi_Formula_No_Inquiry")
                    Lang_Display_Transaksi_Formula_Kd_Cusotmer = dr("Lang_Display_Transaksi_Formula_Kd_Cusotmer")
                    Lang_Display_Transaksi_Formula_Nama_Usaha = dr("Lang_Display_Transaksi_Formula_Nama_Usaha")
                    Lang_Display_Transaksi_Formula_Lokasi = dr("Lang_Display_Transaksi_Formula_Lokasi")
                    Lang_Display_Transaksi_Formula_Kd_barang = dr("Lang_Display_Transaksi_Formula_Kd_barang")
                    Lang_Display_Transaksi_Formula_Nama_barang = dr("Lang_Display_Transaksi_Formula_Nama_barang")
                    Lang_Display_Transaksi_Formula_Tgl = dr("Lang_Display_Transaksi_Formula_Tgl")
                    Lang_Display_Transaksi_Formula_Penanggung = dr("Lang_Display_Transaksi_Formula_Penanggung")
                    Lang_Display_Transaksi_Formula_Detail_Step = dr("Lang_Display_Transaksi_Formula_Detail_Step")
                    Lang_Display_Transaksi_Formula_Komposisi = dr("Lang_Display_Transaksi_Formula_Komposisi")
                    Lang_Display_Transaksi_Formula_No_Step = dr("Lang_Display_Transaksi_Formula_No_Step")
                    Lang_Display_Transaksi_Formula_Tipe = dr("Lang_Display_Transaksi_Formula_Tipe")
                    Lang_Display_Transaksi_Formula_Kode = dr("Lang_Display_Transaksi_Formula_Kode")
                    Lang_Display_Transaksi_Formula_Deskripsi = dr("Lang_Display_Transaksi_Formula_Deskripsi")
                    Lang_Display_Transaksi_Formula_Jumlah = dr("Lang_Display_Transaksi_Formula_Jumlah")
                    Lang_Display_Transaksi_Formula_Satuan = dr("Lang_Display_Transaksi_Formula_Satuan")
                    Lang_Display_Transaksi_Formula_Presentase = dr("Lang_Display_Transaksi_Formula_Presentase")
                    Lang_Display_Transaksi_Formula_Error_Tolak_Batal = dr("Lang_Display_Transaksi_Formula_Error_Tolak_Batal")
                    Lang_Display_Transaksi_Formula_Error_Batal = dr("Lang_Display_Transaksi_Formula_Error_Batal")
                    Lang_Display_Transaksi_Formula_Error_Tolak_Batal2 = dr("Lang_Display_Transaksi_Formula_Error_Tolak_Batal2")
                ElseIf jenis = "Display_Penawaran" Then
                    Lang_Display_Penawaran_Provinsi_Awal = dr("Lang_Display_Penawaran_Provinsi_Awal")
                    Lang_Display_Penawaran_Provinsi_Tujuan = dr("Lang_Display_Penawaran_Provinsi_Tujuan")
                    Lang_Display_Penawaran_Judul = dr("Lang_Display_Penawaran_Judul")
                    Lang_Display_Penawaran_Media_Kirim = dr("Lang_Display_Penawaran_Media_Kirim")
                    Lang_Display_Penawaran_Kota_Asal = dr("Lang_Display_Penawaran_Kota_Asal")
                    Lang_Display_Penawaran_Kota_Tujuan = dr("Lang_Display_Penawaran_Kota_Tujuan")
                    Lang_Display_Penawaran_Kecamatan_Asal = dr("Lang_Display_Penawaran_Kecamatan_Asal")
                    Lang_Display_Penawaran_Kecamatan_Tujuan = dr("Lang_Display_Penawaran_Kecamatan_Tujuan")
                    Lang_Display_Penawaran_Kelurahan_Asal = dr("Lang_Display_Penawaran_Kelurahan_Asal")
                    Lang_Display_Penawaran_Kelurahan_Tujuan = dr("Lang_Display_Penawaran_Kelurahan_Tujuan")
                ElseIf jenis = "Transaksi_Validasi_Harga_Jual" Then
                    Lang_Transaksi_Validasi_Harga_Jual_Judul = dr("Lang_Transaksi_Validasi_Harga_Jual_Judul")
                    Lang_Transaksi_Validasi_Harga_Jual_No_trasaksi = dr("Lang_Transaksi_Validasi_Harga_Jual_No_trasaksi")
                    Lang_Transaksi_Validasi_Harga_Jual_Tanggal = dr("Lang_Transaksi_Validasi_Harga_Jual_Tanggal")
                    Lang_Transaksi_Validasi_Harga_Jual_No_inquiry = dr("Lang_Transaksi_Validasi_Harga_Jual_No_inquiry")
                    Lang_Transaksi_Validasi_Harga_Jual_Customer = dr("Lang_Transaksi_Validasi_Harga_Jual_Customer")
                    Lang_Transaksi_Validasi_Harga_Jual_Pilih_Brg = dr("Lang_Transaksi_Validasi_Harga_Jual_Pilih_Brg")
                    Lang_Transaksi_Validasi_Harga_Jual_Kode_Brg = dr("Lang_Transaksi_Validasi_Harga_Jual_Kode_Brg")
                    Lang_Transaksi_Validasi_Harga_Jual_Nama_Brg = dr("Lang_Transaksi_Validasi_Harga_Jual_Nama_Brg")
                    Lang_Transaksi_Validasi_Harga_Jual_Gudang = dr("Lang_Transaksi_Validasi_Harga_Jual_Gudang")
                    Lang_Transaksi_Validasi_Harga_Jual_Harga_Satuan = dr("Lang_Transaksi_Validasi_Harga_Jual_Harga_Satuan")
                    Lang_Transaksi_Validasi_Harga_Jual_Est_Serapan = dr("Lang_Transaksi_Validasi_Harga_Jual_Est_Serapan")
                    Lang_Transaksi_Validasi_Harga_Jual_Mark_Up = dr("Lang_Transaksi_Validasi_Harga_Jual_Mark_Up")
                    Lang_Transaksi_Validasi_Harga_Jual_Harga_Jual = dr("Lang_Transaksi_Validasi_Harga_Jual_Harga_Jual")
                    Lang_Transaksi_Validasi_Harga_Jual_Total_Jual = dr("Lang_Transaksi_Validasi_Harga_Jual_Total_Jual")
                    Lang_Transaksi_Validasi_Harga_Jual_Mark_Up_Fixed = dr("Lang_Transaksi_Validasi_Harga_Jual_Mark_Up_Fixed")
                    Lang_Transaksi_Validasi_Harga_Jual_Hrg_Jual_Fixed = dr("Lang_Transaksi_Validasi_Harga_Jual_Hrg_Jual_Fixed")
                    Lang_Transaksi_Validasi_Harga_Jual_Ttl_Jual_Fixed = dr("Lang_Transaksi_Validasi_Harga_Jual_Ttl_Jual_Fixed")
                    Lang_Transaksi_Validasi_Harga_Jual_Gdg_Asal = dr("Lang_Transaksi_Validasi_Harga_Jual_Gdg_Asal")
                    Lang_Transaksi_Validasi_Harga_Jual_Gdg_Tujuan = dr("Lang_Transaksi_Validasi_Harga_Jual_Gdg_Tujuan")
                    Lang_Transaksi_Validasi_Harga_Jual_Err_Mark_Up = dr("Lang_Transaksi_Validasi_Harga_Jual_Err_Mark_Up")
                    Lang_Transaksi_Validasi_Harga_Jual_Hrg_Jual = dr("Lang_Transaksi_Validasi_Harga_Jual_Hrg_Jual")
                    Lang_Transaksi_Validasi_Harga_Jual_Ttl_Jual = dr("Lang_Transaksi_Validasi_Harga_Jual_Ttl_Jual")
                    Lang_Transaksi_Validasi_Harga_Jual_Err_Selisih = dr("Lang_Transaksi_Validasi_Harga_Jual_Err_Selisih")
                ElseIf jenis = "Lokasi_PO" Then
                    Lang_Lokasi_PO_Judul = dr("Lang_Lokasi_PO_Judul")
                    Lang_Lokasi_PO_Cari_PO = dr("Lang_Lokasi_PO_Cari_PO")
                    Lang_Lokasi_PO_Gudang_Tujuan = dr("Lang_Lokasi_PO_Gudang_Tujuan")
                    Lang_Lokasi_PO_Judul_Display = dr("Lang_Lokasi_PO_Judul_Display")
                    Lang_Lokasi_PO_Kolom = dr("Lang_Lokasi_PO_Kolom")
                    Lang_Lokasi_PO_Error_Banding_Jmlh = dr("Lang_Lokasi_PO_Error_Banding_Jmlh")
                    Lang_Lokasi_PO_Error_Kode_Brg_Lv = dr("Lang_Lokasi_PO_Error_Kode_Brg_Lv")
                    Lang_Lokasi_PO_Error_Banding_Jmlh2 = dr("Lang_Lokasi_PO_Error_Banding_Jmlh2")
                ElseIf jenis = "Master_MediaKirim" Then
                    Lang_MediaKirim_Judul = dr("Lang_MediaKirim_Judul")
                    Lang_MediaKirim_KodeMediaKirim = dr("Lang_MediaKirim_KodeMediaKirim")
                ElseIf jenis = "Master_Ekspedisi" Then
                    Lang_Ekspedisi_Judul = dr("Lang_Ekspedisi_Judul")
                    Lang_Ekspedisi_KodeEkspedisi = dr("Lang_Ekspedisi_KodeEkspedisi")
                    Lang_Ekspedisi_NamaEkspedisi = dr("Lang_Ekspedisi_NamaEkspedisi")
                    Lang_Ekspedisi_AlamatEkspedisi = dr("Lang_Ekspedisi_AlamatEkspedisi")
                    Lang_Ekspedisi_TeleponEkspedisi = dr("Lang_Ekspedisi_TeleponEkspedisi")
                    Lang_Ekspedisi_PenanggungJawab = dr("Lang_Ekspedisi_PenanggungJawab")
                    Lang_Ekspedisi_Pembayaran = dr("Lang_Ekspedisi_Pembayaran")
                    Lang_Ekspedisi_GolonganPPH = dr("Lang_Ekspedisi_GolonganPPH")
                    Lang_Ekspedisi_NilaiPPH = dr("Lang_Ekspedisi_NilaiPPH")
                ElseIf jenis = "Display_Transaksi_PO_Pembelian" Then
                    Lang_Display_Transaksi_PO_Pembelian_Judul = dr("Lang_Display_Transaksi_PO_Pembelian_Judul")
                ElseIf jenis = "Display_Transaksi_Pembelian" Then
                    Lang_Display_Transaksi_Pembelian = dr("Lang_Display_Transaksi_Pembelian")
                ElseIf jenis = "Display_Transaksi_PO_Desktop" Then
                    Lang_Display_Transaksi_PO_Desktop_Judul = dr("Lang_Display_Transaksi_PO_Desktop_Judul")
                ElseIf jenis = "Prepare_Bahan" Then
                    Lang_Prepare_Bahan_Label_Jumlah_Stok = dr("Lang_Prepare_Bahan_Label_Jumlah_Stok")
                    Lang_Prepare_Bahan_Label_Jumlah_Butuh = dr("Lang_Prepare_Bahan_Label_Jumlah_Butuh")
                    Lang_Prepare_Bahan_Label_Harus_Order = dr("Lang_Prepare_Bahan_Label_Harus_Order")
                    Lang_Prepare_Bahan_Label_NoPenawaran = dr("Lang_Prepare_Bahan_Label_NoPenawaran")
                    Lang_Prepare_Bahan_Error_StokNegatif1 = dr("Lang_Prepare_Bahan_Error_StokNegatif1")
                    Lang_Prepare_Bahan_Error_StokNegatif2 = dr("Lang_Prepare_Bahan_Error_StokNegatif2")
                    Lang_Prepare_Bahan_BrgTdkDitemukan = dr("Lang_Prepare_Bahan_BrgTdkDitemukan")
                    Lang_Prepare_Bahan_Judul = dr("Lang_Prepare_Bahan_Judul")
                ElseIf jenis = "Pembelian_Barang_Masuk" Then
                    Lang_Pmb_Barang_masuk_No_Plat = dr("Lang_Pmb_Barang_masuk_No_Plat")
                    Lang_Pmb_Barang_Masuk_Tanggal_Bongkar = dr("Lang_Pmb_Barang_Masuk_Tanggal_Bongkar")
                    Lang_Pmb_Barang_Masuk_Judul = dr("Lang_Pmb_Barang_Masuk_Judul")
                    Lang_Pmb_Barang_Masuk_SD_Judul = dr("Lang_Pmb_Barang_Masuk_SD_Judul")
                    Lang_Pmb_Barang_Masuk_Tanggal_Expire = dr("Lang_Pmb_Barang_Masuk_Tanggal_Expire")
                    Lang_Pmb_Barang_Masuk_SD_Judul_Pili_PO = dr("Lang_Pmb_Barang_Masuk_SD_Judul_Pili_PO")
                    Lang_Pmb_Barang_Masuk_Error_Faktur = dr("Lang_Pmb_Barang_Masuk_Error_Faktur")
                    Lang_Pmb_Barang_Masuk_Error_Nota = dr("Lang_Pmb_Barang_Masuk_Error_Nota")
                    Lang_Pmb_Barang_Masuk_Error_Plat = dr("Lang_Pmb_Barang_Masuk_Error_Plat")
                    Lang_Pmb_Barang_Masuk_Error_PO = dr("Lang_Pmb_Barang_Masuk_Error_PO")
                    Lang_Pmb_Barang_Masuk_Tanggal_produksi = dr("Lang_Pmb_Barang_Masuk_Tanggal_produksi")
                ElseIf jenis = "Simulasi_HPP" Then
                    Lang_SimulasiHPP_Judul = dr("Lang_SimulasiHPP_Judul")
                    Lang_DetailSimulasiHPP_Judul_BB = dr("Lang_DetailSimulasiHPP_Judul_BB")
                    Lang_DetailSimulasiHPP_Judul_BP = dr("Lang_DetailSimulasiHPP_Judul_BP")
                    Lang_DetailSimulasiHPP_Judul_BK = dr("Lang_DetailSimulasiHPP_Judul_BK")
                    Lang_DetailSimulasiHPP_Judul_BLL = dr("Lang_DetailSimulasiHPP_Judul_BLL")
                    Lang_DetailSimulasiHPP_Validasi1 = dr("Lang_DetailSimulasiHPP_Validasi1")
                    Lang_DetailSimulasiHPP_Validasi2 = dr("Lang_DetailSimulasiHPP_Validasi2")
                    Lang_DetailSimulasiHPP_Validasi3 = dr("Lang_DetailSimulasiHPP_Validasi3")
                ElseIf jenis = "Validasi_Barang_Masuk" Then
                    Lang_Validasi_Barang_Masuk_Judul = dr("Lang_Validasi_Barang_Masuk_Judul")
                    Lang_Validasi_Barang_Masuk_Nomor_Plat = dr("Lang_Validasi_Barang_Masuk_Nomor_Plat")
                    Lang_Validasi_Barang_Masuk_Tgl_bongkar = dr("Lang_Validasi_Barang_Masuk_Tgl_bongkar")
                    Lang_Validasi_Barang_Masuk_Tgl_Produksi = dr("Lang_Validasi_Barang_Masuk_Tgl_Produksi")
                    Lang_Validasi_Barang_Masuk_Tgl_Expired = dr("Lang_Validasi_Barang_Masuk_Tgl_Expired")
                    Lang_Validasi_Barang_Masuk_Tny_Val = dr("Lang_Validasi_Barang_Masuk_Tny_Val")
                    Lang_Validasi_Barang_Masuk_Tny_Val = dr("Lang_Validasi_Barang_Masuk_Tny_Val")
                    Lang_Validasi_Barang_Masuk_Error1 = dr("Lang_Validasi_Barang_Masuk_Error1")
                    Lang_Validasi_Barang_Masuk_Error2 = dr("Lang_Validasi_Barang_Masuk_Error2")
                    Lang_Validasi_Barang_Masuk_Error3 = dr("Lang_Validasi_Barang_Masuk_Error3")
                    Lang_Validasi_Barang_Masuk_Error4 = dr("Lang_Validasi_Barang_Masuk_Error4")
                    Lang_Validasi_Barang_Masuk_Error5 = dr("Lang_Validasi_Barang_Masuk_Error5")
                    Lang_Validasi_Barang_Masuk_Error6 = dr("Lang_Validasi_Barang_Masuk_Error6")
                    Lang_Validasi_Barang_Masuk_Error7 = dr("Lang_Validasi_Barang_Masuk_Error7")
                    Lang_Validasi_Barang_Masuk_Error8 = dr("Lang_Validasi_Barang_Masuk_Error8")
                    Lang_Validasi_Barang_Masuk_Error9 = dr("Lang_Validasi_Barang_Masuk_Error9")
                    Lang_Validasi_Barang_Masuk_Tdk_Ditemu = dr("Lang_Validasi_Barang_Masuk_Tdk_Ditemu")
                    Lang_Validasi_Barang_Masuk_Data_Tdk_Ditemu = dr("Lang_Validasi_Barang_Masuk_Data_Tdk_Ditemu")
                    Lang_Validasi_Barang_Masuk_Berbeda = dr("Lang_Validasi_Barang_Masuk_Berbeda")
                    Lang_Validasi_Barang_Masuk_Jadi_Salah = dr("Lang_Validasi_Barang_Masuk_Jadi_Salah")
                ElseIf jenis = "Transaksi_Timbang_Kosong" Then
                    Lang_TransUnloading_Judul = dr("Lang_TransUnloading_Judul")
                ElseIf jenis = "Transaksi_Timbang_Kosong_PO" Then
                    Lang_TransUnloadingPO_Judul = dr("Lang_TransUnloadingPO_Judul")
                ElseIf jenis = "Pembelian" Then
                    Lang_Pembelian_Judul = dr("Lang_Pembelian_Judul")
                    Lang_Pembelian_No_SO = dr("Lang_Pembelian_No_SO")
                    Lang_Pembelian_Cari_PO = dr("Lang_Pembelian_Cari_PO")
                    Lang_Pembelian_Kd_Bahan = dr("Lang_Pembelian_Kd_Bahan")
                    Lang_Pembelian_Nm_Bahan = dr("Lang_Pembelian_Nm_Bahan")
                    Lang_Pembelian_No_Seri = dr("Lang_Pembelian_No_Seri")
                    Lang_Pembelian_Harga = dr("Lang_Pembelian_Harga")
                    Lang_Pembelian_Jumlah = dr("Lang_Pembelian_Jumlah")
                    Lang_Pembelian_Satuan = dr("Lang_Pembelian_Satuan")
                    Lang_Pembelian_Total = dr("Lang_Pembelian_Total")
                    Lang_Pembelian_Sisa = dr("Lang_Pembelian_Sisa")
                    Lang_Pembelian_Total_Harga = dr("Lang_Pembelian_Total_Harga")
                ElseIf jenis = "QC_BAHAN" Then
                    Lang_QC_Bahan_Judul_Form = dr("Lang_QC_Bahan_Judul_Form")
                    Lang_QC_Bahan_Nm_Ekspedisi = dr("Lang_QC_Bahan_Nm_Ekspedisi")
                    Lang_QC_Bahan_Judul_Form2 = dr("Lang_QC_Bahan_Judul_Form2")
                    Lang_QC_Bahan_Err_No_SJ = dr("Lang_QC_Bahan_Err_No_SJ")
                    Lang_QC_Bahan_Err_Penaggung = dr("Lang_QC_Bahan_Err_Penaggung")
                    Lang_QC_Bahan_Err_QC_Bahan = dr("Lang_QC_Bahan_Err_QC_Bahan")
                    Lang_QC_Bahan_Err_Save_QC = dr("Lang_QC_Bahan_Err_Save_QC")
                    Lang_QC_Bahan_Err_Save_QC = dr("Lang_QC_Bahan_Err_Save_QC")
                    Lang_QC_Bahan_Judul3 = dr("Lang_QC_Bahan_Judul3")
                    Lang_QC_Bahan_Err_Hasil_Qc = dr("Lang_QC_Bahan_Err_Hasil_Qc")
                ElseIf jenis = "Selisih_BM" Then
                    Lang_Selisih_BM_Jenis_Selisih = dr("Lang_Selisih_BM_Jenis_Selisih")
                    Lang_Selisih_BM_Judul = dr("Lang_Selisih_BM_Judul")
                    Lang_Selisih_BM_Total_Selisih_QTY = dr("Lang_Selisih_BM_Total_Selisih_QTY")
                    Lang_Selisih_BM_Total_Selisih_Rp = dr("Lang_Selisih_BM_Total_Selisih_Rp")
                    Lang_Selisih_BM_Err_Jenis = dr("Lang_Selisih_BM_Err_Jenis")
                    Lang_Selisih_BM_Err_KdSupp = dr("Lang_Selisih_BM_Err_KdSupp")
                    Lang_Selisih_BM_Err_No_PO = dr("Lang_Selisih_BM_Err_No_PO")
                    Lang_Selisih_BM_Err_Brng_Msk = dr("Lang_Selisih_BM_Err_Brng_Msk")
                    Lang_Selisih_BM_Err_Data_Kosong = dr("Lang_Selisih_BM_Err_Data_Kosong")
                    Lang_Selisih_BM_Jumlah_PL = dr("Lang_Selisih_BM_Jumlah_PL")
                    Lang_Selisih_BM_Jumlah_BM = dr("Lang_Selisih_BM_Jumlah_BM")
                    Lang_Selisih_BM_Selisih = dr("Lang_Selisih_BM_Selisih")
                    Lang_Selisih_BM_Penyelesaian_Plus = dr("Lang_Selisih_BM_Penyelesaian_Plus")
                    Lang_Selisih_BM_Penyelesaian_Min = dr("Lang_Selisih_BM_Penyelesaian_Min")
                    Lang_Selisih_BM_Selisih_Fix = dr("Lang_Selisih_BM_Selisih_Fix")
                    Lang_Selisih_BM_Selisih_Rp = dr("Lang_Selisih_BM_Selisih_Rp")
                    Lang_Selisih_BM_Harga_Per_PCS = dr("Lang_Selisih_BM_Harga_Per_PCS")
                    Lang_Selisih_BM_Err_only_one_cell = dr("Lang_Selisih_BM_Err_only_one_cell")
                ElseIf jenis = "Master_Jenis_Kategori_Harga" Then
                    Lang_JenisKategoriHarga_Judul = dr("Lang_JenisKategoriHarga_Judul")
                    Lang_JenisKategoriHarga_Kode = dr("Lang_JenisKategoriHarga_Kode")
                    Lang_JenisKategoriHarga_Nama = dr("Lang_JenisKategoriHarga_Nama")
                ElseIf jenis = "Master_Jenis_Member_Perbarang" Then
                    Lang_JenisMemberPerbarang_Judul = dr("Lang_JenisMemberPerbarang_Judul")
                    Lang_JenisMemberPerbarang_Persen = dr("Lang_JenisMemberPerbarang_Persen")
                ElseIf jenis = "Set_Gudang" Then
                    Lang_SetGudang_KodeArea = dr("Lang_SetGudang_KodeArea")
                    Lang_SetGudang_JenisGudang = dr("Lang_SetGudang_JenisGudang")
                    Lang_SetGudang_KodeKolom = dr("Lang_SetGudang_KodeKolom")
                    Lang_SetGudang_KodeLevel = dr("Lang_SetGudang_KodeLevel")
                    Lang_SetGudang_KodeBaris = dr("Lang_SetGudang_KodeBaris")
                    Lang_SetGudang_KodeUkuran = dr("Lang_SetGudang_KodeUkuran")
                    Lang_SetGudang_LevelUkuran = dr("Lang_SetGudang_LevelUkuran")
                    Lang_SetGudang_KodePosisi = dr("Lang_SetGudang_KodePosisi")
                    Lang_SetGudang_KodePalet = dr("Lang_SetGudang_KodePalet")
                    Lang_SetGudang_UkuranPalet = dr("Lang_SetGudang_UkuranPalet")
                    Lang_SetGudang_Judul = dr("Lang_SetGudang_Judul")

                    Lang_SetGudang_Susunan = dr("Lang_SetGudang_Susunan")
                    Lang_SetGudang_Palet = dr("Lang_SetGudang_Palet")
                    Lang_SetGudang_SetGudang = dr("Lang_SetGudang_SetGudang")
                    Lang_SetGudang_Position = dr("Lang_SetGudang_Position")
                    Lang_SetGudang_LevelSize = dr("Lang_SetGudang_LevelSize")
                    Lang_SetGudang_Level = dr("Lang_SetGudang_Level")
                    Lang_SetGudang_Kolom = dr("Lang_SetGudang_Kolom")
                    Lang_SetGudang_Area = dr("Lang_SetGudang_Area")
                    Lang_SetGudang_Baris = dr("Lang_SetGudang_Baris")
                    Lang_SetGudang_KodeSusunan = dr("Lang_SetGudang_KodeSusunan")
                    Lang_SetGudang_UkSusunan = dr("Lang_SetGudang_UkSusunan")
                ElseIf jenis = "Transaksi_Permintaan_BB_Produksi" Then
                    Lang_Transaksi_Permintaan_BB_Produksi_Judul = dr("Lang_Transaksi_Permintaan_BB_Produksi_Judul")
                    Lang_Transaksi_Permintaan_BB_Produksi_Tambah = dr("Lang_Transaksi_Permintaan_BB_Produksi_Tambah")
                    Lang_Transaksi_Permintaan_BB_Produksi_Jns_Brg = dr("Lang_Transaksi_Permintaan_BB_Produksi_Jns_Brg")
                    Lang_Transaksi_Permintaan_BB_Produksi_Req_Date = dr("Lang_Transaksi_Permintaan_BB_Produksi_Req_Date")
                    Lang_Transaksi_Permintaan_BB_Produksi_Error_Inqu = dr("Lang_Transaksi_Permintaan_BB_Produksi_Error_Inqu")
                    Lang_Transaksi_Permintaan_BB_Produksi_Error_Cus = dr("Lang_Transaksi_Permintaan_BB_Produksi_Error_Cus")
                    Lang_Transaksi_Permintaan_BB_Produksi_Error1 = dr("Lang_Transaksi_Permintaan_BB_Produksi_Error1")
                    Lang_Transaksi_Permintaan_BB_Produksi_Error2 = dr("Lang_Transaksi_Permintaan_BB_Produksi_Error2")
                    Lang_Transaksi_Permintaan_BB_Produksi_Error3 = dr("Lang_Transaksi_Permintaan_BB_Produksi_Error3")
                    Lang_Transaksi_Permintaan_BB_Produksi_Error4 = dr("Lang_Transaksi_Permintaan_BB_Produksi_Error4")
                    Lang_Transaksi_Permintaan_BB_Produksi_Error5 = dr("Lang_Transaksi_Permintaan_BB_Produksi_Error5")
                    Lang_Transaksi_Permintaan_BB_Produksi_Error6 = dr("Lang_Transaksi_Permintaan_BB_Produksi_Error6")
                    Lang_Transaksi_Permintaan_BB_Produksi_Error7 = dr("Lang_Transaksi_Permintaan_BB_Produksi_Error7")
                    Lang_Transaksi_Permintaan_BB_Produksi_Error8 = dr("Lang_Transaksi_Permintaan_BB_Produksi_Error8")
                    Lang_Transaksi_Permintaan_BB_Produksi_Error9 = dr("Lang_Transaksi_Permintaan_BB_Produksi_Error9")
                    Lang_Transaksi_Permintaan_BB_Produksi_Error10 = dr("Lang_Transaksi_Permintaan_BB_Produksi_Error10")
                    Lang_Transaksi_Permintaan_BB_Produksi_Error11 = dr("Lang_Transaksi_Permintaan_BB_Produksi_Error11")
                    Lang_Transaksi_Permintaan_BB_Produksi_Error12 = dr("Lang_Transaksi_Permintaan_BB_Produksi_Error12")
                    Lang_Transaksi_Permintaan_BB_Produksi_Error13 = dr("Lang_Transaksi_Permintaan_BB_Produksi_Error13")
                ElseIf jenis = "Binding_Barcode" Then
                    Lang_Binding_Barcode_Judul_SD = dr("Lang_Binding_Barcode_Judul_SD")
                    Lang_Binding_Barcode_Judul = dr("Lang_Binding_Barcode_Judul")
                    Lang_Binding_Barcode_Sampel = dr("Lang_Binding_Barcode_Sampel")
                    Lang_Binding_Barcode_Kd_Barcode = dr("Lang_Binding_Barcode_Kd_Barcode")
                ElseIf jenis = "Master_Satuan" Then
                    Lang_Master_Satuan_Judul = dr("Lang_Master_Satuan_Judul")
                    Lang_Master_Satuan_Kd_Satuan = dr("Lang_Master_Satuan_Kd_Satuan")
                    Lang_Master_Satuan_Default_Insert = dr("Lang_Master_Satuan_Default_Insert")
                    Lang_Master_Satuan_Tampil_Inq = dr("Lang_Master_Satuan_Tampil_Inq")
                    Lang_Master_Satuan_Tampil_PO = dr("Lang_Master_Satuan_Tampil_PO")
                    Lang_Master_Satuan_Pembulatan = dr("Lang_Master_Satuan_Pembulatan")
                    Lang_Master_Satuan_Tampil_Vol = dr("Lang_Master_Satuan_Tampil_Vol")
                    Lang_Master_Satuan_Tampil_Berat = dr("Lang_Master_Satuan_Tampil_Berat")
                    Lang_Master_Satuan_Kolom = dr("Lang_Master_Satuan_Kolom")
                    Lang_Master_Satuan_Error_Kd_Satuan = dr("Lang_Master_Satuan_Error_Kd_Satuan")
                    Lang_Master_Satuan_Error_Blm_Pilih = dr("Lang_Master_Satuan_Error_Blm_Pilih")
                    Lang_Master_Satuan_Error1 = dr("Lang_Master_Satuan_Error1")
                    Lang_Master_Satuan_Error2 = dr("Lang_Master_Satuan_Error2")
                    Lang_Master_Satuan_Error3 = dr("Lang_Master_Satuan_Error3")
                    Lang_Master_Satuan_Tampil_Jumlah = dr("Lang_Master_Satuan_Tampil_Jumlah")
                    Lang_Master_Satuan_Tampil_Panjang = dr("Lang_Master_Satuan_Tampil_Panjang")

                ElseIf jenis = "Master_Quality_Control" Then
                    Lang_Quality_Control_Judul = dr("Lang_Quality_Control_Judul")
                    Lang_Quality_Control_Kode = dr("Lang_Quality_Control_Kode")
                    Lang_Quality_Control_Keterangan = dr("Lang_Quality_Control_Keterangan")
                    Lang_Quality_Control_Target = dr("Lang_Quality_Control_Target")
                    Lang_Quality_Control_Satuan = dr("Lang_Quality_Control_Satuan")
                    Lang_Quality_Control_Tampil_Formula = dr("Lang_Quality_Control_Tampil_Formula")
                    Lang_Quality_Control_Tampil_Bahan = dr("Lang_Quality_Control_Tampil_Bahan")
                    Lang_Quality_Control_Kolom = dr("Lang_Quality_Control_Kolom")
                    Lang_Quality_Control_Error_Kode = dr("Lang_Quality_Control_Error_Kode")
                    Lang_Quality_Control_Error_Keterangan = dr("Lang_Quality_Control_Error_Keterangan")
                    Lang_Quality_Control_Error_Target = dr("Lang_Quality_Control_Error_Target")
                    Lang_Quality_Control_Error_Satuan = dr("Lang_Quality_Control_Error_Satuan")
                    Lang_Quality_Control_Error_Blm_Pilih = dr("Lang_Quality_Control_Error_Blm_Pilih")
                    Lang_Quality_Control_Error1 = dr("Lang_Quality_Control_Error1")
                    Lang_Quality_Control_Error2 = dr("Lang_Quality_Control_Error2")
                    Lang_Quality_Control_Error_Kategori = dr("Lang_Quality_Control_Error_Kategori")
                    Lang_Quality_Control_Judul_Kategori = dr("Lang_Quality_Control_Judul_Kategori")
                ElseIf jenis = "Master_Mesin" Then
                    Lang_Mesin_Judul = dr("Lang_Mesin_Judul")
                    Lang_Mesin_Divisi = dr("Lang_Mesin_Divisi")
                    Lang_Mesin_NmMesin = dr("Lang_Mesin_NmMesin")
                    Lang_Mesin_SeriMesin = dr("Lang_Mesin_SeriMesin")
                ElseIf jenis = "Master_Tipe_Seal" Then
                    Lang_Tipe_Seal_Judul = dr("Lang_Tipe_Seal_Judul")
                    Lang_Tipe_Seal_Kode = dr("Lang_Tipe_Seal_Kode")
                    Lang_Tipe_Seal_Kode_Kemasan_Utama = dr("Lang_Tipe_Seal_Kode_Kemasan_Utama")
                    Lang_Tipe_Seal_Keterangan = dr("Lang_Tipe_Seal_Keterangan")
                    Lang_Tipe_Seal_Kolom = dr("Lang_Tipe_Seal_Kolom")
                    Lang_Tipe_Seal_Jenis_Produk = dr("Lang_Tipe_Seal_Jenis_Produk")
                    Lang_Tipe_Seal_Error_Kode = dr("Lang_Tipe_Seal_Error_Kode")
                    Lang_Tipe_Seal_Error_Keterangan = dr("Lang_Tipe_Seal_Error_Keterangan")
                    Lang_Tipe_Seal_Error_Jenis_Produk = dr("Lang_Tipe_Seal_Error_Jenis_Produk")
                    Lang_Tipe_Seal_Error_Kemasan_Utama = dr("Lang_Tipe_Seal_Error_Kemasan_Utama")
                ElseIf jenis = "Master_Komposisi" Then
                    ''master komposisi
                    Lang_Komposisi_Judul = dr("Lang_Komposisi_Judul")
                    Lang_Komposisi_Kode = dr("Lang_Komposisi_Kode")
                    Lang_Komposisi_Keterangan = dr("Lang_Komposisi_Keterangan")
                    Lang_Komposisi_Kolom = dr("Lang_Komposisi_Kolom")
                    Lang_Komposisi_Error_Kode = dr("Lang_Komposisi_Error_Kode")
                    Lang_Komposisi_Error_Nama = dr("Lang_Komposisi_Error_Nama")
                ElseIf jenis = "Master_Barang_Susunan" Then
                    Lang_Barang_Susunan_Judul = dr("Lang_Barang_Susunan_Judul")
                    Lang_Barang_Susunan_Susunan = dr("Lang_Barang_Susunan_Susunan")
                    Lang_Barang_Susunan_Jenis_Palet = dr("Lang_Barang_Susunan_Jenis_Palet")
                    Lang_Barang_Susunan_Lebar = dr("Lang_Barang_Susunan_Lebar")
                    Lang_Barang_Susunan_Panjang = dr("Lang_Barang_Susunan_Panjang")
                    Lang_Barang_Susunan_Tinggi_Per = dr("Lang_Barang_Susunan_Tinggi_Per")
                    Lang_Barang_Susunan_Kolom = dr("Lang_Barang_Susunan_Kolom")
                    Lang_Barang_Susunan_Error_Susunan = dr("Lang_Barang_Susunan_Error_Susunan")
                    Lang_Barang_Susunan_Error_Jenis_Palet = dr("Lang_Barang_Susunan_Error_Jenis_Palet")
                    Lang_Barang_Susunan_Error_Panjang = dr("Lang_Barang_Susunan_Error_Panjang")
                    Lang_Barang_Susunan_Error_Lebar = dr("Lang_Barang_Susunan_Error_Lebar")
                    Lang_Barang_Susunan_Error_Total = dr("Lang_Barang_Susunan_Error_Total")
                    Lang_Barang_Susunan_Error_Jumlah = dr("Lang_Barang_Susunan_Error_Jumlah")
                    Lang_Barang_Susunan_Error_Ukuran = dr("Lang_Barang_Susunan_Error_Ukuran")
                    Lang_Barang_Susunan_Error_Tinggi_Per = dr("Lang_Barang_Susunan_Error_Tinggi_Per")
                    Lang_Barang_Susunan_Error_Ket = dr("Lang_Barang_Susunan_Error_Ket")
                    Lang_Barang_Susunan_Error1 = dr("Lang_Barang_Susunan_Error1")
                ElseIf jenis = "Master_Kemasan_Packing_Level" Then
                    Lang_Master_Kemasan_Packing_Level_Judul = dr("Lang_Master_Kemasan_Packing_Level_Judul")
                    Lang_Master_Kemasan_Packing_Level_Jenis = dr("Lang_Master_Kemasan_Packing_Level_Jenis")
                    Lang_Master_Kemasan_Packing_Level_Jenis_Pro = dr("Lang_Master_Kemasan_Packing_Level_Jenis_Pro")
                    Lang_Master_Kemasan_Packing_Level_Kd_Pack = dr("Lang_Master_Kemasan_Packing_Level_Kd_Pack")
                    Lang_Master_Kemasan_Packing_Level_Kolom = dr("Lang_Master_Kemasan_Packing_Level_Kolom")
                    Lang_Master_Kemasan_Packing_Level_Error_Ket = dr("Lang_Master_Kemasan_Packing_Level_Error_Ket")
                    Lang_Master_Kemasan_Packing_Level_Error_Jenis = dr("Lang_Master_Kemasan_Packing_Level_Error_Jenis")
                ElseIf jenis = "Master_Pelapis_Packing_Level" Then
                    Lang_Pelapis_Packing_Level_Judul = dr("Lang_Pelapis_Packing_Level_Judul")
                    Lang_Pelapis_Packing_Level_Jenis = dr("Lang_Pelapis_Packing_Level_Jenis")
                    Lang_Pelapis_Packing_Level_Jenis_Pro = dr("Lang_Pelapis_Packing_Level_Jenis_Pro")
                    Lang_Pelapis_Packing_Level_Kd_Pelapis = dr("Lang_Pelapis_Packing_Level_Kd_Pelapis")
                    Lang_Pelapis_Packing_Level_Kolom = dr("Lang_Pelapis_Packing_Level_Kolom")
                    Lang_Pelapis_Packing_Level_Kemasan_Pack = dr("Lang_Pelapis_Packing_Level_Kemasan_Pack")
                    Lang_Pelapis_Packing_Level_Error_Ket = dr("Lang_Pelapis_Packing_Level_Error_Ket")
                    Lang_Pelapis_Packing_Level_Error_Jenis = dr("Lang_Pelapis_Packing_Level_Error_Jenis")
                    Lang_Pelapis_Packing_Level_Error_Kode = dr("Lang_Pelapis_Packing_Level_Error_Kode")
                ElseIf jenis = "Master_Bundling_Promo" Then
                    Lang_Bundling_Promo_Judul = dr("Lang_Bundling_Promo_Judul")
                    Lang_Bundling_Promo_Kolom = dr("Lang_Bundling_Promo_Kolom")
                    Lang_Bundling_Promo_Error_Kode = dr("Lang_Bundling_Promo_Error_Kode")
                    Lang_Bundling_Promo_Error_Ket = dr("Lang_Bundling_Promo_Error_Ket")
                ElseIf jenis = "Master_Finishing_Kemasan" Then
                    Lang_Finishing_Kemasan_Judul = dr("Lang_Finishing_Kemasan_Judul")
                    Lang_Finishing_Kemasan_Jenis_Kemasan = dr("Lang_Finishing_Kemasan_Jenis_Kemasan")
                    Lang_Finishing_Kemasan_Kolom = dr("Lang_Finishing_Kemasan_Kolom")
                    Lang_Finishing_Kemasan_Jenis_Produk = dr("Lang_Finishing_Kemasan_Jenis_Produk")
                    Lang_Finishing_Kemasan_Kode_Finising = dr("Lang_Finishing_Kemasan_Kode_Finising")
                    Lang_Finishing_Kemasan_Error_Ket = dr("Lang_Finishing_Kemasan_Error_Ket")
                    Lang_Finishing_Kemasan_Error_Jenis = dr("Lang_Finishing_Kemasan_Error_Jenis")
                ElseIf jenis = "Barang" Then
                    Lang_Barang_Msg_Berat = dr("Lang_Barang_Msg_Berat")
                    Lang_Barang_Msg_Uk = dr("Lang_Barang_Msg_Uk")
                    Lang_Barang_Flag_Tampil_Display = dr("Lang_Barang_Flag_Tampil_Display")
                    Lang_Barang_Sd_Satuan_Judul = dr("Lang_Barang_Sd_Satuan_Judul")
                    Lang_Barang_Satuan_Belum_Dipilih = dr("Lang_Barang_Satuan_Belum_Dipilih")
                    Lang_Barang_Err_Sudah_Di_List = dr("Lang_Barang_Err_Sudah_Di_List")

                    Lang_Barang_Err_Kode_Stock_Owner = dr("Lang_Barang_Err_Kode_Stock_Owner")
                    Lang_Barang_Err_Kode_Barang = dr("Lang_Barang_Err_Kode_Barang")
                    Lang_Barang_Err_Nama_Barang = dr("Lang_Barang_Err_Nama_Barang")
                    Lang_Barang_Err_Satuan = dr("Lang_Barang_Err_Satuan")
                    Lang_Barang_Err_Keterangan = dr("Lang_Barang_Err_Keterangan")
                    Lang_Barang_Err_Harga_Barang = dr("Lang_Barang_Err_Harga_Barang")
                    Lang_Barang_Err_Stock_Min = dr("Lang_Barang_Err_Stock_Min")
                    Lang_Barang_Err_Kategori = dr("Lang_Barang_Err_Kategori")
                    Lang_Barang_Err_Status_Aktif = dr("Lang_Barang_Err_Status_Aktif")
                    Lang_Barang_Err_Flag_PPN = dr("Lang_Barang_Err_Flag_PPN")
                    Lang_Barang_Err_Barang_Sendiri = dr("Lang_Barang_Err_Barang_Sendiri")
                    Lang_Barang_Err_Berat_Bersih = dr("Lang_Barang_Err_Berat_Bersih")
                    Lang_Barang_Err_Berat_Kotor = dr("Lang_Barang_Err_Berat_Kotor")
                    Lang_Barang_Err_Panjang = dr("Lang_Barang_Err_Panjang")
                    Lang_Barang_Err_Lebar = dr("Lang_Barang_Err_Lebar")
                    Lang_Barang_Err_Tinggi = dr("Lang_Barang_Err_Tinggi")
                    Lang_Barang_Err_Kategori_Besar = dr("Lang_Barang_Err_Kategori_Besar")
                    Lang_Barang_Err_Kategori_Kecil = dr("Lang_Barang_Err_Kategori_Kecil")
                    Lang_Barang_Err_Jenis_Gudang = dr("Lang_Barang_Err_Jenis_Gudang")

                    Lang_Barang_Err_Nilai_Pengali1 = dr("Lang_Barang_Err_Nlai_Pengali1")
                    Lang_Barang_Err_Nilai_Pengali2 = dr("Lang_Barang_Err_Nilai_Pengali2")
                    Lang_Barang_Err_Flag_Tampil_Display = dr("Lang_Barang_Err_Flag_Tampil_Display")
                    Lang_Barang_Err_Lokasi_Sudah_Ada = dr("Lang_Barang_Err_Lokasi_Sudah_Ada")
                    Lang_Barang_Err_Kode_Barang_Sudah_Ada = dr("Lang_Barang_Err_Kode_Barang_Sudah_Ada")
                    Lang_Barang_Err_Lokasi_Tidak_Ditemukan = dr("Lang_Barang_Err_Lokasi_Tidak_Ditemukan")
                    Lang_Barang_Err_Pilih_Jenis_Barang = dr("Lang_Barang_Err_Pilih_Jenis_Barang")

                ElseIf jenis = "Ras" Then
                    Lang_Ras_Judul = dr("Lang_Ras_Judul")
                    Lang_Ras_Kode = dr("Lang_Ras_Kode")
                    Lang_Ras_Jenis_Hwan = dr("Lang_Ras_Jenis_Hwan")
                    Lang_Ras_Error_Jenis_Hewan = dr("Lang_Ras_Error_Jenis_Hewan")
                    Lang_Ras_Error_Kode = dr("Lang_Ras_Error_Kode")
                    Lang_Ras_Error_Keterangan = dr("Lang_Ras_Error_Keterangan")
                ElseIf jenis = "Size" Then
                    Lang_Size_Judul = dr("Lang_Size_Judul")
                ElseIf jenis = "Isi_PL" Then
                    Lang_Isi_PL_Judul = dr("Lang_Isi_PL_Judul")
                ElseIf jenis = "Trah" Then
                    Lang_Trah_Judul = dr("Lang_Trah_Judul")
                    Lang_Trah_Kode = dr("Lang_Trah_Kode")
                ElseIf jenis = "Transaksi_Produksi" Then
                    Lang_Transaksi_Produksi_Judul_SD = dr("Lang_Transaksi_Produksi_Judul_SD")
                    Lang_Transaksi_Produksi_Error_Pilih = dr("Lang_Transaksi_Produksi_Error_Pilih")
                    Lang_Transaksi_Produksi_Judul = dr("Lang_Transaksi_Produksi_Judul")
                    Lang_Transaksi_Produksi_No_Batch = dr("Lang_Transaksi_Produksi_No_Batch")
                    Lang_Transaksi_Produksi_No_Rencana = dr("Lang_Transaksi_Produksi_No_Rencana")
                    Lang_Transaksi_Produksi_Error_No_Batch = dr("Lang_Transaksi_Produksi_Error_No_Batch")
                    Lang_Transaksi_Produksi_Error_Operator = dr("Lang_Transaksi_Produksi_Error_Operator")

                ElseIf jenis = "Transaksi_Hasil_Produksi" Then
                    Lang_Hasil_Produksi_Judul = dr("Lang_Hasil_Produksi_Judul")
                    Lang_Hasil_Produksi_Tgl_Transaksi = dr("Lang_Hasil_Produksi_Tgl_Transaksi")
                    Lang_Hasil_Produksi_Operator = dr("Lang_Hasil_Produksi_Operator")
                    Lang_Hasil_Produksi_Mesin = dr("Lang_Hasil_Produksi_Mesin")
                    Lang_Hasil_Produksi_Hasil_Pro = dr("Lang_Hasil_Produksi_Hasil_Pro")
                    Lang_Hasil_Produksi_Gagal_Pro = dr("Lang_Hasil_Produksi_Gagal_Pro")
                    Lang_Hasil_Produksi_Akumulasi = dr("Lang_Hasil_Produksi_Akumulasi")
                    Lang_Hasil_Produksi_No_Batch = dr("Lang_Hasil_Produksi_No_Batch")
                    Lang_Hasil_Produksi_Tarik_Data = dr("Lang_Hasil_Produksi_Tarik_Data")
                    Lang_Hasil_Produksi_Error_Operator = dr("Lang_Hasil_Produksi_Error_Operator")
                    Lang_Hasil_Produksi_Error_No_Pro = dr("Lang_Hasil_Produksi_Error_No_Pro")
                    Lang_Hasil_Produksi_Error_Gagal = dr("Lang_Hasil_Produksi_Error_Gagal")
                    Lang_Hasil_Produksi_Error_Tgl_Pro = dr("Lang_Hasil_Produksi_Error_Tgl_Pro")
                    Lang_Hasil_Produksi_Error_Tgl_Exp = dr("Lang_Hasil_Produksi_Error_Tgl_Exp")
                    Lang_Hasil_Produksi_Error_Tgl_Pro2 = dr("Lang_Hasil_Produksi_Error_Tgl_Pro2")
                    Lang_Hasil_Produksi_Error_Gagal2 = dr("Lang_Hasil_Produksi_Error_Gagal2")
                    Lang_Hasil_Produksi_Error_Brg_SN = dr("Lang_Hasil_Produksi_Error_Brg_SN")
                ElseIf jenis = "Master_Cek_Pengiriman" Then
                    Lang_Master_CekPengiriman_Judul = dr("Lang_Master_CekPengiriman_Judul")
                    Lang_Master_CekPengiriman_Judul_Sd = dr("Lang_Master_CekPengiriman_Judul_Sd")
                    Lang_Master_CekPengiriman_Judul_Sd1 = dr("Lang_Master_CekPengiriman_Judul_Sd1")
                    Lang_Master_CekPengiriman_Err_Ekspedisi_Kosong = dr("Lang_Master_CekPengiriman_Err_Ekspedisi_Kosong")
                    Lang_Master_CekPengiriman_Err_Id_Ekspedisi_Null = dr("Lang_Master_CekPengiriman_Err_Id_Ekspedisi_Null")
                    Lang_Master_CekPengiriman_Err_EkspedisiHrsDipilih = dr("Lang_Master_CekPengiriman_Err_EkspedisiHrsDipilih")
                ElseIf jenis = "Rencana_Produksi" Then
                    Lang_Rencana_Produksi_Judul = dr("Lang_Rencana_Produksi_Judul")
                    Lang_Rencana_Produksi_Err_Hapus_Temp = dr("Lang_Rencana_Produksi_Err_Hapus_Temp")
                    Lang_Rencana_Produksi_Err_Jmlh_Sisa = dr("Lang_Rencana_Produksi_Err_Jmlh_Sisa")
                    Lang_Rencana_Produksi_Err_No_Tdk_Ada = dr("Lang_Rencana_Produksi_Err_No_Tdk_Ada")
                    Lang_Rencana_Produksi_Err_Harus_Sesuai_Antrian = dr("Lang_Rencana_Produksi_Err_Harus_Sesuai_Antrian")
                    Lang_Rencana_Produksi_No_Antrian = dr("Lang_Rencana_Produksi_No_Antrian")
                    Lang_Rencana_Produksi_No_PO = dr("Lang_Rencana_Produksi_No_PO")
                    Lang_Rencana_Produksi_Sisa = dr("Lang_Rencana_Produksi_Sisa")
                    Lang_Rencana_Produksi_Err_Belum_Pilih = dr("Lang_Rencana_Produksi_Err_Belum_Pilih")
                    Lang_Rencana_Produksi_Err_Line = dr("Lang_Rencana_Produksi_Err_Line")
                    Lang_Rencana_Produksi_Err_Faktur = dr("Lang_Rencana_Produksi_Err_Faktur")
                    Lang_Rencana_Produksi_Err_Jumlah_Lebih_Besar = dr("Lang_Rencana_Produksi_Err_Jumlah_Lebih_Besar")
                    Lang_Rencana_Produksi_Err_No_PO_Sdh_Ada = dr("Lang_Rencana_Produksi_Err_No_PO_Sdh_Ada")
                    Lang_Rencana_Produksi_Err_Jumlah_hrs_diisi = dr("Lang_Rencana_Produksi_Err_Jumlah_hrs_diisi")
                    Lang_Rencana_Produksi_Err_Hrs_Selesai_Semua = dr("Lang_Rencana_Produksi_Err_Hrs_Selesai_Semua")
                    Lang_Rencana_Produksi_Err_Hrs_Sesuai = dr("Lang_Rencana_Produksi_Err_Hrs_Sesuai")
                    Lang_Rencana_Produksi_Err_Jns_Produk_Beda = dr("Lang_Rencana_Produksi_Err_Jns_Produk_Beda")
                    Lang_Rencana_Produksi_Judul_Sd = dr("Lang_Rencana_Produksi_Judul_Sd")
                    Lang_Rencana_Produksi_Err_Tanggal_Awal = dr("Lang_Rencana_Produksi_Err_Tanggal_Awal")
                ElseIf jenis = "Validasi_Permintaan_BB" Then
                    Lang_Validasi_Permintaan_BB_Judul = dr("Lang_Validasi_Permintaan_BB_Judul")
                    Lang_Validasi_Permintaan_BB_No_Rencana = dr("Lang_Validasi_Permintaan_BB_No_Rencana")
                ElseIf jenis = "Po_Bahan" Then
                    Lang_PO_Bahan_Judul = dr("Lang_PO_Bahan_Judul")
                ElseIf jenis = "Compare_HPP" Then
                    Lang_Compare_HPP_Judul = dr("Lang_Compare_HPP_Judul")
                    Lang_Compare_HPP_Real_HPP = dr("Lang_Compare_HPP_Real_HPP")
                    Lang_Compare_HPP_Simulasi_HPP = dr("Lang_Compare_HPP_Simulasi_HPP")
                    Lang_Compare_HPP_Selisih_HPP = dr("Lang_Compare_HPP_Selisih_HPP")
                    Lang_Compare_HPP_Error_Ket = dr("Lang_Compare_HPP_Error_Ket")
                    Lang_Compare_HPP_Error = dr("Lang_Compare_HPP_Error")
                ElseIf jenis = "Display_HPP" Then
                    Lang_Display_HPP_Judul = dr("Lang_Display_HPP_Judul")
                ElseIf jenis = "Display_Barang_Masuk" Then
                    Lang_Display_Barang_Masuk_Judul_Display = dr("Lang_Display_Barang_Masuk_Judul_Display")
                ElseIf jenis = "Master_Klasifikasi_Bahan" Then
                    Lang_Klasifikasi_Bahan_Judul = dr("Lang_Klasifikasi_Bahan_Judul")
                    Lang_Klasifikasi_Bahan_Kode = dr("Lang_Klasifikasi_Bahan_Kode")
                    Lang_Klasifikasi_Bahan_Keterangan = dr("Lang_Klasifikasi_Bahan_Keterangan")
                    Lang_Klasifikasi_Bahan_Prefix = dr("Lang_Klasifikasi_Bahan_Prefix")
                    Lang_Klasifikasi_Bahan_Kolom = dr("Lang_Klasifikasi_Bahan_Kolom")
                    Lang_Klasifikasi_Bahan_Error_Kode = dr("Lang_Klasifikasi_Bahan_Error_Kode")
                    Lang_Klasifikasi_Bahan_Error_Nama = dr("Lang_Klasifikasi_Bahan_Error_Nama")
                    Lang_Klasifikasi_Bahan_Error_Prefix = dr("Lang_Klasifikasi_Bahan_Error_Prefix")
                    Lang_Klasifikasi_Bahan_Error_Prefix_Numeric = dr("Lang_Klasifikasi_Bahan_Error_Prefix_Numeric")
                    Lang_Klasifikasi_Bahan_Error_Prefix_Sudah_Ada = dr("Lang_Klasifikasi_Bahan_Error_Prefix_Sudah_Ada")
                ElseIf jenis = "Master_Kategori_PO" Then
                    Lang_Kategori_PO_Judul = dr("Lang_Kategori_PO_Judul")
                    Lang_Kategori_PO_Kode = dr("Lang_Kategori_PO_Kode")
                    Lang_Kategori_PO_Keterangan = dr("Lang_Kategori_PO_Keterangan")
                    Lang_Kategori_PO_Kolom = dr("Lang_Kategori_PO_Kolom")
                    Lang_Kategori_PO_Error_Kode = dr("Lang_Kategori_PO_Error_Kode")
                    Lang_Kategori_PO_Error_Nama = dr("Lang_Kategori_PO_Error_Nama")
                ElseIf jenis = "Master_Jenis_Muatan" Then
                    Lang_Master_Jenis_Muatan_Judul = dr("Lang_Master_Jenis_Muatan_Judul")
                ElseIf jenis = "Master_Packing" Then
                    Lang_Master_Packing_Judul = dr("Lang_Master_Packing_Judul")
                End If
            End If
        End Using
    End Sub


    Public Shared Sub Get_Languages_Global(lang As String)

    End Sub

End Class

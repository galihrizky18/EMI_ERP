Public Class Tes_Sync_Approval_Waste

    Dim Random As New Random()

    Private Sub Tes_Sync_Approval_Waste_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ListView1.Columns.Add("Error", 900, HorizontalAlignment.Left)
        ListView1.View = View.Details
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Handle_Proses_Waste_Process()
        Handle_Proses_Waste_Product()

    End Sub


    Private Sub Handle_Proses_Waste_Process()

        get_jam()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            '========================================
            '=     GET APPROVAL LEVEL TERTINGGI     =
            '========================================
            Dim ApprovalLevelTertinggi As Integer = 0
            SQL = "select isnull(max(Approval_Level), 0) as LevelTertinggi "
            SQL = SQL & "from N_EMI_Transaksi_Approval_Waste "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and Flag_Approve is null and Jenis_Approval = 'Waste_Process' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    ApprovalLevelTertinggi = Dr("LevelTertinggi")
                End If
            End Using

            For i As Integer = 1 To ApprovalLevelTertinggi

                SQL = "select a.No_Transaksi, a.No_Faktur_Waste, a.ID_User_Android_Approve, b.No_HP, a.Flag_Approve, a.Flag_Sudah_Kirim_WA, "
                SQL = SQL & "b.No_HP, a.tanggal, a.Jam, "
                SQL = SQL & "case when b.Jenis_Approval = 'Waste_Process' then 'Waste Process' "
                SQL = SQL & "when b.Jenis_Approval = 'Waste_Produk' then 'Waste Produk' end as Jenis_Pemusnahan, "
                SQL = SQL & "a.No_Berita_Acara "
                SQL = SQL & "from N_EMI_Transaksi_Approval_Waste a "
                SQL = SQL & "inner join N_EMI_Master_Hierarchy_Approval_Waste b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.ID_User_Android_Approve = b.ID_User_Android and a.Jenis_Approval = b.Jenis_Approval and a.kode_Stock_owner = b.kode_Stock_owner "
                SQL = SQL & "where a.Status is null "
                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and a.Approval_Level = " & i & " "
                SQL = SQL & "and a.Flag_Approve is null "
                SQL = SQL & "and a.Jenis_Approval = 'Waste_Process' "
                SQL = SQL & "order by a.approval_Level, a.No_Transaksi "
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        If .Rows.Count <> 0 Then
                            For j As Integer = 0 To .Rows.Count - 1

                                If i > 1 Then

                                    '=====================================================
                                    '=     CEK APAKAH LEVEL SEBELUMNYA SUDAH APPROVE     =
                                    '=====================================================


                                    SQL = "select top 1 a.No_Transaksi, a.No_Faktur_Waste, a.ID_User_Android_Approve, b.No_HP, a.Flag_Approve, a.Flag_Sudah_Kirim_WA "
                                    SQL = SQL & "from N_EMI_Transaksi_Approval_Waste a "
                                    SQL = SQL & "inner join N_EMI_Master_Hierarchy_Approval_Waste b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.ID_User_Android_Approve = b.ID_User_Android and a.Jenis_Approval = b.Jenis_Approval and a.kode_Stock_owner = b.kode_Stock_owner "
                                    SQL = SQL & "where a.Status is null "
                                    SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                                    SQL = SQL & "and a.Approval_Level = " & i - 1 & " "
                                    SQL = SQL & "and a.No_Transaksi = '" & .Rows(j).Item("No_Transaksi") & "' "
                                    SQL = SQL & "and a.No_Faktur_Waste = '" & .Rows(j).Item("No_Faktur_Waste") & "' "
                                    SQL = SQL & "and a.Flag_Sudah_Kirim_WA = 'Y' "
                                    SQL = SQL & "and a.Flag_Approve = 'Y' "
                                    SQL = SQL & "and a.Jenis_Approval = 'Waste_Process' "
                                    Using Dr = OpenTrans(SQL)
                                        If Not Dr.Read Then
                                            Continue For
                                        End If
                                    End Using

                                End If

                                If General_Class.CekNULL(.Rows(j).Item("Flag_Sudah_Kirim_WA")) = "" Then

                                    Dim NoHp As String = .Rows(j).Item("No_HP")
                                    'Dim NoHp As String = "6285117547880"
                                    Dim Jenis_Pemusnahan As String = If(General_Class.CekNULL(.Rows(j).Item("Jenis_Pemusnahan")) = "", "-", .Rows(j).Item("Jenis_Pemusnahan"))
                                    Dim No_Ba As String = If(General_Class.CekNULL(.Rows(j).Item("No_Berita_Acara")) = "", "-", .Rows(j).Item("No_Berita_Acara"))
                                    Dim No_Faktur_Waste As String = If(General_Class.CekNULL(.Rows(j).Item("No_Faktur_Waste")) = "", "-", .Rows(j).Item("No_Faktur_Waste"))

                                    Dim Total As Double = 0
                                    Dim Satuan As String = ""
                                    SQL = "select b.Total, b.Satuan "
                                    SQL &= $"from N_EMI_Transaksi_Transfer_Waste a "
                                    SQL &= $"inner join N_EMI_Transaksi_Transfer_Waste_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
                                    SQL &= $"where a.status is NULL "
                                    SQL &= $"and a.Kode_Perusahaan = '{KodePerusahaan}' "
                                    SQL &= $"and a.No_Faktur = '{ .Rows(j).Item("No_Faktur_Waste")}' "
                                    Using Dr = OpenTrans(SQL)
                                        If Dr.Read Then
                                            Total = Format(Dr("Total"), "N4")
                                            Satuan = Dr("Satuan")
                                        End If
                                    End Using




                                    Dim payload As String = "{
                                        ""messaging_product"": ""whatsapp"",
                                        ""to"": """ & NoHp & """,
                                        ""type"": ""template"",
                                        ""template"": {
                                            ""name"": ""emi_approval_pemushahan"",
                                            ""language"": {
                                                ""code"": ""id""
                                            },
                                            ""components"": [
                                                {
                                                    ""type"": ""body"",
                                                    ""parameters"": [
                                                        {""type"": ""text"", ""text"": """ & No_Ba & """},
                                                        {""type"": ""text"", ""text"": """ & No_Faktur_Waste & """},
                                                        {""type"": ""text"", ""text"": """ & Format(.Rows(j).Item("tanggal"), "dd MMM yyyy") & ", " & .Rows(j).Item("Jam") & """},
                                                        {""type"": ""text"", ""text"": """ & Jenis_Pemusnahan & """},
                                                        {""type"": ""text"", ""text"": """ & Format(Total, "N4") & " " & Satuan & """}
                                                    ]
                                                }
                                                
                                            ]
                                        }
                                    }"



                                    Dim hasil As String = Helper_API.CallAPI(Url_WA_Business, "POST", Token_WA_Business, payload)

                                    If hasil.StartsWith("API Error") OrElse hasil.StartsWith("Error") Then
                                        ' Ambil hanya isi pesan setelah titik dua
                                        Dim parts() As String = hasil.Split(New Char() {":"c}, 2, StringSplitOptions.None)
                                        Dim msg As String = If(parts.Length > 1, parts(1).Trim(), hasil)

                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Terjadi error waktu call API: " & msg)
                                        Exit Sub
                                    End If



                                    SQL = "update N_EMI_Transaksi_Approval_Waste set Flag_Sudah_Kirim_WA = 'Y' "
                                    SQL &= $"where kode_perusahaan = '{KodePerusahaan}' "
                                    SQL &= $"and Flag_Sudah_Kirim_WA is NULL "
                                    SQL &= $"and Flag_Approve is null "
                                    SQL &= $"and No_Faktur_Waste = '{ .Rows(j).Item("No_Faktur_Waste")}' "
                                    SQL &= $"and No_Transaksi = '{ .Rows(j).Item("No_Transaksi")}' "
                                    SQL &= $"and Approval_Level = '{i}' "
                                    ExecuteTrans(SQL)

                                    Continue For

                                End If

                            Next
                        End If
                    End With
                End Using

            Next





            SQL = "select distinct a.No_Transaksi, a.No_Faktur_Waste, a.Jenis_Approval, "
            SQL = SQL & "case when exists ( select 1 from N_EMI_Transaksi_Approval_Waste z "
            SQL = SQL & "where z.Kode_Perusahaan = a.Kode_Perusahaan "
            SQL = SQL & "and z.No_Transaksi = a.No_Transaksi "
            SQL = SQL & "and z.No_Faktur_Waste = a.No_Faktur_Waste "
            SQL = SQL & "and z.Flag_Approve IS NULL "
            SQL = SQL & ") then 'T' else 'Y' end as isApprovedAll "
            SQL = SQL & "from N_EMI_Transaksi_Approval_Waste a "
            SQL = SQL & "where a.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.Jenis_Approval = 'Waste_Process' "
            SQL = SQL & "and a.Flag_Selesai is null "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            If .Rows(i).Item("isApprovedAll") = "Y" Then



                                Dim No_Faktur_Waste As String = .Rows(i).Item("No_Faktur_Waste")
                                Dim No_Transaksi_Approval As String = .Rows(i).Item("No_Transaksi")

                                'Proses Waste

#Region "PROSES WASTE DI SINI"

                                Dim QrLama As String = ""
                                Dim expDate As String = ""
                                Dim batchLama As String = ""
                                Dim tglMsk As String = ""
                                Dim SN As String = ""
                                Dim UrutOto As String = ""
                                Dim IdWarehouseTujuan As String = ""
                                Dim PalletTujuan As String = ""
                                Dim KdBarang As String = ""
                                Dim KdSo As String = ""
                                Dim SatuanBesar As String = ""
                                Dim SatuanKecil As String = ""
                                Dim JumlahEstimasi As String = ""
                                Dim JumlahBagsEstimasi As String = ""

                                SQL = "select a.No_Faktur, a.Kode_Stock_Owner, b.Kode_Barang, c.Serial_Number_Awal, c.Jumlah, c.Jumlah_Bags, b.Satuan, b.Satuan_Barang, "
                                SQL = SQL & "d.Qr_Code, d.Batch_Number, d.Tgl_Expired, d.Tgl_Masuk, c.Urut_Oto, c.Id_Wms_Tujuan "
                                SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste a  "
                                SQL = SQL & "inner join N_EMI_Transaksi_Transfer_Waste_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
                                SQL = SQL & "inner join N_EMI_Transaksi_Transfer_Waste_Det c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.Urut_TF "
                                SQL = SQL & "inner join Barang_SN d on c.Kode_Perusahaan = d.Kode_Perusahaan and a.Kode_Stock_Owner = d.Kode_Stock_Owner and b.Kode_Barang = d.Kode_Barang and c.Serial_Number_Awal = d.Serial_Number "
                                SQL = SQL & "where a.Status is null "
                                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                                SQL = SQL & "and a.No_Faktur = '" & .Rows(i).Item("No_Faktur_Waste") & "' "
                                SQL = SQL & "and c.Selesai is null "
                                Using Ds2 = BindingTrans(SQL)
                                    If Ds2.Tables("MyTable").Rows.Count <> 0 Then
                                        For j As Integer = 0 To Ds2.Tables("MyTable").Rows.Count - 1

                                            QrLama = General_Class.CekNULL(Ds2.Tables("MyTable").Rows(j).Item("Qr_Code"))
                                            batchLama = General_Class.CekNULL(Ds2.Tables("MyTable").Rows(j).Item("Batch_Number"))
                                            SN = Ds2.Tables("MyTable").Rows(j).Item("Serial_Number_Awal")
                                            expDate = General_Class.CekNULL(Ds2.Tables("MyTable").Rows(j).Item("Tgl_Expired"))
                                            tglMsk = General_Class.CekNULL(Ds2.Tables("MyTable").Rows(j).Item("Tgl_Masuk"))
                                            UrutOto = General_Class.CekNULL(Ds2.Tables("MyTable").Rows(j).Item("Urut_Oto"))
                                            IdWarehouseTujuan = General_Class.CekNULL(Ds2.Tables("MyTable").Rows(j).Item("Id_Wms_Tujuan"))
                                            KdBarang = General_Class.CekNULL(Ds2.Tables("MyTable").Rows(j).Item("Kode_Barang"))
                                            KdSo = General_Class.CekNULL(Ds2.Tables("MyTable").Rows(j).Item("Kode_Stock_Owner"))
                                            SatuanBesar = General_Class.CekNULL(Ds2.Tables("MyTable").Rows(j).Item("Satuan"))
                                            SatuanKecil = General_Class.CekNULL(Ds2.Tables("MyTable").Rows(j).Item("Satuan_Barang"))
                                            JumlahEstimasi = HilangkanTanda(Ds2.Tables("MyTable").Rows(j).Item("Jumlah"))
                                            JumlahBagsEstimasi = HilangkanTanda(Ds2.Tables("MyTable").Rows(j).Item("Jumlah_Bags"))

                                            SQL = "select a.Status, c.Selesai, b.Flag_Timbang "
                                            SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste a, N_EMI_Transaksi_Transfer_Waste_Detail b, N_EMI_Transaksi_Transfer_Waste_Det c "
                                            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.no_Faktur = b.No_Faktur and "
                                            SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan and b.no_Faktur = c.No_Faktur and b.urut_oto=c.urut_TF "
                                            SQL = SQL & "and a.No_Faktur = '" & No_Faktur_Waste & "' and c.urut_oto = '" & UrutOto & "'  "
                                            Using Dr = OpenTrans(SQL)
                                                If Dr.Read Then

                                                    If General_Class.CekNULL(Dr("status")) <> "" Then
                                                        Dr.Close()
                                                        CloseTrans()
                                                        CloseConn()
                                                        'MessageBox.Show("Proses tidak bisa dilanjutkan, barang sudah dibatalkan!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                        Dim Lvw As ListViewItem
                                                        Lvw = ListView1.Items.Add("Automatization - Waste Process: barang sudah dibatalkan!!")
                                                        'MessageBox.Show(ex.Message)
                                                        Exit Sub
                                                    ElseIf General_Class.CekNULL(Dr("selesai")) = "Y" Then
                                                        Dr.Close()
                                                        CloseTrans()
                                                        CloseConn()
                                                        'MessageBox.Show("Terjadi kesalahan, barang sudah selesai diproses!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                        Dim Lvw As ListViewItem
                                                        Lvw = ListView1.Items.Add("Automatization - Waste Process: barang sudah selesai diproses!")
                                                        Exit Sub
                                                    ElseIf General_Class.CekNULL(Dr("Flag_Timbang")) = "Y" Then
                                                        Dr.Close()
                                                        CloseTrans()
                                                        CloseConn()
                                                        'MessageBox.Show("Terjadi kesalahan, ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                        Dim Lvw As ListViewItem
                                                        Lvw = ListView1.Items.Add("Automatization - Waste Process: Terjadi kesalahan")
                                                        Exit Sub
                                                    End If
                                                Else
                                                    Dr.Close()
                                                    CloseTrans()
                                                    CloseConn()
                                                    'MessageBox.Show("Data barang tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                    Exit Sub
                                                End If
                                            End Using

                                            SQL = "Select Top(1) nomor_urut from view_warehouse_position_detail where "
                                            SQL = SQL & "kode_Perusahaan ='" & KodePerusahaan & "' and kode_barang is null and "
                                            SQL = SQL & "id_wms_warehouse_position = '" & IdWarehouseTujuan & "' "
                                            SQL = SQL & "order by nomor_urut "
                                            Using dr = OpenTrans(SQL)
                                                If dr.Read Then
                                                    PalletTujuan = dr("nomor_urut")
                                                Else
                                                    dr.Close()
                                                    CloseTrans()
                                                    CloseConn()
                                                    'MessageBox.Show("data Rak Sudah Penuh . . ! ! ")
                                                    Dim Lvw As ListViewItem
                                                    Lvw = ListView1.Items.Add("Automatization - Waste Process: data Rak Sudah Penuh . . ! !")
                                                    Exit Sub
                                                End If
                                            End Using

                                            Dim nilai_kecildetail As Double = 0
                                            SQL = "select dbo.ubah_satuan('" & KodePerusahaan & "', 'masa','" & KdBarang & "', '" & SatuanBesar & "',"
                                            SQL = SQL & "'" & SatuanKecil & "', '" & JumlahEstimasi & "' ) as hasil"
                                            Using Dr1 = OpenTrans(SQL)
                                                If Dr1.Read Then
                                                    If General_Class.CekNULL(Dr1("hasil")) = "" Then
                                                        Dr1.Close()
                                                        CloseTrans()
                                                        CloseConn()
                                                        'MessageBox.Show("data konversi satuan kirim tidak ada ")
                                                        Dim Lvw As ListViewItem
                                                        Lvw = ListView1.Items.Add("Automatization - Waste Process: data konversi satuan kirim tidak ada")
                                                        Exit Sub
                                                    End If

                                                    nilai_kecildetail = Dr1("hasil")
                                                Else
                                                    Dr1.Close()
                                                    CloseTrans()
                                                    CloseConn()
                                                    'MessageBox.Show("data konversi satuan kirim tidak ada ")
                                                    Dim Lvw As ListViewItem
                                                    Lvw = ListView1.Items.Add("Automatization - Waste Process: data konversi satuan kirim tidak ada")
                                                    Exit Sub
                                                End If
                                            End Using

                                            '============================
                                            '=       POTONG STOCK       =
                                            '============================
                                            Dim nilai_persediaan_min As Double = 0
                                            SQL = "select round(dbo.get_hpp(serial_number) * " & nilai_kecildetail & ", 2) as rp_persediaan_min from barang_sn where "
                                            SQL = SQL & "Kode_Stock_Owner='" & KdSo & "' and Kode_Barang='" & KdBarang & "' "
                                            SQL = SQL & "and Serial_Number='" & SN & "'"
                                            Using dr = OpenTrans(SQL)
                                                If dr.Read Then
                                                    nilai_persediaan_min = dr("rp_persediaan_min")
                                                Else
                                                    dr.Close()
                                                    CloseTrans()
                                                    CloseConn()
                                                    'MessageBox.Show("Data SN tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                    Dim Lvw As ListViewItem
                                                    Lvw = ListView1.Items.Add("Automatization - Waste Process: Data SN tidak ditemukan!")
                                                    Exit Sub
                                                End If
                                            End Using

                                            Dim Nama As String = ""
                                            'Dim jumlahAkhir As Double = Val(dgv_GoodStock) - Val(dgv_Jumlah)
                                            SQL = "select Nama, Kode_Barang, round(good_stock,4) as good_stock, Jumlah_Bags from Barang where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & KdSo & "' "
                                            SQL = SQL & "and Kode_Barang='" & KdBarang & "' "
                                            Using dr = OpenTrans(SQL)
                                                If dr.Read Then
                                                    Nama = dr("Kode_Barang")
                                                    If dr("good_stock") < nilai_kecildetail Then
                                                        dr.Close()
                                                        CloseTrans()
                                                        CloseConn()
                                                        'MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat stock " & Nama & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                                        Dim Lvw As ListViewItem
                                                        Lvw = ListView1.Items.Add("Automatization - Waste Process: Proses tidak dapat dilanjutkan karena akan membuat stock " & Nama & " menjadi negatif.")
                                                        Exit Sub
                                                    ElseIf dr("Jumlah_Bags") < JumlahBagsEstimasi Then
                                                        dr.Close()
                                                        CloseTrans()
                                                        CloseConn()
                                                        'MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat jumlah bags " & Nama & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                                        Dim Lvw As ListViewItem
                                                        Lvw = ListView1.Items.Add("Automatization - Waste Process: Proses tidak dapat dilanjutkan karena akan membuat jumlah bags " & Nama & " menjadi negatif.")
                                                        Exit Sub
                                                    Else
                                                        dr.Close()
                                                        SQL = "update barang set Good_Stock = Good_Stock - Round(" & nilai_kecildetail & ",4), Jumlah_Bags = Jumlah_Bags - " & JumlahBagsEstimasi & " "
                                                        SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & KdSo & "' "
                                                        SQL = SQL & " and Kode_Barang='" & KdBarang & "'"
                                                        ExecuteTrans(SQL)
                                                    End If
                                                Else
                                                    dr.Close()
                                                    CloseTrans()
                                                    CloseConn()
                                                    'MessageBox.Show("Barang " & Nama & " tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                    Dim Lvw As ListViewItem
                                                    Lvw = ListView1.Items.Add("Automatization - Waste Process: Barang " & Nama & " tidak ditemukan!")
                                                    Exit Sub
                                                End If
                                            End Using

                                            SQL = "select round(jumlah,4) as jumlah, Jumlah_Bags from Barang_SN where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & KdSo & "' "
                                            SQL = SQL & "and Kode_Barang='" & KdBarang & "' "
                                            SQL = SQL & "and Serial_Number='" & SN & "'"
                                            Using dr = OpenTrans(SQL)
                                                If dr.Read Then
                                                    If dr("jumlah") < nilai_kecildetail Then
                                                        dr.Close()
                                                        CloseTrans()
                                                        CloseConn()
                                                        'MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat stock " & Nama & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                                        Dim Lvw As ListViewItem
                                                        Lvw = ListView1.Items.Add("Automatization - Waste Process: Proses tidak dapat dilanjutkan karena akan membuat stock " & Nama & " menjadi negatif.")
                                                        Exit Sub
                                                    ElseIf dr("Jumlah_Bags") < JumlahBagsEstimasi Then
                                                        dr.Close()
                                                        CloseTrans()
                                                        CloseConn()
                                                        'MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat jumlah bags " & Nama & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                                        Dim Lvw As ListViewItem
                                                        Lvw = ListView1.Items.Add("Automatization - Waste Process: Proses tidak dapat dilanjutkan karena akan membuat jumlah bags " & Nama & " menjadi negatif.")
                                                        Exit Sub
                                                    Else
                                                        dr.Close()
                                                        SQL = "update barang_sn set jumlah = jumlah - Round(" & nilai_kecildetail & ",4), Jumlah_Bags = Jumlah_Bags - " & JumlahBagsEstimasi & " "
                                                        SQL = SQL & "where Kode_Stock_Owner='" & KdSo & "' and Kode_Barang='" & KdBarang & "' "
                                                        SQL = SQL & "and Serial_Number='" & SN & "'"
                                                        ExecuteTrans(SQL)
                                                    End If
                                                Else
                                                    dr.Close()
                                                    CloseTrans()
                                                    CloseConn()
                                                    'MessageBox.Show("Barang " & Nama & " tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                    Dim Lvw As ListViewItem
                                                    Lvw = ListView1.Items.Add("Automatization - Waste Process: Barang " & Nama & " tidak ditemukan!")
                                                    Exit Sub
                                                End If
                                            End Using

                                            '====================================
                                            '=       CEK KESESUAIAN STOCK       =
                                            '====================================
                                            SQL = "SELECT round(SUM(good_stock),4) AS good_stock, isnull((select round(sum(jumlah),4) from Barang_sn x "
                                            SQL = SQL & "where a.kode_Barang=x.kode_Barang and a.Kode_Stock_Owner=x.kode_Stock_Owner "
                                            SQL = SQL & "and a.kode_Perusahaan=x.kode_Perusahaan ),0) as Jumlah_sn, "
                                            SQL = SQL & "isnull(round(SUM(jumlah_bags), 2), 0) AS jumlah_bags_barang, "
                                            SQL = SQL & "isnull((select round(sum(Jumlah_Bags), 2) from Barang_sn y "
                                            SQL = SQL & "where a.kode_Barang=y.kode_Barang and a.Kode_Stock_Owner=y.kode_Stock_Owner and a.kode_Perusahaan=y.kode_Perusahaan ), 0) as jumlah_bags_sn "
                                            SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner = '" & KdSo & "' "
                                            SQL = SQL & "AND a.Kode_Barang = '" & KdBarang & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
                                            SQL = SQL & "group by a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan "
                                            Using D3 = BindingTrans(SQL)
                                                With D3.Tables("MyTable")
                                                    If D3.Tables("MyTable").Rows.Count <> 0 Then
                                                        If D3.Tables("MyTable").Rows(0).Item("good_stock") <> D3.Tables("MyTable").Rows(0).Item("Jumlah_sn") Or D3.Tables("MyTable").Rows(0).Item("jumlah_bags_barang") <> D3.Tables("MyTable").Rows(0).Item("jumlah_bags_sn") Then
                                                            CloseTrans()
                                                            CloseConn()
                                                            'MessageBox.Show("Terjadi Kesalahan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                            Dim Lvw As ListViewItem
                                                            Lvw = ListView1.Items.Add("Automatization - Waste Process: Stock Tidak Sesuai Saat Potong Stock")
                                                            Exit Sub
                                                        End If
                                                    Else
                                                        CloseTrans()
                                                        CloseConn()
                                                        'MessageBox.Show("Data tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                        Dim Lvw As ListViewItem
                                                        Lvw = ListView1.Items.Add("Automatization - Waste Process: Data tidak ditemukan . . ! !")
                                                        Exit Sub
                                                    End If
                                                End With
                                            End Using

                                            '==============================
                                            '=       INSERT SN BARU       =
                                            '==============================

                                            Dim hargaIsn As String = ""
                                            Dim namaBarang As String = ""
                                            Dim warnaLama As String = ""

                                            'Ambil Data Lama
                                            SQL = "select a.Serial_Number, a.Qr_Code, a.Kode_Unik_Berjalan, b.Nama, a.Batch_Number, a.Tgl_Expired, a.warna "
                                            SQL = SQL & "from barang_sn a, barang b "
                                            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
                                            SQL = SQL & "and a.Kode_Stock_Owner = b.Kode_Stock_Owner "
                                            SQL = SQL & "and a.Kode_Barang = b.Kode_Barang "
                                            SQL = SQL & "and a.Kode_Perusahaan='" & KodePerusahaan & "' "
                                            SQL = SQL & "and a.Kode_Stock_Owner='" & KdSo & "' "
                                            SQL = SQL & "and a.Kode_Barang ='" & KdBarang & "' "
                                            SQL = SQL & "and a.Serial_Number='" & SN & "' "
                                            'SQL = SQL & "and a.Jumlah <> 0 "
                                            Using Dr = OpenTrans(SQL)
                                                Do While Dr.Read
                                                    hargaIsn = Get_Harga_SN(Dr("Serial_Number"))
                                                    QrLama = General_Class.CekNULL(Dr("Qr_Code"))
                                                    batchLama = General_Class.CekNULL(Dr("Batch_Number"))
                                                    namaBarang = General_Class.CekNULL(Dr("Nama"))
                                                    expDate = General_Class.CekNULL(Dr("Tgl_Expired"))
                                                    warnaLama = General_Class.CekNULL(Dr("warna"))
                                                Loop
                                            End Using

                                            'GENERATE SN BARU
                                            Dim str As String = Format(Random.Next(0, 999), "000") & Format(tgl_skg, "HHmmss")
                                            Dim Kode_Unik As String = str.Substring(0, 5) & "BB" & Chr(64 + str.Substring(6, 1)) & str.Substring(6, Len(str) - 6)
                                            Dim SN_Baru As String = Kode_Unik & Tanda_SN & "01" & Tanda_SN & hargaIsn & Tanda_SN & "02" & Tanda_SN & Format(tgl_skg, "yyyy-MM-dd")

                                            Dim newKodeUnikBerjalan As String = Generate_Random_Kode(10)

                                            'INSERT BARANG SN BARU
                                            SQL = "insert into Barang_SN (Kode_Perusahaan, Kode_Stock_Owner, Kode_Barang, Serial_Number, Jumlah,  Jumlah_Bags, "
                                            SQL = SQL & "Tgl_Expired, Tgl_Produksi, Stock_PO, Stock_Inquiry, Id_Warehouse, id_Susunan, Qr_Code, Kode_Unik_Berjalan, Kode_Unik_Asal, Nomor_Pallet, batch_number, Warna, Tgl_masuk, Blok_SN) "
                                            SQL = SQL & "select Kode_Perusahaan, '" & KdSo & "', Kode_Barang, '" & SN_Baru & "', '" & nilai_kecildetail & "', " & JumlahBagsEstimasi & ", "
                                            SQL = SQL & "Tgl_Expired, Tgl_Produksi, Stock_PO, Stock_Inquiry, '" & IdWarehouseTujuan & "', id_Susunan , Qr_Code, '" & newKodeUnikBerjalan & "', "
                                            SQL = SQL & "Kode_Unik_Asal, '" & PalletTujuan & "', batch_number, '" & warnaLama & "', Tgl_Masuk, 'Y' "
                                            SQL = SQL & "from Barang_SN "
                                            SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' "
                                            SQL = SQL & "and Kode_Stock_Owner='" & KdSo & "' "
                                            SQL = SQL & "and Kode_Barang='" & KdBarang & "' "
                                            SQL = SQL & "and Serial_Number='" & SN & "' "
                                            ExecuteTrans(SQL)

                                            '============================
                                            '=       TAMBAH STOCK       =
                                            '============================

                                            SQL = "update barang set Good_Stock= Good_Stock + Round(" & nilai_kecildetail & ",4), Jumlah_Bags = Jumlah_Bags + " & JumlahBagsEstimasi & " "
                                            SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & KdSo & "' "
                                            SQL = SQL & " and Kode_Barang='" & KdBarang & "'"
                                            ExecuteTrans(SQL)

                                            'CEK KESESUAIAN STOCK
                                            SQL = "SELECT round(SUM(good_stock),4) AS good_stock, isnull((select round(sum(jumlah),4) from Barang_sn x "
                                            SQL = SQL & "where a.kode_Barang=x.kode_Barang and a.Kode_Stock_Owner=x.kode_Stock_Owner "
                                            SQL = SQL & "and a.kode_Perusahaan=x.kode_Perusahaan ),0) as Jumlah_sn, "
                                            SQL = SQL & "isnull(round(SUM(jumlah_bags), 2), 0) AS jumlah_bags_barang, "
                                            SQL = SQL & "isnull((select round(sum(Jumlah_Bags), 2) from Barang_sn y "
                                            SQL = SQL & "where a.kode_Barang=y.kode_Barang and a.Kode_Stock_Owner=y.kode_Stock_Owner and a.kode_Perusahaan=y.kode_Perusahaan ), 0) as jumlah_bags_sn "
                                            SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner = '" & KdSo & "' "
                                            SQL = SQL & "AND a.Kode_Barang = '" & KdBarang & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
                                            SQL = SQL & "group by a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan "
                                            Using Ds3 = BindingTrans(SQL)
                                                With Ds3.Tables("MyTable")
                                                    If Ds3.Tables("MyTable").Rows.Count <> 0 Then
                                                        If Ds3.Tables("MyTable").Rows(0).Item("good_stock") <> Ds3.Tables("MyTable").Rows(0).Item("Jumlah_sn") Or Ds3.Tables("MyTable").Rows(0).Item("jumlah_bags_barang") <> Ds3.Tables("MyTable").Rows(0).Item("jumlah_bags_sn") Then
                                                            CloseTrans()
                                                            CloseConn()
                                                            'MessageBox.Show("Terjadi Kesalahan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                            Dim Lvw As ListViewItem
                                                            Lvw = ListView1.Items.Add("Automatization - Waste Process: Stock Tidak Sesuai")
                                                            Exit Sub
                                                        End If
                                                    Else
                                                        CloseTrans()
                                                        CloseConn()
                                                        'MessageBox.Show("Data tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                        Dim Lvw As ListViewItem
                                                        Lvw = ListView1.Items.Add("Automatization - Waste Process: Data tidak ditemukan . . ! !")
                                                        Exit Sub
                                                    End If
                                                End With
                                            End Using

#Region "Jurnal"

                                            'dari
                                            Dim inisial_faktur_dari As String = ""
                                            Dim akun_persediaan_dari As String = ""
                                            Dim akun_persediaan_tujuan As String = ""

                                            SQL = "select inisial_faktur,Persediaan_Bahan_Baku,Persediaan,Persediaan_Bahan_Setengah_Jadi,Persediaan_Scrap, Persediaan_Packaging from stock_owner_gudang "
                                            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & KdSo & "' "
                                            Using Dr = OpenTrans(SQL)
                                                If Dr.Read Then
                                                    'akun_persediaan_dari = Dr("persediaan")
                                                    inisial_faktur_dari = Dr("inisial_faktur")
                                                Else
                                                    Dr.Close()
                                                    CloseTrans()
                                                    CloseConn()
                                                    'MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                    Dim Lvw As ListViewItem
                                                    Lvw = ListView1.Items.Add("Automatization - Waste Process: Data akun tidak ditemukan!")
                                                    Exit Sub
                                                End If
                                            End Using

                                            SQL = "select c.akun_Persediaan "
                                            SQL = SQL & "from EMI_Group_Jenis a, Barang b, EMI_Group_Jenis_Akun c where "
                                            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Group_Jenis = b.Id_Group_Jenis and "
                                            SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Group_Jenis = c.Id_Group_Jenis and "
                                            SQL = SQL & "b.kode_stock_owner = c.kode_stock_owner and b.Kode_Perusahaan = '" & KodePerusahaan & "' "
                                            SQL = SQL & "and b.kode_stock_owner = '" & KdSo & "' and b.Kode_Barang='" & KdBarang & "' "
                                            Using Dr = OpenTrans(SQL)
                                                If Dr.Read Then
                                                    akun_persediaan_dari = Dr("akun_Persediaan")
                                                Else
                                                    Dr.Close()
                                                    CloseTrans()
                                                    CloseConn()
                                                    'MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                    Dim Lvw As ListViewItem
                                                    Lvw = ListView1.Items.Add("Automatization - Waste Process: Data akun tidak ditemukan!")
                                                    Exit Sub
                                                End If
                                            End Using

                                            SQL = "select c.akun_Persediaan "
                                            SQL = SQL & "from EMI_Group_Jenis a, Barang b, EMI_Group_Jenis_Akun c where "
                                            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Group_Jenis = b.Id_Group_Jenis and "
                                            SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Group_Jenis = c.Id_Group_Jenis and "
                                            SQL = SQL & "b.kode_stock_owner = c.kode_stock_owner and b.Kode_Perusahaan = '" & KodePerusahaan & "' "
                                            SQL = SQL & "and b.kode_stock_owner = '" & KdSo & "' and b.Kode_Barang='" & KdBarang & "' "
                                            Using Dr = OpenTrans(SQL)
                                                If Dr.Read Then
                                                    akun_persediaan_tujuan = Dr("akun_Persediaan")
                                                Else
                                                    Dr.Close()
                                                    CloseTrans()
                                                    CloseConn()
                                                    'MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                    Dim Lvw As ListViewItem
                                                    Lvw = ListView1.Items.Add("Automatization - Waste Process: Data akun tidak ditemukan!")
                                                    Exit Sub
                                                End If
                                            End Using

                                            Dim Kode_voucher As String = ""
                                            Kode_voucher = GetLastNumberJurnal(Format(tgl_skg, "yyyyMM"), "JS" & inisial_faktur_dari, KodePerusahaan)
                                            Dim pagenumber As Integer = 1

                                            SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
                                            SQL = SQL & "Keterangan, JudulBank, KetDK, userid) values("
                                            SQL = SQL & "'" & Kode_voucher & "', "
                                            SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', "
                                            SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
                                            SQL = SQL & "'" & KodeProyek & "', 'Transfer Stock " & No_Faktur_Waste & "', '', "
                                            SQL = SQL & "'-', '" & UserID & "')"
                                            ExecuteTrans(SQL)

                                            SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_persediaan_dari, 1),
                              Strings.Mid(akun_persediaan_dari, 2, 1),
                              Strings.Mid(Ganti(akun_persediaan_dari), 3),
                              KodePerusahaan, KodeProyek, "Persedian " & No_Faktur_Waste, "0", nilai_persediaan_min, pagenumber, KdSo, Bahasa_Pilihan, Ket_Cost_Center_HO)
                                            ExecuteTrans(SQL)
                                            pagenumber = pagenumber + 1

                                            SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_persediaan_tujuan, 1),
                             Strings.Mid(akun_persediaan_tujuan, 2, 1),
                             Strings.Mid(Ganti(akun_persediaan_tujuan), 3),
                             KodePerusahaan, KodeProyek, "Persedian " & No_Faktur_Waste, nilai_persediaan_min, "0", pagenumber, KdSo, Bahasa_Pilihan, Ket_Cost_Center_HO)
                                            ExecuteTrans(SQL)
                                            pagenumber = pagenumber + 1

                                            SQL = "select sum(debit) as debit, sum(kredit) as kredit from detail_jurnal where "
                                            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                            SQL = SQL & "kode_voucher = '" & Kode_voucher & "'"
                                            Using Dr = OpenTrans(SQL)
                                                If Dr.Read Then
                                                    If Dr("debit") <> Dr("kredit") Then
                                                        Dr.Close()
                                                        CloseTrans()
                                                        CloseConn()
                                                        'MessageBox.Show("Jurnal salah!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                        Dim Lvw As ListViewItem
                                                        Lvw = ListView1.Items.Add("Automatization - Waste Process: Jurnal salah!")
                                                        Exit Sub
                                                    End If
                                                Else
                                                    Dr.Close()
                                                    CloseTrans()
                                                    CloseConn()
                                                    'MessageBox.Show("Data jurnal tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                    Dim Lvw As ListViewItem
                                                    Lvw = ListView1.Items.Add("Automatization - Waste Process: Data jurnal tidak ditemukan!")
                                                    Exit Sub
                                                End If
                                            End Using

#End Region

                                            SQL = "insert into N_EMI_Transaksi_Transfer_Waste_Det2(kode_perusahaan, No_faktur, Urut_Det, No_Pallet, "
                                            SQL = SQL & "Serial_Number, Jumlah, UserID, Tanggal, Jam, Kode_Voucher, Jumlah_Bags) values( "
                                            SQL = SQL & "'" & KodePerusahaan & "', '" & No_Faktur_Waste & "', '" & UrutOto & "', "
                                            SQL = SQL & "'" & PalletTujuan & "', '" & SN_Baru & "', '" & nilai_kecildetail & "', "
                                            SQL = SQL & "'" & UserID & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', "
                                            SQL = SQL & "'" & Kode_voucher & "', '" & JumlahBagsEstimasi & "') "
                                            ExecuteTrans(SQL)

                                            SQL = "update N_EMI_Transaksi_Transfer_Waste_Det set  "
                                            SQL = SQL & "Selesai = 'Y' "
                                            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
                                            SQL = SQL & "and urut_oto = '" & UrutOto & "' "
                                            ExecuteTrans(SQL)

                                        Next
                                    End If
                                End Using

#End Region

                                SQL = "update N_EMI_Transaksi_Approval_Waste set Flag_Selesai = 'Y' "
                                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and Status is null and No_Transaksi = '" & No_Transaksi_Approval & "' and No_Faktur_Waste = '" & No_Faktur_Waste & "' "
                                ExecuteTrans(SQL)

                            End If

                        Next
                    End If
                End With
            End Using

            Cmd.Transaction.Commit()
            CloseConn()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Dim Lvw As ListViewItem
            Lvw = ListView1.Items.Add("Automatization - Waste Process: " & ex.Message)
            Exit Sub
        End Try


    End Sub

    Private Sub Handle_Proses_Waste_Product()

        get_jam()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            '========================================
            '=     GET APPROVAL LEVEL TERTINGGI     =
            '========================================
            Dim ApprovalLevelTertinggi As Integer = 0
            SQL = "select isnull(max(Approval_Level), 0) as LevelTertinggi "
            SQL = SQL & "from N_EMI_Transaksi_Approval_Waste "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and Flag_Approve is null and Jenis_Approval = 'Waste_Produk' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    ApprovalLevelTertinggi = Dr("LevelTertinggi")
                End If
            End Using

            For i As Integer = 1 To ApprovalLevelTertinggi

                SQL = "select a.No_Transaksi, a.No_Faktur_Waste, a.ID_User_Android_Approve, b.No_HP, a.Flag_Approve, a.Flag_Sudah_Kirim_WA, "
                SQL = SQL & "b.No_HP, a.tanggal, a.Jam, "
                SQL = SQL & "case when b.Jenis_Approval = 'Waste_Process' then 'Waste Process' "
                SQL = SQL & "when b.Jenis_Approval = 'Waste_Produk' then 'Waste Produk' end as Jenis_Pemusnahan, "
                SQL = SQL & "a.No_Berita_Acara "
                SQL = SQL & "from N_EMI_Transaksi_Approval_Waste a "
                SQL = SQL & "inner join N_EMI_Master_Hierarchy_Approval_Waste b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.ID_User_Android_Approve = b.ID_User_Android and a.Jenis_Approval = b.Jenis_Approval and a.kode_Stock_owner = b.kode_Stock_owner "
                SQL = SQL & "where a.Status is null "
                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and a.Approval_Level = " & i & " "
                SQL = SQL & "and a.Flag_Approve is null "
                SQL = SQL & "and a.Jenis_Approval = 'Waste_Produk' "
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        If .Rows.Count <> 0 Then
                            For j As Integer = 0 To .Rows.Count - 1

                                If i > 1 Then

                                    '=====================================================
                                    '=     CEK APAKAH LEVEL SEBELUMNYA SUDAH APPROVE     =
                                    '=====================================================
                                    SQL = "select top 1 a.No_Transaksi, a.No_Faktur_Waste, a.ID_User_Android_Approve, b.No_HP, a.Flag_Approve, a.Flag_Sudah_Kirim_WA "
                                    SQL = SQL & "from N_EMI_Transaksi_Approval_Waste a "
                                    SQL = SQL & "inner join N_EMI_Master_Hierarchy_Approval_Waste b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.ID_User_Android_Approve = b.ID_User_Android and a.Jenis_Approval = b.Jenis_Approval and a.kode_Stock_owner = b.kode_Stock_owner "
                                    SQL = SQL & "where a.Status is null "
                                    SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                                    SQL = SQL & "and a.Approval_Level = " & i - 1 & " "
                                    SQL = SQL & "and a.No_Transaksi = '" & .Rows(j).Item("No_Transaksi") & "' "
                                    SQL = SQL & "and a.No_Faktur_Waste = '" & .Rows(j).Item("No_Faktur_Waste") & "' "
                                    SQL = SQL & "and a.Flag_Sudah_Kirim_WA = 'Y' "
                                    SQL = SQL & "and a.Flag_Approve = 'Y' "
                                    SQL = SQL & "and a.Jenis_Approval = 'Waste_Produk' "
                                    Using Dr = OpenTrans(SQL)
                                        If Not Dr.Read Then
                                            Continue For
                                        End If
                                    End Using

                                End If

                                If General_Class.CekNULL(.Rows(j).Item("Flag_Sudah_Kirim_WA")) = "" Then

                                    Dim NoHp As String = .Rows(j).Item("No_HP")
                                    'Dim NoHp As String = "6285117547880"
                                    'Dim Jenis_Pemusnahan As String = .Rows(j).Item("Jenis_Pemusnahan")
                                    'Dim No_Ba As String = .Rows(j).Item("No_Berita_Acara")
                                    'Dim No_Faktur_Waste As String = .Rows(j).Item("No_Faktur_Waste")

                                    Dim Jenis_Pemusnahan As String = If(General_Class.CekNULL(.Rows(j).Item("Jenis_Pemusnahan")) = "", "-", .Rows(j).Item("Jenis_Pemusnahan"))
                                    Dim No_Ba As String = If(General_Class.CekNULL(.Rows(j).Item("No_Berita_Acara")) = "", "-", .Rows(j).Item("No_Berita_Acara"))
                                    Dim No_Faktur_Waste As String = If(General_Class.CekNULL(.Rows(j).Item("No_Faktur_Waste")) = "", "-", .Rows(j).Item("No_Faktur_Waste"))

                                    Dim Total As Double = 0
                                    Dim Satuan As String = ""
                                    SQL = "select b.Total, b.Satuan "
                                    SQL &= $"from N_EMI_Transaksi_Transfer_Waste_Produk a "
                                    SQL &= $"inner join N_EMI_Transaksi_Transfer_Waste_Produk_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
                                    SQL &= $"where a.status is NULL "
                                    SQL &= $"and a.Kode_Perusahaan = '{KodePerusahaan}' "
                                    SQL &= $"and a.No_Faktur = '{ .Rows(j).Item("No_Faktur_Waste")}' "
                                    Using Dr = OpenTrans(SQL)
                                        If Dr.Read Then
                                            Total = Format(Dr("Total"), "N4")
                                            Satuan = Dr("Satuan")
                                        End If
                                    End Using




                                    Dim payload As String = "{
                                        ""messaging_product"": ""whatsapp"",
                                        ""to"": """ & NoHp & """,
                                        ""type"": ""template"",
                                        ""template"": {
                                            ""name"": ""emi_approval_pemushahan"",
                                            ""language"": {
                                                ""code"": ""id""
                                            },
                                            ""components"": [
                                                {
                                                    ""type"": ""body"",
                                                    ""parameters"": [
                                                        {""type"": ""text"", ""text"": """ & No_Ba & """},
                                                        {""type"": ""text"", ""text"": """ & No_Faktur_Waste & """},
                                                        {""type"": ""text"", ""text"": """ & Format(.Rows(j).Item("tanggal"), "dd MMM yyyy") & ", " & .Rows(j).Item("Jam") & """},
                                                        {""type"": ""text"", ""text"": """ & Jenis_Pemusnahan & """},
                                                        {""type"": ""text"", ""text"": """ & Format(Total, "N4") & " " & Satuan & """}
                                                    ]
                                                }
                                                
                                            ]
                                        }
                                    }"



                                    Dim hasil As String = Helper_API.CallAPI(Url_WA_Business, "POST", Token_WA_Business, payload)

                                    If hasil.StartsWith("API Error") OrElse hasil.StartsWith("Error") Then
                                        ' Ambil hanya isi pesan setelah titik dua
                                        Dim parts() As String = hasil.Split(New Char() {":"c}, 2, StringSplitOptions.None)
                                        Dim msg As String = If(parts.Length > 1, parts(1).Trim(), hasil)

                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Terjadi error waktu call API: " & msg)
                                        Exit Sub
                                    End If


                                    SQL = "update N_EMI_Transaksi_Approval_Waste set Flag_Sudah_Kirim_WA = 'Y' "
                                    SQL &= $"where kode_perusahaan = '{KodePerusahaan}' "
                                    SQL &= $"and Flag_Sudah_Kirim_WA is NULL "
                                    SQL &= $"and Flag_Approve is null "
                                    SQL &= $"and No_Faktur_Waste = '{ .Rows(j).Item("No_Faktur_Waste")}' "
                                    SQL &= $"and No_Transaksi = '{ .Rows(j).Item("No_Transaksi")}' "
                                    SQL &= $"and Approval_Level = '{i}' "
                                    ExecuteTrans(SQL)

                                    Continue For

                                End If

                            Next
                        End If
                    End With
                End Using

            Next

            SQL = "select distinct a.No_Transaksi, a.No_Faktur_Waste, a.Jenis_Approval, "
            SQL = SQL & "case when exists ( select 1 from N_EMI_Transaksi_Approval_Waste z "
            SQL = SQL & "where z.Kode_Perusahaan = a.Kode_Perusahaan "
            SQL = SQL & "and z.No_Transaksi = a.No_Transaksi "
            SQL = SQL & "and z.No_Faktur_Waste = a.No_Faktur_Waste "
            SQL = SQL & "and z.Flag_Approve IS NULL "
            SQL = SQL & ") then 'T' else 'Y' end as isApprovedAll "
            SQL = SQL & "from N_EMI_Transaksi_Approval_Waste a "
            SQL = SQL & "where a.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.Jenis_Approval = 'Waste_Produk' "
            SQL = SQL & "and a.Flag_Selesai is null "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            If .Rows(i).Item("isApprovedAll") = "Y" Then

                                Dim No_Faktur_Waste As String = .Rows(i).Item("No_Faktur_Waste")
                                Dim No_Transaksi_Approval As String = .Rows(i).Item("No_Transaksi")
                                Dim SoTujuan As String = ""
                                Dim SoAwal As String = ""

#Region "PROSES WASTE DI SINI"

                                Dim QrLama As String = ""
                                Dim expDate As String = ""
                                Dim batchLama As String = ""
                                Dim tglMsk As String = ""
                                Dim SN As String = ""
                                Dim UrutOto As String = ""
                                Dim IdWarehouseTujuan As String = ""
                                Dim PalletTujuan As String = ""
                                Dim KdBarang As String = ""
                                Dim KdSo As String = ""
                                Dim KdSoTujuan As String = ""
                                Dim SatuanBesar As String = ""
                                Dim SatuanKecil As String = ""
                                Dim JumlahEstimasi As String = ""
                                Dim JumlahBagsEstimasi As String = ""
                                Dim No_Faktur_Waste_Product As String = ""

                                SQL = "select a.No_Faktur, a.Kode_Stock_Owner, a.Kode_Stock_Owner_Tujuan, b.Kode_Barang,c.Jumlah, c.Jumlah_Bags, b.Satuan, c.Serial_Number_Awal, "
                                SQL = SQL & "b.Satuan_Barang, c.Urut_Oto, c.Warna, c.Id_Wms_Tujuan, d.Qr_Code, d.Batch_Number, d.Tgl_Expired, d.Tgl_Masuk "
                                SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste_Produk a "
                                SQL = SQL & "inner join N_EMI_Transaksi_Transfer_Waste_Produk_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
                                SQL = SQL & "inner join N_EMI_Transaksi_Transfer_Waste_Produk_Det c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.Urut_TF "
                                SQL = SQL & "inner join Barang_SN d on c.Kode_Perusahaan = d.Kode_Perusahaan and a.Kode_Stock_Owner = d.Kode_Stock_Owner and b.Kode_Barang = d.Kode_Barang and c.Serial_Number_Awal = d.Serial_Number "
                                SQL = SQL & "where a.Status is null "
                                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                                SQL = SQL & "and a.No_Faktur = '" & No_Faktur_Waste & "' "
                                Using Ds2 = BindingTrans(SQL)
                                    If Ds2.Tables("MyTable").Rows.Count <> 0 Then
                                        For j As Integer = 0 To Ds2.Tables("MyTable").Rows.Count - 1

                                            No_Faktur_Waste_Product = General_Class.CekNULL(Ds2.Tables("MyTable").Rows(j).Item("No_Faktur"))
                                            QrLama = General_Class.CekNULL(Ds2.Tables("MyTable").Rows(j).Item("Qr_Code"))
                                            batchLama = General_Class.CekNULL(Ds2.Tables("MyTable").Rows(j).Item("Batch_Number"))
                                            SN = Ds2.Tables("MyTable").Rows(j).Item("Serial_Number_Awal")
                                            expDate = General_Class.CekNULL(Ds2.Tables("MyTable").Rows(j).Item("Tgl_Expired"))
                                            tglMsk = General_Class.CekNULL(Ds2.Tables("MyTable").Rows(j).Item("Tgl_Masuk"))
                                            UrutOto = General_Class.CekNULL(Ds2.Tables("MyTable").Rows(j).Item("Urut_Oto"))
                                            IdWarehouseTujuan = General_Class.CekNULL(Ds2.Tables("MyTable").Rows(j).Item("Id_Wms_Tujuan"))
                                            KdBarang = General_Class.CekNULL(Ds2.Tables("MyTable").Rows(j).Item("Kode_Barang"))
                                            KdSo = General_Class.CekNULL(Ds2.Tables("MyTable").Rows(j).Item("Kode_Stock_Owner"))
                                            KdSoTujuan = General_Class.CekNULL(Ds2.Tables("MyTable").Rows(j).Item("Kode_Stock_Owner_Tujuan"))
                                            SatuanBesar = General_Class.CekNULL(Ds2.Tables("MyTable").Rows(j).Item("Satuan"))
                                            SatuanKecil = General_Class.CekNULL(Ds2.Tables("MyTable").Rows(j).Item("Satuan_Barang"))
                                            JumlahEstimasi = HilangkanTanda(Ds2.Tables("MyTable").Rows(j).Item("Jumlah"))
                                            JumlahBagsEstimasi = HilangkanTanda(Ds2.Tables("MyTable").Rows(j).Item("Jumlah_Bags"))

                                            SoTujuan = General_Class.CekNULL(Ds2.Tables("MyTable").Rows(j).Item("Kode_Stock_Owner_Tujuan"))
                                            SoAwal = General_Class.CekNULL(Ds2.Tables("MyTable").Rows(j).Item("Kode_Stock_Owner"))

                                            SQL = "select a.Status, c.Selesai, b.Flag_Timbang "
                                            SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste_Produk a, N_EMI_Transaksi_Transfer_Waste_Produk_Detail b, N_EMI_Transaksi_Transfer_Waste_Produk_Det c "
                                            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.no_Faktur = b.No_Faktur and "
                                            SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan and b.no_Faktur = c.No_Faktur and b.urut_oto=c.urut_TF "
                                            SQL = SQL & "and a.No_Faktur = '" & No_Faktur_Waste & "' and c.urut_oto = '" & UrutOto & "'  "
                                            Using Dr = OpenTrans(SQL)
                                                If Dr.Read Then

                                                    If General_Class.CekNULL(Dr("status")) <> "" Then
                                                        Dr.Close()
                                                        CloseTrans()
                                                        CloseConn()
                                                        'MessageBox.Show("Proses tidak bisa dilanjutkan, barang sudah dibatalkan!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                        Dim Lvw As ListViewItem
                                                        Lvw = ListView1.Items.Add("Automatization - Waste Product: Proses tidak bisa dilanjutkan, barang sudah dibatalkan!!")
                                                        Exit Sub
                                                    ElseIf General_Class.CekNULL(Dr("selesai")) = "Y" Then
                                                        Dr.Close()
                                                        CloseTrans()
                                                        CloseConn()
                                                        'MessageBox.Show("Terjadi kesalahan, barang sudah selesai diproses!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                        Dim Lvw As ListViewItem
                                                        Lvw = ListView1.Items.Add("Automatization - Waste Product: Terjadi kesalahan, barang sudah selesai diproses!")
                                                        Exit Sub
                                                    ElseIf General_Class.CekNULL(Dr("Flag_Timbang")) = "Y" Then
                                                        Dr.Close()
                                                        CloseTrans()
                                                        CloseConn()
                                                        'MessageBox.Show("Terjadi kesalahan, ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                        Dim Lvw As ListViewItem
                                                        Lvw = ListView1.Items.Add("Automatization - Waste Product: Terjadi kesalahan, barang sudah Ditimbang!")
                                                        Exit Sub
                                                    End If
                                                Else
                                                    Dr.Close()
                                                    CloseTrans()
                                                    CloseConn()
                                                    MessageBox.Show("Data barang tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                    Dim Lvw As ListViewItem
                                                    Lvw = ListView1.Items.Add("Automatization - Waste Product: Data barang tidak ditemukan!")
                                                    Exit Sub
                                                End If
                                            End Using

                                            SQL = "Select Top(1) nomor_urut from view_warehouse_position_detail where "
                                            SQL = SQL & "kode_Perusahaan ='" & KodePerusahaan & "' and kode_barang is null and "
                                            SQL = SQL & "id_wms_warehouse_position = '" & IdWarehouseTujuan & "' "
                                            SQL = SQL & "order by nomor_urut "
                                            Using dr = OpenTrans(SQL)
                                                If dr.Read Then
                                                    PalletTujuan = dr("nomor_urut")
                                                Else
                                                    dr.Close()
                                                    CloseTrans()
                                                    CloseConn()
                                                    'MessageBox.Show("data Rak Sudah Penuh . . ! ! ")
                                                    Dim Lvw As ListViewItem
                                                    Lvw = ListView1.Items.Add("Automatization - Waste Product: data Rak Sudah Penuh . . ! ! ")
                                                    Exit Sub
                                                End If
                                            End Using

                                            '====================================
                                            '=       CONVERT SATUAN KECIL       =
                                            '====================================
                                            Dim nilai_kecildetail As Double = 0
                                            SQL = "select dbo.ubah_satuan('" & KodePerusahaan & "', 'masa','" & KdBarang & "', '" & SatuanBesar & "',"
                                            SQL = SQL & "'" & SatuanKecil & "', '" & JumlahEstimasi & "' ) as hasil"
                                            Using Dr1 = OpenTrans(SQL)
                                                If Dr1.Read Then
                                                    If General_Class.CekNULL(Dr1("hasil")) = "" Then
                                                        Dr1.Close()
                                                        CloseTrans()
                                                        CloseConn()
                                                        'MessageBox.Show("data konversi satuan kirim tidak ada ")
                                                        Dim Lvw As ListViewItem
                                                        Lvw = ListView1.Items.Add("Automatization - Waste Product: data konversi satuan kirim tidak ada ")
                                                        Exit Sub
                                                    End If

                                                    nilai_kecildetail = Dr1("hasil")
                                                Else
                                                    Dr1.Close()
                                                    CloseTrans()
                                                    CloseConn()
                                                    'MessageBox.Show("data konversi satuan kirim tidak ada ")
                                                    Dim Lvw As ListViewItem
                                                    Lvw = ListView1.Items.Add("Automatization - Waste Product: data konversi satuan kirim tidak ada ")
                                                    Exit Sub
                                                End If
                                            End Using

                                            '============================
                                            '=       POTONG STOCK       =
                                            '============================

                                            Dim nilai_persediaan_min As Double = 0
                                            SQL = "select round(dbo.get_hpp(serial_number) * " & JumlahEstimasi & ", 2) as rp_persediaan_min from barang_sn where "
                                            SQL = SQL & "Kode_Stock_Owner='" & KdSo & "' and Kode_Barang='" & KdBarang & "' "
                                            SQL = SQL & "and Serial_Number='" & SN & "'"
                                            Using dr = OpenTrans(SQL)
                                                If dr.Read Then
                                                    nilai_persediaan_min = dr("rp_persediaan_min")
                                                Else
                                                    dr.Close()
                                                    CloseTrans()
                                                    CloseConn()
                                                    'MessageBox.Show("Data SN tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                    Dim Lvw As ListViewItem
                                                    Lvw = ListView1.Items.Add("Automatization - Waste Product: Data SN tidak ditemukan!")
                                                    Exit Sub
                                                End If
                                            End Using

                                            Dim Nama As String = ""
                                            'Dim jumlahAkhir As Double = Val(dgv_GoodStock) - Val(dgv_Jumlah)
                                            SQL = "select Nama, Kode_Barang, round(good_stock,4) as good_stock, Jumlah_Bags from Barang where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & KdSo & "' "
                                            SQL = SQL & "and Kode_Barang='" & KdBarang & "' "
                                            Using dr = OpenTrans(SQL)
                                                If dr.Read Then
                                                    Nama = dr("Kode_Barang")
                                                    If dr("good_stock") < JumlahEstimasi Then
                                                        dr.Close()
                                                        CloseTrans()
                                                        CloseConn()
                                                        'MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat stock " & Nama & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                                        Dim Lvw As ListViewItem
                                                        Lvw = ListView1.Items.Add("Automatization - Waste Product: Proses tidak dapat dilanjutkan karena akan membuat stock " & Nama & " menjadi negatif.")
                                                        Exit Sub
                                                    ElseIf dr("Jumlah_Bags") < JumlahBagsEstimasi Then
                                                        dr.Close()
                                                        CloseTrans()
                                                        CloseConn()
                                                        'MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat jumlah bags " & Nama & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                                        Dim Lvw As ListViewItem
                                                        Lvw = ListView1.Items.Add("Automatization - Waste Product: Proses tidak dapat dilanjutkan karena akan membuat jumlah bags " & Nama & " menjadi negatif.")
                                                        Exit Sub
                                                    Else
                                                        dr.Close()
                                                        SQL = "update barang set Good_Stock = Good_Stock - Round(" & JumlahEstimasi & ",4), Jumlah_Bags = Jumlah_Bags - " & JumlahBagsEstimasi & " "
                                                        SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & KdSo & "' "
                                                        SQL = SQL & " and Kode_Barang='" & KdBarang & "'"
                                                        ExecuteTrans(SQL)
                                                    End If
                                                Else
                                                    dr.Close()
                                                    CloseTrans()
                                                    CloseConn()
                                                    'MessageBox.Show("Barang " & Nama & " tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                    Dim Lvw As ListViewItem
                                                    Lvw = ListView1.Items.Add("Automatization - Waste Product: Barang " & Nama & " tidak ditemukan!")
                                                    Exit Sub
                                                End If
                                            End Using

                                            SQL = "select round(jumlah,4) as jumlah, Jumlah_Bags from Barang_SN where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & KdSo & "' "
                                            SQL = SQL & "and Kode_Barang='" & KdBarang & "' "
                                            SQL = SQL & "and Serial_Number='" & SN & "'"
                                            Using dr = OpenTrans(SQL)
                                                If dr.Read Then
                                                    If dr("jumlah") < JumlahEstimasi Then
                                                        dr.Close()
                                                        CloseTrans()
                                                        CloseConn()
                                                        'MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat stock " & Nama & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                                        Dim Lvw As ListViewItem
                                                        Lvw = ListView1.Items.Add("Automatization - Waste Product: Proses tidak dapat dilanjutkan karena akan membuat stock " & Nama & " menjadi negatif.")
                                                        Exit Sub
                                                    ElseIf dr("Jumlah_Bags") < JumlahBagsEstimasi Then
                                                        dr.Close()
                                                        CloseTrans()
                                                        CloseConn()
                                                        'MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat jumlah bags " & Nama & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                                        Dim Lvw As ListViewItem
                                                        Lvw = ListView1.Items.Add("Automatization - Waste Product: Proses tidak dapat dilanjutkan karena akan membuat jumlah bags " & Nama & " menjadi negatif.")
                                                        Exit Sub
                                                    Else
                                                        dr.Close()
                                                        SQL = "update barang_sn set jumlah = jumlah - Round(" & JumlahEstimasi & ",4), Jumlah_Bags = Jumlah_Bags - " & JumlahBagsEstimasi & " "
                                                        SQL = SQL & "where Kode_Stock_Owner='" & KdSo & "' and Kode_Barang='" & KdBarang & "' "
                                                        SQL = SQL & "and Serial_Number='" & SN & "'"
                                                        ExecuteTrans(SQL)
                                                    End If
                                                Else
                                                    dr.Close()
                                                    CloseTrans()
                                                    CloseConn()
                                                    'MessageBox.Show("Barang " & Nama & " tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                    Dim Lvw As ListViewItem
                                                    Lvw = ListView1.Items.Add("Automatization - Waste Product: Barang " & Nama & " tidak ditemukan!")
                                                    Exit Sub
                                                End If
                                            End Using

                                            '====================================
                                            '=       CEK KESESUAIAN STOCK       =
                                            '====================================
                                            SQL = "SELECT round(SUM(good_stock),4) AS good_stock, isnull((select round(sum(jumlah),4) from Barang_sn x "
                                            SQL = SQL & "where a.kode_Barang=x.kode_Barang and a.Kode_Stock_Owner=x.kode_Stock_Owner "
                                            SQL = SQL & "and a.kode_Perusahaan=x.kode_Perusahaan ),0) as Jumlah_sn, "
                                            SQL = SQL & "isnull(round(SUM(jumlah_bags), 2), 0) AS jumlah_bags_barang, "
                                            SQL = SQL & "isnull((select round(sum(Jumlah_Bags), 2) from Barang_sn y "
                                            SQL = SQL & "where a.kode_Barang=y.kode_Barang and a.Kode_Stock_Owner=y.kode_Stock_Owner and a.kode_Perusahaan=y.kode_Perusahaan ), 0) as jumlah_bags_sn "
                                            SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner = '" & KdSo & "' "
                                            SQL = SQL & "AND a.Kode_Barang = '" & KdBarang & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
                                            SQL = SQL & "group by a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan "
                                            Using Ds7 = BindingTrans(SQL)
                                                If Ds7.Tables("MyTable").Rows.Count <> 0 Then
                                                    If Ds7.Tables("MyTable").Rows(0).Item("good_stock") <> Ds7.Tables("MyTable").Rows(0).Item("Jumlah_sn") Or Ds7.Tables("MyTable").Rows(0).Item("jumlah_bags_barang") <> Ds7.Tables("MyTable").Rows(0).Item("jumlah_bags_sn") Then
                                                        CloseTrans()
                                                        CloseConn()
                                                        'MessageBox.Show("Terjadi Kesalahan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                        Dim Lvw As ListViewItem
                                                        Lvw = ListView1.Items.Add("Automatization - Waste Product: Terjadi Kesalahan . . ! !, Stock Tidak Sama Saat Potong Stock")
                                                        Exit Sub
                                                    End If
                                                Else
                                                    CloseTrans()
                                                    CloseConn()
                                                    'MessageBox.Show("Data tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                    Dim Lvw As ListViewItem
                                                    Lvw = ListView1.Items.Add("Automatization - Waste Product: Data tidak ditemukan . . ! !")
                                                    Exit Sub
                                                End If
                                            End Using

                                            '==============================
                                            '=       INSERT SN BARU       =
                                            '==============================

                                            Dim hargaIsn As String = ""
                                            Dim namaBarang As String = ""
                                            Dim warnaLama As String = ""

                                            'Ambil Data Lama
                                            SQL = "select a.Serial_Number, a.Qr_Code, a.Kode_Unik_Berjalan, b.Nama, a.Batch_Number, a.Tgl_Expired, a.warna "
                                            SQL = SQL & "from barang_sn a, barang b "
                                            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
                                            SQL = SQL & "and a.Kode_Stock_Owner = b.Kode_Stock_Owner "
                                            SQL = SQL & "and a.Kode_Barang = b.Kode_Barang "
                                            SQL = SQL & "and a.Kode_Perusahaan='" & KodePerusahaan & "' "
                                            SQL = SQL & "and a.Kode_Stock_Owner='" & KdSo & "' "
                                            SQL = SQL & "and a.Kode_Barang ='" & KdBarang & "' "
                                            SQL = SQL & "and a.Serial_Number='" & SN & "' "
                                            'SQL = SQL & "and a.Jumlah <> 0 "
                                            Using Dr = OpenTrans(SQL)
                                                Do While Dr.Read
                                                    hargaIsn = Get_Harga_SN(Dr("Serial_Number"))
                                                    QrLama = General_Class.CekNULL(Dr("Qr_Code"))
                                                    batchLama = General_Class.CekNULL(Dr("Batch_Number"))
                                                    namaBarang = General_Class.CekNULL(Dr("Nama"))
                                                    expDate = General_Class.CekNULL(Dr("Tgl_Expired"))
                                                    warnaLama = General_Class.CekNULL(Dr("warna"))
                                                Loop
                                            End Using

                                            'GENERATE SN BARU
                                            Dim str As String = Format(Random.Next(0, 999), "000") & Format(tgl_skg, "HHmmss")
                                            Dim Kode_Unik As String = str.Substring(0, 5) & "BB" & Chr(64 + str.Substring(6, 1)) & str.Substring(6, Len(str) - 6)
                                            Dim SN_Baru As String = Kode_Unik & Tanda_SN & "01" & Tanda_SN & hargaIsn & Tanda_SN & "02" & Tanda_SN & Format(tgl_skg, "yyyy-MM-dd")

                                            Dim newKodeUnikBerjalan As String = Generate_Random_Kode(10)

                                            'INSERT BARANG SN BARU
                                            SQL = "insert into Barang_SN (Kode_Perusahaan, Kode_Stock_Owner, Kode_Barang, Serial_Number, Jumlah,  Jumlah_Bags, "
                                            SQL = SQL & "Tgl_Expired, Tgl_Produksi, Stock_PO, Stock_Inquiry, Id_Warehouse, id_Susunan, Qr_Code, Kode_Unik_Berjalan, Kode_Unik_Asal, Nomor_Pallet, batch_number, Warna, Tgl_masuk, Blok_SN) "
                                            SQL = SQL & "select Kode_Perusahaan, '" & KdSoTujuan & "', Kode_Barang, '" & SN_Baru & "', '" & nilai_kecildetail & "', " & JumlahBagsEstimasi & ", "
                                            SQL = SQL & "Tgl_Expired, Tgl_Produksi, Stock_PO, Stock_Inquiry, '" & IdWarehouseTujuan & "', id_Susunan , Qr_Code, '" & newKodeUnikBerjalan & "', "
                                            SQL = SQL & "Kode_Unik_Asal, '" & PalletTujuan & "', batch_number, '" & warnaLama & "', Tgl_Masuk, 'Y' "
                                            SQL = SQL & "from Barang_SN "
                                            SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' "
                                            SQL = SQL & "and Kode_Stock_Owner='" & KdSo & "' "
                                            SQL = SQL & "and Kode_Barang='" & KdBarang & "' "
                                            SQL = SQL & "and Serial_Number='" & SN & "' "
                                            ExecuteTrans(SQL)

                                            '============================
                                            '=       TAMBAH STOCK       =
                                            '============================

                                            SQL = "update barang set Good_Stock= Good_Stock + Round(" & nilai_kecildetail & ",4), Jumlah_Bags = Jumlah_Bags + " & JumlahBagsEstimasi & " "
                                            SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & KdSoTujuan & "' "
                                            SQL = SQL & " and Kode_Barang='" & KdBarang & "'"
                                            ExecuteTrans(SQL)

                                            'CEK KESESUAIAN STOCK
                                            SQL = "SELECT round(SUM(good_stock),4) AS good_stock, isnull((select round(sum(jumlah),4) from Barang_sn x "
                                            SQL = SQL & "where a.kode_Barang=x.kode_Barang and a.Kode_Stock_Owner=x.kode_Stock_Owner "
                                            SQL = SQL & "and a.kode_Perusahaan=x.kode_Perusahaan ),0) as Jumlah_sn, "
                                            SQL = SQL & "isnull(round(SUM(jumlah_bags), 2), 0) AS jumlah_bags_barang, "
                                            SQL = SQL & "isnull((select round(sum(Jumlah_Bags), 2) from Barang_sn y "
                                            SQL = SQL & "where a.kode_Barang=y.kode_Barang and a.Kode_Stock_Owner=y.kode_Stock_Owner and a.kode_Perusahaan=y.kode_Perusahaan ), 0) as jumlah_bags_sn "
                                            SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner = '" & KdSoTujuan & "' "
                                            SQL = SQL & "AND a.Kode_Barang = '" & KdBarang & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
                                            SQL = SQL & "group by a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan "
                                            Using Ds4 = BindingTrans(SQL)
                                                If Ds4.Tables("MyTable").Rows.Count <> 0 Then
                                                    If Ds4.Tables("MyTable").Rows(0).Item("good_stock") <> Ds4.Tables("MyTable").Rows(0).Item("Jumlah_sn") Or Ds4.Tables("MyTable").Rows(0).Item("jumlah_bags_barang") <> Ds4.Tables("MyTable").Rows(0).Item("jumlah_bags_sn") Then
                                                        CloseTrans()
                                                        CloseConn()
                                                        'MessageBox.Show("Terjadi Kesalahan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                        Dim Lvw As ListViewItem
                                                        Lvw = ListView1.Items.Add("Automatization - Waste Product: Terjadi Kesalahan . . ! !, Stock Tidak Sama Saat Potong Stock")
                                                        Exit Sub
                                                    End If
                                                Else
                                                    CloseTrans()
                                                    CloseConn()
                                                    'MessageBox.Show("Data tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                    Dim Lvw As ListViewItem
                                                    Lvw = ListView1.Items.Add("Automatization - Waste Product: Data tidak ditemukan . . ! , Saat Tambah Stock")
                                                    Exit Sub
                                                End If
                                            End Using

#Region "Jurnal"

                                            'dari
                                            Dim inisial_faktur_dari As String = ""
                                            Dim akun_persediaan_dari As String = ""
                                            Dim akun_persediaan_tujuan As String = ""

                                            SQL = "select inisial_faktur,Persediaan_Bahan_Baku,Persediaan,Persediaan_Bahan_Setengah_Jadi,Persediaan_Scrap, Persediaan_Packaging from stock_owner_gudang "
                                            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & KdSo & "' "
                                            Using Dr = OpenTrans(SQL)
                                                If Dr.Read Then
                                                    'akun_persediaan_dari = Dr("persediaan")
                                                    inisial_faktur_dari = Dr("inisial_faktur")
                                                Else
                                                    Dr.Close()
                                                    CloseTrans()
                                                    CloseConn()
                                                    'MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                    Dim Lvw As ListViewItem
                                                    Lvw = ListView1.Items.Add("Automatization - Waste Product: Data akun tidak ditemukan!")
                                                    Exit Sub
                                                End If
                                            End Using

                                            SQL = "select akun_gantung_waste_produk from stock_owner_gudang where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Stock_Owner = '" & KdSoTujuan & "' "
                                            Using Dr = OpenTrans(SQL)
                                                If Dr.Read Then
                                                    akun_persediaan_dari = Dr("akun_gantung_waste_produk")
                                                Else
                                                    Dr.Close()
                                                    CloseTrans()
                                                    CloseConn()
                                                    'MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                    Dim Lvw As ListViewItem
                                                    Lvw = ListView1.Items.Add("Automatization - Waste Product: Data akun tidak ditemukan!")
                                                    Exit Sub
                                                End If
                                            End Using

                                            SQL = "select c.akun_Persediaan "
                                            SQL = SQL & "from EMI_Group_Jenis a, Barang b, EMI_Group_Jenis_Akun c where "
                                            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Group_Jenis = b.Id_Group_Jenis and "
                                            SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Group_Jenis = c.Id_Group_Jenis and "
                                            SQL = SQL & "b.kode_stock_owner = c.kode_stock_owner and b.Kode_Perusahaan = '" & KodePerusahaan & "' "
                                            SQL = SQL & "and b.kode_stock_owner = '" & KdSo & "' and b.Kode_Barang='" & KdBarang & "' "
                                            Using Dr = OpenTrans(SQL)
                                                If Dr.Read Then
                                                    akun_persediaan_tujuan = Dr("akun_Persediaan")
                                                Else
                                                    Dr.Close()
                                                    CloseTrans()
                                                    CloseConn()
                                                    'MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                    Dim Lvw As ListViewItem
                                                    Lvw = ListView1.Items.Add("Automatization - Waste Product: Data akun tidak ditemukan!")
                                                    Exit Sub
                                                End If
                                            End Using

                                            Dim Kode_voucher As String = ""
                                            Kode_voucher = GetLastNumberJurnal(Format(tgl_skg, "yyyyMM"), "JS" & inisial_faktur_dari, KodePerusahaan)
                                            Dim pagenumber As Integer = 1

                                            SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
                                            SQL = SQL & "Keterangan, JudulBank, KetDK, userid) values("
                                            SQL = SQL & "'" & Kode_voucher & "', "
                                            SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', "
                                            SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
                                            SQL = SQL & "'" & KodeProyek & "', 'Transfer Stock " & No_Faktur_Waste & "', '', "
                                            SQL = SQL & "'-', '" & UserID & "')"
                                            ExecuteTrans(SQL)

                                            SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_persediaan_dari, 1),
                                          Strings.Mid(akun_persediaan_dari, 2, 1),
                                          Strings.Mid(Ganti(akun_persediaan_dari), 3),
                                          KodePerusahaan, KodeProyek, "Persedian " & No_Faktur_Waste, "0", nilai_persediaan_min, pagenumber, KdSo, Bahasa_Pilihan, Ket_Cost_Center_HO)
                                            ExecuteTrans(SQL)
                                            pagenumber = pagenumber + 1

                                            SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_persediaan_tujuan, 1),
                                         Strings.Mid(akun_persediaan_tujuan, 2, 1),
                                         Strings.Mid(Ganti(akun_persediaan_tujuan), 3),
                                         KodePerusahaan, KodeProyek, "Persedian " & No_Faktur_Waste, nilai_persediaan_min, "0", pagenumber, KdSo, Bahasa_Pilihan, Ket_Cost_Center_HO)
                                            ExecuteTrans(SQL)
                                            pagenumber = pagenumber + 1

                                            SQL = "select sum(debit) as debit, sum(kredit) as kredit from detail_jurnal where "
                                            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                            SQL = SQL & "kode_voucher = '" & Kode_voucher & "'"
                                            Using Dr = OpenTrans(SQL)
                                                If Dr.Read Then
                                                    If Dr("debit") <> Dr("kredit") Then
                                                        Dr.Close()
                                                        CloseTrans()
                                                        CloseConn()
                                                        'MessageBox.Show("Jurnal salah!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                        Dim Lvw As ListViewItem
                                                        Lvw = ListView1.Items.Add("Automatization - Waste Product: Jurnal salah!")
                                                        Exit Sub
                                                    End If
                                                Else
                                                    Dr.Close()
                                                    CloseTrans()
                                                    CloseConn()
                                                    'MessageBox.Show("Data jurnal tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                    Dim Lvw As ListViewItem
                                                    Lvw = ListView1.Items.Add("Automatization - Waste Product: Data jurnal tidak ditemukan!")
                                                    Exit Sub
                                                End If
                                            End Using

#End Region

                                            SQL = "insert into N_EMI_Transaksi_Transfer_Waste_Produk_Det2(kode_perusahaan, No_faktur, Urut_Det, No_Pallet, "
                                            SQL = SQL & "Serial_Number, Jumlah, UserID, Tanggal, Jam, Kode_Voucher, Jumlah_Bags) values( "
                                            SQL = SQL & "'" & KodePerusahaan & "', '" & No_Faktur_Waste & "', '" & UrutOto & "', "
                                            SQL = SQL & "'" & PalletTujuan & "', '" & SN_Baru & "', '" & nilai_kecildetail & "', "
                                            SQL = SQL & "'" & UserID & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', "
                                            SQL = SQL & "'" & Kode_voucher & "', '" & JumlahBagsEstimasi & "') "
                                            ExecuteTrans(SQL)

                                            SQL = "update N_EMI_Transaksi_Transfer_Waste_Produk_Det set  "
                                            SQL = SQL & "Selesai = 'Y' "
                                            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
                                            SQL = SQL & "and urut_oto = '" & UrutOto & "' "
                                            ExecuteTrans(SQL)

                                        Next
                                    End If
                                End Using

#End Region

#Region "PROSES PENGAJUAN PEMUSNAHAN BARANG"

                                '                                Dim InitialFaktur As String = ""
                                '                                SQL = "Select kode_stock_owner, inisial_faktur "
                                '                                SQL = SQL & "From Stock_Owner_Gudang "
                                '                                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and aktif = 'Y' and flag_waste='Y' "
                                '                                SQL = SQL & "and Kode_Stock_Owner = '" & SoTujuan & "' "
                                '                                Using Dr = OpenTrans(SQL)
                                '                                    If Dr.Read Then
                                '                                        InitialFaktur = Dr("inisial_faktur")
                                '                                    End If
                                '                                End Using

                                '                                Dim Keterangan_Faktur_Pengajuan As String = $"Automation Waste Product Transfer Submissions - {No_Faktur_Waste}"

                                '                                Dim Initial_Faktur As String = ""
                                '                                SQL = "Select kode_stock_owner, inisial_faktur, pending_persediaan, persediaan, Keterangan From Stock_Owner_Gudang where "
                                '                                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and aktif = 'Y' and kode_Stock_owner = '" & SoAwal & "' "
                                '                                SQL = SQL & "order by kode_stock_owner"
                                '                                Using dr = OpenTrans(SQL)
                                '                                    Do While dr.Read
                                '                                        Initial_Faktur = dr("inisial_faktur")
                                '                                    Loop
                                '                                End Using

                                '                                Dim FPro_Results As String = "PMB-"
                                '                                Dim No_Faktur_Waste_process = FPro_Results & Initial_Faktur & "-" & Format(tgl_skg, "MM/yy") & "-" &
                                '                                      General_Class.Get_Last_Number2("N_EMI_Transaksi_Transfer_Waste", "no_faktur", JumlahDigit,
                                '                                      "Kode_perusahaan", KodePerusahaan,
                                '                                      "And", "substring(no_faktur,1," & Len(FPro_Results) + Len(Initial_Faktur) + 6 & ")", FPro_Results & Initial_Faktur & "-" & Format(tgl_skg, "MM/yy"))


                                '                                '=================================================
                                '                                '=     INSERT N_EMI_Transaksi_Transfer_Waste     =
                                '                                '=================================================
                                '                                SQL = "insert into N_EMI_Transaksi_Transfer_Waste (kode_perusahaan, No_faktur, Kode_Stock_Owner, Tanggal, Jam, UserID, Lokasi, Keterangan, Flag_Waste_Product, No_Faktur_Produk) Values "
                                '                                SQL = SQL & "('" & KodePerusahaan & "', '" & Trim(No_Faktur_Waste_process) & "', '" & SoTujuan & "', "
                                '                                SQL = SQL & "'" & tgl_skg & "', '" & tgl_skg.ToString("HH:mm:ss") & "', '" & UserID & "', "
                                '                                SQL = SQL & "'" & Ket_Lokasi_HO & "', '" & Keterangan_Faktur_Pengajuan & "', 'Y', '" & No_Faktur_Waste & "')"
                                '                                ExecuteTrans(SQL)

                                '                                SQL = "select a.Kode_Stock_Owner, a.Kode_Stock_Owner_Tujuan, b.Kode_Barang, c.Serial_Number_Awal, d.Serial_Number, d.Jumlah, d.Jumlah_Bags, "
                                '                                SQL = SQL & "b.Satuan, b.Satuan_Barang, c.Id_Wms_Awal, c.Id_Wms_Tujuan, c.No_Pallet_Awal, d.No_Pallet, c.Warna "
                                '                                SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste_Produk a "
                                '                                SQL = SQL & "inner join N_EMI_Transaksi_Transfer_Waste_Produk_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
                                '                                SQL = SQL & "inner join N_EMI_Transaksi_Transfer_Waste_Produk_Det c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.Urut_TF "
                                '                                SQL = SQL & "inner join N_EMI_Transaksi_Transfer_Waste_Produk_Det2 d on c.Kode_Perusahaan = d.Kode_Perusahaan and c.No_Faktur = d.No_Faktur and c.Urut_Oto = d.Urut_Det "
                                '                                SQL = SQL & "where a.Status is null "
                                '                                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                                '                                SQL = SQL & "and a.No_Faktur = '" & No_Faktur_Waste & "'"
                                '                                Using Ds2 = BindingTrans(SQL)
                                '                                    If Ds2.Tables("MyTable").Rows.Count <> 0 Then
                                '                                        For j As Integer = 0 To Ds2.Tables("MyTable").Rows.Count - 1

                                '                                            Dim Produk_KdBarang As String = General_Class.CekNULL(Ds2.Tables("MyTable").Rows(j).Item("Kode_Barang"))
                                '                                            Dim Produk_Satuan As String = General_Class.CekNULL(Ds2.Tables("MyTable").Rows(j).Item("Satuan"))
                                '                                            Dim Produk_SatuanKecil As String = General_Class.CekNULL(Ds2.Tables("MyTable").Rows(j).Item("Satuan_Barang"))
                                '                                            Dim Produk_Jumlah As String = General_Class.CekNULL(Ds2.Tables("MyTable").Rows(j).Item("Jumlah"))
                                '                                            Dim Produk_Jumlah_Bags As String = General_Class.CekNULL(Ds2.Tables("MyTable").Rows(j).Item("Jumlah_Bags"))
                                '                                            Dim Produk_SN_Awal As String = General_Class.CekNULL(Ds2.Tables("MyTable").Rows(j).Item("Serial_Number_Awal"))
                                '                                            Dim Produk_SN As String = General_Class.CekNULL(Ds2.Tables("MyTable").Rows(j).Item("Serial_Number"))
                                '                                            Dim Produk_Kd_SO As String = General_Class.CekNULL(Ds2.Tables("MyTable").Rows(j).Item("Kode_Stock_Owner_Tujuan"))
                                '                                            Dim Produk_IDWarehouse As String = General_Class.CekNULL(Ds2.Tables("MyTable").Rows(j).Item("Id_Wms_Awal"))
                                '                                            Dim Produk_IDWarehouse_Tujuan As String = General_Class.CekNULL(Ds2.Tables("MyTable").Rows(j).Item("Id_Wms_Tujuan"))
                                '                                            Dim Produk_Pallet As String = General_Class.CekNULL(Ds2.Tables("MyTable").Rows(j).Item("No_Pallet_Awal"))
                                '                                            Dim Produk_Pallet_Tujuan As String = General_Class.CekNULL(Ds2.Tables("MyTable").Rows(j).Item("No_Pallet"))
                                '                                            Dim Produk_Warna As String = General_Class.CekNULL(Ds2.Tables("MyTable").Rows(j).Item("Warna"))

                                '                                            Dim nilai_kecil As Double = 0
                                '                                            SQL = "select dbo.ubah_satuan('" & KodePerusahaan & "', 'masa','" & Produk_KdBarang & "', '" & Produk_Satuan & "',"
                                '                                            SQL = SQL & "'" & Produk_SatuanKecil & "', '" & HilangkanTanda(Produk_Jumlah) & "' ) as hasil"
                                '                                            Using Dr1 = OpenTrans(SQL)
                                '                                                If Dr1.Read Then
                                '                                                    If General_Class.CekNULL(Dr1("hasil")) = "" Then
                                '                                                        Dr1.Close()
                                '                                                        CloseTrans()
                                '                                                        CloseConn()
                                '                                                        'MessageBox.Show("data konversi satuan kirim tidak ada ")
                                '                                                        Dim Lvw As ListViewItem
                                '                                                        Lvw = ListView1.Items.Add("Automatization - Waste Product: data konversi satuan kirim tidak ada")
                                '                                                        Exit Sub
                                '                                                    End If

                                '                                                    nilai_kecil = Dr1("hasil")
                                '                                                Else
                                '                                                    Dr1.Close()
                                '                                                    CloseTrans()
                                '                                                    CloseConn()
                                '                                                    'MessageBox.Show("data konversi satuan kirim tidak ada ")
                                '                                                    Dim Lvw As ListViewItem
                                '                                                    Lvw = ListView1.Items.Add("Automatization - Waste Product: data konversi satuan kirim tidak ada")
                                '                                                    Exit Sub
                                '                                                End If
                                '                                            End Using

                                '                                            Dim Jenis_Berat As String = ""
                                '                                            SQL = "Select isnull(flag_tampil_berat,'T') as flag_tampil_berat from emi_satuan where "
                                '                                            SQL = SQL & "satuan='" & Produk_Satuan & "' and kode_perusahaan='" & KodePerusahaan & "' "
                                '                                            Using dr = OpenTrans(SQL)
                                '                                                If dr.Read Then
                                '                                                    Jenis_Berat = dr("flag_tampil_berat")
                                '                                                Else
                                '                                                    dr.Close()
                                '                                                    CloseTrans()
                                '                                                    CloseConn()
                                '                                                    'MessageBox.Show("data Satuan Tidak ada . . ! ! ")
                                '                                                    Dim Lvw As ListViewItem
                                '                                                    Lvw = ListView1.Items.Add("Automatization - Waste Product: data Satuan Tidak ada . . ! !")
                                '                                                    Exit Sub
                                '                                                End If
                                '                                            End Using

                                '                                            Dim Jenis_kemasan As String = ""
                                '                                            SQL = "Select Jenis_Kemasan from barang where "
                                '                                            SQL = SQL & "Kode_Barang='" & Produk_KdBarang & "' and kode_perusahaan='" & KodePerusahaan & "' "
                                '                                            Using dr = OpenTrans(SQL)
                                '                                                If dr.Read Then
                                '                                                    Jenis_kemasan = dr("Jenis_Kemasan")
                                '                                                Else
                                '                                                    dr.Close()
                                '                                                    CloseTrans()
                                '                                                    CloseConn()
                                '                                                    'MessageBox.Show("data Satuan Tidak ada . . ! ! ")
                                '                                                    Dim Lvw As ListViewItem
                                '                                                    Lvw = ListView1.Items.Add("Automatization - Waste Product: data Satuan Tidak ada . . ! !")
                                '                                                    Exit Sub
                                '                                                End If
                                '                                            End Using

                                '                                            Dim Flag_Timbang As String = "T"

                                '                                            '========================================================
                                '                                            '=     INSERT N_EMI_Transaksi_Transfer_Waste_Detail     =
                                '                                            '========================================================
                                '                                            SQL = "select Kode_Perusahaan "
                                '                                            SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste_Detail "
                                '                                            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                '                                            SQL = SQL & "and No_Faktur = '" & Trim(No_Faktur_Waste_process) & "' "
                                '                                            SQL = SQL & "and Kode_Barang = '" & Produk_KdBarang & "' "
                                '                                            Using Dr = OpenTrans(SQL)
                                '                                                If Dr.Read Then
                                '                                                    Dr.Close()
                                '                                                    SQL = "update N_EMI_Transaksi_Transfer_Waste_Detail set total += " & HilangkanTanda(Produk_Jumlah) & ", "
                                '                                                    SQL = SQL & "Total_Barang += " & HilangkanTanda(nilai_kecil) & ", Total_Bags += " & HilangkanTanda(Produk_Jumlah_Bags) & " "
                                '                                                    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                '                                                    SQL = SQL & "and No_Faktur = '" & Trim(No_Faktur_Waste_process) & "' "
                                '                                                    SQL = SQL & "and Kode_Barang = '" & Produk_KdBarang & "' "
                                '                                                    ExecuteTrans(SQL)
                                '                                                Else
                                '                                                    Dr.Close()
                                '                                                    SQL = "insert into N_EMI_Transaksi_Transfer_Waste_Detail (Kode_Perusahaan, No_faktur, Kode_Barang, Total, Satuan, "
                                '                                                    SQL = SQL & "Total_Barang, Satuan_Barang, Total_Bags, Flag_Timbang) values "
                                '                                                    SQL = SQL & "('" & KodePerusahaan & "', '" & Trim(No_Faktur_Waste_process) & "', '" & Produk_KdBarang & "', "
                                '                                                    SQL = SQL & "'" & HilangkanTanda(Produk_Jumlah) & "', '" & Produk_Satuan & "', "
                                '                                                    SQL = SQL & "'" & nilai_kecil & "', '" & Produk_SatuanKecil & "', "
                                '                                                    SQL = SQL & "'" & HilangkanTanda(Produk_Jumlah_Bags) & "', '" & Flag_Timbang & "')"
                                '                                                    ExecuteTrans(SQL)
                                '                                                End If
                                '                                            End Using

                                '                                            Dim x_ident_current As Integer = 0
                                '                                            SQL = "select IDENT_CURRENT('N_EMI_Transaksi_Transfer_Waste_Detail') as urutan"
                                '                                            Using Dr = OpenTrans(SQL)
                                '                                                If Dr.Read Then
                                '                                                    x_ident_current = Dr("urutan")
                                '                                                End If
                                '                                            End Using

                                '                                            SQL = "select kode_Perusahaan from N_EMI_Transaksi_Transfer_Waste_Detail where "
                                '                                            SQL = SQL & "kode_Perusahaan='" & KodePerusahaan & "' and "
                                '                                            SQL = SQL & "No_Faktur='" & No_Faktur_Waste_process & "' and "
                                '                                            SQL = SQL & "Kode_barang='" & Produk_KdBarang & "' and "
                                '                                            SQL = SQL & "urut_oto='" & x_ident_current & "' "
                                '                                            Using Dr = OpenTrans(SQL)
                                '                                                If Not Dr.Read Then
                                '                                                    Dr.Close()
                                '                                                    CloseTrans()
                                '                                                    CloseConn()
                                '                                                    'MessageBox.Show("Terjadi Kesalahan, Silahkan Ulangi Transaksi  . . ! ! ")
                                '                                                    Dim Lvw As ListViewItem
                                '                                                    Lvw = ListView1.Items.Add("Automatization - Waste Product: Terjadi Kesalahan, Data Detail Tidak Ditemukan  . . ! !")
                                '                                                    Exit Sub
                                '                                                End If
                                '                                            End Using

                                '                                            '============================================
                                '                                            '=     CEK APAKAH DATA BELUM DI TIMBANG     =
                                '                                            '============================================

                                '                                            SQL = "select a.Kode_Perusahaan from N_EMI_Transaksi_Transfer_Waste a, N_EMI_Transaksi_Transfer_Waste_Det b, barang_sn c  where "
                                '                                            SQL = SQL & "a.kode_Perusahaan=b.kode_Perusahaan And a.No_Faktur=b.no_faktur and "
                                '                                            SQL = SQL & "b.kode_Perusahaan=c.kode_Perusahaan And b.Serial_Number_Awal=c.serial_number and "
                                '                                            SQL = SQL & "c.serial_number = '" & Produk_SN & "' and "
                                '                                            SQL = SQL & "selesai is null and a.status is null "
                                '                                            Using Dr = OpenTrans(SQL)
                                '                                                If Dr.Read Then
                                '                                                    Dr.Close()
                                '                                                    CloseTrans()
                                '                                                    CloseConn()
                                '                                                    Exit Sub
                                '                                                Else
                                '                                                    Dr.Close()
                                '                                                End If
                                '                                            End Using

                                '                                            Dim nilai_kecildetail As Double = 0
                                '                                            SQL = "select dbo.ubah_satuan('" & KodePerusahaan & "', 'masa','" & Produk_KdBarang & "', '" & Produk_Satuan & "',"
                                '                                            SQL = SQL & "'" & Produk_SatuanKecil & "', '" & HilangkanTanda(Produk_Jumlah) & "' ) as hasil"
                                '                                            Using Dr1 = OpenTrans(SQL)
                                '                                                If Dr1.Read Then
                                '                                                    If General_Class.CekNULL(Dr1("hasil")) = "" Then
                                '                                                        Dr1.Close()
                                '                                                        CloseTrans()
                                '                                                        CloseConn()
                                '                                                        'MessageBox.Show("data konversi satuan kirim tidak ada ")
                                '                                                        Dim Lvw As ListViewItem
                                '                                                        Lvw = ListView1.Items.Add("Automatization - Waste Product: data konversi satuan kirim tidak ada")
                                '                                                        Exit Sub
                                '                                                    End If

                                '                                                    nilai_kecildetail = Dr1("hasil")
                                '                                                Else
                                '                                                    Dr1.Close()
                                '                                                    CloseTrans()
                                '                                                    CloseConn()
                                '                                                    'MessageBox.Show("data konversi satuan kirim tidak ada ")
                                '                                                    Dim Lvw As ListViewItem
                                '                                                    Lvw = ListView1.Items.Add("Automatization - Waste Product: data konversi satuan kirim tidak ada")
                                '                                                    Exit Sub
                                '                                                End If
                                '                                            End Using

                                '                                            Dim jenis_bags As String = ""
                                '                                            Dim isi_per_bags As Double = 0
                                '                                            SQL = "select Jenis_Kemasan, isnull(Isi_Per_Bags,0) as Isi_Per_Bags from barang where "
                                '                                            SQL = SQL & "kode_perusahaan='" & KodePerusahaan & "' and "
                                '                                            SQL = SQL & "kode_barang='" & Produk_KdBarang & "' and "
                                '                                            SQL = SQL & "kode_stock_owner='" & Produk_Kd_SO & "'"
                                '                                            Using Dr = OpenTrans(SQL)
                                '                                                If Dr.Read Then
                                '                                                    jenis_bags = Dr("Jenis_Kemasan").ToString.ToUpper
                                '                                                    isi_per_bags = Dr("Isi_Per_Bags")
                                '                                                Else
                                '                                                    Dr.Close()
                                '                                                    CloseTrans()
                                '                                                    CloseConn()
                                '                                                    'MessageBox.Show("Data barang tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                '                                                    Dim Lvw As ListViewItem
                                '                                                    Lvw = ListView1.Items.Add("Automatization - Waste Product: Data barang tidak ditemukan . . ! !")
                                '                                                    Exit Sub
                                '                                                End If
                                '                                            End Using

                                '                                            Dim sisaPotong As Double = 0
                                '                                            Dim JumlahDipotong As Double = 0
                                '                                            SQL = "select a.Jumlah as Stock_SN, a.serial_number "
                                '                                            SQL = SQL & "from Barang_SN a where "
                                '                                            SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                                '                                            SQL = SQL & "and a.serial_number = '" & Produk_SN & "' "
                                '                                            SQL = SQL & "and a.Kode_stock_owner = '" & Produk_Kd_SO & "' and a.jumlah<>0 "
                                '                                            SQL = SQL & "order by a.Tgl_Expired "
                                '                                            Using Ds3 = BindingTrans(SQL)
                                '                                                If Ds3.Tables("MyTable").Rows.Count <> 0 Then

                                '                                                    sisaPotong = Val(HilangkanTanda(nilai_kecildetail))

                                '                                                    For Index As Integer = 0 To Ds3.Tables("MyTable").Rows.Count - 1
                                '                                                        If sisaPotong = 0 Then
                                '                                                            Exit For
                                '                                                        ElseIf sisaPotong < 0 Then
                                '                                                            CloseTrans()
                                '                                                            CloseConn()
                                '                                                            'MessageBox.Show("Terdapat Kesalahan saat Potong Barang Produksi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                '                                                            Dim Lvw As ListViewItem
                                '                                                            Lvw = ListView1.Items.Add("Automatization - Waste Product: Terdapat Kesalahan saat Potong Barang Produksi")
                                '                                                            Exit Sub
                                '                                                        End If

                                '                                                        Dim JumlahInsert As Double = 0
                                '                                                        Dim Satuan As String = ""

                                '                                                        Dim Data_SN As String = Ds3.Tables("MyTable").Rows(Index).Item("serial_number")

                                '                                                        If sisaPotong < Val(HilangkanTanda(Ds3.Tables("MyTable").Rows(Index).Item("Stock_SN"))) Or sisaPotong = Val(HilangkanTanda(Ds3.Tables("MyTable").Rows(Index).Item("Stock_SN"))) Then

                                '                                                            JumlahInsert = sisaPotong
                                '                                                            ' Satuan = .Rows(Index).Item("Satuan").ToString.Trim

                                '                                                            JumlahDipotong += sisaPotong
                                '                                                            sisaPotong = 0

                                '                                                        ElseIf sisaPotong > Val(HilangkanTanda(Ds3.Tables("MyTable").Rows(Index).Item("Stock_SN"))) Then

                                '                                                            JumlahInsert = Val(HilangkanTanda(Format(Ds3.Tables("MyTable").Rows(Index).Item("Stock_SN"), "N4")))
                                '                                                            'Satuan = .Rows(Index).Item("Satuan").ToString.Trim

                                '                                                            JumlahDipotong += Val(HilangkanTanda(Format(Ds3.Tables("MyTable").Rows(Index).Item("Stock_SN"), "N4")))
                                '                                                            sisaPotong = sisaPotong - Val(HilangkanTanda(Format(Ds3.Tables("MyTable").Rows(Index).Item("Stock_SN"), "N4")))
                                '                                                        Else
                                '                                                            CloseTrans()
                                '                                                            CloseConn()
                                '                                                            'MessageBox.Show("Terjadi Kesalaham pada Barang SN untuk Kode Barang !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                '                                                            Dim Lvw As ListViewItem
                                '                                                            Lvw = ListView1.Items.Add("Automatization - Waste Product: Terjadi Kesalaham pada Barang SN untuk Kode Barang !")
                                '                                                            Exit Sub
                                '                                                        End If

                                '                                                        Dim Jumlah_Bags = 0
                                '                                                        If jenis_bags = "ORIGINAL BAGS" Then
                                '                                                            Jumlah_Bags = JumlahInsert / isi_per_bags
                                '                                                        End If

                                '                                                        '========================================================
                                '                                                        '=     INSERT N_EMI_Transaksi_Transfer_Waste_Detail     =
                                '                                                        '========================================================
                                '                                                        Dim nilai_BesarInsert As Double = 0
                                '                                                        SQL = "select dbo.ubah_satuan('" & KodePerusahaan & "', 'masa','" & Produk_KdBarang & "', '" & Produk_SatuanKecil & "',"
                                '                                                        SQL = SQL & "'" & Produk_Satuan & "', '" & HilangkanTanda(JumlahInsert) & "' ) as hasil"
                                '                                                        Using Dr1 = OpenTrans(SQL)
                                '                                                            If Dr1.Read Then
                                '                                                                If General_Class.CekNULL(Dr1("hasil")) = "" Then
                                '                                                                    Dr1.Close()
                                '                                                                    CloseTrans()
                                '                                                                    CloseConn()
                                '                                                                    'MessageBox.Show("data konversi satuan kirim tidak ada ")
                                '                                                                    Dim Lvw As ListViewItem
                                '                                                                    Lvw = ListView1.Items.Add("Automatization - Waste Product: data konversi satuan kirim tidak ada")
                                '                                                                    Exit Sub
                                '                                                                End If

                                '                                                                nilai_BesarInsert = Dr1("hasil")
                                '                                                            Else
                                '                                                                Dr1.Close()
                                '                                                                CloseTrans()
                                '                                                                CloseConn()
                                '                                                                'MessageBox.Show("data konversi satuan kirim tidak ada ")
                                '                                                                Dim Lvw As ListViewItem
                                '                                                                Lvw = ListView1.Items.Add("Automatization - Waste Product: data konversi satuan kirim tidak ada")
                                '                                                                Exit Sub
                                '                                                            End If
                                '                                                        End Using

                                '                                                        Dim ID_Warehouse_Akhir As String = ""
                                '                                                        SQL = "Select top 1 a.Id_WMS_Warehouse_Position, a.Keterangan from "
                                '                                                        SQL = SQL & "view_warehouse_position a "
                                '                                                        SQL = SQL & "where a.kode_perusahaan='" & KodePerusahaan & "' "
                                '                                                        SQL = SQL & "and isnull((select top(1)  'Y' from view_warehouse_position_detail b where "
                                '                                                        SQL = SQL & "a.id_wms_warehouse_position = b.id_wms_warehouse_position And a.Kode_Perusahaan = b.KOde_Perusahaan "
                                '                                                        SQL = SQL & "And b.kode_Barang Is null ),null) ='Y' "
                                '                                                        SQL = SQL & "and a.Kode_Stock_Owner = '" & Produk_Kd_SO & "' "
                                '                                                        SQL = SQL & "order by a.Keterangan "
                                '                                                        Using Dr = OpenTrans(SQL)
                                '                                                            If Dr.Read Then
                                '                                                                ID_Warehouse_Akhir = Dr("Id_WMS_Warehouse_Position")
                                '                                                            End If
                                '                                                        End Using

                                '                                                        SQL = "insert into N_EMI_Transaksi_Transfer_Waste_Det(Kode_Perusahaan, No_faktur, Id_Wms_Awal, No_Pallet_Awal, Id_Wms_Tujuan, "
                                '                                                        SQL = SQL & "Serial_Number_Awal, Jumlah, Jumlah_Barang, Jumlah_Bags, Warna, Urut_TF) values( "
                                '                                                        SQL = SQL & "'" & KodePerusahaan & "', '" & Trim(No_Faktur_Waste_process) & "', '" & Produk_IDWarehouse_Tujuan & "', "
                                '                                                        SQL = SQL & "'" & Produk_Pallet_Tujuan & "', '" & ID_Warehouse_Akhir & "', '" & Data_SN & "', "
                                '                                                        SQL = SQL & "'" & nilai_BesarInsert & "', '" & JumlahInsert & "', "
                                '                                                        SQL = SQL & "'" & Jumlah_Bags & "', "
                                '                                                        SQL = SQL & "'" & Produk_Warna & "', '" & x_ident_current & "')"
                                '                                                        ExecuteTrans(SQL)

                                '                                                    Next
                                '                                                Else
                                '                                                    CloseTrans()
                                '                                                    CloseConn()
                                '                                                    'MessageBox.Show("Terjadi Kesalahan Pada Barang !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                '                                                    Dim Lvw As ListViewItem
                                '                                                    Lvw = ListView1.Items.Add("Automatization - Waste Product: Terjadi Kesalahan Pada Barang !")
                                '                                                    Exit Sub
                                '                                                End If

                                '                                            End Using

                                '                                            If Val(HilangkanTanda(JumlahDipotong)) <> Val(HilangkanTanda(nilai_kecildetail)) Then
                                '                                                CloseTrans()
                                '                                                CloseConn()
                                '                                                'MessageBox.Show("Terjadi Kesalahan Saat Memotong Stock Barang !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                '                                                Dim Lvw As ListViewItem
                                '                                                Lvw = ListView1.Items.Add("Automatization - Waste Product: Terjadi Kesalahan Saat Memotong Stock Barang !")
                                '                                                Exit Sub
                                '                                            End If

                                '                                        Next
                                '                                    End If
                                '                                End Using

                                '                                Dim Validasi_QrCode As String = ""
                                '                                Dim Validasi_Batch As String = ""
                                '                                Dim Validasi_SN As String = ""
                                '                                Dim Validasi_ExpDate As String = ""
                                '                                Dim Validasi_TglMasuk As String = ""
                                '                                Dim Validasi_KDBarang As String = ""
                                '                                Dim Validasi_Jumlah As String = ""
                                '                                Dim Validasi_Jumlah_Bags As String = ""
                                '                                Dim Validasi_Urut_Oto As String = ""
                                '                                Dim Validasi_Warna As String = ""
                                '                                Dim Validasi_Satuan As String = ""
                                '                                Dim Validasi_Satuan_Kecil As String = ""
                                '                                Dim Validasi_Rak_Tujuan As String = ""
                                '                                Dim Validasi_Pallet_Tujuaan As String = ""
                                '                                Dim Validasi_KDSo As String = ""

                                '                                '==============================
                                '                                '=     VALIDASI PENGAJUAN     =
                                '                                '==============================
                                '                                SQL = "select a.No_Faktur, a.Kode_Stock_Owner, b.Kode_Barang, c.Jumlah, c.Jumlah_Bags, b.Satuan, c.Serial_Number_Awal, "
                                '                                SQL = SQL & "b.Satuan_Barang, c.Urut_Oto, c.Warna, c.Id_Wms_Tujuan, d.Qr_Code, d.Batch_Number, d.Tgl_Expired, d.Tgl_Masuk "
                                '                                SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste a "
                                '                                SQL = SQL & "inner join N_EMI_Transaksi_Transfer_Waste_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
                                '                                SQL = SQL & "inner join N_EMI_Transaksi_Transfer_Waste_Det c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.Urut_TF "
                                '                                SQL = SQL & "inner join Barang_SN d on c.kode_perusahaan = d.kode_perusahaan and c.Serial_Number_Awal = d.Serial_Number "
                                '                                SQL = SQL & "where a.Status is null "
                                '                                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                                '                                SQL = SQL & "and a.no_Faktur = '" & No_Faktur_Waste_process & "' "
                                '                                Using Ds3 = BindingTrans(SQL)
                                '                                    If Ds3.Tables("MyTable").Rows.Count <> 0 Then
                                '                                        For k As Integer = 0 To Ds3.Tables("MyTable").Rows.Count - 1

                                '                                            Validasi_QrCode = General_Class.CekNULL(Ds3.Tables("MyTable").Rows(k).Item("Qr_Code"))
                                '                                            Validasi_Batch = General_Class.CekNULL(Ds3.Tables("MyTable").Rows(k).Item("Batch_Number"))
                                '                                            Validasi_SN = General_Class.CekNULL(Ds3.Tables("MyTable").Rows(k).Item("Serial_Number_Awal"))
                                '                                            Validasi_ExpDate = General_Class.CekNULL(Ds3.Tables("MyTable").Rows(k).Item("Tgl_Expired"))
                                '                                            Validasi_TglMasuk = General_Class.CekNULL(Ds3.Tables("MyTable").Rows(k).Item("Tgl_Masuk"))
                                '                                            Validasi_KDBarang = General_Class.CekNULL(Ds3.Tables("MyTable").Rows(k).Item("Kode_Barang"))
                                '                                            Validasi_Jumlah = General_Class.CekNULL(Ds3.Tables("MyTable").Rows(k).Item("Jumlah"))
                                '                                            Validasi_Jumlah_Bags = General_Class.CekNULL(Ds3.Tables("MyTable").Rows(k).Item("Jumlah_Bags"))
                                '                                            Validasi_Urut_Oto = General_Class.CekNULL(Ds3.Tables("MyTable").Rows(k).Item("Urut_Oto"))
                                '                                            Validasi_Warna = General_Class.CekNULL(Ds3.Tables("MyTable").Rows(k).Item("Warna"))
                                '                                            Validasi_Satuan = General_Class.CekNULL(Ds3.Tables("MyTable").Rows(k).Item("Satuan"))
                                '                                            Validasi_Satuan_Kecil = General_Class.CekNULL(Ds3.Tables("MyTable").Rows(k).Item("Satuan_Barang"))
                                '                                            Validasi_Rak_Tujuan = General_Class.CekNULL(Ds3.Tables("MyTable").Rows(k).Item("Id_Wms_Tujuan"))
                                '                                            Validasi_KDSo = General_Class.CekNULL(Ds3.Tables("MyTable").Rows(k).Item("Kode_Stock_Owner"))

                                '                                            SQL = "select a.Status, c.Selesai, b.Flag_Timbang "
                                '                                            SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste a, N_EMI_Transaksi_Transfer_Waste_Detail b, N_EMI_Transaksi_Transfer_Waste_Det c "
                                '                                            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.no_Faktur = b.No_Faktur and "
                                '                                            SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan and b.no_Faktur = c.No_Faktur and b.urut_oto=c.urut_TF "
                                '                                            SQL = SQL & "and a.No_Faktur = '" & No_Faktur_Waste_process & "' and c.urut_oto = '" & Validasi_Urut_Oto & "'  "
                                '                                            Using Dr = OpenTrans(SQL)
                                '                                                If Dr.Read Then

                                '                                                    If General_Class.CekNULL(Dr("status")) <> "" Then
                                '                                                        Dr.Close()
                                '                                                        CloseTrans()
                                '                                                        CloseConn()
                                '                                                        'MessageBox.Show("Proses tidak bisa dilanjutkan, barang sudah dibatalkan!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                '                                                        Dim Lvw As ListViewItem
                                '                                                        Lvw = ListView1.Items.Add("Automatization - Waste Product: Proses tidak bisa dilanjutkan, barang sudah dibatalkan!!")
                                '                                                        Exit Sub
                                '                                                    ElseIf General_Class.CekNULL(Dr("selesai")) = "Y" Then
                                '                                                        Dr.Close()
                                '                                                        CloseTrans()
                                '                                                        CloseConn()
                                '                                                        'MessageBox.Show("Terjadi kesalahan, barang sudah selesai diproses!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                '                                                        Dim Lvw As ListViewItem
                                '                                                        Lvw = ListView1.Items.Add("Automatization - Waste Product: Terjadi kesalahan, barang sudah selesai diproses!")
                                '                                                        Exit Sub
                                '                                                    ElseIf General_Class.CekNULL(Dr("Flag_Timbang")) = "Y" Then
                                '                                                        Dr.Close()
                                '                                                        CloseTrans()
                                '                                                        CloseConn()
                                '                                                        'MessageBox.Show("Terjadi kesalahan, ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                '                                                        Dim Lvw As ListViewItem
                                '                                                        Lvw = ListView1.Items.Add("Automatization - Waste Product: Terjadi kesalahan")
                                '                                                        Exit Sub
                                '                                                    End If
                                '                                                Else
                                '                                                    Dr.Close()
                                '                                                    CloseTrans()
                                '                                                    CloseConn()
                                '                                                    'MessageBox.Show("Data barang tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                '                                                    Dim Lvw As ListViewItem
                                '                                                    Lvw = ListView1.Items.Add("Automatization - Waste Product: Data barang tidak ditemukan!")
                                '                                                    Exit Sub
                                '                                                End If
                                '                                            End Using

                                '                                            SQL = "Select Top(1) nomor_urut from view_warehouse_position_detail where "
                                '                                            SQL = SQL & "kode_Perusahaan ='" & KodePerusahaan & "' and kode_barang is null and "
                                '                                            SQL = SQL & "id_wms_warehouse_position = '" & Validasi_Rak_Tujuan & "' "
                                '                                            SQL = SQL & "order by nomor_urut "
                                '                                            Using dr = OpenTrans(SQL)
                                '                                                If dr.Read Then
                                '                                                    Validasi_Pallet_Tujuaan = dr("nomor_urut")
                                '                                                Else
                                '                                                    dr.Close()
                                '                                                    CloseTrans()
                                '                                                    CloseConn()
                                '                                                    'MessageBox.Show("data Rak Sudah Penuh . . ! ! ")
                                '                                                    Dim Lvw As ListViewItem
                                '                                                    Lvw = ListView1.Items.Add("Automatization - Waste Product: data Rak Sudah Penuh . . ! ! ")
                                '                                                    Exit Sub
                                '                                                End If
                                '                                            End Using

                                '                                            '====================================
                                '                                            '=       CONVERT SATUAN KECIL       =
                                '                                            '====================================
                                '                                            Dim Validasi_nilai_kecildetail As Double = 0
                                '                                            SQL = "select dbo.ubah_satuan('" & KodePerusahaan & "', 'masa','" & Validasi_KDBarang & "', '" & Validasi_Satuan & "',"
                                '                                            SQL = SQL & "'" & Validasi_Satuan_Kecil & "', '" & Validasi_Jumlah & "' ) as hasil"
                                '                                            Using Dr1 = OpenTrans(SQL)
                                '                                                If Dr1.Read Then
                                '                                                    If General_Class.CekNULL(Dr1("hasil")) = "" Then
                                '                                                        Dr1.Close()
                                '                                                        CloseTrans()
                                '                                                        CloseConn()
                                '                                                        'MessageBox.Show("data konversi satuan kirim tidak ada ")
                                '                                                        Dim Lvw As ListViewItem
                                '                                                        Lvw = ListView1.Items.Add("Automatization - Waste Product: data konversi satuan kirim tidak ada ")
                                '                                                        Exit Sub
                                '                                                    End If

                                '                                                    Validasi_nilai_kecildetail = Dr1("hasil")
                                '                                                Else
                                '                                                    Dr1.Close()
                                '                                                    CloseTrans()
                                '                                                    CloseConn()
                                '                                                    'MessageBox.Show("data konversi satuan kirim tidak ada ")
                                '                                                    Dim Lvw As ListViewItem
                                '                                                    Lvw = ListView1.Items.Add("Automatization - Waste Product: data konversi satuan kirim tidak ada ")
                                '                                                    Exit Sub
                                '                                                End If
                                '                                            End Using

                                '                                            '============================
                                '                                            '=       POTONG STOCK       =
                                '                                            '============================

                                '                                            Dim nilai_persediaan_min As Double = 0
                                '                                            SQL = "select round(dbo.get_hpp(serial_number) * " & Validasi_nilai_kecildetail & ", 2) as rp_persediaan_min from barang_sn where "
                                '                                            SQL = SQL & "Kode_Stock_Owner='" & Validasi_KDSo & "' and Kode_Barang='" & Validasi_KDBarang & "' "
                                '                                            SQL = SQL & "and Serial_Number='" & Validasi_SN & "'"
                                '                                            Using dr = OpenTrans(SQL)
                                '                                                If dr.Read Then
                                '                                                    nilai_persediaan_min = dr("rp_persediaan_min")
                                '                                                Else
                                '                                                    dr.Close()
                                '                                                    CloseTrans()
                                '                                                    CloseConn()
                                '                                                    'MessageBox.Show("Data SN tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                '                                                    Dim Lvw As ListViewItem
                                '                                                    Lvw = ListView1.Items.Add("Automatization - Waste Product: Data SN tidak ditemukan!")
                                '                                                    Exit Sub
                                '                                                End If
                                '                                            End Using

                                '                                            Dim Nama As String = ""
                                '                                            'Dim jumlahAkhir As Double = Val(dgv_GoodStock) - Val(dgv_Jumlah)
                                '                                            SQL = "select Nama, Kode_Barang, round(good_stock,4) as good_stock, Jumlah_Bags from Barang where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & Validasi_KDSo & "' "
                                '                                            SQL = SQL & "and Kode_Barang='" & Validasi_KDBarang & "' "
                                '                                            Using dr = OpenTrans(SQL)
                                '                                                If dr.Read Then
                                '                                                    Nama = dr("Kode_Barang")
                                '                                                    If dr("good_stock") < Validasi_nilai_kecildetail Then
                                '                                                        dr.Close()
                                '                                                        CloseTrans()
                                '                                                        CloseConn()
                                '                                                        'MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat stock " & Nama & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                '                                                        Dim Lvw As ListViewItem
                                '                                                        Lvw = ListView1.Items.Add("Automatization - Waste Product: Proses tidak dapat dilanjutkan karena akan membuat stock " & Nama & " menjadi negatif.")
                                '                                                        Exit Sub
                                '                                                    ElseIf dr("Jumlah_Bags") < Validasi_Jumlah_Bags Then
                                '                                                        dr.Close()
                                '                                                        CloseTrans()
                                '                                                        CloseConn()
                                '                                                        'MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat jumlah bags " & Nama & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                '                                                        Dim Lvw As ListViewItem
                                '                                                        Lvw = ListView1.Items.Add("Automatization - Waste Product: Proses tidak dapat dilanjutkan karena akan membuat jumlah bags " & Nama & " menjadi negatif.")
                                '                                                        Exit Sub
                                '                                                    Else
                                '                                                        dr.Close()
                                '                                                        SQL = "update barang set Good_Stock = Good_Stock - Round(" & Validasi_nilai_kecildetail & ",4), Jumlah_Bags = Jumlah_Bags - " & Validasi_Jumlah_Bags & " "
                                '                                                        SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & Validasi_KDSo & "' "
                                '                                                        SQL = SQL & " and Kode_Barang='" & Validasi_KDBarang & "'"
                                '                                                        ExecuteTrans(SQL)
                                '                                                    End If
                                '                                                Else
                                '                                                    dr.Close()
                                '                                                    CloseTrans()
                                '                                                    CloseConn()
                                '                                                    'MessageBox.Show("Barang " & Nama & " tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                '                                                    Dim Lvw As ListViewItem
                                '                                                    Lvw = ListView1.Items.Add("Automatization - Waste Product: Barang " & Nama & " tidak ditemukan!")
                                '                                                    Exit Sub
                                '                                                End If
                                '                                            End Using

                                '                                            SQL = "select round(jumlah,4) as jumlah, Jumlah_Bags from Barang_SN where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & Validasi_KDSo & "' "
                                '                                            SQL = SQL & "and Kode_Barang='" & Validasi_KDBarang & "' "
                                '                                            SQL = SQL & "and Serial_Number='" & Validasi_SN & "'"
                                '                                            Using dr = OpenTrans(SQL)
                                '                                                If dr.Read Then
                                '                                                    If dr("jumlah") < Validasi_nilai_kecildetail Then
                                '                                                        dr.Close()
                                '                                                        CloseTrans()
                                '                                                        CloseConn()
                                '                                                        'MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat stock " & Nama & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                '                                                        Dim Lvw As ListViewItem
                                '                                                        Lvw = ListView1.Items.Add("Automatization - Waste Product: Proses tidak dapat dilanjutkan karena akan membuat stock " & Nama & " menjadi negatif.")
                                '                                                        Exit Sub
                                '                                                    ElseIf dr("Jumlah_Bags") < Validasi_Jumlah_Bags Then
                                '                                                        dr.Close()
                                '                                                        CloseTrans()
                                '                                                        CloseConn()
                                '                                                        'MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat jumlah bags " & Nama & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                '                                                        Dim Lvw As ListViewItem
                                '                                                        Lvw = ListView1.Items.Add("Automatization - Waste Product: Proses tidak dapat dilanjutkan karena akan membuat jumlah bags " & Nama & " menjadi negatif.")
                                '                                                        Exit Sub
                                '                                                    Else
                                '                                                        dr.Close()
                                '                                                        SQL = "update barang_sn set jumlah = jumlah - Round(" & Validasi_nilai_kecildetail & ",4), Jumlah_Bags = Jumlah_Bags - " & Validasi_Jumlah_Bags & " "
                                '                                                        SQL = SQL & "where Kode_Stock_Owner='" & Validasi_KDSo & "' and Kode_Barang='" & Validasi_KDBarang & "' "
                                '                                                        SQL = SQL & "and Serial_Number='" & Validasi_SN & "'"
                                '                                                        ExecuteTrans(SQL)
                                '                                                    End If
                                '                                                Else
                                '                                                    dr.Close()
                                '                                                    CloseTrans()
                                '                                                    CloseConn()
                                '                                                    'MessageBox.Show("Barang " & Nama & " tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                '                                                    Dim Lvw As ListViewItem
                                '                                                    Lvw = ListView1.Items.Add("Automatization - Waste Product: Barang " & Nama & " tidak ditemukan!")
                                '                                                    Exit Sub
                                '                                                End If
                                '                                            End Using

                                '                                            '====================================
                                '                                            '=       CEK KESESUAIAN STOCK       =
                                '                                            '====================================
                                '                                            SQL = "SELECT round(SUM(good_stock),4) AS good_stock, isnull((select round(sum(jumlah),4) from Barang_sn x "
                                '                                            SQL = SQL & "where a.kode_Barang=x.kode_Barang and a.Kode_Stock_Owner=x.kode_Stock_Owner "
                                '                                            SQL = SQL & "and a.kode_Perusahaan=x.kode_Perusahaan ),0) as Jumlah_sn, "
                                '                                            SQL = SQL & "isnull(round(SUM(jumlah_bags), 2), 0) AS jumlah_bags_barang, "
                                '                                            SQL = SQL & "isnull((select round(sum(Jumlah_Bags), 2) from Barang_sn y "
                                '                                            SQL = SQL & "where a.kode_Barang=y.kode_Barang and a.Kode_Stock_Owner=y.kode_Stock_Owner and a.kode_Perusahaan=y.kode_Perusahaan ), 0) as jumlah_bags_sn "
                                '                                            SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner = '" & Validasi_KDSo & "' "
                                '                                            SQL = SQL & "AND a.Kode_Barang = '" & Validasi_KDBarang & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
                                '                                            SQL = SQL & "group by a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan "
                                '                                            Using Ds4 = BindingTrans(SQL)
                                '                                                If Ds4.Tables("MyTable").Rows.Count <> 0 Then
                                '                                                    If Ds4.Tables("MyTable").Rows(0).Item("good_stock") <> Ds4.Tables("MyTable").Rows(0).Item("Jumlah_sn") Or Ds4.Tables("MyTable").Rows(0).Item("jumlah_bags_barang") <> Ds4.Tables("MyTable").Rows(0).Item("jumlah_bags_sn") Then
                                '                                                        CloseTrans()
                                '                                                        CloseConn()
                                '                                                        'MessageBox.Show("Terjadi Kesalahan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                '                                                        Dim Lvw As ListViewItem
                                '                                                        Lvw = ListView1.Items.Add("Automatization - Waste Product: Terjadi Kesalahan . . ! !")
                                '                                                        Exit Sub
                                '                                                    End If
                                '                                                Else
                                '                                                    CloseTrans()
                                '                                                    CloseConn()
                                '                                                    'MessageBox.Show("Data tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                '                                                    Dim Lvw As ListViewItem
                                '                                                    Lvw = ListView1.Items.Add("Automatization - Waste Product: Data tidak ditemukan . . ! !")
                                '                                                    Exit Sub
                                '                                                End If
                                '                                            End Using

                                '                                            '==============================
                                '                                            '=       INSERT SN BARU       =
                                '                                            '==============================

                                '                                            Dim hargaIsn As String = ""
                                '                                            Dim namaBarang As String = ""
                                '                                            Dim warnaLama As String = ""

                                '                                            'Ambil Data Lama
                                '                                            SQL = "select a.Serial_Number, a.Qr_Code, a.Kode_Unik_Berjalan, b.Nama, a.Batch_Number, a.Tgl_Expired, a.warna "
                                '                                            SQL = SQL & "from barang_sn a, barang b "
                                '                                            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
                                '                                            SQL = SQL & "and a.Kode_Stock_Owner = b.Kode_Stock_Owner "
                                '                                            SQL = SQL & "and a.Kode_Barang = b.Kode_Barang "
                                '                                            SQL = SQL & "and a.Kode_Perusahaan='" & KodePerusahaan & "' "
                                '                                            SQL = SQL & "and a.Kode_Stock_Owner='" & Validasi_KDSo & "' "
                                '                                            SQL = SQL & "and a.Kode_Barang ='" & Validasi_KDBarang & "' "
                                '                                            SQL = SQL & "and a.Serial_Number='" & Validasi_SN & "' "
                                '                                            'SQL = SQL & "and a.Jumlah <> 0 "
                                '                                            Using Dr = OpenTrans(SQL)
                                '                                                Do While Dr.Read
                                '                                                    hargaIsn = Get_Harga_SN(Dr("Serial_Number"))
                                '                                                    QrLama = General_Class.CekNULL(Dr("Qr_Code"))
                                '                                                    batchLama = General_Class.CekNULL(Dr("Batch_Number"))
                                '                                                    namaBarang = General_Class.CekNULL(Dr("Nama"))
                                '                                                    expDate = General_Class.CekNULL(Dr("Tgl_Expired"))
                                '                                                    warnaLama = General_Class.CekNULL(Dr("warna"))
                                '                                                Loop
                                '                                            End Using

                                '                                            'GENERATE SN BARU
                                '                                            Dim str As String = Format(Random.Next(0, 999), "000") & Format(tgl_skg, "HHmmss")
                                '                                            Dim Kode_Unik As String = str.Substring(0, 5) & "BB" & Chr(64 + str.Substring(6, 1)) & str.Substring(6, Len(str) - 6)
                                '                                            Dim SN_Baru As String = Kode_Unik & Tanda_SN & "01" & Tanda_SN & hargaIsn & Tanda_SN & "02" & Tanda_SN & Format(tgl_skg, "yyyy-MM-dd")

                                '                                            Dim newKodeUnikBerjalan As String = Generate_Random_Kode(10)

                                '                                            'INSERT BARANG SN BARU
                                '                                            SQL = "insert into Barang_SN (Kode_Perusahaan, Kode_Stock_Owner, Kode_Barang, Serial_Number, Jumlah,  Jumlah_Bags, "
                                '                                            SQL = SQL & "Tgl_Expired, Tgl_Produksi, Stock_PO, Stock_Inquiry, Id_Warehouse, id_Susunan, Qr_Code, Kode_Unik_Berjalan, Kode_Unik_Asal, Nomor_Pallet, batch_number, Warna, Tgl_masuk, Blok_SN) "
                                '                                            SQL = SQL & "select Kode_Perusahaan, '" & Validasi_KDSo & "', Kode_Barang, '" & SN_Baru & "', '" & Validasi_nilai_kecildetail & "', " & Validasi_Jumlah_Bags & ", "
                                '                                            SQL = SQL & "Tgl_Expired, Tgl_Produksi, Stock_PO, Stock_Inquiry, '" & Validasi_Rak_Tujuan & "', id_Susunan , Qr_Code, '" & newKodeUnikBerjalan & "', "
                                '                                            SQL = SQL & "Kode_Unik_Asal, '" & Validasi_Pallet_Tujuaan & "', batch_number, '" & Validasi_Warna & "', Tgl_Masuk, 'Y' "
                                '                                            SQL = SQL & "from Barang_SN "
                                '                                            SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' "
                                '                                            SQL = SQL & "and Kode_Stock_Owner='" & Validasi_KDSo & "' "
                                '                                            SQL = SQL & "and Kode_Barang='" & Validasi_KDBarang & "' "
                                '                                            SQL = SQL & "and Serial_Number='" & Validasi_SN & "' "
                                '                                            ExecuteTrans(SQL)

                                '                                            '============================
                                '                                            '=       TAMBAH STOCK       =
                                '                                            '============================

                                '                                            SQL = "update barang set Good_Stock= Good_Stock + Round(" & Validasi_nilai_kecildetail & ",4), Jumlah_Bags = Jumlah_Bags + " & Validasi_Jumlah_Bags & " "
                                '                                            SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & Validasi_KDSo & "' "
                                '                                            SQL = SQL & " and Kode_Barang='" & Validasi_KDBarang & "'"
                                '                                            ExecuteTrans(SQL)

                                '                                            'CEK KESESUAIAN STOCK
                                '                                            SQL = "SELECT round(SUM(good_stock),4) AS good_stock, isnull((select round(sum(jumlah),4) from Barang_sn x "
                                '                                            SQL = SQL & "where a.kode_Barang=x.kode_Barang and a.Kode_Stock_Owner=x.kode_Stock_Owner "
                                '                                            SQL = SQL & "and a.kode_Perusahaan=x.kode_Perusahaan ),0) as Jumlah_sn, "
                                '                                            SQL = SQL & "isnull(round(SUM(jumlah_bags), 2), 0) AS jumlah_bags_barang, "
                                '                                            SQL = SQL & "isnull((select round(sum(Jumlah_Bags), 2) from Barang_sn y "
                                '                                            SQL = SQL & "where a.kode_Barang=y.kode_Barang and a.Kode_Stock_Owner=y.kode_Stock_Owner and a.kode_Perusahaan=y.kode_Perusahaan ), 0) as jumlah_bags_sn "
                                '                                            SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner = '" & Validasi_KDSo & "' "
                                '                                            SQL = SQL & "AND a.Kode_Barang = '" & Validasi_KDBarang & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
                                '                                            SQL = SQL & "group by a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan "
                                '                                            Using Ds4 = BindingTrans(SQL)

                                '                                                If Ds4.Tables("MyTable").Rows.Count <> 0 Then
                                '                                                    If Ds4.Tables("MyTable").Rows(0).Item("good_stock") <> Ds4.Tables("MyTable").Rows(0).Item("Jumlah_sn") Or Ds4.Tables("MyTable").Rows(0).Item("jumlah_bags_barang") <> Ds4.Tables("MyTable").Rows(0).Item("jumlah_bags_sn") Then
                                '                                                        CloseTrans()
                                '                                                        CloseConn()
                                '                                                        'MessageBox.Show("Terjadi Kesalahan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                '                                                        Dim Lvw As ListViewItem
                                '                                                        Lvw = ListView1.Items.Add("Automatization - Waste Product: Terjadi Kesalahan . . ! !")
                                '                                                        Exit Sub
                                '                                                    End If
                                '                                                Else
                                '                                                    CloseTrans()
                                '                                                    CloseConn()
                                '                                                    'MessageBox.Show("Data tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                '                                                    Dim Lvw As ListViewItem
                                '                                                    Lvw = ListView1.Items.Add("Automatization - Waste Product: Data tidak ditemukan . . ! !")
                                '                                                    Exit Sub
                                '                                                End If

                                '                                            End Using

                                '#Region "Jurnal"

                                '                                            'dari
                                '                                            Dim inisial_faktur_dari As String = ""
                                '                                            Dim akun_persediaan_dari As String = ""
                                '                                            Dim akun_persediaan_tujuan As String = ""

                                '                                            SQL = "select inisial_faktur,Persediaan_Bahan_Baku,Persediaan,Persediaan_Bahan_Setengah_Jadi,Persediaan_Scrap, Persediaan_Packaging from stock_owner_gudang "
                                '                                            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & Validasi_KDSo & "' "
                                '                                            Using Dr = OpenTrans(SQL)
                                '                                                If Dr.Read Then
                                '                                                    'akun_persediaan_dari = Dr("persediaan")
                                '                                                    inisial_faktur_dari = Dr("inisial_faktur")
                                '                                                Else
                                '                                                    Dr.Close()
                                '                                                    CloseTrans()
                                '                                                    CloseConn()
                                '                                                    'MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                '                                                    Dim Lvw As ListViewItem
                                '                                                    Lvw = ListView1.Items.Add("Automatization - Waste Product: Data akun tidak ditemukan!")
                                '                                                    Exit Sub
                                '                                                End If
                                '                                            End Using

                                '                                            SQL = "select c.akun_Persediaan "
                                '                                            SQL = SQL & "from EMI_Group_Jenis a, Barang b, EMI_Group_Jenis_Akun c where "
                                '                                            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Group_Jenis = b.Id_Group_Jenis and "
                                '                                            SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Group_Jenis = c.Id_Group_Jenis and "
                                '                                            SQL = SQL & "b.kode_stock_owner = c.kode_stock_owner and b.Kode_Perusahaan = '" & KodePerusahaan & "' "
                                '                                            SQL = SQL & "and b.kode_stock_owner = '" & Validasi_KDSo & "' and b.Kode_Barang='" & Validasi_KDBarang & "' "
                                '                                            Using Dr = OpenTrans(SQL)
                                '                                                If Dr.Read Then
                                '                                                    akun_persediaan_dari = Dr("akun_Persediaan")
                                '                                                Else
                                '                                                    Dr.Close()
                                '                                                    CloseTrans()
                                '                                                    CloseConn()
                                '                                                    'MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                '                                                    Dim Lvw As ListViewItem
                                '                                                    Lvw = ListView1.Items.Add("Automatization - Waste Product: Data akun tidak ditemukan!")
                                '                                                    Exit Sub
                                '                                                End If
                                '                                            End Using

                                '                                            SQL = "select c.akun_Persediaan "
                                '                                            SQL = SQL & "from EMI_Group_Jenis a, Barang b, EMI_Group_Jenis_Akun c where "
                                '                                            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Group_Jenis = b.Id_Group_Jenis and "
                                '                                            SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Group_Jenis = c.Id_Group_Jenis and "
                                '                                            SQL = SQL & "b.kode_stock_owner = c.kode_stock_owner and b.Kode_Perusahaan = '" & KodePerusahaan & "' "
                                '                                            SQL = SQL & "and b.kode_stock_owner = '" & Validasi_KDSo & "' and b.Kode_Barang='" & Validasi_KDBarang & "' "
                                '                                            Using Dr = OpenTrans(SQL)
                                '                                                If Dr.Read Then
                                '                                                    akun_persediaan_tujuan = Dr("akun_Persediaan")
                                '                                                Else
                                '                                                    Dr.Close()
                                '                                                    CloseTrans()
                                '                                                    CloseConn()
                                '                                                    'MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                '                                                    Dim Lvw As ListViewItem
                                '                                                    Lvw = ListView1.Items.Add("Automatization - Waste Product: Data akun tidak ditemukan!")
                                '                                                    Exit Sub
                                '                                                End If
                                '                                            End Using

                                '                                            Dim Kode_voucher As String = ""
                                '                                            Kode_voucher = GetLastNumberJurnal(Format(tgl_skg, "yyyyMM"), "JS" & inisial_faktur_dari, KodePerusahaan)
                                '                                            Dim pagenumber As Integer = 1

                                '                                            SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
                                '                                            SQL = SQL & "Keterangan, JudulBank, KetDK, userid) values("
                                '                                            SQL = SQL & "'" & Kode_voucher & "', "
                                '                                            SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', "
                                '                                            SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
                                '                                            SQL = SQL & "'" & KodeProyek & "', 'Transfer Stock " & No_Faktur_Waste & "', '', "
                                '                                            SQL = SQL & "'-', '" & UserID & "')"
                                '                                            ExecuteTrans(SQL)

                                '                                            SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_persediaan_dari, 1),
                                '                                                                  Strings.Mid(akun_persediaan_dari, 2, 1),
                                '                                                                  Strings.Mid(Ganti(akun_persediaan_dari), 3),
                                '                                                                  KodePerusahaan, KodeProyek, "Persedian " & No_Faktur_Waste, "0", nilai_persediaan_min, pagenumber, Validasi_KDSo, Bahasa_Pilihan, Ket_Cost_Center_HO)
                                '                                            ExecuteTrans(SQL)
                                '                                            pagenumber = pagenumber + 1

                                '                                            SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_persediaan_tujuan, 1),
                                '                                                                 Strings.Mid(akun_persediaan_tujuan, 2, 1),
                                '                                                                 Strings.Mid(Ganti(akun_persediaan_tujuan), 3),
                                '                                                                 KodePerusahaan, KodeProyek, "Persedian " & No_Faktur_Waste, nilai_persediaan_min, "0", pagenumber, Validasi_KDSo, Bahasa_Pilihan, Ket_Cost_Center_HO)
                                '                                            ExecuteTrans(SQL)
                                '                                            pagenumber = pagenumber + 1

                                '                                            SQL = "select sum(debit) as debit, sum(kredit) as kredit from detail_jurnal where "
                                '                                            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                '                                            SQL = SQL & "kode_voucher = '" & Kode_voucher & "'"
                                '                                            Using Dr = OpenTrans(SQL)
                                '                                                If Dr.Read Then
                                '                                                    If Dr("debit") <> Dr("kredit") Then
                                '                                                        Dr.Close()
                                '                                                        CloseTrans()
                                '                                                        CloseConn()
                                '                                                        'MessageBox.Show("Jurnal salah!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                '                                                        Dim Lvw As ListViewItem
                                '                                                        Lvw = ListView1.Items.Add("Automatization - Waste Product: Jurnal salah!")
                                '                                                        Exit Sub
                                '                                                    End If
                                '                                                Else
                                '                                                    Dr.Close()
                                '                                                    CloseTrans()
                                '                                                    CloseConn()
                                '                                                    'MessageBox.Show("Data jurnal tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                '                                                    Dim Lvw As ListViewItem
                                '                                                    Lvw = ListView1.Items.Add("Automatization - Waste Product: Data jurnal tidak ditemukan!")
                                '                                                    Exit Sub
                                '                                                End If
                                '                                            End Using

                                '#End Region

                                '                                            SQL = "insert into N_EMI_Transaksi_Transfer_Waste_Det2(kode_perusahaan, No_faktur, Urut_Det, No_Pallet, "
                                '                                            SQL = SQL & "Serial_Number, Jumlah, UserID, Tanggal, Jam, Kode_Voucher, Jumlah_Bags) values( "
                                '                                            SQL = SQL & "'" & KodePerusahaan & "', '" & No_Faktur_Waste_process & "', '" & Validasi_Urut_Oto & "', "
                                '                                            SQL = SQL & "'" & Validasi_Pallet_Tujuaan & "', '" & SN_Baru & "', '" & Validasi_nilai_kecildetail & "', "
                                '                                            SQL = SQL & "'" & UserID & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', "
                                '                                            SQL = SQL & "'" & Kode_voucher & "', '" & Validasi_Jumlah_Bags & "') "
                                '                                            ExecuteTrans(SQL)

                                '                                            SQL = "update N_EMI_Transaksi_Transfer_Waste_Det set  "
                                '                                            SQL = SQL & "Selesai = 'Y' "
                                '                                            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
                                '                                            SQL = SQL & "and urut_oto = '" & Validasi_Urut_Oto & "' "
                                '                                            ExecuteTrans(SQL)

                                '                                        Next
                                '                                    End If
                                '                                End Using

#End Region

                                SQL = "update N_EMI_Transaksi_Approval_Waste set Flag_Selesai = 'Y' "
                                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and Status is null and No_Transaksi = '" & No_Transaksi_Approval & "' and No_Faktur_Waste = '" & No_Faktur_Waste & "' "
                                ExecuteTrans(SQL)

                            End If

                        Next
                    End If
                End With
            End Using

            'If True Then
            '    CloseTrans()
            '    CloseConn()
            '    MessageBox.Show("TahanS")
            '    Exit Sub
            'End If

            Cmd.Transaction.Commit()
            CloseConn()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            Dim Lvw As ListViewItem
            Lvw = ListView1.Items.Add("Automatization - Waste Product: " & ex.Message)
            Exit Sub
        End Try
    End Sub


End Class
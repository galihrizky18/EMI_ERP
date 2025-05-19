Public Class SD_Pilih_PO2
    Dim arrcari As New ArrayList
    Dim Jenis = "Lokasi_PO"
    Dim LvNo_Po As String
    Dim LvLokasi As String
    Dim LvTanggal As String
    Dim LvKd_Supplier As String
    Dim LvNm_Supplier As String

    Public filter_tambahan As String
    Public asal As String

    Private Sub Get_Isi_Listview(ByVal No_Index As Integer)
        LvNo_Po = ListView1.Items(No_Index).Text
        LvLokasi = ListView1.Items(No_Index).SubItems(1).Text
        LvTanggal = ListView1.Items(No_Index).SubItems(2).Text
        LvKd_Supplier = ListView1.Items(No_Index).SubItems(3).Text
        LvNm_Supplier = ListView1.Items(No_Index).SubItems(4).Text
    End Sub
    Private Sub SD_Pilih_PO_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
        Try

            OpenConn()

            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)

            Btn_Cari.Text = Base_Language.Lang_Global_Cari
            Label1.Text = Base_Language.Lang_Lokasi_PO_Judul_Display
            Label4.Text = Base_Language.Lang_Lokasi_PO_Kolom

            ListView1.Columns.Clear()
            ListView1.Columns.Add(Base_Language.Lang_Global_No_PO, 150, HorizontalAlignment.Left)
            ListView1.Columns.Add(Base_Language.Lang_Global_Lokasi, 130, HorizontalAlignment.Left)
            ListView1.Columns.Add(Base_Language.Lang_Global_Tanggal, 120, HorizontalAlignment.Center)
            ListView1.Columns.Add(Base_Language.Lang_Global_Kode_Supplier, 150, HorizontalAlignment.Left)
            ListView1.Columns.Add(Base_Language.Lang_Global_Supplier, 250, HorizontalAlignment.Left)
            ListView1.View = View.Details

            ListView1.Items.Clear()
            SQL = "select a.No_Faktur,a.Lokasi,a.Tanggal,a.Kode_Supplier,b.Nama "
            SQL = SQL & "from EMI_Pembelian_Loading a,Suppliers b where "
            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Supplier = b.Kode_Supplier "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Status is null "
            SQL = SQL & " " & filter_tambahan & " "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim Lvw As ListViewItem
                    Lvw = ListView1.Items.Add(dr("No_Faktur"))
                    Lvw.SubItems.Add(dr("Lokasi"))
                    Lvw.SubItems.Add(Format(dr("Tanggal"), "dd MMMM yyyy"))
                    Lvw.SubItems.Add(dr("Kode_Supplier"))
                    Lvw.SubItems.Add(dr("Nama"))
                Loop
            End Using

            ComboBox1.Items.Clear() : arrcari.Clear()
            ComboBox1.Items.Add(Base_Language.Lang_Global_No_PO) : arrcari.Add("a.No_Faktur")
            ComboBox1.Items.Add(Base_Language.Lang_Global_Lokasi) : arrcari.Add("a.Lokasi")
            ComboBox1.Items.Add(Base_Language.Lang_Global_Kode_Supplier) : arrcari.Add("a.Kode_Supplier")
            ComboBox1.Items.Add(Base_Language.Lang_Global_Supplier) : arrcari.Add("b.Nama")
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub SD_Pilih_PO_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
        If ComboBox1.Text.Trim.Length = 0 Then Exit Sub
        If TextBox3.Text.Trim.Length = 0 Then Exit Sub

        Cari("T")
    End Sub

    Private Sub Cari(ByVal semua As String)
        Try
            OpenConn()

            ListView1.Items.Clear()
            SQL = "select a.No_Faktur,a.Lokasi,a.Tanggal,a.Kode_Supplier,b.Nama "
            SQL = SQL & "from EMI_Pembelian_PO a,Suppliers b where "
            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Supplier = b.Kode_Supplier "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Status is null "
            SQL = SQL & " " & filter_tambahan & " and  "
            If semua = "T" Then
                SQL = SQL & " " & arrcari.Item(ComboBox1.SelectedIndex) & " like '%" & TextBox3.Text & "%' "
                SQL = SQL & "order by " & arrcari.Item(ComboBox1.SelectedIndex) & " "
            Else
                SQL = SQL & " "
            End If
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim Lvw As ListViewItem
                    Lvw = ListView1.Items.Add(dr("No_Faktur"))
                    Lvw.SubItems.Add(dr("Lokasi"))
                    Lvw.SubItems.Add(Format(dr("Tanggal"), "dd MMMM yyyy"))
                    Lvw.SubItems.Add(dr("Kode_Supplier"))
                    Lvw.SubItems.Add(dr("Nama"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub ListView1_DoubleClick(sender As Object, e As EventArgs) Handles ListView1.DoubleClick
        Get_Isi_Listview(ListView1.FocusedItem.Index)

        'If asal = "Lokasi_PO" Then
        '    'EMI_Lokasi_PO.DtpLokasiTujuanPO_TglPO.Value = LvTanggal
        '    'EMI_Lokasi_PO.CmbLokasiTujuanPO_Lokasi.Text = LvLokasi
        '    'EMI_Lokasi_PO.TxtLokasiTujuanPO_NoPO.Text = LvNo_Po
        '    'EMI_Lokasi_PO.TxtLokasiTujuanPO_Supplier.Text = LvNm_Supplier
        '    'EMI_Lokasi_PO.Kode_Sup = LvKd_Supplier
        '    'EMI_Lokasi_PO.TxtLokasiTujuanPO_NoPO_Leave(ListView1, e)
        'ElseIf asal = "Pembelian" Then
        EMI_Pembelian2.Kosong()

        Try
            OpenConn()
            SQL = "select a.No_Faktur,f.No_Nota,a.Tanggal,a.Kode_Supplier as Supplier,b.Nama,"
            SQL = SQL & "f.Jenis_Pembayaran,f.Mata_Uang,f.Kurs,f.Cara_Bayar,f.PPN, f.Total_MUA,"
            SQL = SQL & "f.Total_IDR,f.Grand_Sebelum_PPN,f.Grand,"
            SQL = SQL & "isnull((select c.Keterangan from Cara_Bayar c where "
            SQL = SQL & "a.Kode_Perusahaan = c.Kode_Perusahaan and "
            SQL = SQL & "a.Lokasi = c.Lokasi and f.Cara_Bayar = c.Kode_CB "
            SQL = SQL & "),NULL) as ket_cb "
            SQL = SQL & "from EMI_Pembelian_Loading a,Suppliers b,EMI_Pembelian_Loading_Detail d,"
            SQL = SQL & "EMI_Pembelian_PO_Detail e,EMI_Pembelian_PO f "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Supplier = b.Kode_Supplier "
            SQL = SQL & "and a.Kode_Perusahaan = d.Kode_Perusahaan and a.No_Faktur = d.No_Faktur "
            SQL = SQL & "and d.Kode_Perusahaan = e.Kode_Perusahaan and d.Urut_PO = e.No_Urut "
            SQL = SQL & "and e.Kode_Perusahaan = f.Kode_Perusahaan and e.No_Faktur = f.No_Faktur "
            SQL = SQL & "and f.Status is null  and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Faktur = '" & LvNo_Po & "'"
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    EMI_Pembelian2.TxtPembelian_NoPO.Text = dr("No_Faktur")
                    'EMI_Pembelian.DtpPembelian_Tgl.Value = Format(dr("Tanggal"), "dd MMMM yyyy")
                    EMI_Pembelian2.TxtPembelian_NoNota.Text = dr("No_Nota")
                    EMI_Pembelian2.TxtPembelian_KdSupplier.Text = dr("Supplier")
                    EMI_Pembelian2.TxtPembelian_NmSupplier.Text = dr("Nama")
                    If dr("Jenis_Pembayaran") = "T" Then
                        EMI_Pembelian2.CmbPembelian_JnsBayar.SelectedIndex = 0
                    Else
                        EMI_Pembelian2.CmbPembelian_JnsBayar.SelectedIndex = 1
                    End If
                    EMI_Pembelian2.CmbPembelian_MataUang.Text = dr("Mata_Uang")
                    EMI_Pembelian2.TxtPembelian_Kurs.Text = Format(dr("Kurs"), "N2")
                    If General_Class.CekNULL(dr("Cara_Bayar")) = "" Then
                        EMI_Pembelian2.CmbPembelian_CaraBayar.SelectedIndex = -1
                    Else
                        EMI_Pembelian2.CmbPembelian_CaraBayar.Text = dr("Keterangan")
                    End If

                    If dr("PPN") > 0 Then
                        EMI_Pembelian2.TxtPembelian_PersenPPN.Text = Format(dr("PPN"), "N2")
                    Else
                        EMI_Pembelian2.TxtPembelian_PersenPPN.Text = 0
                    End If

                    'Dim nilai_ppn As Double = 0
                    'nilai_ppn = dr("Grand_Sebelum_PPN") * dr("PPN") / 100
                    'EMI_Pembelian2.TxtPembelian_TotalMUA.Text = Format(dr("Total_MUA"), "N2")
                    'EMI_Pembelian2.TxtPembelian_TotalIDR.Text = Format(dr("Total_IDR"), "N2")
                    'EMI_Pembelian2.TxtPembelian_TotalSblmPPN.Text = Format(dr("Grand_Sebelum_PPN"), "N2")
                    'EMI_Pembelian2.TxtPembelian_NilaiPPN.Text = Format(nilai_ppn, "N2")
                    'EMI_Pembelian2.TxtPembelian_GrandTotal.Text = Format(dr("Grand"), "N2")
                End If
            End Using
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        EMI_Pembelian2.TxtPembelian_NoPO_Leave(ListView1, e)
        EMI_Pembelian2.ShowDialog()
        'Else
        '    MessageBox.Show(Base_Language.Lang_Global_FormAsal & " . .!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '    Exit Sub
        'End If

        'Me.Close()
        'Format(LvTanggal, "dd MMMM yyyy")
    End Sub
End Class
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button

Public Class EMI_Display_Selesai_Produksi
    Public asal As String
    Public Filter_Tambahan, Filter_NoInquiry, Filter_KdProduk As String
    Dim arrDataJenis As New ArrayList
    Private Sub SD_Pilih_Split_Produksi_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
        kosong()
    End Sub


    Private Sub kosong()

        Try
            OpenConn()
            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            'Base_Language.Get_Languages(Bahasa_Pilihan, "QC_Formula")
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        ComboBox3.Items.Clear()
        ComboBox3.Items.Add(Base_Language.Lang_Global_SeluruhCombobox) : arrDataJenis.Add("seluruh")
        ComboBox3.Items.Add("No Split PO") : arrDataJenis.Add("a.No_Transaksi")
        ComboBox3.Items.Add("No PO") : arrDataJenis.Add("a.No_PO")
        ComboBox3.Items.Add("Lokasi") : arrDataJenis.Add("a.Lokasi")
        ComboBox3.Items.Add("Kode Stock Owner") : arrDataJenis.Add("a.Kode_Stock_Owner")
        ComboBox3.Items.Add("Kode Barang") : arrDataJenis.Add("a.Kode_Barang")
        ComboBox3.Items.Add("Nama Barang") : arrDataJenis.Add("a.Nama")
        ComboBox3.Items.Add("Routing") : arrDataJenis.Add("ket_routing")
        ComboBox3.SelectedIndex = 0
        TextBox3.Text = ""

        ListView1.Columns.Clear()
        ListView1.Columns.Add("No Split PO", 130, HorizontalAlignment.Left)
        ListView1.Columns.Add("No PO", 130, HorizontalAlignment.Left)
        ListView1.Columns.Add("Lokasi", 120, HorizontalAlignment.Left)
        ListView1.Columns.Add("Tanggal", 100, HorizontalAlignment.Center)
        ListView1.Columns.Add("Jam", 100, HorizontalAlignment.Center)
        ListView1.Columns.Add("Kode Stock Owner", 130, HorizontalAlignment.Left)
        ListView1.Columns.Add("Kode Barang", 150, HorizontalAlignment.Left)
        ListView1.Columns.Add("Nama Barang", 250, HorizontalAlignment.Left)
        ListView1.Columns.Add("Jumlah", 100, HorizontalAlignment.Right)
        ListView1.Columns.Add("Satuan", 120, HorizontalAlignment.Left)
        ListView1.Columns.Add("Catatan", 250, HorizontalAlignment.Left)
        ListView1.Columns.Add("Routing", 140, HorizontalAlignment.Left)
        ListView1.Columns.Add("Tgl Produksi", 100, HorizontalAlignment.Center)
        ListView1.Columns.Add("Jam Produksi", 100, HorizontalAlignment.Center)
        ListView1.View = View.Details

        get_data_formula()

        Label2.Text = "Type"
        Label5.Text = "Value"
        Btn_Cari.Text = Base_Language.Lang_Global_Cari

    End Sub

    Private Sub get_data_formula()
        Try
            OpenConn()

            ListView1.Items.Clear()
            SQL = "select a.No_Transaksi,a.No_PO,a.Lokasi,a.Tanggal,a.Jam,a.UserID,a.Kode_Stock_Owner, "
            SQL = SQL & "a.Kode_Barang,b.Nama,a.Jumlah,a.Satuan,a.Catatan,c.Id_Routing,d.Keterangan as ket_routing,"
            SQL = SQL & "a.Tgl_Produksi,a.Jam_Produksi "
            SQL = SQL & "from Emi_Split_Production_Order a,Barang b,EMI_Order_Produksi c,EMI_Master_Routing d "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Stock_Owner = b.Kode_Stock_Owner "
            SQL = SQL & "and a.Kode_Barang = b.Kode_Barang and a.Kode_Perusahaan = c.Kode_Perusahaan and a.No_PO = c.No_Faktur "
            SQL = SQL & "and c.Kode_Perusahaan = d.Kode_Perusahaan and c.Id_Routing = d.Id_Routing and c.Status is null "
            SQL = SQL & "and c.Flag_Release = 'Y' and a.Flag_Produksi = 'Y' and a.Flag_Selesai_Produksi is null and c.Selesai is null "
            If ComboBox3.SelectedIndex <> 0 Then
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "
                SQL = SQL & arrDataJenis.Item(ComboBox3.SelectedIndex) & "  like  '%" & Trim(TextBox3.Text) & "%' "

            End If
            SQL = SQL & "order by a.No_Transaksi "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim lvw As ListViewItem
                    lvw = ListView1.Items.Add(dr("No_Transaksi"))
                    lvw.SubItems.Add(dr("No_PO"))
                    lvw.SubItems.Add(dr("Lokasi"))
                    lvw.SubItems.Add(Format(dr("Tanggal"), "dd-MMM-yyyy"))
                    lvw.SubItems.Add(dr("Jam"))
                    lvw.SubItems.Add(dr("Kode_Stock_Owner"))
                    lvw.SubItems.Add(dr("Kode_Barang"))
                    lvw.SubItems.Add(dr("Nama"))
                    lvw.SubItems.Add(Format(dr("Jumlah"), "N2"))
                    lvw.SubItems.Add(dr("Satuan"))
                    lvw.SubItems.Add(General_Class.CekNULL(dr("Catatan")))
                    lvw.SubItems.Add(dr("ket_routing"))
                    lvw.SubItems.Add(Format(dr("Tgl_Produksi"), "dd-MMM-yyyy"))
                    lvw.SubItems.Add(dr("Jam_Produksi"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Public Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
        If ComboBox3.SelectedIndex <> 0 Then
            If TextBox3.Text.Trim.Length = 0 Then
                MessageBox.Show("Keterangan cari harus diisi...!!", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                TextBox3.Focus()
            End If
        End If
        get_data_formula()
    End Sub

    Private Sub SD_Pilih_Split_Produksi_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub ListView1_DoubleClick(sender As Object, e As EventArgs) Handles ListView1.DoubleClick
        EMI_Selesai_Produksi.TextBox4.Text = ListView1.FocusedItem.Text
        EMI_Selesai_Produksi.ShowDialog()
    End Sub
End Class
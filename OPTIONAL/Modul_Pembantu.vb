Imports System.Text

Public Class Modul_Pembantu

    Private random As New Random()
    Private Tahun_MulaiProduksi As String = "2024"

    Private Sub ActivedForm()
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub LoadForm()
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub Ubah_SatuanKecil()
        SQL = "select dbo.ubah_satuan('KODE_PERUSAHAAN', 'masa','KODE BARANG', 'SATUAN AWAL', 'SATUAN TUJUAN', 'JUMLAH UBAH' ) as hasil"
        SQL = "select dbo.ubah_satuan('KODE_PERUSAHAAN', 'UANG','KODE BARANG', 'SATUAN AWAL', 'SATUAN TUJUAN', 'JUMLAH UBAH' ) as hasil"
    End Sub

    Private Sub GeT_HPP()
        SQL = "dbo.get_hpp(c.Serial_Number) as Harga"
    End Sub

    Private Sub CekRoleButton()
        If CekButtonRole("Ganti_Lokasi_Display_Penjualan") = "T" Then
            CloseTrans()
            CloseConn()
            MessageBox.Show("Anda Tidak Memiliki Akses Untuk Mengganti Lokasi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
    End Sub

    Private Function Generate_Batch_New(ByVal productionDate As String, ByVal lineCode As String, ByVal expDate As String) As String

        Dim productionTime As Date = Date.Parse(productionDate)
        Dim Produksi_Tanggal As String = productionTime.Day.ToString
        Dim Produksi_Bulan As String = productionTime.Month.ToString
        Dim Produksi_Tahun As String = If((productionTime.Year - Tahun_MulaiProduksi) Mod 9 = 0, 1, (productionTime.Year - Tahun_MulaiProduksi) Mod 9)
        Dim exp_date As String = Format(Date.Parse(expDate), "ddMMyy")

        Dim NumberToChar As New ArrayList From {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L",
                                        "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"}
        Dim finalBatch As String = ""
        finalBatch = Produksi_Tanggal & NumberToChar(Produksi_Bulan - 1) & Produksi_Tahun & lineCode & exp_date

        Return finalBatch

    End Function


    Private Function Generate_QR(ByVal KodeBarang As String, ByVal BatchCode As String) As String

        'Dim chars As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"
        'Dim UnixCode As New StringBuilder()

        'For i As Integer = 1 To 10
        '    Dim index As Integer = random.Next(0, chars.Length)
        '    UnixCode.Append(chars(index))
        'Next

        Dim Qr As String = ""
        Qr = KodeBarang & "-" & BatchCode

        Return Qr
    End Function

    Private Function Generate_Random_Kode(ByVal length As Integer) As String
        Dim chars As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"
        Dim result As New StringBuilder()

        For i As Integer = 1 To length
            Dim index As Integer = random.Next(0, chars.Length)
            result.Append(chars(index))
        Next

        Return result.ToString()
    End Function


    Private Function Generate_New_Sn(ByVal SerialNumber As String) As String
        'GENERATE SN BARU
        Dim hargaIsn As String = Get_Harga_SN(SerialNumber)

        Dim Random As New Random()
        Dim str As String = Format(Random.Next(0, 999), "000") & Format(CDate(FMenu.ToolStripStatusLabel3.Text), "HHmmss")
        Dim Kode_Unik As String = str.Substring(0, 5) & "BB" & Chr(64 + str.Substring(6, 1)) & str.Substring(6, Len(str) - 6)
        Dim SN_Baru As String = Kode_Unik & Tanda_SN & "01" & Tanda_SN & hargaIsn & Tanda_SN & "02" & Tanda_SN & Format(DateTime.Now, "yyyy-MM-dd")

        Return SN_Baru
    End Function


    Private Function Get_Rak_Kosong() As (String, String)

        Dim available_Id_Warehouse As String = ""
        Dim available_NoPallet As String = ""

        SQL = "select top(1) id_wms_warehouse_position, nomor_urut from view_warehouse_position_detail where kode_barang is null "
        Using Dr2 = OpenTrans(SQL)
            Do While Dr2.Read
                available_Id_Warehouse = Dr2("id_wms_warehouse_position")
                available_NoPallet = Dr2("nomor_urut")
            Loop
        End Using

        Return (available_Id_Warehouse, available_NoPallet)
    End Function

    Public Function Get_CurrentIndex()
        'SQL = "select IDENT_CURRENT('rencana_order') as urut"
        'Using Dr1 = OpenTrans(SQL)
        '    If Dr1.Read Then
        '        idRencana_Order = Dr1("urut")
        '    End If
        'End Using
    End Function


    '======================================================================================================================================================================================================
    'My.Application.ChangeCulture("en-us")
    'My.Application.ChangeUICulture("en-us")
    '======================================================================================================================================================================================================

    Private Sub CellEndEdit()

        'If Not DataGridView1.Rows.Count = 0 Then
        ''======================
        ''=     SET FORMAT     =
        ''======================
        'Dim culture As CultureInfo = CultureInfo.CurrentCulture

        'If Dgv_DataBarang.CurrentCell.ColumnIndex = CellQty Then

        '    Dim cellKuantity As String = Dgv_Data.CurrentCell.Value.ToString()

        '    If cellKuantity.Contains(",") Then
        '        MessageBox.Show("Kuantity Tidak Boleh Koma, Ganti dengan Titik", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '        Dgv_Data.CurrentCell.Value = Format(0, "N2")
        '        Exit Sub
        '    End If

        '    Dim nilai As Decimal = Decimal.Parse(cellKuantity)
        '    Dim formattedValue As String = nilai.ToString("N2", culture)

        '    Dgv_DataBarang.CurrentCell.Value = formattedValue
        'End If
        'End If
    End Sub

    Private Sub CellEnter()

        'If Not DataGridView1.Rows.Count = 0 Then
        ''======================
        ''=     SET FORMAT     =
        ''======================

        'If Dgv_DataBarang.CurrentCell.ColumnIndex = CellQty Then
        '    Dim cellKuantity As String = Dgv_DataBarang.CurrentCell.Value.ToString()

        '    If cellKuantity = "" Then
        '        Exit Sub
        '    End If

        '    Dim cleanedStr As String = HilangkanTanda(cellKuantity) ' Menghapus titik
        '    Dim nilai As Decimal = Decimal.Parse(cleanedStr)

        '    Dgv_DataBarang.CurrentCell.Value = nilai
        'End If
        'End If
    End Sub

    Private Sub CellLeave()
        'If Not DataGridView1.Rows.Count = 0 Then

        ''======================
        ''=     SET FORMAT     =
        ''======================
        'Dim culture As CultureInfo = CultureInfo.CurrentCulture

        'If Dgv_DataBarang.CurrentCell.ColumnIndex = CellQty Then
        '    Dim cellKuantity As String = Dgv_DataBarang.CurrentCell.Value.ToString()

        '    If cellKuantity = "" Then
        '        Exit Sub
        '    End If


        '    Dim nilai As Decimal = Decimal.Parse(cellKuantity)
        '    Dim formattedValue As String = nilai.ToString("N2", culture)

        '    Dgv_DataBarang.CurrentCell.Value = formattedValue

        'End If
        'End If
    End Sub


    Private Sub Format_Currency_Leave()
        'If Not Txt_Fix.Text.Length = 0 Then
        '    Try


        '        Dim culture As CultureInfo = CultureInfo.CurrentCulture
        '        'Dim input As String = Txt_Fix.Text.Replace(culture.NumberFormat.CurrencySymbol, "").Replace(",", "").Trim()
        '        Dim input As String = HilangkanTanda(Txt_Fix.Text)

        '        If Txt_Fix.Text.Contains(",") Then
        '            MessageBox.Show("Kuantity Tidak Boleh Koma, Ganti dengan Titik", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '            Txt_Fix.Text = ""
        '            Exit Sub
        '        End If

        '        If IsNumeric(input) Then
        '            If input.Length > Decimal.MaxValue Then
        '                MessageBox.Show("Angka Terlalu Panjang", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '                Txt_Fix.Text = ""
        '                Exit Sub
        '            End If

        '            Dim value As Decimal = Convert.ToDecimal(input)
        '            'Txt_Fix.Text = culture.NumberFormat.CurrencySymbol & " " & value.ToString("N2", culture) ' Jika Dengan Simbol Mata Uang
        '            Txt_Fix.Text = value.ToString("N2", culture) ' Jika Dengan Simbol Mata Uang
        '        Else
        '            MessageBox.Show("Kuantity Harus Berupa Angka!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '            Txt_Fix.Text = ""
        '        End If

        '    Catch ex As Exception
        '        MessageBox.Show("Terjadi Kesalahan saat Convert", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '        Txt_Fix.Text = ""
        '        Exit Sub
        '    End Try
        'End If
    End Sub

    Private Sub Format_Currency_Enter()
        'If Not Txt_Fix.Text.Length = 0 Then
        '    Try

        '        Dim culture As CultureInfo = CultureInfo.CurrentCulture
        '        'Txt_Fix.Text = Txt_Fix.Text.Replace(culture.NumberFormat.CurrencySymbol, "").Trim() ' Jika Dengan Simbol Mata Uang

        '        Dim cleanedStr As String = HilangkanTanda(Txt_Fix.Text).Trim() ' Menghapus titik
        '        Dim nilai As Decimal = Decimal.Parse(Val(cleanedStr))
        '        Txt_Fix.Text = nilai

        '    Catch ex As Exception
        '        MessageBox.Show("Terjadi Kesalahan saat Convert", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '        Txt_Fix.Text = ""
        '        Exit Sub
        '    End Try
        'End If
    End Sub


    Private Sub Get_Pallet_Tujuan()
        Dim palletTujuan As Double = 0
        SQL = "Select Top(1) nomor_urut from view_warehouse_position_detail where "
        SQL = SQL & "kode_Perusahaan ='" & KodePerusahaan & "' and kode_barang is null and "
        SQL = SQL & "id_wms_warehouse_position = 'ISI ID WAREHOUSE TUJUAN' "
        SQL = SQL & "order by nomor_urut "
        Using dr = OpenTrans(SQL)
            If dr.Read Then
                palletTujuan = dr("nomor_urut")
            Else
                dr.Close()
                CloseTrans()
                CloseConn()
                MessageBox.Show("data Rak Sudah Penuh . . ! ! ")
                Exit Sub
            End If
        End Using
    End Sub


    Private Sub Get_ID_Warehouse_Tujuan()

        'GET WAREHOUSE KOSONG
        Dim Id_WarehouseTujuan, NoPalletTujuan As String
        SQL = "SELECT TOP(1) "
        SQL = SQL & "a.id_wms_warehouse_position, b.nomor_urut "
        SQL = SQL & "FROM view_warehouse_position a, view_warehouse_position_detail b "
        SQL = SQL & "WHERE a.Id_WMS_Warehouse_Position = b.Id_WMS_Warehouse_Position "
        SQL = SQL & "AND a.kode_Perusahaan = b.kode_Perusahaan "
        SQL = SQL & "AND a.kode_Perusahaan = '" & KodePerusahaan & "' "
        SQL = SQL & "AND a.Kode_Stock_Owner = 'KODE STOCK OWNER' "
        SQL = SQL & "AND b.Kode_Barang IS NULL;"
        Using Dr = OpenTrans(SQL)
            If Dr.Read Then
                Id_WarehouseTujuan = Dr("id_wms_warehouse_position")
                NoPalletTujuan = Dr("nomor_urut")
            Else
                Dr.Close()
                CloseTrans()
                CloseConn()
                MessageBox.Show("Pallet Kosong Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
        End Using
    End Sub




    '====================================================================================================================================================================================================================================
    '=     HANDLE KEY PRESS
    '====================================================================================================================================================================================================================================

    Private Sub Handle_KeyPress_Focus()
        'If e.KeyChar = Chr(13) Then Tgl2.Focus()
    End Sub
    Private Sub Handle_Leve_AutoComplete()
        'If Txt_KdSupplier.Text.Trim.Length = 0 Then Exit Sub
        'If Lv_Supplier.Focused = True Then Exit Sub

        'Try
        '    OpenConn()

        '    If Not Txt_KdSupplier.Text = "--- SELURUH ---" Then

        '        SQL = "select Kode_Supplier, Nama from Suppliers where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Supplier = '" & Txt_KdSupplier.Text & "' "
        '        Using Dr = OpenTrans(SQL)
        '            If Dr.Read Then

        '                Txt_KdSupplier.Text = Dr("Kode_Supplier")
        '                Txt_NmSupplier.Text = Dr("Nama")
        '            Else
        '                MessageBox.Show("Supplier tidak ditemukan . . ! !", Judul)
        '                Txt_KdSupplier.Text = "" : Txt_NmSupplier.Text = ""
        '                Txt_KdSupplier.Focus()

        '            End If

        '            Me.Size = New Size(610, 333)
        '            Lv_Supplier.Location = New Point(600, 172)
        '            Lv_Supplier.Visible = False
        '        End Using

        '    End If

        '    CloseConn()
        'Catch ex As Exception
        '    CloseConn()
        '    MessageBox.Show(ex.Message)
        '    Exit Sub
        'End Try

    End Sub

    Private Sub Handle_KeyPress()
        'If e.KeyChar = Chr(13) Then
        '    If Txt_KdSupplier.Text.Trim.Length = 0 Then Txt_KdSupplier.Focus()
        '    Txt_KdSupplier_Leave(Txt_KdSupplier, e)

        '    Me.Size = New Size(610, 300)
        '    Lv_Supplier.Location = New Point(600, 167)
        '    Lv_Supplier.Visible = False

        '    Txt_KdBarang.Focus()
        'End If
    End Sub

    Private Sub Hadnle_KeyDown_AutoComplte()
        'If e.KeyCode = Keys.Down Then Lv_Supplier.Focus()
    End Sub

    Private Sub Haandle_KeyDown_LV_AutoComplte()
        'If e.KeyCode = Keys.Enter Then
        '    Lv_Faktur_DoubleClick(Lv_Faktur, e)
        'End If
    End Sub

    Private Sub Handle_DpubleKlik_AutoComplete()
        'If Lv_Faktur.Items.Count = 0 Or Lv_Faktur.FocusedItem.Index = -1 Then Exit Sub

        'Dim KdSupplier As String = Lv_Faktur.FocusedItem.SubItems(0).Text

        'Txt_Faktur.Text = KdSupplier

        'Me.Size = New Size(610, 300)
        'Lv_Faktur.Location = New Point(600, 139)
        'Lv_Faktur.Visible = False
        'Txt_KdSupplier.Focus()

    End Sub

    '======================================================================================================================================================
    '=     HANDLE FUTURE TEXT
    '======================================================================================================================================================
    'Private Sub Txt_Limit_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Limit.KeyPress

    '    If e.KeyChar = Chr(13) Then
    '        Btn_Simpan.PerformClick()
    '        e.Handled = True
    '        Exit Sub
    '    End If

    '    If Char.IsControl(e.KeyChar) Then
    '        e.Handled = True
    '        Exit Sub
    '    End If

    '    If Not Char.IsDigit(e.KeyChar) Then
    '        e.Handled = True
    '        Exit Sub
    '    End If

    '    Chk_Belum_Selesai.Checked = False

    '    Dim txt As TextBox = DirectCast(sender, TextBox)

    '    Dim futureText As String = txt.Text.Substring(0, txt.SelectionStart) & e.KeyChar & txt.Text.Substring(txt.SelectionStart + txt.SelectionLength)

    '    If futureText.Length > 1 AndAlso futureText.StartsWith("0") Then
    '        e.Handled = True
    '        Exit Sub
    '    End If

    '    Dim value As Integer
    '    If Integer.TryParse(futureText, value) Then
    '        If value < 0 OrElse value > 10000 Then
    '            e.Handled = True
    '           Exit Sub
    '        End If
    '    Else
    '        e.Handled = True
    '        Exit Sub
    '    End If

    'End Sub

    '======================================================================================================================================================
    '=     UNTUK KONTEKS MENU MUNCUL HANYA KETIKA ITEMS ADA ATAU DI SELECT
    '======================================================================================================================================================
    ' Event On Oppening of Context Menu
    Private Sub Konteks_menu_condition(sender As Object, e As System.ComponentModel.CancelEventArgs)
        'If Lv_PR.Items.Count = 0 Then
        '    e.Cancel = True
        '    Exit Sub
        'End If

        ''=========================================================
        ''=     CEK APAKAH MOUSE BERADA DI ATAS ROWS LISTVIEW     =
        ''=========================================================
        'Dim mousePos As Point = Lv_PR.PointToClient(Cursor.Position)
        'Dim info As ListViewHitTestInfo = Lv_PR.HitTest(mousePos)

        'If info.Item Is Nothing Then
        '    e.Cancel = True
        '    Exit Sub
        'End If

        'Lv_PR.FocusedItem = info.Item
        'info.Item.Selected = True
    End Sub

    '======================================================================================================================================================
    '=     UNTUK HANDLE POSISI LISTVIEW 
    '======================================================================================================================================================
    ' SYARATNYA OWNERDRAW PADA LISTVIEW = TRUE
    'Private Sub Letakan_Di_Function_Load()
    '    Private ReadOnly BodyAlignments As New Dictionary(Of Integer, StringAlignment) 'Bagian ini di letakan barisan code initial
    '       Lv_Display_Kendaraan.Columns.Clear()  BodyAlignments.Clear()
    '       Lv_Display_Kendaraan.Columns.Add("", 0) : BodyAlignments(1) = StringAlignment.Near
    '       Lv_Display_Kendaraan.Columns.Add("Plat Kendaraan", 150) : BodyAlignments(2) = StringAlignment.Far
    '       Lv_Display_Kendaraan.Columns.Add("Kapasitas Muatan", 130) : BodyAlignments(3) = StringAlignment.Near
    '       Lv_Display_Kendaraan.Columns.Add("Jenis Kendaraan", 150) : BodyAlignments(4) = StringAlignment.Center
    '       Lv_Display_Kendaraan.Columns.Add("STNK Sendiri", 150) : BodyAlignments(1) = StringAlignment.Near
    '       Lv_Display_Kendaraan.View = View.Details
    'End Sub

    'Private Sub Lv_Display_Kendaraan_DrawColumnHeader(sender As Object, e As DrawListViewColumnHeaderEventArgs) Handles Lv_Display_Kendaraan.DrawColumnHeader

    '    ' Background gradient
    '    Using bgBrush As New Drawing2D.LinearGradientBrush(
    '        e.Bounds,
    '        Color.FromArgb(245, 245, 245),
    '        Color.FromArgb(220, 220, 220),
    '        Drawing2D.LinearGradientMode.Vertical)

    '        e.Graphics.FillRectangle(bgBrush, e.Bounds)
    '    End Using

    '    ' Border bawah (lebih modern dari full border)
    '    Using borderPen As New Pen(Color.FromArgb(180, 180, 180))
    '        e.Graphics.DrawLine(
    '            borderPen,
    '            e.Bounds.Left,
    '            e.Bounds.Bottom - 1,
    '            e.Bounds.Right,
    '            e.Bounds.Bottom - 1)
    '    End Using

    '    ' Teks header
    '    Using sf As New StringFormat()
    '        sf.Alignment = StringAlignment.Center
    '        sf.LineAlignment = StringAlignment.Center
    '        sf.Trimming = StringTrimming.EllipsisCharacter

    '        ' Padding teks
    '        Dim textRect As Rectangle = Rectangle.Inflate(e.Bounds, -4, -2)

    '        e.Graphics.DrawString(
    '            e.Header.Text,
    '            Lv_Display_Kendaraan.Font,
    '            Brushes.Black,
    '            textRect,
    '            sf)
    '    End Using

    'End Sub


    'KETIKA MAU MENAMPILKAN STATUS BATAL ATAU TIDAK GUNAKAN ITEM TAG
    'Lv.Tag = General_Class.CekNULL(Dr("status"))

    'Private Sub Lv_Display_Kendaraan_DrawSubItem(sender As Object, e As DrawListViewSubItemEventArgs) Handles Lv_Display_Kendaraan.DrawSubItem

    '    Dim sf As New StringFormat()
    '    sf.LineAlignment = StringAlignment.Center

    '    If BodyAlignments.ContainsKey(e.ColumnIndex) Then
    '        sf.Alignment = BodyAlignments(e.ColumnIndex)
    '    Else
    '        sf.Alignment = StringAlignment.Near
    '    End If

    '    Dim status As String = CStr(e.Item.Tag)

    '    Dim bgBrush As Brush = Brushes.White
    '    Dim fgBrush As Brush = Brushes.Black

    '    If status = "Y" Then
    '        bgBrush = Brushes.DarkRed
    '        fgBrush = Brushes.White
    '    End If

    '    If e.Item.Selected Then
    '        bgBrush = SystemBrushes.Highlight
    '        fgBrush = SystemBrushes.HighlightText
    '    End If

    '    e.Graphics.FillRectangle(bgBrush, e.Bounds)
    '    e.Graphics.DrawString(e.SubItem.Text, Lv_Detail_RM.Font, fgBrush, e.Bounds, sf)

    '    sf.Dispose()

    'End Sub


    '======================================================================================================================================================
    '=     UNTUK CEGAK AGAR KETIKA TITLE BAR DOUBLE KLIK TIDAK MAXIMIZED
    '======================================================================================================================================================
    'Protected Overrides Sub WndProc(ByRef m As Message)
    '    ' WM_NCLBUTTONDBLCLK = 0xA3 (double click di title bar)
    '    If m.Msg = &HA3 Then
    '        Return  ' Abaikan pesan, sehingga form tidak maximize
    '    End If

    '    MyBase.WndProc(m)
    'End Sub


    '======================================================================================================================================================
    '=     UNTUK MOUSE BERUBAH JIKA DI ATAS ROW
    '======================================================================================================================================================
    'Private Sub Lv_Data_MouseMove(sender As Object, e As MouseEventArgs) Handles Lv_Data.MouseMove
    '    Dim info As ListViewHitTestInfo = Lv_Data.HitTest(e.Location)

    '    If info.Item IsNot Nothing Then
    '        ' Mouse sedang berada di atas row
    '        Lv_Data.Cursor = Cursors.Hand
    '    Else
    '        ' Mouse tidak mengenai row
    '        Lv_Data.Cursor = Cursors.Default
    '    End If
    'End Sub

    'Private Sub Lv_Data_MouseLeave(sender As Object, e As EventArgs) Handles Lv_Data.MouseLeave
    '    Lv_Data.Cursor = Cursors.Default
    'End Sub

    '=========================================
    '=     PAKAI HANDLER LEBIH FLEKSIBEL     =
    '=========================================

    'Private Sub ListView_MouseLeave(sender As Object, e As EventArgs)
    '    DirectCast(sender, ListView).Cursor = Cursors.Default
    'End Sub

    'Private Sub ListView_MouseMove(sender As Object, e As MouseEventArgs)

    '    Dim lv As ListView = DirectCast(sender, ListView)

    '    Dim info As ListViewHitTestInfo = lv.HitTest(e.Location)

    '    If info.Item IsNot Nothing Then
    '        lv.Cursor = Cursors.Hand
    '    Else
    '        lv.Cursor = Cursors.Default
    '    End If

    'End Sub



    '====================================
    '=     FUNGSI UNTUK PLACEHOLDER     =
    '====================================

    'Imports System.Runtime.InteropServices

    'Private Sub N_EMI_Master_Approval_Hierarchy_Waste_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    '    'Fungsi Set PlaceHolder
    '    SetCueBanner(Txt_Filter_Lokasi_Pengajuan, "Filter Lokasi")
    '    SetCueBanner(Txt_Filter_Lokasi_Pemindahan, "Filter Lokasi")

    'End Sub

    '<DllImport("user32.dll", CharSet:=CharSet.Unicode)>
    'Private Shared Function SendMessage(hWnd As IntPtr, msg As Integer, wParam As IntPtr, lParam As String) As IntPtr
    'End Function

    'Private Const EM_SETCUEBANNER As Integer = &H1501

    'Private Sub SetCueBanner(tb As TextBox, text As String)
    '    SendMessage(tb.Handle, EM_SETCUEBANNER, CType(1, IntPtr), text)
    'End Sub



    '======================================
    '=     MENERAPKAN KONSEP DEBOUNCE     =
    '======================================

    'TAMBAHKAN INI PAD ABAGIAN INTIAL DI FORM
    'Private WithEvents TypingTimer As New Timer()

    'Private Sub Txt_Filter_Lokasi_Pengajuan_TextChanged(sender As Object, e As EventArgs) Handles Txt_Filter_Lokasi_Pengajuan.TextChanged
    '    ' Reset timer setiap ada ketikan
    '    TypingTimer.Stop()
    '    TypingTimer.Start()
    'End Sub

    'Private Sub TypingTimer_Tick(sender As Object, e As EventArgs) Handles TypingTimer.Tick
    '    TypingTimer.Stop()

    '    Dim keyword As String = Txt_Filter_Lokasi_Pengajuan.Text.Trim()

    '    '==============================================
    '    '=     FUNGSI RELOAD TAMPILKAN DATA ULANG     =
    '    '==============================================
    '    Load_Data_Tab1()
    '    Load_Data_Tab2()

    'End Sub

End Class

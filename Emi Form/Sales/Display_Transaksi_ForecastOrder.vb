Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button

Public Class Display_Transaksi_ForecastOrder
    Public fStatus, asal, nfak, fvalidasi As String
    Dim Jenis = "Tampil_Inquiry"
    Dim arrCari As New ArrayList

    Private Sub Display_Transaksi_MaterialRequsition_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            OpenConn()

            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            Base_Language.Get_Languages(Bahasa_Pilihan, "Pembelian_Barang_Masuk")

            Lv_Barang.Columns.Add("No Faktur", 120, HorizontalAlignment.Left) '0
            Lv_Barang.Columns.Add("Tanggal", 120, HorizontalAlignment.Center) '1
            Lv_Barang.Columns.Add("Keterangan", 190, HorizontalAlignment.Left) '2
            Lv_Barang.Columns.Add("Lokasi", 150, HorizontalAlignment.Left) '3
            Lv_Barang.Columns.Add("Bulan", 82, HorizontalAlignment.Center) '4
            Lv_Barang.Columns.Add("Tahun", 82, HorizontalAlignment.Center) '5
            Lv_Barang.Columns.Add("Ref", 0, HorizontalAlignment.Center) '6
            Lv_Barang.Columns.Add("Validasi", 0, HorizontalAlignment.Center) '7
            Lv_Barang.View = View.Details

            Lv_Barang_Detail.Columns.Add("Kode Barang", 300, HorizontalAlignment.Left)
            Lv_Barang_Detail.Columns.Add("Nama Barang", 450, HorizontalAlignment.Left)
            Lv_Barang_Detail.View = View.Details

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        kosong()
    End Sub

    Private Sub kosong()
        '
        Try
            OpenConn()

            Cmb_Kolom.Items.Clear() : arrCari.Clear()
            Cmb_Kolom.Items.Add("No Faktur") : arrCari.Add("No_Faktur")
            Cmb_Kolom.Items.Add("Keterangan") : arrCari.Add("Keterangan")
            Cmb_Kolom.Items.Add("Bulan") : arrCari.Add("Bulan")
            Cmb_Kolom.Items.Add("Tahun") : arrCari.Add("Tahun")
            Cmb_Kolom.SelectedIndex = -1

            Txt_Value.Text = ""
            Cb_Referensi.Checked = False

            Lv_Barang.Items.Clear()
            SQL = "select No_Faktur,Tanggal,Keterangan,Lokasi,Bulan,Tahun,flag_Referensi,flag_validasi "
            SQL = SQL & "from EMI_Transaksi_Sales_Forecasting "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' order by Tanggal Desc "

            OpenConn()

            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim LV As New ListViewItem
                    LV = Lv_Barang.Items.Add(dr("No_Faktur"))
                    LV.SubItems.Add(Format(dr("Tanggal"), "dd-MMMM-yyyy"))
                    LV.SubItems.Add(dr("Keterangan"))
                    LV.SubItems.Add(dr("Lokasi"))
                    LV.SubItems.Add(dr("Bulan"))
                    LV.SubItems.Add(dr("Tahun"))
                    If General_Class.CekNULL(dr("Flag_referensi")) = "" Then
                        LV.SubItems.Add("-")
                    Else
                        LV.SubItems.Add(dr("Flag_referensi"))
                    End If
                    If General_Class.CekNULL(dr("flag_validasi")) = "" Then
                        LV.SubItems.Add("-")
                    Else
                        LV.SubItems.Add(dr("flag_validasi"))
                    End If
                Loop
            End Using



            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        '
        Try
            OpenConn()

            If CekButtonRole("EMI_Transaksi_ForecastOrder_PPIC") = "Y" Then
                fStatus = "Transaksi_ForecastOrder_PPIC"
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        '
        Try
            OpenConn()

            If CekButtonRole("EMI_Transaksi_ForecastOrder_PPIC") = "Y" Then
                fStatus = "Transaksi_ForecastOrder_PPIC"
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        '
        Try
            OpenConn()

            If CekButtonRole("EMI_Transaksi_ForecastOrder_Sales") = "Y" Then
                fStatus = "Transaksi_ForecastOrder_Sales"
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        '
        Try
            OpenConn()

            If CekButtonRole("EMI_Transaksi_ForecastOrder_Display") = "Y" Then
                fStatus = "Transaksi_ForecastOrder_Display"
            End If

            If fStatus = "Transaksi_ForecastOrder_Sales" Then
                Cb_Referensi.Visible = True
                Btn_New.Enabled = True
            Else
                Cb_Referensi.Visible = False
                Btn_New.Enabled = False
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub Display_Transaksi_MaterialRequsition_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Public Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        kosong()
    End Sub

    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
        If Cmb_Kolom.Text.Trim.Length = 0 Then
            MessageBox.Show("Parameter cari harus diisi ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Kolom.Focus()
            Exit Sub
        ElseIf Txt_Value.Text.Trim.Length = 0 Then
            MessageBox.Show("value cari harus diisi ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_Value.Focus()
            Exit Sub
        End If

        Try
            OpenConn()

            Lv_Barang.Items.Clear()
            SQL = "select No_Faktur,Tanggal,Keterangan,Lokasi,Bulan,Tahun,Flag_Referensi from EMI_Transaksi_Sales_Forecasting where "
            SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and " & arrCari.Item(Cmb_Kolom.SelectedIndex) & " like '%" & Txt_Value.Text & "%' "
            If Cb_Referensi.Checked = True Then
                SQL = SQL & "and Flag_Referensi = 'Y' "
            Else
                SQL = SQL & " "
            End If
            SQL = SQL & "order by Tanggal Desc "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim LV As New ListViewItem
                    LV = Lv_Barang.Items.Add(dr("No_Faktur"))
                    LV.SubItems.Add(Format(dr("Tanggal"), "dd-MMMM-yyyy"))
                    LV.SubItems.Add(dr("Keterangan"))
                    LV.SubItems.Add(dr("Lokasi"))
                    LV.SubItems.Add(dr("Bulan"))
                    LV.SubItems.Add(dr("Tahun"))
                    If General_Class.CekNULL(dr("Flag_referensi")) = "" Then
                        LV.SubItems.Add("T")
                    Else
                        LV.SubItems.Add(dr("Flag_referensi"))
                    End If
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Btn_New_Click(sender As Object, e As EventArgs) Handles Btn_New.Click
        asal = "Baru"
        EMI_Transaksi_ForecastOrder.ShowDialog()
    End Sub

    Private Sub Lv_Barang_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_Barang.SelectedIndexChanged

        If Lv_Barang.Items.Count = 0 Then Exit Sub

        Try
            OpenConn()

            Lv_Barang_Detail.Items.Clear()
            SQL = "select a.No_Faktur,a.Kode_Barang,b.Nama,a.Kode_Stock_Owner from EMI_Transaksi_Sales_Forecasting_Detail a,"
            SQL = SQL & "Barang b where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang and "
            SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Faktur = '" & Lv_Barang.FocusedItem.Text & "' "
            SQL = SQL & "group by a.No_Faktur,a.Kode_Barang,b.Nama,a.Kode_Stock_Owner "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim LV As New ListViewItem
                    LV = Lv_Barang_Detail.Items.Add(dr("Kode_Barang"))
                    LV.SubItems.Add(dr("Nama"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Lv_Barang_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Barang.DoubleClick
        If Lv_Barang.Items.Count = 0 Then Exit Sub

        If Cb_Referensi.Checked = False Then
            asal = "isi_lv"
            EMI_Transaksi_ForecastOrder.fRef = Lv_Barang.FocusedItem.SubItems(6).Text
            EMI_Transaksi_ForecastOrder.fValidasi = Lv_Barang.FocusedItem.SubItems(7).Text
            EMI_Transaksi_ForecastOrder.Txt_NoFaktur.Text = Lv_Barang.FocusedItem.Text
            EMI_Transaksi_ForecastOrder.ShowDialog()
        Else
            asal = "Ref" 'Referensi
            nfak = Lv_Barang.FocusedItem.Text
            EMI_Transaksi_ForecastOrder.fValidasi = Lv_Barang.FocusedItem.SubItems(7).Text
            EMI_Transaksi_ForecastOrder.ShowDialog()
        End If

        ''EMI_Transaksi_ForecastOrder.Txt_NoFaktur.Text = Lv_Barang.FocusedItem.Text
        ''EMI_Transaksi_ForecastOrder.Txt_NoFaktur_Leave(EMI_Transaksi_ForecastOrder.Txt_NoFaktur, EventArgs.Empty)
        ''EMI_Transaksi_ForecastOrder.ShowDialog()

    End Sub

    Private Sub Cb_Referensi_CheckedChanged(sender As Object, e As EventArgs) Handles Cb_Referensi.CheckedChanged
        Try
            OpenConn()
            If Cb_Referensi.Checked = True Then
                Lv_Barang.Items.Clear()
                SQL = "select No_Faktur,Tanggal,Keterangan,Lokasi,Bulan,Tahun,Flag_Referensi,flag_validasi "
                SQL = SQL & "from EMI_Transaksi_Sales_Forecasting "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and Flag_Referensi = 'Y' order by Tanggal Desc "
                Using dr = OpenTrans(SQL)
                    Do While dr.Read
                        Dim LV As New ListViewItem
                        LV = Lv_Barang.Items.Add(dr("No_Faktur"))
                        LV.SubItems.Add(Format(dr("Tanggal"), "dd-MMMM-yyyy"))
                        LV.SubItems.Add(dr("Keterangan"))
                        LV.SubItems.Add(dr("Lokasi"))
                        LV.SubItems.Add(dr("Bulan"))
                        LV.SubItems.Add(dr("Tahun"))
                        If General_Class.CekNULL(dr("Flag_referensi")) = "" Then
                            LV.SubItems.Add("-")
                        Else
                            LV.SubItems.Add(dr("Flag_referensi"))
                        End If
                        If General_Class.CekNULL(dr("flag_validasi")) = "" Then
                            LV.SubItems.Add("-")
                        Else
                            LV.SubItems.Add(dr("flag_validasi"))
                        End If
                    Loop
                End Using
            Else
                Lv_Barang.Items.Clear()
                SQL = "select No_Faktur,Tanggal,Keterangan,Lokasi,Bulan,Tahun,Flag_Referensi,flag_validasi "
                SQL = SQL & "from EMI_Transaksi_Sales_Forecasting "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' order by Tanggal Desc "
                Using dr = OpenTrans(SQL)
                    Do While dr.Read
                        Dim LV As New ListViewItem
                        LV = Lv_Barang.Items.Add(dr("No_Faktur"))
                        LV.SubItems.Add(Format(dr("Tanggal"), "dd-MMMM-yyyy"))
                        LV.SubItems.Add(dr("Keterangan"))
                        LV.SubItems.Add(dr("Lokasi"))
                        LV.SubItems.Add(dr("Bulan"))
                        LV.SubItems.Add(dr("Tahun"))
                        If General_Class.CekNULL(dr("Flag_referensi")) = "" Then
                            LV.SubItems.Add("-")
                        Else
                            LV.SubItems.Add(dr("Flag_referensi"))
                        End If
                        If General_Class.CekNULL(dr("flag_validasi")) = "" Then
                            LV.SubItems.Add("-")
                        Else
                            LV.SubItems.Add(dr("flag_validasi"))
                        End If
                    Loop
                End Using

            End If
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Lv_Barang_Disposed(sender As Object, e As EventArgs) Handles Lv_Barang.Disposed

    End Sub
    '
End Class
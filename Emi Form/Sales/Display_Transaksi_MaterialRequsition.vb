Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button


Public Class Display_Transaksi_MaterialRequsition
    Public fStatus, asal, nfak As String
    Dim Jenis = "Tampil_Inquiry"
    Dim arrCari As New ArrayList

    Private Sub Display_Transaksi_MaterialRequsition_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            OpenConn()

            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            Base_Language.Get_Languages(Bahasa_Pilihan, "Pembelian_Barang_Masuk")

            Lv_Barang.Columns.Add("No Faktur", 120, HorizontalAlignment.Left)
            Lv_Barang.Columns.Add("Tanggal", 120, HorizontalAlignment.Center)
            Lv_Barang.Columns.Add("Keterangan", 200, HorizontalAlignment.Left)
            Lv_Barang.Columns.Add("Lokasi", 150, HorizontalAlignment.Left)
            Lv_Barang.Columns.Add("Bulan", 100, HorizontalAlignment.Center)
            Lv_Barang.Columns.Add("Tahun", 100, HorizontalAlignment.Center)
            Lv_Barang.Columns.Add("Ref", 0, HorizontalAlignment.Center)
            Lv_Barang.View = View.Details

            ListView1.Columns.Add("Kode Barang", 150, HorizontalAlignment.Left)
            ListView1.Columns.Add("Nama Barang", 640, HorizontalAlignment.Left)
            ListView1.View = View.Details

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        kosong()
    End Sub

    Private Sub kosong()
        Try
            OpenConn()

            cmbCari.Items.Clear() : arrCari.Clear()
            cmbCari.Items.Add("No Faktur") : arrCari.Add("No_Faktur")
            cmbCari.Items.Add("Keterangan") : arrCari.Add("Keterangan")
            cmbCari.Items.Add("Bulan") : arrCari.Add("Bulan")
            cmbCari.Items.Add("Tahun") : arrCari.Add("Tahun")
            cmbCari.SelectedIndex = -1

            txtvalue.Text = ""

            Lv_Barang.Items.Clear()
            ListView1.Items.Clear()
            SQL = "select No_Faktur,Tanggal,Keterangan,Lokasi,Bulan,Tahun,flag_Referensi from EMI_Transaksi_Material_Requsition where "
            SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' order by Tanggal Desc "
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

        Try
            OpenConn()

            If CekButtonRole("MRP_PPIC") = "Y" Then

            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Try
            OpenConn()

            If CekButtonRole("MRP_Formulator") = "Y" Then
                fStatus = "MRP_Formulator"
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Try
            OpenConn()

            If CekButtonRole("MRP_Display") = "Y" Then
                CheckBox1.Visible = False
                fStatus = "MRP_Display"
            End If

            If fStatus = "MRP_Formulator" Then
                CheckBox1.Visible = True
                Button1.Enabled = True
            Else
                CheckBox1.Visible = False
                Button1.Enabled = False
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

    Public Sub BtnInquiry_Refresh_Click(sender As Object, e As EventArgs) Handles BtnInquiry_Refresh.Click
        kosong()
    End Sub

    Private Sub BtnInquiry_Cari_Click(sender As Object, e As EventArgs) Handles BtnInquiry_Cari.Click
        If cmbCari.Text.Trim.Length = 0 Then
            MessageBox.Show("Parameter cari harus diisi ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            cmbCari.Focus()
            Exit Sub
        ElseIf txtvalue.Text.Trim.Length = 0 Then
            MessageBox.Show("value cari harus diisi ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtvalue.Focus()
            Exit Sub
        End If

        Try
            OpenConn()

            Lv_Barang.Items.Clear()
            ListView1.Items.Clear()
            SQL = "select No_Faktur,Tanggal,Keterangan,Lokasi,Bulan,Tahun,Flag_Referensi from EMI_Transaksi_Material_Requsition where "
            SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and " & arrCari.Item(cmbCari.SelectedIndex) & " like '%" & txtvalue.Text & "%' "
            If CheckBox1.Checked = True Then
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

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        asal = "Baru"
        EMI_Transaksi_MaterialRequisition.ShowDialog()
    End Sub

    Private Sub Lv_Barang_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Barang.DoubleClick
        If Lv_Barang.Items.Count = 0 Then Exit Sub

        If CheckBox1.Checked = False Then
            asal = "isi_lv"
            EMI_Transaksi_MaterialRequisition.fRef = Lv_Barang.FocusedItem.SubItems(6).Text
            EMI_Transaksi_MaterialRequisition.TxtBarangMasuk_NoFaktur.Text = Lv_Barang.FocusedItem.Text
            EMI_Transaksi_MaterialRequisition.ShowDialog()
        Else
            asal = "Ref"
            nfak = Lv_Barang.FocusedItem.Text
            EMI_Transaksi_MaterialRequisition.ShowDialog()
        End If


        'EMI_Transaksi_MaterialRequsition.TxtBarangMasuk_NoFaktur_Leave(Lv_Barang, e)

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        Try
            OpenConn()
            If CheckBox1.Checked = True Then
                Lv_Barang.Items.Clear()
                ListView1.Items.Clear()
                SQL = "select No_Faktur,Tanggal,Keterangan,Lokasi,Bulan,Tahun,Flag_Referensi from EMI_Transaksi_Material_Requsition where "
                SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and Flag_Referensi = 'Y' order by Tanggal Desc "
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

            Else
                Lv_Barang.Items.Clear()
                ListView1.Items.Clear()
                SQL = "select No_Faktur,Tanggal,Keterangan,Lokasi,Bulan,Tahun,Flag_Referensi from EMI_Transaksi_Material_Requsition where "
                SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' order by Tanggal Desc "
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
            End If
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Lv_Barang_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_Barang.SelectedIndexChanged
        If Lv_Barang.Items.Count = 0 Then Exit Sub

        Try
            OpenConn()

            ListView1.Items.Clear()
            SQL = "select a.No_Faktur,a.Kode_Barang,b.Nama,a.Kode_Stock_Owner from EMI_Transaksi_Material_Requsition_Detail a,"
            SQL = SQL & "Barang b where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang and "
            SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Faktur = '" & Lv_Barang.FocusedItem.Text & "' "
            SQL = SQL & "group by a.No_Faktur,a.Kode_Barang,b.Nama,a.Kode_Stock_Owner "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim LV As New ListViewItem
                    LV = ListView1.Items.Add(dr("Kode_Barang"))
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
End Class
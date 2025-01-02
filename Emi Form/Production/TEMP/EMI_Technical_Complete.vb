Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button

Public Class EMI_Technical_Complete
    Dim arrcari, arrId_line, arrId_Karyawan As New ArrayList
    Dim Jenis = "Transaksi_Produksi"

    Private Sub Transaksi_Produksi_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
        Try
            OpenConn()



            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)

            'Label1.Text = Base_Language.Lang_Transaksi_Produksi_Judul
            'Label8.Text = Base_Language.Lang_Global_No_Transaksi
            Label1.Text = "Technical Completion"
            Label6.Text = Base_Language.Lang_Transaksi_Produksi_No_Rencana
            Label7.Text = Base_Language.Lang_Global_Tanggal_Produksi
            Label2.Text = Base_Language.Lang_Global_Jam
            'Label3.Text = "Line"
            'Label4.Text = Base_Language.Lang_Transaksi_Produksi_No_Batch
            'Label5.Text = "Operator"
            Btn_Refresh.Text = Base_Language.Lang_Global_Simpan

            'ListView2.Columns.Clear()
            'ListView2.Columns.Add(Base_Language.Lang_Global_No_PO, 140, HorizontalAlignment.Left)
            'ListView2.Columns.Add(Base_Language.Lang_Global_Lokasi, 0, HorizontalAlignment.Left)
            'ListView2.Columns.Add(Base_Language.Lang_Global_KodeCustomer, 140, HorizontalAlignment.Left)
            'ListView2.Columns.Add(Base_Language.Lang_Global_NamaCustomer, 200, HorizontalAlignment.Left)
            'ListView2.Columns.Add(Base_Language.Lang_Global_KodeBarang, 130, HorizontalAlignment.Left)
            'ListView2.Columns.Add(Base_Language.Lang_Global_NamaBarang, 220, HorizontalAlignment.Left)
            'ListView2.Columns.Add(Base_Language.Lang_Global_Jumlah, 100, HorizontalAlignment.Center)
            'ListView2.Columns.Add(Base_Language.Lang_Global_Satuan, 90, HorizontalAlignment.Center)
            'ListView2.View = View.Details

            'ComboBox2.Items.Clear() : arrId_line.Clear()
            'SQL = "select Id_Line,Kode_Line from EMI_Line order by Kode_Line"
            'Using dr = OpenTrans(SQL)
            '    Do While dr.Read
            '        ComboBox2.Items.Add(dr("Kode_Line"))
            '        arrId_line.Add(dr("Id_Line"))
            '    Loop
            'End Using
            'TextBox3.Text = ""

            'ComboBox3.Items.Clear() : arrId_Karyawan.Clear()
            'SQL = "select a.Id_Karyawan,a.Nama from Emi_Karyawan a,Emi_Jabatan_Internal b "
            'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and "
            'SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' and "
            'SQL = SQL & "a.Id_Jabatan = b.Id_Jabatan and b.Flag_Tampil_Produksi = 'Y' "
            'SQL = SQL & "order by Nama"
            'Using dr = OpenTrans(SQL)
            '    Do While dr.Read
            '        ComboBox3.Items.Add(dr("Nama"))
            '        arrId_Karyawan.Add(dr("Id_Karyawan"))
            '    Loop
            'End Using

            get_no_faktur()
            isiOtomatis()
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        TextBox4_Leave(Nothing, e)
    End Sub


    Private Sub isiOtomatis()
        Dim dgvcc As DataGridViewComboBoxCell


        TextBox4.Text = "RP0-08/24-0001"
        TextBox1.Text = "10:30:00"

        Dgv.Rows.Add(3)

        Dgv.Rows(0).Cells(0).Value = "KD001"
        Dgv.Rows(0).Cells(1).Value = "Downtime"
        Dgv.Rows(0).Cells(2).Value = "MENIT"
        Dgv.Rows(0).Cells(3).Value = "132"
        Dgv.Rows(0).Cells(4).ReadOnly = True
        Dgv.Rows(0).Cells(5).Value = "Kesalahan pada resep sehingga harus dihentikan sementara."



        dgvcc = Dgv.Rows(2).Cells(4)
        dgvcc.Items.Clear()

        dgvcc.Items.Add("YA")
        dgvcc.Items.Add("TIDAK")
        dgvcc.Items.Add("Dengan Catatan")

        Dgv.Rows(1).Cells(0).Value = "KD002"
        Dgv.Rows(1).Cells(1).Value = "Kelengkapan Dokumen"
        Dgv.Rows(1).Cells(2).Value = "NONE"
        Dgv.Rows(1).Cells(3).ReadOnly = True
        'Dgv.Rows(1).Cells(4).Value = 1
        Dgv.Rows(1).Cells(5).Value = ""

        dgvcc = Dgv.Rows(1).Cells(4)
        dgvcc.Items.Clear()
        dgvcc.Items.Add("YA")
        dgvcc.Items.Add("TIDAK")
        dgvcc.Items.Add("Dengan Catatan")





        Dgv.Rows(2).Cells(0).Value = "KD003"
        Dgv.Rows(2).Cells(1).Value = "Produksi Selesai"
        Dgv.Rows(2).Cells(2).Value = "NONE"
        Dgv.Rows(2).Cells(3).ReadOnly = True
        'Dgv.Rows(2).Cells(4).Value = 2
        Dgv.Rows(2).Cells(5).Value = "Ada beberapa PO barang yang belum diproduksi"

        'For i As Integer = 0 To 5
        '    dgvcc = Dgv.Rows(i).Cells(1)
        '    dgvcc.Items.Clear()

        '    dgvcc.Items.Add("YA")
        '    dgvcc.Items.Add("TIDAK")
        'Next






    End Sub

    Private Sub get_no_faktur()
        TxtFormulator_NoFaktur.Text = fProduksi & Format(tgl_skg, "MMyy") & "-" &
                             General_Class.Get_Last_Number2("Emi_Produksi", "No_Faktur", 5,
                             "Kode_perusahaan", KodePerusahaan,
                             "And", "substring(No_Faktur, 1, " & Len(fProduksi) + 4 & ")", fProduksi & Format(tgl_skg, "MMyy"))
    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        'If TxtFormulator_NoFaktur.Text.Trim.Length = 0 Then
        '    MessageBox.Show(Base_Language.Lang_Global_Error_No_Transaksi, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '    TxtFormulator_NoFaktur.Focus() : Exit Sub
        'ElseIf TextBox3.Text.Trim.Length = 0 Then
        '    MessageBox.Show(Base_Language.Lang_Transaksi_Produksi_Error_No_Batch, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '    TextBox3.Focus() : Exit Sub
        'ElseIf ComboBox3.Text.Trim.Length = 0 Then
        '    MessageBox.Show(Base_Language.Lang_Transaksi_Produksi_Error_Operator, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '    ComboBox3.Focus() : Exit Sub
        'End If
        'get_jam()
        'Try
        '    OpenConn()

        '    SQL = "INSERT INTO Emi_Produksi(Kode_Perusahaan,No_Faktur,No_Rencana_Produksi,"
        '    SQL = SQL & "Tanggal,Jam,No_Batch,Operator,Line, UserID) VALUES("
        '    SQL = SQL & "'" & KodePerusahaan & "','" & TxtFormulator_NoFaktur.Text & "',"
        '    SQL = SQL & "'" & TextBox4.Text & "','" & Format(tgl_skg, "yyyy-MM-dd") & "',"
        '    SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "','" & TextBox3.Text & "',"
        '    SQL = SQL & "'" & arrId_Karyawan.Item(ComboBox3.SelectedIndex) & "',"
        '    SQL = SQL & "'" & arrId_line.Item(ComboBox2.SelectedIndex) & "', '" & UserID & "')"
        '    ExecuteTrans(SQL)

        '    SQL = "update emi_rencana_produksi set Flag_Produksi = 'Y' where "
        '    SQL = SQL & "Kode_perusahaan = '" & KodePerusahaan & "' and "
        '    SQL = SQL & "No_Faktur = '" & TextBox4.Text & "'"
        '    ExecuteTrans(SQL)

        '    CloseConn()
        'Catch ex As Exception
        '    CloseConn()
        '    MessageBox.Show(ex.Message)
        '    Exit Sub
        'End Try
        'EMI_Produksi_Display.Button1_Click(Btn_Refresh, e)
        'Me.Close()
    End Sub

    Private Sub Dgv_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv.CellContentClick

    End Sub

    Private Sub Transaksi_Produksi_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Public Sub TextBox4_Leave(sender As Object, e As EventArgs) Handles TextBox4.Leave
        'Try
        '    OpenConn()

        '    SQL = "Select a.No_Faktur,a.Tanggal_Produksi,a.Jam_Produksi,a.Line,b.Kode_Line "
        '    SQL = SQL & "from emi_rencana_produksi a,EMI_Line b "
        '    SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' and "
        '    SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Line = b.Id_Line and "
        '    SQL = SQL & "a.Status is null and a.Selesai is null and "
        '    SQL = SQL & "a.No_Faktur = '" & TextBox4.Text & "' "
        '    SQL = SQL & "order by a.Tanggal_Produksi,a.Jam_Produksi"
        '    Using dr = OpenTrans(SQL)
        '        If dr.Read Then
        '            DateTimePicker1.Value = Format(dr("Tanggal_Produksi"), "dd MMMM yyyy")
        '            TextBox1.Text = dr("Jam_Produksi")
        '            ComboBox2.Text = dr("Kode_Line")
        '        End If
        '    End Using

        '    ListView2.Items.Clear()
        '    SQL = "select a.No_PO,a.Kode_Stock_Owner,a.Kode_Customer,b.Nama as nama_cus,"
        '    SQL = SQL & "a.Kode_Barang,c.Nama as nama_brg,a.Jumlah,a.Satuan from "
        '    SQL = SQL & "emi_rencana_produksi_detail a,Customers b, Barang c where "
        '    SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and "
        '    SQL = SQL & "a.Kode_Perusahaan = c.Kode_Perusahaan and "
        '    SQL = SQL & "a.Kode_Customer = b.Kode_Customer and "
        '    SQL = SQL & "a.Kode_Stock_Owner = c.Kode_Stock_Owner and "
        '    SQL = SQL & "a.Kode_Barang = c.Kode_Barang and "
        '    SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' and "
        '    SQL = SQL & "a.No_Faktur = '" & TextBox4.Text & "'"
        '    Using dr = OpenTrans(SQL)
        '        Do While dr.Read
        '            Dim lvw As ListViewItem
        '            lvw = ListView2.Items.Add(dr("No_PO"))
        '            lvw.SubItems.Add(dr("Kode_Stock_Owner"))
        '            lvw.SubItems.Add(dr("Kode_Customer"))
        '            lvw.SubItems.Add(dr("nama_cus"))
        '            lvw.SubItems.Add(dr("Kode_Barang"))
        '            lvw.SubItems.Add(dr("nama_brg"))
        '            lvw.SubItems.Add(Format(dr("Jumlah"), "N0"))
        '            lvw.SubItems.Add(dr("Satuan"))
        '        Loop
        '    End Using

        '    CloseConn()
        'Catch ex As Exception
        '    CloseConn()
        '    MessageBox.Show(ex.Message)
        '    Exit Sub
        'End Try
    End Sub
End Class
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button

Public Class EMI_Selesai_Produksi
    Dim arrcari, arrId_line, arrId_Karyawan, arrInisialFaktur, arrInisialRouting As New ArrayList
    Dim Jenis = "Transaksi_Produksi"
    Dim kd_so, no_fak As String

    Private Sub Transaksi_Produksi_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
        Try
            OpenConn()

            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)

            Label1.Text = Base_Language.Lang_Transaksi_Produksi_Judul
            'Label8.Text = Base_Language.Lang_Global_No_Transaksi
            Label6.Text = Base_Language.Lang_Transaksi_Produksi_No_Rencana
            Label7.Text = Base_Language.Lang_Global_Tanggal_Produksi
            Label2.Text = Base_Language.Lang_Global_Jam
            Label4.Text = Base_Language.Lang_Transaksi_Produksi_No_Batch
            Label5.Text = "Operator"
            Btn_Refresh.Text = Base_Language.Lang_Global_Simpan

            TextBox3.Text = ""

            ComboBox3.Items.Clear() : arrId_Karyawan.Clear()
            SQL = "select a.Id_Karyawan,a.Nama from Emi_Karyawan a,Emi_Jabatan_Internal b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and "
            SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "a.Id_Jabatan = b.Id_Jabatan and b.Flag_Tampil_Produksi = 'Y' "
            SQL = SQL & "order by Nama"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    ComboBox3.Items.Add(dr("Nama"))
                    arrId_Karyawan.Add(dr("Id_Karyawan"))
                Loop
            End Using

            arrInisialFaktur.Clear() : CmbFormulator_LokasiInquiry.Items.Clear()
            SQL = "select Kode_Stock_Owner, persediaan ,inisial_faktur from stock_owner where kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and aktif = 'Y'  and kode_stock_owner = '" & Lokasi & "' order by Kode_Stock_Owner"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    CmbFormulator_LokasiInquiry.Items.Add(dr("Kode_Stock_Owner")) : arrInisialFaktur.Add(dr("inisial_faktur"))
                Loop
            End Using

            arrInisialRouting.Clear() : ComboBox1.Items.Clear()
            SQL = "select Id_Routing,Keterangan from EMI_Master_Routing where kode_perusahaan = '" & KodePerusahaan & "' order by Keterangan"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    ComboBox1.Items.Add(dr("Keterangan")) : arrInisialRouting.Add(dr("Id_Routing"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        TextBox4_Leave(Nothing, e)
    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        If TextBox3.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Transaksi_Produksi_Error_No_Batch, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox3.Focus() : Exit Sub
        ElseIf ComboBox3.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Transaksi_Produksi_Error_Operator, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox3.Focus() : Exit Sub
        ElseIf DateTimePicker3.Value < DateTimePicker1.Value Then
            MessageBox.Show("Tanggal selesai mulai tidak boleh lebih dari tanggal expired . . ! !", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Information)
            DateTimePicker1.Focus() : Exit Sub
        End If
        get_jam()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            SQL = "update Emi_Split_Production_Order set "
            SQL = SQL & "Flag_Selesai_Produksi = 'Y',Tgl_Selesai_Produksi = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "',"
            SQL = SQL & "Jam_Selesai_Produksi = '" & Format(DateTimePicker2.Value, "HH:mm:ss") & "',UserID_Selesai_Produksi = '" & UserID & "',"
            SQL = SQL & "Tgl_Expired = '" & Format(DateTimePicker3.Value, "yyyy-MM-dd") & "' "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Transaksi = '" & TextBox4.Text & "' "
            ExecuteTrans(SQL)

            SQL = "select a.No_Faktur,a.Jumlah, "
            SQL = SQL & "ISNULL((select sum(z.Jumlah) from Emi_Split_Production_Order z where "
            SQL = SQL & "z.No_PO = a.No_Faktur and z.Flag_Produksi = 'Y' and z.Flag_Selesai_Produksi = 'Y' ),0) as Jml_Sdh_Split "
            SQL = SQL & "from EMI_Order_Produksi a where a.Status is null and a.Selesai is null and "
            SQL = SQL & "a.Flag_Release = 'Y' and a.Flag_Selesai_Hasil_Produksi is null and Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "a.No_Faktur = '" & no_fak & "' "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    If dr("Jumlah") = dr("Jml_Sdh_Split") Then
                        dr.Close()

                        SQL = "update EMI_Order_Produksi set Flag_Selesai_Hasil_Produksi = 'Y' where "
                        SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & no_fak & "' "
                        ExecuteTrans(SQL)
                    End If
                End If
            End Using

            Cmd.Transaction.Commit()
            CloseConn()
            MessageBox.Show(Base_Language.Lang_Global_Sukses_Simpan, Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        EMI_Display_Selesai_Produksi.Btn_Cari_Click(Btn_Refresh, e)
        Me.Close()
    End Sub

    Private Sub Transaksi_Produksi_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Public Sub TextBox4_Leave(sender As Object, e As EventArgs) Handles TextBox4.Leave
        Try
            OpenConn()

            SQL = "select a.No_Transaksi,a.No_PO,a.Lokasi,a.Tanggal,a.Jam,a.UserID,a.Kode_Stock_Owner,a.No_PO, "
            SQL = SQL & "a.Kode_Barang,b.Nama,a.Jumlah,a.Satuan,a.Catatan,c.Id_Routing,d.Keterangan as ket_routing,a.No_Batch, "
            SQL = SQL & "a.Operator,e.Nama as nm_operator "
            SQL = SQL & "from Emi_Split_Production_Order a,Barang b,EMI_Order_Produksi c,EMI_Master_Routing d,"
            SQL = SQL & "Emi_Karyawan e,Emi_Jabatan_Internal f "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Stock_Owner = b.Kode_Stock_Owner "
            SQL = SQL & "and a.Kode_Barang = b.Kode_Barang and a.Kode_Perusahaan = c.Kode_Perusahaan and a.No_PO = c.No_Faktur "
            SQL = SQL & "and c.Kode_Perusahaan = d.Kode_Perusahaan and c.Id_Routing = d.Id_Routing and c.Status is null "
            SQL = SQL & "and c.Flag_Release = 'Y' and a.Flag_Produksi = 'Y' and a.Flag_Selesai_Produksi is null and c.Selesai is null  "
            SQL = SQL & "and e.Id_Jabatan = f.Id_Jabatan and f.Flag_Tampil_Produksi = 'Y' and a.Operator = e.Id_Karyawan "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Transaksi = '" & TextBox4.Text & "'"
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    CmbFormulator_LokasiInquiry.Text = dr("Lokasi")
                    kd_so = dr("Kode_Stock_Owner")
                    TextBox2.Text = dr("Kode_Barang")
                    TextBox5.Text = dr("Nama")
                    TextBox6.Text = dr("Jumlah")
                    ComboBox1.Text = dr("ket_routing")
                    no_fak = dr("NO_PO")
                    TextBox3.Text = dr("No_Batch")
                    ComboBox3.Text = dr("nm_operator")
                    DateTimePicker1.Focus()
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub DateTimePicker1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles DateTimePicker1.KeyPress
        If e.KeyChar = Chr(13) Then DateTimePicker2.Focus()
    End Sub

    Private Sub DateTimePicker2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles DateTimePicker2.KeyPress
        If e.KeyChar = Chr(13) Then TextBox3.Focus()
    End Sub

    Private Sub TextBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox3.KeyPress
        If e.KeyChar = Chr(13) Then ComboBox3.Focus()
    End Sub
End Class
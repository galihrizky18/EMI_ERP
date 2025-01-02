Imports System.Reflection
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar


Public Class EMI_Compare_HPP
    Dim arrcari, arrId_Karyawan As New ArrayList
    Dim Jenis = "Compare_HPP"
    Public no_ro As String
    Dim dtp As DateTimePicker = New DateTimePicker()
    Dim rect As Rectangle
    Dim dtp1 As DateTimePicker = New DateTimePicker()
    Dim rect1 As Rectangle
    Public Tanda_SN As String = "#"

    Dim LvNo_Inquiry As String
    Dim LvKd_Cust As String
    Dim LvNm_Cust As String
    Dim LvLokasi As String
    Dim LvKd_Brg As String
    Dim LvNm_Brg As String
    Dim LvHPP_Real As String
    Dim LvHPP_Simulasi As String
    Dim LvSelisih_HPP As String

    Dim CellNo_Inquiry As Integer = 0
    Dim CellKd_Cust As Integer = 1
    Dim CellNm_Cust As Integer = 2
    Dim CellLokasi As Integer = 3
    Dim CellKd_Brg As Integer = 4
    Dim CellNm_Brg As Integer = 5
    Dim CellHPP_Real As Integer = 6
    Dim CellHPP_Simulasi As Integer = 7
    Dim CellSelisih_HPP As Integer = 8

    Private Sub get_isi_listview(index)
        LvNo_Inquiry = DataGridView1.Rows(index).Cells(CellNo_Inquiry).Value
        LvKd_Cust = DataGridView1.Rows(index).Cells(CellKd_Cust).Value
        LvNm_Cust = DataGridView1.Rows(index).Cells(CellNm_Cust).Value
        LvLokasi = DataGridView1.Rows(index).Cells(CellLokasi).Value
        LvKd_Brg = DataGridView1.Rows(index).Cells(CellKd_Brg).Value
        LvNm_Brg = DataGridView1.Rows(index).Cells(CellNm_Brg).Value
        LvHPP_Real = DataGridView1.Rows(index).Cells(CellHPP_Real).Value
        LvHPP_Simulasi = DataGridView1.Rows(index).Cells(CellHPP_Simulasi).Value
        LvSelisih_HPP = DataGridView1.Rows(index).Cells(CellSelisih_HPP).Value
    End Sub
    Private Sub EMI_Compare_HPP_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        Try
            OpenConn()

            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)

            Label1.Text = Base_Language.Lang_Compare_HPP_Judul
            Label6.Text = Base_Language.Lang_Global_No_Transaksi
            Label7.Text = Base_Language.Lang_Global_Tanggal
            Label8.Text = Base_Language.lang_global_keterangan
            Label18.Text = Base_Language.Lang_Global_No_Produksi
            Btn_Refresh.Text = Base_Language.Lang_Global_Simpan

            DataGridView1.Columns(0).HeaderText = "No Inquiry"
            DataGridView1.Columns(1).HeaderText = Base_Language.Lang_Global_KodeCustomer
            DataGridView1.Columns(2).HeaderText = Base_Language.Lang_Global_NamaCustomer
            DataGridView1.Columns(3).HeaderText = Base_Language.Lang_Global_Lokasi
            DataGridView1.Columns(4).HeaderText = Base_Language.Lang_Global_KodeBarang
            DataGridView1.Columns(5).HeaderText = Base_Language.Lang_Global_NamaBarang
            DataGridView1.Columns(6).HeaderText = Base_Language.Lang_Compare_HPP_Real_HPP
            DataGridView1.Columns(7).HeaderText = Base_Language.Lang_Compare_HPP_Simulasi_HPP
            DataGridView1.Columns(8).HeaderText = Base_Language.Lang_Compare_HPP_Selisih_HPP

            get_no_faktur()

            SQL = "select a.No_Faktur,a.Tanggal from Emi_Hasil_Produksi a,Emi_Produksi b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and "
            SQL = SQL & "a.No_Produksi = b.No_Faktur and "
            SQL = SQL & "a.Status is null and b.Status is null and "
            SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "b.No_Rencana_Produksi = '" & no_ro & "'"
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    TextBox4.Text = dr("No_Faktur")
                    DateTimePicker1.Value = Format(dr("Tanggal"), "dd MMMM yyyy")
                End If
            End Using

            TextBox1.Text = ""
            TextBox1.Focus()

            DataGridView1.Rows.Clear()
            SQL = "select f.No_Inquiry,b.Kode_Customer,i.nama as cust,b.Kode_Stock_Owner,"
            SQL = SQL & "b.Kode_Barang,j.Nama as Nm_Barang,b.Harga,h.Biaya_Bahan_Tertinggi,"
            SQL = SQL & "h.Biaya_Packaging_Tertinggi from Emi_Hasil_Produksi a,"
            SQL = SQL & "Emi_Hasil_Produksi_Detail b,Emi_Produksi c,EMI_Rencana_Produksi d,"
            SQL = SQL & "EMI_Rencana_Produksi_Detail e,View_PO_Detail f,EMI_HPP_Simulasi g,"
            SQL = SQL & "EMI_HPP_Simulasi_Detail h,Customers i,Barang j where "
            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and a.Status is null and a.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and a.No_Produksi = c.No_Faktur and c.Kode_Perusahaan = d.Kode_Perusahaan "
            SQL = SQL & "and c.No_Rencana_Produksi = d.No_Faktur and d.Kode_Perusahaan = e.Kode_Perusahaan "
            SQL = SQL & "and d.No_Faktur = e.No_Faktur and e.Kode_Perusahaan = f.KODE_PERUSAHAAN "
            SQL = SQL & "and e.No_PO = f.NO_FAKTUR and e.Kode_Stock_Owner = f.KODE_STOCK_OWNER "
            SQL = SQL & "and e.Kode_Barang = f.KODE_BARANG and f.KODE_PERUSAHAAN = g.Kode_Perusahaan "
            SQL = SQL & "and f.No_Inquiry = g.No_Inquiry and g.Kode_Perusahaan = h.Kode_Perusahaan "
            SQL = SQL & "and g.No_Faktur = h.No_Faktur and c.Status is null and d.Status is null "
            SQL = SQL & "and b.Kode_Perusahaan = i.Kode_Perusahaan and b.Kode_Customer = i.Kode_Customer "
            SQL = SQL & "and b.Kode_Perusahaan = j.Kode_Perusahaan and b.Kode_Stock_Owner  =j.Kode_Stock_Owner "
            SQL = SQL & "and b.Kode_Barang = j.Kode_Barang and c.No_Rencana_Produksi = '" & no_ro & "'"
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1
                            DataGridView1.Rows.Add(1)
                            DataGridView1.Rows.Item(i).Cells(0).Value = .Rows(i).Item("No_Inquiry")
                            DataGridView1.Rows.Item(i).Cells(1).Value = .Rows(i).Item("Kode_Customer")
                            DataGridView1.Rows.Item(i).Cells(2).Value = .Rows(i).Item("cust")
                            DataGridView1.Rows.Item(i).Cells(3).Value = .Rows(i).Item("Kode_Stock_Owner")
                            DataGridView1.Rows.Item(i).Cells(4).Value = .Rows(i).Item("Kode_Barang")
                            DataGridView1.Rows.Item(i).Cells(5).Value = .Rows(i).Item("Nm_Barang")
                            DataGridView1.Rows.Item(i).Cells(6).Value = Format(.Rows(i).Item("Harga"), "N0")
                            DataGridView1.Rows.Item(i).Cells(7).Value = Format((.Rows(i).Item("Biaya_Bahan_Tertinggi") + .Rows(i).Item("Biaya_Packaging_Tertinggi")), "N0")
                            DataGridView1.Rows.Item(i).Cells(8).Value = Format((.Rows(i).Item("Harga") - (.Rows(i).Item("Biaya_Bahan_Tertinggi") + .Rows(i).Item("Biaya_Packaging_Tertinggi"))), "N0")
                        Next
                    End If
                End With
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub get_no_faktur()
        TxtFormulator_NoFaktur.Text = fCHPP & Format(tgl_skg, "MMyy") & "-" &
                             General_Class.Get_Last_Number2("Emi_Compare_HPP", "no_faktur", 5,
                             "Kode_perusahaan", KodePerusahaan,
                             "And", "substring(no_faktur, 1, " & Len(fCHPP) + 4 & ")", fCHPP & Format(tgl_skg, "MMyy"))
    End Sub

    Private Sub EMI_Compare_HPP_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        If TxtFormulator_NoFaktur.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Global_Error_No_Transaksi, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtFormulator_NoFaktur.Focus() : Exit Sub
        ElseIf TextBox4.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Compare_HPP_Error_Ket, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox4.Focus() : Exit Sub
        End If

        get_jam()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            SQL = "select selesai from EMI_Rencana_Produksi where "
            SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "No_Faktur = '" & no_ro & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    If General_Class.CekNULL(Dr("selesai")) = "" Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show(Base_Language.Lang_Compare_HPP_Error & "!")
                        Exit Sub
                    End If
                End If
            End Using

            SQL = "INSERT INTO Emi_Compare_HPP (Kode_Perusahaan,No_Faktur,No_Hasil_Produksi,"
            SQL = SQL & "Tanggal,Jam,UserID,Keterangan) VALUES('" & KodePerusahaan & "',"
            SQL = SQL & "'" & TxtFormulator_NoFaktur.Text & "','" & TextBox4.Text & "',"
            SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "',"
            SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "','" & UserID & "',"
            SQL = SQL & "'" & TextBox1.Text & "')"
            ExecuteTrans(SQL)

            For i As Integer = 0 To DataGridView1.RowCount - 1
                get_isi_listview(i)
                SQL = "INSERT INTO Emi_Compare_HPP_Detail (Kode_Perusahaan,No_Faktur,"
                SQL = SQL & "No_Inquiry,Kode_Customer,Kode_Stock_Owner,Kode_Barang,"
                SQL = SQL & "HPP_Real,HPP_Simulasi,Selisih_HPP) VALUES("
                SQL = SQL & "'" & KodePerusahaan & "','" & TxtFormulator_NoFaktur.Text & "',"
                SQL = SQL & "'" & LvNo_Inquiry & "','" & LvKd_Cust & "','" & LvLokasi & "',"
                SQL = SQL & "'" & LvKd_Brg & "','" & HilangkanTanda(LvHPP_Real) & "',"
                SQL = SQL & "'" & HilangkanTanda(LvHPP_Simulasi) & "',"
                SQL = SQL & "'" & HilangkanTanda(LvSelisih_HPP) & "')"
                ExecuteTrans(SQL)
            Next

            SQL = "update Emi_Hasil_Produksi set Flag_Compare_HPP = 'Y' where "
            SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "No_Faktur = '" & TextBox4.Text & "'"
            ExecuteTrans(SQL)

            SQL = "update EMI_Rencana_Produksi set Flag_Compare_HPP = 'Y' where "
            SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "No_Faktur = '" & no_ro & "'"
            ExecuteTrans(SQL)

            Cmd.Transaction.Commit()
            CloseConn()
            MessageBox.Show(Base_Language.Lang_Global_Sukses_Simpan, Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        'EMI_Produksi_Display.Button1_Click(Btn_Refresh, e)
        Me.Close()
    End Sub

End Class
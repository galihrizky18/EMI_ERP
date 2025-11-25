Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button

Public Class N_EMI_SD_Keep_Stock_Barang_Lain

    Private Sub get_no_faktur()
        Dim fKeep_stock As String = "KP"
        TextBox1.Text = fKeep_stock & Format(tgl_skg, "MMyy") & "-" &
                             General_Class.Get_Last_Number2("N_EMI_Keep_Stock_Barang_Lain_Departement", "no_Faktur", 5,
                             "Kode_perusahaan", KodePerusahaan,
                             "And", "substring(no_Faktur, 1, " & Len(fKeep_stock) + 4 & ")", fKeep_stock & Format(tgl_skg, "MMyy"))
    End Sub
    Private Sub N_EMI_SD_Keep_Stock_Barang_Lain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        get_jam()

        Try
            OpenConn()

            get_no_faktur()

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub N_EMI_SD_Keep_Stock_Barang_Lain_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub TextBox5_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox5.KeyPress
        If e.KeyChar = Chr(13) Then BtnPilihBarang_Simpan.Focus()
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub BtnPilihBarang_Simpan_Click(sender As Object, e As EventArgs) Handles BtnPilihBarang_Simpan.Click
        If TextBox5.Text.Trim.Length = 0 Then
            MessageBox.Show("jumlah Belum diinput !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        get_jam()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            SQL = "select a.No_Faktur,a.Kode_Stock_Owner, a.Kode_Barang, a.Nama_Barang, a.Jumlah, a.Jmlh_PR, a.Satuan, b.Keterangan as Cost_Center, a.No_Urut, d.Lokasi, a.Flag_Ajukan, "
            SQL = SQL & "isnull(( select c.Keterangan from N_EMI_Master_Gedung_Barang_Lain c where "
            SQL = SQL & "a.Kode_Perusahaan = c.Kode_Perusahaan and a.ID_Gedung = c.ID_Gedung ), NULL) as Gedung, a.Id_Cost_Center, a.Alasan_Tolak, "
            SQL = SQL & "isnull((select sum(e.Jumlah) from Barang_Lain_SN e where a.Kode_Perusahaan = e.Kode_Perusahaan and a.Kode_Stock_Owner = e.Kode_Stock_Owner "
            SQL = SQL & "and a.Kode_Barang = e.Kode_Barang),0) as stock, "
            SQL = SQL & "isnull((select sum(f.Jumlah) from N_EMI_Keep_Stock_Barang_Lain_Departement f where a.Kode_Perusahaan = f.Kode_Perusahaan and a.Kode_Stock_Owner = f.Kode_Stock_Owner "
            SQL = SQL & "and a.Kode_Barang = f.Kode_Barang and a.No_Urut = f.Urut_Departement and f.Flag_Selesai_Pengeluaran_Barang is null and f.Status is null),0) as Jumlah_Keep_Stock, "
            SQL = SQL & "isnull((select sum(f.Jumlah) from N_EMI_Keep_Stock_Barang_Lain_Departement f where a.Kode_Perusahaan = f.Kode_Perusahaan and "
            SQL = SQL & "a.Kode_Barang = f.Kode_Barang and a.Kode_Stock_Owner = f.Kode_Stock_Owner and f.Flag_Selesai_Pengeluaran_Barang is null and f.Status is null),0) as Jumlah_Keep_Stock_2 "
            SQL = SQL & "from N_EMI_Purchase_Requisition_Barang_Lain_Departement_Detail a, EMI_Master_Cost_Center b, N_EMI_Purchase_Requisition_Barang_Lain_Departement d "
            SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' and d.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Cost_Center = b.Id_Cost_Center "
            SQL = SQL & "and a.Flag_Sudah_PR is null and a.Kode_Barang <> '-' and d.Flag_Release = 'Y' and d.Flag_PR is null "
            SQL = SQL & "and a.Kode_Perusahaan = d.Kode_Perusahaan and a.No_Faktur = d.No_Faktur and a.No_urut = '" & TextBox6.Text & "' "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    If dr("Jumlah") - dr("Jmlh_PR") - dr("Jumlah_Keep_Stock") < TextBox5.Text Then
                        dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("jumlah yang diinput lebih besar dari jumlah PR !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    ElseIf dr("stock") - dr("Jumlah_Keep_Stock_2") < TextBox5.Text Then
                        dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("jumlah yang diinput lebih besar dari jumlah Stock !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                Else
                    dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data tidak ditemuakan !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            get_no_faktur()

            SQL = "insert into N_EMI_Keep_Stock_Barang_Lain_Departement"
            SQL = SQL & "(Kode_Perusahaan, No_Faktur, Urut_Departement, Kode_Stock_Owner, Kode_Barang, Jumlah, Satuan, Tanggal, Jam) values"
            SQL = SQL & "('" & KodePerusahaan & "', '" & TextBox1.Text & "', '" & TextBox6.Text & "', '" & TextBox8.Text & "', "
            SQL = SQL & "'" & TextBox2.Text & "', '" & HilangkanTanda(TextBox5.Text) & "', '" & TextBox7.Text & "', "
            SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "')"
            ExecuteTrans(SQL)

            Cmd.Transaction.Commit()
            CloseConn()
            'MessageBox.Show(Base_Language.Lang_Global_Sukses_Simpan, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        N_EMI_Display_Request_Departement_Barang_Lain.BtnCari_Click(BtnPilihBarang_Simpan, e)
        Me.Close()
    End Sub
End Class
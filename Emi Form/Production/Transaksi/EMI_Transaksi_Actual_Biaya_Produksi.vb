Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button


Public Class EMI_Transaksi_Actual_Biaya_Produksi
    Dim arrcari As New ArrayList
    Dim Jenis = "Binding_Barcode"
    Public urut As Integer
    Dim Kd_Brg_Sampel As String

    Dim arrJenisBiaya, arrSatuan, arrMesin As New ArrayList


    Private Sub Transaksi_Binding_Barcode_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        get_jam()
        Try
            OpenConn()

            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)


            Label1.Text = "Transaksi - Actual Biaya Produksi"
            Label2.Text = Base_Language.Lang_Global_No_Transaksi
            Label4.Text = Base_Language.Lang_Global_Jumlah
            Label5.Text = Base_Language.Lang_Global_Satuan


            get_no_faktur()


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        kosong()
    End Sub


    Private Sub get_no_faktur()
        TxtFaktur.Text = fab & Format(tgl_skg, "MMyy") & "-" &
                             General_Class.Get_Last_Number2("emi_actual_biaya_produksi", "no_faktur", 5,
                             "Kode_perusahaan", KodePerusahaan,
                             "And", "substring(no_faktur, 1, " & Len(fab) + 4 & ")", fab & Format(tgl_skg, "MMyy"))
    End Sub


    Private Sub kosong()

        get_jam()

        Try

            OpenConn()

            get_no_faktur()

            DtpTanggal.ResetText()
            txtJumlah.Text = ""
            txtSatuan.Text = ""

            cmbJenisBiaya.Items.Clear() : arrJenisBiaya.Clear() : arrSatuan.Clear()
            SQL = "select id_jenis_biaya_produksi,keterangan,satuan from Emi_Jenis_Biaya_Produksi where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "'  "
            SQL = SQL & "order by keterangan "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    cmbJenisBiaya.Items.Add(Dr("keterangan")) : arrJenisBiaya.Add(Dr("id_jenis_biaya_produksi"))
                    arrSatuan.Add(Dr("satuan"))
                Loop
            End Using

            cmbMesin.Items.Clear() : arrMesin.Clear()
            SQL = "select id_work_center,keterangan from EMI_Master_Work_Center where  "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "'  "
            SQL = SQL & "order by keterangan "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    cmbMesin.Items.Add(Dr("Keterangan")) : arrMesin.Add(Dr("Id_Work_Center"))
                Loop
            End Using


            cmbStockOwner.Items.Clear()
            cmbStockOwner.Items.Add("-- Seluruh --")
            xSplit = CekKotaRole().Split(",")

            SQL = "Select kode_stock_owner From "
            SQL = SQL & "stock_owner where kode_perusahaan = '" & KodePerusahaan & "' and kode_kota in("
            For i As Integer = 0 To xSplit.Count - 1
                SQL = SQL & "'" & xSplit(i).Trim & "', "
            Next
            SQL = Strings.Left(SQL, Len(SQL) - 2)

            SQL = SQL & ") "
            SQL = SQL & "order by kode_stock_owner"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    cmbStockOwner.Items.Add(dr("kode_stock_owner"))
                Loop
            End Using
            cmbStockOwner.Text = Lokasi


            CloseConn()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try




    End Sub

    Private Sub cmbJenisBiaya_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbJenisBiaya.KeyPress
        If e.KeyChar = Chr(13) Then
            cmbMesin.Focus()
        End If
    End Sub

    Private Sub cmbMesin_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbMesin.KeyPress
        If e.KeyChar = Chr(13) Then
            txtJumlah.Focus()
        End If
    End Sub

    Private Sub cmbJenisBiaya_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbJenisBiaya.SelectedIndexChanged
        txtSatuan.Text = arrSatuan.Item(cmbJenisBiaya.SelectedIndex)
    End Sub

    Private Sub btnKosong_Click(sender As Object, e As EventArgs) Handles btnKosong.Click
        kosong()
    End Sub

    Private Sub txtJumlah_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtJumlah.KeyPress
        If e.KeyChar = Chr(13) Then
            Btn_Simpan.Focus()
        End If
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
    End Sub



    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click


        If cmbJenisBiaya.Text.Trim.Length = 0 Then
            MessageBox.Show("Jenis Biaya harus di isi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        ElseIf cmbMesin.Text.Trim.Length = 0 Then
            MessageBox.Show("Work Center harus di isi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        ElseIf txtSatuan.Text.Trim.Length = 0 Then
            MessageBox.Show("Satuan harus di isi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        ElseIf txtJumlah.Text.Trim.Length = 0 Then
            MessageBox.Show("Work Center harus di isi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        get_jam()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            SQL = "insert into emi_actual_biaya_produksi(kode_perusahaan,no_faktur,tanggal,jam,iduser,id_jenis_biaya,id_work_center,jumlah,satuan,lokasi) values ( "
            SQL = SQL & "'" & KodePerusahaan & "', '" & TxtFaktur.Text.Trim & "',"
            SQL = SQL & " '" & Format(DtpTanggal.Value, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "' ,"
            SQL = SQL & "'" & UserID & "', '" & arrJenisBiaya.Item(cmbJenisBiaya.SelectedIndex) & "',   "
            SQL = SQL & "'" & arrMesin.Item(cmbMesin.SelectedIndex) & "',"
            SQL = SQL & " '" & txtJumlah.Text.Trim & "',  '" & txtSatuan.Text.Trim & "',"
            SQL = SQL & "'" & cmbStockOwner.Text & "'"
            SQL = SQL & ")"
            ExecuteTrans(SQL)



            Cmd.Transaction.Commit()
            MessageBox.Show("Data berhasil disimpan ", Judul, MessageBoxButtons.OK)
            CloseConn()
            kosong()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


    End Sub
End Class
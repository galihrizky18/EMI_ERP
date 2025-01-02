Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button


Public Class Transaksi_Binding_Barcode
    Dim arrcari As New ArrayList
    Dim Jenis = "Binding_Barcode"
    Public urut As Integer
    Dim Kd_Brg_Sampel As String
    Private Sub Transaksi_Binding_Barcode_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        get_jam()
        Try
            OpenConn()

            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)

            Btn_Simpan.Text = Base_Language.Lang_Global_Simpan
            Label1.Text = Base_Language.Lang_Binding_Barcode_Judul
            Label2.Text = Base_Language.Lang_Global_No_Transaksi
            Label4.Text = Base_Language.Lang_Global_KodeBarang
            Label5.Text = Base_Language.Lang_Global_NamaBarang
            CheckBox1.Text = Base_Language.Lang_Binding_Barcode_Sampel
            Label7.Text = Base_Language.Lang_Binding_Barcode_Kd_Barcode

            get_no_faktur()
            CheckBox1.Checked = False
            TextBox4.Enabled = True
            TextBox4.Text = ""
            TextBox4.Focus()

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        Try
            OpenConn()
            If CheckBox1.Checked = False Then
                TextBox4.Enabled = True
                TextBox4.Text = ""
                TextBox4.Focus()
            Else
                get_no_Sample()
                TextBox4.Enabled = False
                'TextBox4.Text = ""
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub
    Private Sub get_no_faktur()
        TxtPO_NoFaktur.Text = fbb & Format(tgl_skg, "MMyy") & "-" &
                             General_Class.Get_Last_Number2("Emi_Binding_Barcode", "no_transaksi", 5,
                             "Kode_perusahaan", KodePerusahaan,
                             "And", "substring(no_transaksi, 1, " & Len(fbb) + 4 & ")", fbb & Format(tgl_skg, "MMyy"))
    End Sub

    Private Sub get_no_Sample()
        TextBox4.Text = fbs & Format(tgl_skg, "MMyy") & "" &
                             General_Class.Get_Last_Number2("Emi_Binding_Barcode", "Kode_Barang_Baru", 5,
                             "Kode_perusahaan", KodePerusahaan,
                             "And", "substring(Kode_Barang_Baru, 1, " & Len(fbs) + 4 & ")", fbs & Format(tgl_skg, "MMyy"))
    End Sub

    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
        If TextBox4.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Global_Error_Kd_Brg, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox1.Focus() : Exit Sub
        End If

        get_jam()

        Try
            OpenConn()

            SQL = "select top(1)Id_Group_Jenis from EMI_Group_Jenis where "
            If CheckBox1.Checked = True Then
                SQL = SQL & "Flag_Sample = 'Y'"
            Else
                SQL = SQL & "Flag_Finished_Good = 'Y'"
            End If
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    Dim id_group As String = dr("Id_Group_Jenis")
                    dr.Close()

                    SQL = "update barang set Kode_Barang = '" & TextBox4.Text & "', "
                    SQL = SQL & "Flag_Barcode = 'Y',Id_Group_Jenis = '" & id_group & " '"
                    SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "Kode_Barang_Inq = '" & TextBox1.Text & "'"
                    ExecuteTrans(SQL)

                    SQL = "update barang_detail_Harga_Jual set Kode_Barang = '" & TextBox4.Text & "' "
                    SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "Kode_Barang = '" & TextBox1.Text & "'"
                    ExecuteTrans(SQL)
                Else
                    dr.Close()
                    CloseConn()
                    MessageBox.Show(Base_Language.Lang_Global_Data_Tdk_Ditemukan, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            SQL = "update Emi_Inquiry_Detail set Flag_Barcode = 'Y' "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "No_Urut = '" & urut & "'"
            ExecuteTrans(SQL)


            SQL = "INSERT INTO Emi_Binding_Barcode(Kode_Perusahaan,No_Transaksi,Kode_Barang_Lama,"
            SQL = SQL & "Kode_Barang_Baru,Tanggal,Jam,UserId) VALUES('" & KodePerusahaan & "',"
            SQL = SQL & "'" & TxtPO_NoFaktur.Text & "','" & TextBox1.Text & "',"
            SQL = SQL & "'" & TextBox4.Text & "','" & Format(tgl_skg, "yyyy-MM-dd") & "',"
            SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "','" & UserID & "')"
            ExecuteTrans(SQL)

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        EMI_Display_Barcode.BtnInquiry_Cari_Click(Btn_Simpan, e)
        Me.Close()
    End Sub
End Class
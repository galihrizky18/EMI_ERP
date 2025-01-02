Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button

Public Class EMI_Perencanaan_Produksi_SD_Antrian
    Private Sub SD_Barang_Masuk_Tanggal_Expire_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        kosong()
    End Sub

    Private Sub kosong()

        Try
            OpenConn()
            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            Base_Language.Get_Languages(Bahasa_Pilihan, "Rencana_Produksi")
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try




        Label1.Text = Base_Language.Lang_Rencana_Produksi_Judul_Sd
        Label2.Text = Base_Language.Lang_Global_NO

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Try
            OpenConn()

            Dim total As Integer = 0

            SQL = "select count(kode_perusahaan) as total_nomor from EMI_Produksi_Terpenuhi where kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "no_antrian  = '" & ComboBox1.Text & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    total = Dr("total_nomor") + 1
                End If
            End Using



            SQL = "update EMI_Produksi_Terpenuhi set no_antrian = '" & ComboBox1.Text & "', indx = indx - " & Val(total) & " where  "
            SQL = SQL & "urut = '" & EMI_Perencanaan_Produksi.ListView1.FocusedItem.SubItems(11).Text & "' "
            ExecuteTrans(SQL)


            EMI_Perencanaan_Produksi.get_daftar_po()
            Me.Close()


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub
End Class
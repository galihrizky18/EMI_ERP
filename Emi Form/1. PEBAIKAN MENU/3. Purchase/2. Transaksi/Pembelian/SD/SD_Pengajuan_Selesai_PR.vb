Public Class SD_Pengajuan_Selesai_PR

    Public UrutPR

    Private Sub SD_Pengajuan_Selesai_PR_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub
    Private Sub EMI_Pengajuan_Selesai_PR_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        Txt_Keterangan.Text = ""
    End Sub


    Private Sub BtnPO_Simpan_Click(sender As Object, e As EventArgs) Handles BtnPO_Simpan.Click
        If Txt_NoPR.Text.Trim.Length = 0 Then
            MessageBox.Show("Data PR Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        If Txt_Keterangan.Text.Trim.Length = 0 Then
            MessageBox.Show("Keterangan Harus Diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_Keterangan.Focus() : Exit Sub
        End If

        get_jam()
        Try
            OpenConn()

            SQL = "update EMI_Purchase_Requisition_Detail set "
            SQL = SQL & "Flag_Pengajuan_Selesai = 'Y', Tgl_Pengajuan_Selesai = '" & Format(tgl_skg, "yyyy-MM-dd") & "', Jam_Pengajuan_Selesai = '" & Format(tgl_skg, "HH:mm:ss") & "', "
            SQL = SQL & "User_Pengajuan_Selesai = '" & UserID & "', Keterangan_Pengajuan = '" & Txt_Keterangan.Text.Trim & "' "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and No_Faktur = '" & Txt_NoPR.Text.Trim & "' "
            SQL = SQL & "and No_Urut= '" & UrutPR & "' "
            ExecuteTrans(SQL)

            CloseConn()
            MessageBox.Show("Data Berhasil Di Ajukan", "Pengajuan Penyelesaian PR", MessageBoxButtons.OK, MessageBoxIcon.Information)
            EMI_PO_Pembelian_Display_User.kosong()
            Me.Close()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try



    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub


End Class
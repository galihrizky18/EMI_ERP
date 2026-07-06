Imports System.Web.UI.WebControls
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button

Public Class N_EMI_SD_Alasan_Tolak_Kode_Barang_Baru
    Public xurut_departement As String
    Public xNoFakturPengajuanBarang As String
    Public xFrom As String
    Private Sub N_EMI_SD_Alasan_Tolak_Kode_Barang_Baru_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.Text = ""
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
        If TextBox1.Text.Trim.Length = 0 Then
            MessageBox.Show("alasan tolak harus diisi...... ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox1.Focus() : Exit Sub
        End If
        get_jam()

        Try
            OpenConn()


            If xFrom = "departement" Then

                SQL = "update N_EMI_Purchase_Requisition_Barang_Lain_Departement_Detail set "
                SQL = SQL & "Alasan_Tolak = '" & TextBox1.Text.Trim & "', "
                SQL = SQL & "Flag_Ajukan = 'T' "
                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and no_Urut = '" & xurut_departement & "' "
                ExecuteTrans(SQL)

                If xNoFakturPengajuanBarang <> "-" Then
                    SQL = "update N_EMI_Pengajuan_Barang_Baru_Lain set  "
                    SQL = SQL & "Flag_Pengajuan_Barang_Baru = 'T', "
                    SQL = SQL & "Tanggal_Approve = '" & Format(tgl_skg, "yyyy-MM-dd") & "', "
                    SQL = SQL & "jam_approve = '" & Format(tgl_skg, "HH:mm:ss") & "' ,"
                    SQL = SQL & "userid_approve = '" & UserID & "' ,"
                    SQL = SQL & "keterangan_tolak = '" & TextBox1.Text.Trim & "' "
                    SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "and no_faktur = '" & xNoFakturPengajuanBarang & "' "
                    ExecuteTrans(SQL)
                End If
            ElseIf xFrom = "warehouse" Then
                SQL = "update N_EMI_Pengajuan_Barang_Baru_Lain set  "
                SQL = SQL & "Flag_Pengajuan_Barang_Baru = 'T', "
                SQL = SQL & "Tanggal_Approve = '" & Format(tgl_skg, "yyyy-MM-dd") & "', "
                SQL = SQL & "jam_approve = '" & Format(tgl_skg, "HH:mm:ss") & "' ,"
                SQL = SQL & "userid_approve = '" & UserID & "' ,"
                SQL = SQL & "keterangan_tolak = '" & TextBox1.Text.Trim & "' "
                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and no_faktur = '" & xNoFakturPengajuanBarang & "' "
                ExecuteTrans(SQL)
            End If



            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Master_Barang_Lain.Cari2("Y")
        Me.Close()
    End Sub
End Class
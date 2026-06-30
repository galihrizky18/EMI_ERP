Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button

Public Class N_EMI_SD_Edit_Jumlah_Production_Plan

    Public mustUpdate As Boolean = False
    Public kodeBarang As String
    Public namaBarang As String
    Public jumlahSekarang As Double
    Public bulanTahunSkrng As String
    Public no_urut As Integer
    Public noRv As String
    Public jumlahSudahTerpakai As Double
    Private Sub N_EMI_SD_Edit_Jumlah_Production_Plan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtJmlhInginDiubah.Text = ""
        mustUpdate = False
        txtKdBrng.Text = kodeBarang
        txtNamaBarang.Text = namaBarang
        txtJumlahSkrng.Text = Format(jumlahSekarang, "N2")
        TxtTanggal.Text = bulanTahunSkrng
        txt_NoUrut.Text = no_urut
        txtNoRv.Text = noRv
        txtJumlahTerpakai.Text = Format(jumlahSudahTerpakai, "N2")
    End Sub

    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
        Try
            OpenConn()

            Cmd.Transaction = Cn.BeginTransaction


            If CekButtonRole("Production_Plan_Schedule_PPIC") = "T" Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("anda tidak memiliki akses ! !")
                Exit Sub
            End If

            'cek rv 1
            SQL = "select cast(rv as bigint) as rvx, urut "
            SQL = SQL & "from EMI_Transaksi_Sales_Forecasting_Detail "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and  urut = '" & txt_NoUrut.Text & "' "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    If dr("rvx") <> txtNoRv.Text Then
                        dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data ini sudah diubah sebelumnya! silahkan refresh dan coba lagi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End If
            End Using

            '========================================================================================
            '============ nilai update tidak boleh melebihi jumlah yang sudah diinput ===============
            '========================================================================================

            SQL = "select a.No_Faktur,a.Flag_Validasi_PPIC,urut, a.Nilai_PPIC,a.nilai_sales,a.urut,b.bulan,b.tahun, "
            SQL = SQL & "isnull((select sum(y.Jumlah) From N_EMI_Production_Plan_Schedule x, "
            SQL = SQL & "N_EMI_Production_Plan_Schedule_Detail y where x.Kode_Perusahaan = y.Kode_Perusahaan "
            SQL = SQL & "and x.No_Transaksi = y.No_Transaksi and x.Status is null  "
            SQL = SQL & "and y.Kode_Perusahaan = a.Kode_Perusahaan and y.Urut_Production_Plan = a.urut "
            SQL = SQL & "),0) as Nilai_Sdh_Input "

            SQL = SQL & "From EMI_Transaksi_Sales_Forecasting_Detail a, EMI_Transaksi_Sales_Forecasting b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and b.Status is null  and a.urut = " & txt_NoUrut.Text & " "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            Dim nilai_sudah_diinput As Double = .Rows(i).Item("Nilai_Sdh_Input")
                            Dim nilai_asli As Double = .Rows(i).Item("nilai_ppic")

                            Dim bulann As String = .Rows(i).Item("bulan")
                            Dim tahunn As String = .Rows(i).Item("tahun")


                            Dim nilaiAsli As Double = Val(nilai_asli)
                            Dim nilaiTerinput As Double = Val(nilai_sudah_diinput)

                            Dim inginDiubah As Double = Val(txtJmlhInginDiubah.Text.Trim)

                            ' Cek apakah inputan baru melebihi batas sisa
                            If inginDiubah < nilaiTerinput Then
                                CloseTrans()
                                CloseConn()

                                ' Pesan yang jelas buat user
                                Dim pesan As String = "Gagal Update!" & vbCrLf &
                                  "Maksimal jumlah yang bisa diinput adalah: " & Format(nilaiTerinput, "N2") & vbCrLf &
                                  "Karena nilai asli (" & Format(nilaiAsli, "N0") & ") sudah terpakai (" & Format(nilaiTerinput, "N0") & ")."

                                MessageBox.Show(pesan, "Batas Maksimal Terlampaui", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                Exit Sub
                            End If


                            SQL = "INSERT INTO EMI_Transaksi_Sales_Forecasting_Log(Kode_Perusahaan,No_Faktur,Urut_Detail,Jumlah_Lama_PPIC,Jumlah_Lama_Sales,Jenis,UserID,"
                            SQL = SQL & "Tanggal,Jam,Jumlah_Semi_FG) VALUES('" & KodePerusahaan & "','" & .Rows(i).Item("no_faktur") & "','" & txt_NoUrut.Text & "','" & .Rows(i).Item("nilai_ppic") & "'"
                            SQL = SQL & ",'" & .Rows(i).Item("nilai_sales") & "','Update Dari Schedule', "
                            SQL = SQL & "'" & UserID & "','" & Format(tgl_skg, "yyyy-MM-dd") & "','" & Format(tgl_skg, "HH:mm:ss") & "', '" & .Rows(i).Item("nilai_ppic") & "') "
                            ExecuteTrans(SQL)

                            SQL = "update EMI_Transaksi_Sales_Forecasting_Detail set nilai_ppic = '" & txtJmlhInginDiubah.Text.Trim & "', "
                            SQL = SQL & "Jumlah_Semi_FG =   '" & txtJmlhInginDiubah.Text.Trim & "' "
                            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and urut = '" & .Rows(i).Item("urut") & "' "
                            ExecuteTrans(SQL)


                            Dim listFilter As New List(Of String)

                            listFilter.Add("(a.bulan = " & bulann & " AND a.tahun = " & tahunn & ")")
                            Dim filterBulanTahun As String = String.Join(" OR ", listFilter)
                            RefreshForecastingSemiFG(
                                KodePerusahaan,
                               filterBulanTahun,
                               "Update Schedule",
                                UserID
                                )


                        Next

                    Else
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Terjadi kesalahan, data tidak ditemukan, atau terjadi perubahan pada barang! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End With
            End Using




            
            Cmd.Transaction.Commit()

            CloseConn()
            MessageBox.Show("Data berhasil disimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
            mustUpdate = True
            Me.Close()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub



    Private Sub txtJmlhInginDiubah_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtJmlhInginDiubah.KeyPress

        If e.KeyChar = Chr(13) Then
            Btn_Simpan.Focus()
        End If

        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(Asc("."))) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub
End Class
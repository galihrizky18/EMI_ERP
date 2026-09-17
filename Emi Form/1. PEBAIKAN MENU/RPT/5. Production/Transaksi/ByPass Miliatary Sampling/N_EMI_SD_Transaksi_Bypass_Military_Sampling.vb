Public Class N_EMI_SD_Transaksi_Bypass_Military_Sampling


    Private Sub N_EMI_SD_Transaksi_Bypass_Military_Sampling_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub
    Private Sub N_EMI_SD_Transaksi_Bypass_Military_Sampling_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        Kosong()
    End Sub

    Private Sub Kosong()
        Txt_Keterangan.Text = ""
    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        Kosong()
    End Sub

    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click

        get_jam()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            Dim JudulNotif As String = "Bypass Military Sampling"

            '====================
            '=     CEK ROLE     =
            '====================
            If CekButtonRole("Bypass_Military_Sampling") = "T" Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Anda Tidak Memiliki Akses Untuk Melakukan Bypass Military Sampling", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            If MessageBox.Show("Yakin Ingin Melakukan Bypass Split ini?", JudulNotif, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = vbNo Then
                CloseTrans()
                CloseConn()
                Exit Sub
            End If



            Dim No_PO As String = Txt_No_PO.Text
            Dim No_Split As String = Txt_No_Split.Text
            Dim Batch As String = Txt_Batch.Text
            Dim Jumlah_PO As Double = Txt_Jumlah_PO.Text
            Dim jumlah_GR As Double = Txt_Jumlah_GR.Text


            '====================================================
            '=     CEK APAKAH DATA SUDAH MELEWATI STEP GR 1     =
            '====================================================
            SQL = "select top 1 1 from Emi_Production_Results a, Emi_Production_Results_Detail_Pallet b, EMI_Production_Results_Detail_Barang c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
            SQL = SQL & "and b.No_Transaksi = c.No_Transaksi and b.Proses = c.Proses "
            SQL = SQL & "and b.No_Transaksi = c.No_Transaksi and b.Proses = c.Proses "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Production_Order = '" & No_Split & "' "
            SQL = SQL & "and b.Tahap = '" & Batch & "' "
            Using Dr = OpenTrans(SQL)
                If Not Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data Belum Selesai Dari GR 1", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '==================================================
            '=     CEK APAKAH DATA SUDAH TERIMA HASIL LAB     =
            '==================================================
            SQL = "select Flag_Ok from N_EMI_LAB_Hasil_Uji_Validasi_Final "
            SQL = SQL & "where No_Po = '" & No_PO & "' "
            SQL = SQL & "and No_Split_Po = '" & No_Split & "' "
            SQL = SQL & "and No_Batch = '" & Batch & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    If General_Class.CekNULL(Dr("Flag_Ok")) = "Y" Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data Sudah Menerima Hasil Uji Lab", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End If
            End Using


            '======================================================
            '=     CEK APAKAH DATA SUDAH DI BYPASS SEBELUMNYA     =
            '======================================================
            'SQL = "select 1 from N_EMI_Transaksi_Bypass_Military_Sampling "
            'SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            'SQL = SQL & "and No_PO = '" & No_PO & "' "
            'SQL = SQL & "and No_Split = '" & No_Split & "' "
            'SQL = SQL & "and No_Batch = '" & Batch & "' "
            'SQL = SQL & "and status is null "
            'Using Dr = OpenTrans(SQL)
            '    If Dr.Read Then
            '        Dr.Close()
            '        CloseTrans()
            '        CloseConn()
            '        MessageBox.Show("Data Sudah Dibypass Sebelumnya", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '        Exit Sub
            '    End If
            'End Using

            '==============================================
            '=     CEK APAKAH DATA SUDAH LEWAT 4 HARI     =
            '==============================================
            'SQL = "select 1 from N_EMI_LAB_PO_Sampel a, EMI_Master_Mesin b "
            'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            'SQL = SQL & "and a.Id_Mesin = b.Id_Master_Mesin "
            'SQL = SQL & "and b.Nama_Mesin = 'AUTOCLAVE' "
            'SQL = SQL & "and a.Status is null "
            'SQL = SQL & "and DATEADD(DAY, 4, a.Tanggal) <= CAST(GETDATE() AS DATE)"
            'SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            'SQL = SQL & "and a.no_po = '" & No_PO & "' "
            'SQL = SQL & "and a.No_Split_Po = '" & No_Split & "' "
            'SQL = SQL & "and a.No_Batch = '" & Batch & "' "
            'Using Dr = OpenTrans(SQL)
            '    If Not Dr.Read Then
            '        Dr.Close()
            '        CloseTrans()
            '        CloseConn()
            '        MessageBox.Show("Data Belum di Registerasi Lab atau Belum lewat dari H+4 Hari", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '        Exit Sub
            '    End If
            'End Using

            '==============================
            '=     INSERT DATA BYPASS     =
            '==============================

            Dim teksKeterangan As String = Txt_Keterangan.Text
            teksKeterangan = teksKeterangan.Replace(vbCrLf, " ")
            teksKeterangan = teksKeterangan.Replace(vbLf, " ")
            teksKeterangan = teksKeterangan.Trim()



            '================================
            '=     GET TAHAPAN TERAKHIR     =
            '================================
            Dim Tahap_Military_Sampling As String = ""
            SQL = "select isnull(( "
            SQL = SQL & "select top 1 z.Tahap_Military_Sampling from N_EMI_Military_Sampling z "
            SQL = SQL & "where z.Kode_Perusahaan = a.Kode_Perusahaan "
            SQL = SQL & "and z.No_Split = a.No_Production_Order "
            SQL = SQL & "and z.No_Batch = b.Tahap "
            SQL = SQL & "and z.No_GR = 1 "
            SQL = SQL & "and z.status is null "
            SQL = SQL & "order by z.Tahap_Military_Sampling DESC "
            SQL = SQL & ")+1, 1) as Tahap_Military_Sampling "
            SQL = SQL & "from Emi_Production_Results a, Emi_Production_Results_Detail_Pallet b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Production_Order = '" & No_Split & "' "
            SQL = SQL & "and b.Tahap = '" & Batch & "' "
            SQL = SQL & "and a.status is null "
            SQL = SQL & "group by a.Kode_Perusahaan, a.No_Production_Order, b.Tahap "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Tahap_Military_Sampling = Dr("Tahap_Military_Sampling")
                End If
            End Using


            SQL = "insert into N_EMI_Transaksi_Bypass_Military_Sampling (Kode_Perusahaan, No_PO, No_Split, No_Batch, Keterangan, Tanggal, Jam, UserID, Tahapan, Jumlah_PO, Jumlah_GR) "
            SQL = SQL & "values ('" & KodePerusahaan & "', '" & No_PO & "', '" & No_Split & "', '" & Batch & "', "
            SQL = SQL & "'" & teksKeterangan & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', '" & UserID & "', '" & Tahap_Military_Sampling & "', "
            SQL = SQL & "" & Jumlah_PO & ", " & jumlah_GR & " ) "
            ExecuteTrans(SQL)







            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
            MessageBox.Show("Bypass Military Sampling Berhasil Dilakukan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        N_EMI_Transaksi_Bypass_Military_Sampling.Kosong()
        Me.Close()
    End Sub


End Class
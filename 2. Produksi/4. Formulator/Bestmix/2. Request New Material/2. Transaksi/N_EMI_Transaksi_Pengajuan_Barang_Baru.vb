Public Class N_EMI_Transaksi_Pengajuan_Barang_Baru


    Dim arrGudangRawMaterial, arrIDGroupJenis, arrIDKlasifikasiBahan, arrIDKlasifikasiBahan2, arrPrefixKlasifikasiBahan, arrPrefixKlasifikasiBahan2 As New ArrayList



    Private Sub N_EMI_Transaksi_Pengajuan_Barang_Baru_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try
            OpenConn()

            Cmb_Group_Jenis.Items.Clear() : arrIDGroupJenis.Clear() : arrGudangRawMaterial.Clear()
            SQL = "select id_group_jenis,kode_group_jenis, Flag_Raw_Material from EMI_GROUP_JENIS where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "order by kode_group_jenis"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Cmb_Group_Jenis.Items.Add(dr("kode_group_jenis")) : arrIDGroupJenis.Add(dr("id_group_jenis"))

                    If dr("Flag_Raw_Material") = "Y" Then
                        arrGudangRawMaterial.Add(dr("id_group_jenis"))
                    End If
                Loop
            End Using

            Cmb_Klasifikasi_Bahan.Items.Clear() : arrIDKlasifikasiBahan.Clear() : arrPrefixKlasifikasiBahan.Clear()
            SQL = "select ID_Klasifikasi_Bahan,Keterangan, Prefix_Klasifikasi_Bahan from Emi_Klasifikasi_Bahan where kode_perusahaan = '" & KodePerusahaan & "' order by Keterangan"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Cmb_Klasifikasi_Bahan.Items.Add(Dr("Keterangan"))
                    arrIDKlasifikasiBahan.Add(Dr("ID_Klasifikasi_Bahan"))
                    arrPrefixKlasifikasiBahan.Add(Dr("Prefix_Klasifikasi_Bahan"))
                Loop
            End Using




            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Lv_Data.Columns.Clear()
        Lv_Data.Columns.Add("No Transaksi", 150, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Tanggal", 120, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Jam", 100, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Kode Barang", 150, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Nama Barang", 300, HorizontalAlignment.Left)
        Lv_Data.View = View.Details

        Kosong()
    End Sub

    Private Sub get_no_faktur()
        Dim FRequestNewMaterial As String = "RNM-"
        TxtNo_Transaksi.Text = FRequestNewMaterial & Format(tgl_skg, "MMyy") & "-" &
                            General_Class.Get_Last_Number2("N_EMI_Transaksi_Pengajuan_Barang_Baru", "No_Transaksi", 5,
                            "Kode_perusahaan", KodePerusahaan,
                            "And", "substring(No_Transaksi, 1, " & Len(FRequestNewMaterial) + 4 & ")", FRequestNewMaterial & Format(tgl_skg, "MMyy"))

    End Sub

    Private Sub Kosong()

        Cmb_Group_Jenis.SelectedIndex = -1
        Cmb_Klasifikasi_Bahan.SelectedIndex = -1
        Cmb_Klasifikasi_Bahan_2.SelectedIndex = -1
        Cmb_Klasifikasi_Bahan.Enabled = False
        Cmb_Klasifikasi_Bahan_2.Enabled = False
        Txt_Kd_Barang.Text = String.Empty
        Txt_Nm_Barang.Text = String.Empty

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

        LoadData()

    End Sub

    Private Sub LoadData()

        Try
            OpenConn()

            Lv_Data.Items.Clear()
            SQL = "select No_Transaksi, Tanggal, Jam, Kode_Group_Jenis, Klasifikasi_Bahan, Klasifikasi_Bahan_2, "
            SQL &= $"kode_barang, Nama_Barang, Flag_Validasi_Procurement "
            SQL &= $"from N_EMI_Transaksi_Pengajuan_Barang_Baru "
            SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
            SQL &= $"and Status is null "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Data.Items.Add(Dr("No_Transaksi"))
                    Lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
                    Lv.SubItems.Add(Dr("Jam"))
                    Lv.SubItems.Add(Dr("kode_barang"))
                    Lv.SubItems.Add(Dr("Nama_Barang"))

                    If General_Class.CekNULL(Dr("Flag_Validasi_Procurement")) = "" Then
                        Lv.BackColor = Color.Goldenrod
                        Lv.ForeColor = Color.Black
                    ElseIf General_Class.CekNULL(Dr("Flag_Validasi_Procurement")) = "Y" Then
                        Lv.BackColor = Color.LightGreen
                        Lv.ForeColor = Color.Black
                    ElseIf General_Class.CekNULL(Dr("Flag_Validasi_Procurement")) = "T" Then
                        Lv.BackColor = Color.DarkRed
                        Lv.ForeColor = Color.White
                    End If
                Loop
            End Using



            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
        If Cmb_Group_Jenis.SelectedIndex = -1 Then
            MessageBox.Show("Group Jenis Harus Dipilih Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Group_Jenis.DroppedDown = True
            Cmb_Group_Jenis.Focus()
            Exit Sub
        ElseIf Txt_Kd_Barang.Text.Trim.Length = 0 Then
            MessageBox.Show("Kode Barang Gagal Digenerate. Harap Ulangi Transaksi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        ElseIf Txt_Nm_Barang.Text.Trim.Length = 0 Then
            MessageBox.Show("Nama Barang Harus Diisis", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_Nm_Barang.Focus()
            Exit Sub
        End If

        If arrGudangRawMaterial.Contains(arrIDGroupJenis(Cmb_Group_Jenis.SelectedIndex)) Then
            If Cmb_Klasifikasi_Bahan.SelectedIndex = -1 Then
                MessageBox.Show("Klasifikasi Bahan Harus Dipilih Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Cmb_Klasifikasi_Bahan.DroppedDown = True
                Cmb_Klasifikasi_Bahan.Focus()
                Exit Sub
            ElseIf Cmb_Klasifikasi_Bahan_2.SelectedIndex = -1 Then
                MessageBox.Show("Klasifikasi Bahan 2 Harus Dipilih Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Cmb_Klasifikasi_Bahan_2.DroppedDown = True
                Cmb_Klasifikasi_Bahan_2.Focus()
                Exit Sub
            End If

        End If


        If MessageBox.Show("Yakin Ingin Melakukan Simpan Barang Ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = vbNo Then Exit Sub

        get_jam()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction


            get_no_faktur()
            '============================================
            '=     CEK APAKAH KODE BARANG SUDAH ADA     =
            '============================================
            SQL = "select top 1 1 from barang "
            SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' and Kode_Barang = '{Txt_Kd_Barang.Text.Trim}' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Terjadi Kesalahan. Kode Barang Sudah Ada Didatabase. Harap Ulangi Transaksi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            Dim IDKlasifikasiBahan As String = "NULL"
            Dim IDKlasifikasiBahan2 As String = "NULL"
            Dim KlasifikasiBahan As String = "NULL"
            Dim KlasifikasiBahan2 As String = "NULL"
            If arrGudangRawMaterial.Contains(arrIDGroupJenis(Cmb_Group_Jenis.SelectedIndex)) Then
                IDKlasifikasiBahan = $"'{arrIDKlasifikasiBahan(Cmb_Klasifikasi_Bahan.SelectedIndex)}'"
                IDKlasifikasiBahan2 = $"'{arrIDKlasifikasiBahan2(Cmb_Klasifikasi_Bahan_2.SelectedIndex)}'"
                KlasifikasiBahan = $"'{Cmb_Klasifikasi_Bahan.Text.Trim}'"
                KlasifikasiBahan2 = $"'{Cmb_Klasifikasi_Bahan_2.Text.Trim}'"
            End If


            '============================
            '=     INSERT TRANSAKSI     =
            '============================
            SQL = "insert into N_EMI_Transaksi_Pengajuan_Barang_Baru (Kode_Perusahaan, No_Transaksi, Tanggal, Jam, UserID, Id_Group_Jenis, Kode_Group_Jenis, ID_Klasifikasi_Bahan, Klasifikasi_Bahan, "
            SQL &= $"ID_Klasifikasi_Bahan_2, Klasifikasi_Bahan_2, Kode_Barang, Nama_Barang) "
            SQL &= $"values('{KodePerusahaan}', '{TxtNo_Transaksi.Text.Trim}', '{Format(tgl_skg, "yyyy-MM-dd")}', '{Format(tgl_skg, "HH:mm:ss")}', "
            SQL &= $"'{UserID}', '{arrIDGroupJenis(Cmb_Group_Jenis.SelectedIndex)}', '{Cmb_Group_Jenis.Text.Trim}', "
            SQL &= $"{IDKlasifikasiBahan}, {KlasifikasiBahan}, "
            SQL &= $"{IDKlasifikasiBahan2}, {KlasifikasiBahan2}, "
            SQL &= $"'{Txt_Kd_Barang.Text.Trim}', '{Txt_Nm_Barang.Text.Trim}') "
            ExecuteTrans(SQL)

            '=========================
            '=     INSERT BARANG     =
            '=========================
            SQL = "select kode_stock_owner from View_Lokasi_Stock where kode_perusahaan = '" & KodePerusahaan & "' and aktif = 'Y'  "
            SQL = SQL & "order by kode_stock_owner"
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            Dim KdSo As String = .Rows(i).Item("kode_stock_owner")

                            SQL = "insert into barang(Kode_Perusahaan, Id_Group_Jenis, Id_Klasifikasi_Bahan, Id_Klasifikasi_Bahan2, Flag_Request_Barang_Baru, Kode_Stock_Owner, Kode_Barang, Kode_Barang_Inq, Nama) "
                            SQL &= $"values('{KodePerusahaan}', {arrIDGroupJenis(Cmb_Group_Jenis.SelectedIndex)}, {IDKlasifikasiBahan}, "
                            SQL &= $"{IDKlasifikasiBahan2}, 'Y', '{KdSo}', '{Txt_Kd_Barang.Text.Trim}', "
                            SQL &= $"'{Txt_Kd_Barang.Text.Trim}', '{Txt_Nm_Barang.Text.Trim}')  "
                            ExecuteTrans(SQL)


                        Next
                    End If
                End With
            End Using





            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
            MessageBox.Show("Pengajuan Request Barang Baru Berhasil di Simpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Kosong()

    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        Kosong()
    End Sub

    Private Sub Cmb_Group_Jenis_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Group_Jenis.SelectedIndexChanged
        Cmb_Klasifikasi_Bahan.SelectedIndex = -1
        Cmb_Klasifikasi_Bahan_2.SelectedIndex = -1

        Txt_Kd_Barang.Text = String.Empty
        'If Cmb_Klasifikasi_Bahan.Enabled Then Cmb_Klasifikasi_Bahan.Enabled = False
        If Cmb_Klasifikasi_Bahan_2.Enabled Then Cmb_Klasifikasi_Bahan_2.Enabled = False
    End Sub



    Private Sub Cmb_Klasifikasi_Bahan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Klasifikasi_Bahan.SelectedIndexChanged
        Cmb_Klasifikasi_Bahan_2.SelectedIndex = -1
        Txt_Kd_Barang.Text = String.Empty
        'If Cmb_Klasifikasi_Bahan_2.Enabled Then Cmb_Klasifikasi_Bahan.Enabled = False
    End Sub



    Private Sub Cmb_Group_Jenis_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles Cmb_Group_Jenis.SelectionChangeCommitted
        If arrGudangRawMaterial.Contains(arrIDGroupJenis(Cmb_Group_Jenis.SelectedIndex)) Then
            Cmb_Klasifikasi_Bahan.Enabled = True
            Txt_Kd_Barang.Enabled = False
            'Exit Sub
        Else
            Cmb_Klasifikasi_Bahan.Enabled = False
            Txt_Kd_Barang.Enabled = True
        End If

        Txt_Kd_Barang.Text = ""
        Cmb_Klasifikasi_Bahan.SelectedIndex = -1

    End Sub



    Private Sub Cmb_Klasifikasi_Bahan_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles Cmb_Klasifikasi_Bahan.SelectionChangeCommitted
        If Cmb_Klasifikasi_Bahan.SelectedIndex = -1 Then
            Cmb_Klasifikasi_Bahan_2.Items.Clear()
            Cmb_Klasifikasi_Bahan_2.Enabled = False
            Exit Sub
        Else
            Cmb_Klasifikasi_Bahan_2.Enabled = True
            Try
                OpenConn()

                Cmb_Klasifikasi_Bahan_2.Items.Clear() : arrIDKlasifikasiBahan2.Clear() : arrPrefixKlasifikasiBahan2.Clear()
                SQL = "select Id_Klasifikasi_Bahan2, Keterangan, Prefix_Klasifikasi_Bahan from EMI_Klasifikasi_Bahan2 "
                SQL = SQL & "where kode_perusahaan='" & KodePerusahaan & "' and Id_Klasifikasi_Bahan1='" & arrIDKlasifikasiBahan(Cmb_Klasifikasi_Bahan.SelectedIndex) & "'"
                Using Dr = OpenTrans(SQL)
                    Do While Dr.Read
                        Cmb_Klasifikasi_Bahan_2.Items.Add(Dr("Keterangan"))
                        arrIDKlasifikasiBahan2.Add(Dr("Id_Klasifikasi_Bahan2"))
                        arrPrefixKlasifikasiBahan2.Add(Dr("Prefix_Klasifikasi_Bahan"))
                    Loop

                End Using

                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        End If

        Cmb_Klasifikasi_Bahan_2.SelectedIndex = -1
    End Sub


    Private Sub Cmb_Klasifikasi_Bahan_2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Klasifikasi_Bahan_2.SelectedIndexChanged
        Txt_Nm_Barang.Text = String.Empty
    End Sub

    Private Sub Cmb_Klasifikasi_Bahan_2_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles Cmb_Klasifikasi_Bahan_2.SelectionChangeCommitted
        If Cmb_Klasifikasi_Bahan.SelectedIndex = -1 Then
            Txt_Kd_Barang.Text = String.Empty
            Txt_Nm_Barang.Text = String.Empty
            Exit Sub
        Else
            Txt_Nm_Barang.Text = String.Empty

            Try
                OpenConn()

                Dim No_Urut As String
                SQL = "select top(1) substring(kode_barang, 5, 3) as no_urut from barang where kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "id_klasifikasi_bahan = '" & arrIDKlasifikasiBahan.Item(Cmb_Klasifikasi_Bahan.SelectedIndex) & "' and "
                SQL = SQL & "id_klasifikasi_bahan2 = '" & arrIDKlasifikasiBahan2.Item(Cmb_Klasifikasi_Bahan_2.SelectedIndex) & "' "
                SQL = SQL & "order by no_urut desc"
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        No_Urut = Format(Val(Dr("No_Urut")) + 1, "000")
                    Else
                        No_Urut = "001"
                    End If
                    Txt_Kd_Barang.Text = arrPrefixKlasifikasiBahan.Item(Cmb_Klasifikasi_Bahan.SelectedIndex) & arrPrefixKlasifikasiBahan2.Item(Cmb_Klasifikasi_Bahan_2.SelectedIndex) & No_Urut
                End Using

                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        End If
    End Sub



    '========================================================================================================================================================================
    '=     UTLITY
    '========================================================================================================================================================================

    Protected Overrides Sub WndProc(ByRef m As Message)
        ' WM_NCLBUTTONDBLCLK = 0xA3 (double click di title bar)
        If m.Msg = &HA3 Then
            Return  ' Abaikan pesan, sehingga form tidak maximize
        End If

        MyBase.WndProc(m)
    End Sub


End Class
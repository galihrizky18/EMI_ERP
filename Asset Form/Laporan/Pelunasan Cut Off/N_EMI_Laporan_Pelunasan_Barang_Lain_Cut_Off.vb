Public Class N_EMI_Laporan_Pelunasan_Barang_Lain_Cut_Off


    Dim Switch_Lv As Boolean = False

    Private Sub N_EMI_Laporan_Pelunasan_Cut_Off_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        Lv_Faktur.Columns.Clear()
        Lv_Faktur.Columns.Add("No Faktur", 130, HorizontalAlignment.Left)
        Lv_Faktur.Columns.Add("Tanggal", 110, HorizontalAlignment.Center)
        Lv_Faktur.Columns.Add("Jam", 110, HorizontalAlignment.Center)
        Lv_Faktur.Columns.Add("User", 130, HorizontalAlignment.Center)
        Lv_Faktur.View = View.Details

        Lv_PO.Columns.Clear()
        Lv_PO.Columns.Add("No Faktur", 130, HorizontalAlignment.Left)
        Lv_PO.Columns.Add("Keterangan", 200, HorizontalAlignment.Left)
        Lv_PO.Columns.Add("Tanggal", 110, HorizontalAlignment.Left)
        Lv_PO.View = View.Details

        LV_Perusahaan_Import.Columns.Clear()
        LV_Perusahaan_Import.Columns.Add("Kode Perusahaan Biaya Import", 140, HorizontalAlignment.Left)
        LV_Perusahaan_Import.Columns.Add("Keterangan", 200, HorizontalAlignment.Left)
        LV_Perusahaan_Import.View = View.Details


        Lv_Kategori.Columns.Clear()
        Lv_Kategori.Columns.Add("Kode Kategori Biaya Import", 140, HorizontalAlignment.Left)
        Lv_Kategori.Columns.Add("Keterangan", 200, HorizontalAlignment.Left)
        Lv_Kategori.View = View.Details

        Cmb_JenisLaporan.Items.Clear()
        Cmb_JenisLaporan.Items.Add("Laporan Hutang")
        Cmb_JenisLaporan.Items.Add("Laporan Hutang Rekap")
        Cmb_JenisLaporan.SelectedIndex = 0

        Kosong()

    End Sub


    Private Sub Kosong()

        Tgl1.Value = Now.Date : Tgl2.Value = Now.Date

        Switch_Lv = False
        Txt_Faktur.Text = OpsiSeluruh
        Txt_PO.Text = OpsiSeluruh : Txt_Ket_PO.Text = OpsiSeluruh
        Txt_Kd_Perusahaan_Import.Text = OpsiSeluruh : Txt_Nm_Perusahaan_Import.Text = OpsiSeluruh
        Txt_Kd_Kategori.Text = OpsiSeluruh : Txt_Nm_Kategori.Text = OpsiSeluruh
        Switch_Lv = True


        Lv_Faktur.Items.Clear() : Lv_PO.Items.Clear() : LV_Perusahaan_Import.Items.Clear() : Lv_Kategori.Items.Clear()



    End Sub

    Private Sub Txt_Faktur_TextChanged(sender As Object, e As EventArgs) Handles Txt_Faktur.TextChanged
        If Switch_Lv = False Then Exit Sub
        If Txt_Faktur.Text.Trim.Length = 0 Then
            Me.Size = New Size(733, 338)
            Lv_Faktur.Visible = False
            Lv_Faktur.Location = New Point(725, 153)
            Txt_Faktur.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(733, 405)
            Lv_Faktur.Location = New Point(174, 153)
            Lv_Faktur.Visible = True
        End If

        Try
            OpenConn()

            Lv_Faktur.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_Faktur.Items.Add("--- SELURUH ---")
            Lv.SubItems.Add("--- SELURUH ---")
            Lv.SubItems.Add("--- SELURUH ---")
            Lv.SubItems.Add("--- SELURUH ---")
            SQL = "select No_Faktur, Tanggal, Jam, UserID from EMI_Pembelian_Barang_Lain where Kode_Perusahaan = '" & KodePerusahaan & "' and Status is null and No_Faktur like '%" & Txt_Faktur.Text & "%' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lv = Lv_Faktur.Items.Add(Dr("No_Faktur"))

                    If General_Class.CekNULL(Dr("Tanggal")) = "" Then
                        Lv.SubItems.Add("-")
                    Else
                        Lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
                    End If

                    If General_Class.CekNULL(Dr("Jam")) = "" Then
                        Lv.SubItems.Add("-")
                    Else
                        Lv.SubItems.Add(Dr("Jam"))
                    End If
                    Lv.SubItems.Add(Dr("UserID"))

                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_Faktur_Leave(sender As Object, e As EventArgs) Handles Txt_Faktur.Leave
        If Txt_Faktur.Text.Trim.Length = 0 Then Exit Sub
        If Lv_Faktur.Focused = True Then Exit Sub

        Try
            OpenConn()

            If Not Txt_Faktur.Text = "--- SELURUH ---" Then

                SQL = "select No_Faktur, Tanggal, Jam, UserID from EMI_Pembelian_Barang_Lain where Kode_Perusahaan = '" & KodePerusahaan & "' and Status is null and No_Faktur = '" & Txt_Faktur.Text & "' "
                Using Dr = Open(SQL)
                    If Dr.Read Then
                        Txt_Faktur.Text = Dr("No_Faktur")
                        Txt_PO.Focus()
                    Else
                        MessageBox.Show("No Faktur tidak ditemukan . . ! !", Judul)
                        Txt_Faktur.Text = ""
                        Txt_Faktur.Focus()
                    End If

                    Me.Size = New Size(733, 338)
                    Lv_Faktur.Visible = False
                    Lv_Faktur.Location = New Point(725, 153)
                End Using

            Else
                Txt_PO.Focus()
            End If


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub


    Private Sub Txt_PO_TextChanged(sender As Object, e As EventArgs) Handles Txt_PO.TextChanged
        If Switch_Lv = False Then Exit Sub
        If Txt_PO.Text.Trim.Length = 0 Then
            Me.Size = New Size(733, 338)
            Lv_PO.Visible = False
            Lv_PO.Location = New Point(725, 231)
            Txt_PO.Text = ""
            Txt_Ket_PO.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(733, 436)
            Lv_PO.Location = New Point(174, 231)
            Lv_PO.Visible = True
        End If

        Try
            OpenConn()

            Lv_PO.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_PO.Items.Add("--- SELURUH ---")
            Lv.SubItems.Add("--- SELURUH ---")
            Lv.SubItems.Add("--- SELURUH ---")
            SQL = "select No_Faktur, No_Nota, Tanggal, Jam from EMI_Pembelian_PO_Barang_Lain where Kode_Perusahaan = '" & KodePerusahaan & "' and status is null and No_Faktur like '%" & Txt_PO.Text & "%' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lv = Lv_PO.Items.Add(Dr("No_Faktur"))
                    Lv.SubItems.Add(Dr("No_Nota"))

                    If General_Class.CekNULL(Dr("Tanggal")) = "" Then
                        Lv.SubItems.Add("-")
                    Else
                        Lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
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

    Private Sub Txt_Ket_PO_TextChanged(sender As Object, e As EventArgs) Handles Txt_Ket_PO.TextChanged
        If Switch_Lv = False Then Exit Sub
        If Txt_Ket_PO.Text.Trim.Length = 0 Then
            Me.Size = New Size(733, 338)
            Lv_PO.Visible = False
            Lv_PO.Location = New Point(725, 231)
            Txt_PO.Text = ""
            Txt_Ket_PO.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(733, 436)
            Lv_PO.Location = New Point(174, 231)
            Lv_PO.Visible = True
        End If

        Try
            OpenConn()

            Lv_PO.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_PO.Items.Add("--- SELURUH ---")
            Lv.SubItems.Add("--- SELURUH ---")
            Lv.SubItems.Add("--- SELURUH ---")
            SQL = "select No_Faktur, No_Nota, Tanggal, Jam from EMI_Pembelian_PO_Barang_Lain where Kode_Perusahaan = '" & KodePerusahaan & "' and status is null and No_Nota like '%" & Txt_Ket_PO.Text & "%' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lv = Lv_PO.Items.Add(Dr("No_Faktur"))
                    Lv.SubItems.Add(Dr("No_Nota"))

                    If General_Class.CekNULL(Dr("Tanggal")) = "" Then
                        Lv.SubItems.Add("-")
                    Else
                        Lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
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

    Private Sub Txt_PO_Leave(sender As Object, e As EventArgs) Handles Txt_PO.Leave
        If Txt_PO.Text.Trim.Length = 0 Then Exit Sub
        If Lv_PO.Focused = True Then Exit Sub

        Try
            OpenConn()

            If Not Txt_PO.Text = "--- SELURUH ---" Then

                SQL = "select No_Faktur, No_Nota, Tanggal, Jam from EMI_Pembelian_PO_Barang_Lain where Kode_Perusahaan = '" & KodePerusahaan & "' and status is null and No_Faktur = '" & Txt_PO.Text & "' "
                Using Dr = Open(SQL)
                    If Dr.Read Then
                        Txt_PO.Text = Dr("No_Faktur")
                        Txt_Ket_PO.Text = Dr("No_Nota")
                        Txt_Kd_Perusahaan_Import.Focus()
                    Else
                        MessageBox.Show("No PO tidak ditemukan . . ! !", Judul)
                        Txt_PO.Text = ""
                        Txt_Ket_PO.Text = ""
                        Txt_PO.Focus()
                    End If

                    Me.Size = New Size(733, 338)
                    Lv_PO.Visible = False
                    Lv_PO.Location = New Point(725, 231)
                End Using

            Else
                Txt_Kd_Perusahaan_Import.Focus()
            End If


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_Kd_Perusahaan_Import_TextChanged(sender As Object, e As EventArgs) Handles Txt_Kd_Perusahaan_Import.TextChanged
        If Switch_Lv = False Then Exit Sub
        If Txt_Kd_Perusahaan_Import.Text.Trim.Length = 0 Then
            Me.Size = New Size(733, 338)
            LV_Perusahaan_Import.Visible = False
            LV_Perusahaan_Import.Location = New Point(725, 205)
            Txt_Kd_Perusahaan_Import.Text = ""
            Txt_Nm_Perusahaan_Import.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(733, 465)
            LV_Perusahaan_Import.Location = New Point(174, 205)
            LV_Perusahaan_Import.Visible = True
        End If

        Try
            OpenConn()

            LV_Perusahaan_Import.Items.Clear()

            Dim Lv As ListViewItem
            Lv = LV_Perusahaan_Import.Items.Add("--- SELURUH ---")
            Lv.SubItems.Add("--- SELURUH ---")

            SQL = "select Kode_Perusahaan_Biaya_Import as Kode_Perusahaan_Biaya_Import, Nama as Keterangan from perusahaan_biaya_import where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Perusahaan_Biaya_Import like '%" & Txt_Kd_Perusahaan_Import.Text & "%' "
            SQL = SQL & "union all "
            SQL = SQL & "select Kode_Supplier as Kode_Perusahaan_Biaya_Import, Nama as Keterangan from suppliers where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Supplier like '%" & Txt_Kd_Perusahaan_Import.Text & "%' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lv = LV_Perusahaan_Import.Items.Add(Dr("Kode_Perusahaan_Biaya_Import"))
                    Lv.SubItems.Add(Dr("Keterangan"))

                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_Nm_Perusahaan_Import_TextChanged(sender As Object, e As EventArgs) Handles Txt_Nm_Perusahaan_Import.TextChanged
        If Switch_Lv = False Then Exit Sub
        If Txt_Nm_Perusahaan_Import.Text.Trim.Length = 0 Then
            Me.Size = New Size(733, 338)
            LV_Perusahaan_Import.Visible = False
            LV_Perusahaan_Import.Location = New Point(725, 205)
            Txt_Kd_Perusahaan_Import.Text = ""
            Txt_Nm_Perusahaan_Import.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(733, 465)
            LV_Perusahaan_Import.Location = New Point(174, 205)
            LV_Perusahaan_Import.Visible = True
        End If

        Try
            OpenConn()

            LV_Perusahaan_Import.Items.Clear()

            Dim Lv As ListViewItem
            Lv = LV_Perusahaan_Import.Items.Add("--- SELURUH ---")
            Lv.SubItems.Add("--- SELURUH ---")

            SQL = "select Kode_Perusahaan_Biaya_Import as Kode_Perusahaan_Biaya_Import, Nama as Keterangan from perusahaan_biaya_import where Kode_Perusahaan = '" & KodePerusahaan & "' and Nama like '%" & Txt_Nm_Perusahaan_Import.Text & "%' "
            SQL = SQL & "union all "
            SQL = SQL & "select Kode_Supplier as Kode_Perusahaan_Biaya_Import, Nama as Keterangan from suppliers where Kode_Perusahaan = '" & KodePerusahaan & "' and Nama like '%" & Txt_Nm_Perusahaan_Import.Text & "%' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lv = LV_Perusahaan_Import.Items.Add(Dr("Kode_Perusahaan_Biaya_Import"))
                    Lv.SubItems.Add(Dr("Keterangan"))

                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_Kd_Perusahaan_Import_Leave(sender As Object, e As EventArgs) Handles Txt_Kd_Perusahaan_Import.Leave
        If Txt_Kd_Perusahaan_Import.Text.Trim.Length = 0 Then Exit Sub
        If LV_Perusahaan_Import.Focused = True Then Exit Sub

        Try
            OpenConn()

            If Not Txt_Kd_Perusahaan_Import.Text = "--- SELURUH ---" Then

                SQL = "select Kode_Perusahaan_Biaya_Import as Kode_Perusahaan_Biaya_Import, Nama as Keterangan from perusahaan_biaya_import where Kode_Perusahaan = '" & KodePerusahaan & "' and Nama = '" & Txt_Kd_Perusahaan_Import.Text & "' "
                SQL = SQL & "union all "
                SQL = SQL & "select Kode_Supplier as Kode_Perusahaan_Biaya_Import, Nama as Keterangan from suppliers where Kode_Perusahaan = '" & KodePerusahaan & "' and Nama = '" & Txt_Kd_Perusahaan_Import.Text & "' "
                Using Dr = Open(SQL)
                    If Dr.Read Then
                        Txt_Kd_Perusahaan_Import.Text = Dr("No_Faktur")
                        Txt_Nm_Perusahaan_Import.Text = Dr("No_Nota")
                        Txt_Kd_Kategori.Focus()
                    Else
                        MessageBox.Show("Perusahaan Biaya Import tidak ditemukan . . ! !", Judul)
                        Txt_Kd_Perusahaan_Import.Text = ""
                        Txt_Nm_Perusahaan_Import.Text = ""
                        Txt_Kd_Perusahaan_Import.Focus()
                    End If

                    Me.Size = New Size(733, 338)
                    LV_Perusahaan_Import.Visible = False
                    LV_Perusahaan_Import.Location = New Point(725, 205)
                End Using

            Else
                Txt_Kd_Kategori.Focus()
            End If


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_Kd_Kategori_TextChanged(sender As Object, e As EventArgs) Handles Txt_Kd_Kategori.TextChanged
        If Switch_Lv = False Then Exit Sub
        If Txt_Kd_Kategori.Text.Trim.Length = 0 Then
            Me.Size = New Size(733, 338)
            Lv_Kategori.Visible = False
            Lv_Kategori.Location = New Point(725, 231)
            Txt_Kd_Kategori.Text = ""
            Txt_Nm_Kategori.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(733, 490)
            Lv_Kategori.Location = New Point(174, 231)
            Lv_Kategori.Visible = True
        End If

        Try
            OpenConn()

            Lv_Kategori.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_Kategori.Items.Add("--- SELURUH ---")
            Lv.SubItems.Add("--- SELURUH ---")

            SQL = ";with cte as ( "
            SQL = SQL & "select Kode_Master_Kategori_Biaya_Import, nama as Keterangan from perusahaan_biaya_import where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "union all "
            SQL = SQL & "select kode as Kode_Master_Kategori_Biaya_Import, kode as Keterangan from Pelunasan_Pembelian_Barang_Lain where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "union all "
            SQL = SQL & "select top(1) z.Kode_Group_Jenis as Kode_Master_Kategori_Biaya_Import, z.Kode_Group_Jenis as Keterangan "
            SQL = SQL & "from EMI_Pembelian_Detail_Barang_Lain x, barang_Lain y, emi_group_jenis_lain z "
            SQL = SQL & "where x.kode_Perusahaan='" & KodePerusahaan & "' "
            SQL = SQL & "and x.kode_perusahaan=y.kode_Perusahaan and x.kode_Barang=y.kode_barang and x.kode_stock_Owner=y.kode_stock_owner "
            SQL = SQL & "and y.kode_perusahaan=z.kode_Perusahaan and y.id_group_jenis=z.id_group_jenis  "
            SQL = SQL & ") select distinct Kode_Master_Kategori_Biaya_Import, Keterangan from cte where Kode_Master_Kategori_Biaya_Import like '%" & Txt_Kd_Kategori.Text & "%' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lv = Lv_Kategori.Items.Add(Dr("Kode_Master_Kategori_Biaya_Import"))
                    Lv.SubItems.Add(Dr("Keterangan"))

                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_Nm_Kategori_TextChanged(sender As Object, e As EventArgs) Handles Txt_Nm_Kategori.TextChanged
        If Switch_Lv = False Then Exit Sub
        If Txt_Nm_Kategori.Text.Trim.Length = 0 Then
            Me.Size = New Size(733, 338)
            Lv_Kategori.Visible = False
            Lv_Kategori.Location = New Point(725, 231)
            Txt_Kd_Kategori.Text = ""
            Txt_Nm_Kategori.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(733, 490)
            Lv_Kategori.Location = New Point(174, 231)
            Lv_Kategori.Visible = True
        End If

        Try
            OpenConn()

            Lv_Kategori.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_Kategori.Items.Add("--- SELURUH ---")
            Lv.SubItems.Add("--- SELURUH ---")

            SQL = ";with cte as ( "
            SQL = SQL & "select Kode_Master_Kategori_Biaya_Import, nama as Keterangan from perusahaan_biaya_import where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "union all "
            SQL = SQL & "select kode as Kode_Master_Kategori_Biaya_Import, kode as Keterangan from Pelunasan_Pembelian_Barang_Lain where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "union all "
            SQL = SQL & "select top(1) z.Kode_Group_Jenis as Kode_Master_Kategori_Biaya_Import, z.Kode_Group_Jenis as Keterangan "
            SQL = SQL & "from EMI_Pembelian_Detail_Barang_Lain x, barang_Lain y, emi_group_jenis_lain z "
            SQL = SQL & "where x.kode_Perusahaan='" & KodePerusahaan & "' "
            SQL = SQL & "and x.kode_perusahaan=y.kode_Perusahaan and x.kode_Barang=y.kode_barang and x.kode_stock_Owner=y.kode_stock_owner "
            SQL = SQL & "and y.kode_perusahaan=z.kode_Perusahaan and y.id_group_jenis=z.id_group_jenis  "
            SQL = SQL & ") select distinct Kode_Master_Kategori_Biaya_Import, Keterangan from cte where Keterangan like '%" & Txt_Nm_Kategori.Text & "%' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lv = Lv_Kategori.Items.Add(Dr("Kode_Master_Kategori_Biaya_Import"))
                    Lv.SubItems.Add(Dr("Keterangan"))

                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_Kd_Kategori_Leave(sender As Object, e As EventArgs) Handles Txt_Kd_Kategori.Leave
        If Txt_Kd_Kategori.Text.Trim.Length = 0 Then Exit Sub
        If Lv_Kategori.Focused = True Then Exit Sub

        Try
            OpenConn()

            If Not Txt_Kd_Kategori.Text = "--- SELURUH ---" Then

                SQL = ";with cte as ( "
                SQL = SQL & "select Kode_Master_Kategori_Biaya_Import, nama as Keterangan from perusahaan_biaya_import where Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "union all "
                SQL = SQL & "select kode as Kode_Master_Kategori_Biaya_Import, kode as Keterangan from Pelunasan_Pembelian_Barang_Lain where Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "union all "
                SQL = SQL & "select top(1) z.Kode_Group_Jenis as Kode_Master_Kategori_Biaya_Import, z.Kode_Group_Jenis as Keterangan "
                SQL = SQL & "from EMI_Pembelian_Detail_Barang_Lain x, barang_Lain y, emi_group_jenis_lain z "
                SQL = SQL & "where x.kode_Perusahaan='" & KodePerusahaan & "' "
                SQL = SQL & "and x.kode_perusahaan=y.kode_Perusahaan and x.kode_Barang=y.kode_barang and x.kode_stock_Owner=y.kode_stock_owner "
                SQL = SQL & "and y.kode_perusahaan=z.kode_Perusahaan and y.id_group_jenis=z.id_group_jenis  "
                SQL = SQL & ") select distinct Kode_Master_Kategori_Biaya_Import, Keterangan from cte where Kode_Master_Kategori_Biaya_Import = '" & Txt_Kd_Kategori.Text & "' "
                Using Dr = Open(SQL)
                    If Dr.Read Then
                        Txt_Kd_Kategori.Text = Dr("Kode_Master_Kategori_Biaya_Import")
                        Txt_Nm_Kategori.Text = Dr("Keterangan")
                        BtnCetak.Focus()
                    Else
                        MessageBox.Show("Kategori Biaya Import tidak ditemukan . . ! !", Judul)
                        Txt_Kd_Kategori.Text = ""
                        Txt_Nm_Kategori.Text = ""
                        Txt_Kd_Kategori.Focus()
                    End If

                    Me.Size = New Size(733, 338)
                    Lv_Kategori.Visible = False
                    Lv_Kategori.Location = New Point(725, 231)
                End Using

            Else
                BtnCetak.Focus()
            End If


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    '==========================================================================================================================================================================================================
    '=     HANDLE KEYPRESS
    '==========================================================================================================================================================================================================

    Private Sub Tgl1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl1.KeyPress
        If e.KeyChar = Chr(13) Then Tgl2.Focus()
    End Sub

    Private Sub Tgl2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl2.KeyPress
        If e.KeyChar = Chr(13) Then
            Cmb_JenisLaporan.DroppedDown = True
            Cmb_JenisLaporan.Focus()
        End If
    End Sub

    Private Sub Txt_Faktur_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Faktur.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_Faktur.Text.Trim.Length = 0 Then Txt_Faktur.Focus()
            Txt_Faktur_Leave(Txt_Faktur, e)

            Me.Size = New Size(733, 338)
            Lv_Faktur.Visible = False
            Lv_Faktur.Location = New Point(725, 153)

            'Txt_UserValidasi.Focus()
        End If
    End Sub

    Private Sub Txt_Faktur_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_Faktur.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Faktur.Focus()
    End Sub

    Private Sub Txt_PO_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_PO.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_PO.Text.Trim.Length = 0 Then Txt_PO.Focus()
            Txt_PO_Leave(Txt_PO, e)

            Me.Size = New Size(733, 338)
            Lv_PO.Visible = False
            Lv_PO.Location = New Point(725, 231)

            'Txt_UserValidasi.Focus()
        End If
    End Sub

    Private Sub Txt_PO_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_PO.KeyDown
        If e.KeyCode = Keys.Down Then Lv_PO.Focus()
    End Sub

    Private Sub Txt_Ket_PO_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Ket_PO.KeyPress
        If e.KeyChar = Chr(13) Then
            Txt_PO_Leave(Txt_Ket_PO, e)

            Me.Size = New Size(733, 338)
            Lv_PO.Visible = False
            Lv_PO.Location = New Point(725, 231)

            'Txt_KdKategori.Focus()
        End If
    End Sub

    Private Sub Txt_Ket_PO_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_Ket_PO.KeyDown
        If e.KeyCode = Keys.Down Then Lv_PO.Focus()
    End Sub

    Private Sub Txt_Kd_Perusahaan_Import_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Kd_Perusahaan_Import.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_Kd_Perusahaan_Import.Text.Trim.Length = 0 Then Txt_Kd_Perusahaan_Import.Focus()
            Txt_Kd_Perusahaan_Import_Leave(Txt_Kd_Perusahaan_Import, e)

            Me.Size = New Size(733, 338)
            LV_Perusahaan_Import.Visible = False
            LV_Perusahaan_Import.Location = New Point(725, 231)

            'Txt_UserValidasi.Focus()
        End If
    End Sub

    Private Sub Txt_Kd_Perusahaan_Import_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_Kd_Perusahaan_Import.KeyDown
        If e.KeyCode = Keys.Down Then LV_Perusahaan_Import.Focus()
    End Sub

    Private Sub Txt_Nm_Perusahaan_Import_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Nm_Perusahaan_Import.KeyPress
        If e.KeyChar = Chr(13) Then
            Txt_Kd_Perusahaan_Import_Leave(Txt_Nm_Perusahaan_Import, e)

            Me.Size = New Size(733, 338)
            LV_Perusahaan_Import.Visible = False
            LV_Perusahaan_Import.Location = New Point(725, 231)

            'Txt_KdKategori.Focus()
        End If
    End Sub

    Private Sub Txt_Nm_Perusahaan_Import_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_Nm_Perusahaan_Import.KeyDown
        If e.KeyCode = Keys.Down Then LV_Perusahaan_Import.Focus()
    End Sub

    Private Sub Txt_Kd_Kategori_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Kd_Kategori.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_Kd_Kategori.Text.Trim.Length = 0 Then Txt_Kd_Kategori.Focus()
            Txt_Kd_Kategori_Leave(Txt_Kd_Kategori, e)

            Me.Size = New Size(733, 338)
            Lv_Kategori.Visible = False
            Lv_Kategori.Location = New Point(725, 231)

            'Txt_UserValidasi.Focus()
        End If
    End Sub

    '==========================================================================================================================================================================================================
    '=     HANDLE LISTVIEW
    '==========================================================================================================================================================================================================
    Private Sub Lv_Faktur_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Faktur.DoubleClick
        If Lv_Faktur.Items.Count = 0 Or Lv_Faktur.FocusedItem.Index = -1 Then Exit Sub

        Dim Faktur As String = Lv_Faktur.FocusedItem.SubItems(0).Text

        Txt_Faktur.Text = Faktur

        Me.Size = New Size(733, 338)
        Lv_Faktur.Visible = False
        Lv_Faktur.Location = New Point(725, 153)
        Txt_PO.Focus()
    End Sub

    Private Sub Lv_Faktur_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Faktur.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_Faktur_DoubleClick(Lv_Faktur, e)
        End If
    End Sub

    Private Sub Lv_PO_DoubleClick(sender As Object, e As EventArgs) Handles Lv_PO.DoubleClick
        If Lv_PO.Items.Count = 0 Or Lv_PO.FocusedItem.Index = -1 Then Exit Sub

        Dim Faktur As String = Lv_PO.FocusedItem.SubItems(0).Text
        Dim Keterangan As String = Lv_PO.FocusedItem.SubItems(1).Text

        Txt_PO.Text = Faktur
        Txt_Ket_PO.Text = Keterangan

        Me.Size = New Size(733, 338)
        Lv_PO.Visible = False
        Lv_PO.Location = New Point(725, 231)
        Txt_Kd_Perusahaan_Import.Focus()
    End Sub

    Private Sub Lv_PO_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_PO.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_PO_DoubleClick(Lv_PO, e)
        End If
    End Sub

    Private Sub LV_Perusahaan_Import_DoubleClick(sender As Object, e As EventArgs) Handles LV_Perusahaan_Import.DoubleClick
        If LV_Perusahaan_Import.Items.Count = 0 Or LV_Perusahaan_Import.FocusedItem.Index = -1 Then Exit Sub

        Dim Faktur As String = LV_Perusahaan_Import.FocusedItem.SubItems(0).Text
        Dim Keterangan As String = LV_Perusahaan_Import.FocusedItem.SubItems(1).Text

        Txt_Kd_Perusahaan_Import.Text = Faktur
        Txt_Nm_Perusahaan_Import.Text = Keterangan

        Me.Size = New Size(733, 338)
        LV_Perusahaan_Import.Visible = False
        LV_Perusahaan_Import.Location = New Point(725, 205)
        Txt_Kd_Kategori.Focus()
    End Sub

    Private Sub LV_Perusahaan_Import_KeyDown(sender As Object, e As KeyEventArgs) Handles LV_Perusahaan_Import.KeyDown
        If e.KeyCode = Keys.Enter Then
            LV_Perusahaan_Import_DoubleClick(LV_Perusahaan_Import, e)
        End If
    End Sub

    Private Sub Lv_Kategori_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Kategori.DoubleClick
        If Lv_Kategori.Items.Count = 0 Or Lv_Kategori.FocusedItem.Index = -1 Then Exit Sub

        Dim Faktur As String = Lv_Kategori.FocusedItem.SubItems(0).Text
        Dim Keterangan As String = Lv_Kategori.FocusedItem.SubItems(1).Text

        Txt_Kd_Kategori.Text = Faktur
        Txt_Nm_Kategori.Text = Keterangan

        Me.Size = New Size(733, 338)
        Lv_Kategori.Visible = False
        Lv_Kategori.Location = New Point(725, 231)
        Txt_Kd_Kategori.Focus()
    End Sub


    '==========================================================================================================================================================================================================
    '=     HANDLE BUTTON
    '==========================================================================================================================================================================================================
    Private Sub BtnCetak_Click(sender As Object, e As EventArgs) Handles BtnCetak.Click
        If Tgl1.Value > Tgl2.Value Then
            MessageBox.Show("Periode I tidak boleh lebih dari periode II!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Tgl1.Value = Now.Date : Tgl2.Value = Now.Date
            Tgl1.Focus() : Exit Sub
        ElseIf Txt_Faktur.Text.Trim.Length = 0 Then
            MessageBox.Show("Faktur harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_Faktur.Focus() : Exit Sub
        ElseIf Txt_PO.Text.Trim.Length = 0 Then
            MessageBox.Show("No PO diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_PO.Focus() : Exit Sub
        ElseIf Txt_Kd_Perusahaan_Import.Text.Trim.Length = 0 Then
            MessageBox.Show("Kode Perusahaan Biaya Import harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_Kd_Perusahaan_Import.Focus() : Exit Sub
        ElseIf Txt_Kd_Kategori.Text.Trim.Length = 0 Then
            MessageBox.Show("Kode Kategori Biaya Import harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_Kd_Kategori.Focus() : Exit Sub
        End If

        Try
            OpenConn()


            Dim NoFaktur, No_PO, KdPerusahaanBiayaImport, KdMasterKategorim As String

            SQL = "EXEC N_EMI_SP_Pelunasan_Asset "
            SQL = SQL & "@kode_perusahaan = '" & KodePerusahaan & "', "


            If Not Txt_Faktur.Text = "--- SELURUH ---" Then
                NoFaktur = Txt_Faktur.Text
                SQL = SQL & "@no_faktur = '" & Txt_Faktur.Text & "', "
            Else
                NoFaktur = "NULL"
                SQL = SQL & "@no_faktur = NULL, "
            End If

            If Not Txt_PO.Text = "--- SELURUH ---" Then
                No_PO = Txt_PO.Text
                SQL = SQL & "@no_po = '" & Txt_PO.Text & "', "
            Else
                No_PO = "NULL"
                SQL = SQL & "@no_po = NULL, "
            End If

            If Not Txt_Kd_Perusahaan_Import.Text = "--- SELURUH ---" Then
                KdPerusahaanBiayaImport = Txt_Kd_Perusahaan_Import.Text
                SQL = SQL & "@Kd_perusahaan_biaya_import = '" & Txt_Kd_Perusahaan_Import.Text & "', "
            Else
                KdPerusahaanBiayaImport = "NULL"
                SQL = SQL & "@Kd_perusahaan_biaya_import = NULL, "
            End If

            If Not Txt_Kd_Kategori.Text = "--- SELURUH ---" Then
                KdMasterKategorim = Txt_Kd_Kategori.Text
                SQL = SQL & "@Kd_master_kategori_biaya_import = '" & Txt_Kd_Kategori.Text & "', "
            Else
                KdMasterKategorim = "NULL"
                SQL = SQL & "@Kd_master_kategori_biaya_import = NULL, "
            End If
            SQL = SQL & "@tanggal_awal = '" & Format(Tgl1.Value, "yyyy-MM-dd") & "', "
            SQL = SQL & "@tanggal_akhir = '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then

                    Dim CrDoc As Object
                    If Cmb_JenisLaporan.SelectedIndex = 0 Then
                        CrDoc = New N_EMI_CR_Laporan_Pelunasan_Barang_Lain_Cut_Off
                    Else
                        CrDoc = New N_EMI_CR_Laporan_Pelunasan_Barang_Lain_Cut_Off_Rekap
                    End If

                    With A_Place_For_Printing2
                        ' Set data dan koneksi database
                        CrDoc.SetDataSource(Ds)
                        CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)

                        CrDoc.SetParameterValue("@kode_perusahaan", KodePerusahaan)

                        If String.IsNullOrEmpty(NoFaktur) OrElse NoFaktur = "NULL" Then
                            CrDoc.SetParameterValue("@no_faktur", DBNull.Value)
                        Else
                            CrDoc.SetParameterValue("@no_faktur", NoFaktur)
                        End If

                        If String.IsNullOrEmpty(No_PO) OrElse No_PO = "NULL" Then
                            CrDoc.SetParameterValue("@no_po", DBNull.Value)
                        Else
                            CrDoc.SetParameterValue("@no_po", No_PO)
                        End If

                        If String.IsNullOrEmpty(KdPerusahaanBiayaImport) OrElse KdPerusahaanBiayaImport = "NULL" Then
                            CrDoc.SetParameterValue("@Kd_perusahaan_biaya_import", DBNull.Value)
                        Else
                            CrDoc.SetParameterValue("@Kd_perusahaan_biaya_import", KdPerusahaanBiayaImport)
                        End If

                        If String.IsNullOrEmpty(KdMasterKategorim) OrElse KdMasterKategorim = "NULL" Then
                            CrDoc.SetParameterValue("@Kd_master_kategori_biaya_import", DBNull.Value)
                        Else
                            CrDoc.SetParameterValue("@Kd_master_kategori_biaya_import", KdMasterKategorim)
                        End If

                        CrDoc.SetParameterValue("@tanggal_awal", Format(Tgl1.Value, "yyyy-MM-dd"))
                        CrDoc.SetParameterValue("@tanggal_akhir", Format(Tgl2.Value, "yyyy-MM-dd"))

                        CrDoc.SummaryInfo.ReportTitle = "Periode: " & Format(Tgl1.Value, "yyyy-MM-dd") & " s/d " & Format(Tgl2.Value, "yyyy-MM-dd") & Chr(13) & " "

                        .Text = "Laporan Pelunasan Cut Off"
                        .CrystalReportViewer1.ReportSource = CrDoc
                        '.CrystalReportViewer1.DisplayGroupTree = False
                        .Refresh()
                        .Show()
                        .Focus()
                    End With
                Else
                    MessageBox.Show("Tidak ada data yang dapat dicetak!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
            End Using



            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub BtnExit_Click(sender As Object, e As EventArgs) Handles BtnExit.Click
        Me.Close()
    End Sub

    Private Sub Cmb_JenisLaporan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_JenisLaporan.KeyPress
        If e.KeyChar = Chr(13) Then Txt_Faktur.Focus()
    End Sub
End Class
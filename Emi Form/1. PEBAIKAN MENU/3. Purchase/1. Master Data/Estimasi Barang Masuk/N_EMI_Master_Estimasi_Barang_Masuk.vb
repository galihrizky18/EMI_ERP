

Public Class N_EMI_Master_Estimasi_Barang_Masuk


    Dim arrJenisSupplier As New ArrayList


    Dim Lv_Kategori_Supplier, Lv_Kd_Barang, Lv_Nm_Barang, Lv_Waktu_Pabrikasi, Lv_Waktu_Pengiriman, Lv_Id_Kategori As String

    Dim item_Kategori_Supplier As Integer = 0
    Dim item_Kd_Barang As Integer = 1
    Dim item_Nm_Barang As Integer = 2
    Dim item_Waktu_Pabrikasi As Integer = 3
    Dim item_Waktu_Pengiriman As Integer = 4
    Dim item_Id_Kategori As Integer = 5

    Dim Switch_Auto_Complete As Boolean = False

    Private Sub N_EMI_Master_Estimasi_Barang_Masuk_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        Lv_Data.Columns.Clear()
        Lv_Data.Columns.Add("Kategori Supplier", 170, HorizontalAlignment.Left) '0
        Lv_Data.Columns.Add("Kode Barang", 130, HorizontalAlignment.Left) '1
        Lv_Data.Columns.Add("Barang", 250, HorizontalAlignment.Left) '2
        Lv_Data.Columns.Add("Waktu Pabrikasi", 130, HorizontalAlignment.Center) '3
        Lv_Data.Columns.Add("Waktu Pengiriman", 130, HorizontalAlignment.Center) '4
        'Hide
        Lv_Data.Columns.Add("ID_Kategori", 0, HorizontalAlignment.Center) '5
        Lv_Data.View = View.Details


        Lv_Barang.Columns.Clear()
        Lv_Barang.Columns.Add("Kode Barang", 150, HorizontalAlignment.Left)
        Lv_Barang.Columns.Add("Barang", 250, HorizontalAlignment.Left)
        Lv_Barang.View = View.Details


        Try
            OpenConn()

            Cmb_Jenis_Supplier.Items.Clear() : arrJenisSupplier.Clear()
            SQL = $"
                select ID_Kategori_Suppliers, Kode_Kategori_Suppliers
                FROM Suppliers_Kategori
                WHERE Kode_Perusahaan = '{KodePerusahaan}'
            "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Cmb_Jenis_Supplier.Items.Add(Dr("Kode_Kategori_Suppliers")) : arrJenisSupplier.Add(Dr("ID_Kategori_Suppliers").ToString.Trim)
                Loop
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


        Kosong()

    End Sub


    Private Sub Kosong()

        Cmb_Jenis_Supplier.SelectedIndex = -1

        Switch_Auto_Complete = True
        Txt_Kd_Barang.Text = ""
        Txt_Nm_Barang.Text = ""
        Txt_Waktu_Pabrikasi.Text = ""
        Txt_Waktu_Pengiriman.Text = ""
        Switch_Auto_Complete = False

        Btn_Simpan.Tag = "SIMPAN"
        Btn_Simpan.Text = "&Simpan"

        Cmb_Jenis_Supplier.Enabled = True
        Txt_Kd_Barang.Enabled = True : Txt_Nm_Barang.Enabled = True

        Load_Data()

        Cmb_Jenis_Supplier.Focus()

    End Sub


    Private Sub Load_Data()

        Try
            OpenConn()

            Lv_Data.Items.Clear()
            SQL = $"
                SELECT a.Kode_Perusahaan, a.Id_Kategori_Supplier, b.Kode_Kategori_Suppliers, a.Kode_Barang, c.nama AS Nama_Barang, a.Waktu_Pabrikasi, a.Waktu_Pengiriman
                FROM emi_detail_proses_pengiriman_po a
	                INNER JOIN Suppliers_Kategori b ON a.kode_perusahaan = b.Kode_Perusahaan and a.Id_Kategori_Supplier = b.ID_Kategori_Suppliers
	                CROSS Apply (
		                select TOP 1 z.kode_Barang, z.nama
		                from barang z 
		                where a.kode_perusahaan = z.Kode_Perusahaan 
		                and a.kode_barang = z.Kode_Barang
	                ) c 
                where a.Kode_Perusahaan = '{KodePerusahaan}'
                order BY a.Id_Kategori_Supplier, a.Kode_Barang
            "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Data.Items.Add(Dr("Kode_Kategori_Suppliers"))
                    Lv.SubItems.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add(Dr("Nama_Barang"))
                    Lv.SubItems.Add($"{Dr("Waktu_Pabrikasi")} Hari")
                    Lv.SubItems.Add($"{Dr("Waktu_Pengiriman")} Hari")
                    Lv.SubItems.Add(Dr("Id_Kategori_Supplier"))
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
        If Cmb_Jenis_Supplier.SelectedIndex = -1 Then
            MessageBox.Show("Jenis Supllier Harus Dipilih Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Jenis_Supplier.DroppedDown = True
            Cmb_Jenis_Supplier.Focus()
            Exit Sub
        ElseIf Txt_Kd_Barang.Text.Trim.Length = 0 Then
            MessageBox.Show("Kode Barang Harus Diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_Kd_Barang.Focus()
            Exit Sub
        ElseIf Txt_Waktu_Pabrikasi.Text.Trim.Length = 0 Then
            MessageBox.Show("Waktu Pabrikasi Harus Diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_Waktu_Pabrikasi.Focus()
            Exit Sub
        ElseIf Txt_Waktu_Pengiriman.Text.Trim.Length = 0 Then
            MessageBox.Show("Waktu Pengiriman Harus Diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_Waktu_Pengiriman.Focus()
            Exit Sub
        End If

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            Dim Action As String = ""


            If Btn_Simpan.Tag.ToString.ToUpper = "SIMPAN" Then


                '=======================================
                '=     CEK APAKAH BARANG SUDAH ADA     =
                '=======================================
                SQL = $"
                    select Kode_Perusahaan
                    FROM emi_detail_proses_pengiriman_po
                    WHERE Kode_Perusahaan = '{KodePerusahaan}'
                    AND Id_Kategori_Supplier = '{arrJenisSupplier(Cmb_Jenis_Supplier.SelectedIndex)}'
                    and Kode_Barang = '{Txt_Kd_Barang.Text.Trim}'
                "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show($"Jenis Supplier {Cmb_Jenis_Supplier.Text} dan Kode Barang {Txt_Kd_Barang.Text.Trim} Sudah Diinput Sebelumnya", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                SQL = $"
                    insert INTO emi_detail_proses_pengiriman_po (kode_perusahaan, Id_Kategori_Supplier, Kode_Barang, Waktu_Pabrikasi, Waktu_Pengiriman)
                    values ('{KodePerusahaan}', '{arrJenisSupplier(Cmb_Jenis_Supplier.SelectedIndex)}', 
                        '{Txt_Kd_Barang.Text}', '{Txt_Waktu_Pabrikasi.Text}', '{Txt_Waktu_Pengiriman.Text}')
                "
                ExecuteTrans(SQL)

                Action = "simpan"


            ElseIf Btn_Simpan.Tag.ToString.ToUpper = "UPDATE" Then

                '===============================
                '=     CEK APAKAH DATA ADA     =
                '===============================
                SQL = $"
                    select Kode_Perusahaan
                    FROM emi_detail_proses_pengiriman_po
                    WHERE Kode_Perusahaan = '{KodePerusahaan}'
                    AND Id_Kategori_Supplier = '{arrJenisSupplier(Cmb_Jenis_Supplier.SelectedIndex)}'
                    and Kode_Barang = '{Txt_Kd_Barang.Text.Trim}'
                "
                Using Dr = OpenTrans(SQL)
                    If Not Dr.Read Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show($"Jenis Supplier {Cmb_Jenis_Supplier.Text} dan Kode Barang {Txt_Kd_Barang.Text.Trim} Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                SQL = $"
                    UPDATE emi_detail_proses_pengiriman_po SET Waktu_Pabrikasi = '{Txt_Waktu_Pabrikasi.Text}', Waktu_Pengiriman = '{Txt_Waktu_Pengiriman.Text}' 
                    where Kode_Perusahaan = '{KodePerusahaan}' AND Id_Kategori_Supplier = '{arrJenisSupplier(Cmb_Jenis_Supplier.SelectedIndex)}' and Kode_Barang = '{Txt_Kd_Barang.Text.Trim}'
                "
                ExecuteTrans(SQL)

                Action = "update"

            End If


            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
            MessageBox.Show($"Data Berhasil Di{Action}", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


        Kosong()

    End Sub


    Private Sub Btn_Hapus_Click(sender As Object, e As EventArgs) Handles Btn_Hapus.Click
        If Cmb_Jenis_Supplier.SelectedIndex = -1 Then
            MessageBox.Show("Jenis Supllier Harus Dipilih Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Jenis_Supplier.DroppedDown = True
            Cmb_Jenis_Supplier.Focus()
            Exit Sub
        ElseIf Txt_Kd_Barang.Text.Trim.Length = 0 Then
            MessageBox.Show("Kode Barang Harus Diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_Kd_Barang.Focus()
        End If

        If (MessageBox.Show($"Yakin ingin Menghapus Data Ini??", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)) = vbNo Then Exit Sub

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            '=======================================
            '=     CEK APAKAH BARANG SUDAH ADA     =
            '=======================================
            SQL = $"
                select Kode_Perusahaan
                FROM emi_detail_proses_pengiriman_po
                WHERE Kode_Perusahaan = '{KodePerusahaan}'
                AND Id_Kategori_Supplier = '{arrJenisSupplier(Cmb_Jenis_Supplier.SelectedIndex)}'
                and Kode_Barang = '{Txt_Kd_Barang.Text.Trim}'
            "
            Using Dr = OpenTrans(SQL)
                If Not Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show($"Jenis Supplier {Cmb_Jenis_Supplier.Text} dan Kode Barang {Txt_Kd_Barang.Text.Trim} Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            SQL = $"
                DELETE emi_detail_proses_pengiriman_po 
                where Kode_Perusahaan = '{KodePerusahaan}' 
                AND Id_Kategori_Supplier = '{arrJenisSupplier(Cmb_Jenis_Supplier.SelectedIndex)}' 
                and Kode_Barang = '{Txt_Kd_Barang.Text.Trim}'
            "
            ExecuteTrans(SQL)



            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
            MessageBox.Show("Data Berhasil Dihapus", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
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

    Private Sub Lv_Data_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Data.DoubleClick
        If Lv_Data.Items.Count = 0 Or Lv_Data.FocusedItem Is Nothing Then Exit Sub

        Dim Selected_Id_Kategori_Supplier As Integer = Lv_Data.FocusedItem.SubItems(item_Id_Kategori).Text
        Dim Selected_Kd_Barang As String = Lv_Data.FocusedItem.SubItems(item_Kd_Barang).Text

        Dim asdasda As String = arrJenisSupplier.IndexOf(Selected_Id_Kategori_Supplier)

        Dim val_string As String = Selected_Id_Kategori_Supplier.ToString()
        Dim idx As Integer = arrJenisSupplier.IndexOf(val_string)

        Cmb_Jenis_Supplier.SelectedIndex = idx

        Switch_Auto_Complete = True
        Txt_Kd_Barang.Text = Selected_Kd_Barang
        Switch_Auto_Complete = False
        Txt_Kd_Barang_Leave(sender, e)


        Try
            OpenConn()

            SQL = $"
                SELECT Id_Kategori_Supplier, Kode_Barang, Waktu_Pabrikasi, Waktu_Pengiriman
                FROM emi_detail_proses_pengiriman_po 
                where Kode_Perusahaan = '{KodePerusahaan}' 
                AND Id_Kategori_Supplier = '{Selected_Id_Kategori_Supplier}' 
                and Kode_Barang = '{Selected_Kd_Barang}'
            "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Txt_Waktu_Pabrikasi.Text = Dr("Waktu_Pabrikasi")
                    Txt_Waktu_Pengiriman.Text = Dr("Waktu_Pengiriman")
                Else
                    CloseConn()
                    MessageBox.Show("Data Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Txt_Waktu_Pabrikasi.Text = ""
                    Txt_Waktu_Pengiriman.Text = ""
                    Cmb_Jenis_Supplier.SelectedIndex = -1
                    Switch_Auto_Complete = True
                    Txt_Kd_Barang.Text = ""
                    Txt_Nm_Barang.Text = ""
                    Switch_Auto_Complete = False
                    Exit Sub
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Btn_Simpan.Tag = "UPDATE"
        Btn_Simpan.Text = "&Update"

        Cmb_Jenis_Supplier.Enabled = False
        Txt_Kd_Barang.Enabled = False : Txt_Nm_Barang.Enabled = False

        Txt_Waktu_Pabrikasi.Focus()


    End Sub
















    Private Sub Txt_Kd_Barang_TextChanged(sender As Object, e As EventArgs) Handles Txt_Kd_Barang.TextChanged
        If Switch_Auto_Complete Then Exit Sub

        If Txt_Kd_Barang.Text.Trim.Length = 0 Then
            Lv_Barang.Visible = False
            Lv_Barang.Location = New Point(890, 110)
            Txt_Kd_Barang.Text = ""
            Txt_Nm_Barang.Text = ""
            Exit Sub
        Else
            Lv_Barang.Location = New Point(140, 110)
            Lv_Barang.Visible = True
        End If

        Try
            OpenConn()

            Lv_Barang.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_Barang.Items.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)

            SQL = $"
				select Distinct a.Kode_Barang, a.Nama
                from barang a
                where a.Kode_Perusahaan = '{KodePerusahaan}'
                and a.Kode_Barang like '%{Txt_Kd_Barang.Text}%'
                order by Kode_Barang
			"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lv = Lv_Barang.Items.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add(Dr("Nama"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_Kd_Barang_Leave(sender As Object, e As EventArgs) Handles Txt_Kd_Barang.Leave
        If Txt_Kd_Barang.Text.Trim.Length = 0 Then Exit Sub
        If Lv_Barang.Focused = True Then Exit Sub
        If Txt_Kd_Barang.Text.Trim.Length = 0 Then Exit Sub

        Try
            OpenConn()

            SQL = $"
				    select Distinct a.Kode_Barang, a.Nama
                    from barang a
                    where a.Kode_Perusahaan = '{KodePerusahaan}'
                    and a.Kode_Barang = '{Txt_Kd_Barang.Text}'
                    order by Kode_Barang
			    "
            Using Dr = Open(SQL)
                If Dr.Read Then
                    Txt_Kd_Barang.Text = Dr("Kode_Barang")
                    Txt_Nm_Barang.Text = Dr("Nama")
                    Txt_Waktu_Pabrikasi.Focus()
                Else
                    MessageBox.Show("Barang tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Txt_Kd_Barang.Text = ""
                    Txt_Nm_Barang.Text = ""
                    Txt_Kd_Barang.Focus()
                End If

                Lv_Barang.Visible = False
                Lv_Barang.Location = New Point(890, 110)
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_Kd_Barang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Kd_Barang.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_Kd_Barang.Text.Trim.Length = 0 Then Txt_Kd_Barang.Focus()
            Txt_Kd_Barang_Leave(Txt_Kd_Barang, e)


            Lv_Barang.Visible = False
            Lv_Barang.Location = New Point(890, 110)

        End If
    End Sub

    Private Sub Txt_Kd_Barang_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_Kd_Barang.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Barang.Focus()
    End Sub

    Private Sub Txt_Waktu_Pabrikasi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Waktu_Pabrikasi.KeyPress
        If e.KeyChar = Chr(13) Then
            Txt_Waktu_Pengiriman.Focus()
            Exit Sub
        End If

        Dim tb As TextBox = CType(sender, TextBox)
        Dim ch As Char = e.KeyChar

        If Char.IsControl(ch) Then
            Return
        End If

        If Char.IsDigit(ch) Then
            Return
        End If

        If ch = ","c Or ch = "."c Then

            ' Jika sudah ada koma atau titik → tolak
            If tb.Text.Contains(",") Or tb.Text.Contains(".") Then
                e.Handled = True
                Return
            End If

            Return
        End If

        e.Handled = True

    End Sub

    Private Sub Txt_Waktu_Pengiriman_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Waktu_Pengiriman.KeyPress
        If e.KeyChar = Chr(13) Then
            Btn_Simpan.Focus()
            Exit Sub
        End If

        Dim tb As TextBox = CType(sender, TextBox)
        Dim ch As Char = e.KeyChar

        If Char.IsControl(ch) Then
            Return
        End If

        If Char.IsDigit(ch) Then
            Return
        End If

        If ch = ","c Or ch = "."c Then

            ' Jika sudah ada koma atau titik → tolak
            If tb.Text.Contains(",") Or tb.Text.Contains(".") Then
                e.Handled = True
                Return
            End If

            Return
        End If

        e.Handled = True
    End Sub



    Private Sub Txt_Nm_Barang_TextChanged(sender As Object, e As EventArgs) Handles Txt_Nm_Barang.TextChanged
        If Switch_Auto_Complete Then Exit Sub

        If Txt_Nm_Barang.Text.Trim.Length = 0 Then
            Lv_Barang.Visible = False
            Lv_Barang.Location = New Point(890, 110)
            Txt_Kd_Barang.Text = ""
            Txt_Nm_Barang.Text = ""
            Exit Sub
        Else
            Lv_Barang.Location = New Point(140, 110)
            Lv_Barang.Visible = True
        End If

        Try
            OpenConn()

            Lv_Barang.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_Barang.Items.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)

            SQL = $"
				select Distinct a.Kode_Barang, a.Nama
                from barang a
                where a.Kode_Perusahaan = '{KodePerusahaan}'
                and a.Nama like '%{Txt_Nm_Barang.Text}%'
                order by Kode_Barang
			"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lv = Lv_Barang.Items.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add(Dr("Nama"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_Nm_Barang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Nm_Barang.KeyPress
        If e.KeyChar = Chr(13) Then
            Txt_Kd_Barang_Leave(Txt_Nm_Barang, e)

            Lv_Barang.Visible = False
            Lv_Barang.Location = New Point(890, 110)

            'Txt_KdKategori.Focus()
        End If
    End Sub

    Private Sub Txt_Nm_Barang_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_Nm_Barang.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Barang.Focus()
    End Sub

    Private Sub Lv_Barang_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Barang.DoubleClick
        If Lv_Barang.Items.Count = 0 Or Lv_Barang.FocusedItem.Index = -1 Then Exit Sub

        Dim KdBarng As String = Lv_Barang.FocusedItem.SubItems(0).Text
        Dim NmBarang As String = Lv_Barang.FocusedItem.SubItems(1).Text

        Switch_Auto_Complete = True
        Txt_Kd_Barang.Text = KdBarng
        Txt_Nm_Barang.Text = NmBarang
        Switch_Auto_Complete = False

        Lv_Barang.Visible = False
        Lv_Barang.Location = New Point(890, 110)

        Txt_Waktu_Pabrikasi.Focus()
    End Sub

    Private Sub Lv_Barang_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Barang.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_Barang_DoubleClick(Lv_Barang, e)
        End If
    End Sub




    Private Sub Cmb_Jenis_Supplier_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Jenis_Supplier.KeyPress
        If e.KeyChar = Chr(13) Then Txt_Kd_Barang.Focus()
    End Sub








End Class
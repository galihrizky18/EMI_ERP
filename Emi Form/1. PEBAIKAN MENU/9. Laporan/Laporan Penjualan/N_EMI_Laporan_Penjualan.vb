Public Class N_EMI_Laporan_Penjualan


    Dim arrTanggal, arrTanggalSF As New ArrayList

    Dim Switch_Auto_Complete As Boolean = False

    Private Sub N_EMI_Laporan_Penjualan_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Lv_Penjualan.Columns.Clear()
        Lv_Penjualan.Columns.Add("No Faktur", 150, HorizontalAlignment.Left)
        Lv_Penjualan.Columns.Add("Tanggal", 130, HorizontalAlignment.Center)
        Lv_Penjualan.Columns.Add("Jenis Transaksi", 180, HorizontalAlignment.Left)
        Lv_Penjualan.View = View.Details

        Lv_DO.Columns.Clear()
        Lv_DO.Columns.Add("No DO", 150, HorizontalAlignment.Left)
        Lv_DO.Columns.Add("Tanggal", 130, HorizontalAlignment.Center)
        Lv_DO.Columns.Add("Keterangan", 230, HorizontalAlignment.Left)
        Lv_DO.View = View.Details

        Lv_Customer.Columns.Clear()
        Lv_Customer.Columns.Add("Kode Customer", 150, HorizontalAlignment.Left)
        Lv_Customer.Columns.Add("Customer", 330, HorizontalAlignment.Left)
        Lv_Customer.View = View.Details

        Lv_Barang.Columns.Clear()
        Lv_Barang.Columns.Add("Kode Barang", 150, HorizontalAlignment.Left)
        Lv_Barang.Columns.Add("Barang", 330, HorizontalAlignment.Left)
        Lv_Barang.View = View.Details


        Cmb_Tanggal.Items.Clear() : arrTanggal.Clear() : arrTanggalSF.Clear()
        Cmb_Tanggal.Items.Add("Tanggal Penjualan") : arrTanggal.Add("Tanggal_Penjualan") : arrTanggalSF.Add("Tanggal_Penjualan")
        Cmb_Tanggal.Items.Add("Tanggal DO") : arrTanggal.Add("Tanggal_DO") : arrTanggalSF.Add("Tanggal_DO")
        Cmb_Tanggal.Items.Add("Tanggal Jatuh Tempo") : arrTanggal.Add("Tgl_Jatuh_Tempo") : arrTanggalSF.Add("Tgl_Jatuh_Tempo")

        Cmb_Jenis_Laporan.Items.Clear()
        Cmb_Jenis_Laporan.Items.Add("Laporan Penjualan Rekap")
        Cmb_Jenis_Laporan.Items.Add("Laporan Penjualan Detail")

        Kosong()

    End Sub

    Private Sub Kosong()

        Lv_Penjualan.Items.Clear() : Lv_DO.Items.Clear() : Lv_Customer.Items.Clear() : Lv_Barang.Items.Clear()

        Tgl1.Value = Date.Now : Tgl2.Value = Date.Now

        Cmb_Tanggal.SelectedIndex = 1
        Cmb_Jenis_Laporan.SelectedIndex = 0

        Switch_Auto_Complete = True
        Txt_No_Penjualan.Text = OpsiSeluruh
        Txt_No_DO.Text = OpsiSeluruh : Txt_Keterangan_DO.Text = OpsiSeluruh
        Txt_Kd_Customer.Text = OpsiSeluruh : Txt_Ket_Customer.Text = OpsiSeluruh
        Txt_KdBarang.Text = OpsiSeluruh : Txt_NmBarang.Text = OpsiSeluruh
        Switch_Auto_Complete = False



    End Sub


    Private Sub Txt_No_Penjualan_TextChanged(sender As Object, e As EventArgs) Handles Txt_No_Penjualan.TextChanged
        If Switch_Auto_Complete Then Exit Sub

        If Txt_No_Penjualan.Text.Trim.Length = 0 Then
            Me.Size = New Size(720, 340)
            Lv_Penjualan.Visible = False
            Lv_Penjualan.Location = New Point(720, 154)
            Txt_No_Penjualan.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(720, 408)
            Lv_Penjualan.Location = New Point(126, 154)
            Lv_Penjualan.Visible = True
        End If

        Try
            OpenConn()

            Lv_Penjualan.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_Penjualan.Items.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)

            SQL = $"
				select No_Faktur, Tanggal, 
					case 
						when Jenis_Transaksi = 'T' then 'Tunai'
						else 'Non Tunai'
					end Jenis_Transaksi
				from penjualan
				where Kode_Perusahaan = '{KodePerusahaan}'
				and status is null
				and No_Faktur like '%{Txt_No_Penjualan.Text.Trim}%'
				order by Tanggal DESC, Jam DESC
			"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lv = Lv_Penjualan.Items.Add(Dr("No_Faktur"))
                    Lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
                    Lv.SubItems.Add(Dr("Jenis_Transaksi"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_No_Penjualan_Leave(sender As Object, e As EventArgs) Handles Txt_No_Penjualan.Leave
        If Txt_No_Penjualan.Text.Trim.Length = 0 Then Exit Sub
        If Lv_Penjualan.Focused = True Then Exit Sub

        Try
            OpenConn()

            If Not Txt_No_Penjualan.Text.ToUpper = OpsiSeluruh.ToUpper Then

                SQL = $"
					select No_Faktur, Tanggal, 
						case 
							when Jenis_Transaksi = 'T' then 'Tunai'
							else 'Non Tunai'
						end Jenis_Transaksi
					from penjualan
					where Kode_Perusahaan = '{KodePerusahaan}'
					and status is null
					and No_Faktur = '{Txt_No_Penjualan.Text.Trim}'
					order by Tanggal DESC, Jam DESC
				"
                Using Dr = Open(SQL)
                    If Dr.Read Then
                        Txt_No_Penjualan.Text = Dr("No_Faktur")
                        Txt_No_DO.Focus()
                    Else
                        MessageBox.Show("No Penjualan tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Txt_No_Penjualan.Text = ""
                        Txt_No_Penjualan.Focus()
                    End If

                    Me.Size = New Size(720, 340)
                    Lv_Penjualan.Visible = False
                    Lv_Penjualan.Location = New Point(720, 154)
                End Using
            Else
                Txt_No_DO.Focus()
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_No_Penjualan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_No_Penjualan.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_No_Penjualan.Text.Trim.Length = 0 Then Txt_No_Penjualan.Focus()
            Txt_No_Penjualan_Leave(Txt_No_Penjualan, e)


            Me.Size = New Size(720, 340)
            Lv_Penjualan.Visible = False
            Lv_Penjualan.Location = New Point(720, 154)

            'Txt_KdKategori.Focus()
        End If
    End Sub

    Private Sub Txt_No_Penjualan_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_No_Penjualan.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Penjualan.Focus()
    End Sub

    Private Sub Lv_Penjualan_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Penjualan.DoubleClick
        If Lv_Penjualan.Items.Count = 0 Or Lv_Penjualan.FocusedItem.Index = -1 Then Exit Sub

        Dim No_Penjualan As String = Lv_Penjualan.FocusedItem.SubItems(0).Text

        Switch_Auto_Complete = True
        Txt_No_Penjualan.Text = No_Penjualan
        Switch_Auto_Complete = False

        Me.Size = New Size(720, 340)
        Lv_Penjualan.Visible = False
        Lv_Penjualan.Location = New Point(720, 154)

        Txt_No_DO.Focus()
    End Sub

    Private Sub Lv_Penjualan_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Penjualan.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_Penjualan_DoubleClick(Lv_Penjualan, e)
        End If
    End Sub

    Private Sub Txt_No_DO_TextChanged(sender As Object, e As EventArgs) Handles Txt_No_DO.TextChanged
        If Switch_Auto_Complete Then Exit Sub

        If Txt_No_DO.Text.Trim.Length = 0 Then
            Me.Size = New Size(720, 340)
            Lv_DO.Visible = False
            Lv_DO.Location = New Point(720, 180)
            Txt_No_DO.Text = ""
            Txt_Keterangan_DO.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(720, 435)
            Lv_DO.Location = New Point(126, 180)
            Lv_DO.Visible = True
        End If

        Try
            OpenConn()

            Lv_DO.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_DO.Items.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)

            SQL = $"
				select No_DO, Tanggal, Keterangan
                from DO_New
                where Kode_Perusahaan = '{KodePerusahaan}'
                and Status is null
                and No_DO like '%{Txt_No_DO.Text.Trim}%'
                order by tanggal DESC, Jam DESC
			"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lv = Lv_DO.Items.Add(Dr("No_DO"))
                    Lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
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

    Private Sub Txt_No_DO_Leave(sender As Object, e As EventArgs) Handles Txt_No_DO.Leave
        If Txt_No_DO.Text.Trim.Length = 0 Then Exit Sub
        If Lv_DO.Focused = True Then Exit Sub

        Try
            OpenConn()

            If Not Txt_No_DO.Text.ToUpper = OpsiSeluruh.ToUpper Then

                SQL = $"
				    select No_DO, Tanggal, Keterangan
                    from DO_New
                    where Kode_Perusahaan = '{KodePerusahaan}'
                    and Status is null
                    and No_DO = '{Txt_No_DO.Text.Trim}'
                    order by tanggal DESC, Jam DESC
			    "
                Using Dr = Open(SQL)
                    If Dr.Read Then
                        Txt_No_DO.Text = Dr("No_DO")
                        Txt_Keterangan_DO.Text = Dr("Keterangan")
                        Txt_Kd_Customer.Focus()
                    Else
                        MessageBox.Show("No DO tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Txt_No_DO.Text = ""
                        Txt_Keterangan_DO.Text = ""
                        Txt_No_DO.Focus()
                    End If

                    Me.Size = New Size(720, 340)
                    Lv_DO.Visible = False
                    Lv_DO.Location = New Point(720, 180)
                End Using
            Else
                Txt_Kd_Customer.Focus()
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_No_DO_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_No_DO.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_No_DO.Text.Trim.Length = 0 Then Txt_No_DO.Focus()
            Txt_No_DO_Leave(Txt_No_DO, e)


            Me.Size = New Size(720, 340)
            Lv_DO.Visible = False
            Lv_DO.Location = New Point(720, 180)

            'Txt_KdKategori.Focus()
        End If
    End Sub

    Private Sub Txt_No_DO_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_No_DO.KeyDown
        If e.KeyCode = Keys.Down Then Lv_DO.Focus()
    End Sub

    Private Sub Lv_DO_DoubleClick(sender As Object, e As EventArgs) Handles Lv_DO.DoubleClick
        If Lv_DO.Items.Count = 0 Or Lv_DO.FocusedItem.Index = -1 Then Exit Sub

        Dim No_DO As String = Lv_DO.FocusedItem.SubItems(0).Text
        Dim Keterangan As String = Lv_DO.FocusedItem.SubItems(2).Text

        Switch_Auto_Complete = True
        Txt_No_DO.Text = No_DO
        Txt_Keterangan_DO.Text = Keterangan
        Switch_Auto_Complete = False

        Me.Size = New Size(720, 340)
        Lv_DO.Visible = False
        Lv_DO.Location = New Point(720, 180)

        Txt_Kd_Customer.Focus()
    End Sub

    Private Sub Lv_DO_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_DO.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_DO_DoubleClick(Lv_DO, e)
        End If
    End Sub

    Private Sub Txt_Kd_Customer_TextChanged(sender As Object, e As EventArgs) Handles Txt_Kd_Customer.TextChanged
        If Switch_Auto_Complete Then Exit Sub

        If Txt_Kd_Customer.Text.Trim.Length = 0 Then
            Me.Size = New Size(720, 340)
            Lv_Customer.Visible = False
            Lv_Customer.Location = New Point(720, 207)
            Txt_Kd_Customer.Text = ""
            Txt_Ket_Customer.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(720, 460)
            Lv_Customer.Location = New Point(126, 207)
            Lv_Customer.Visible = True
        End If

        Try
            OpenConn()

            Lv_Customer.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_Customer.Items.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)

            SQL = $"
				select Kode_Customer, Nama
                from Customers
                where Kode_Perusahaan = '{KodePerusahaan}'
                and Nama like '%{Txt_Kd_Customer.Text}%'
                order by Kode_Customer
			"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lv = Lv_Customer.Items.Add(Dr("Kode_Customer"))
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

    Private Sub Txt_Kd_Customer_Leave(sender As Object, e As EventArgs) Handles Txt_Kd_Customer.Leave
        If Txt_Kd_Customer.Text.Trim.Length = 0 Then Exit Sub
        If Lv_Customer.Focused = True Then Exit Sub

        Try
            OpenConn()

            If Not Txt_Kd_Customer.Text.ToUpper = OpsiSeluruh.ToUpper Then

                SQL = $"
				    select Kode_Customer, Nama
                    from Customers
                    where Kode_Perusahaan = '{KodePerusahaan}'
                    and Kode_Customer = '{Txt_Kd_Customer.Text}'
                    order by Kode_Customer
			    "
                Using Dr = Open(SQL)
                    If Dr.Read Then
                        Txt_Kd_Customer.Text = Dr("Kode_Customer")
                        Txt_Ket_Customer.Text = Dr("Nama")

                        If Cmb_Jenis_Laporan.SelectedIndex = 0 Then
                            BtnCetak.Focus()
                        Else
                            Txt_KdBarang.Focus()
                        End If

                    Else
                        MessageBox.Show("Customer tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Txt_Kd_Customer.Text = ""
                        Txt_Ket_Customer.Text = ""
                        Txt_Kd_Customer.Focus()
                    End If

                    Me.Size = New Size(720, 340)
                    Lv_Customer.Visible = False
                    Lv_Customer.Location = New Point(720, 207)
                End Using
            Else
                If Cmb_Jenis_Laporan.SelectedIndex = 0 Then
                    BtnCetak.Focus()
                Else
                    Txt_KdBarang.Focus()
                End If
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_Kd_Customer_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Kd_Customer.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_Kd_Customer.Text.Trim.Length = 0 Then Txt_Kd_Customer.Focus()
            Txt_Kd_Customer_Leave(Txt_Kd_Customer, e)


            Me.Size = New Size(720, 340)
            Lv_Customer.Visible = False
            Lv_Customer.Location = New Point(720, 207)

            'Txt_KdKategori.Focus()
        End If
    End Sub

    Private Sub Txt_Kd_Customer_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_Kd_Customer.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Customer.Focus()
    End Sub

    Private Sub Lv_Customer_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Customer.DoubleClick
        If Lv_Customer.Items.Count = 0 Or Lv_Customer.FocusedItem.Index = -1 Then Exit Sub

        Dim Kd_Cutomer As String = Lv_Customer.FocusedItem.SubItems(0).Text
        Dim Keterangan As String = Lv_Customer.FocusedItem.SubItems(1).Text

        Switch_Auto_Complete = True
        Txt_Kd_Customer.Text = Kd_Cutomer
        Txt_Ket_Customer.Text = Keterangan
        Switch_Auto_Complete = False

        Me.Size = New Size(720, 340)
        Lv_Customer.Visible = False
        Lv_Customer.Location = New Point(720, 207)

        If Cmb_Jenis_Laporan.SelectedIndex = 0 Then
            BtnCetak.Focus()
        Else
            Txt_KdBarang.Focus()
        End If
    End Sub

    Private Sub Lv_Customer_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Customer.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_Customer_DoubleClick(Lv_Customer, e)
        End If
    End Sub

    Private Sub Txt_KdBarang_TextChanged(sender As Object, e As EventArgs) Handles Txt_KdBarang.TextChanged
        If Switch_Auto_Complete Then Exit Sub

        If Txt_KdBarang.Text.Trim.Length = 0 Then
            Me.Size = New Size(720, 340)
            Lv_Barang.Visible = False
            Lv_Barang.Location = New Point(720, 233)
            Txt_KdBarang.Text = ""
            Txt_NmBarang.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(720, 485)
            Lv_Barang.Location = New Point(126, 233)
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
                from barang a, EMI_Group_Jenis b
                where a.Kode_Perusahaan = b.Kode_Perusahaan
                and a.Kode_Perusahaan = '{KodePerusahaan}'
                and a.Kode_Barang like '%{Txt_KdBarang.Text}%'
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

    Private Sub Txt_KdBarang_Leave(sender As Object, e As EventArgs) Handles Txt_KdBarang.Leave
        If Txt_KdBarang.Text.Trim.Length = 0 Then Exit Sub
        If Lv_Barang.Focused = True Then Exit Sub

        Try
            OpenConn()

            If Not Txt_KdBarang.Text.ToUpper = OpsiSeluruh.ToUpper Then

                SQL = $"
				    select Distinct a.Kode_Barang, a.Nama
                    from barang a, EMI_Group_Jenis b
                    where a.Kode_Perusahaan = b.Kode_Perusahaan
                    and a.Kode_Perusahaan = '{KodePerusahaan}'
                    and a.Kode_Barang = '{Txt_KdBarang.Text}'
                    order by Kode_Barang
			    "
                Using Dr = Open(SQL)
                    If Dr.Read Then
                        Txt_KdBarang.Text = Dr("Kode_Barang")
                        Txt_NmBarang.Text = Dr("Nama")
                        BtnCetak.Focus()
                    Else
                        MessageBox.Show("Barang tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Txt_KdBarang.Text = ""
                        Txt_NmBarang.Text = ""
                        Txt_KdBarang.Focus()
                    End If

                    Me.Size = New Size(720, 340)
                    Lv_Barang.Visible = False
                    Lv_Barang.Location = New Point(720, 233)
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

    Private Sub Txt_KdBarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_KdBarang.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_KdBarang.Text.Trim.Length = 0 Then Txt_KdBarang.Focus()
            Txt_KdBarang_Leave(Txt_KdBarang, e)


            Me.Size = New Size(720, 340)
            Lv_Barang.Visible = False
            Lv_Barang.Location = New Point(720, 233)

            'Txt_KdKategori.Focus()
        End If
    End Sub

    Private Sub Txt_KdBarang_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_KdBarang.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Barang.Focus()
    End Sub

    Private Sub Txt_NmBarang_TextChanged(sender As Object, e As EventArgs) Handles Txt_NmBarang.TextChanged
        If Switch_Auto_Complete Then Exit Sub

        If Txt_NmBarang.Text.Trim.Length = 0 Then
            Me.Size = New Size(720, 340)
            Lv_Barang.Visible = False
            Lv_Barang.Location = New Point(720, 233)
            Txt_KdBarang.Text = ""
            Txt_NmBarang.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(720, 485)
            Lv_Barang.Location = New Point(126, 233)
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
                from barang a, EMI_Group_Jenis b
                where a.Kode_Perusahaan = b.Kode_Perusahaan
                and a.Kode_Perusahaan = '{KodePerusahaan}'
                and a.Nama like '%{Txt_NmBarang.Text}%'
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

    Private Sub Txt_NmBarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_NmBarang.KeyPress
        If e.KeyChar = Chr(13) Then
            Txt_KdBarang_Leave(Txt_NmBarang, e)

            Me.Size = New Size(720, 340)
            Lv_Barang.Visible = False
            Lv_Barang.Location = New Point(720, 233)

            'Txt_KdKategori.Focus()
        End If
    End Sub

    Private Sub Txt_NmBarang_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_NmBarang.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Barang.Focus()
    End Sub

    Private Sub Lv_Barang_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Barang.DoubleClick
        If Lv_Barang.Items.Count = 0 Or Lv_Barang.FocusedItem.Index = -1 Then Exit Sub

        Dim KdBarng As String = Lv_Barang.FocusedItem.SubItems(0).Text
        Dim NmBarang As String = Lv_Barang.FocusedItem.SubItems(1).Text

        Switch_Auto_Complete = True
        Txt_KdBarang.Text = KdBarng
        Txt_NmBarang.Text = NmBarang
        Switch_Auto_Complete = False

        Me.Size = New Size(720, 340)
        Lv_Barang.Visible = False
        Lv_Barang.Location = New Point(720, 233)

        BtnCetak.Focus()
    End Sub

    Private Sub Lv_Barang_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Barang.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_Barang_DoubleClick(Lv_Barang, e)
        End If
    End Sub

    Private Sub BtnExit_Click(sender As Object, e As EventArgs) Handles BtnExit.Click
        Me.Close()
    End Sub

    Private Sub BtnCetak_Click(sender As Object, e As EventArgs) Handles BtnCetak.Click
        If Cmb_Tanggal.SelectedIndex = -1 Then
            MessageBox.Show("Pilih Dahulu Jenis Tanggal!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Tanggal.DroppedDown = True
            Cmb_Tanggal.Focus() : Exit Sub
        ElseIf Tgl1.Value > Tgl2.Value Then
            MessageBox.Show("Periode I tidak boleh lebih dari periode II!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Tgl1.Value = Now.Date : Tgl2.Value = Now.Date
            Tgl1.Focus() : Exit Sub
        ElseIf Cmb_Jenis_Laporan.SelectedIndex = -1 Then
            MessageBox.Show("Jenis Laporan Harus Dipilih!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Jenis_Laporan.DroppedDown = True
            Cmb_Jenis_Laporan.Focus() : Exit Sub
        ElseIf Txt_No_Penjualan.Text.Trim.Length = 0 Then
            MessageBox.Show("No Penjualan harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_No_Penjualan.Focus() : Exit Sub
        ElseIf Txt_No_DO.Text.Trim.Length = 0 Then
            MessageBox.Show("No DO harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_No_DO.Focus() : Exit Sub
        ElseIf Txt_Kd_Customer.Text.Trim.Length = 0 Then
            MessageBox.Show("Kode Customers harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_Kd_Customer.Focus() : Exit Sub
        End If

        If Cmb_Jenis_Laporan.SelectedIndex <> 0 Then
            If Txt_KdBarang.Text.Trim.Length = 0 Then
                MessageBox.Show("Barang harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Txt_KdBarang.Focus() : Exit Sub
            End If
        End If

        Try
            OpenConn()

            Dim SF As String = ""

            If Cmb_Jenis_Laporan.SelectedIndex = 0 Then

                SQL = "select Kode_Perusahaan from N_EMI_View_Laporan_Penjualan_Rekap "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "

                If Cmb_Tanggal.SelectedIndex <> -1 Then
                    SQL = SQL & "and " & arrTanggal(Cmb_Tanggal.SelectedIndex) & " between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "
                End If

                'SQL = SQL & "and Tanggal between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "

                SF = "{N_EMI_View_Laporan_Penjualan_Rekap.Kode_Perusahaan} = '" & KodePerusahaan & "' "
                SF = SF & "and {N_EMI_View_Laporan_Penjualan_Rekap." & arrTanggalSF(Cmb_Tanggal.SelectedIndex) & "} >= #" & Format(Tgl1.Value, "yyyy-MM-dd") & "# and "
                SF = SF & "{N_EMI_View_Laporan_Penjualan_Rekap." & arrTanggalSF(Cmb_Tanggal.SelectedIndex) & "} <= #" & Format(Tgl2.Value, "yyyy-MM-dd") & "# "

                If Not Txt_No_Penjualan.Text.ToUpper = OpsiSeluruh.ToUpper Then
                    SQL = SQL & "and No_Penjualan = '" & Txt_No_Penjualan.Text & "' "
                    SF = SF & "And {N_EMI_View_Laporan_Penjualan_Rekap.No_Penjualan} = '" & Txt_No_Penjualan.Text & "'"
                End If

                If Not Txt_No_DO.Text.ToUpper = OpsiSeluruh.ToUpper Then
                    SQL = SQL & "and No_DO = '" & Txt_No_DO.Text & "' "
                    SF = SF & "And {N_EMI_View_Laporan_Penjualan_Rekap.No_DO} = '" & Txt_No_DO.Text & "'"
                End If

                If Not Txt_Kd_Customer.Text.ToUpper = OpsiSeluruh.ToUpper Then
                    SQL = SQL & "and Kode_Customer = '" & Txt_Kd_Customer.Text & "' "
                    SF = SF & "And {N_EMI_View_Laporan_Penjualan_Rekap.Kode_Customer} = '" & Txt_Kd_Customer.Text & "'"
                End If

                'If Not Txt_KdBarang.Text.ToUpper = OpsiSeluruh.ToUpper Then
                '    SQL = SQL & "and Kode_Barang = '" & Txt_KdBarang.Text & "' "
                '    SF = SF & "And {N_EMI_View_Laporan_Penjualan_Rekap.Kode_Barang} = '" & Txt_KdBarang.Text & "'"
                'End If

                Using DS = BindingTrans(SQL)
                    With DS.Tables("MyTable")
                        If .Rows.Count <> 0 Then

                            Dim CrDoc As New N_EMI_CR_Laporan_Penjualan_Rekap

                            CrDoc.SetDataSource(DS)
                            CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                            CrDoc.SummaryInfo.ReportTitle = "Periode : " & Format(Tgl1.Value, "dd/MMM/yyyy") & " s/d " &
                                                                                Format(Tgl2.Value, "dd/MMM/yyyy")
                            CrDoc.RecordSelectionFormula = SF

                            With A_Place_For_Printing2
                                .Text = "Laporan Penjualan Rekap"
                                .CrystalReportViewer1.ReportSource = CrDoc
                                .CrystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
                                .Refresh()
                                .Show()
                            End With

                        Else

                            CloseConn()
                            MessageBox.Show("Data Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub

                        End If
                    End With
                End Using

            ElseIf Cmb_Jenis_Laporan.SelectedIndex = 1 Then

                SQL = "select Kode_Perusahaan from N_EMI_View_Laporan_Penjualan_Detail "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "

                If Cmb_Tanggal.SelectedIndex <> -1 Then
                    SQL = SQL & "and " & arrTanggal(Cmb_Tanggal.SelectedIndex) & " between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "
                End If

                'SQL = SQL & "and Tanggal between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "

                SF = "{N_EMI_View_Laporan_Penjualan_Detail.Kode_Perusahaan} = '" & KodePerusahaan & "' "
                SF = SF & "and {N_EMI_View_Laporan_Penjualan_Detail." & arrTanggalSF(Cmb_Tanggal.SelectedIndex) & "} >= #" & Format(Tgl1.Value, "yyyy-MM-dd") & "# and "
                SF = SF & "{N_EMI_View_Laporan_Penjualan_Detail." & arrTanggalSF(Cmb_Tanggal.SelectedIndex) & "} <= #" & Format(Tgl2.Value, "yyyy-MM-dd") & "# "

                If Not Txt_No_Penjualan.Text.ToUpper = OpsiSeluruh.ToUpper Then
                    SQL = SQL & "and No_Penjualan = '" & Txt_No_Penjualan.Text & "' "
                    SF = SF & "And {N_EMI_View_Laporan_Penjualan_Detail.No_Penjualan} = '" & Txt_No_Penjualan.Text & "'"
                End If

                If Not Txt_No_DO.Text.ToUpper = OpsiSeluruh.ToUpper Then
                    SQL = SQL & "and No_DO = '" & Txt_No_DO.Text & "' "
                    SF = SF & "And {N_EMI_View_Laporan_Penjualan_Detail.No_DO} = '" & Txt_No_DO.Text & "'"
                End If

                If Not Txt_Kd_Customer.Text.ToUpper = OpsiSeluruh.ToUpper Then
                    SQL = SQL & "and Kode_Customer = '" & Txt_Kd_Customer.Text & "' "
                    SF = SF & "And {N_EMI_View_Laporan_Penjualan_Detail.Kode_Customer} = '" & Txt_Kd_Customer.Text & "'"
                End If

                If Not Txt_KdBarang.Text.ToUpper = OpsiSeluruh.ToUpper Then
                    SQL = SQL & "and Kode_Barang = '" & Txt_KdBarang.Text & "' "
                    SF = SF & "And {N_EMI_View_Laporan_Penjualan_Detail.Kode_Barang} = '" & Txt_KdBarang.Text & "'"
                End If

                Using DS = BindingTrans(SQL)
                    With DS.Tables("MyTable")
                        If .Rows.Count <> 0 Then

                            Dim CrDoc As New N_EMI_CR_Laporan_Penjualan_Detail

                            CrDoc.SetDataSource(DS)
                            CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                            CrDoc.SummaryInfo.ReportTitle = "Periode : " & Format(Tgl1.Value, "dd/MMM/yyyy") & " s/d " &
                                                                                Format(Tgl2.Value, "dd/MMM/yyyy")
                            CrDoc.RecordSelectionFormula = SF

                            With A_Place_For_Printing2
                                .Text = "Laporan Penjualan Detail"
                                .CrystalReportViewer1.ReportSource = CrDoc
                                .CrystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
                                .Refresh()
                                .Show()
                            End With

                        Else

                            CloseConn()
                            MessageBox.Show("Data Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub

                        End If
                    End With
                End Using


            End If


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


    End Sub

    Private Sub Cmb_Tanggal_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Tanggal.KeyPress
        If e.KeyChar = Chr(13) Then Tgl1.Focus()
    End Sub

    Private Sub Tgl1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl1.KeyPress
        If e.KeyChar = Chr(13) Then Tgl2.Focus()
    End Sub

    Private Sub Tgl2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl2.KeyPress
        If e.KeyChar = Chr(13) Then
            Cmb_Jenis_Laporan.DroppedDown = True
            Cmb_Jenis_Laporan.Focus()
        End If
    End Sub

    Private Sub Cmb_Jenis_Laporan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Jenis_Laporan.SelectedIndexChanged
        Switch_Auto_Complete = True
        If Cmb_Jenis_Laporan.SelectedIndex = 0 Then
            Txt_KdBarang.Enabled = False
            Txt_NmBarang.Enabled = False
            Txt_KdBarang.Text = ""
            Txt_NmBarang.Text = ""
        Else
            Txt_KdBarang.Enabled = True
            Txt_NmBarang.Enabled = True
            Txt_KdBarang.Text = OpsiSeluruh
            Txt_NmBarang.Text = OpsiSeluruh
        End If


        Switch_Auto_Complete = False
    End Sub

    Private Sub Cmb_Jenis_Laporan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Jenis_Laporan.KeyPress
        If e.KeyChar = Chr(13) Then Txt_No_Penjualan.Focus()
    End Sub
End Class
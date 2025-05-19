Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button

Public Class Laporan_Mutasi_Bahan
    Dim arrId_Group As New ArrayList
    Dim arrGetLokasi, arrGetIdGroup As String
    Private Sub Laporan_Mutasi_Bahan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Lv_DetBarang.Location = New Point(141, 195)
        Me.Size = New Size(626, 309)
        Lv_DetBarang.Visible = False
        Tgl1.Value = Now.Date : Tgl2.Value = Now.Date : Tgl1.Focus()

        Try
            OpenConn()

            ComboBox1.Items.Clear() : arrId_Group.Clear()
            ComboBox1.Items.Add("Seluruh") : arrId_Group.Add("Seluruh")
            SQL = "select a.Kode_Group_Jenis,a.Id_Group_Jenis from EMI_Group_Jenis a, EMI_Pengeluaran_Barang_Roles b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Group_Jenis = b.Nama_Role "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and b.UserID = '" & UserID & "' "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    ComboBox1.Items.Add(dr("Kode_Group_Jenis"))
                    arrId_Group.Add(dr("Id_Group_Jenis"))
                Loop
            End Using
            ComboBox1.SelectedIndex = 0

            CmbSO_Asal.Items.Clear()
            CmbSO_Asal.Items.Add("Seluruh")
            SQL = "select Kode_Stock_Owner from Stock_Owner_Gudang where "
            SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    CmbSO_Asal.Items.Add(dr("Kode_Stock_Owner"))
                Loop
            End Using

            CmbSO_Asal.SelectedIndex = 0

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Tgl1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl1.KeyPress
        If e.KeyChar = Chr(13) Then Tgl2.Focus()
    End Sub

    Private Sub Tgl2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl2.KeyPress
        If e.KeyChar = Chr(13) Then TxtKd_Barang.Focus()
    End Sub

    Private Sub BtnExit_Click(sender As Object, e As EventArgs) Handles BtnExit.Click
        Me.Close()
    End Sub

    Private Sub TxtKd_Barang_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtKd_Barang.KeyDown
        If e.KeyCode = Keys.Down Then
            If Lv_DetBarang.Items.Count = 0 Then Exit Sub
            Lv_DetBarang.Focus()
        End If
    End Sub

    Private Sub TxtKd_Barang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtKd_Barang.KeyPress
        If e.KeyChar = Chr(13) Then BtnCetak.Focus()
    End Sub

    Private Sub TxtKd_Barang_Leave(sender As Object, e As EventArgs) Handles TxtKd_Barang.Leave
        If TxtKd_Barang.Text.Trim.Length = 0 Then Exit Sub
        If Lv_DetBarang.Focused = True Then Exit Sub

        Try
            OpenConn()

            If TxtKd_Barang.Text = "Seluruh" Then
                Txt_NmBarang.Text = "Seluruh"
                BtnCetak.Focus()
                Lv_DetBarang.Visible = False
            Else
                SQL = "select Kode_Barang,Nama From barang where Kode_Perusahaan = '" & KodePerusahaan & "'  "
                SQL = SQL & "and kode_barang = '" & TxtKd_Barang.Text.Trim & "' "
                SQL = SQL & "group by kode_barang,nama"
                Using dr = OpenTrans(SQL)
                    If dr.Read Then
                        Txt_NmBarang.Text = dr("nama")
                        'BtnCetak.Location = New Point(415, 175)
                        'BtnExit.Location = New Point(498, 175)
                        BtnCetak.Focus()
                        Lv_DetBarang.Visible = False
                    Else
                        TxtKd_Barang.Text = ""
                        Txt_NmBarang.Text = ""
                        Lv_DetBarang.Visible = False
                        'Lv_DetBarang.Location = New Point(803, 258)
                        Lv_DetBarang.Visible = False

                        Me.Size = New Point(626, 309)
                    End If
                End Using
            End If


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub TxtKd_Barang_TextChanged(sender As Object, e As EventArgs) Handles TxtKd_Barang.TextChanged
        If TxtKd_Barang.Text.Length >= 1 Then

            If TxtKd_Barang.Text.Trim.Length = 0 Then
                TxtKd_Barang.Visible = False : Exit Sub
            Else
                Lv_DetBarang.Visible = True
            End If

            If TxtKd_Barang.Text.Trim.Length = 0 Then
                Lv_DetBarang.Visible = False : Exit Sub
            Else
                Lv_DetBarang.Visible = True
            End If

            Lv_DetBarang.Location = New Point(141, 195)
            Lv_DetBarang.Visible = True

            'BtnCetak.Location = New Point(399, 281)
            'BtnExit.Location = New Point(489, 281)

            Me.Size = New Point(625, 391)

            Try
                OpenConn()

                Lv_DetBarang.Items.Clear()
                Txt_NmBarang.Text = ""

                Dim lv1 As New ListViewItem

                lv1 = Lv_DetBarang.Items.Add("Seluruh")
                lv1.SubItems.Add("Seluruh")

                SQL = "select Kode_Barang,Nama From barang where Kode_Perusahaan = '" & KodePerusahaan & "'  "
                SQL = SQL & "and kode_barang like '%" & TxtKd_Barang.Text.Trim & "%' "

                If CmbSO_Asal.SelectedIndex <> 0 Then
                    SQL = SQL & "and Kode_Stock_Owner = '" & CmbSO_Asal.Text & "' "
                Else
                    SQL = SQL & "group by kode_barang,nama"
                End If
                Using Dr = OpenTrans(SQL)
                    Do While Dr.Read
                        Dim Lv As New ListViewItem
                        Lv = Lv_DetBarang.Items.Add(Dr("kode_barang"))
                        Lv.SubItems.Add(Dr("nama"))

                    Loop
                End Using

                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        Else
            Lv_DetBarang.Visible = False
            'Lv_DetBarang.Location = New Point(803, 258)
            Lv_DetBarang.Visible = False

            'BtnCetak.Location = New Point(415, 175)
            'BtnExit.Location = New Point(498, 175)

            Me.Size = New Point(626, 309)

        End If
    End Sub

    Private Sub Txt_NmBarang_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_NmBarang.KeyDown
        If e.KeyCode = Keys.Down Then
            If Lv_DetBarang.Items.Count = 0 Then Exit Sub
            Lv_DetBarang.Focus()
        End If
    End Sub

    Private Sub Txt_NmBarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_NmBarang.KeyPress
        If e.KeyChar = Chr(13) Then BtnCetak.Focus()
    End Sub

    Private Sub Txt_NmBarang_TextChanged(sender As Object, e As EventArgs) Handles Txt_NmBarang.TextChanged
        If Txt_NmBarang.Text.Length >= 1 Then

            If Txt_NmBarang.Text.Trim.Length = 0 Then
                Txt_NmBarang.Visible = False : Exit Sub
            Else
                Lv_DetBarang.Visible = True
            End If

            If Txt_NmBarang.Text.Trim.Length = 0 Then
                Lv_DetBarang.Visible = False : Exit Sub
            Else
                Lv_DetBarang.Visible = True
            End If

            Lv_DetBarang.Location = New Point(141, 195)
            Lv_DetBarang.Visible = True

            'BtnCetak.Location = New Point(399, 281)
            'BtnExit.Location = New Point(489, 281)

            Me.Size = New Point(625, 391)

            Try
                OpenConn()

                Lv_DetBarang.Items.Clear()
                Dim lv1 As New ListViewItem

                lv1 = Lv_DetBarang.Items.Add("Seluruh")
                lv1.SubItems.Add("Seluruh")

                SQL = "select Kode_Barang,Nama From barang where Kode_Perusahaan = '" & KodePerusahaan & "'  "
                SQL = SQL & "and nama like '%" & Txt_NmBarang.Text.Trim & "%' "
                If CmbSO_Asal.SelectedIndex <> 0 Then
                    SQL = SQL & "and Kode_Stock_Owner = '" & CmbSO_Asal.Text & "' "
                Else
                    SQL = SQL & "group by kode_barang,nama"
                End If
                Using Dr = OpenTrans(SQL)
                    Do While Dr.Read
                        Dim Lv As New ListViewItem
                        Lv = Lv_DetBarang.Items.Add(Dr("kode_barang"))
                        Lv.SubItems.Add(Dr("nama"))
                    Loop
                End Using

                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        Else
            Lv_DetBarang.Visible = False
            'Lv_DetBarang.Location = New Point(803, 258)
            Lv_DetBarang.Visible = False

            'BtnCetak.Location = New Point(415, 175)
            'BtnExit.Location = New Point(498, 175)

            Me.Size = New Point(626, 309)

        End If
    End Sub

    Private Sub Lv_DetBarang_DoubleClick(sender As Object, e As EventArgs) Handles Lv_DetBarang.DoubleClick
        If Lv_DetBarang.Items.Count = 0 Then Exit Sub

        TxtKd_Barang.Text = Lv_DetBarang.FocusedItem.SubItems(0).Text
        TxtKd_Barang.Focus() : BtnCetak.Focus()
        Lv_DetBarang.Visible = False


        Me.Size = New Point(626, 309)
    End Sub

    Private Sub BtnCetak_Click(sender As Object, e As EventArgs) Handles BtnCetak.Click
        If Tgl1.Value > Tgl2.Value Then
            MessageBox.Show("Periode I tidak boleh lebih dari periode II!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            'Tgl1.Value = Now.Date : Tgl2.Value = Now.Date
            Exit Sub
        End If

        Try
            OpenConn()

            Dim Auth As String = ""
            SQL = "select Auth_SP from Users where kode_perusahaan = '" & KodePerusahaan & "' and UserID = '" & UserID & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Auth = Dr("Auth_SP")
                Else
                    Dr.Close()
                    CloseConn()
                    MessageBox.Show("User Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            SQL = "EXEC  EMI_Mutasi_Bahan "
            SQL = SQL & "@kode_perusahaan = '" & KodePerusahaan & "', "
            SQL = SQL & "@tanggal_awal = '" & Format(Tgl1.Value, "yyyy-MM-dd") & "', "
            SQL = SQL & "@tanggal_akhir = '" & Format(Tgl2.Value, "yyyy-MM-dd") & "', "

            If CmbSO_Asal.SelectedIndex = 0 Then
                SQL = SQL & "@ArrLokasi = '"

                Dim list_kota As String = ""

                For x As Integer = 1 To CmbSO_Asal.Items.Count - 1
                    list_kota = list_kota & "" & CmbSO_Asal.Items(x).ToString & ", "
                Next

                list_kota = Strings.Left(list_kota, Len(list_kota) - 2)

                SQL = SQL & list_kota & "', "

                arrGetLokasi = list_kota
            End If

            If ComboBox1.SelectedIndex = 0 Then
                SQL = SQL & "@ArrGroupJenis = '"

                Dim list_id As String = ""

                For x As Integer = 1 To ComboBox1.Items.Count - 1
                    list_id = list_id & "" & arrId_Group.Item(x) & ", "
                Next

                list_id = Strings.Left(list_id, Len(list_id) - 2)

                SQL = SQL & list_id & "', "

                arrGetIdGroup = list_id
            End If

            Dim list_brg As String = ""
            If TxtKd_Barang.Text.ToUpper = "SELURUH" Then
                list_brg = ""
                SQL = SQL & "@ArrKd_Barang = '', "
            Else
                list_brg = TxtKd_Barang.Text
                SQL = SQL & "@ArrKd_Barang = '" & TxtKd_Barang.Text & "', "
            End If

            SQL = SQL & "@Auth_SP = '" & Auth & "', "
            SQL = SQL & "@UserID = '" & UserID & "' "
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then
                    Dim CrDoc As New Rpt_Laporan_Mutasi_Bahan

                    With A_Place_For_Printing
                        CrDoc.SetDataSource(Ds)
                        CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        CrDoc.SetParameterValue("@kode_perusahaan", KodePerusahaan)
                        CrDoc.SetParameterValue("@tanggal_awal", Format(Tgl1.Value, "yyyy-MM-dd"))
                        CrDoc.SetParameterValue("@tanggal_akhir", Format(Tgl2.Value, "yyyy-MM-dd"))
                        CrDoc.SetParameterValue("@ArrLokasi", arrGetLokasi)
                        CrDoc.SetParameterValue("@ArrGroupJenis", arrGetIdGroup)
                        CrDoc.SetParameterValue("@ArrKd_Barang", list_brg)
                        CrDoc.SetParameterValue("@Auth_SP", Auth)
                        CrDoc.SetParameterValue("@UserID", UserID)


                        CrDoc.SummaryInfo.ReportTitle = "Periode: " & Format(Tgl1.Value, "yyyy-MM-dd") & " s/d " & Format(Tgl2.Value, "yyyy-MM-dd") & Chr(13) & " "

                        .Text = "Laporan Detail Uang Masuk Global"
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
End Class
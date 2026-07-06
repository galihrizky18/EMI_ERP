Imports System.Web.UI.WebControls
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button

Public Class N_EMI_Check_In_Out_Shift_Packing
    Private Sub N_EMI_Check_In_Out_Shift_Packing_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub get_no_transaksi()
        Dim FPro_Results As String = "TC"
        TextBox1.Text = FPro_Results & "-" & Format(tgl_skg, "MM/yy") & "-" &
                                                      General_Class.Get_Last_Number2("N_EMI_Transaksi_Packing_Check_In_Out", "No_Transaksi", 5,
                                                      "Kode_perusahaan", KodePerusahaan,
                                                      "And", "substring(No_Transaksi,1," & Len(FPro_Results) + 6 & ")", FPro_Results & "-" & Format(tgl_skg, "MM/yy"))
    End Sub

    Public Sub kosong()
        TextBox2.Text = ""
        TextBox2.ReadOnly = False
        TextBox3.Text = ""
        ListView1.Items.Clear()

        ComboBox2.Items.Clear()
        ComboBox2.Items.Add("Shift 1")
        ComboBox2.Items.Add("Shift 2")
        ComboBox2.Items.Add("Shift 3")
        ComboBox2.SelectedIndex = -1
        TextBox2.Focus()

        Try
            OpenConn()

            If Button1.Text = "Shift In" Then
                get_no_transaksi()
            ElseIf Button1.Text = "Shift Out" Then
                Dim a As Integer = 0

                SQL = "select c.Kode_Karyawan, c.Nama "
                SQL = SQL & "from N_EMI_Transaksi_Packing_Check_In_Out a, N_EMI_Transaksi_Packing_Check_In_Out_Detail b, Karyawan c "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Transaksi "
                SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Karyawan = c.Kode_Karyawan "
                SQL = SQL & "and a.Status is null and a.Tanggal_Out is null "
                SQL = SQL & "and b.Kode_Perusahaan = '" & KodePerusahaan & "' and b.No_Transaksi = '" & TextBox1.Text & "' "
                Using dr = OpenTrans(SQL)
                    Do While dr.Read
                        a = a + 1

                        Dim lvw As ListViewItem
                        lvw = ListView1.Items.Add(a)
                        lvw.SubItems.Add(dr("Kode_Karyawan"))
                        lvw.SubItems.Add(dr("Nama"))
                    Loop
                End Using
            Else
                CloseConn()
                MessageBox.Show("Terjadi kesalahan !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        get_jam()
        If TextBox1.Text.Trim.Length = 0 Then
            MessageBox.Show("Group packing harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox1.Focus() : Exit Sub
        ElseIf ComboBox2.Text.Trim.Length = 0 Then
            MessageBox.Show("Shif harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox2.Focus() : Exit Sub
        ElseIf TextBox2.Text.Trim.Length = 0 Then
            MessageBox.Show("Line harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox2.Focus() : Exit Sub
        ElseIf ListView1.Items.Count = 0 Then
            MessageBox.Show("Karyawan harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox3.Focus() : Exit Sub
        End If

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            If Button1.Text = "Shift In" Then
                SQL = "select No_Transaksi from N_EMI_Transaksi_Packing_Check_In_Out where "
                SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and Line = '" & TextBox2.Text.Trim.ToUpper & "' "
                SQL = SQL & "and Tanggal_Out is null "
                Using dr = OpenTrans(SQL)
                    If dr.Read Then
                        dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("ada Line yang belum di shfit out pada shift sebelumnya !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                SQL = "insert into N_EMI_Transaksi_Packing_Check_In_Out(Kode_Perusahaan, No_Transaksi, Line, Shift, Tanggal_In, Jam_In) values"
                SQL = SQL & "('" & KodePerusahaan & "', '" & TextBox1.Text.Trim & "', '" & TextBox2.Text.Trim.ToUpper & "', '" & ComboBox2.Text.Trim & "', "
                SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "') "
                ExecuteTrans(SQL)

                For a As Integer = 0 To ListView1.Items.Count - 1
                    SQL = "insert into N_EMI_Transaksi_Packing_Check_In_Out_Detail(Kode_Perusahaan, No_Transaksi, Kode_Karyawan) "
                    SQL = SQL & "values('" & KodePerusahaan & "', '" & TextBox1.Text.Trim & "', '" & ListView1.Items(a).SubItems(1).Text & "')"
                    ExecuteTrans(SQL)
                Next

                N_EMI_Transaksi_Scan_Packing.Label2.Text = "● " & TextBox1.Text.Trim & "  ●  " & ComboBox2.Text.Trim & " ● " & TextBox2.Text.Trim.ToUpper & "  ●  Masuk: " & Format(tgl_skg, "HH:mm:ss") & " "
                N_EMI_Transaksi_Scan_Packing.Label12.Text = TextBox1.Text.Trim
                N_EMI_Transaksi_Scan_Packing.Label13.Text = ComboBox2.Text.Trim
                N_EMI_Transaksi_Scan_Packing.Label14.Text = TextBox2.Text.Trim.ToUpper

                N_EMI_Transaksi_Scan_Packing.Button4.Text = "Shift Out"

            ElseIf Button1.Text = "Shift Out" Then
                SQL = "update N_EMI_Transaksi_Packing_Check_In_Out set "
                SQL = SQL & "Tanggal_Out = '" & Format(tgl_skg, "yyyy-MM-dd") & "', "
                SQL = SQL & "Jam_Out = '" & Format(tgl_skg, "HH:mm:ss") & "' "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Transaksi = '" & TextBox1.Text.Trim & "' "
                ExecuteTrans(SQL)

                N_EMI_Transaksi_Scan_Packing.kosong()
            Else
                CloseTrans()
                CloseConn()
                MessageBox.Show("Terjadi kesalahan !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        If N_EMI_Transaksi_Scan_Packing.Button4.Text = "Shift Out" Then
            N_EMI_Transaksi_Scan_Packing.get_data()
            N_EMI_Transaksi_Scan_Packing.Txt_QR.Focus()
        End If

        Me.Close()
    End Sub

    Private Sub N_EMI_Check_In_Out_Shift_Packing_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub TextBox3_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox3.KeyDown
        ' Jika scanner menekan Enter setelah scan
        If e.KeyCode = Keys.Enter Then
            Dim noInduk As String = TextBox3.Text.Trim()
            CariUserID(noInduk)
        End If
    End Sub

    Private Sub CariUserID(noInduk As String)
        Try
            OpenConn()

            SQL = "select a.No_Transaksi, a.Tanggal_In, a.Jam_In, a.Line "
            SQL = SQL & "from N_EMI_Transaksi_Packing_Check_In_Out a, N_EMI_Transaksi_Packing_Check_In_Out_Detail b, Karyawan c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Transaksi "
            SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Karyawan = c.Kode_Karyawan "
            SQL = SQL & "and a.Status is null and a.Tanggal_Out is null "
            SQL = SQL & "and b.Kode_Perusahaan = '" & KodePerusahaan & "' and c.No_Induk_Karyawan= '" & noInduk & "' "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    dr.Close()
                    CloseConn()
                    MessageBox.Show("karyawan belum melakukan shift out sebelum nya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    TextBox3.Text = ""
                    TextBox3.Focus()
                    Exit Sub
                End If
            End Using

            Dim a As Integer = 0

            SQL = "select b.Kode_Karyawan, b.Nama  from Karyawan b "
            SQL = SQL & "where b.Kode_Perusahaan = '" & KodePerusahaan & "' and b.No_Induk_Karyawan = '" & noInduk & "' "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    For x As Integer = 0 To ListView1.Items.Count - 1
                        If dr("Kode_Karyawan") = ListView1.Items(x).SubItems(1).Text Then
                            dr.Close()
                            CloseConn()
                            MessageBox.Show("karyawan sudah ada di list!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            TextBox3.Text = ""
                            TextBox3.Focus()
                            Exit Sub
                        End If
                    Next
                    a = a + 1
                    Dim lvw As ListViewItem
                    lvw = ListView1.Items.Add(a)
                    lvw.SubItems.Add(dr("Kode_Karyawan"))
                    lvw.SubItems.Add(dr("Nama"))
                Else
                    dr.Close()
                    CloseConn()
                    MessageBox.Show("karyawan tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    TextBox3.Text = ""
                    TextBox3.Focus()
                    Exit Sub
                End If

            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        TextBox3.Text = ""
        TextBox3.Focus()
    End Sub

    Private Sub TextBox2_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox2.KeyDown
        ' Jika scanner menekan Enter setelah scan
        If e.KeyCode = Keys.Enter Then
            Dim noInduk As String = TextBox2.Text.Trim()
            If TextBox2.Text <> "" Then
                ComboBox2.Focus()
                TextBox2.ReadOnly = True
            End If
        End If
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        If ComboBox2.SelectedIndex <> -1 Then
            TextBox3.Focus()
        End If
    End Sub

    'Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
    '    If TextBox3.Text.Trim.Length <> 0 Then
    '        Dim noInduk As String = TextBox3.Text.Trim()
    '        CariUserID(noInduk)
    '    End If
    'End Sub
End Class
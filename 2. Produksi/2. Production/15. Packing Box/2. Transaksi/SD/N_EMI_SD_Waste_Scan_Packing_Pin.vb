Imports System.Data.SqlClient
Imports System.Web.UI.WebControls
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button
Imports Org.BouncyCastle.Asn1

Public Class N_EMI_SD_Waste_Scan_Packing_Pin
    Public asal As String
    Dim nilaiAsli As String
    Private activeTextBox As String
    Private Sub N_EMI_SD_Waste_Scan_Packing_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        TextBox1.Text = ""
        TextBox2.Text = ""
        nilaiAsli = ""
        TextBox2.Focus()
    End Sub

    Private Sub N_EMI_SD_Waste_Scan_Packing_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub CariUserID(noInduk As String)
        Try
            OpenConn()

            SQL = "select a.Kode_Karyawan_HCIS, a.UserID, a.Pin_Packing from Users a, Karyawan b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Karyawan_HCIS = b.Kode_Karyawan "
            SQL = SQL & "and b.Kode_Perusahaan = '" & KodePerusahaan & "' and b.No_Induk_Karyawan = '" & noInduk & "' "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    TextBox2.Text = (dr("UserID"))
                    TextBox1.Focus()
                    'Else
                    '    TextBox2.Text = ""
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub ButtonAngka_Click(sender As Object, e As EventArgs) Handles btn0.Click, btn1.Click, btn2.Click, btn3.Click, btn4.Click, btn5.Click, btn6.Click, btn7.Click, btn8.Click, btn9.Click
        Dim tombol As System.Windows.Forms.Button = CType(sender, System.Windows.Forms.Button)
        nilaiAsli &= tombol.Text
        If asal = "N_EMI_Transaksi_Packing_Cetak_Barcode_Box_Baru - Hold" AndAlso activeTextBox = "TextBox2" Then
            TextBox2.Text = nilaiAsli
            TextBox2.Focus()
        Else
            TextBox1.Text = nilaiAsli
            TextBox1.Focus()
        End If
    End Sub

    Private Sub TextBox1_GotFocus(sender As Object, e As EventArgs) Handles TextBox1.GotFocus
        Me.activeTextBox = "TextBox1"

        If Me.TextBox1.Text = "" Then
            Me.nilaiAsli = ""
        End If
    End Sub

    Private Sub TextBox2_GotFocus(sender As Object, e As EventArgs) Handles TextBox2.GotFocus
        activeTextBox = "TextBox2"

        If TextBox2.Text = "" Then
            nilaiAsli = ""
        End If
    End Sub

    Private Sub TextBox2_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox2.KeyDown
        ' Jika scanner menekan Enter setelah scan
        If e.KeyCode = Keys.Enter Then
            Dim noInduk As String = TextBox2.Text.Trim()
            CariUserID(noInduk)
            'TextBox2.Focus()
        End If
    End Sub

    Private Sub TextBox2_Leave(sender As Object, e As EventArgs) Handles TextBox2.Leave
        If asal = "N_EMI_Transaksi_Packing_Cetak_Barcode_Box_Baru - Hold" And activeTextBox = "TextBox2" Then
            Dim noInduk As String = TextBox2.Text.Trim()
            CariUserID(noInduk)
            'TextBox2.Focus()
        End If
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        If asal = "N_EMI_Transaksi_Packing_Cetak_Barcode_Box_Baru - Hold" And activeTextBox = "TextBox2" Then
            TextBox2.Text = ""
            nilaiAsli = ""
            TextBox2.Focus()
        Else
            TextBox1.Text = ""
            nilaiAsli = ""
            TextBox1.Focus()
        End If
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        If asal = "N_EMI_Transaksi_Packing_Cetak_Barcode_Box_Baru - Hold" And activeTextBox = "TextBox2" Then
            Dim noInduk As String = TextBox2.Text.Trim()
            CariUserID(noInduk)
        End If
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        Select Case Me.asal
            Case "N_EMI_Transaksi_Scan_Packing - Hapus"
                get_jam()

                Try
                    OpenConn()
                    Cmd.Transaction = Cn.BeginTransaction

                    SQL = "SELECT a.Kode_Karyawan_HCIS, a.UserID, a.Pin_Packing FROM Users a, Karyawan b "
                    SQL = SQL & "WHERE a.Kode_Perusahaan = b.Kode_Perusahaan AND a.Kode_Karyawan_HCIS = b.Kode_Karyawan "
                    SQL = SQL & "AND b.Kode_Perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "AND a.UserID = '" & TextBox2.Text & "' "
                    SQL = SQL & "AND a.Pin_Packing = '" & TextBox1.Text & "' "
                    Using dr = OpenTrans(SQL)
                        If Not dr.Read Then
                            dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Pin anda masukan salah", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using

                    SQL = "SELECT a.Flag_Selesai, SUM(b.Jumlah_Sdh_Packing) As Jumlah_Sdh_Packing, "
                    SQL = SQL & "SUM(b.Jumlah_Waste) As Jumlah_Waste, a.Status "
                    SQL = SQL & "FROM N_EMI_Transaksi_Packing a, N_EMI_Transaksi_Packing_Detail b "
                    SQL = SQL & "WHERE a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_Transaksi = b.No_Transaksi "
                    SQL = SQL & "And a.No_Transaksi = '" & Label3.Text & "' GROUP BY a.Flag_Selesai, a.Status "
                    Using dr = OpenTrans(SQL)
                        If dr.Read Then
                            If General_Class.CekNULL(dr("Status")) <> "" Then
                                dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Transaksi sudah pernah dibatalin....", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            ElseIf General_Class.CekNULL(dr("Flag_Selesai")) <> "" Then
                                dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Transaksi sudah selesai, jadi tidak bisa dihapus", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            ElseIf dr("Jumlah_Sdh_Packing") <> 0 Then
                                dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Transaksi sedang proses packing, jadi tidak bisa dihapus", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            ElseIf dr("Jumlah_Waste") <> 0 Then
                                dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Transaksi ada input waste, jadi tidak bisa dihapus", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        Else
                            dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Terjadi kesalahan !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using

                    SQL = "SELECT b.Urut_Results_Detail_Pallet FROM N_EMI_Transaksi_Packing a, N_EMI_Transaksi_Packing_Detail b "
                    SQL = SQL & "WHERE a.Kode_Perusahaan = b.Kode_Perusahaan AND a.No_Transaksi = b.No_Transaksi AND a.Status is null "
                    SQL = SQL & "AND a.Kode_Perusahaan = '" & KodePerusahaan & "' AND a.No_Transaksi = '" & Label3.Text & "' "
                    Using Ds = BindingTrans(SQL)
                        With Ds.Tables("MyTable")
                            If .Rows.Count <> 0 Then
                                For i As Integer = 0 To .Rows.Count - 1
                                    SQL = "UPDATE Emi_Production_Results_Detail_Pallet "
                                    SQL = SQL & "SET Flag_Scan = NULL WHERE Kode_Perusahaan = '" & KodePerusahaan & "' "
                                    SQL = SQL & "AND Urut_Oto = '" & .Rows(i).Item("Urut_Results_Detail_Pallet") & "' "
                                    ExecuteTrans(SQL)
                                Next
                            Else
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("No transaksi tidak ditemukan.....", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        End With
                    End Using
                    SQL = "UPDATE N_EMI_Transaksi_Packing SET Status = 'Y' "
                    SQL = SQL & "WHERE Kode_Perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "AND No_Transaksi = '" & Label3.Text & "' "
                    ExecuteTrans(SQL)

                    Cmd.Transaction.Commit()
                    CloseConn()
                Catch ex As Exception
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show(ex.Message)
                    Exit Sub
                End Try

                N_EMI_Transaksi_Scan_Packing.get_data()
                Me.Close()

            Case "N_EMI_Transaksi_Packing_Cetak_Barcode_Box_Baru - Hold"
                get_jam()

                Try
                    OpenConn()
                    Cmd.Transaction = Cn.BeginTransaction

                    SQL = "SELECT a.Kode_Karyawan_HCIS, a.UserID, a.Pin_Packing FROM Users a, Karyawan b "
                    SQL = SQL & "WHERE a.Kode_Perusahaan = b.Kode_Perusahaan AND a.Kode_Karyawan_HCIS = b.Kode_Karyawan "
                    SQL = SQL & "AND b.Kode_Perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "AND a.UserID = '" & TextBox2.Text & "' "
                    SQL = SQL & "AND a.Pin_Packing = '" & TextBox1.Text & "' "
                    Using dr = OpenTrans(SQL)
                        If Not dr.Read Then
                            dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Pin anda masukan salah", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using

                    SQL = "SELECT a.Flag_Selesai, SUM(b.Jumlah_Sdh_Packing) As Jumlah_Sdh_Packing, "
                    SQL = SQL & "SUM(b.Jumlah_Waste) As Jumlah_Waste, a.Status "
                    SQL = SQL & "FROM N_EMI_Transaksi_Packing a, N_EMI_Transaksi_Packing_Detail b "
                    SQL = SQL & "WHERE a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_Transaksi = b.No_Transaksi "
                    SQL = SQL & "And a.No_Transaksi = '" & Label3.Text & "' GROUP BY a.Flag_Selesai, a.Status "
                    Using dr = OpenTrans(SQL)
                        If dr.Read Then
                            If General_Class.CekNULL(dr("Status")) <> "" Then
                                dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Transaksi sudah pernah dibatalin....", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            End If
                        Else
                            dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Terjadi kesalahan !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using

                    SQL = "select b.Urut_Results_Detail_Pallet from N_EMI_Transaksi_Packing a, N_EMI_Transaksi_Packing_Detail b "
                    SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Transaksi "
                    SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Transaksi = '" & Label3.Text & "' "
                    Using Ds = BindingTrans(SQL)
                        With Ds.Tables("MyTable")
                            If .Rows.Count <> 0 Then
                                For i As Integer = 0 To .Rows.Count - 1
                                    SQL = "update Emi_Production_Results_Detail_Pallet set Flag_Scan = NULL where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                    SQL = SQL & "and Urut_Oto = '" & .Rows(i).Item("Urut_Results_Detail_Pallet") & "' "
                                Next
                            Else
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("no transaksi tidak ditemukan.....", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        End With
                    End Using

                    SQL = "UPDATE N_EMI_Transaksi_Packing SET "
                    SQL = SQL & "Flag_Hold = 'Y', Flag_Selesai = 'Y', "
                    SQL = SQL & "Tanggal_Hold = '" & Format(tgl_skg, "yyyy-MM-dd") & "', "
                    SQL = SQL & "Jam_Hold = '" & Format(tgl_skg, "HH:mm:ss") & "' "
                    SQL = SQL & "WHERE Kode_Perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "AND No_Transaksi = '" & Label3.Text & "' "
                    ExecuteTrans(SQL)

                    Cmd.Transaction.Commit()
                    CloseConn()
                Catch ex As Exception
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show(ex.Message)
                    Exit Sub
                End Try

                N_EMI_Transaksi_Packing_Cetak_Barcode_Box_Baru.get_data()
                N_EMI_Transaksi_Packing_Cetak_Barcode_Box_Baru.get_log_scan()
                N_EMI_Transaksi_Packing_Cetak_Barcode_Box_Baru.get_data_repacking()
                Me.Close()

            Case "N_EMI_Transaksi_Scan_Packing - Waste"
                get_jam()

                Try
                    OpenConn()
                    Cmd.Transaction = Cn.BeginTransaction

                    SQL = "SELECT a.Kode_Karyawan_HCIS, a.UserID, a.Pin_Packing FROM Users a, Karyawan b "
                    SQL = SQL & "WHERE a.Kode_Perusahaan = b.Kode_Perusahaan AND a.Kode_Karyawan_HCIS = b.Kode_Karyawan "
                    SQL = SQL & "AND b.Kode_Perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "AND a.UserID = '" & TextBox2.Text & "' "
                    SQL = SQL & "AND a.Pin_Packing = '" & TextBox1.Text & "' "
                    Using dr = OpenTrans(SQL)
                        If Not dr.Read Then
                            dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Pin anda masukan salah", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using

                    SQL = "SELECT a.Flag_Selesai, SUM(b.Jumlah_Sdh_Packing) AS Jumlah_Sdh_Packing, "
                    SQL = SQL & "SUM(b.Jumlah_Waste) AS Jumlah_Waste, a.Status "
                    SQL = SQL & "FROM N_EMI_Transaksi_Packing a, N_EMI_Transaksi_Packing_Detail b "
                    SQL = SQL & "WHERE a.Kode_Perusahaan = b.Kode_Perusahaan AND a.No_Transaksi = b.No_Transaksi "
                    SQL = SQL & "AND a.No_Transaksi = '" & Label3.Text & "' "
                    SQL = SQL & "GROUP BY a.Flag_Selesai, a.Status "
                    Using dr = OpenTrans(SQL)
                        If dr.Read Then
                            If General_Class.CekNULL(dr("Status")) <> "" Then
                                dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Transaksi sudah pernah dibatalin....", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            ElseIf General_Class.CekNULL(dr("Flag_Selesai")) <> "" Then
                                dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Transaksi sudah selesai ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        Else
                            dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Terjadi kesalahan !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using

                    SQL = "SELECT SUM(b.Jumlah) AS Jumlah, SUM(b.Jumlah_Sdh_Packing) AS Jumlah_Sdh_Packing, "
                    SQL = SQL & "SUM(b.Jumlah_Waste) AS Jumlah_Waste "
                    SQL = SQL & "FROM N_EMI_Transaksi_Packing a,N_EMI_Transaksi_Packing_Detail b "
                    SQL = SQL & "WHERE a.Kode_Perusahaan = b.Kode_Perusahaan AND a.No_Transaksi = b.No_Transaksi "
                    SQL = SQL & "AND a.Flag_Selesai IS NULL AND a.Status IS NULL "
                    SQL = SQL & "AND a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "AND a.No_Transaksi = '" & Label3.Text & "' "
                    Using dr = OpenTrans(SQL)
                        If dr.Read Then
                            Dim totalBarang As Integer = Val(dr("Jumlah"))
                            Dim totalPacking As Integer = Val(dr("Jumlah_Sdh_Packing"))
                            Dim totalWaste As Integer = Val(dr("Jumlah_Waste"))
                            Dim inputWaste As Integer = Val(Me.Label5.Text)
                            Dim totalProses As Integer = totalPacking + totalWaste + inputWaste

                            If totalProses > totalBarang Then
                                dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("jumlah waste melebihi sisa barang !, jadi tidak bisa dihapus", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        Else
                            dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Terjadi kesalahan !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using

                    SQL = "SELECT b.No_Transaksi, b.Urut_Oto, b.Urut_Results_Detail_Pallet, "
                    SQL = SQL & "(b.Jumlah - b.Jumlah_Sdh_Packing - b.Jumlah_Waste) AS Jmlh_Sisa "
                    SQL = SQL & "FROM N_EMI_Transaksi_Packing a, N_EMI_Transaksi_Packing_Detail b "
                    SQL = SQL & "WHERE a.Kode_Perusahaan = b.Kode_Perusahaan  And a.No_Transaksi = b.No_Transaksi "
                    SQL = SQL & "And a.Flag_Selesai Is NULL AND a.Status IS NULL "
                    SQL = SQL & "AND a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "AND a.No_Transaksi = '" & Label3.Text & "' "
                    Using Ds = BindingTrans(SQL)
                        With Ds.Tables("MyTable")
                            If .Rows.Count <> 0 Then
                                Dim sisaWaste As Integer = Val(Label5.Text)
                                For i As Integer = 0 To .Rows.Count - 1
                                    If sisaWaste = 0 Then
                                        Exit For
                                    ElseIf sisaWaste < 0 Then
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Sisa < 0", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If

                                    If sisaWaste < .Rows(i).Item("Jmlh_Sisa") Or sisaWaste = .Rows(i).Item("Jmlh_Sisa") Then
                                        SQL = "UPDATE N_EMI_Transaksi_Packing_Detail SET "
                                        SQL = SQL & "Jumlah_Waste = Jumlah_Waste + '" & sisaWaste & "' "
                                        SQL = SQL & "WHERE Urut_Oto = '" & .Rows(i).Item("Urut_Oto") & "' "
                                        ExecuteTrans(SQL)

                                        SQL = "UPDATE Emi_Production_Results_Detail_Pallet SET "
                                        SQL = SQL & "Jumlah_Waste = Jumlah_Waste + '" & sisaWaste & "' "
                                        SQL = SQL & "WHERE Urut_Oto = '" & .Rows(i).Item("Urut_Results_Detail_Pallet") & "' "
                                        ExecuteTrans(SQL)

                                        sisaWaste = 0
                                    ElseIf sisaWaste > .Rows(i).Item("Jmlh_Sisa") Then
                                        SQL = "UPDATE N_EMI_Transaksi_Packing_Detail SET "
                                        SQL = SQL & "Jumlah_Waste = Jumlah_Waste + '" & .Rows(i).Item("Urut_Results_Detail_Pallet") & "' "
                                        SQL = SQL & "WHERE Urut_Oto = '" & .Rows(i).Item("Urut_Oto") & "' "
                                        ExecuteTrans(SQL)

                                        SQL = "UPDATE Emi_Production_Results_Detail_Pallet SET "
                                        SQL = SQL & "Jumlah_Waste = Jumlah_Waste + '" & .Rows(i).Item("Jmlh_Sisa") & "' "
                                        SQL = SQL & "WHERE Urut_Oto = '" & .Rows(i).Item("Urut_Results_Detail_Pallet") & "' "
                                        ExecuteTrans(SQL)

                                        sisaWaste = sisaWaste - .Rows(i).Item("Jmlh_Sisa")
                                    Else
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Barang tidak cukup harap scan barang terlebih dahulu !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If

                                    SQL = "SELECT SUM(a.Jumlah) AS Jumlah, SUM(a.Jumlah_Sdh_Packing) AS Jumlah_Sdh_Packing, "
                                    SQL = SQL & "SUM(a.Jumlah_Waste) AS Jumlah_Waste "
                                    SQL = SQL & "FROM N_EMI_Transaksi_Packing_Detail a, N_EMI_Transaksi_Packing b "
                                    SQL = SQL & "WHERE a.No_Transaksi = '" & .Rows(i).Item("No_Transaksi") & "' "
                                    SQL = SQL & "AND a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                                    SQL = SQL & "AND a.Kode_Perusahaan = b.Kode_Perusahaan "
                                    SQL = SQL & "AND a.No_Transaksi = b.No_Transaksi "
                                    Using dr = OpenTrans(SQL)
                                        If dr.Read Then
                                            Dim total As Integer = Val(dr("Jumlah"))
                                            Dim selesai As Integer = Val(dr("Jumlah_Sdh_Packing")) + Val(dr("Jumlah_Waste"))

                                            If total = selesai Then
                                                SQL = "UPDATE N_EMI_Transaksi_Packing SET Flag_Selesai = 'Y' "
                                                SQL = SQL & "WHERE Kode_Perusahaan = '" & KodePerusahaan & "' "
                                                SQL = SQL & "AND No_Transaksi = '" & .Rows(i).Item("No_Transaksi") & "' "
                                                ExecuteTrans(SQL)
                                            End If
                                        End If
                                    End Using

                                Next
                            Else
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Barang tidak ditemukan !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        End With
                    End Using

                    Cmd.Transaction.Commit()
                    CloseConn()
                Catch ex As Exception
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show(ex.Message)
                    Exit Sub
                End Try
                N_EMI_Transaksi_Scan_Packing.get_data()
                Me.Close()

            Case Else
                MessageBox.Show("Asal tidak ditemukan.....", Module1.Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
        End Select
    End Sub
End Class
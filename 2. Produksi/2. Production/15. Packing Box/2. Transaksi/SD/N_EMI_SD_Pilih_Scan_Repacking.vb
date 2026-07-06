Imports System.Runtime.CompilerServices
Imports System.Web.UI.WebControls
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button

Public Class N_EMI_SD_Pilih_Scan_Repacking
    Dim Lvcheckbox As String
    Dim Lvnosplit As String
    Dim Lvbarcode As String
    Dim Lvkode As String
    Dim Lvnama As String
    Dim Lvjumlah As String
    Dim Lvurut_box As String
    Dim Lvurut_pallet As String

    Dim Cellcheckbox As Integer = 0
    Dim Cellnosplit As Integer = 1
    Dim Cellbarcode As Integer = 2
    Dim Cellkode As Integer = 3
    Dim Cellnama As Integer = 4
    Dim Celljumlah As Integer = 5
    Dim Cellurut_box As Integer = 6
    Dim Cellurut_pallet As Integer = 7

    Public filtertambah As String
    Dim arrcari As ArrayList
    Private Sub N_EMI_SD_Pilih_Scan_Packing_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub N_EMI_SD_Pilih_Scan_Packing_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Public Sub kosong()
        Me.DataGridView1.Rows.Clear()
        Me.Txt_QR.Text = ""
        Me.Txt_QR.Focus()
    End Sub

    Public Sub Cari(ByVal semua As String)
        Try
            OpenConn()

            SQL = "select Flag_Input_GR2, Status from N_EMI_Transaksi_Packing_Pallet "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Unik_Print = '" & semua & "' "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    If General_Class.CekNULL(dr("Status")) <> "" Then
                        dr.Close()
                        CloseConn()
                        MessageBox.Show("Pallet sudah pernah dibatalin....", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    ElseIf General_Class.CekNULL(dr("Flag_Input_GR2")) <> "" Then
                        dr.Close()
                        CloseConn()
                        MessageBox.Show("pallet sudah input gr2, jadi tidak bisa repacking", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                Else
                    dr.Close()
                    CloseConn()
                    MessageBox.Show("Terjadi kesalahan, data pallet tidak ditemukan !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            DataGridView1.Rows.Clear()
            SQL = "With Prod As ( "
            SQL = SQL & "Select y.No_Production_Order, x.Kode_Perusahaan, x.Urut_Oto, "
            SQL = SQL & "ROW_NUMBER() OVER (PARTITION BY x.Kode_Perusahaan, x.Urut_Oto "
            SQL = SQL & "ORDER BY DATEADD(SECOND, DATEDIFF(SECOND, 0, y.Jam), y.Tanggal) "
            SQL = SQL & ") As rn "
            SQL = SQL & "FROM Emi_Production_Results y "
            SQL = SQL & "INNER JOIN Emi_Production_Results_Detail_Pallet x "
            SQL = SQL & "On x.Kode_Perusahaan = y.Kode_Perusahaan "
            SQL = SQL & "And x.No_Transaksi = y.No_Transaksi "
            SQL = SQL & "WHERE y.Status Is NULL) "
            SQL = SQL & "Select p.No_Production_Order, a.Kode_Unik_Print As Barcode_Box,  a.Urut_Oto As Urut_Box,  a.Kode_Barang, "
            SQL = SQL & "e.Nama As Nama_Barang,  SUM(b.Jumlah) As Jumlah, z.Kode_Unik_Print As Barcode_Pallet, a.Urut_Pallet "
            SQL = SQL & "FROM N_EMI_Transaksi_Packing_Box a INNER JOIN N_EMI_Transaksi_Packing_Det b "
            SQL = SQL & "On a.Kode_Perusahaan = b.Kode_Perusahaan And a.Urut_Oto = b.Urut_Transaksi_Box "
            SQL = SQL & "INNER JOIN N_EMI_Transaksi_Packing_Detail c "
            SQL = SQL & "On b.Kode_Perusahaan = c.Kode_Perusahaan And b.Urut_Detail_Transaksi_Packing = c.Urut_Oto "
            SQL = SQL & "INNER JOIN Barang_SN d "
            SQL = SQL & "On c.Kode_Perusahaan = d.Kode_Perusahaan And c.SN_Baru = d.Serial_Number "
            SQL = SQL & "INNER JOIN Barang e "
            SQL = SQL & "On d.Kode_Perusahaan = e.Kode_Perusahaan And d.Kode_Stock_Owner = e.Kode_Stock_Owner And d.Kode_Barang = e.Kode_Barang "
            SQL = SQL & "INNER JOIN N_EMI_Transaksi_Packing_Pallet z "
            SQL = SQL & "On a.Kode_Perusahaan = z.Kode_Perusahaan And a.Urut_Pallet = z.Urut_Oto "
            SQL = SQL & "INNER JOIN Prod p On c.Kode_Perusahaan = p.Kode_Perusahaan "
            SQL = SQL & "And c.Urut_Results_Detail_Pallet = p.Urut_Oto And p.rn = 1 "
            SQL = SQL & "WHERE a.Status Is NULL And b.Status Is NULL and z.Flag_Input_GR2 is null "
            SQL = SQL & "And z.Kode_Unik_Print = '" & semua & "' "
            SQL = SQL & " " & filtertambah & " "
            SQL = SQL & "GROUP BY p.No_Production_Order, a.Kode_Unik_Print,  a.Urut_Oto,  a.Kode_Barang,  e.Nama,  z.Kode_Unik_Print,  a.Urut_Pallet "

            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1
                            DataGridView1.Rows.Add(1)
                            DataGridView1.Rows(i).Cells(Cellnosplit).Value = .Rows(i).Item("No_Production_Order")
                            DataGridView1.Rows(i).Cells(Cellbarcode).Value = .Rows(i).Item("Barcode_Box")
                            DataGridView1.Rows(i).Cells(Cellkode).Value = .Rows(i).Item("Kode_Barang")
                            DataGridView1.Rows(i).Cells(Cellnama).Value = .Rows(i).Item("Nama_Barang")
                            DataGridView1.Rows(i).Cells(Celljumlah).Value = Format(.Rows(i).Item("Jumlah"), "N0")
                            DataGridView1.Rows(i).Cells(Cellurut_box).Value = .Rows(i).Item("Urut_Box")
                            DataGridView1.Rows(i).Cells(Cellurut_pallet).Value = .Rows(i).Item("Urut_Pallet")

                            If i Mod 2 = 0 Then
                                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.Beige
                            Else
                                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.LightGray
                            End If
                        Next
                    Else
                        CloseConn()
                        MessageBox.Show("Pallet tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Txt_QR.Text = ""
                        Txt_QR.Focus()
                        Exit Sub
                    End If
                End With
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        For Each row As DataGridViewRow In DataGridView1.Rows
            If Not row.IsNewRow Then
                If Convert.ToBoolean(row.Cells(0).Value) = True Then

                    Dim qr As String = row.Cells(2).Value.ToString()

                    N_Emi_Transaksi_Repacking.Txt_QR.Text = qr
                    N_Emi_Transaksi_Repacking.xscan(qr)

                End If
            End If
        Next
        Me.Close()
    End Sub

    Private Sub Txt_QR_Enter(sender As Object, e As EventArgs) Handles Txt_QR.Enter
        If Txt_QR.Text = "Scan QR Code" Then
            Txt_QR.Text = ""
            Txt_QR.ForeColor = Color.Black
        End If
    End Sub

    Private Sub Txt_QR_Leave(sender As Object, e As EventArgs) Handles Txt_QR.Leave
        ' Jangan ubah kembali ke "Scan QR Code" kalau sudah ada hasil scan
        If Txt_QR.Text = "" Then
            Txt_QR.Text = "Scan QR Code"
            Txt_QR.ForeColor = Color.Gray
            'Else
            '    Button1.Focus()
        End If
    End Sub

    Private Sub Txt_QR_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_QR.KeyDown
        If e.KeyCode = Keys.Enter Then
            ' Abaikan placeholder
            If Txt_QR.Text.Trim <> "" AndAlso Txt_QR.Text <> "Scan QR Code" Then
                Cari(Txt_QR.Text)
            End If
        End If
    End Sub
End Class
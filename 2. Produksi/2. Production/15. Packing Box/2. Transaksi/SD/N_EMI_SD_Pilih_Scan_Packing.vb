Imports System.Runtime.CompilerServices
Imports System.Web.UI.WebControls
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button

Public Class N_EMI_SD_Pilih_Scan_Packing
    Dim Lvcheckbox As String
    Dim Lvnosplit As String
    Dim Lvnobatch As String
    Dim Lvkode As String
    Dim Lvnama As String
    Dim Lvpcs As String
    Dim Lvsatuan As String
    Dim Lvtglpro As String
    Dim Lvnobox As String
    Dim Lvbarcode As String
    Dim Lvqr As String
    Dim Lvkodeunik As String

    Dim Cellcheckbox As Integer = 0
    Dim Cellnosplit As Integer = 1
    Dim Cellnobatch As Integer = 2
    Dim Cellkode As Integer = 3
    Dim Cellnama As Integer = 4
    Dim Cellpcs As Integer = 5
    Dim Cellsatuan As Integer = 6
    Dim Celltglpro As Integer = 7
    Dim Cellnobox As Integer = 8
    Dim Cellbarcode As Integer = 9
    Dim Cellqr As Integer = 10
    Dim Cellkodeunik As Integer = 11

    Public filtertambah As String
    Dim arrcari As New ArrayList
    Private Sub N_EMI_SD_Pilih_Scan_Packing_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub N_EMI_SD_Pilih_Scan_Packing_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Public Sub kosong()
        TextBox1.Text = ""
        ComboBox1.Items.Clear() : arrcari.Clear()
        ComboBox1.Items.Add("No Split") : arrcari.Add("a.No_Production_Order")
        ComboBox1.Items.Add("Nama Barang") : arrcari.Add("d.Nama")
    End Sub

    Public Sub Cari(ByVal semua As String)
        Try
            OpenConn()

            DataGridView1.Rows.Clear()
            SQL = "with cte as( "
            SQL = SQL & "select a.No_Production_Order, e.Proses, d.Kode_Barang, d.Nama as Nama_Barang, c.Jumlah as Jumlah_Pcs, "
            SQL = SQL & "isnull(b.Jumlah_Waste, 0) as Jumlah_Waste, isnull(b.Jumlah_Sdh_Packing, 0) as Jumlah_Sdh_Packing, "
            SQL = SQL & "b.Satuan, b.Tgl_Produksi, b.Nomor, b.Qr_Code + '-' + b.Kode_Unik_Berjalan as Barcode, b.Qr_Code, b.Kode_Unik_Berjalan, "
            SQL = SQL & "isnull(b.Jumlah_Input_GR_2_Pallet, 0) as Jumlah_Input_GR_2_Pallet, isnull(b.Jumlah_Input_GR_2_Waste, 0) as Jumlah_Input_GR_2_Waste "
            SQL = SQL & "from Emi_Production_Results a, Emi_Production_Results_Detail_Pallet b, Barang_SN c, Barang d, Emi_Production_Results_HPP e "
            SQL = SQL & "where a.Status is null and a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Transaksi "
            SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.SN_Baru = c.Serial_Number and b.Flag_Scan is null "
            SQL = SQL & "and c.Kode_Perusahaan = d.Kode_Perusahaan and c.Kode_Stock_Owner = d.Kode_Stock_Owner and c.Kode_Barang = d.Kode_Barang "
            SQL = SQL & "and b.Kode_Perusahaan = e.Kode_Perusahaan and b.Urut_HPP = e.Urut and b.No_Transaksi = e.No_Transaksi "

            If semua = "T" Then
                SQL = SQL & "and " & arrcari(ComboBox1.SelectedIndex) & "  like '%" & TextBox1.Text & "%' "
            End If

            SQL = SQL & " " & filtertambah & " "
            SQL = SQL & "and c.Jumlah - (isnull(b.Jumlah_Waste, 0) - isnull(b.Jumlah_Input_GR_2_Waste, 0)) - (isnull(b.Jumlah_Sdh_Packing, 0) - isnull(b.Jumlah_Input_GR_2_Pallet, 0) ) > 0) "
            SQL = SQL & "select z.No_Production_Order, STRING_AGG(z.Proses, ', ') as proses, z.Kode_Barang, z.Nama_Barang, "
            SQL = SQL & "sum(z.Jumlah_Pcs) as Jumlah_Pcs, sum(z.Jumlah_Waste) as Jumlah_Waste, sum(z.Jumlah_Sdh_Packing) as Jumlah_Sdh_Packing, z.Satuan, "
            SQL = SQL & "sum(Jumlah_Input_GR_2_Pallet) as Jumlah_Input_GR_2_Pallet, sum(Jumlah_Input_GR_2_Waste) as Jumlah_Input_GR_2_Waste, "
            SQL = SQL & "z.Tgl_Produksi, z.Nomor, z.Barcode, z.Qr_Code, z.Kode_Unik_Berjalan "
            SQL = SQL & "from cte z "
            SQL = SQL & "group by z.No_Production_Order, z.Kode_Barang, z.Nama_Barang, z.Satuan, "
            SQL = SQL & "z.Tgl_Produksi, z.Nomor, z.Barcode, z.Qr_Code, z.Kode_Unik_Berjalan "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1
                            Dim xjum As Integer = .Rows(i).Item("Jumlah_Pcs") - (.Rows(i).Item("Jumlah_Sdh_Packing") - .Rows(i).Item("Jumlah_Input_GR_2_Pallet")) - (.Rows(i).Item("Jumlah_Waste") - .Rows(i).Item("Jumlah_Input_GR_2_Waste"))

                            DataGridView1.Rows.Add(1)
                            DataGridView1.Rows(i).Cells(Cellnosplit).Value = .Rows(i).Item("No_Production_Order")
                            DataGridView1.Rows(i).Cells(Cellnobatch).Value = .Rows(i).Item("proses")
                            DataGridView1.Rows(i).Cells(Cellkode).Value = .Rows(i).Item("Kode_Barang")
                            DataGridView1.Rows(i).Cells(Cellnama).Value = .Rows(i).Item("Nama_Barang")
                            DataGridView1.Rows(i).Cells(Cellpcs).Value = Format(xjum, "N0")
                            DataGridView1.Rows(i).Cells(Cellsatuan).Value = .Rows(i).Item("Satuan")
                            DataGridView1.Rows(i).Cells(Celltglpro).Value = Format(.Rows(i).Item("Tgl_Produksi"), "dd MMM yyyy")
                            DataGridView1.Rows(i).Cells(Cellnobox).Value = .Rows(i).Item("Nomor")
                            DataGridView1.Rows(i).Cells(Cellbarcode).Value = .Rows(i).Item("Barcode")
                            DataGridView1.Rows(i).Cells(Cellqr).Value = .Rows(i).Item("Qr_Code")
                            DataGridView1.Rows(i).Cells(Cellkodeunik).Value = .Rows(i).Item("Kode_Unik_Berjalan")

                            If i Mod 2 = 0 Then
                                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.Beige
                            Else
                                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.LightGray
                            End If
                        Next
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

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If ComboBox1.Text.Trim.Length = 0 Then
            MessageBox.Show("kolom harus diisi!", Module1.Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox1.Focus()
            Exit Sub
        ElseIf TextBox1.Text.Trim.Length = 0 Then
            MessageBox.Show("value harus diisi!", Module1.Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox1.Focus()
            Exit Sub
        End If

        Cari("T")
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        For Each row As DataGridViewRow In DataGridView1.Rows
            If Not row.IsNewRow Then
                If Convert.ToBoolean(row.Cells(0).Value) = True Then

                    Dim qr As String = row.Cells(9).Value.ToString()

                    N_EMI_Transaksi_Scan_Packing.Txt_QR.Text = qr
                    N_EMI_Transaksi_Scan_Packing.xscan(qr)

                End If
            End If
        Next
        Me.Close()
    End Sub
End Class
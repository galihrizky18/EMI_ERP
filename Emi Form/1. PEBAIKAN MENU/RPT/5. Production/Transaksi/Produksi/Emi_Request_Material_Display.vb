'Public Class Emi_Request_Material_Display_vb
Public Class Emi_Request_Material_Display

    Dim Lv_Status, Lv_NoFaktur, Lv_Lokasi, Lv_KdBarang, Lv_Tanggal, Lv_Jam, Lv_Jmlh, Lv_Satuan, Lv_User, Lv_Nama As String

    Dim item_Status As Integer = 0
    Dim item_NoFak As Integer = 1
    Dim item_Lokasi As Integer = 2
    Dim item_KdBarang As Integer = 3
    Dim item_Nama As Integer = 4
    Dim item_Tanggal As Integer = 5
    Dim item_Jam As Integer = 6
    Dim item_Jumlah As Integer = 7
    Dim item_Satuan As Integer = 8
    Dim item_User As Integer = 9

    Private Sub Emi_Request_Material_Display_vb_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub Emi_Request_Material_Display_vb_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        kosong()
    End Sub

    Public Sub kosong()
        Lv_Data.Rows.Clear()
        Load_Lv()
    End Sub


    Private Sub Get_Data_Lv(ByVal index As Integer)

        Lv_Status = Lv_Data.Rows(index).Cells(item_Status).Value
        Lv_NoFaktur = Lv_Data.Rows(index).Cells(item_NoFak).Value
        Lv_Lokasi = Lv_Data.Rows(index).Cells(item_Lokasi).Value
        Lv_KdBarang = Lv_Data.Rows(index).Cells(item_KdBarang).Value
        Lv_Nama = Lv_Data.Rows(index).Cells(item_Nama).Value
        Lv_Tanggal = Lv_Data.Rows(index).Cells(item_Tanggal).Value
        Lv_Jam = Lv_Data.Rows(index).Cells(item_Jam).Value
        Lv_Jmlh = Lv_Data.Rows(index).Cells(item_Jumlah).Value
        Lv_Satuan = Lv_Data.Rows(index).Cells(item_Satuan).Value
        Lv_User = Lv_Data.Rows(index).Cells(item_User).Value

    End Sub

    Private Sub Load_Lv()

        Try
            OpenConn()

            Lv_Data.Rows.Clear()
            SQL = "select a.no_transaksi, a.Kode_stock_Owner, a.Kode_Barang, b.Nama, a.Tgl_Produksi, a.Jam_Produksi, a.Jumlah, a.Satuan, a.UserId, "

            SQL = SQL & "ISNULL(( "
            SQL = SQL & "select top 1 'T' "
            SQL = SQL & "from Emi_Split_Production_Order_Detail_Bahan z where a.Kode_Perusahaan = z.Kode_Perusahaan and a.no_transaksi = z.No_Faktur and Flag_Terpenuhi is null "
            SQL = SQL & "), 'Y') as Produksi_Bahan_Terpenuhi, "

            SQL = SQL & "ISNULL(( "
            SQL = SQL & "select top 1 'T' "
            SQL = SQL & "from Emi_Split_Production_Order_Detail_Packaging z "
            SQL = SQL & "where a.Kode_Perusahaan = z.Kode_Perusahaan "
            SQL = SQL & "and a.no_transaksi = z.No_Faktur "
            SQL = SQL & "and Flag_Terpenuhi is null "
            SQL = SQL & "), 'Y') as Produksi_Packaging_Terpenuhi "

            SQL = SQL & "from Emi_Split_Production_Order a, barang b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.Kode_stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.Selesai is null "
            SQL = SQL & "and a.Flag_Selesai_Request_Material is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "order by a.tgl_produksi desc "
            Using Dr = OpenTrans(SQL)
                Dim row As Integer = 0
                Do While Dr.Read

                    Lv_Data.Rows.Add(1)
                    If General_Class.CekNULL(Dr("Produksi_Bahan_Terpenuhi")) = "Y" And General_Class.CekNULL(Dr("Produksi_Packaging_Terpenuhi")) = "Y" Then
                        Lv_Data.Rows(row).Cells(item_Status).Value = "Terpenuhi"
                        Lv_Data.Rows(row).Cells(item_Status).Style.BackColor = Color.LightGreen
                    ElseIf General_Class.CekNULL(Dr("Produksi_Bahan_Terpenuhi")) = "T" Or General_Class.CekNULL(Dr("Produksi_Packaging_Terpenuhi")) = "T" Then
                        Lv_Data.Rows(row).Cells(item_Status).Value = "Belum Terpenuhi"
                        Lv_Data.Rows(row).Cells(item_Status).Style.BackColor = Color.LightYellow
                    End If

                    Lv_Data.Rows(row).Cells(item_NoFak).Value = Dr("no_transaksi")
                    Lv_Data.Rows(row).Cells(item_Lokasi).Value = Dr("Kode_stock_Owner")
                    Lv_Data.Rows(row).Cells(item_KdBarang).Value = Dr("Kode_Barang")
                    Lv_Data.Rows(row).Cells(item_Nama).Value = Dr("Nama")
                    If Not General_Class.CekNULL(Dr("tgl_produksi")) = "" Then
                        Lv_Data.Rows(row).Cells(item_Tanggal).Value = Format(Dr("tgl_produksi"), "dd MMM yyyy")
                        Lv_Data.Rows(row).Cells(item_Jam).Value = Dr("Jam_Produksi")
                    Else
                        Lv_Data.Rows(row).Cells(item_Tanggal).Value = "-"
                        Lv_Data.Rows(row).Cells(item_Jam).Value = "-"
                    End If

                    Lv_Data.Rows(row).Cells(item_Jumlah).Value = Dr("Jumlah")
                    Lv_Data.Rows(row).Cells(item_Satuan).Value = Dr("Satuan")
                    Lv_Data.Rows(row).Cells(item_User).Value = Dr("UserId")

                    row = row + 1
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub


    Private Sub Lv_Data_DoubleClick_1(sender As Object, e As EventArgs) Handles Lv_Data.DoubleClick

        If Lv_Data.Rows.Count = 0 Then Exit Sub

        Get_Data_Lv(Lv_Data.CurrentRow.Index)

        Emi_Request_Material.Txt_NoFaktur.Text = Lv_NoFaktur
        Emi_Request_Material.Txt_So.Text = Lv_Lokasi
        Emi_Request_Material.Txt_KdBarang.Text = Lv_KdBarang
        Emi_Request_Material.Txt_NmBarang.Text = Lv_Nama

        Emi_Request_Material.ShowDialog()
    End Sub
    Private Sub SelesaiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SelesaiToolStripMenuItem.Click

        If Lv_Data.Rows.Count = 0 Or Lv_Data.CurrentRow.Index = -1 Then Exit Sub

        Try
            OpenConn()
            Get_Data_Lv(Lv_Data.CurrentRow.Index)

            '=============================================
            '=     CEK APAKAH BARANG SUDAH TERPENUHI     =
            '=============================================

            If Lv_Status = "Terpenuhi" Then

                SQL = "update Emi_Split_Production_Order set Flag_Selesai_Request_Material = 'Y' where Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and No_transaksi = '" & Lv_NoFaktur & "' and Kode_stock_Owner = '" & Lv_Lokasi & "' and Kode_Barang = '" & Lv_KdBarang & "' "
                ExecuteTrans(SQL)

            Else

                Dim Validasi As String = MessageBox.Show("Requst Material Belum Terpenuhi, Tetap ingin Akhiri Request Produksi . . ? ?", "Production", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If Validasi = vbYes Then

                    SQL = "update Emi_Split_Production_Order set Flag_Selesai_Request_Material = 'Y' where Kode_Perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "and No_transaksi = '" & Lv_NoFaktur & "' and Kode_stock_Owner = '" & Lv_Lokasi & "' and Kode_Barang = '" & Lv_KdBarang & "' "
                    ExecuteTrans(SQL)

                End If

            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        kosong()

    End Sub




End Class
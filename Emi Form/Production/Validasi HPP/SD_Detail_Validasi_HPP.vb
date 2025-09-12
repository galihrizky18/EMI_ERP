Public Class SD_Detail_Validasi_HPP

    Public noSplit As String = ""
    Public Proses As String = ""



    Private Sub SD_Detail_Validasi_HPP_DockChanged(sender As Object, e As EventArgs) Handles Me.DockChanged
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub
    Private Sub SD_Detail_Validasi_HPP_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        Kosong()
    End Sub

    Public Sub Kosong()

        Lv_BahanBaku.Items.Clear()

        Txt_TotalBahanBaku.Text = ""

        Lv_BahanBaku.Columns.Clear()
        Lv_BahanBaku.Columns.Add("Kode Barang", 230, HorizontalAlignment.Left)
        'Lv_BahanBaku.Columns.Add("Serial Number", 0, HorizontalAlignment.Left)
        Lv_BahanBaku.Columns.Add("Jumlah", 250, HorizontalAlignment.Right)
        Lv_BahanBaku.Columns.Add("Harga", 200, HorizontalAlignment.Right)
        Lv_BahanBaku.Columns.Add("Total", 250, HorizontalAlignment.Right)
        Lv_BahanBaku.View = View.Details





        Dim akses_bahan_baku As Boolean = True

        Try
            OpenConn()

            If CekButtonRole("View_Validasi_HPP_Bahan_Baku") = "T" Then
                akses_bahan_baku = False
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        If akses_bahan_baku = True Then
            Lv_BahanBaku.Visible = True
        Else
            Lv_BahanBaku.Visible = False
        End If

        LoadLvBahanBaku()
        LoadPackaging()
        LoadProduksi()

    End Sub

    Private Sub LoadLvBahanBaku()

        Try
            OpenConn()
            Dim Total As Double = 0

            Lv_BahanBaku.Items.Clear()
            SQL = "select a.No_Transaksi, a.No_Production_Order as no_split, b.Kode_Barang, b.Serial_Number, SUM(b.nilai) as Nilai, "
            SQL = SQL & "ISNULL(( select dbo.get_hpp(b.Serial_Number)), 0) as Harga_HPP, "
            SQL = SQL & "ISNULL(( sum(b.Nilai) * (select dbo.get_hpp(b.Serial_Number))), 0) as Harga_BahanBaku, c.satuan "
            SQL = SQL & "from Emi_Production_Results a, Emi_Production_Results_det b, Emi_Production_Results_Detail c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and a.No_Transaksi = b.No_Transaksi  "
            SQL = SQL & "and b.No_Transaksi = c.No_Transaksi and b.No_Urut_Detail = c.Urut "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.status is null "
            SQL = SQL & "and a.No_Production_Order = '" & noSplit & "' "
            SQL = SQL & "and c.Proses = '" & Proses & "' "
            SQL = SQL & "GROUP BY a.No_Transaksi, a.No_Production_Order, b.Kode_Barang, b.Serial_Number, dbo.get_hpp(b.Serial_Number), c.satuan "
            SQL = SQL & "order by b.Kode_Barang, c.satuan "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read()
                    Dim Lv As ListViewItem
                    Lv = Lv_BahanBaku.Items.Add(Dr("Kode_Barang"))
                    'Lv.SubItems.Add(Dr("Serial_Number"))
                    Lv.SubItems.Add(Format(Val(HilangkanTanda(Dr("nilai"))), "N2") & " " & Dr("satuan"))
                    Lv.SubItems.Add(Format(Val(HilangkanTanda(Dr("Harga_HPP"))), "N2"))
                    Lv.SubItems.Add(Format(Val(HilangkanTanda(Dr("Harga_BahanBaku"))), "N2"))

                    Total += Val(HilangkanTanda(Dr("Harga_BahanBaku")))
                Loop
            End Using

            Txt_TotalBahanBaku.Text = Format(Total, "N2")

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub LoadPackaging()
        'Try
        '    OpenConn()
        '    Dim Total As Double = 0

        '    Lv_Packaging.Items.Clear()
        '    SQL = "select a.No_Transaksi, a.No_Production_Order as no_split, b.Kode_Barang, b.Serial_Number, sum(b.nilai) as Nilai, "
        '    SQL = SQL & "ISNULL(( select dbo.get_hpp(b.Serial_Number)), 0) as Harga_HPP, "
        '    SQL = SQL & "ISNULL(( sum(b.Nilai) * (select dbo.get_hpp(b.Serial_Number))), 0) as Harga_BahanBaku, c.satuan "
        '    SQL = SQL & "from Emi_Production_Results a, Emi_Production_Results_Packaging_Det b, Emi_Production_Results_Packaging_Detail c "
        '    SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
        '    SQL = SQL & "and a.No_Transaksi = b.No_Transaksi  "
        '    SQL = SQL & "and b.No_Transaksi = c.No_Transaksi and b.No_Urut_Detail = c.Urut "
        '    SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
        '    SQL = SQL & "and a.status is null "
        '    SQL = SQL & "and a.No_Production_Order = '" & noSplit & "' "
        '    SQL = SQL & "and c.Proses = '" & Proses & "' "
        '    SQL = SQL & "GROUP BY a.No_Transaksi, a.No_Production_Order, b.Kode_Barang, b.Serial_Number, dbo.get_hpp(b.Serial_Number), c.satuan "
        '    SQL = SQL & "order by b.Kode_Barang, c.satuan "
        '    Using Dr = OpenTrans(SQL)
        '        Do While Dr.Read()
        '            Dim Lv As ListViewItem
        '            Lv = Lv_Packaging.Items.Add(Dr("Kode_Barang"))
        '            'Lv.SubItems.Add(Dr("Serial_Number"))
        '            Lv.SubItems.Add(Format(Val(HilangkanTanda(Dr("nilai"))), "N2") & " " & Dr("satuan"))
        '            Lv.SubItems.Add(Format(Val(HilangkanTanda(Dr("Harga_HPP"))), "N2"))
        '            Lv.SubItems.Add(Format(Val(HilangkanTanda(Dr("Harga_BahanBaku"))), "N2"))

        '            Total += Val(HilangkanTanda(Dr("Harga_BahanBaku")))
        '        Loop
        '    End Using

        '    Txt_TotalPackaging.Text = Format(Total, "N2")


        '    CloseConn()
        'Catch ex As Exception
        '    CloseConn()
        '    MessageBox.Show(ex.Message)
        '    Exit Sub
        'End Try
    End Sub

    Private Sub LoadProduksi()
        'Try
        '    OpenConn()
        '    Dim Total As Double = 0

        '    Lv_Produksi.Items.Clear()
        '    SQL = "select a.No_Transaksi, a.No_Production_Order as no_split, c.Kode_Jenis_Biaya, e.keterangan as Jenis_Biaya, c.ID_Work_Center, d.Keterangan as Mesin, b.Jumlah_Dosing, c.Nilai, "
        '    SQL = SQL & "ISNULL(( b.Jumlah_Dosing * c.Nilai ), 0) as Nilai_Produksi "
        '    SQL = SQL & "from Emi_Production_Results a, Emi_Production_Results_HPP b, Emi_Production_Results_HPP_Detail_Work_Center c, EMI_Master_Work_Center d, Emi_Jenis_Biaya_Produksi e "
        '    SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan and c.Kode_Perusahaan = e.Kode_Perusahaan "
        '    SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
        '    SQL = SQL & "and b.No_Transaksi = c.No_Transaksi and b.Proses = c.Proses "
        '    SQL = SQL & "and c.ID_Work_Center = d.Id_Work_Center "
        '    SQL = SQL & "and c.Kode_Jenis_Biaya = e.Kode_Jenis_Biaya_Produksi "
        '    SQL = SQL & "and a.Status is null "
        '    SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
        '    SQL = SQL & "and a.No_Production_Order = '" & noSplit & "' "
        '    SQL = SQL & "and b.Proses = '" & Proses & "' "
        '    SQL = SQL & "order by c.Kode_Jenis_Biaya ,c.ID_Work_Center "
        '    Using Dr = OpenTrans(SQL)
        '        Do While Dr.Read()
        '            Dim Lv As ListViewItem
        '            Lv = Lv_Produksi.Items.Add(Dr("Jenis_Biaya"))
        '            Lv.SubItems.Add(Dr("Mesin"))
        '            Lv.SubItems.Add(Format(Val(HilangkanTanda(Dr("Jumlah_Dosing"))), "N2") & " Kg")
        '            Lv.SubItems.Add(Format(Val(HilangkanTanda(Dr("Nilai"))), "N2"))
        '            Lv.SubItems.Add(Format(Val(HilangkanTanda(Dr("Nilai_Produksi"))), "N2"))

        '            Total += Val(HilangkanTanda(Dr("Nilai_Produksi")))
        '        Loop
        '    End Using

        '    Txt_TotalProduksi.Text = Format(Total, "N2")


        '    CloseConn()
        'Catch ex As Exception
        '    CloseConn()
        '    MessageBox.Show(ex.Message)
        '    Exit Sub
        'End Try
    End Sub


End Class

Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button


Public Class EMI_Pra_Produksi_Display
    Dim arrcari As New ArrayList
    Dim Jenis = "Master_Customer"

    Private Sub Display_Customer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
        Try

            OpenConn()

            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)

            Btn_Cari.Text = Base_Language.Lang_Global_Refresh
            Label1.Text = Base_Language.Lang_Global_List_Inquiry_PO


            ListView1.Columns.Add(Base_Language.Lang_Global_NoFaktur, 130, HorizontalAlignment.Left)
            ListView1.Columns.Add(Base_Language.Lang_Global_KodeCustomer, 0, HorizontalAlignment.Left)
            ListView1.Columns.Add(Base_Language.Lang_Global_Nama, 220, HorizontalAlignment.Left)
            ListView1.Columns.Add(Base_Language.Lang_Global_KodeBarang, 140, HorizontalAlignment.Left)
            ListView1.Columns.Add(Base_Language.Lang_Global_NamaBarang, 220, HorizontalAlignment.Left)
            ListView1.Columns.Add(Base_Language.Lang_Global_Tanggal, 120, HorizontalAlignment.Center)
            ListView1.Columns.Add(Base_Language.Lang_Global_Bahan_Baku, 120, HorizontalAlignment.Center)
            ListView1.Columns.Add(Base_Language.Lang_Global_Bahan_Penolong, 120, HorizontalAlignment.Center)
            ListView1.Columns.Add(Base_Language.Lang_Global_Bahan_Pengiriman, 120, HorizontalAlignment.Center)
            ListView1.View = View.Details


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Btn_Cari_Click(Me, Nothing)

    End Sub

    Private Sub Display_Customer_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
        Cari()
    End Sub

    Private Sub Cari()
        Try
            OpenConn()

            ListView1.Items.Clear()
            SQL = "select a.NO_FAKTUR,a.KODE_CUSTOMER,a.NAMA,b.kode_barang,c.Nama as nama_produk, a.TANGGAL, b.Flag_Bahan_Baku, b.Flag_Bahan_Penolong, b.Flag_Pengiriman from View_PO a, View_PO_Detail b, Barang c "
            SQL = SQL & "where a.KODE_PERUSAHAAN = b.Kode_Perusahaan and a.NO_FAKTUR = b.No_Faktur "
            SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Stock_Owner = c.Kode_Stock_Owner  "
            SQL = SQL & "and b.kode_barang = c.kode_barang "
            SQL = SQL & "and (b.Flag_Bahan_Baku is null or b.Flag_Bahan_Penolong is null or b.Flag_Pengiriman is null) "
            SQL = SQL & "order by No_Faktur"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim Lvw As ListViewItem
                    Lvw = ListView1.Items.Add(dr("no_faktur"))
                    Lvw.SubItems.Add(dr("kode_customer"))
                    Lvw.SubItems.Add(dr("nama"))
                    Lvw.SubItems.Add(dr("kode_barang"))
                    Lvw.SubItems.Add(dr("nama_produk"))
                    Lvw.SubItems.Add(Format(dr("tanggal"), "dd MMM yyyy"))

                    If General_Class.CekNULL(dr("Flag_Bahan_Baku")) = "Y" Then
                        Lvw.SubItems.Add(dr("Flag_Bahan_Baku"))
                    Else
                        Lvw.SubItems.Add("-")
                    End If

                    If General_Class.CekNULL(dr("Flag_Bahan_Penolong")) = "Y" Then
                        Lvw.SubItems.Add(dr("Flag_Bahan_Penolong"))
                    Else
                        Lvw.SubItems.Add("-")
                    End If

                    If General_Class.CekNULL(dr("Flag_Pengiriman")) = "Y" Then
                        Lvw.SubItems.Add(dr("Flag_Pengiriman"))
                    Else
                        Lvw.SubItems.Add("-")
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


End Class
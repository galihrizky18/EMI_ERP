Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button


Public Class EMI_Display_Barcode
    Public filter_tambahan As String
    Public asal As String
    Dim arrcari, arrIdPenanggungJawab As New ArrayList
    Dim Jenis = "Binding_Barcode"

    Dim lvNoInquiry As String
    Dim lvLokasi As String
    'Dim lvTanggal As String
    Dim lvKdBrg As String
    Dim lvUrut As String

    Dim CellNoInquiry As Integer = 0
    Dim CellLokasi As Integer = 1
    'Dim cellTanggal As Integer = 2
    Dim cellKdBrg As Integer = 2
    Dim cellUrut As Integer = 3

    Public Sub Get_Isi_Listview(ByVal No_Index As Integer)

        lvNoInquiry = DgvInquiry_DataInquiry.Rows(No_Index).Cells(CellNoInquiry).Value.ToString
        lvLokasi = DgvInquiry_DataInquiry.Rows(No_Index).Cells(CellLokasi).Value.ToString
        'lvTanggal = DgvInquiry_DataInquiry.Rows(No_Index).Cells(cellTanggal).Value.ToString
        lvKdBrg = DgvInquiry_DataInquiry.Rows(No_Index).Cells(cellKdBrg).Value.ToString
        lvUrut = DgvInquiry_DataInquiry.Rows(No_Index).Cells(cellUrut).Value.ToString

    End Sub

    Private Sub Get_Data()

        If CmbInquiry_Lokasi.SelectedIndex = -1 Then
            MessageBox.Show(Base_Language.Lang_Global_Error_Lokasi, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            CmbInquiry_Lokasi.Focus()
            Exit Sub
        End If

        Try
            OpenConn()

            DgvInquiry_DataInquiry.Rows.Clear()
            Dim index As Integer = 0
            SQL = "select b.No_Faktur,b.Kode_Stock_Owner,b.Kode_Barang,b.No_Urut "
            SQL = SQL & "from Emi_Inquiry a,Emi_Inquiry_Detail b, Barang c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and b.Kode_Stock_Owner = c.Kode_Stock_Owner and b.Kode_Barang = C.Kode_Barang_Inq "
            SQL = SQL & "and a.Status is null and b.Flag_Barcode is null  "
            SQL = SQL & "and c.Flag_Barcode is null"

            If CmbInquiry_Lokasi.SelectedIndex = 0 Then
                SQL = SQL & " and a.Lokasi IN("
                Dim list_Lokasi As String = ""
                For x As Integer = 1 To CmbInquiry_Lokasi.Items.Count - 1
                    list_Lokasi = list_Lokasi & "'" & CmbInquiry_Lokasi.Items(x).ToString & "', "
                Next

                list_Lokasi = Strings.Left(list_Lokasi, Len(list_Lokasi) - 2)

                SQL = SQL & list_Lokasi & ")"
            Else
                SQL = SQL & "and a.Lokasi = '" & CmbInquiry_Lokasi.Text & "' "
            End If

            If TxtInquiry_Cari.Text.Trim.Length <> 0 Then
                SQL = SQL & "and c.Kode_Barang_Inq like '%" & TxtInquiry_Cari.Text & "%' "
            End If
            SQL = SQL & "order by c.Kode_Barang_Inq "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    DgvInquiry_DataInquiry.Rows.Add(1)
                    DgvInquiry_DataInquiry.Rows(index).Cells(CellNoInquiry).Value = dr("No_Faktur")
                    DgvInquiry_DataInquiry.Rows(index).Cells(CellLokasi).Value = dr("Kode_Stock_Owner")
                    DgvInquiry_DataInquiry.Rows(index).Cells(cellKdBrg).Value = dr("Kode_Barang")
                    DgvInquiry_DataInquiry.Rows(index).Cells(cellUrut).Value = dr("No_Urut")

                    index += 1
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub CmbInquiry_Lokasi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbInquiry_Lokasi.SelectedIndexChanged
        Get_Data()
    End Sub

    Public Sub BtnInquiry_Cari_Click(sender As Object, e As EventArgs) Handles BtnInquiry_Cari.Click
        Get_Data()
    End Sub

    Private Sub BtnInquiry_Refresh_Click(sender As Object, e As EventArgs) Handles BtnInquiry_Refresh.Click
        CmbInquiry_Lokasi.SelectedIndex = 0
        TxtInquiry_Cari.Text = ""
        Get_Data()
    End Sub

    Private Sub Transaksi_Formulator_Pilih_Barang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            OpenConn()

            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)

            LblInquiry_Lokasi.Text = Base_Language.Lang_Global_Lokasi
            LblInquiry_Customer.Text = Base_Language.Lang_Global_KodeBarang
            LblInquiry_Judul.Text = Base_Language.Lang_Binding_Barcode_Judul_SD

            BtnInquiry_Cari.Text = Base_Language.Lang_Global_Cari

            DgvInquiry_DataInquiry.Columns(CellNoInquiry).HeaderText = Base_Language.Lang_Global_NoInquiry
            DgvInquiry_DataInquiry.Columns(CellLokasi).HeaderText = Base_Language.Lang_Global_Lokasi
            DgvInquiry_DataInquiry.Columns(cellKdBrg).HeaderText = Base_Language.Lang_Global_KodeBarang

            CmbInquiry_Lokasi.Items.Clear()
            CmbInquiry_Lokasi.Items.Add(Base_Language.Lang_Global_SeluruhCombobox)
            SQL = "select Kode_Stock_Owner from stock_owner where Aktif='Y' "
            SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    CmbInquiry_Lokasi.Items.Add(dr("Kode_Stock_Owner"))
                Loop
            End Using

            CmbInquiry_Lokasi.SelectedIndex = 0

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        CmbInquiry_Lokasi.Focus()
    End Sub

    Private Sub DgvInquiry_DataInquiry_DoubleClick(sender As Object, e As EventArgs) Handles DgvInquiry_DataInquiry.DoubleClick
        If DgvInquiry_DataInquiry.Rows.Count = 0 Or DgvInquiry_DataInquiry.SelectedRows.Count = 0 Then
            MessageBox.Show(Base_Language.Lang_Global_Data_Tdk_Ditemukan, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        'If asal = "Binding_Barcode" Then
        Transaksi_Binding_Barcode.TextBox1.Text = DgvInquiry_DataInquiry.CurrentRow.Cells(cellKdBrg).Value
        Transaksi_Binding_Barcode.urut = DgvInquiry_DataInquiry.CurrentRow.Cells(cellUrut).Value
        Transaksi_Binding_Barcode.ShowDialog()
        'Else
        '    MessageBox.Show(Base_Language.Lang_Global_FormAsal & " . .!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '    Exit Sub
        'End If

    End Sub

End Class
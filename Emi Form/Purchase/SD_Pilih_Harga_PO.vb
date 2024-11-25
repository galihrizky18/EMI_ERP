Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button

Public Class SD_Pilih_Harga_PO

    Dim arrIndex, arrNoPenawaran, arrSatuanPenawaran, arrHargaPenawaran, arrHarga As New ArrayList
    Dim no_formula, no_inquiry, kode_customer, kode_barang As String

    Public kodeSupplier As String
    Public kodeBarang As String
    Public rowDgv As Integer
    Public cellDgv, cellNoPenawaran, cellSatuanHarga, cellHargaID As Integer

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If cmbHarga.Text.Trim.Length = 0 Then
            MessageBox.Show("Harga harus di isi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        EMI_PO_Pembelian_Display_User.Dgv_Pr.Rows(rowDgv).Cells(cellDgv).Value = Format(arrHarga(cmbHarga.SelectedIndex), "N2")
        EMI_PO_Pembelian_Display_User.Dgv_Pr.Rows(rowDgv).Cells(cellNoPenawaran).Value = arrNoPenawaran.Item(cmbHarga.SelectedIndex)
        EMI_PO_Pembelian_Display_User.Dgv_Pr.Rows(rowDgv).Cells(cellSatuanHarga).Value = arrSatuanPenawaran.Item(cmbHarga.SelectedIndex)
        EMI_PO_Pembelian_Display_User.Dgv_Pr.Rows(rowDgv).Cells(cellHargaID).Value = arrHargaPenawaran.Item(cmbHarga.SelectedIndex)
        Me.Close()

    End Sub



    Private Sub SD_Formulator_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        kosong()

    End Sub


    Private Sub kosong()

        Try
            OpenConn()

            cmbHarga.Items.Clear()
            Dim dgvcc As DataGridViewComboBoxCell
            cmbHarga.Items.Clear() : arrHargaPenawaran.Clear()
            arrSatuanPenawaran.Clear() : arrNoPenawaran.Clear() : arrHarga.Clear()
            SQL = "select a.No_Faktur,a.no_penawaran,a.Kode_Supplier, c.Nama, b.harga_satuan,b.satuan_barang,b.nilai_barang, b.satuan from EMI_Master_Penawaran a, EMI_Master_Penawaran_Detail b, Suppliers c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and a.Kode_Perusahaan = c.Kode_Perusahaan and a.Kode_Supplier = c.Kode_Supplier "
            SQL = SQL & "and b.kode_barang = '" & kodeBarang & "' and a.Kode_Supplier='" & kodeSupplier & "'"
            Using dr2 = OpenTrans(SQL)
                Do While dr2.Read

                    cmbHarga.Items.Add(dr2("harga_satuan") & " / " & dr2("satuan") & " - " & dr2("Nama")) : arrNoPenawaran.Add(dr2("no_penawaran"))
                    arrSatuanPenawaran.Add(dr2("satuan_Barang")) : arrHargaPenawaran.Add(dr2("Nilai_Barang"))
                    arrHarga.Add(dr2("harga_satuan"))
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
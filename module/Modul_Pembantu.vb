Imports System.Text

Public Class Modul_Pembantu

    Private random As New Random()
    Private Tahun_MulaiProduksi As String = "2024"

    Private Function Generate_Batch_New(ByVal productionDate As String, ByVal lineCode As String, ByVal expDate As String) As String

        Dim productionTime As Date = Date.Parse(productionDate)
        Dim Produksi_Tanggal As String = productionTime.Day.ToString
        Dim Produksi_Bulan As String = productionTime.Month.ToString
        Dim Produksi_Tahun As String = If((productionTime.Year - Tahun_MulaiProduksi) Mod 9 = 0, 1, (productionTime.Year - Tahun_MulaiProduksi) Mod 9)
        Dim exp_date As String = Format(Date.Parse(expDate), "ddMMyy")

        Dim NumberToChar As New ArrayList From {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L",
                                        "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"}
        Dim finalBatch As String = ""
        finalBatch = Produksi_Tanggal & NumberToChar(Produksi_Bulan - 1) & Produksi_Tahun & lineCode & exp_date

        Return finalBatch

    End Function


    Private Function Generate_QR(ByVal MaterialCode As String, ByVal BatchCode As String) As String

        'Dim chars As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"
        'Dim UnixCode As New StringBuilder()

        'For i As Integer = 1 To 10
        '    Dim index As Integer = random.Next(0, chars.Length)
        '    UnixCode.Append(chars(index))
        'Next

        Dim Qr As String = ""
        Qr = MaterialCode & "-" & BatchCode

        Return Qr
    End Function

    Private Function Generate_Random_Kode(ByVal length As Integer) As String
        Dim chars As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"
        Dim result As New StringBuilder()

        For i As Integer = 1 To length
            Dim index As Integer = random.Next(0, chars.Length)
            result.Append(chars(index))
        Next

        Return result.ToString()
    End Function


    Private Function Generate_New_Sn(ByVal SerialNumber As String) As String
        'GENERATE SN BARU
        Dim hargaIsn As String = Get_Harga_SN(SerialNumber)

        Dim Random As New Random()
        Dim str As String = Format(Random.Next(0, 999), "000") & Format(CDate(FMenu.ToolStripStatusLabel3.Text), "HHmmss")
        Dim Kode_Unik As String = str.Substring(0, 5) & "BB" & Chr(64 + str.Substring(6, 1)) & str.Substring(6, Len(str) - 6)
        Dim SN_Baru As String = Kode_Unik & Tanda_SN & "01" & Tanda_SN & hargaIsn & Tanda_SN & "02" & Tanda_SN & Format(DateTime.Now, "yyyy-MM-dd")

        Return SN_Baru
    End Function


    Private Function Get_Rak_Kosong() As (String, String)

        Dim available_Id_Warehouse As String = ""
        Dim available_NoPallet As String = ""

        SQL = "select top(1) id_wms_warehouse_position, nomor_urut from view_warehouse_position_detail where kode_barang is null "
        Using Dr2 = OpenTrans(SQL)
            Do While Dr2.Read
                available_Id_Warehouse = Dr2("id_wms_warehouse_position")
                available_NoPallet = Dr2("nomor_urut")
            Loop
        End Using

        Return (available_Id_Warehouse, available_NoPallet)
    End Function

    Private Sub CellEndEdit()
        ''======================
        ''=     SET FORMAT     =
        ''======================

        'If Dgv_DataBarang.CurrentCell.ColumnIndex = CellQty Then

        '    Dim cellKuantity As String = Dgv_DataBarang.CurrentRow.Cells(CellQty).Value

        '    If cellKuantity.Contains(",") Then
        '        MessageBox.Show("Kuantity Tidak Boleh Koma, Ganti dengan Titik", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '        Dgv_DataBarang.CurrentRow.Cells(CellQty).Value = Format(0, "N2")
        '        Exit Sub
        '    End If

        '    Dim nilai As Decimal = Decimal.Parse(cellKuantity)
        '    Dim formattedValue As String = nilai.ToString("N2", Globalization.CultureInfo.GetCultureInfo("en-us"))

        '    Dgv_DataBarang.CurrentRow.Cells(CellQty).Value = formattedValue
        'End If
    End Sub

    Private Sub CellEnter()
        ''======================
        ''=     SET FORMAT     =
        ''======================

        'If Dgv_DataBarang.CurrentCell.ColumnIndex = CellQty Then
        '    Dim cellKuantity As String = Dgv_DataBarang.CurrentCell.Value

        '    If cellKuantity = "" Then
        '        Exit Sub
        '    End If

        '    Dim cleanedStr As String = HilangkanTanda(cellKuantity) ' Menghapus titik
        '    Dim nilai As Decimal = Decimal.Parse(cleanedStr)

        '    Dgv_DataBarang.CurrentCell.Value = nilai
        'End If
    End Sub

    Private Sub CellLeave()
        ''======================
        ''=     SET FORMAT     =
        ''======================

        'If Dgv_DataBarang.CurrentCell.ColumnIndex = CellQty Then
        '    Dim cellKuantity As String = Dgv_DataBarang.CurrentCell.Value

        '    If cellKuantity = "" Then
        '        Exit Sub
        '    End If


        '    Dim nilai As Decimal = Decimal.Parse(cellKuantity)
        '    Dim formattedValue As String = nilai.ToString("N2", Globalization.CultureInfo.GetCultureInfo("en-us"))

        '    Dgv_DataBarang.CurrentCell.Value = formattedValue

        'End If
    End Sub

End Class

Public Class TSC_Printer_API
    ' Membuka koneksi ke printer (Nama printer sesuai yang ada di Control Panel)
    Declare Sub openport Lib "TSCLib.dll" (ByVal printername As String)

    ' Menutup koneksi
    Declare Sub closeport Lib "TSCLib.dll" ()

    ' Mengatur ukuran label (Lebar, Tinggi, Speed, Density, Sensor, Vertical, Offset)
    Declare Sub setup Lib "TSCLib.dll" (ByVal width As String, ByVal height As String,
            ByVal speed As String, ByVal density As String, ByVal sensor As String,
            ByVal vertical As String, ByVal offset As String)

    ' Membersihkan buffer sebelum mencetak data baru
    Declare Sub clearbuffer Lib "TSCLib.dll" ()

    ' Mengirim perintah TSPL murni (Raw Command)
    Declare Sub sendcommand Lib "TSCLib.dll" (ByVal command As String)

    ' Mencetak label (Jumlah Set, Jumlah Copy)
    Declare Sub printlabel Lib "TSCLib.dll" (ByVal set_num As String, ByVal copy_num As String)

    ' Fungsi untuk font Windows agar bisa tebal/ukuran spesifik
    Declare Sub windowsfont Lib "TSCLib.dll" (ByVal x As Integer, ByVal y As Integer, ByVal height As Integer, ByVal rotation As Integer, ByVal fontstyle As Integer, ByVal withunderline As Integer, ByVal szFaceName As String, ByVal content As String)

    ' Fungsi untuk mengirim file gambar atau data bitmap ke printer
    Declare Sub sendbitmap Lib "TSCLib.dll" (ByVal x As Integer, ByVal y As Integer, ByVal bitmap As Bitmap)
End Class
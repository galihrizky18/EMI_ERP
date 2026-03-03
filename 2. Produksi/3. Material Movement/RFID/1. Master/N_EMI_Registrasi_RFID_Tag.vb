Imports System.IO
Imports ZXing.QrCode

Public Class N_EMI_Registrasi_RFID_Tag
    Private RFIDTagPrefix As String = "CS-"
    Private GeneratedLabel As String = ""
    Private CrDoc As Object

    Private Sub BtnScan_Click(sender As Object, e As EventArgs) Handles Btn_Scan.Click
        If UHF18ReaderHelper.ConnectCH340Port() = False Then
            MessageBox.Show("Reader CH340 tidak ditemukan!")
            Exit Sub
        End If

        Dim epc = UHF18ReaderHelper.ReadSingleTag()

        If epc <> "" Then
            Txt_RFIDTag.Text = epc
        Else
            MessageBox.Show("Gagal membaca tag RFID!")
        End If

        UHF18ReaderHelper.CloseComPort()
    End Sub


    Private Sub Txt_RFIDTag_TextChanged(sender As Object, e As EventArgs) Handles Txt_RFIDTag.TextChanged
        If String.IsNullOrWhiteSpace(Txt_RFIDTag.Text) Then Exit Sub

        Try
            OpenConn()

            SQL = "SELECT RFID_Label 
               FROM N_EMI_Master_Data_RFID_Tags 
               WHERE RFID_Tag = @RFIDTag"

            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@RFIDTag", Txt_RFIDTag.Text.Trim)
            Cmd.CommandText = SQL

            Dim result = Cmd.ExecuteScalar()

            If result IsNot Nothing AndAlso result IsNot DBNull.Value Then
                Txt_RFIDLabel.Text = result.ToString()
                Btn_Simpan.Enabled = False
                GeneratedLabel = result.ToString()
                Exit Sub
            End If

            SQL = $"
                SELECT ISNULL(MAX(CAST(SUBSTRING(RFID_Label, 4, 5) AS INT)), 0) + 1
                FROM N_EMI_Master_Data_RFID_Tags
                WHERE RFID_Label LIKE '{RFIDTagPrefix}%'
            "
            Cmd.Parameters.Clear()
            Cmd.CommandText = SQL

            Dim nextNumber As Integer = Convert.ToInt32(Cmd.ExecuteScalar())
            GeneratedLabel = $"{RFIDTagPrefix}" & nextNumber.ToString("D5")

            Txt_RFIDLabel.Text = GeneratedLabel
            Btn_Simpan.Enabled = True

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
        End Try
    End Sub


    Private Function GenerateRFIDLabel() As String
        SQL = $"
            SELECT ISNULL(MAX(CAST(SUBSTRING(RFID_Label, 4, 5) AS INT)), 0) + 1 
            FROM N_EMI_Master_Data_RFID_Tags
            WHERE RFID_Label LIKE '{RFIDTagPrefix}%'
        "

        Cmd.CommandText = SQL
        Dim nextNumber As Integer = Convert.ToInt32(Cmd.ExecuteScalar())

        Return $"{RFIDTagPrefix}" & nextNumber.ToString("D5")
    End Function

    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click

        If String.IsNullOrWhiteSpace(Txt_RFIDTag.Text) OrElse
       String.IsNullOrWhiteSpace(GeneratedLabel) Then
            MessageBox.Show("RFID belum siap untuk disimpan.", "Warning")
            Exit Sub
        End If

        Dim confirm = MessageBox.Show(
            "Apakah kartu RFID sudah berada di printer?",
            "Konfirmasi Cetak",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question
        )

        If confirm = DialogResult.No Then Exit Sub

        Try
            OpenConn()

            get_jam()

            Cmd.Parameters.Clear()
            Using ImgBarcode1 As Image = Generate_QRRFIDTag(Txt_RFIDTag.Text.Trim)
                Using ms1 As New MemoryStream()
                    ImgBarcode1.Save(ms1, Imaging.ImageFormat.Jpeg)
                    Dim rawData1 As Byte() = ms1.ToArray()

                    Cmd.Parameters.Add("@QR_Code", SqlDbType.Image).Value = rawData1
                End Using
            End Using

            SQL = "
                INSERT INTO N_EMI_Master_Data_RFID_Tags (Kode_Perusahaan, RFID_Tag, RFID_Label, Tanggal, Jam, UserID, QR_Code)
                VALUES (@KodePerusahaan, @RFIDTag, @RFIDLabel, @Tanggal, @Jam, @UserID, @QR_Code)
            "
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("@RFIDTag", Txt_RFIDTag.Text.Trim)
            Cmd.Parameters.AddWithValue("@RFIDLabel", GeneratedLabel)
            Cmd.Parameters.AddWithValue("@UserID", UserID)
            Cmd.Parameters.AddWithValue("@Tanggal", Format(tgl_skg, "yyyy-MM-dd"))
            Cmd.Parameters.AddWithValue("@Jam", Format(tgl_skg, "HH:mm:ss"))
            Cmd.CommandText = SQL
            Cmd.ExecuteNonQuery()

            CetakRFID(Txt_RFIDTag.Text)
            CloseConn()

            Btn_Simpan.Enabled = False
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Public Function Generate_QRRFIDTag(ByVal isi As String) As Bitmap
        Dim options As New ZXing.QrCode.QrCodeEncodingOptions With {
        .DisableECI = True,
        .CharacterSet = "UTF-8",
        .Width = 200,
        .Height = 200,
        .Margin = 0
    }

        Dim qr As New ZXing.BarcodeWriter With {
        .Format = ZXing.BarcodeFormat.QR_CODE,
        .Options = options,
        .Renderer = New ZXing.Rendering.BitmapRenderer With {
            .Foreground = Color.Black,
            .Background = Color.White
        }
    }

        Return qr.Write(isi)
    End Function

    Private Sub CetakRFID(RFIDTag As String)
        Try
            SQL = "SELECT Kode_Perusahaan, RFID_Tag, RFID_Label, QR_Code
               FROM N_EMI_Master_Data_RFID_Tags 
               WHERE Kode_Perusahaan = @KodePerusahaan 
               AND RFID_Tag = @RFIDTag"

            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("@RFIDTag", RFIDTag)

            Using Ds = BindingTrans(SQL)

                If Ds.Tables.Count = 0 OrElse Ds.Tables(0).Rows.Count = 0 Then
                    MessageBox.Show("Data RFID tag tidak ditemukan!",
                                "Informasi",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                Dim CrDoc As New N_EMI_Template_Tags()
                CrDoc.SetDataSource(Ds.Tables(0))
                CrDoc.RecordSelectionFormula = "{N_EMI_Master_Data_RFID_Tags.Kode_Perusahaan} = '" & KodePerusahaan & "' and {N_EMI_Master_Data_RFID_Tags.RFID_Tag}='" & RFIDTag & "' "

                CrDoc.PrintToPrinter(1, False, 0, 0)
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Txt_RFIDTag.Text = ""
        Txt_RFIDLabel.Text = ""
    End Sub
End Class

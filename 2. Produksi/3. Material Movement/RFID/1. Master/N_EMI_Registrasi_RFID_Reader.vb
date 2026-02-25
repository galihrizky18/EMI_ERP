Imports System.Net

Public Class N_EMI_Registrasi_RFID_Reader
    Private Sub Cmb_Kode_Perangkat_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CbKodePerangkat.SelectedIndexChanged
        If CbKodePerangkat.SelectedIndex = -1 Then Exit Sub

        TxtIPAddress.Text = ""
        TxtKeterangan.Text = ""
        CbPower.SelectedIndex = -1

        Try
            OpenConn()

            SQL = "SELECT IP_Address, Power, Keterangan FROM N_EMI_Master_Data_RFID_Readers WHERE Kode_Perusahaan = @KodePerusahaan AND Kode_Perangkat = @KodePerangkat"

            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("@KodePerangkat", CbKodePerangkat.Text)

            Using Dr = OpenTrans(SQL)
                If Dr.Read() Then
                    TxtIPAddress.Text = Dr("IP_Address").ToString()
                    CbPower.Text = Dr("Power").ToString()
                    TxtKeterangan.Text = Dr("Keterangan").ToString()
                End If

                Dr.Close()
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub
    Private Function IsValidIPv4(ip As String) As Boolean
        Dim address As IPAddress = Nothing

        If IPAddress.TryParse(ip, address) Then
            Return address.AddressFamily = Sockets.AddressFamily.InterNetwork
        End If

        Return False
    End Function

    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles BtnSimpan.Click
        Dim IPAddress As String = TxtIPAddress.Text.Trim()
        Dim Power As Integer

        If String.IsNullOrWhiteSpace(IPAddress) Then
            MessageBox.Show("IP Address tidak boleh kosong!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If Not IsValidIPv4(IPAddress) Then
            MessageBox.Show("Format IP Address tidak valid!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If String.IsNullOrWhiteSpace(CbPower.Text) Then
            MessageBox.Show("Power tidak boleh kosong!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            CbPower.Focus()
            Exit Sub
        End If

        If Not Integer.TryParse(CbPower.Text, Power) Then
            MessageBox.Show("Power harus berupa angka!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            CbPower.Focus()
            Exit Sub
        End If

        If Power < 1 OrElse Power > 30 Then
            MessageBox.Show("Power harus antara 0 - 30!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            CbPower.Focus()
            Exit Sub
        End If

        Power = CbPower.Text

        Try
            OpenConn()

            get_jam()

            SQL = "
                IF EXISTS (
                    SELECT 1 
                    FROM N_EMI_Master_Data_RFID_Readers 
                    WHERE Kode_Perusahaan = @KodePerusahaan 
                    AND Kode_Perangkat = @KodePerangkat
                )
                BEGIN
                    UPDATE N_EMI_Master_Data_RFID_Readers
                    SET IP_Address = @IPAddress,
                        Power = @Power,
                        Keterangan = @Keterangan
                    WHERE Kode_Perusahaan = @KodePerusahaan
                    AND Kode_Perangkat = @KodePerangkat
                END
                ELSE
                BEGIN
                    INSERT INTO N_EMI_Master_Data_RFID_Readers
                    (Kode_Perusahaan, Kode_Perangkat, IP_Address, Power, Keterangan, Tanggal, Jam, UserID)
                    VALUES
                    (@KodePerusahaan, @KodePerangkat, @IPAddress, @Power, @Keterangan, @Tanggal, @Jam, @UserID)
                END
            "

            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("@KodePerangkat", CbKodePerangkat.Text)
            Cmd.Parameters.AddWithValue("@IPAddress", IPAddress)
            Cmd.Parameters.AddWithValue("@Power", Power)
            Cmd.Parameters.AddWithValue("@Keterangan", TxtKeterangan.Text.Trim)
            Cmd.Parameters.AddWithValue("@Tanggal", Format(tgl_skg, "yyyy-MM-dd"))
            Cmd.Parameters.AddWithValue("@Jam", Format(tgl_skg, "HH:mm:ss"))
            Cmd.Parameters.AddWithValue("@UserID", UserID)

            ExecuteTrans(SQL)

            MessageBox.Show("Data berhasil disimpan!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)

            CloseConn()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub N_EMI_Registrasi_RFID_Reader_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
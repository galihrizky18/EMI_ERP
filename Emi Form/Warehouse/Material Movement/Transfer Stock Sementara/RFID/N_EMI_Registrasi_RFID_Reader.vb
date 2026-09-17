Imports System.Net

Public Class N_EMI_Registrasi_RFID_Reader
    Private Sub N_EMI_Registrasi_RFID_Reader_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            OpenConn()

            SQL = "SELECT Kode_Perangkat FROM N_EMI_Master_Data_RFID_Readers"

            Using Dr = OpenTrans(SQL)
                While Dr.Read()
                    Cmb_Kode_Perangkat.Items.Add(Dr("Kode_Perangkat").ToString())
                End While

                Dr.Close()
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Cmb_Kode_Perangkat_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Kode_Perangkat.SelectedIndexChanged
        If Cmb_Kode_Perangkat.SelectedIndex = -1 Then Exit Sub

        Try
            OpenConn()

            SQL = "SELECT IP_Address FROM N_EMI_Master_Data_RFID_Readers WHERE Kode_Perusahaan = @KodePerusahaan AND Kode_Perangkat = @KodePerangkat"

            Cmd.Parameters.Clear()
            Cmd.Parameters.AddWithValue("@KodePerusahaan", KodePerusahaan)
            Cmd.Parameters.AddWithValue("@KodePerangkat", Cmb_Kode_Perangkat.Text)

            Using Dr = OpenTrans(SQL)
                If Dr.Read() Then
                    Txt_IP_Address.Text = Dr("IP_Address").ToString()
                End If

                Dr.Close()
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Function IsValidIPv4(ip As String) As Boolean
        Dim address As IPAddress = Nothing

        If IPAddress.TryParse(ip, address) Then
            Return address.AddressFamily = Sockets.AddressFamily.InterNetwork
        End If

        Return False
    End Function

    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
        Dim ip As String = Txt_IP_Address.Text.Trim()

        If String.IsNullOrWhiteSpace(ip) Then
            MessageBox.Show("IP Address tidak boleh kosong!",
                            "Warning",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning)
            Exit Sub
        End If

        If Not IsValidIPv4(ip) Then
            MessageBox.Show("Format IP Address tidak valid!",
                            "Warning",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning)
            Exit Sub
        End If

        Try
            OpenConn()

            SQL = "UPDATE N_EMI_Master_Data_RFID_Readers 
               SET IP_Address = @IPAddress 
               WHERE Kode_Perusahaan = @KodePerusahaan 
               AND Kode_Perangkat = @KodePerangkat"

            Cmd.Parameters.Clear()
            Cmd.Parameters.Add("@KodePerusahaan", SqlDbType.Char, 3).Value = KodePerusahaan
            Cmd.Parameters.Add("@KodePerangkat", SqlDbType.VarChar, 100).Value = Cmb_Kode_Perangkat.Text
            Cmd.Parameters.Add("@IPAddress", SqlDbType.VarChar, 45).Value = ip

            ExecuteTrans(SQL)

            MessageBox.Show("IP Address berhasil diperbarui!",
                            "Informasi",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information)

            CloseConn()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class
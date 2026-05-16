Public Class N_EMI_SD_Tambah_Master_Data_Hierarki_Binding_Formula

    Public Mode As String = "TAMBAH"
    Public KodeHierarkiEdit As String = ""
    Public UrutanEdit As String = ""

    Private Sub N_EMI_SD_Tambah_Master_Data_Hierarki_Binding_Formula_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Mode = "EDIT" Then
            Me.Text = "Edit Hierarki Formula"
            TxtKodeHierarki.Text = KodeHierarkiEdit
            TxtKodeHierarki.Enabled = False
            TxtUrutan.Text = UrutanEdit
        Else
            Me.Text = "Tambah Hierarki Formula"
        End If
    End Sub

    Private Function GetMaxUrutan() As Integer
        Try
            OpenConn()

            SQL = $"
            SELECT ISNULL(MAX(Urutan), 0)  AS MaxUrutan
            FROM N_EMI_Master_Hierarki_Formula
            WHERE Kode_Perusahaan = '{KodePerusahaan}'
        "

            Dim maxUrutan As Integer = 0
            Using Dr = OpenTrans(SQL)
                If Dr.Read() Then
                    maxUrutan = Convert.ToInt32(Dr("MaxUrutan"))
                End If
            End Using

            CloseConn()
            Return maxUrutan
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return 0
        End Try
    End Function

    Private Sub BtnSimpan_Click(sender As Object, e As EventArgs) Handles BtnSimpan.Click
        If String.IsNullOrWhiteSpace(TxtKodeHierarki.Text) Then
            MessageBox.Show("Kode Hierarki tidak boleh kosong.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        If String.IsNullOrWhiteSpace(TxtUrutan.Text) Then
            MessageBox.Show("Urutan tidak boleh kosong.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Dim urutan As Integer
        If Not Integer.TryParse(TxtUrutan.Text.Trim, urutan) Then
            MessageBox.Show("Urutan harus berupa angka.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        If urutan < 1 Then
            MessageBox.Show("Urutan minimal adalah 1.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Dim maxUrutan As Integer = GetMaxUrutan()

        If Mode = "TAMBAH" Then
            If urutan > maxUrutan + 1 Then
                MessageBox.Show($"Urutan maksimal yang diperbolehkan adalah {maxUrutan + 1}.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
            Simpan_Tambah(urutan)

        ElseIf Mode = "EDIT" Then
            If urutan > maxUrutan Then
                MessageBox.Show($"Urutan maksimal yang diperbolehkan adalah {maxUrutan}.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
            Simpan_Edit(urutan)
        End If
    End Sub

    Private Sub Simpan_Tambah(urutanBaru As Integer)
        Try
            OpenConn()

            SQL = $"
            SELECT COUNT(1) AS Jumlah
            FROM N_EMI_Master_Hierarki_Formula
            WHERE Kode_Perusahaan = '{KodePerusahaan}'
            AND Kode_Hierarki = '{TxtKodeHierarki.Text.Trim}'
        "
            Dim jumlah As Integer = 0
            Using Dr = OpenTrans(SQL)
                If Dr.Read() Then
                    jumlah = Convert.ToInt32(Dr("Jumlah"))
                End If
            End Using

            If jumlah > 0 Then
                CloseConn()
                MessageBox.Show("Kode Hierarki sudah ada.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            Cmd.Transaction = Cn.BeginTransaction

            SQL = $"
                UPDATE N_EMI_Master_Hierarki_Formula
                SET Urutan = Urutan + 1
                WHERE Kode_Perusahaan = '{KodePerusahaan}'
                AND Urutan >= {urutanBaru}
            "
            ExecuteTrans(SQL)

            SQL = $"
                INSERT INTO N_EMI_Master_Hierarki_Formula (Kode_Perusahaan, Kode_Hierarki, Urutan)
                VALUES ('{KodePerusahaan}', '{TxtKodeHierarki.Text.Trim}', {urutanBaru})
            "
            ExecuteTrans(SQL)

            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()

            MessageBox.Show("Data berhasil disimpan.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Close()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Simpan_Edit(urutanBaru As Integer)
        Try
            OpenConn()

            SQL = $"
                SELECT Urutan
                FROM N_EMI_Master_Hierarki_Formula
                WHERE Kode_Perusahaan = '{KodePerusahaan}'
                AND Kode_Hierarki = '{KodeHierarkiEdit}'
            "
            Dim urutanLama As Integer = 0
            Using Dr = OpenTrans(SQL)
                If Dr.Read() Then
                    urutanLama = Convert.ToInt32(Dr("Urutan"))
                End If
            End Using

            Cmd.Transaction = Cn.BeginTransaction

            If urutanBaru <> urutanLama Then
                If urutanBaru > urutanLama Then
                    SQL = $"
                    UPDATE N_EMI_Master_Hierarki_Formula
                    SET Urutan = Urutan - 1
                    WHERE Kode_Perusahaan = '{KodePerusahaan}'
                    AND Urutan > {urutanLama}
                    AND Urutan <= {urutanBaru}
                "
                Else
                    SQL = $"
                    UPDATE N_EMI_Master_Hierarki_Formula
                    SET Urutan = Urutan + 1
                    WHERE Kode_Perusahaan = '{KodePerusahaan}'
                    AND Urutan >= {urutanBaru}
                    AND Urutan < {urutanLama}
                "
                End If
                ExecuteTrans(SQL)
            End If

            SQL = $"
            UPDATE N_EMI_Master_Hierarki_Formula
            SET Urutan = {urutanBaru}
            WHERE Kode_Perusahaan = '{KodePerusahaan}'
            AND Kode_Hierarki = '{KodeHierarkiEdit}'
        "
            ExecuteTrans(SQL)

            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()

            MessageBox.Show("Data berhasil diperbarui.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Close()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub
End Class
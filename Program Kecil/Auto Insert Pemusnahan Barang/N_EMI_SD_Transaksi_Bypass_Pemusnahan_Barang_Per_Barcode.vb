Public Class N_EMI_SD_Transaksi_Bypass_Pemusnahan_Barang_Per_Barcode

    Dim arrKdSo As New ArrayList
    Private Sub N_EMI_SD_Transaksi_Bypass_Pemusnahan_Barang_Per_Barcode_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Kosong()

    End Sub

    Private Sub Kosong()
        LoadData()
    End Sub

    Private Sub LoadData()

        Try
            OpenConn()

            Dgv_Data.Rows.Clear()
            SQL = "select a.Barcode, a.Jumlah, b.Kode_Stock_Owner, b.Kode_Barang "
            SQL &= $"from N_EMI_Table_Set_Barcode_Pengajuan_Waste a "
            SQL &= $"inner join Barang_SN b on (b.Qr_Code+'-'+b.Kode_Unik_Berjalan) = a.Barcode "
            SQL &= $"where a.Flag_Sudah_Input is null "
            SQL &= $"and b.Kode_Perusahaan = '{KodePerusahaan}' "
            SQL &= $"and b.Flag_Pengajuan_Waste is null "
            SQL &= $"group by a.Barcode, a.Jumlah, b.Kode_Stock_Owner, b.Kode_Barang "
            SQL &= $"order by a.Barcode "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            Dgv_Data.Rows.Add(1)
                            Dgv_Data.Rows(i).Cells(0).Value = .Rows(i).Item("Kode_Stock_Owner")
                            Dgv_Data.Rows(i).Cells(1).Value = .Rows(i).Item("Kode_Barang")
                            Dgv_Data.Rows(i).Cells(2).Value = .Rows(i).Item("Barcode")
                            Dgv_Data.Rows(i).Cells(3).Value = False
                            Dgv_Data.Rows(i).Cells(4).Value = Format(Val(HilangkanTanda(.Rows(i).Item("Jumlah"))), "N4")
                            Dgv_Data.Rows(i).Cells(5).Value = Format(Val(HilangkanTanda(.Rows(i).Item("Jumlah"))), "N4")


                        Next
                    End If
                End With
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Dgv_Data_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_Data.CellEndEdit
        If Dgv_Data.Rows.Count = 0 Then Exit Sub

        If Dgv_Data.CurrentRow.Cells(3).Value = True Then
            Dgv_Data.CurrentRow.Cells(4).ReadOnly = False

            If Not IsNumeric(Dgv_Data.CurrentRow.Cells(4).Value) Then
                Dgv_Data.CurrentRow.Cells(4).Value = Format(Val(HilangkanTanda(0)), "N4")
            Else
                Dgv_Data.CurrentRow.Cells(4).Value = Format(Val(HilangkanTanda(Dgv_Data.CurrentRow.Cells(4).Value)), "N4")
            End If
        Else
            Dgv_Data.CurrentRow.Cells(4).ReadOnly = True
            Dgv_Data.CurrentRow.Cells(4).Value = Format(Val(HilangkanTanda(Dgv_Data.CurrentRow.Cells(5).Value)), "N4")
        End If
    End Sub

    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
        If Dgv_Data.Rows.Count = 0 Then Exit Sub

        If MessageBox.Show("Yakin ingin melakukan simpan?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = vbNo Then Exit Sub

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            For i As Integer = 0 To Dgv_Data.Rows.Count - 1
                If Dgv_Data.Rows(i).Cells(3).Value = False Then
                    Continue For
                End If


                Dim sisaPotong As Double = 0
                Dim JumlahDipotong As Double = 0
                SQL = "select a.Jumlah as Stock_SN, a.serial_number "
                SQL = SQL & "from Barang_SN a where "
                SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and a.qr_Code+'-'+a.kode_unik_berjalan = '" & Dgv_Data.Rows(i).Cells(2).Value & "' "
                SQL = SQL & "and a.Kode_stock_owner = '" & Dgv_Data.Rows(i).Cells(0).Value & "' and a.jumlah<>0 "
                SQL = SQL & "and a.Flag_Pengajuan_Waste is null and a.Flag_Sdh_Pengajuan_Waste is null "
                SQL = SQL & "order by a.Tgl_Expired "
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        If .Rows.Count <> 0 Then

                            sisaPotong = Val(HilangkanTanda(Dgv_Data.Rows(i).Cells(4).Value))

                            For Index As Integer = 0 To .Rows.Count - 1
                                If sisaPotong = 0 Then
                                    Exit For
                                ElseIf sisaPotong < 0 Then
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Terdapat Kesalahan saat Potong Barang Produksi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If

                                Dim JumlahInsert As Double = 0
                                Dim Satuan As String = ""

                                Dim Data_SN As String = .Rows(Index).Item("serial_number")

                                If sisaPotong < Val(HilangkanTanda(.Rows(Index).Item("Stock_SN"))) Or sisaPotong = Val(HilangkanTanda(.Rows(Index).Item("Stock_SN"))) Then

                                    JumlahInsert = sisaPotong
                                    ' Satuan = .Rows(Index).Item("Satuan").ToString.Trim


                                    JumlahDipotong += sisaPotong
                                    sisaPotong = 0

                                ElseIf sisaPotong > Val(HilangkanTanda(.Rows(Index).Item("Stock_SN"))) Then

                                    JumlahInsert = Val(HilangkanTanda(Format(.Rows(Index).Item("Stock_SN"), "N4")))
                                    'Satuan = .Rows(Index).Item("Satuan").ToString.Trim

                                    JumlahDipotong += Val(HilangkanTanda(Format(.Rows(Index).Item("Stock_SN"), "N4")))
                                    sisaPotong = sisaPotong - Val(HilangkanTanda(Format(.Rows(Index).Item("Stock_SN"), "N4")))
                                Else
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Terjadi Kesalaham pada Barang SN untuk Kode Barang !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If


                                SQL = $"update Barang_SN set Flag_Pengajuan_Waste = 'Y', Qty_Pengajuan_Waste = '{Val(HilangkanTanda(JumlahInsert))}' "
                                SQL &= $"where kode_perusahaan = '{KodePerusahaan}' "
                                SQL &= $"and kode_stock_owner = '{Dgv_Data.Rows(i).Cells(0).Value}' "
                                SQL &= $"and kode_barang = '{Dgv_Data.Rows(i).Cells(1).Value}' "
                                SQL &= $"and Serial_Number = '{Data_SN}' "
                                SQL &= $"and Flag_Pengajuan_Waste is null "
                                SQL &= $"and Flag_Sdh_Pengajuan_Waste is null "
                                ExecuteTrans(SQL)
                            Next
                        Else
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Terjadi Kesalahan Pada Barang !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End With
                End Using

                If Val(HilangkanTanda(Dgv_Data.Rows(i).Cells(4).Value)) <> Val(HilangkanTanda(JumlahDipotong)) Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show($"Stock pada barcode {Dgv_Data.Rows(i).Cells(2).Value} kurang dari jumlah pengajuan waste", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

            Next



            Cmd.Transaction.Commit()
            CloseConn()
            MessageBox.Show("Data Berhasil Disimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Kosong()
        Me.Close()
    End Sub
End Class
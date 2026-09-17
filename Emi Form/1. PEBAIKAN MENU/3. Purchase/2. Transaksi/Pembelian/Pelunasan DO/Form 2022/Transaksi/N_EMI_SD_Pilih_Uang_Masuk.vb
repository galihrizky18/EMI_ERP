Public Class N_EMI_SD_Pilih_Uang_Masuk
    Dim arrCrByr, ArrAkunCB1 As New ArrayList


    Dim LvNo_Val As String
    Dim LvTangal As String
    Dim LvSisa As String
    Dim LvNilai_Input As String
    Dim LvUrut_Um As String

    Public CellNo_Val As Integer
    Public CellTanggal As Integer
    Public CellSisa As Integer
    Public CellNilai_Input As Integer
    Public CellUrut_Um As Integer


    Dim LvNo_ValDPT As String
    Dim LvTangalDPT As String
    Dim LvSisaDPT As String
    Dim LvNilai_InputDPT As String
    Dim LvUrut_DPT As String
    Dim LvPPN_DPT As String
    Dim LvPPH_DPT As String
    Dim LvNilaiKlaim_DPT As String
    Dim LvNilaiPPN_DPT As String
    Dim LvNilaiPPH_DPT As String
    Dim LvFlagB2B_DPT As String
    Dim LvAsal_DPT As String
    Dim LvJenisKlaim_DPT As String
    Dim LvNama_DPT As String

    Public CellNo_ValDPT As Integer = 0
    Public CellTanggalDPT As Integer = 1
    Public CellSisaDPT As Integer = 2
    Public CellPPN_DPT As Integer = 3
    Public CellPPH_DPT As Integer = 4
    Public CellNilai_InputDPT As Integer = 5
    Public CellNilaiPPN_DPT As Integer = 6
    Public CellNilaiPPH_DPT As Integer = 7
    Public CellNilaiKlaim_DPT As Integer = 8
    Public CellUrut_DPT As Integer = 9
    Public CellFlagB2B_DPT As Integer = 10
    Public CellAsal_DPT As Integer = 11
    Public CellJenisKlaim_DPT As Integer = 12
    Public CellNama_DPT As Integer = 13




    Public Sub Get_Isi_Listview(ByVal No_Index As Integer)
        LvNo_Val = DataGridView1.Rows(No_Index).Cells(0).Value.ToString : CellNo_Val = 0
        LvTangal = DataGridView1.Rows(No_Index).Cells(1).Value.ToString : CellTanggal = 1
        LvSisa = DataGridView1.Rows(No_Index).Cells(2).Value.ToString : CellSisa = 2
        LvNilai_Input = CekNothing(DataGridView1.Rows(No_Index).Cells(3).Value) : CellNilai_Input = 3
        LvUrut_Um = DataGridView1.Rows(No_Index).Cells(4).Value.ToString : CellUrut_Um = 4
    End Sub

    Public Sub Get_Isi_Listview2(ByVal No_Index As Integer)
        LvNo_ValDPT = DataGridView2.Rows(No_Index).Cells(CellNo_ValDPT).Value.ToString
        LvTangalDPT = DataGridView2.Rows(No_Index).Cells(CellTanggalDPT).Value.ToString
        LvSisaDPT = DataGridView2.Rows(No_Index).Cells(CellSisaDPT).Value.ToString
        LvNilai_InputDPT = CekNothing(DataGridView2.Rows(No_Index).Cells(CellNilai_InputDPT).Value)
        LvUrut_DPT = DataGridView2.Rows(No_Index).Cells(CellUrut_DPT).Value.ToString
        LvPPN_DPT = DataGridView2.Rows(No_Index).Cells(CellPPN_DPT).Value.ToString
        LvPPH_DPT = DataGridView2.Rows(No_Index).Cells(CellPPH_DPT).Value.ToString
        LvNilaiPPN_DPT = DataGridView2.Rows(No_Index).Cells(CellNilaiPPN_DPT).Value.ToString
        LvNilaiPPH_DPT = DataGridView2.Rows(No_Index).Cells(CellNilaiPPH_DPT).Value.ToString
        LvNilaiKlaim_DPT = DataGridView2.Rows(No_Index).Cells(CellNilaiKlaim_DPT).Value.ToString
        LvFlagB2B_DPT = DataGridView2.Rows(No_Index).Cells(CellFlagB2B_DPT).Value.ToString
        LvAsal_DPT = DataGridView2.Rows(No_Index).Cells(CellAsal_DPT).Value.ToString
        LvJenisKlaim_DPT = DataGridView2.Rows(No_Index).Cells(CellJenisKlaim_DPT).Value.ToString
        LvNama_DPT = DataGridView2.Rows(No_Index).Cells(CellNama_DPT).Value.ToString
    End Sub

    Private Function CekNothing(ByVal str As String) As String
        Dim hasil As String = ""

        If str Is Nothing Then
            hasil = ""
        Else
            hasil = str
        End If

        Return hasil
    End Function

    Private Sub HitungTotal()
        Dim total_UM As Double = 0
        Dim total_Dpt As Double = 0


        For index As Integer = 0 To DataGridView1.Rows.Count - 1
            Get_Isi_Listview(index)
            total_UM = total_UM + Val(HilangkanTanda(LvNilai_Input))
        Next

        For index As Integer = 0 To DataGridView2.Rows.Count - 1
            Get_Isi_Listview2(index)

            Dim nilai_PPN As Double = Math.Round(Val(LvNilai_InputDPT) * LvPPN_DPT / 100, 0)
            Dim nilai_PPH As Double = Math.Round(Val(LvNilai_InputDPT) * LvPPH_DPT / 100, 0)

            DataGridView2.Rows(index).Cells(CellNilaiPPN_DPT).Value = Format(nilai_PPN, "N0")
            DataGridView2.Rows(index).Cells(CellNilaiPPH_DPT).Value = Format(nilai_PPH, "N0")

            If CkbReimburse.Checked = True Then
                DataGridView2.Rows(index).Cells(CellNilaiKlaim_DPT).Value = Format(LvNilai_InputDPT + nilai_PPN, "N0")
            Else
                DataGridView2.Rows(index).Cells(CellNilaiKlaim_DPT).Value = Format(LvNilai_InputDPT + nilai_PPN - nilai_PPH, "N0")
            End If

        Next

        For index As Integer = 0 To DataGridView2.Rows.Count - 1
            Get_Isi_Listview2(index)
            total_Dpt = total_Dpt + Val(HilangkanTanda(LvNilaiKlaim_DPT))
        Next

        TextBoxttl.Text = Format(total_Dpt + total_UM, "N0")
    End Sub


    Public Sub Cara_Bayar()
        Try
            OpenConn()
            ComboBoxCb1.Items.Clear() : arrCrByr.Clear() : ArrAkunCB1.Clear()
            ComboBoxCb1.Items.Add("-- Cara Bayar --") : arrCrByr.Add("") : ArrAkunCB1.Add("")
            SQL = "select kode_cb, keterangan, kode_account_cb from cara_bayar where kode_perusahaan = '" & KodePerusahaan & "' order by keterangan"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    ComboBoxCb1.Items.Add(Dr("keterangan")) : arrCrByr.Add(Dr("kode_cb")) : ArrAkunCB1.Add(Dr("kode_account_cb"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Display_Pilih_Uang_Masuk_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub Form_Input_No_Invoice_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        get_jam()
        DataGridView2.Columns(CellNama_DPT).DisplayIndex = 0
        Try
            OpenConn()

            DateTimePicker2.Value = tgl_skg
            DateTimePicker4.Value = tgl_skg
            DateTimePicker1.Value = DateAdd("d", -2, tgl_skg)
            DateTimePicker3.Value = DateAdd("d", -2, tgl_skg)


            If DateTimePicker1.Value > DateTimePicker2.Value Then
                MessageBox.Show("Periode I tidak boleh lebih dari periode II!", Judul)
                DateTimePicker1.Value = DateAdd("d", -2, tgl_skg) : DateTimePicker2.Value = tgl_skg
                Exit Sub
            End If

            DataGridView1.Rows.Clear()
            SQL = "select a.no_val,a.tanggal,a.sisa,a.urut from Um_global a,Uang_Masuk_Global b "
            SQL = SQL & "where a.kode_perusahaan = b.kode_perusahaan and a.no_val = b.no_val and "
            SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and a.sisa <> 0 and b.status is null "
            SQL = SQL & "and a.kode_cb = '" & arrCrByr.Item(ComboBoxCb1.SelectedIndex) & "' "
            If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "
            SQL = SQL & "a.tanggal between '"
            SQL = SQL & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' and '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1
                            DataGridView1.Rows.Add(1)
                            DataGridView1.Rows.Item(i).Cells(0).Value = .Rows(i).Item("no_val")
                            DataGridView1.Rows.Item(i).Cells(1).Value = (Format(.Rows(i).Item("tanggal"), "dd MMM yyyy"))
                            DataGridView1.Rows.Item(i).Cells(2).Value = (Format(.Rows(i).Item("sisa"), "N0"))
                            DataGridView1.Rows.Item(i).Cells(3).Value = "0"
                            DataGridView1.Rows.Item(i).Cells(4).Value = .Rows(i).Item("urut")
                        Next
                    End If
                End With
            End Using

            If DateTimePicker3.Value > DateTimePicker4.Value Then
                MessageBox.Show("Periode I tidak boleh lebih dari periode II!", Judul)
                DateTimePicker3.Value = DateAdd("d", -2, tgl_skg) : DateTimePicker4.Value = tgl_skg
                Exit Sub
            End If
            DataGridView2.Rows.Clear()
            SQL = "select a.asal_Promo, a.No_Klaim,a.tanggal,a.sisa,a.urut, a.PersenPPN, a.PersenPPH, c.Flag_B2B, isnull(a.Jenis_Subsidi,'') as Jenis_Klaim, c.nama_promo from Deposit_Customers a, Master_Promo_Selesai_Klaim b, master_Promo c "
            SQL = SQL & "where a.Kode_perusahaan = b.Kode_Perusahaan And a.No_Klaim = b.No_faktur "
            SQL = SQL & "and b.Kode_Perusahaan=c.Kode_Perusahaan and b.Kode_Promo=c.No_faktur "
            SQL = SQL & "and a.status is null and b.status is null and c.status is null and a.Kode_Customer='" & TextBox3.Text & "' "
            SQL = SQL & "and a.sisa <>0 "
            If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "
            SQL = SQL & "a.tanggal between '"
            SQL = SQL & Format(DateTimePicker3.Value, "yyyy-MM-dd") & "' and '" & Format(DateTimePicker4.Value, "yyyy-MM-dd") & "' "
            SQL = SQL & "union all "
            SQL = SQL & "select a.asal_Promo, a.No_Klaim,a.tanggal,a.sisa,a.urut, a.PersenPPN, a.PersenPPH, NULL as Flag_B2B, a.Jenis_Subsidi as Jenis_Klaim, c.nama_promo from Deposit_Customers a, Master_Promo_Binding b, master_Promo c "
            SQL = SQL & "where a.Kode_perusahaan = b.Kode_Perusahaan And a.No_Klaim = b.No_faktur "
            SQL = SQL & "and b.Kode_Perusahaan=c.Kode_Perusahaan and b.Kode_Promo=c.No_faktur "
            SQL = SQL & "and a.status is null and b.status is null and c.status is null and a.Kode_Customer='" & TextBox3.Text & "' "
            SQL = SQL & "and a.sisa <>0 "
            If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "
            SQL = SQL & "a.tanggal between '"
            SQL = SQL & Format(DateTimePicker3.Value, "yyyy-MM-dd") & "' and '" & Format(DateTimePicker4.Value, "yyyy-MM-dd") & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1
                            DataGridView2.Rows.Add(1)
                            DataGridView2.Rows.Item(i).Cells(CellNo_ValDPT).Value = .Rows(i).Item("No_Klaim")
                            DataGridView2.Rows.Item(i).Cells(CellTanggalDPT).Value = (Format(.Rows(i).Item("tanggal"), "dd MMM yyyy"))
                            DataGridView2.Rows.Item(i).Cells(CellSisaDPT).Value = (Format(.Rows(i).Item("sisa"), "N0"))
                            DataGridView2.Rows.Item(i).Cells(CellNilai_InputDPT).Value = "0"
                            DataGridView2.Rows.Item(i).Cells(CellPPN_DPT).Value = .Rows(i).Item("PersenPPN")
                            DataGridView2.Rows.Item(i).Cells(CellPPH_DPT).Value = .Rows(i).Item("PersenPPH")
                            DataGridView2.Rows.Item(i).Cells(CellNilaiKlaim_DPT).Value = 0
                            DataGridView2.Rows.Item(i).Cells(CellUrut_DPT).Value = .Rows(i).Item("urut")
                            DataGridView2.Rows.Item(i).Cells(CellFlagB2B_DPT).Value = General_Class.CekNULL(.Rows(i).Item("Flag_B2B"))
                            DataGridView2.Rows.Item(i).Cells(CellNilai_InputDPT).Value = "0"
                            DataGridView2.Rows.Item(i).Cells(CellNilaiPPN_DPT).Value = 0
                            DataGridView2.Rows.Item(i).Cells(CellNilaiPPH_DPT).Value = 0
                            DataGridView2.Rows.Item(i).Cells(CellAsal_DPT).Value = .Rows(i).Item("asal_Promo")
                            DataGridView2.Rows.Item(i).Cells(CellJenisKlaim_DPT).Value = .Rows(i).Item("Jenis_Klaim")
                            DataGridView2.Rows.Item(i).Cells(CellNama_DPT).Value = .Rows(i).Item("Nama_Promo")
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
        HitungTotal()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If TextBox1.Text.Trim.Length = 0 Then
            MessageBox.Show("Nilai Pelunasan harus diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox1.Focus()
            Exit Sub
        ElseIf DataGridView1.RowCount = 0 And DataGridView2.RowCount = 0 Then
            MessageBox.Show("Yang akan dilunasi harus diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox1.Focus()
            Exit Sub
        ElseIf TextBox2.Text.Trim.Length = 0 Then
            MessageBox.Show("No Do harus diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox2.Focus()
            Exit Sub
        ElseIf ComboBoxCb1.SelectedIndex = -1 Or ComboBoxCb1.SelectedIndex = 0 Then
            MessageBox.Show("Cara bayar harus diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBoxCb1.Focus()
            Exit Sub
        ElseIf TextBox3.Text.Trim.Length = 0 Then
            MessageBox.Show("Kode Customers harus diisi.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox3.Focus()
            Exit Sub
        End If

        Try
            OpenConn()

            Dim total As Double = 0
            For i As Integer = 0 To DataGridView1.RowCount - 1
                Get_Isi_Listview(i)
                If Val(LvNilai_Input) > Val(HilangkanTanda(LvSisa)) Then
                    CloseConn()
                    MessageBox.Show("Nilai yang diinput lebih besar dari sisa!!! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            Next

            For i As Integer = 0 To DataGridView2.RowCount - 1
                Get_Isi_Listview2(i)
                If Val(LvNilai_InputDPT) > Val(HilangkanTanda(LvSisaDPT)) Then
                    CloseConn()
                    MessageBox.Show("Nilai yang diinput lebih besar dari sisa!!! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

            Next

            If Val(HilangkanTanda(TextBoxttl.Text)) > HilangkanTanda(TextBox1.Text) Then
                CloseConn()
                MessageBox.Show("Total Nilai yang diinput lebih besar dari sisa!!! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            ElseIf Val(HilangkanTanda(TextBoxttl.Text)) < HilangkanTanda(TextBox1.Text) Then
                CloseConn()
                MessageBox.Show("Total Nilai yang diinput lebih Kecil dari sisa!!! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            SQL = "delete from Perlunasan_Per_Step_Sementara where Kode_unik='" & Label4.Text & "' and no_do ='" & TextBox2.Text & "' "
            ExecuteTrans(SQL)

            SQL = "delete from Perlunasan_Per_Step_Sementara_deposit where Kode_unik='" & Label4.Text & "' and no_do ='" & TextBox2.Text & "' "
            ExecuteTrans(SQL)




            For i As Integer = 0 To DataGridView1.RowCount - 1
                Get_Isi_Listview(i)
                If LvNilai_Input <> 0 Then
                    SQL = "INSERT INTO Perlunasan_Per_Step_Sementara (Kode_Perusahaan,No_DO,Kode_Unik,No_Val,"
                    SQL = SQL & "Tanggal,Nilai_Yang_Diinput,Urut_UM,kode_cb,kode_customer) VALUES('" & KodePerusahaan & "',"
                    SQL = SQL & "'" & TextBox2.Text & "','" & Label4.Text & "','" & LvNo_Val & "',"
                    SQL = SQL & "'" & Format(CDate(LvTangal), "yyyy-MM-dd") & "','" & HilangkanTanda(LvNilai_Input) & "'"
                    SQL = SQL & ",'" & HilangkanTanda(LvUrut_Um) & "','" & arrCrByr.Item(ComboBoxCb1.SelectedIndex) & "'"
                    SQL = SQL & ",'" & TextBox3.Text & "')"
                    ExecuteTrans(SQL)

                End If
            Next

            Dim Reimburse As String = "NULL"
            Dim Reimburse_ As String = ""

            If CkbReimburse.Checked = True Then
                Reimburse = "'Y'"
                Reimburse_ = "Y"
            End If






            For i As Integer = 0 To DataGridView2.RowCount - 1
                Get_Isi_Listview2(i)
                Dim flagB2B As String = "NULL"

                If LvFlagB2B_DPT = "Y" Then
                    flagB2B = "'Y'"
                End If

                If LvNilai_InputDPT <> 0 Then

                    SQL = "select top(1) Flag_Reimburse from Perlunasan_Per_Step_Sementara_Deposit where Kode_unik='" & Label4.Text & "' "
                    Using dr = OpenTrans(SQL)
                        If dr.Read Then
                            If General_Class.CekNULL(dr("Flag_Reimburse")) <> Reimburse_ Then
                                dr.Close()
                                CloseConn()
                                MessageBox.Show("Jenis Reimburse Tidak Boleh Berbeda . . !!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        End If
                    End Using

                    SQL = "INSERT INTO Perlunasan_Per_Step_Sementara_Deposit (Kode_Perusahaan,No_DO,Kode_Unik,No_Klaim,"
                    SQL = SQL & "Tanggal,Nilai_Yang_Diinput,Urut_Dpt,kode_customer, PersenPPN, PersenPPH, NilaiPPN, NilaiPPH, Flag_reimburse, Nilai_Yang_DiKlaim, Flag_B2B, Asal, Jenis_Klaim) VALUES('" & KodePerusahaan & "',"
                    SQL = SQL & "'" & TextBox2.Text & "','" & Label4.Text & "','" & LvNo_ValDPT & "',"
                    SQL = SQL & "'" & Format(CDate(LvTangalDPT), "yyyy-MM-dd") & "','" & HilangkanTanda(LvNilai_InputDPT) & "', "
                    SQL = SQL & "'" & HilangkanTanda(LvUrut_DPT) & "','" & TextBox3.Text & "','" & LvPPN_DPT & "','" & LvPPH_DPT & "', "
                    SQL = SQL & "'" & HilangkanTanda(LvNilaiPPN_DPT) & "','" & HilangkanTanda(LvNilaiPPH_DPT) & "'," & Reimburse & ", '" & HilangkanTanda(LvNilaiKlaim_DPT) & "', " & flagB2B & ",'" & LvAsal_DPT & "','" & LvJenisKlaim_DPT & "') "
                    ExecuteTrans(SQL)

                End If
            Next

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        If LabelDari.Text = "KR" Then
            N_EMI_Transaksi_Pelunasan_Kredit_Per_DO.cek_data = True
        ElseIf LabelDari.Text = "TN" Then
            'Pelunasan_Tunai_Per_DO_St2.cek_data = True
        ElseIf LabelDari.Text = "TN3" Then
            N_EMI_Transaksi_Pelunasan_Tunai_Per_DO.cek_data = True
        Else
            MessageBox.Show("Terjadi Kesalahan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If


        Me.Close()
    End Sub

    Private Sub DataGridView1_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellEndEdit
        Get_Isi_Listview(DataGridView1.CurrentRow.Index)
        If IsNumeric(LvNilai_Input) = False Or Val(LvNilai_Input) < 0 Then
            DataGridView1.CurrentRow.Cells(CellNilai_Input).Value = 0
        End If
        HitungTotal()
    End Sub

    Private Sub DataGridView2_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView2.CellEndEdit
        Get_Isi_Listview2(DataGridView2.CurrentRow.Index)
        If IsNumeric(LvNilai_InputDPT) = False Or Val(LvNilai_InputDPT) < 0 Then
            DataGridView2.CurrentRow.Cells(CellNilai_InputDPT).Value = 0
        End If
        HitungTotal()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            OpenConn()

            If DateTimePicker1.Value > DateTimePicker2.Value Then
                MessageBox.Show("Periode I tidak boleh lebih dari periode II!", Judul)
                DateTimePicker1.Value = DateAdd("d", -2, tgl_skg) : DateTimePicker2.Value = tgl_skg
                Exit Sub
            End If
            DataGridView1.Rows.Clear()
            SQL = "select a.no_val,a.tanggal,a.sisa,a.urut from Um_global a,Uang_Masuk_Global b "
            SQL = SQL & "where a.kode_perusahaan = b.kode_perusahaan and a.no_val = b.no_val and "
            SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and a.sisa <> 0 and b.status is null "
            SQL = SQL & "and a.kode_cb = '" & arrCrByr.Item(ComboBoxCb1.SelectedIndex) & "' "
            If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "
            SQL = SQL & "a.tanggal between '"
            SQL = SQL & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' and '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1
                            DataGridView1.Rows.Add(1)
                            DataGridView1.Rows.Item(i).Cells(0).Value = .Rows(i).Item("no_val")
                            DataGridView1.Rows.Item(i).Cells(1).Value = (Format(.Rows(i).Item("tanggal"), "dd MMM yyyy"))
                            DataGridView1.Rows.Item(i).Cells(2).Value = (Format(.Rows(i).Item("sisa"), "N0"))
                            DataGridView1.Rows.Item(i).Cells(3).Value = "0"
                            DataGridView1.Rows.Item(i).Cells(4).Value = .Rows(i).Item("urut")
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

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Try
            OpenConn()

            If DateTimePicker3.Value > DateTimePicker4.Value Then
                MessageBox.Show("Periode I tidak boleh lebih dari periode II!", Judul)
                DateTimePicker3.Value = DateAdd("d", -2, tgl_skg) : DateTimePicker4.Value = tgl_skg
                Exit Sub
            End If


            DataGridView2.Rows.Clear()
            SQL = "select a.asal_Promo, a.No_Klaim,a.tanggal,a.sisa,a.urut, a.PersenPPN, a.PersenPPH, c.Flag_B2B, isnull(a.Jenis_Subsidi,'') as Jenis_Klaim, c.nama_promo from Deposit_Customers a, Master_Promo_Selesai_Klaim b, master_Promo c "
            SQL = SQL & "where a.Kode_perusahaan = b.Kode_Perusahaan And a.No_Klaim = b.No_faktur "
            SQL = SQL & "and b.Kode_Perusahaan=c.Kode_Perusahaan and b.Kode_Promo=c.No_faktur "
            SQL = SQL & "and a.status is null and b.status is null and c.status is null and a.Kode_Customer='" & TextBox3.Text & "' "
            SQL = SQL & "and a.sisa <>0 "
            If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "
            SQL = SQL & "a.tanggal between '"
            SQL = SQL & Format(DateTimePicker3.Value, "yyyy-MM-dd") & "' and '" & Format(DateTimePicker4.Value, "yyyy-MM-dd") & "' "
            SQL = SQL & "union all "
            SQL = SQL & "select a.asal_Promo, a.No_Klaim,a.tanggal,a.sisa,a.urut, a.PersenPPN, a.PersenPPH, NULL as Flag_B2B, a.Jenis_Subsidi as Jenis_Klaim, c.nama_promo from Deposit_Customers a, Master_Promo_Binding b, master_Promo c "
            SQL = SQL & "where a.Kode_perusahaan = b.Kode_Perusahaan And a.No_Klaim = b.No_faktur "
            SQL = SQL & "and b.Kode_Perusahaan=c.Kode_Perusahaan and b.Kode_Promo=c.No_faktur "
            SQL = SQL & "and a.status is null and b.status is null and c.status is null and a.Kode_Customer='" & TextBox3.Text & "' "
            SQL = SQL & "and a.sisa <>0 "
            If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "
            SQL = SQL & "a.tanggal between '"
            SQL = SQL & Format(DateTimePicker3.Value, "yyyy-MM-dd") & "' and '" & Format(DateTimePicker4.Value, "yyyy-MM-dd") & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1
                            DataGridView2.Rows.Add(1)
                            DataGridView2.Rows.Item(i).Cells(CellNo_ValDPT).Value = .Rows(i).Item("No_Klaim")
                            DataGridView2.Rows.Item(i).Cells(CellTanggalDPT).Value = (Format(.Rows(i).Item("tanggal"), "dd MMM yyyy"))
                            DataGridView2.Rows.Item(i).Cells(CellSisaDPT).Value = (Format(.Rows(i).Item("sisa"), "N0"))
                            DataGridView2.Rows.Item(i).Cells(CellNilai_InputDPT).Value = "0"
                            DataGridView2.Rows.Item(i).Cells(CellPPN_DPT).Value = .Rows(i).Item("PersenPPN")
                            DataGridView2.Rows.Item(i).Cells(CellPPH_DPT).Value = .Rows(i).Item("PersenPPH")
                            DataGridView2.Rows.Item(i).Cells(CellNilaiKlaim_DPT).Value = 0
                            DataGridView2.Rows.Item(i).Cells(CellUrut_DPT).Value = .Rows(i).Item("urut")
                            DataGridView2.Rows.Item(i).Cells(CellFlagB2B_DPT).Value = General_Class.CekNULL(.Rows(i).Item("Flag_B2B"))
                            DataGridView2.Rows.Item(i).Cells(CellNilai_InputDPT).Value = "0"
                            DataGridView2.Rows.Item(i).Cells(CellNilaiPPN_DPT).Value = 0
                            DataGridView2.Rows.Item(i).Cells(CellNilaiPPH_DPT).Value = 0
                            DataGridView2.Rows.Item(i).Cells(CellAsal_DPT).Value = .Rows(i).Item("asal_Promo")
                            DataGridView2.Rows.Item(i).Cells(CellJenisKlaim_DPT).Value = .Rows(i).Item("Jenis_Klaim")
                            DataGridView2.Rows.Item(i).Cells(CellNama_DPT).Value = .Rows(i).Item("Nama_Promo")
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
        HitungTotal()
    End Sub

    Private Sub CkbReimburse_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CkbReimburse.CheckedChanged
        Button3_Click(CkbReimburse, e)

    End Sub

End Class
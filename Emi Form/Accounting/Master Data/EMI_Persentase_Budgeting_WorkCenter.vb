Imports System.Globalization

Public Class EMI_Persentase_Budgeting_WorkCenter

    Dim judulForm As String = "Persentase Budgeting Work Center"
    Dim FPB_Workcenter As String = "PBW"

    Dim Dgv_IdWorkCenter, Dgv_WorkCenter, Dgv_Persentase As String

    Dim Cell_IdWorkCenter As Integer = 0
    Dim Cell_WorkCenter As Integer = 1
    Dim Cell_Persentase As Integer = 2




    Private Sub EMI_Persentase_Budgeting_WorkCenter_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub
    Private Sub EMI_Persentase_Budgeting_WorkCenter_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        kosong()

    End Sub

    Private Sub get_no_faktur()
        Txt_NoTransaksi.Text = FPB_Workcenter & Format(tgl_skg, "MMyy") & "-" &
                             General_Class.Get_Last_Number2("EMI_Persentase_Budget_WorkCenter", "No_Transaksi", 5,
                             "Kode_perusahaan", KodePerusahaan,
                             "And", "substring(No_Transaksi, 1, " & Len(FPB_Workcenter) + 4 & ")", FPB_Workcenter & Format(tgl_skg, "MMyy"))
    End Sub


    Private Sub kosong()

        get_jam()

        Try
            OpenConn()
            get_no_faktur()

            DateTimePicker1.Value = Date.Now
            Txt_Keterangan.Text = ""
            Txt_TotPersen.Text = Format(0, "N2")

            Btn_Simpan.Text = "&Simpan"
            Btn_Simpan.Tag = "SIMPAN"

            Btn_Simpan.Enabled = True
            DateTimePicker1.Enabled = True
            Txt_Keterangan.Enabled = True

            Btn_Release.Visible = False


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Load_WorkCenter()

    End Sub


    Private Sub Load_WorkCenter()

        Try
            OpenConn()

            Dgv_Work_Center.Rows.Clear()
            SQL = "select Id_Work_Center, Keterangan from EMI_Master_Work_Center "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "order by Id_Work_Center"
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            Dgv_Work_Center.Rows.Add(1)
                            Dgv_Work_Center.Rows(i).Cells(Cell_IdWorkCenter).Value = .Rows(i).Item("Id_Work_Center")
                            Dgv_Work_Center.Rows(i).Cells(Cell_WorkCenter).Value = .Rows(i).Item("Keterangan")
                            Dgv_Work_Center.Rows(i).Cells(Cell_Persentase).Value = Format(0, "N2")


                            Dgv_Work_Center.Rows(i).Cells(Cell_Persentase).Style.BackColor = Color.LightGray

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

    Private Sub Get_Data_DGV(ByVal index As Integer)

        Dgv_IdWorkCenter = Dgv_Work_Center.Rows(index).Cells(Cell_IdWorkCenter).Value
        Dgv_WorkCenter = Dgv_Work_Center.Rows(index).Cells(Cell_WorkCenter).Value
        Dgv_Persentase = Dgv_Work_Center.Rows(index).Cells(Cell_Persentase).Value

    End Sub

    Private Sub Txt_NoTransaksi_Leave(sender As Object, e As EventArgs) Handles Txt_NoTransaksi.Leave

        If Txt_NoTransaksi.Text.Trim.Length = 0 Then Exit Sub

        Dim flagRelease As Boolean = True

        Try
            OpenConn()




            SQL = "select a.Tanggal, a.Keterangan, a.Flag_Release "
            SQL = SQL & "from EMI_Persentase_Budget_WorkCenter a "
            SQL = SQL & "where  a.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Transaksi = '" & Txt_NoTransaksi.Text & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("Mytable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            If General_Class.CekNULL(.Rows(i).Item("Flag_Release")) = "" Then
                                flagRelease = False
                            End If

                            DateTimePicker1.Value = .Rows(i).Item("Tanggal")
                            Txt_Keterangan.Text = .Rows(i).Item("Keterangan")


                            Dgv_Work_Center.Rows.Clear()
                            SQL = "select a.Id_Work_Center, a.Keterangan, "
                            SQL = SQL & "ISNULL(( select z.Persentase from EMI_Persentase_Budget_WorkCenter_Detail z where a.Kode_Perusahaan = z.Kode_Perusahaan "
                            SQL = SQL & "and a.Id_Work_Center = z.Id_Work_Center and z.No_Transaksi = '" & Txt_NoTransaksi.Text & "' ), 0) as Persentase "
                            SQL = SQL & "from EMI_Master_Work_Center a "
                            SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                            SQL = SQL & "order by a.Id_Work_Center "
                            Using Ds2 = BindingTrans(SQL)
                                If Ds2.Tables("MyTable").Rows.Count <> 0 Then
                                    For j As Integer = 0 To Ds2.Tables("MyTable").Rows.Count - 1

                                        Dgv_Work_Center.Rows.Add(1)
                                        Dgv_Work_Center.Rows(j).Cells(Cell_IdWorkCenter).Value = Ds2.Tables("MyTable").Rows(j).Item("Id_Work_Center")
                                        Dgv_Work_Center.Rows(j).Cells(Cell_WorkCenter).Value = Ds2.Tables("MyTable").Rows(j).Item("Keterangan")
                                        Dgv_Work_Center.Rows(j).Cells(Cell_Persentase).Value = Format(Ds2.Tables("MyTable").Rows(j).Item("Persentase"), "N2")




                                        If flagRelease Then
                                            Dgv_Work_Center.Rows(j).Cells(Cell_Persentase).ReadOnly = True
                                            Dgv_Work_Center.Rows(j).Cells(Cell_Persentase).Style.BackColor = Color.White
                                        Else
                                            Dgv_Work_Center.Rows(j).Cells(Cell_Persentase).ReadOnly = False
                                            Dgv_Work_Center.Rows(j).Cells(Cell_Persentase).Style.BackColor = Color.LightGray
                                        End If

                                    Next
                                End If
                            End Using



                        Next

                        HitungPersen()
                    Else
                        CloseConn()
                        MessageBox.Show("Data Tidak Ditemukan", judulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        kosong()
                        Exit Sub
                    End If
                End With
            End Using


            If flagRelease Then
                Btn_Simpan.Enabled = False
                Btn_Release.Visible = False
                DateTimePicker1.Enabled = False
                Txt_Keterangan.Enabled = False
            Else
                Btn_Simpan.Enabled = True
                Btn_Simpan.Text = "&Update"
                Btn_Simpan.Tag = "UPDATE"

                DateTimePicker1.Enabled = True
                Txt_Keterangan.Enabled = True
                Btn_Release.Visible = True
            End If



            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try





    End Sub


    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click

        If Txt_Keterangan.Text.Trim.Length = 0 Then
            MessageBox.Show("Keterangan Tidak Boleh Kosong", judulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_Keterangan.Focus() : Exit Sub

        ElseIf Val(HilangkanTanda(Txt_TotPersen.Text)) < 100 Then
            MessageBox.Show("Total Persen Harus Mencapai 100%", judulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Dgv_Work_Center.Focus() : Exit Sub

        ElseIf Val(HilangkanTanda(Txt_TotPersen.Text)) > 100 Then
            MessageBox.Show("Total Persen Tidak Boleh Lebih Dari 100%", judulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Dgv_Work_Center.Focus() : Exit Sub


        End If


        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            Dim action As String = ""

            If Btn_Simpan.Tag = "SIMPAN" Then

                get_no_faktur()

                '========================
                '=     INSERT INDUK     =
                '========================
                SQL = "insert into EMI_Persentase_Budget_WorkCenter (Kode_Perusahaan, No_Transaksi, Tanggal, Keterangan) values "
                SQL = SQL & "('" & KodePerusahaan & "', '" & Txt_NoTransaksi.Text & "', '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', '" & Txt_Keterangan.Text & "') "
                ExecuteTrans(SQL)


                For i As Integer = 0 To Dgv_Work_Center.Rows.Count - 1

                    Get_Data_DGV(i)

                    If Val(HilangkanTanda(Dgv_Persentase)) = 0 Then
                        Continue For
                    End If

                    '=========================
                    '=     INSERT DETAIL     =
                    '=========================
                    SQL = "insert into EMI_Persentase_Budget_WorkCenter_Detail (Kode_Perusahaan, No_Transaksi, Id_Work_Center, Persentase) values "
                    SQL = SQL & "('" & KodePerusahaan & "', '" & Txt_NoTransaksi.Text & "', '" & Dgv_IdWorkCenter & "', '" & Dgv_Persentase & "')"
                    ExecuteTrans(SQL)


                Next

                action = "DiSimpan"

            ElseIf Btn_Simpan.Tag = "UPDATE" Then


                '========================
                '=     UPDATE INDUK     =
                '========================
                SQL = "update EMI_Persentase_Budget_WorkCenter set Tanggal = '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', Keterangan = '" & Txt_Keterangan.Text & "'  "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Transaksi = '" & Txt_NoTransaksi.Text & "'"
                ExecuteTrans(SQL)


                '=========================
                '=     UPDATE DETAIL     =
                '=========================
                SQL = "delete EMI_Persentase_Budget_WorkCenter_Detail where Kode_Perusahaan = '" & KodePerusahaan & "' and  No_Transaksi = '" & Txt_NoTransaksi.Text & "' "
                ExecuteTrans(SQL)

                '=========================
                '=     INSERT DETAIL     =
                '=========================
                For i As Integer = 0 To Dgv_Work_Center.Rows.Count - 1

                    Get_Data_DGV(i)

                    If Val(HilangkanTanda(Dgv_Persentase)) = 0 Then
                        Continue For
                    End If


                    SQL = "insert into EMI_Persentase_Budget_WorkCenter_Detail (Kode_Perusahaan, No_Transaksi, Id_Work_Center, Persentase) values "
                    SQL = SQL & "('" & KodePerusahaan & "', '" & Txt_NoTransaksi.Text & "', '" & Dgv_IdWorkCenter & "', '" & Dgv_Persentase & "')"
                    ExecuteTrans(SQL)


                Next

                action = "DiUpdate"

            End If




            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
            MessageBox.Show($"Data Berhasil {action}", judulForm, MessageBoxButtons.OK, MessageBoxIcon.Information)
            kosong()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


    End Sub


    Private Sub Btn_Release_Click(sender As Object, e As EventArgs) Handles Btn_Release.Click

        If Txt_Keterangan.Text.Trim.Length = 0 Then
            MessageBox.Show("Keterangan Tidak Boleh Kosong", judulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_Keterangan.Focus() : Exit Sub

        ElseIf Val(HilangkanTanda(Txt_TotPersen.Text)) < 100 Then
            MessageBox.Show("Total Persen Harus Mencapai 100%", judulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Dgv_Work_Center.Focus() : Exit Sub

        ElseIf Val(HilangkanTanda(Txt_TotPersen.Text)) > 100 Then
            MessageBox.Show("Total Persen Tidak Boleh Lebih Dari 100%", judulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Dgv_Work_Center.Focus() : Exit Sub


        End If

        Dim pertanyaan As String = MessageBox.Show("Yakin Ingin Release?", judulForm, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If pertanyaan = vbNo Then Exit Sub

        Try
            OpenConn()

            '=========================================
            '=     CEK APAKAH DATA SUDAH RELEASE     =
            '=========================================
            SQL = "select Flag_Release from EMI_Persentase_Budget_WorkCenter where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Transaksi = '" & Txt_NoTransaksi.Text & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    For i As Integer = 0 To .Rows.Count - 1
                        If General_Class.CekNULL(.Rows(i).Item("Flag_Release")) = "" Then

                            SQL = "update EMI_Persentase_Budget_WorkCenter set Flag_Release = 'Y' where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Transaksi = '" & Txt_NoTransaksi.Text & "' "
                            ExecuteTrans(SQL)

                        Else
                            CloseConn()
                            MessageBox.Show("Data Sudah DiRelease Sebelumnya", judulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub


                        End If
                    Next
                End With
            End Using


            CloseConn()
            MessageBox.Show("Daata Berhasil DiRelease", judulForm, MessageBoxButtons.OK, MessageBoxIcon.Information)
            kosong()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try




    End Sub














    Private Sub Dgv_Work_Center_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_Work_Center.CellEndEdit
        If Not Dgv_Work_Center.Rows.Count = 0 Then

            '======================
            '=     SET FORMAT     =
            '======================
            Dim culture As CultureInfo = CultureInfo.CurrentCulture

            If Dgv_Work_Center.CurrentCell.ColumnIndex = Cell_Persentase Then

                Get_Data_DGV(Dgv_Work_Center.CurrentRow.Index)

                If Not IsNumeric(Dgv_Persentase) Then
                    Dgv_Work_Center.CurrentCell.Value = Format(0, "N2")
                    HitungPersen()
                    Exit Sub
                End If

                Dim cellKuantity As String = Dgv_Work_Center.CurrentCell.Value.ToString()

                If cellKuantity.Contains(",") Then
                    MessageBox.Show("Kuantity Tidak Boleh Koma, Ganti dengan Titik", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Dgv_Work_Center.CurrentCell.Value = Format(0, "N2")
                    HitungPersen()
                    Exit Sub
                End If

                Dim nilai As Decimal = Decimal.Parse(cellKuantity)
                Dim formattedValue As String = nilai.ToString("N2", culture)

                Dgv_Work_Center.CurrentCell.Value = formattedValue
            End If

            HitungPersen()
        End If
    End Sub

    Private Sub Dgv_Work_Center_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_Work_Center.CellEnter
        If Not Dgv_Work_Center.Rows.Count = 0 Then
            '======================
            '=     SET FORMAT     =
            '======================

            If Dgv_Work_Center.CurrentCell.ColumnIndex = Cell_Persentase Then
                Dim cellKuantity As String = Dgv_Work_Center.CurrentCell.Value

                If cellKuantity = "" Then
                    Exit Sub
                End If

                Dim cleanedStr As String = HilangkanTanda(cellKuantity) ' Menghapus titik
                Dim nilai As Decimal = Decimal.Parse(cleanedStr)

                Dgv_Work_Center.CurrentCell.Value = nilai
            End If
        End If
    End Sub



    Private Sub Dgv_Work_Center_CellLeave(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_Work_Center.CellLeave
        If Not Dgv_Work_Center.Rows.Count = 0 Then

            '======================
            '=     SET FORMAT     =
            '======================
            Dim culture As CultureInfo = CultureInfo.CurrentCulture

            If Dgv_Work_Center.CurrentCell.ColumnIndex = Cell_Persentase Then
                Dim cellKuantity As String = Dgv_Work_Center.CurrentCell.Value

                If cellKuantity = "" Then
                    Exit Sub
                End If


                Dim nilai As Decimal = Decimal.Parse(cellKuantity)
                Dim formattedValue As String = nilai.ToString("N2", culture)

                Dgv_Work_Center.CurrentCell.Value = formattedValue

            End If
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        kosong()
    End Sub



    Private Sub HitungPersen()

        If Dgv_Work_Center.Rows.Count = 0 Then Exit Sub

        Dim totalPersen As Double = 0

        For i As Integer = 0 To Dgv_Work_Center.Rows.Count - 1
            If Dgv_Work_Center.Rows(i).Cells(Cell_Persentase).Value = "" Then
                Continue For
            End If

            Get_Data_DGV(i)

            totalPersen += Val(HilangkanTanda(Dgv_Persentase))

        Next



        Txt_TotPersen.Text = Format(totalPersen, "N2")

    End Sub


    Private Sub Txt_NoTransaksi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_NoTransaksi.KeyPress
        If e.KeyChar = Chr(13) Then DateTimePicker1.Focus()
    End Sub


End Class
Public Class Form3

    Dim textbox_cell As New DataGridViewTextBoxCell
    Dim combo_cell As New DataGridViewComboBoxCell()

    Dim arrOption As New List(Of List(Of String))

    Dim Lv_KdParameter, Lv_NmParameter, Lv_Tahapan, Lv_Value, Lv_IDParameter, Lv_IDParameter_Tahapan, Lv_IDTahapan, Lv_IDKategori_Komponen _
        , Lv_FlagInput, Lv_FlagOption, Lv_FlagSlider, Lv_FlagDate, Lv_FlagTime, Lv_FlagAngka As String

    Dim Cell_KdParameter As Integer = 0
    Dim Cell_NmParameter As Integer = 1
    Dim Cell_Tahapan As Integer = 2
    Dim Cell_Value As Integer = 3
    Dim Cell_IDParameter As Integer = 4
    Dim Cell_IDParameter_Tahapan As Integer = 5
    Dim Cell_IDTahapan As Integer = 6
    Dim Cell_IDKategori_Komponen As Integer = 7
    Dim Cell_Flag_Input As Integer = 8
    Dim Cell_Flag_Option As Integer = 9
    Dim Cell_Flag_Slider As Integer = 10
    Dim Cell_Flag_Date As Integer = 11
    Dim Cell_Flag_Time As Integer = 12
    Dim Cell_Flag_Angka As Integer = 13

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
        ' Atur jumlah kolom dan baris
        'DataGridView1.ColumnCount = 1
        'DataGridView1.Columns(0).Name = "Isi Data"
        'DataGridView1.Columns(0).Width = 400
        'DataGridView1.RowCount = 3

        '' Atur row header sebagai label
        'DataGridView1.Rows(0).HeaderCell.Value = "Tanggal"
        'DataGridView1.Rows(1).HeaderCell.Value = "Pilih"
        'DataGridView1.Rows(2).HeaderCell.Value = "Angka"

        '' Ganti baris 1 jadi DateTimePicker
        'Dim dateCell As New CalendarCell()
        'DataGridView1.Rows(0).Cells(0) = dateCell
        'DataGridView1.Rows(0).Cells(0).Value = DateTime.Now

        '' Ganti baris 2 jadi ComboBox
        'Dim comboCell As New DataGridViewComboBoxCell()
        'comboCell.Items.AddRange("Option 1", "Option 2", "Option 3")
        'DataGridView1.Rows(1).Cells(0) = comboCell

        Kosong()

    End Sub

    Private Sub Kosong()
        DataGridView1.Rows.Clear()

        Try
            OpenConn()

            arrOption.Clear()
            SQL = "select Kode_Parameter , Keterangan_Parameter , Keterangan_Tahapan , Id_Parameter, Id_Parameter_Tahapan, Id_Kategori_Komponen , Id_Tahapan, "
            SQL = SQL & "flag_input, flag_option, flag_slider, flag_date, flag_time, Flag_Angka "
            SQL = SQL & "from View_Parameter_Per_Tahapan "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and Aktif_Tahapan = 'Y' "
            SQL = SQL & "order by Order_Parameter_Tahapan "
            Using ds = BindingTrans(SQL)
                With ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1
                            DataGridView1.Rows.Add(1)

                            Dim subArr As New List(Of String)

                            Dim flag_input As String = General_Class.CekNULL(.Rows(i).Item("flag_input"))
                            Dim flag_option As String = General_Class.CekNULL(.Rows(i).Item("flag_option"))
                            Dim flag_slider As String = General_Class.CekNULL(.Rows(i).Item("flag_slider"))
                            Dim flag_date As String = General_Class.CekNULL(.Rows(i).Item("flag_date"))
                            Dim flag_time As String = General_Class.CekNULL(.Rows(i).Item("flag_time"))
                            Dim flag_angka As String = General_Class.CekNULL(.Rows(i).Item("flag_angka"))



                            DataGridView1.Rows(i).Cells(Cell_KdParameter).Value = .Rows(i).Item("Kode_Parameter")
                            DataGridView1.Rows(i).Cells(Cell_NmParameter).Value = .Rows(i).Item("Keterangan_Parameter")
                            DataGridView1.Rows(i).Cells(Cell_Tahapan).Value = .Rows(i).Item("Keterangan_Tahapan")


                            If flag_input = "Y" Or flag_slider = "Y" Then
                                DataGridView1.Rows(i).Cells(Cell_Value) = CType(textbox_cell.Clone(), DataGridViewTextBoxCell)
                                'DataGridView1.Rows(i).Cells(Cell_Value) = textbox_cell
                                DataGridView1.Rows(i).Cells(Cell_Value).Value = ""

                                subArr.Add("")

                            ElseIf flag_option = "Y" Then
                                DataGridView1.Rows(i).Cells(Cell_Value) = CType(combo_cell.Clone(), DataGridViewComboBoxCell)
                                'DataGridView1.Rows(i).Cells(Cell_Value) = combo_cell

                                combo_cell = DataGridView1.Rows(i).Cells(Cell_Value)

                                combo_cell.Items.Clear()
                                SQL = "select Id_Pilihan_Parameter_Tahapan , Id_Parameter_Tahapan , Keterangan , Order_Pilihan  "
                                SQL = SQL & "from HRIS_Pilihan_Parameter_Tahapan "
                                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
                                SQL = SQL & "and Id_Parameter_Tahapan = '" & .Rows(i).Item("Id_Parameter_Tahapan") & "' "
                                SQL = SQL & "and Aktif = 'Y' "
                                SQL = SQL & "order by Order_Pilihan  "
                                Using Dr = OpenTrans(SQL)
                                    Do While Dr.Read

                                        combo_cell.Items.Add(Dr("Keterangan")) : subArr.Add(Dr("Id_Pilihan_Parameter_Tahapan"))

                                    Loop
                                End Using


                            ElseIf flag_date = "Y" Then
                                Dim date_cell As New CalendarCell()
                                DataGridView1.Rows(i).Cells(Cell_Value) = date_cell
                                DataGridView1.Rows(i).Cells(Cell_Value).Value = Tanggal_Default
                                subArr.Add("")
                            ElseIf flag_time = "Y" Then
                                Dim time_cell As New TimeCell()
                                DataGridView1.Rows(i).Cells(Cell_Value) = time_cell
                                DataGridView1.Rows(i).Cells(Cell_Value).Value = DateTime.Now
                                subArr.Add("")
                            End If

                            arrOption.Add(subArr)

                            DataGridView1.Rows(i).Cells(Cell_IDParameter).Value = General_Class.CekNULL(.Rows(i).Item("Id_Parameter"))
                            DataGridView1.Rows(i).Cells(Cell_IDParameter_Tahapan).Value = General_Class.CekNULL(.Rows(i).Item("Id_Parameter_Tahapan"))
                            DataGridView1.Rows(i).Cells(Cell_IDTahapan).Value = General_Class.CekNULL(.Rows(i).Item("Id_Tahapan"))
                            DataGridView1.Rows(i).Cells(Cell_IDKategori_Komponen).Value = General_Class.CekNULL(.Rows(i).Item("Id_Kategori_Komponen"))


                            DataGridView1.Rows(i).Cells(Cell_Flag_Input).Value = flag_input
                            DataGridView1.Rows(i).Cells(Cell_Flag_Option).Value = flag_option
                            DataGridView1.Rows(i).Cells(Cell_Flag_Slider).Value = Lv_FlagSlider
                            DataGridView1.Rows(i).Cells(Cell_Flag_Date).Value = flag_date
                            DataGridView1.Rows(i).Cells(Cell_Flag_Time).Value = Lv_FlagTime
                            DataGridView1.Rows(i).Cells(Cell_Flag_Angka).Value = flag_angka




                            'DataGridView1.Rows(i).Cells(CellValueParameter) = dateCell
                            'DataGridView1.Rows(i).Cells(0).Value = Tanggal_Default

                            '' Ganti baris 2 jadi ComboBox
                            'comboCell.Items.AddRange("Option 1", "Option 2", "Option 3")
                            'DataGridView1.Rows(1).Cells(0) = comboCell
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

    Public Sub Get_Isi_Listview(ByVal No_Index As Integer)

        Lv_KdParameter = If(General_Class.CekNULL(DataGridView1.Rows(No_Index).Cells(Cell_KdParameter).Value) = "", "-", General_Class.CekNULL(DataGridView1.Rows(No_Index).Cells(Cell_KdParameter).Value))
        Lv_NmParameter = If(General_Class.CekNULL(DataGridView1.Rows(No_Index).Cells(Cell_NmParameter).Value) = "", "-", General_Class.CekNULL(DataGridView1.Rows(No_Index).Cells(Cell_NmParameter).Value))
        Lv_Tahapan = If(General_Class.CekNULL(DataGridView1.Rows(No_Index).Cells(Cell_Tahapan).Value) = "", "-", General_Class.CekNULL(DataGridView1.Rows(No_Index).Cells(Cell_Tahapan).Value))
        Lv_Value = If(General_Class.CekNULL(DataGridView1.Rows(No_Index).Cells(Cell_Value).Value) = "", "-", General_Class.CekNULL(DataGridView1.Rows(No_Index).Cells(Cell_Value).Value))
        Lv_IDParameter = If(General_Class.CekNULL(DataGridView1.Rows(No_Index).Cells(Cell_IDParameter).Value) = "", "-", General_Class.CekNULL(DataGridView1.Rows(No_Index).Cells(Cell_IDParameter).Value))
        Lv_IDParameter_Tahapan = If(General_Class.CekNULL(DataGridView1.Rows(No_Index).Cells(Cell_IDParameter_Tahapan).Value) = "", "-", General_Class.CekNULL(DataGridView1.Rows(No_Index).Cells(Cell_IDParameter_Tahapan).Value))
        Lv_IDTahapan = If(General_Class.CekNULL(DataGridView1.Rows(No_Index).Cells(Cell_IDTahapan).Value) = "", "-", General_Class.CekNULL(DataGridView1.Rows(No_Index).Cells(Cell_IDTahapan).Value))
        Lv_IDKategori_Komponen = If(General_Class.CekNULL(DataGridView1.Rows(No_Index).Cells(Cell_IDKategori_Komponen).Value) = "", "-", General_Class.CekNULL(DataGridView1.Rows(No_Index).Cells(Cell_IDKategori_Komponen).Value))

        Lv_FlagInput = If(General_Class.CekNULL(DataGridView1.Rows(No_Index).Cells(Cell_Flag_Input).Value) = "", "-", General_Class.CekNULL(DataGridView1.Rows(No_Index).Cells(Cell_Flag_Input).Value))
        Lv_FlagOption = If(General_Class.CekNULL(DataGridView1.Rows(No_Index).Cells(Cell_Flag_Option).Value) = "", "-", General_Class.CekNULL(DataGridView1.Rows(No_Index).Cells(Cell_Flag_Option).Value))
        Lv_FlagSlider = If(General_Class.CekNULL(DataGridView1.Rows(No_Index).Cells(Cell_Flag_Slider).Value) = "", "-", General_Class.CekNULL(DataGridView1.Rows(No_Index).Cells(Cell_Flag_Slider).Value))
        Lv_FlagDate = If(General_Class.CekNULL(DataGridView1.Rows(No_Index).Cells(Cell_Flag_Date).Value) = "", "-", General_Class.CekNULL(DataGridView1.Rows(No_Index).Cells(Cell_Flag_Date).Value))
        Lv_FlagTime = If(General_Class.CekNULL(DataGridView1.Rows(No_Index).Cells(Cell_Flag_Time).Value) = "", "-", General_Class.CekNULL(DataGridView1.Rows(No_Index).Cells(Cell_Flag_Time).Value))
        Lv_FlagAngka = If(General_Class.CekNULL(DataGridView1.Rows(No_Index).Cells(Cell_Flag_Angka).Value) = "", "-", General_Class.CekNULL(DataGridView1.Rows(No_Index).Cells(Cell_Flag_Angka).Value))

    End Sub

    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction


            For i As Integer = 0 To DataGridView1.Rows.Count - 1
                Get_Isi_Listview(i)

                If Lv_Value = "" Then
                    Continue For
                End If

                SQL = "insert into TESTING_SIMPAN_RIX (Kode_Perusahaan, Kode_Parameter, Parameter, Tahapan, Value, ID_Paremeter, ID_Parameter_Tahapan, ID_Tahapan, ID_Kategori_Komponen) "
                SQL = SQL & "VALUES('" & KodePerusahaan & "', '" & Lv_KdParameter & "', '" & Lv_NmParameter & "', '" & Lv_Tahapan & "',  "

                If Lv_FlagInput = "Y" Or Lv_FlagSlider = "Y" Then

                    SQL = SQL & "'" & If(General_Class.CekNULL(Lv_Value) = "", "-", Lv_Value) & "', "

                ElseIf Lv_FlagOption = "Y" Then

                    Dim DataCmb As DataGridViewComboBoxCell = CType(DataGridView1.Rows(i).Cells(Cell_Value), DataGridViewComboBoxCell)

                    Dim index As Integer = DataCmb.Items.IndexOf(DataCmb.Value)

                    Dim DataSelected As String = arrOption(i)(index).ToString

                    SQL = SQL & "'" & DataSelected & "', "

                ElseIf Lv_FlagDate = "Y" Then

                    SQL = SQL & "'" & If(General_Class.CekNULL(Lv_Value) = "", "-", Format(Lv_Value, "yyyy-MM-dd")) & "', "

                ElseIf Lv_FlagTime = "Y" Then
                    SQL = SQL & "'" & If(General_Class.CekNULL(Lv_Value) = "", "-", Format(Lv_Value, "HH:mm:ss")) & "', "

                Else
                    SQL = SQL & "'" & If(General_Class.CekNULL(Lv_Value) = "", "-", Lv_Value) & "', "

                End If


                SQL = SQL & "'" & Lv_IDParameter & "', '" & Lv_IDParameter_Tahapan & "', '" & Lv_IDTahapan & "', '" & Lv_IDKategori_Komponen & "')"
                ExecuteTrans(SQL)


            Next





            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()

            MessageBox.Show("Selesai", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try





    End Sub



    ' Validasi input angka saja di baris 3
    Private Sub DataGridView1_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles DataGridView1.EditingControlShowing
        'If DataGridView1.CurrentCell.RowIndex = 2 Then
        '    Dim tb As TextBox = TryCast(e.Control, TextBox)
        '    If tb IsNot Nothing Then
        '        RemoveHandler tb.KeyPress, AddressOf OnlyNumbers_KeyPress
        '        AddHandler tb.KeyPress, AddressOf OnlyNumbers_KeyPress
        '    End If
        'End If

        Dim currentRow = DataGridView1.CurrentCell.RowIndex
        Dim currentCol = DataGridView1.CurrentCell.ColumnIndex

        ' Cek apakah sedang edit kolom ke-5 (index 4)
        If currentCol = Cell_Value Then
            ' Ambil nilai dari kolom ke-10 (index 9) pada baris yang sama
            Dim flagCell = DataGridView1.Rows(currentRow).Cells(Cell_Flag_Angka)
            If flagCell.Value IsNot Nothing AndAlso flagCell.Value.ToString().ToUpper() = "Y" Then
                ' Terapkan validasi angka
                Dim tb As TextBox = TryCast(e.Control, TextBox)
                If tb IsNot Nothing Then
                    RemoveHandler tb.KeyPress, AddressOf OnlyNumbers_KeyPress
                    AddHandler tb.KeyPress, AddressOf OnlyNumbers_KeyPress
                End If
            End If
        End If
    End Sub

    Private Sub OnlyNumbers_KeyPress(sender As Object, e As KeyPressEventArgs)
        ' Hanya angka dan backspace
        'If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
        '    e.Handled = True
        'End If

        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> "."c Then
            e.Handled = True
        End If

        ' Cegah lebih dari satu titik
        If e.KeyChar = "."c AndAlso CType(sender, TextBox).Text.Contains(".") Then
            e.Handled = True
        End If
    End Sub
    Private Sub DataGridView1_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellEnter
        If TypeOf DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex) Is DataGridViewComboBoxCell Then
            DataGridView1.BeginEdit(True)
            Dim cb As ComboBox = TryCast(DataGridView1.EditingControl, ComboBox)
            If cb IsNot Nothing Then
                cb.DroppedDown = True
            End If
        End If
    End Sub

    Private Sub Form3_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub
End Class

Imports System.Reflection
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button
Imports System.Xml


Public Class Master_Quality_Control_Kategori_Detail
    Dim arrcari, arrJenisQC, arrID_Kategori As New ArrayList
    Dim Jenis = "Master_Quality_Control"
    Dim id_qc As String


    Dim LvIDUji As String
    Dim LvKodeUji As String
    Dim LvKet As String
    Dim LvSatuan As String
    'Dim LvMinAwal As String
    'Dim LvMaxAwal As String
    'Dim LvMinHasil As String
    'Dim LvMaxHasil As String
    'Dim LvJenis As String
    'Dim LvTampilMasuk As String
    'Dim LvTampilBongkar As String

    Dim CellIDUji As Integer = 0
    Dim CellKodeUji As Integer = 1
    Dim CellKet As Integer = 2
    Dim CellSatuan As Integer = 3
    'Dim CellMinAwal As Integer = 4
    'Dim CellMaxAwal As Integer = 5
    'Dim CellMinHasil As Integer = 6
    'Dim CellMaxHasil As Integer = 7
    'Dim CellJenis As Integer = 8
    'Dim CellTampilMasuk As Integer = 9
    'Dim CellTampilBongkar As Integer = 10

    Dim LvData_Kode As String
    Dim LvData_Ket As String
    Dim LvData_satuan As String
    Dim LvData_ID As String

    Dim cellData_Kode As Integer = 0
    Dim cellData_Ket As Integer = 1
    Dim cellData_satuan As Integer = 2
    Dim cellData_ID As Integer = 3


    Public Sub Get_Isi_Listview(ByVal No_Index As Integer)

        LvIDUji = DgvSimulasi_DataHPP.Rows(No_Index).Cells(CellIDUji).Value.ToString
        LvKodeUji = DgvSimulasi_DataHPP.Rows(No_Index).Cells(CellKodeUji).Value.ToString
        LvKet = DgvSimulasi_DataHPP.Rows(No_Index).Cells(CellKet).Value.ToString
        LvSatuan = DgvSimulasi_DataHPP.Rows(No_Index).Cells(CellSatuan).Value.ToString
        'LvMinAwal = DgvSimulasi_DataHPP.Rows(No_Index).Cells(CellMinAwal).Value.ToString
        'LvMaxAwal = DgvSimulasi_DataHPP.Rows(No_Index).Cells(CellMaxAwal).Value.ToString
        'LvMinHasil = DgvSimulasi_DataHPP.Rows(No_Index).Cells(CellMinHasil).Value.ToString
        'LvMaxHasil = DgvSimulasi_DataHPP.Rows(No_Index).Cells(CellMaxHasil).Value.ToString
        'LvJenis = DgvSimulasi_DataHPP.Rows(No_Index).Cells(CellJenis).Value.ToString
        'LvTampilMasuk = DgvSimulasi_DataHPP.Rows(No_Index).Cells(CellTampilMasuk).Value.ToString
        'LvTampilBongkar = DgvSimulasi_DataHPP.Rows(No_Index).Cells(CellTampilBongkar).Value.ToString
    End Sub

    Public Sub Get_Isi_ListviewData(ByVal No_Index As Integer)

        LvData_Kode = ListView1.Items(No_Index).SubItems(cellData_Kode).Text
        LvData_Ket = ListView1.Items(No_Index).SubItems(cellData_Ket).Text
        LvData_satuan = ListView1.Items(No_Index).SubItems(cellData_satuan).Text
        LvData_ID = ListView1.Items(No_Index).SubItems(cellData_ID).Text

    End Sub
    Private Sub kosong()
        TextBox1.Text = ""
        TextBox4.Text = ""

        'TextBox1.Focus()

        DgvSimulasi_DataHPP.Rows.Clear()

        TextBox3.Text = ""
        ComboBox1.Items.Clear() : arrcari.Clear()
        ComboBox1.Items.Add(Base_Language.Lang_Quality_Control_Kode) : arrcari.Add("kode_Uji")
        ComboBox1.Items.Add(Base_Language.Lang_Quality_Control_Keterangan) : arrcari.Add("keterangan")

        ComboBox1.SelectedIndex = -1


        Btn_Simpan.Text = Base_Language.Lang_Global_Simpan
        Btn_Hapus.Text = Base_Language.Lang_Global_Hapus
        Btn_Cari.Text = Base_Language.Lang_Global_Cari
        Btn_Refresh.Text = Base_Language.Lang_Global_Refresh
        Btn_Simpan.Tag = "&Simpan"
        Btn_Hapus.Enabled = False

        ListView2.Location = New Point(128, 89)

        Cari("Y")

    End Sub

    Private Sub Cari(ByVal semua As String)
        Try
            OpenConn()

            ListView1.Items.Clear()
            SQL = "Select Id_QC_Formula,Kode_Perusahaan,Kode_Uji,Keterangan,Satuan,Target,"
            SQL = SQL & "Flag_Tampil_Formula,Flag_Tampil_Bahan "
            SQL = SQL & "From EMI_Quality_Control where kode_perusahaan = '" & KodePerusahaan & "' "
            If semua = "T" Then
                SQL = SQL & "And " & arrcari.Item(ComboBox1.SelectedIndex) & " Like '%" & TextBox3.Text & "%' "
                SQL = SQL & "order by " & arrcari.Item(ComboBox1.SelectedIndex) & " "
            Else
                SQL = SQL & "order by Kode_Uji "
            End If
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim Lvw As ListViewItem
                    Lvw = ListView1.Items.Add(dr("Kode_Uji"))
                    Lvw.SubItems.Add(dr("Keterangan"))
                    Lvw.SubItems.Add(dr("Satuan"))
                    Lvw.SubItems.Add(dr("Id_QC_Formula"))
                Loop
            End Using

            ComboBox2.Items.Clear() : arrID_Kategori.Clear()
            SQL = "select ID_Kategori_QC,Keterangan from EMI_Kategori_QC where Kode_Perusahaan = '" & KodePerusahaan & "' order by Keterangan"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    ComboBox2.Items.Add(dr("Keterangan")) : arrID_Kategori.Add(dr("ID_Kategori_QC"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub
    Private Sub Master_Quality_Control_Kategori_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub Master_Quality_Control_Kategori_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        Try
            OpenConn()

            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)

            Label1.Text = Base_Language.Lang_Quality_Control_Judul_Kategori
            Label2.Text = Base_Language.Lang_Global_KodeBarang

            Label4.Text = Base_Language.Lang_Quality_Control_Kolom

            ListView1.Columns.Add(Base_Language.Lang_Quality_Control_Kode, 100, HorizontalAlignment.Left)
            ListView1.Columns.Add(Base_Language.Lang_Quality_Control_Keterangan, 280, HorizontalAlignment.Left)
            ListView1.Columns.Add(Base_Language.Lang_Quality_Control_Satuan, 100, HorizontalAlignment.Left)
            ListView1.Columns.Add("Id_qc", 0, HorizontalAlignment.Left)
            ListView1.View = View.Details

            'ListView2.Columns.Add(Base_Language.Lang_Global_KodeBarang, 150, HorizontalAlignment.Left)
            'ListView2.Columns.Add(Base_Language.Lang_Global_NamaBarang, 270, HorizontalAlignment.Left)
            'ListView2.Location = New Point(179, 95)
            'ListView2.Visible = False

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        kosong()
    End Sub

    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
        If ComboBox1.Text.Trim.Length = 0 Then Exit Sub
        If TextBox3.Text.Trim.Length = 0 Then Exit Sub

        Cari("T")
    End Sub

    Private Sub ComboBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox1.KeyPress
        If e.KeyChar = Chr(13) Then TextBox3.Focus()
    End Sub

    Private Sub TextBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox3.KeyPress
        If e.KeyChar = Chr(13) Then Btn_Cari.Focus()
    End Sub

    Private Sub ListView1_DoubleClick(sender As Object, e As EventArgs) Handles ListView1.DoubleClick
        If ListView1.Items.Count = 0 Or ListView1.SelectedItems.Count = 0 Then
            MessageBox.Show("Tidak ada data . . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        ElseIf ComboBox2.Text.Trim.Length = 0 Then
            MessageBox.Show("Pilih dahulu Kategori . . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Get_Isi_ListviewData(ListView1.FocusedItem.Index)



        For ind = 0 To DgvSimulasi_DataHPP.Rows.Count - 1
            Get_Isi_Listview(ind)

            If LvIDUji = LvData_ID Then
                MessageBox.Show("Data Sudah di Tambahkan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

        Next

        DgvSimulasi_DataHPP.Rows.Add(1)
        Dim index As Integer = DgvSimulasi_DataHPP.Rows.Count - 1
        Try
            OpenConn()

            SQL = "select a.id_qc_formula, a.Kode_uji, a.Keterangan, satuan, a.Id_Kategori_Komponen, b.Keterangan as Jenis_input, "
            SQL = SQL & "isnull(flag_option,'T') as flag_option, isnull(flag_input,'T') as flag_input, isnull(Flag_Slider,'T') as Flag_Slider "
            SQL = SQL & "from EMI_Quality_Control a, emi_kategori_komponen b "
            SQL = SQL & "where a.id_kategori_komponen=b.id_kategori_komponen and a.kode_perusahaan = '" & KodePerusahaan & "' and a.Id_QC_Formula = '" & LvData_ID & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellIDUji).Value = Dr("Id_QC_Formula")
                    DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellKodeUji).Value = Dr("Kode_Uji")
                    DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellKet).Value = Dr("Keterangan")
                    DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellSatuan).Value = Dr("Satuan")

                    'DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellJenis).Value = Dr("Id_Kategori_Komponen")
                    'DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellTampilMasuk).Value = False
                    'DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellTampilBongkar).Value = False
                Else
                    DgvSimulasi_DataHPP.Rows.RemoveAt(index)
                    Dr.Close()
                    CloseConn()
                    MessageBox.Show("Data Tidak di Temukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        kosong()
    End Sub

    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
        If ComboBox2.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Quality_Control_Error_Kategori, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox2.Focus() : Exit Sub
            'ElseIf TextBox4.Text.Trim.Length = 0 Then
            '    MessageBox.Show(Base_Language.Lang_Quality_Control_Error_Keterangan, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    TextBox4.Focus() : Exit Sub
        ElseIf DgvSimulasi_DataHPP.Rows.Count = 0 Then
            MessageBox.Show(Base_Language.Lang_Global_Data_Tdk_Ditemukan, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            DgvSimulasi_DataHPP.Focus() : Exit Sub
        End If



        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            SQL = "delete from EMI_Kategori_QC_Detail where "
            SQL = SQL & "Kode_Perusahaan ='" & KodePerusahaan & "' and "
            SQL = SQL & "ID_Kategori_QC='" & arrID_Kategori.Item(ComboBox2.SelectedIndex) & "' "
            ExecuteTrans(SQL)

            For index = 0 To DgvSimulasi_DataHPP.Rows.Count - 1
                Get_Isi_Listview(index)

                SQL = "insert into EMI_Kategori_QC_Detail(Kode_Perusahaan,ID_Kategori_QC,ID_QC_Formula) "
                SQL = SQL & "values('" & KodePerusahaan & "', '" & arrID_Kategori.Item(ComboBox2.SelectedIndex) & "', '" & LvIDUji & "')"
                ExecuteTrans(SQL)
            Next


            Cmd.Transaction.Commit()
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        kosong()
    End Sub

    Private Sub Btn_Hapus_Click(sender As Object, e As EventArgs) Handles Btn_Hapus.Click
        If ComboBox2.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Quality_Control_Error_Kategori, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox2.Focus() : Exit Sub
        End If
        Dim Hapus1 As String = MessageBox.Show(Base_Language.Lang_Global_Tanya_Hapus, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If Hapus1 = vbYes Then
            Try
                OpenConn()
                Cmd.Transaction = Cn.BeginTransaction

                SQL = "delete from EMI_Kategori_QC_Detail where "
                SQL = SQL & "Kode_Perusahaan ='" & KodePerusahaan & "' and "
                SQL = SQL & "ID_Kategori_QC='" & arrID_Kategori.Item(ComboBox2.SelectedIndex) & "' "
                ExecuteTrans(SQL)

                Cmd.Transaction.Commit()
                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        Else
            MessageBox.Show(Base_Language.Lang_Global_Hapus_No, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        kosong()
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If TextBox1.Text.Trim.Length = 0 Then
            ListView2.Visible = False : Exit Sub
        Else
            ListView2.Visible = True
        End If

        ListView2.Items.Clear()
        Dim lv As New ListViewItem

        Try
            OpenConn()

            SQL = "select Kode_Barang,Nama from Barang where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "Kode_Barang like '%" & TextBox1.Text & "%' "
            SQL = SQL & "group by Kode_Barang,Nama order by Nama"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    lv = ListView2.Items.Add(Dr("Kode_Barang"))
                    lv.SubItems.Add(Dr("Nama"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub


    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged
        If TextBox4.Text.Trim.Length = 0 Then
            ListView2.Visible = False : Exit Sub
        Else
            ListView2.Visible = True
        End If

        ListView2.Items.Clear()
        Dim lv As New ListViewItem
        Try
            OpenConn()

            SQL = "select Kode_Barang,Nama from Barang where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "Nama like '%" & TextBox4.Text & "%' "
            SQL = SQL & "group by Kode_Barang,Nama order by Nama"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    lv = ListView2.Items.Add(Dr("Kode_Barang"))
                    lv.SubItems.Add(Dr("Nama"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Down Then
            ListView2.Focus()
        End If
    End Sub

    Private Sub TextBox4_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox4.KeyDown
        If e.KeyCode = Keys.Down Then
            If ListView2.Items.Count = 0 Then Exit Sub
            ListView2.Focus()
        End If
    End Sub

    Private Sub TextBox4_Leave(sender As Object, e As EventArgs) Handles TextBox4.Leave
        If ListView2.Focused = True Then Exit Sub
        TextBox3.Text = "" : TextBox4.Text = ""
    End Sub

    Private Sub ListView2_DoubleClick(sender As Object, e As EventArgs) Handles ListView2.DoubleClick
        If ListView2.Items.Count = 0 Then Exit Sub
        Dim kode As String = ListView2.FocusedItem.Text
        Dim nama As String = ListView2.FocusedItem.SubItems(1).Text
        TextBox1.Text = kode
        TextBox4.Text = nama
        ListView2.Visible = False
        'TextBox1_Leave(ListView1, e)
    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged

    End Sub

    Private Sub DgvSimulasi_DataHPP_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvSimulasi_DataHPP.CellContentClick

    End Sub

    Private Sub ListView2_KeyDown(sender As Object, e As KeyEventArgs) Handles ListView2.KeyDown
        If e.KeyCode = Keys.Enter Then
            ListView2_DoubleClick(ListView2, e)
        End If
    End Sub

    Private Sub DgvSimulasi_DataHPP_KeyDown(sender As Object, e As KeyEventArgs) Handles DgvSimulasi_DataHPP.KeyDown
        If DgvSimulasi_DataHPP.Rows.Count = 0 Or DgvSimulasi_DataHPP.SelectedCells.Count = 0 Then
            Exit Sub
        End If

        Dim currentRow = DgvSimulasi_DataHPP.CurrentRow.Index
        Dim currentCell = DgvSimulasi_DataHPP.CurrentCellAddress.X

        If e.KeyCode = Keys.Delete Then

            If ComboBox2.Text.Trim.Length = 0 Then
                MessageBox.Show(Base_Language.Lang_Quality_Control_Error_Kategori, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                ComboBox2.Focus() : Exit Sub
                'ElseIf TextBox4.Text.Trim.Length = 0 Then
                '    MessageBox.Show(Base_Language.Lang_Quality_Control_Error_Keterangan, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    TextBox4.Focus() : Exit Sub
            ElseIf DgvSimulasi_DataHPP.Rows.Count = 0 Then
                MessageBox.Show(Base_Language.Lang_Global_Data_Tdk_Ditemukan, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                DgvSimulasi_DataHPP.Focus() : Exit Sub
            End If

            Dim Hapus1 As String = MessageBox.Show(Base_Language.Lang_Global_Tanya_Hapus, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If Hapus1 = vbYes Then
                Try
                    OpenConn()
                    Cmd.Transaction = Cn.BeginTransaction

                    Get_Isi_Listview(DgvSimulasi_DataHPP.CurrentRow.Index)
                    Dim a As Integer = DgvSimulasi_DataHPP.CurrentRow.Index
                    ''SQL = "delete from EMI_Kategori_QC_Detail where "
                    ''SQL = SQL & "Kode_Perusahaan ='" & KodePerusahaan & "' and "
                    ''SQL = SQL & "ID_Kategori_QC='" & arrID_Kategori.Item(ComboBox2.SelectedIndex) & "' "
                    ''SQL = SQL & "and ID_QC_Formula = '" & LvIDUji & "' "
                    ''ExecuteTrans(SQL)

                    BeginInvoke(New MethodInvoker(Sub() DgvSimulasi_DataHPP.Rows.RemoveAt(currentRow)))

                    Cmd.Transaction.Commit()
                    CloseConn()
                Catch ex As Exception
                    CloseConn()
                    MessageBox.Show(ex.Message)
                    Exit Sub
                End Try
            Else
                MessageBox.Show(Base_Language.Lang_Global_Hapus_No, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            'ComboBox2_SelectedIndexChanged(DgvSimulasi_DataHPP, e)



        End If
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        If ComboBox2.SelectedIndex = -1 Then
            DgvSimulasi_DataHPP.Rows.Clear()
            Exit Sub
        End If
        Try
            OpenConn()

            DgvSimulasi_DataHPP.Rows.Clear()
            SQL = "select a.ID_Kategori_QC,a.ID_QC_Formula,b.Kode_Uji,b.Keterangan,b.Satuan "
            SQL = SQL & "from EMI_Kategori_QC_Detail a, EMI_Quality_Control b "
            SQL = SQL & "where a.Kode_Perusahaan = b.kode_Perusahaan and a.ID_QC_Formula = b.ID_QC_Formula "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.ID_Kategori_QC = '" & arrID_Kategori.Item(ComboBox2.SelectedIndex) & "'"
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For index As Integer = 0 To .Rows.Count - 1
                            DgvSimulasi_DataHPP.Rows.Add()
                            DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellIDUji).Value = .Rows(index).Item("id_qc_Formula")
                            DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellKodeUji).Value = .Rows(index).Item("Kode_Uji")
                            DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellKet).Value = .Rows(index).Item("Keterangan")
                            DgvSimulasi_DataHPP.Rows.Item(index).Cells(CellSatuan).Value = .Rows(index).Item("Satuan")
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



    Private Sub DgvSimulasi_DataHPP_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvSimulasi_DataHPP.CellDoubleClick

    End Sub
End Class
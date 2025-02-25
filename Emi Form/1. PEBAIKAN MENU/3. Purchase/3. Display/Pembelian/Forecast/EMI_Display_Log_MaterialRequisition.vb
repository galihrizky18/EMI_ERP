Public Class EMI_Display_Log_MaterialRequisition

    Dim Arr1, Arr2, Arr3, Arr4, arrBulan, arrBulanMM, arrCmbParamBarang As New ArrayList
    Dim pertama As Integer = 1
    Dim T As Color = Color.Blue
    Dim KT As Color = Color.Red
    Dim KY As Color = Color.Green
    Dim Batal As Color = Color.Black

    Private Sub Display_Pembelian_Barang_Masuk_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            OpenConn()

            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            'Base_Language.Get_Languages(Bahasa_Pilihan, "Display_Barang_Masuk")
            'Base_Language.Get_Languages(Bahasa_Pilihan, "Pembelian_Barang_Masuk")

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Lv_PR.Columns.Clear()
        Lv_PR.Columns.Add("No Faktur", 0, HorizontalAlignment.Left)
        Lv_PR.Columns.Add("Kode Barang", 125, HorizontalAlignment.Left)
        Lv_PR.Columns.Add("Nama Barang", 250, HorizontalAlignment.Left)
        Lv_PR.Columns.Add("Satuan", 120, HorizontalAlignment.Center)
        Lv_PR.Columns.Add("Bulan", 100, HorizontalAlignment.Center)
        Lv_PR.Columns.Add("Tahun", 100, HorizontalAlignment.Center)
        Lv_PR.Columns.Add("Jenis", 100, HorizontalAlignment.Center)
        Lv_PR.Columns.Add("Jumlah Lama BOM", 150, HorizontalAlignment.Right)
        Lv_PR.Columns.Add("Jumlah Lama PPIC", 150, HorizontalAlignment.Right)
        Lv_PR.Columns.Add("User ID", 100, HorizontalAlignment.Left)
        'Lv_PR.View = View.details
        kosong()
    End Sub

    Private Sub kosong()



        Try
            OpenConn()



            Cmb_Bulan.Items.Clear() : arrBulan.Clear() : arrBulanMM.Clear()
            Cmb_Bulan.Items.Add("January") : arrBulan.Add("1") : arrBulanMM.Add("01")
            Cmb_Bulan.Items.Add("February") : arrBulan.Add("2") : arrBulanMM.Add("02")
            Cmb_Bulan.Items.Add("March") : arrBulan.Add("3") : arrBulanMM.Add("03")
            Cmb_Bulan.Items.Add("April") : arrBulan.Add("4") : arrBulanMM.Add("04")
            Cmb_Bulan.Items.Add("May") : arrBulan.Add("5") : arrBulanMM.Add("05")
            Cmb_Bulan.Items.Add("June") : arrBulan.Add("6") : arrBulanMM.Add("06")
            Cmb_Bulan.Items.Add("July") : arrBulan.Add("7") : arrBulanMM.Add("07")
            Cmb_Bulan.Items.Add("August") : arrBulan.Add("8") : arrBulanMM.Add("08")
            Cmb_Bulan.Items.Add("September") : arrBulan.Add("9") : arrBulanMM.Add("09")
            Cmb_Bulan.Items.Add("October") : arrBulan.Add("10") : arrBulanMM.Add("10")
            Cmb_Bulan.Items.Add("November") : arrBulan.Add("11") : arrBulanMM.Add("11")
            Cmb_Bulan.Items.Add("Desember") : arrBulan.Add("12") : arrBulanMM.Add("12")
            Cmb_Bulan.SelectedIndex = -1

            Cmb_ParamLain.Items.Clear()
            Dim tahun_awal As Integer = Date.Now.Year - 2
            Dim tahun_akhir As Integer = Date.Now.Year + 2
            For a As Integer = tahun_awal To tahun_akhir
                Cmb_Tahun.Items.Add(a)
            Next
            Cmb_Tahun.SelectedIndex = -1

            'TextBoxa.Text = "0" 
            Cmb_Bulan.Enabled = False : Cmb_ParamLain.Enabled = False : Cmb_Tahun.Enabled = False
            Txt_ParamLain.Enabled = False : Txt_ParamLain.Text = ""
            Txt_KdBrg.Enabled = False : Txt_KdBrg.Text = ""


            Cmb_ParamLain.Items.Clear() : Cmb_ParamLain.Text = "" : Arr2.Clear()
            Cmb_ParamLain.Items.Add("Jenis") : Arr2.Add("Jenis")
            Cmb_ParamLain.Items.Add("UserID") : Arr2.Add("UserID")
            Cmb_ParamLain.Items.Add("Satuan") : Arr2.Add("Satuan")
            'ComboBox2.Items.Add("Kode Supplier") : Arr2.Add("a.kode_supplier")

            Label1.Text = "Summary Data - Purchase Requisition"
            'CheckBox3.Text = Base_Language.Lang_Global_Hari_ini
            Cb_ParamTgl.Text = Base_Language.Lang_Global_Para_Tbl
            Cb_ParamLain.Text = Base_Language.Lang_Global_Para_lain
            Btn_Cari.Text = Base_Language.Lang_Global_Cari

            Cmb_KategoriBesar.Items.Clear() : Cmb_KategoriBesar.SelectedIndex = -1 : Cmb_KategoriBesar.Enabled = False
            Cmb_KategoriKecil.Items.Clear() : Cmb_KategoriKecil.SelectedIndex = -1 : Cmb_KategoriKecil.Enabled = False
            Cmb_KategoriBesar.Items.Add("---SELURUH---")
            SQL = "select Kode_Kategori_Besar from Kategori_Besar where Kode_Perusahaan = "
            SQL = SQL & "'" & KodePerusahaan & "' order by Kode_Kategori_Besar"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Cmb_KategoriBesar.Items.Add(Dr("Kode_Kategori_Besar"))
                Loop
            End Using

            Cmb_KategoriBesar.SelectedIndex = 0


            Cmb_ParamLain.Items.Clear() : arrCmbParamBarang.Clear()
            cmbParameterBarang.Items.Add("Kode Barang") : arrCmbParamBarang.Add("kode_barang")
            cmbParameterBarang.Items.Add("Nama") : arrCmbParamBarang.Add("Nama")

            cmbParameterBarang.SelectedIndex = 0

            Cb_ParamBrg.Checked = True
            Lv_PR.Items.Clear()

            Cmb_ParamLain.Items.Clear() : Cmb_ParamLain.Text = "" : Arr2.Clear()
            Cmb_ParamLain.Items.Add("Satuan") : Arr2.Add("satuan")
            Cmb_ParamLain.Items.Add("Jenis") : Arr2.Add("Jenis")
            Cmb_ParamLain.Items.Add("User ID") : Arr2.Add("UserID")



            Cb_ParamTgl.Checked = False
            Cb_ParamLain.Checked = False
            'CheckBox3.Checked = False

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        BtnBarangMasuk_Cari_Click(Me, Nothing)
    End Sub

    'Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs)
    '    If CheckBox3.Checked = True Then
    '        CheckBox1.Checked = False
    '        ComboBox1.Enabled = False
    '        ComboBox3.Enabled = False
    '        ComboBox1.SelectedIndex = -1
    '        ComboBox3.SelectedIndex = -1
    '    End If
    'End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles Cb_ParamTgl.CheckedChanged

        If Cb_ParamTgl.Checked = True Then
            'CheckBox3.Checked = False
            Cmb_Tahun.Enabled = True
            Cmb_Bulan.Enabled = True

            Cb_ParamBrg.Checked = False
            Cb_ParamLain.Checked = False

            Cmb_Tahun.SelectedIndex = -1
            Cmb_Bulan.SelectedIndex = -1
        Else
            Cmb_Tahun.Enabled = False
            Cmb_Bulan.Enabled = False
            Cmb_Tahun.SelectedIndex = -1
            Cmb_Bulan.SelectedIndex = -1
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles Cb_ParamLain.CheckedChanged

        If Cb_ParamLain.Checked = True Then
            Cmb_ParamLain.Enabled = True
            Txt_ParamLain.Enabled = True
            Cb_ParamBrg.Checked = False
            Cb_ParamTgl.Checked = False
        Else
            Cmb_ParamLain.Enabled = False
            Txt_ParamLain.Enabled = False
            Cmb_ParamLain.SelectedIndex = -1
            Txt_ParamLain.Text = ""
        End If
    End Sub

    Private Sub CheckBox4_CheckedChanged(sender As Object, e As EventArgs) Handles Cb_ParamBrg.CheckedChanged


        If Cb_ParamBrg.Checked = True Then
            Txt_KdBrg.Enabled = True : Txt_KdBrg.Text = ""

            cmbParameterBarang.Enabled = True : cmbParameterBarang.SelectedIndex = 0
            Cmb_KategoriBesar.Enabled = True : Cmb_KategoriBesar.SelectedIndex = 0
            Cmb_KategoriKecil.Enabled = True

            Cb_ParamTgl.Checked = False
            Cb_ParamLain.Checked = False
        Else
            Txt_KdBrg.Enabled = False : Txt_KdBrg.Text = ""
            cmbParameterBarang.Enabled = False : cmbParameterBarang.SelectedIndex = -1
            Cmb_KategoriBesar.Enabled = False : Cmb_KategoriBesar.SelectedIndex = -1
            Cmb_KategoriKecil.Enabled = False : Cmb_KategoriKecil.SelectedIndex = -1
        End If
    End Sub

    Private Sub ComboBox5_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_KategoriBesar.SelectedIndexChanged

        Try
            OpenConn()

            Cmb_KategoriKecil.Items.Clear() : Cmb_KategoriKecil.SelectedIndex = -1
            Cmb_KategoriKecil.Items.Add("---SELURUH---")
            SQL = "select Kode_Kategori_Kecil from Kategori_Kecil where Kode_Perusahaan = "
            SQL = SQL & "'" & KodePerusahaan & "' "
            If Cmb_KategoriBesar.SelectedIndex = 0 Then
                SQL = SQL & " "
            Else
                SQL = SQL & "and Kode_Kategori_Besar = '" & Cmb_KategoriBesar.Text & "' "
            End If
            SQL = SQL & "order by Kode_Kategori_Kecil"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Cmb_KategoriKecil.Items.Add(Dr("Kode_Kategori_Kecil"))
                Loop
            End Using

            If Cmb_KategoriBesar.SelectedIndex = 0 Then
                Cmb_KategoriKecil.SelectedIndex = 0
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub Cmb_ParamLain_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_ParamLain.SelectedIndexChanged

    End Sub

    Private Sub Txt_KdBrg_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_KdBrg.KeyPress
        If e.KeyChar = Chr(13) Then
            BtnBarangMasuk_Cari_Click(Me, Nothing)
        End If
    End Sub

    Private Sub BtnBarangMasuk_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click


        If Cb_ParamBrg.Checked = True Then

        ElseIf Cb_ParamTgl.Checked = True Then
            If Cmb_Bulan.Text.Trim.Length = 0 Then
                MessageBox.Show("Bulan harus diisi . . ! !", "Perhatian")
                Cmb_Bulan.Focus() : Exit Sub
            ElseIf Cmb_Tahun.Text.Trim.Length = 0 Then
                MessageBox.Show("Tahun harus diisi . . ! !", "Perhatian")
                Cmb_Tahun.Focus() : Exit Sub
            End If
        ElseIf Cb_ParamLain.Checked = True Then
            If Cmb_ParamLain.Text.Trim.Length = 0 Then
                MessageBox.Show("Parameter harus diisi . . ! !", "Perhatian")
                Cmb_ParamLain.Focus() : Exit Sub
            ElseIf Txt_ParamLain.Text.Trim.Length = 0 Then
                MessageBox.Show("Value harus diisi . . ! !", "Perhatian")
                Txt_ParamLain.Focus() : Exit Sub
            End If
        End If

        Try
            OpenConn()

            Lv_PR.Items.Clear()
            SQL = ";with cte as  ( "
            SQL = SQL & "select b.urut, d.Kode_Kategori_Besar,d.Kode_Kategori_Kecil,a.kode_perusahaan, b.No_Faktur,b.Kode_Barang,d.Nama,b.Satuan,b.Bulan,b.Tahun,a.Jenis,a.Jumlah_Lama_BOM,a.Jumlah_Lama_PPIC, "
            SQL = SQL & "cast(format(a.tanggal,'dd MMM yyyy') as Varchar(20)) as Tanggal,a.Jam ,a.userid from EMI_Transaksi_Material_Requsition_Log a,EMI_Transaksi_Material_Requsition_Detail b, barang d "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur and a.Urut_Detail = b.Urut "
            SQL = SQL & "and b.Kode_Perusahaan = d.Kode_Perusahaan and b.Kode_Stock_Owner = d.Kode_Stock_Owner and b.Kode_Barang = d.Kode_Barang "
            SQL = SQL & "union all  "
            SQL = SQL & "select b.urut, d.Kode_Kategori_Besar,d.Kode_Kategori_Kecil,b.kode_perusahaan, b.No_Faktur,b.Kode_Barang,d.Nama,b.Satuan,b.Bulan,b.Tahun,'-' as jenis,b.Nilai_Bom,b.Nilai_PPIC, '-' as tanggal, '-' as jam ,'-' as userid "
            SQL = SQL & "from EMI_Transaksi_Material_Requsition_Detail b, EMI_Transaksi_Material_Requsition c,Barang d where "
            SQL = SQL & "c.Kode_Perusahaan = b.Kode_Perusahaan and c.No_Faktur = b.No_Faktur "
            SQL = SQL & "and b.Kode_Perusahaan = d.Kode_Perusahaan and b.Kode_Stock_Owner = d.Kode_Stock_Owner and b.Kode_Barang = d.Kode_Barang and c.Status is null ) "
            SQL = SQL & "select * from cte where kode_perusahaan = '" & KodePerusahaan & "'  "
            If Cb_ParamTgl.Checked Then
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "
                SQL = SQL & " Bulan = '" & arrBulanMM.Item(Cmb_Bulan.SelectedIndex) & "' and Tahun = '" & Cmb_Tahun.Text & "' "
            End If

            If Cb_ParamBrg.Checked Then
                'Pasang And
                If Txt_KdBrg.Text = "---SELURUH---" Then
                    SQL = SQL & ""
                Else
                    SQL = SQL & "and " & arrCmbParamBarang.Item(cmbParameterBarang.SelectedIndex) & " like '%" & Txt_KdBrg.Text & "%' "
                End If

                If Cmb_KategoriBesar.SelectedIndex = 0 Then
                    SQL = SQL & ""
                Else
                    SQL = SQL & "and Kode_Kategori_Besar = '" & Cmb_KategoriBesar.Text & "' "
                End If

                If Cmb_KategoriKecil.SelectedIndex = 0 Then
                    SQL = SQL & ""
                Else
                    SQL = SQL & "and Kode_Kategori_Kecil = '" & Cmb_KategoriKecil.Text & "' "
                End If
            End If

            If Cb_ParamLain.Checked Then
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "
                SQL = SQL & Arr2.Item(Cmb_ParamLain.SelectedIndex) & " like '%" & Trim(Txt_ParamLain.Text) & "%' "
            End If



            SQL = SQL & "order by Tahun,Bulan,Nama, urut "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim LV As New ListViewItem
                    LV = Lv_PR.Items.Add(dr("No_Faktur"))
                    LV.SubItems.Add(dr("Kode_Barang"))
                    LV.SubItems.Add(dr("Nama"))
                    LV.SubItems.Add(dr("Satuan"))
                    'LV.SubItems.Add(dr("Bulan"))
                    'Dim a As Date = CDate(dr("Bulan"))
                    'LV.SubItems.Add(Format(a, "MMMM"))

                    Dim bulanNumeric As String = dr("bulan").ToString()
                    Dim bulanNama As String = ""
                    Select Case bulanNumeric
                        Case "01"
                            bulanNama = "January"
                        Case "02"
                            bulanNama = "February"
                        Case "03"
                            bulanNama = "March"
                        Case "04"
                            bulanNama = "April"
                        Case "05"
                            bulanNama = "May"
                        Case "06"
                            bulanNama = "June"
                        Case "07"
                            bulanNama = "July"
                        Case "08"
                            bulanNama = "August"
                        Case "09"
                            bulanNama = "September"
                        Case "10"
                            bulanNama = "October"
                        Case "11"
                            bulanNama = "November"
                        Case "12"
                            bulanNama = "December"
                        Case Else
                            bulanNama = "Invalid Month"
                    End Select
                    LV.SubItems.Add(bulanNama)

                    LV.SubItems.Add(dr("Tahun"))
                    LV.SubItems.Add(dr("Jenis"))
                    LV.SubItems.Add(dr("Jumlah_Lama_BOM"))
                    LV.SubItems.Add(dr("Jumlah_Lama_PPIC"))
                    LV.SubItems.Add(dr("UserID"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        kosong()
    End Sub

    Private Sub ListView2_DoubleClick(sender As Object, e As EventArgs) Handles ListView2.DoubleClick
        If ListView2.Items.Count = 0 Then Exit Sub
        Dim kode As String = ListView2.FocusedItem.Text
        Dim nama As String = ListView2.FocusedItem.SubItems(1).Text
        Txt_KdBrg.Text = kode

        ListView2.Visible = False
        'ComboBox3.Focus()
    End Sub

    Private Sub TextBox4_KeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode = Keys.Down Then
            If ListView2.Items.Count = 0 Then Exit Sub
            ListView2.Focus()
        End If
    End Sub



    Private Sub TextBox4_Leave(sender As Object, e As EventArgs)
        If ListView2.Focused = True Then Exit Sub

    End Sub


End Class
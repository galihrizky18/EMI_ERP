Public Class SD_Pilih_Satuan_Turunan
    Public asal As String
    Dim arrcari, arrIdPenanggungJawab As New ArrayList
    Dim Jenis = "barang"

    Dim lvSatuan As String
    Dim LvJumlah As String
    Dim LvFlagTampil As String


    Dim skipData As String = "Y"


    Dim cellSatuan As Integer = 0
    Dim cellJumlah As Integer = 1
    Dim cellFlagTampil As Integer = 2




    Public Sub Get_Isi_Listview(ByVal No_Index As Integer)



    End Sub
    Private Sub Get_Data()


        Try
            OpenConn()
            lvwSatuan.Visible = True
            lvwSatuan.Items.Clear()

            'SQL = "select satuan_akhir,nilai_pengali,flag_general from EMI_Satuan_Detail_Perhitungan where kode_perusahaan = '" & KodePerusahaan & "' "
            'SQL = SQL & "and satuan_awal = '" & Master_Barang_New.cmbSatuan.Text & "' and jenis = 'masa' "
            'SQL = SQL & "and satuan_akhir <> '" & Master_Barang_New.cmbSatuan.Text & "' "

            SQL = "select Satuan from emi_satuan "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and Flag_Barang = 'Y' "

            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lvw As ListViewItem
                    lvw = lvwSatuan.Items.Add("")
                    'lvw.SubItems.Add(Dr("satuan_akhir"))
                    'lvw.SubItems.Add(Dr("nilai_pengali"))

                    lvw.SubItems.Add(Dr("Satuan"))
                    lvw.SubItems.Add(0)

                    lvw.SubItems.Add("Y")

                    'If General_Class.CekNULL(Dr("flag_general")) = "" Then
                    '    lvw.SubItems.Add("T")
                    'ElseIf Dr("flag_general") = "T" Then
                    '    lvw.SubItems.Add("T")
                    'Else
                    '    lvw.SubItems.Add("Y")
                    'End If

                Loop
            End Using

            CloseConn()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


    End Sub


    'Private Sub BtnInquiry_Refresh_Click(sender As Object, e As EventArgs) Handles BtnInquiry_Refresh.Click
    '    Get_Data()
    'End Sub

    Private Sub Transaksi_Formulator_Pilih_Barang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            OpenConn()

            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)

            lblSatuan.Text = Base_Language.Lang_Global_Satuan
            Label1.Text = Base_Language.Lang_Barang_Sd_Satuan_Judul



            lvwSatuan.Columns.Clear()
            lvwSatuan.Columns.Add("Pilih", 100, HorizontalAlignment.Left)
            lvwSatuan.Columns.Add(Base_Language.Lang_Global_Satuan, 150, HorizontalAlignment.Left)
            lvwSatuan.Columns.Add(Base_Language.Lang_Global_Satuan, 150, HorizontalAlignment.Center)
            lvwSatuan.Columns.Add(Base_Language.Lang_Global_Satuan, 150, HorizontalAlignment.Left)
            'DgvSatuanTerpilih.Columns(cellSatuan).HeaderText = Base_Language.Lang_Global_Satuan
            'DgvSatuanTerpilih.Columns(cellJumlah).HeaderText = Base_Language.Lang_Global_Jumlah
            'DgvSatuanTerpilih.Columns(cellFlagTampil).HeaderText = Base_Language.Lang_Barang_Flag_Tampil_Display

            CheckBox1.Checked = False


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Get_Data()
    End Sub

    Private Sub Btn_Simpan_Area_Click(sender As Object, e As EventArgs) Handles Btn_Pilih.Click
        If lvwSatuan.CheckedItems.Count = 0 Then
            MessageBox.Show(Base_Language.Lang_Barang_Satuan_Belum_Dipilih, Judul, MessageBoxButtons.OK)
            Exit Sub
        End If

        Try

            OpenConn()


            For i As Integer = 0 To lvwSatuan.Items.Count - 1





                If lvwSatuan.Items(i).Checked = True Then


                    For indexDgv As Integer = 0 To Master_Barang_New.DgvSatuanTerpilih.Rows.Count - 1
                        If lvwSatuan.Items(i).SubItems(1).Text = Master_Barang_New.DgvSatuanTerpilih.Rows(indexDgv).Cells(0).Value Then
                            MessageBox.Show(Base_Language.Lang_Global_Satuan & " " & lvwSatuan.Items(i).SubItems(1).Text & " " & Base_Language.Lang_Barang_Err_Sudah_Di_List, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    Next

                    Dim rows As Integer = Master_Barang_New.DgvSatuanTerpilih.Rows.Count

                    Master_Barang_New.DgvSatuanTerpilih.Rows.Add(1)
                    Master_Barang_New.DgvSatuanTerpilih.Rows(rows).Cells(0).Value = lvwSatuan.Items(i).SubItems(1).Text

                    Master_Barang_New.DgvSatuanTerpilih.Rows(rows).Cells(3).Value = lvwSatuan.Items(i).SubItems(3).Text


                    If lvwSatuan.Items(i).SubItems(3).Text = "Y" Then
                        'Master_Barang_New.DgvSatuanTerpilih.Rows(rows).Cells(1).ReadOnly = True
                        Master_Barang_New.DgvSatuanTerpilih.Rows(rows).Cells(1).Style.BackColor = Color.Yellow

                        'Master_Barang_New.DgvSatuanTerpilih.Rows(rows).Cells(1).Value = lvwSatuan.Items(i).SubItems(2).Text
                        Master_Barang_New.DgvSatuanTerpilih.Rows(rows).Cells(1).Value = 0
                    Else
                        Master_Barang_New.DgvSatuanTerpilih.Rows(rows).Cells(1).Value = 0
                    End If


                End If
            Next



            Me.Close()
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub CheckBox1_CheckedChanged_1(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged

        If CheckBox1.Checked = True Then
            For i As Integer = 0 To lvwSatuan.Items.Count - 1
                lvwSatuan.Items(i).Checked = True
            Next
        Else
            For i As Integer = 0 To lvwSatuan.Items.Count - 1
                lvwSatuan.Items(i).Checked = False
            Next

        End If

    End Sub

    'Private Sub lvwSatuan_ItemChecked(sender As Object, e As ItemCheckedEventArgs) Handles lvwSatuan.ItemChecked


    '    If skipData = "Y" Then


    '        If lvwSatuan.CheckedItems.Count < lvwSatuan.Items.Count Then
    '            CheckBox1.Checked = False
    '        ElseIf lvwSatuan.CheckedItems.Count = lvwSatuan.Items.Count Then
    '            CheckBox1.Checked = True
    '        End If



    '        If lvwSatuan.CheckedItems.Count > 0 Then
    '            Dim rows As Integer = 0



    '            Try
    '                OpenConn()


    '                For Each item In lvwSatuan.CheckedItems
    '                    SQL = "select  satuan_akhir,nilai_pengali from EMI_Satuan_Detail_Perhitungan where kode_perusahaan = '" & KodePerusahaan & "' "
    '                    SQL = SQL & "and   satuan_awal = '" & txtSatuan.Text & "' and satuan_akhir = '" & lvwSatuan.Items(0).(1).Text & "'  "
    '                    SQL = SQL & "and jenis = 'Masa' and flag_general = 'Y' "
    '                    Using Dr = OpenTrans(SQL)
    '                        If Dr.Read Then

    '                            Dim rows As Integer = 0
    '                            Master_Barang_New.DgvSatuanTerpilih.Rows.Add(1)
    '                            Master_Barang_New.DgvSatuanTerpilih.Rows(rows).Cells(0).Value = Dr("satuan_akhir")
    '                            Master_Barang_New.DgvSatuanTerpilih.Rows(rows).Cells(0).Value = Dr("satuan_akhir")

    '                        Else
    '                            MessageBox.Show("Terjadi kesalahan pada satuan", Judul, MessageBoxButtons.OK)
    '                        End If
    '                    End Using
    '                Next


    '                CloseConn()
    '            Catch ex As Exception
    '                CloseConn()
    '                MessageBox.Show(ex.Message)
    '                Exit Sub
    '            End Try

    '            DgvSatuanTerpilih.Rows.Add(1)

    '            'DgvSatuanTerpilih.Rows(rows).Cells(0).Value = 

    '        Else
    '            DgvSatuanTerpilih.Rows.Clear()
    '        End If


    '    End If
    'End Sub


End Class
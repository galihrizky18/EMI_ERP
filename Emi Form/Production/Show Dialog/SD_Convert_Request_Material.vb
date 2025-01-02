Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button

Public Class SD_Convert_Request_Material

    Public noFaktur

    Dim LvKso As String
    Dim LvKodeBarang As String
    Dim LvNama As String
    Dim LvJumlah As String
    Dim LvSatuan As String
    Dim LvWarna As String
    Dim LvNoUrutDet As String
    Dim LvNoFaktur As String

    Dim cellKso As Integer = 0
    Dim cellKodeBrg As Integer = 1
    Dim cellNama As Integer = 2
    Dim cellJumlah As Integer = 3
    Dim cellSatuan As Integer = 4
    Dim cellWarna As Integer = 5
    Dim cellNoUrutDet As Integer = 6
    Dim cellNoFaktur As Integer = 7




    Private Sub Get_Isi_ListView(ByVal NoIndex As Integer)
        LvKso = ListView1.Items(cellKso).Text
        LvKodeBarang = ListView1.Items(NoIndex).SubItems(cellKodeBrg).Text
        LvNama = ListView1.Items(NoIndex).SubItems(cellNama).Text
        LvJumlah = ListView1.Items(NoIndex).SubItems(cellJumlah).Text
        LvSatuan = ListView1.Items(NoIndex).SubItems(cellSatuan).Text
        LvWarna = ListView1.Items(NoIndex).SubItems(cellWarna).Text
        LvNoUrutDet = ListView1.Items(NoIndex).SubItems(cellNoUrutDet).Text
        LvNoFaktur = ListView1.Items(NoIndex).SubItems(cellNoFaktur).Text
    End Sub






    Private Sub SD_Formulator_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ListView1.Columns.Clear()
        ListView1.Columns.Add(Base_Language.Lang_Global_Lokasi, 0, HorizontalAlignment.Left)
        ListView1.Columns.Add(Base_Language.Lang_Global_KodeBarang, 110, HorizontalAlignment.Left)
        ListView1.Columns.Add(Base_Language.Lang_Global_NamaBarang, 300, HorizontalAlignment.Left)
        ListView1.Columns.Add(Base_Language.Lang_Global_Jumlah, 110, HorizontalAlignment.Center)
        ListView1.Columns.Add(Base_Language.Lang_Global_Satuan, 110, HorizontalAlignment.Center)
        ListView1.Columns.Add("Warna", 100, HorizontalAlignment.Center)
        ListView1.Columns.Add("no urut det", 0, HorizontalAlignment.Center)
        ListView1.Columns.Add("no_faktur", 0, HorizontalAlignment.Center)

        kosong()

    End Sub


    Private Sub kosong()

        Try
            OpenConn()

            cmbWarna.Items.Clear()

            cmbWarna.Items.Add("Hijau")
            cmbWarna.Items.Add("Kuning")
            cmbWarna.Items.Add("Merah")

            ListView1.Items.Clear()
            SQL = "select a.no_faktur,a.kode_stock_owner,a.kode_barang,b.nama,a.jumlah,a.satuan,a.warna,a.no_urut_det,a.urut_oto "
            SQL = SQL & "from  Emi_Material_Requisition_Det_convert a, barang b where  "
            SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan and a.kode_barang = b.kode_barang and a.kode_stock_owner = b.Kode_Stock_Owner "
            SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' and a.no_faktur = '" & txtNoFaktur.Text & "' "
            SQL = SQL & "and a.kode_stock_owner = '" & txtLokasi.Text & "' and a.kode_barang = '" & txtKodeBarang.Text & "' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lvw As ListViewItem

                    lvw = ListView1.Items.Add(Dr("kode_stock_owner"))
                    lvw.SubItems.Add(Dr("kode_barang"))
                    lvw.SubItems.Add(Dr("nama"))
                    lvw.SubItems.Add(Format(Dr("jumlah"), "N2"))
                    lvw.SubItems.Add(Dr("satuan"))
                    lvw.SubItems.Add(Dr("warna"))
                    lvw.SubItems.Add(Dr("no_urut_det"))
                    lvw.SubItems.Add(Dr("no_faktur"))

                Loop
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try



    End Sub

    Private Sub ListView1_DoubleClick(sender As Object, e As EventArgs) Handles ListView1.DoubleClick
        ListView1.FocusedItem.Remove()
    End Sub

    Private Sub kosong_sebagian()
        txtJumlah.Text = ""
        cmbWarna.SelectedIndex = -1

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        If cmbWarna.SelectedIndex = -1 Then
            MessageBox.Show("Warna harus di pilih!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            cmbWarna.Focus()
            Exit Sub
        ElseIf txtJumlah.Text.Trim.Length = 0 Then
            MessageBox.Show("Jumlah harus di isi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            cmbWarna.Focus()
            Exit Sub
        End If

        Dim totalKeseluruhanRequest As Double = 0
        For i As Integer = 0 To ListView1.Items.Count - 1
            Get_Isi_ListView(i)

            If LvWarna = cmbWarna.Text Then
                MessageBox.Show("Gagal,warna sudah ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                cmbWarna.Focus()
                Exit Sub
                'ElseIf Val(HilangkanTanda(LvJumlah)) > HilangkanTanda(TxtJmlhRequest.Text) Then
                '    MessageBox.Show("Convert tidak boleh melibihi jumlah request!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    cmbWarna.Focus()
                '    Exit Sub
            End If

            totalKeseluruhanRequest = totalKeseluruhanRequest + Val(HilangkanTanda(LvJumlah))

        Next

        totalKeseluruhanRequest = totalKeseluruhanRequest + txtJumlah.Text


        If totalKeseluruhanRequest > HilangkanTanda(TxtJmlhRequest.Text) Then
            MessageBox.Show("Convert tidak boleh melibihi jumlah request!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            cmbWarna.Focus()
            Exit Sub
        End If


        Dim lvw As ListViewItem
        lvw = ListView1.Items.Add(txtLokasi.Text.Trim)
        lvw.SubItems.Add(txtKodeBarang.Text.Trim)
        lvw.SubItems.Add(TxtNama.Text.Trim)
        lvw.SubItems.Add(Format(txtJumlah.Text))
        lvw.SubItems.Add(txtSatuan.Text.Trim)
        lvw.SubItems.Add(cmbWarna.Text)
        lvw.SubItems.Add(txtUrut.Text)
        lvw.SubItems.Add(txtNoFaktur.Text)

        kosong_sebagian()


    End Sub

    Private Sub txtJumlah_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtJumlah.KeyPress
        If e.KeyChar = Chr(13) Then
            Button3.Focus()
        End If

        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(Asc("."))) Then e.KeyChar = Chr(0)

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        kosong()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If ListView1.Items.Count = 0 Then
            MessageBox.Show("Data request tidak boleh kosong!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Dim totalKeseluruhanRequest As Double = 0
        For i As Integer = 0 To ListView1.Items.Count - 1
            Get_Isi_ListView(i)

            totalKeseluruhanRequest = totalKeseluruhanRequest + Val(HilangkanTanda(LvJumlah))

        Next

        If totalKeseluruhanRequest <> TxtJmlhRequest.Text Then
            MessageBox.Show("Gagal menyimpan, Total Convert harus sama dengan jumlah yang direquest", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Try
            OpenConn()

            Cmd.Transaction = Cn.BeginTransaction


            ' hapus table Emi_Material_Requisition_det_convert
            SQL = "delete from Emi_Material_Requisition_det_convert where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & txtNoFaktur.Text.Trim & "' "
            SQL = SQL & "and kode_stock_owner = '" & txtLokasi.Text & "' and kode_barang = '" & txtKodeBarang.Text & "' "
            ExecuteTrans(SQL)


            For i As Integer = 0 To ListView1.Items.Count - 1
                Get_Isi_ListView(i)



                '============= convert ke satuan kecil ============='
                Dim convertKeSatuanAsli_bhn As String = ""
                Dim jumlahConvertBhn As Double = 0

                SQL = "select satuan From barang where Kode_barang = '" & LvKodeBarang & "' "
                SQL = SQL & "and kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & LvKso & "' "
                Using Dr3 = OpenTrans(SQL)
                    If Dr3.Read Then
                        convertKeSatuanAsli_bhn = Dr3("satuan")
                        SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & LvKodeBarang & "',"
                        SQL = SQL & "'" & LvSatuan & "','" & Dr3("satuan") & "',"
                        SQL = SQL & "" & HilangkanTanda(LvJumlah) & ") as Hasil "
                        Dr3.Close()

                        Using dr4 = OpenTrans(SQL)
                            If dr4.Read Then
                                If General_Class.CekNULL(dr4("Hasil")) <> "" Then
                                    If dr4("Hasil") = 0 Then
                                        dr4.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Satuan " & LvSatuan & " Ke " & convertKeSatuanAsli_bhn & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    Else
                                        jumlahConvertBhn = dr4("hasil")

                                    End If
                                Else
                                    dr4.Close()
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Satuan " & LvSatuan & " Ke " & convertKeSatuanAsli_bhn & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End If
                        End Using
                    Else
                        Dr3.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Barang detail satuan belum di set!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                SQL = "insert into Emi_Material_Requisition_det_convert(Kode_Perusahaan,No_Faktur,Kode_Stock_Owner,Kode_Barang,Jumlah,Satuan,Jumlah_Barang,Satuan_Barang,Warna,No_Urut_Det)"
                SQL = SQL & "values("
                SQL = SQL & "'" & KodePerusahaan & "', '" & LvNoFaktur & "', '" & LvKso & "', '" & LvKodeBarang & "', "
                SQL = SQL & "'" & HilangkanTanda(LvJumlah) & "', "
                SQL = SQL & "'" & LvSatuan & "', '" & jumlahConvertBhn & "', '" & convertKeSatuanAsli_bhn & "', '" & LvWarna & "', '" & LvNoUrutDet & "')"
                ExecuteTrans(SQL)


            Next


            MessageBox.Show(Base_Language.Lang_Global_Alert_Simpan, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            Cmd.Transaction.Commit()
            CloseConn()

            EMI_Display_Split_Request_Material.Button1_Click(Button1, Nothing)
            Me.Close()

        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try



    End Sub


End Class
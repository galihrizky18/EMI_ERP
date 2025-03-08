Public Class EMI_Master_Meteran

    Dim JuduLForm As String = "Master Data Meteran"

    Dim arrKdJenisBiaya, arrIdJnsBiaya, arrIdWorkCenter, arrCari As New ArrayList

    Dim Lv_IdMeteran, Lv_KdMeteran, Lv_JnsBiaya, Lv_NoMeteran, Lv_Satuan As String

    Dim item_IdMeteran As Integer = 0
    Dim item_KodeMeteran As Integer = 1
    Dim item_JenisBiaya As Integer = 2
    Dim item_NoMeteran As Integer = 3
    Dim item_Satuan As Integer = 4




    Private Sub EMI_Master_Meteran_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub
    Private Sub EMI_Master_Meteran_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        Kosong()
    End Sub


    Private Sub Kosong()

        Txt_Kode.Text = ""
        Txt_Keterangan.Text = ""
        Txt_NoMeteran.Text = ""
        Id_meteranUpdate.Text = ""

        Btn_Simpan.Text = "&Simpan" : Btn_Simpan.Tag = "SIMPAN"


        Try
            OpenConn()

            Cmb_JenisBiaya.Items.Clear() : arrKdJenisBiaya.Clear() : arrIdJnsBiaya.Clear()
            SQL = "select Kode_Perusahaan, Id_Jenis_Biaya_Produksi, Kode_Jenis_Biaya_Produksi, keterangan, Satuan "
            SQL = SQL & "from Emi_Jenis_Biaya_Produksi "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read()

                    Cmb_JenisBiaya.Items.Add(Dr("keterangan"))
                    arrKdJenisBiaya.Add(Dr("Kode_Jenis_Biaya_Produksi"))
                    arrIdJnsBiaya.Add(Dr("Id_Jenis_Biaya_Produksi"))

                Loop
            End Using

            Cmb_Satuan.Items.Clear()
            SQL = "select Kode_Perusahaan, Satuan from EMI_Satuan where Kode_Perusahaan = '" & KodePerusahaan & "' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read()
                    Cmb_Satuan.Items.Add(Dr("Satuan"))
                Loop
            End Using

            Lv_Data.Columns.Clear()
            Lv_Data.Columns.Add("id_meteran", 0, HorizontalAlignment.Center)
            Lv_Data.Columns.Add("Kode Meteran", 130, HorizontalAlignment.Left)
            Lv_Data.Columns.Add("Jenis Biaya", 200, HorizontalAlignment.Left)
            Lv_Data.Columns.Add("No Meteran", 200, HorizontalAlignment.Right)
            Lv_Data.Columns.Add("Satuan", 100, HorizontalAlignment.Center)
            Lv_Data.View = View.Details

            Cmb_Filter.Items.Clear() : Cmb_Filter.Text = "" : arrCari.Clear() : Txt_Filter.Text = ""
            Cmb_Filter.Items.Add("Kode Meteran") : arrCari.Add("a.Kode_Meteran")
            Cmb_Filter.Items.Add("No Meteran") : arrCari.Add("a.No_Meteran")
            Cmb_Filter.Items.Add("Jenis Biaya") : arrCari.Add("b.keterangan")


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Load_LV()

    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        Kosong()
    End Sub

    Private Sub Load_LV()
        Try
            OpenConn()

            Lv_Data.Items.Clear()
            SQL = "select a.Id_Meteran, a.Kode_Meteran, a.No_Meteran, b.keterangan as Jenis_Biaya, a.Satuan "
            SQL = SQL & "from EMI_Master_Meteran a, Emi_Jenis_Biaya_Produksi b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.Id_Jenis_Biaya_Produksi = b.Id_Jenis_Biaya_Produksi "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.Status is null "

            If Cmb_Filter.SelectedIndex <> -1 Then
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & arrCari.Item(Cmb_Filter.SelectedIndex) & " like '%" & Trim(Txt_Filter.Text) & "%' "

            End If
            SQL = SQL & "order by a.Id_Jenis_Biaya_Produksi "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Data.Items.Add(Dr("Id_Meteran"))
                    Lv.SubItems.Add(Dr("Kode_Meteran"))
                    Lv.SubItems.Add(Dr("Jenis_Biaya"))
                    Lv.SubItems.Add(Dr("No_Meteran"))
                    Lv.SubItems.Add(Dr("Satuan"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Get_Data_Lv(ByVal index As Integer)
        Lv_IdMeteran = Lv_Data.Items(index).SubItems(item_IdMeteran).Text
        Lv_KdMeteran = Lv_Data.Items(index).SubItems(item_KodeMeteran).Text
        Lv_JnsBiaya = Lv_Data.Items(index).SubItems(item_JenisBiaya).Text
        Lv_NoMeteran = Lv_Data.Items(index).SubItems(item_NoMeteran).Text
        Lv_Satuan = Lv_Data.Items(index).SubItems(item_Satuan).Text
    End Sub

    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click

        If Txt_Kode.Text.Trim.Length = 0 Then
            MessageBox.Show("Kode Tidak Boleh Kosong", JuduLForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_Kode.Focus() : Exit Sub
        ElseIf Txt_Keterangan.Text.Trim.Length = 0 Then
            MessageBox.Show("Keterangan Tidak Boleh Kosong", JuduLForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_Keterangan.Focus() : Exit Sub
        ElseIf Cmb_JenisBiaya.SelectedIndex = -1 Then
            MessageBox.Show("Pilih Jenis Biaya Dahulu", JuduLForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_JenisBiaya.Focus() : Exit Sub
        ElseIf Txt_NoMeteran.Text.Trim.Length = 0 Then
            MessageBox.Show("No Meteran Tidak Boleh Kosong", JuduLForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_NoMeteran.Focus() : Exit Sub
        ElseIf Cmb_Satuan.SelectedIndex = -1 Then
            MessageBox.Show("Pilih Satuan Dahulu", JuduLForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Satuan.Focus() : Exit Sub
        End If



        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            Dim Action As String = ""

            If Btn_Simpan.Tag = "SIMPAN" Then

                SQL = "insert into EMI_Master_Meteran (Kode_Perusahaan, Kode_Meteran, Keterangan, Id_Jenis_Biaya_Produksi, No_Meteran, Satuan) values "
                SQL = SQL & "('" & KodePerusahaan & "', '" & Txt_Kode.Text & "', '" & Txt_Keterangan.Text & "', '" & arrIdJnsBiaya(Cmb_JenisBiaya.SelectedIndex) & "', "
                SQL = SQL & "'" & Txt_NoMeteran.Text & "', '" & Cmb_Satuan.Text & "') "
                ExecuteTrans(SQL)

                Action = "DiSimpan"

            ElseIf Btn_Simpan.Tag = "UPDATE" Then

                If Id_meteranUpdate.Text = "" Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Ada Kesalahaan Saat Update Data, Segera Hubungi Tim IT", JuduLForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If


                SQL = "update EMI_Master_Meteran set Kode_Meteran = '" & Txt_Kode.Text & "', Keterangan = '" & Txt_Keterangan.Text & "', "
                SQL = SQL & "Id_Jenis_Biaya_Produksi = '" & arrIdJnsBiaya(Cmb_JenisBiaya.SelectedIndex) & "', No_Meteran = '" & Txt_NoMeteran.Text & "', Satuan = '" & Cmb_Satuan.Text & "' "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and  Id_Meteran = '" & Id_meteranUpdate.Text & "' "
                ExecuteTrans(SQL)

                Action = "DiUpdate"

            End If


            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
            MessageBox.Show($"Data Berhasil {Action}", JuduLForm, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Kosong()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


    End Sub


    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click

        If Cmb_Filter.SelectedIndex = -1 Then
            MessageBox.Show("Pilih Jenis Filter Dahulu", JuduLForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Filter.Focus() : Exit Sub
        End If

        Load_LV()

    End Sub


    Private Sub Lv_Data_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Data.DoubleClick

        If Lv_Data.Items.Count = 0 Then Exit Sub

        Dim selectedIndex As Integer = Lv_Data.FocusedItem.Index

        Get_Data_Lv(selectedIndex)

        Try
            OpenConn()

            SQL = "select a.Id_Meteran, a.Kode_Meteran, a.keterangan, a.No_Meteran, a.Id_Jenis_Biaya_Produksi, b.keterangan as Jenis_Biaya, a.Satuan "
            SQL = SQL & "from EMI_Master_Meteran a, Emi_Jenis_Biaya_Produksi b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.Id_Jenis_Biaya_Produksi = b.Id_Jenis_Biaya_Produksi "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.Id_Meteran = '" & Lv_IdMeteran & "' "
            SQL = SQL & "and a.Status is null "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    Id_meteranUpdate.Text = Dr("Id_Meteran")
                    Txt_Kode.Text = Dr("Kode_Meteran")
                    Txt_Keterangan.Text = Dr("keterangan")
                    Txt_NoMeteran.Text = Dr("No_Meteran")

                    Cmb_JenisBiaya.SelectedIndex = arrIdJnsBiaya.IndexOf(Dr("Id_Jenis_Biaya_Produksi"))

                    Cmb_Satuan.SelectedItem = Dr("Satuan")


                End If
            End Using

            Btn_Simpan.Text = "&Update"
            Btn_Simpan.Tag = "UPDATE"

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try








    End Sub
















    Private Sub Txt_NoMeteran_Leave(sender As Object, e As EventArgs) Handles Txt_NoMeteran.Leave, Txt_Filter.Leave, Id_meteranUpdate.Leave
        If Txt_NoMeteran.Text.Trim.Length = 0 Then Exit Sub

        If Not IsNumeric(Txt_NoMeteran.Text) Then
            Txt_NoMeteran.Text = ""
        End If

    End Sub

    Private Sub Txt_Kode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Kode.KeyPress
        If e.KeyChar = Chr(13) Then Txt_Keterangan.Focus()
    End Sub

    Private Sub Cmb_JenisBiaya_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_JenisBiaya.KeyPress
        If e.KeyChar = Chr(13) Then Txt_NoMeteran.Focus()
    End Sub

    Private Sub Txt_NoMeteran_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_NoMeteran.KeyPress, Txt_Filter.KeyPress, Id_meteranUpdate.KeyPress
        If e.KeyChar = Chr(13) Then Cmb_Satuan.Focus()
    End Sub
    Private Sub Cmb_Satuan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Satuan.KeyPress, Cmb_Filter.KeyPress
        If e.KeyChar = Chr(13) Then Cmb_JenisBiaya.Focus()
    End Sub
    Private Sub Txt_Keterangan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Keterangan.KeyPress
        If e.KeyChar = Chr(13) Then Btn_Simpan.Focus()
    End Sub



End Class
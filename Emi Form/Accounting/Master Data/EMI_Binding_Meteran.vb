Public Class EMI_Binding_Meteran

    Dim JuduLForm As String = "Binding Meteran"

    Dim arridMeteran As New ArrayList

    Dim Lv_KdWorkCenter, Lv_KetWorkCenter, Lv_IdWorkCenter As String

    Dim item_KodeWorkCenter As Integer = 0
    Dim item_WorkCenter As Integer = 1
    Dim item_IdWorkCenter As Integer = 2



    Private Sub EMI_Binding_Meteran_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub
    Private Sub EMI_Binding_Meteran_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        kosong()
    End Sub


    Private Sub kosong()


        Try
            OpenConn()

            Txt_Kode.Text = ""
            Txt_Keterangan.Text = ""
            id_bindingUpdate.Text = ""

            Btn_Simpan.Text = "&Simpan"
            Btn_Simpan.Tag = "SIMPAN"

            Cmb_Meteran.Items.Clear()
            SQL = "select a.Kode_Perusahaan, a.Id_Meteran, a.Kode_Meteran, a.No_Meteran, a.Satuan, b.keterangan as Jenis_Biaya "
            SQL = SQL & "from EMI_Master_Meteran a, Emi_Jenis_Biaya_Produksi b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.Id_Jenis_Biaya_Produksi = b.Id_Jenis_Biaya_Produksi "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.Status is null "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read()

                    Dim formatDisplay As String = $"{Dr("Jenis_Biaya")} - {Dr("No_Meteran")} "
                    Cmb_Meteran.Items.Add(formatDisplay) : arridMeteran.Add(Dr("Id_Meteran"))

                Loop
            End Using

            Lv_WorkCenter.Columns.Clear()
            Lv_WorkCenter.Columns.Add("Kode Work Center", 150, HorizontalAlignment.Left)
            Lv_WorkCenter.Columns.Add("Work Center", 450, HorizontalAlignment.Left)
            Lv_WorkCenter.Columns.Add("id_WorkCenter", 0, HorizontalAlignment.Left)
            Lv_WorkCenter.View = View.Details


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


        Load_Work_Center()

    End Sub




    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        kosong()
    End Sub

    Private Sub Load_Work_Center()

        Try
            OpenConn()

            Lv_WorkCenter.Items.Clear()
            SQL = "select Kode_Perusahaan, Id_Work_Center, Kode_Work_Center, Keterangan  "
            SQL = SQL & "from EMI_Master_Work_Center "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_WorkCenter.Items.Add(Dr("Kode_Work_Center"))
                    Lv.SubItems.Add(Dr("Keterangan"))
                    Lv.SubItems.Add(Dr("Id_Work_Center"))
                Loop
            End Using




            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub Get_Lv_Data(ByVal index As Integer)

        Lv_KdWorkCenter = Lv_WorkCenter.Items(index).SubItems(item_KodeWorkCenter).Text
        Lv_KetWorkCenter = Lv_WorkCenter.Items(index).SubItems(item_WorkCenter).Text
        Lv_IdWorkCenter = Lv_WorkCenter.Items(index).SubItems(item_IdWorkCenter).Text

    End Sub


    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
        If Txt_Kode.Text.Trim.Length = 0 Then
            MessageBox.Show("Kode Tidak Boleh Kosong", JuduLForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_Kode.Focus() : Exit Sub
        ElseIf Txt_Keterangan.Text.Trim.Length = 0 Then
            MessageBox.Show("Keterangan Tidak Boleh Kosong", JuduLForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_Keterangan.Focus() : Exit Sub
        ElseIf Cmb_Meteran.SelectedIndex = -1 Then
            MessageBox.Show("Pilih Meteran Dahulu", JuduLForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Meteran.Focus() : Exit Sub
        End If

        Dim hasData As Boolean = False

        For i As Integer = 0 To Lv_WorkCenter.Items.Count - 1
            If Lv_WorkCenter.Items(i).Checked Then
                hasData = True
                Exit For
            End If
        Next

        If Not hasData Then
            MessageBox.Show("Pilih Dahulu Work Center", JuduLForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Lv_WorkCenter.Focus() : Exit Sub
        End If



        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction


            '========================
            '=     INSERT INDUK     =
            '========================
            SQL = "insert into EMI_Binding_Meteran (Kode_Perusahaan, Id_Meteran, Kode_Binding, Keterangan) values "
            SQL = SQL & "('" & KodePerusahaan & "', '" & arridMeteran(Cmb_Meteran.SelectedIndex) & "', '" & Txt_Kode.Text & "', '" & Txt_Keterangan.Text & "')"
            ExecuteTrans(SQL)


            Dim hasWorkCenter As Boolean = False


            For i As Integer = 0 To Lv_WorkCenter.Items.Count - 1

                If Not Lv_WorkCenter.Items(i).Checked Then
                    Continue For
                End If

                Get_Lv_Data(i)

                Dim id_Binding_Current As String = ""
                SQL = "select IDENT_CURRENT('EMI_Binding_Meteran') as id_binding"
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        id_Binding_Current = Dr("id_binding")
                    End If
                End Using


                SQL = "insert into EMI_Binding_Meteran_Detail (Kode_Perusahaan, Id_Binding, Kode_Binding, Id_Work_Center) values "
                SQL = SQL & "('" & KodePerusahaan & "', '" & id_Binding_Current & "', '" & Txt_Kode.Text & "', '" & Lv_IdWorkCenter & "') "
                ExecuteTrans(SQL)

            Next


            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
            MessageBox.Show("Data Berhasil Disimpan", JuduLForm, MessageBoxButtons.OK, MessageBoxIcon.Information)
            kosong()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


    End Sub




End Class
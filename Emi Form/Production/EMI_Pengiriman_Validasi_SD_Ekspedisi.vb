Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button

Public Class EMI_Pengiriman_Validasi_SD_Ekspedisi

    Dim arrIndex As New ArrayList
    Dim no_formula, no_inquiry, kode_customer, kode_barang As String

    Private Sub DateTimePicker2_ValueChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If ComboBox3.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Master_CekPengiriman_Err_EkspedisiHrsDipilih, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If


        EMI_Pengiriman_Validasi.ListView1T.FocusedItem.SubItems(3).Text = ComboBox3.Text
        EMI_Pengiriman_Validasi.ListView1T.FocusedItem.SubItems(5).Text = arrIndex.Item(ComboBox3.SelectedIndex)
        Me.Close()
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub



    Private Sub SD_Formulator_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")


        Try
            OpenConn()
            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")

            Base_Language.Get_Languages(Bahasa_Pilihan, "Master_Cek_Pengiriman")
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


        Label1.Text = Base_Language.Lang_Master_CekPengiriman_Judul_Sd1
        Label2.Text = Base_Language.Lang_Global_Gudang
        Label3.Text = Base_Language.Lang_Global_Ekspedisi
        Button1.Text = Base_Language.Lang_Global_Simpan

        kosong()
    End Sub


    Private Sub kosong()


        Try
            OpenConn()
            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            Base_Language.Get_Languages(Bahasa_Pilihan, "QC_Formula")

            ComboBox3.Items.Clear() : arrIndex.Clear()
            SQL = "select id_ekspedisi,kode_ekspedisi,nama_ekspedisi from EMI_Master_Ekspedisi where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "order by nama_ekspedisi"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    ComboBox3.Items.Add(Dr("nama_ekspedisi")) : arrIndex.Add(Dr("id_ekspedisi"))
                Loop
            End Using

            Dim id_ekspedisi As Integer = -1


            SQL = "select isnull(id_ekspedisi,-1) as id_ekspedisi from Emi_Customer_Gudang where  "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and  urut_oto = '" & EMI_Pengiriman_Validasi.ListView1T.FocusedItem.SubItems(4).Text & "'  "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    id_ekspedisi = Dr("id_ekspedisi")
                Else
                    Dr.Close()
                    MessageBox.Show(Base_Language.Lang_Master_CekPengiriman_Err_Id_Ekspedisi_Null, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using


            If id_ekspedisi = -1 Then
                ComboBox3.SelectedIndex = -1
            Else
                ComboBox3.SelectedIndex = arrIndex.IndexOf(id_ekspedisi)
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub








End Class
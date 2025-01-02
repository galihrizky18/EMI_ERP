Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button


Public Class EMI_Pengiriman_Validasi_Display
    Dim arrcariT, arrIdT As New ArrayList
    Dim JenisT = "Master_Jenis_Hewan"
    Public Sub cari(ByVal semua As String)

        Try
            OpenConn()

            ListView1T.Items.Clear()
            SQL = "select a.No_Faktur,a.Kode_Customer,b.Nama,a.Tanggal,a.Kode_Karyawan from Emi_PO a, Customers  b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Customer = b.Kode_Customer "
            SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' and a.status is null and a.flag_pengiriman is null "

            If semua = "T" Then
                If TextBox3.Text.Trim.Length <> 0 Then
                    SQL = SQL & "and  " & arrcariT.Item(ComboBox1.SelectedIndex) & " like '%" & TextBox3.Text & "%' "
                    SQL = SQL & "order by no_faktur "
                End If
            Else
                SQL = SQL & "order by no_faktur"
            End If


            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lvw As ListViewItem

                    lvw = ListView1T.Items.Add(Dr("no_faktur"))
                    lvw.SubItems.Add(Dr("kode_customer"))
                    lvw.SubItems.Add(Dr("nama"))
                    lvw.SubItems.Add(Format(Dr("tanggal"), "dd-MMM-yyyy"))
                Loop
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


    End Sub


    Private Sub Master_Jenis_Hewan_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        cari("T")
    End Sub

    Private Sub SetEkspedisiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SetEkspedisiToolStripMenuItem.Click
        EMI_Pengiriman_Validasi.ShowDialog()
    End Sub

    Private Sub TextBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox3.KeyPress
        If e.KeyChar = Chr(13) Then
            btnCari_Click(Me, Nothing)
        End If
    End Sub

    Private Sub Master_Jenis_Hewan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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







        ListView1T.Columns.Add(Base_Language.Lang_Global_No_PO, 150, HorizontalAlignment.Left)
        ListView1T.Columns.Add(Base_Language.Lang_Global_KodeCustomer, 150, HorizontalAlignment.Left)
        ListView1T.Columns.Add(Base_Language.Lang_Global_NamaCustomer, 250, HorizontalAlignment.Left)
        ListView1T.Columns.Add(Base_Language.Lang_Global_Tanggal, 120, HorizontalAlignment.Left)


        ComboBox1.Items.Clear() : arrcariT.Clear()
        ComboBox1.Items.Add(Base_Language.Lang_Global_No_PO) : arrcariT.Add("a.no_faktur")
        ComboBox1.Items.Add(Base_Language.Lang_Global_KodeCustomer) : arrcariT.Add("a.Kode_Customer")
        ComboBox1.Items.Add(Base_Language.Lang_Global_NamaCustomer) : arrcariT.Add("b.nama")
        ComboBox1.Items.Add(Base_Language.Lang_Global_Tanggal) : arrcariT.Add("a.tanggal")

        Label1.Text = Base_Language.Lang_Master_CekPengiriman_Judul
        btnCari.Text = Base_Language.Lang_Global_Cari
        Label4.Text = Base_Language.Lang_Global_Kolom
        SetEkspedisiToolStripMenuItem.Text = Base_Language.Lang_Master_CekPengiriman_Judul_Sd




        cari("Y")




    End Sub





















End Class
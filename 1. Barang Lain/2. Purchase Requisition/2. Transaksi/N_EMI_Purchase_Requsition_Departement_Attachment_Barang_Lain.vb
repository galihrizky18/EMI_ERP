Imports System.IO

Public Class N_EMI_Purchase_Requsition_Departement_Attachment_Barang_Lain


    Dim arrCari As New ArrayList

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        kosong()
    End Sub




    Private Sub Popup_Timbang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        Try
            OpenConn()

            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)

            Btn_Refresh.Text = Base_Language.Lang_Global_Refresh
            Label1.Text = "Attachment - Purchase Requisition Departement Barang Lain"


            cmbKolom.Items.Clear()
            cmbKolom.Items.Add("No Faktur") : arrCari.Add("a.no_faktur")


            Lv_List_Barang.Columns.Clear()

            Lv_List_Barang.Columns.Add("Lokasi", 140, HorizontalAlignment.Left)
            Lv_List_Barang.Columns.Add("No PR Dept", 100, HorizontalAlignment.Left).DisplayIndex = 0 '0

            Lv_List_Barang.Columns.Add("Keterangan", 400, HorizontalAlignment.Left) '1

            Lv_List_Barang.Columns.Add("Tanggal", 110, HorizontalAlignment.Center) '1
            Lv_List_Barang.Columns.Add("jam", 110, HorizontalAlignment.Center) '1
            Lv_List_Barang.Columns.Add("UserId", 120, HorizontalAlignment.Center) '1

            Lv_List_Barang.View = View.Details


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        kosong()

    End Sub


    Public Sub kosong()
        Sync_Data_PR_Pra_Release()

        cmbKolom.SelectedIndex = 0

        get_transfer_stock("T")
    End Sub

    Private Sub get_transfer_stock(ByVal cari As String)
        Try
            OpenConn()

            Lv_List_Barang.Items.Clear()


            ' --- Ambil daftar gudang ---
            Dim listGudang As New List(Of String)

            SQL = "select Kode_Kategori_Gudang From N_EMI_View_Master_Kategori_Gudang_Binding_Departement_Barang_Lain where User_ID = '" & UserID & "'"

            SQL = SQL & "group by kode_kategori_gudang "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    listGudang.Add("'" & Dr("Kode_Kategori_Gudang").ToString() & "'")
                Loop
            End Using

            ' Jika kosong, kasih nilai palsu biar IN() tidak error
            If listGudang.Count = 0 Then
                listGudang.Add("'0'")
            End If

            ' Gabungkan hasil jadi 1 string
            Dim inGudang As String = String.Join(",", listGudang)


            SQL = "select isnull(a.Kode_Kategori_Gudang, 'NONE') as Kode_Kategori_Gudang, a.No_Faktur,a.Tanggal,a.Jam, a.UserId, isnull(a.keterangan, '-') as Keterangan "
            SQL = SQL & "from N_emi_purchase_requisition_barang_lain_departement  a "
            SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Status is null "
            SQL = SQL & "and a.Flag_Release is null and a.flag_pra_release = 'Y' "
            SQL = SQL & "and a.kode_kategori_gudang in (" & inGudang & ") "

            If cari = "Y" Then
                SQL = SQL & "and " & arrCari.Item(cmbKolom.SelectedIndex) & "  like  '%" & txtValue.Text.Trim & "%' "
            End If


            SQL = SQL & "order by tanggal desc ,jam desc "

            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim Lvw As ListViewItem

                    Lvw = Lv_List_Barang.Items.Add(dr("Kode_Kategori_Gudang"))

                    Lvw.SubItems.Add(dr("No_Faktur"))
                    Lvw.SubItems.Add(dr("Keterangan"))
                    Lvw.SubItems.Add(Format(dr("Tanggal"), "dd MMM yyyy"))
                    Lvw.SubItems.Add(dr("jam"))
                    Lvw.SubItems.Add(dr("UserId"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub Lv_List_Barang_DoubleClick(sender As Object, e As EventArgs) Handles Lv_List_Barang.DoubleClick

        Try
            OpenConn()

            If CekButtonRole("attachment_pr_dept_barang_lain") = "T" Then
                CloseConn()
                MessageBox.Show("Anda tidak memiliki akses untuk Attachment PR Dept!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        If Lv_List_Barang.Items.Count = 0 Then Exit Sub

        N_EMI_SD_Upload_Faktur_PR_Dept_Barang_Lain.nopr = Lv_List_Barang.FocusedItem.SubItems(1).Text
        N_EMI_SD_Upload_Faktur_PR_Dept_Barang_Lain.ShowDialog()


    End Sub

    Private Sub BtnBarangMasuk_Cari_Click(sender As Object, e As EventArgs) Handles BtnBarangMasuk_Cari.Click
        get_transfer_stock("Y")
    End Sub
End Class
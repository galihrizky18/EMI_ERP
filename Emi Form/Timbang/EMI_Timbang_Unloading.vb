Imports System.Deployment.Internal
Imports System.IO.Ports
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button
Imports System.Windows.Markup
Imports Azure.Storage.Blobs
Imports Azure.Storage.Blobs.Models
Imports CrystalDecisions.CrystalReports.Engine
Imports Microsoft.VisualBasic.ApplicationServices
Imports WebEye.Controls.WinForms.StreamPlayerControl

Public Class EMI_Timbang_Unloading
    Dim arrcari As New ArrayList
    Dim Jenis = "Transaksi_Timbang_Kosong"
    Public Txt_Ekspedisi As String = ""

    Dim arrIdJenisMuatan, arrMetodeTruckScale As New ArrayList
    Dim arrNamaBarang, arrKodeBarang As New ArrayList
    Dim No_Faktur As String = ""

    Dim LvNoPO As String
    Dim LvKdBarang As String
    Dim LvNama As String
    Dim LvTglExp As String
    Dim LvTglProd As String
    Dim LvUrutPO As String
    Dim LvSatuan As String
    Dim LvJumlah As String
    Dim LvJumlahMasuk As String
    Dim LvUrutLoading As String

    Dim ItemNoPO As Integer = 0
    Dim ItemKdBarang As Integer = 1
    Dim ItemNama As Integer = 2
    Dim ItemTglExp As Integer = 3
    Dim ItemTglProd As Integer = 4
    Dim ItemUrutPO As Integer = 5
    Dim ItemSatuan As Integer = 6
    Dim ItemJumlah As Integer = 7
    Dim ItemJumlahMasuk As Integer = 8
    Dim ItemUrutLoading As Integer = 9

    Dim LvTimbangKdBarang As String
    Dim LvTimbangNmBarang As String
    Dim LvTimbangSatuan As String
    Dim LvTimbangJmlBags As String
    Dim LvTimbangBeratBags As String
    Dim LvTimbangJmlPallet As String
    Dim LvTimbangjmlBarang As String
    Dim LvTimbangBeratBarang As String

    Dim ItemTimbangKdBarang As Integer = 0
    Dim ItemTimbangNmBarang As Integer = 1
    Dim ItemTimbangSatuan As Integer = 2
    Dim ItemTimbangJmlBags As Integer = 3
    Dim ItemTimbangBeratBags As Integer = 4
    Dim ItemTimbangJmlPallet As Integer = 5
    Dim ItemTimbangJmlBarang As Integer = 6
    Dim ItemTimbangBeratBarang As Integer = 7
    Public jenisMasuk As String = ""
    Public filterDetailBarang As String = ""

    Private Function CekNothing(ByVal str As String) As String
        Dim hasil As String = ""

        If str Is Nothing OrElse str = "" Then
            hasil = "0"
        Else
            hasil = str
        End If

        Return hasil
    End Function



    Private Sub Get_Isi_DataGridViewTimbang(ByVal NoIndex As Integer)
        LvTimbangKdBarang = DgvTimbang.Rows(NoIndex).Cells(ItemTimbangKdBarang).Value
        LvTimbangNmBarang = DgvTimbang.Rows(NoIndex).Cells(ItemTimbangNmBarang).Value
        LvTimbangSatuan = DgvTimbang.Rows(NoIndex).Cells(ItemTimbangSatuan).Value
        LvTimbangJmlBags = DgvTimbang.Rows(NoIndex).Cells(ItemTimbangJmlBags).Value
        LvTimbangBeratBags = DgvTimbang.Rows(NoIndex).Cells(ItemTimbangBeratBags).Value
        LvTimbangJmlPallet = DgvTimbang.Rows(NoIndex).Cells(ItemTimbangJmlPallet).Value
        LvTimbangjmlBarang = DgvTimbang.Rows(NoIndex).Cells(ItemTimbangJmlBarang).Value
        LvTimbangBeratBarang = DgvTimbang.Rows(NoIndex).Cells(ItemTimbangBeratBarang).Value
    End Sub

    Private Sub Get_Isi_DataGridView(ByVal NoIndex As Integer)
        LvNoPO = DgvPO.Rows(NoIndex).Cells(ItemNoPO).Value
        LvKdBarang = DgvPO.Rows(NoIndex).Cells(ItemKdBarang).Value
        LvNama = DgvPO.Rows(NoIndex).Cells(ItemNama).Value
        LvTglExp = DgvPO.Rows(NoIndex).Cells(ItemTglExp).Value
        LvTglProd = CekNothing(DgvPO.Rows(NoIndex).Cells(ItemTglProd).Value)
        LvUrutPO = DgvPO.Rows(NoIndex).Cells(ItemUrutPO).Value
        LvSatuan = DgvPO.Rows(NoIndex).Cells(ItemSatuan).Value
        LvJumlah = DgvPO.Rows(NoIndex).Cells(ItemJumlah).Value
        LvJumlahMasuk = DgvPO.Rows(NoIndex).Cells(ItemJumlahMasuk).Value
        LvUrutLoading = DgvPO.Rows(NoIndex).Cells(ItemUrutLoading).Value
    End Sub

    Private Sub Get_Data_Timbangan()
        Try
            Dim sp = New SerialPort(My.Settings.Port_Timbangan, 9600, Parity.None, 8, StopBits.One)
            If Not (sp Is Nothing) Then
                sp.Open()
                sp.ReadLine()

                sp.Close()
                sp.Dispose()
                sp = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub get_no_faktur()
        Txt_NoFaktur.Text = fTransTimbanganKosong & Format(tgl_skg, "MMyy") & "-" &
                             General_Class.Get_Last_Number2("EMI_Timbang_Unloading", "No_Faktur", 5,
                             "Kode_perusahaan", KodePerusahaan,
                             "And", "substring(no_Faktur, 1, " & Len(fTransTimbanganKosong) + 4 & ")", fTransTimbanganKosong & Format(tgl_skg, "MMyy"))
    End Sub

    Private Sub Tampil_Kamera()
        StreamPlayerControl1.Show()
        StreamPlayerControl2.Show()

        Try

            'If StreamPlayerControl1.IsPlaying = True Then
            '    StreamPlayerControl1.Stop()
            '    StreamPlayerControl2.Stop()
            'End If

            'SQL = "select User_IPCAM, Password_IPCAM, IPPORT_CAM from Emi_CAM"
            'Using dr = OpenTrans(SQL)
            '    Dim stream As Integer = 1
            '    Do While dr.Read
            '        Dim controlName As String = "StreamPlayerControl" & stream
            '        Dim control As Object = Me.GetType().GetField(controlName, Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(Me)

            '        If control IsNot Nothing Then
            '            control.StartPlay((New Uri("rtsp://" & dr("User_IPCAM") & ":" & dr("Password_IPCAM") & "@" & dr("IPPORT_CAM") & "/Streaming/channels/102/")))
            '            stream += 1
            '        End If

            '    Loop
            'End Using

            Dim user1 As String = "" : Dim pass1 As String = "" : Dim ipaddr1 As String = ""
            Dim user2 As String = "" : Dim pass2 As String = "" : Dim ipaddr2 As String = ""

            Try
                OpenConn()
                SQL = "select UserName, Password, IP_Address, CAM_Number from Emi_CAM"
                Using dr = OpenTrans(SQL)
                    Do While dr.Read
                        If dr("CAM_Number") = "CAM 1" Then
                            user1 = dr("UserName")
                            pass1 = dr("Password")
                            ipaddr1 = dr("IP_Address")

                        ElseIf dr("CAM_Number") = "CAM 2" Then
                            user2 = dr("UserName")
                            pass2 = dr("Password")
                            ipaddr2 = dr("IP_Address")
                        End If
                    Loop
                End Using

                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
            End Try

            StreamPlayerControl1.StartPlay((New Uri("rtsp://" & user1 & ":" & pass1 & "@" & ipaddr1 & "/Streaming/channels/102/")))
            StreamPlayerControl2.StartPlay((New Uri("rtsp://" & user2 & ":" & pass2 & "@" & ipaddr2 & "/Streaming/channels/102/")))

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Transaksi_Timbang_Unloading_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub Transaksi_Timbang_Unloading_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")


        Try
            OpenConn()

            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)

            DgvPO.Columns(ItemUrutPO).Visible = False
            DgvPO.Columns(ItemUrutLoading).Visible = False

            If jenisMasuk = "MASUK" Then
                Lbl_Judul.Text = "Transaksi - Timbang 1 " 'Base_Language.Lang_TransUnloading_Judul + " | " + Base_Language.Lang_Global_Bruto
                DgvPO.Columns(ItemJumlahMasuk).Visible = False
                DgvPO.Columns(ItemJumlahMasuk).ReadOnly = True
                DgvPO.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

                Txt_Timbang1.Text = Txt_Timbangan.Text
                Txt_Timbang2.Enabled = False

                DTP_Bruto.Value = DateTime.Now
                DTP_Tara.Value = DateTime.Now

            ElseIf jenisMasuk = "KELUAR" Then
                Lbl_Judul.Text = "Transaksi - Timbang 2 " 'Base_Language.Lang_TransUnloading_Judul + " | " + Base_Language.Lang_Global_Tara
                DgvPO.Columns(ItemJumlahMasuk).Visible = True
                DgvPO.Columns(ItemJumlahMasuk).ReadOnly = False
                DgvPO.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

                Txt_Timbang2.Text = Txt_Timbangan.Text
                Txt_Timbang1.Enabled = False


                SQL = "select No_Faktur from EMI_Timbang_Unloading a where "
                SQL = SQL & "kode_Perusahaan='" & KodePerusahaan & "' and no_loading='" & TxtNo_Loading.Text & "' "
                SQL = SQL & "and status is null and flag_selesai is null  "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Txt_NoFaktur.Text = Dr("No_Faktur")
                    End If
                End Using


                DTP_Tara.Value = DateTime.Now
            Else
                MessageBox.Show("Terjadi Kesalahan  . .  !")
                Exit Sub
            End If

            loadJenisMuatan()
            Btn_Simpan.Text = Base_Language.Lang_Global_Simpan
            Btn_Refresh.Text = Base_Language.Lang_Global_Refresh
            Lbl_Supplier.Text = Base_Language.Lang_Global_Supplier
            Lbl_Supir.Text = Base_Language.Lang_Global_Supir
            Lbl_PlatNomor.Text = Base_Language.Lang_Global_PlatNomor
            Lbl_Timbang1.Text = "Timbang 1"
            Lbl_Timbang2.Text = "Timbang 2"
            Lbl_FotoKendaraan.Text = Base_Language.Lang_Global_FotoKendaraan

            ListView2.Columns.Clear()
            ListView2.Columns.Add("No SJ", 160, HorizontalAlignment.Left)
            ListView2.Columns.Add("No PO", 160, HorizontalAlignment.Left)
            ListView2.View = View.Details



            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub

        End Try
        If jenisMasuk = "MASUK" Then
            Get_DGVMasuk()
        ElseIf jenisMasuk = "KELUAR" Then
            Get_DGVKeluar()
        End If

        'kosong()
        Tampil_Kamera()
    End Sub

    Private Sub loadJenisMuatan()
        Try
            OpenConn()

            SQL = "select Id_Jenis_Muatan, Kode_Jenis_Muatan, Keterangan, Metode_Timbang "
            SQL = SQL & "from EMI_Master_Jenis_Muatan"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read

                    CmbJenisMuatan.Items.Add(Dr("Keterangan")) : arrIdJenisMuatan.Add(Dr("Id_Jenis_Muatan")) : arrMetodeTruckScale.Add(Dr("Metode_Timbang"))

                Loop
            End Using

            If jenisMasuk = "KELUAR" Then
                Dim idmuatan As String = ""

                SQL = "select timbang_masuk, id_jenis_muatan, tgl_timbang_masuk, Jam_Timbang_Masuk from EMI_Timbang_Unloading where "
                SQL = SQL & "no_faktur='" & Txt_NoFaktur.Text & "'"
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        idmuatan = Dr("id_jenis_muatan")
                        Txt_Timbang1.Text = Format(Dr("timbang_masuk"), "N2")
                        If General_Class.CekNULL(Dr("tgl_timbang_masuk")) = "" Then
                            DTP_Bruto.Value = DateTime.Now
                        Else
                            DTP_Bruto.Value = Convert.ToDateTime(Dr("tgl_timbang_masuk")).Date.Add(Convert.ToDateTime(Dr("Jam_Timbang_Masuk")).TimeOfDay)
                        End If
                    End If
                End Using

                For index = 0 To arrIdJenisMuatan.Count - 1
                    If arrIdJenisMuatan.Item(index) = idmuatan Then
                        CmbJenisMuatan.SelectedIndex = index
                        Exit For
                    End If
                Next

                CmbJenisMuatan.Enabled = False
                CmbBarang.Enabled = False

                Hitung_Netto()

            End If


            If jenisMasuk = "MASUK" Then


                '===================================
                '=     CEK APAKAH TIMBANG KE 2     =
                '===================================
                SQL = "select top 1 a.ID_Jenis_Muatan, c.Keterangan "
                SQL = SQL & "from EMI_Timbang_Unloading a, EMI_Pembelian_Loading b, EMI_Master_Jenis_Muatan c "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Perusahaan = c.Kode_Perusahaan "
                SQL = SQL & "and a.No_Loading = b.No_Faktur "
                SQL = SQL & "and a.ID_Jenis_Muatan = c.Id_Jenis_Muatan "
                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and a.No_Loading = '" & TxtNo_Loading.Text & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        CmbJenisMuatan.Enabled = False
                        CmbJenisMuatan.SelectedItem = Dr("Keterangan")
                    Else
                        Dr.Close()
                    End If
                End Using
            End If


            Dim nama_barang As String = ""
            Dim id As Integer = 0
            SQL = "select distinct a.kode_supplier, D.nama, b.Kode_Barang, C.nama as Nama_Barang, a.Lokasi, a.No_SJ, a.No_Plat, a.Driver "
            SQL = SQL & "from EMI_Pembelian_Loading a, EMI_Pembelian_Loading_Detail b, barang c, Suppliers d "
            SQL = SQL & "where a.kode_Perusahaan=b.kode_Perusahaan and a.no_faktur=b.no_faktur and a.status is null and "
            SQL = SQL & "b.kode_Barang=c.kode_Barang and b.kode_stock_Owner=c.Kode_Stock_Owner and b.kode_Perusahaan=c.kode_Perusahaan "
            SQL = SQL & "and a.kode_Perusahaan=d.Kode_Perusahaan and a.kode_Supplier=d.Kode_Supplier "
            SQL = SQL & "and a.kode_Perusahaan ='" & KodePerusahaan & "' and a.No_faktur='" & TxtNo_Loading.Text & "' "
            If jenisMasuk = "MASUK" Then
                SQL = SQL & "and b.Flag_Timbang_Masuk is null "
            ElseIf jenisMasuk = "KELUAR" Then
                SQL = SQL & "and b.Flag_Timbang_Keluar is null "
            End If
            SQL = SQL & "Order By d.nama "
            Using dr = OpenTrans(SQL)
                Do While dr.Read

                    If id = 0 Then
                        TxtNoSJ.Text = dr("No_SJ")
                        Txt_PlatNomor.Text = dr("No_Plat")
                        Txt_Supir.Text = dr("Driver")
                        Txt_Supplier.Text = dr("nama")
                        Lbl_KodeSupplier.Text = dr("kode_supplier")
                    End If


                    arrNamaBarang.Add(dr("Nama_Barang")) : arrKodeBarang.Add(dr("Kode_Barang"))
                    CmbBarang.Items.Add(dr("Nama_Barang"))
                    id += 1
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Public Sub kosong()

        ListView2.Items.Clear()
        TxtNoSJ.Text = ""
        Txt_Timbangan.Text = "99999"
        Txt_NoFaktur.Text = ""
        Txt_Supplier.Text = ""
        Txt_Supir.Text = ""
        Txt_Ekspedisi = ""
        Txt_Supir.Text = ""
        Txt_PlatNomor.Text = ""
        Txt_Timbang1.Text = ""
        Txt_Timbang2.Text = ""
        Txt_Netto.Text = ""
        Txt_Timbang1.Enabled = True
        Txt_Timbang2.Enabled = True
        Txt_Netto.Enabled = False
        CmbBarang.Text = ""
        CmbJenisMuatan.Text = ""


        Btn_Simpan.Tag = "&SimpanBruto"
        Btn_Simpan.Text = "&Simpan Bruto"

        Try
            OpenConn()

            CmbSatuan.Items.Clear()
            SQL = "select satuan from emi_satuan where kode_Perusahaan='" & KodePerusahaan & "' "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    CmbSatuan.Items.Add(dr("satuan"))
                Loop
            End Using

            Dim satuan_timbang As String = ""
            SQL = "select Satuan_Timbang from init where kode_Perusahaan='" & KodePerusahaan & "' "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    satuan_timbang = dr("Satuan_Timbang")
                End If
            End Using

            CmbSatuan.Text = satuan_timbang
            CmbSatuan.Enabled = False
            get_no_faktur()
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        '''Tampil_Kamera()
        'Popup_Timbang.Show()
    End Sub

    'Data PO berdasarkan Supplier

    Public Sub Get_DGVMasuk()

        Try
            OpenConn()

            ListView2.Items.Clear()
            ListView2.View = View.Details

            DgvPO.Rows.Clear()

            Dim id As Integer = 0
            SQL = "select a.kode_Perusahaan, a.no_faktur, a.kode_supplier, C.nama as Nama_Barang, a.Lokasi, a.No_SJ, a.No_Plat, "
            SQL = SQL & "a.Driver, b.No_Po, B.Urut_PO, B.Kode_Stock_Owner, b.Kode_Barang, D.nama, b.tanggal_Produksi, b.Tanggal_Expired, "
            SQL = SQL & "b.Jumlah, b.Satuan ,b.urut_Oto as Urut_Loading, b.satuan from "
            SQL = SQL & "EMI_Pembelian_Loading a, EMI_Pembelian_Loading_Detail b, barang c, Suppliers d "
            SQL = SQL & "where a.kode_Perusahaan=b.kode_Perusahaan and a.no_faktur=b.no_faktur and a.status is null and "
            SQL = SQL & "b.kode_Barang=c.kode_Barang and b.kode_stock_Owner=c.Kode_Stock_Owner and b.kode_Perusahaan=c.kode_Perusahaan "
            SQL = SQL & "and a.kode_Perusahaan=d.Kode_Perusahaan and a.kode_Supplier=d.Kode_Supplier "
            SQL = SQL & "and a.kode_Perusahaan ='" & KodePerusahaan & "' and b.Flag_Timbang_Masuk is null and a.No_faktur='" & TxtNo_Loading.Text & "' "
            If CmbBarang.SelectedIndex <> -1 Then
                SQL = SQL & "and b.kode_Barang='" & arrKodeBarang.Item(CmbBarang.SelectedIndex) & "'"
            End If
            SQL = SQL & "Order By d.nama "
            Using dr = OpenTrans(SQL)

                Do While dr.Read

                    DgvPO.Rows.Add(1)
                    DgvPO.Rows(id).Cells(ItemNoPO).Value = dr("No_Po")
                    DgvPO.Rows(id).Cells(ItemKdBarang).Value = dr("Kode_Barang")
                    DgvPO.Rows(id).Cells(ItemNama).Value = dr("Nama_Barang")
                    DgvPO.Rows(id).Cells(ItemTglExp).Value = Format(dr("Tanggal_Expired"), "dd MMM yyyy")
                    DgvPO.Rows(id).Cells(ItemTglProd).Value = Format(dr("tanggal_Produksi"), "dd MMM yyyy")
                    DgvPO.Rows(id).Cells(ItemUrutPO).Value = dr("Urut_PO")
                    DgvPO.Rows(id).Cells(ItemSatuan).Value = dr("Satuan")
                    DgvPO.Rows(id).Cells(ItemJumlah).Value = Format(dr("Jumlah"), "N2")
                    DgvPO.Rows(id).Cells(ItemJumlahMasuk).Value = 0
                    DgvPO.Rows(id).Cells(ItemUrutLoading).Value = dr("Urut_Loading")



                    id += 1
                Loop
            End Using

            DgvTimbang.Rows.Clear()

            SQL = "select a.Kode_Perusahaan, a.no_faktur, b.Kode_Barang, c.nama, b.Satuan, b.Satuan_Per_Bag, "
            SQL = SQL & "b.Jumlah_Per_Bag,c.Berat_Bags, c.Satuan_Berat_Bags "

            SQL = SQL & ",isnull((select sum(Jumlah_Bags) from emi_barang_masuk_perpallet x where "
            SQL = SQL & "x.Kode_Perusahaan=a.Kode_Perusahaan and x.No_Pembelian_Loading=a.No_Faktur and x.status is null "
            SQL = SQL & "and x.Kode_Barang=b.Kode_Barang),0) as Jumlah_Bags "

            SQL = SQL & ",isnull((select sum(Jumlah) from emi_barang_masuk_perpallet x where "
            SQL = SQL & "x.Kode_Perusahaan=a.Kode_Perusahaan and x.No_Pembelian_Loading=a.No_Faktur and x.status is null "
            SQL = SQL & "and x.Kode_Barang=b.Kode_Barang),0) as Jumlah_Barang "

            SQL = SQL & ",isnull((select sum(Jumlah_Pallet) from emi_barang_masuk_perpallet x where "
            SQL = SQL & "x.Kode_Perusahaan=a.Kode_Perusahaan and x.No_Pembelian_Loading=a.No_Faktur and x.status is null "
            SQL = SQL & "and x.Kode_Barang=b.Kode_Barang),0) as Jumlah_Pallet "

            SQL = SQL & "from EMI_Pembelian_Loading a, EMI_Pembelian_Loading_Detail b, barang c where "
            SQL = SQL & "a.No_Faktur=b.No_Faktur and a.Kode_Perusahaan=b.Kode_Perusahaan and a.status is null "
            SQL = SQL & "and b.Kode_Perusahaan=c.Kode_Perusahaan and b.Kode_Barang=c.Kode_Barang and b.Kode_Stock_Owner=c.Kode_Stock_Owner "
            SQL = SQL & "and a.kode_Perusahaan ='" & KodePerusahaan & "' and b.Flag_Timbang_Masuk is null and a.No_faktur='" & TxtNo_Loading.Text & "' "

            If CmbBarang.SelectedIndex <> -1 Then
                SQL = SQL & "and b.kode_Barang='" & arrKodeBarang.Item(CmbBarang.SelectedIndex) & "'"
            End If

            SQL = SQL & "group by a.Kode_Perusahaan, a.no_faktur,b.Kode_Barang, c.nama, b.Satuan, b.Satuan_Per_Bag, "
            SQL = SQL & "b.Jumlah_Per_Bag,c.Berat_Bags, c.Satuan_Berat_Bags "

            SQL = SQL & "Order By c.nama "
            Using ds = BindingTrans(SQL)
                With ds.Tables("MyTable")
                    For id2 = 0 To .Rows.Count - 1

                        Dim berat_bags As Double
                        SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & .Rows(id2).Item("Kode_Barang") & "',"
                        SQL = SQL & "'" & .Rows(id2).Item("Satuan_Berat_Bags") & "','" & CmbSatuan.Text & "',"
                        SQL = SQL & "" & .Rows(id2).Item("Jumlah_Bags") * .Rows(id2).Item("Berat_Bags") & ") as Hasil "
                        Using dr3 = OpenTrans(SQL)
                            If dr3.Read Then
                                If General_Class.CekNULL(dr3("Hasil")) <> "" Then
                                    berat_bags = dr3("Hasil")
                                Else
                                    MessageBox.Show("Satuan " & .Rows(id2).Item("Satuan_Berat_Bags") & " Ke " & CmbSatuan.Text & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End If
                        End Using

                        Dim berat_barang As Double
                        SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & .Rows(id2).Item("Kode_Barang") & "',"
                        SQL = SQL & "'" & .Rows(id2).Item("Satuan") & "','" & CmbSatuan.Text & "',"
                        SQL = SQL & "" & .Rows(id2).Item("Jumlah_Barang") & ") as Hasil "
                        Using dr3 = OpenTrans(SQL)
                            If dr3.Read Then
                                If General_Class.CekNULL(dr3("Hasil")) <> "" Then
                                    berat_barang = dr3("Hasil")
                                Else
                                    MessageBox.Show("Satuan " & .Rows(id2).Item("Satuan_Berat_Bags") & " Ke " & CmbSatuan.Text & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End If
                        End Using

                        DgvTimbang.Rows.Add(1)
                        DgvTimbang.Rows(id2).Cells(ItemTimbangKdBarang).Value = .Rows(id2).Item("Kode_Barang")
                        DgvTimbang.Rows(id2).Cells(ItemTimbangNmBarang).Value = .Rows(id2).Item("Nama")
                        DgvTimbang.Rows(id2).Cells(ItemTimbangSatuan).Value = .Rows(id2).Item("Satuan")
                        DgvTimbang.Rows(id2).Cells(ItemTimbangJmlBags).Value = Format(.Rows(id2).Item("Jumlah_Bags"), "N0")
                        DgvTimbang.Rows(id2).Cells(ItemTimbangBeratBags).Value = Format(berat_bags, "N2")
                        DgvTimbang.Rows(id2).Cells(ItemTimbangJmlPallet).Value = Format(.Rows(id2).Item("Jumlah_Pallet"), "N0")
                        DgvTimbang.Rows(id2).Cells(ItemTimbangJmlBarang).Value = Format(.Rows(id2).Item("Jumlah_Barang"), "N2")
                        DgvTimbang.Rows(id2).Cells(ItemTimbangBeratBarang).Value = Format(berat_barang, "N2")

                    Next
                End With
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Public Sub Get_DGVKeluar()

        Try
            OpenConn()

            ListView2.Items.Clear()
            ListView2.View = View.Details

            DgvPO.Rows.Clear()

            Dim id As Integer = 0
            SQL = " select c.no_PO, c.Kode_Barang, d.nama as Nama_Barang, c.Tanggal_Expired, c.Tanggal_Produksi, "
            SQL = SQL & "c.Urut_PO, c.Jumlah, c.Urut_Oto as Urut_Loading, c.satuan from "
            SQL = SQL & "EMI_Timbang_Unloading a, EMI_Timbang_Unloading_PO_Det b, EMI_Pembelian_Loading_Detail c, barang d "
            SQL = SQL & "where a.Kode_Perusahaan =b.Kode_Perusahaan and a.no_faktur=b.no_faktur and "
            SQL = SQL & "b.Kode_Perusahaan=c.Kode_Perusahaan and b.Urut_loading=c.Urut_Oto "
            SQL = SQL & "and c.Kode_Perusahaan=d.Kode_Perusahaan and c.Kode_Barang=d.Kode_Barang and c.Kode_Stock_Owner=d.Kode_Stock_Owner "
            SQL = SQL & "and a.status is null and a.No_faktur='" & Txt_NoFaktur.Text & "' and a.kode_Perusahaan ='" & KodePerusahaan & "' "
            SQL = SQL & "Order By d.nama "
            Using dr = OpenTrans(SQL)

                Do While dr.Read

                    DgvPO.Rows.Add(1)
                    DgvPO.Rows(id).Cells(ItemNoPO).Value = dr("No_Po")
                    DgvPO.Rows(id).Cells(ItemKdBarang).Value = dr("Kode_Barang")
                    DgvPO.Rows(id).Cells(ItemNama).Value = dr("Nama_Barang")
                    DgvPO.Rows(id).Cells(ItemTglExp).Value = Format(dr("Tanggal_Expired"), "dd MMM yyyy")
                    DgvPO.Rows(id).Cells(ItemTglProd).Value = Format(dr("tanggal_Produksi"), "dd MMM yyyy")
                    DgvPO.Rows(id).Cells(ItemUrutPO).Value = dr("Urut_PO")
                    DgvPO.Rows(id).Cells(ItemSatuan).Value = dr("Satuan")
                    DgvPO.Rows(id).Cells(ItemJumlah).Value = Format(dr("Jumlah"), "N2")
                    DgvPO.Rows(id).Cells(ItemJumlahMasuk).Value = 0
                    DgvPO.Rows(id).Cells(ItemUrutLoading).Value = dr("Urut_Loading")



                    id += 1
                Loop
            End Using

            DgvTimbang.Rows.Clear()

            SQL = "select a.Kode_Perusahaan, a.no_faktur, b.Kode_Barang, c.nama, b.Satuan, b.Satuan_Per_Bag, "
            SQL = SQL & "b.Jumlah_Per_Bag,c.Berat_Bags, c.Satuan_Berat_Bags "

            SQL = SQL & ",isnull((select sum(Jumlah_Bags) from emi_barang_masuk_perpallet x where "
            SQL = SQL & "x.Kode_Perusahaan=a.Kode_Perusahaan and x.No_Pembelian_Loading=a.No_Faktur and x.status is null "
            SQL = SQL & "and x.Kode_Barang=b.Kode_Barang),0) as Jumlah_Bags "

            SQL = SQL & ",isnull((select sum(Jumlah) from emi_barang_masuk_perpallet x where "
            SQL = SQL & "x.Kode_Perusahaan=a.Kode_Perusahaan and x.No_Pembelian_Loading=a.No_Faktur and x.status is null "
            SQL = SQL & "and x.Kode_Barang=b.Kode_Barang),0) as Jumlah_Barang "

            SQL = SQL & ",isnull((select sum(Jumlah_Pallet) from emi_barang_masuk_perpallet x where "
            SQL = SQL & "x.Kode_Perusahaan=a.Kode_Perusahaan and x.No_Pembelian_Loading=a.No_Faktur and x.status is null "
            SQL = SQL & "and x.Kode_Barang=b.Kode_Barang),0) as Jumlah_Pallet "

            SQL = SQL & "from EMI_Pembelian_Loading a, EMI_Pembelian_Loading_Detail b, barang c where "
            SQL = SQL & "a.No_Faktur=b.No_Faktur and a.Kode_Perusahaan=b.Kode_Perusahaan and a.status is null "
            SQL = SQL & "and b.Kode_Perusahaan=c.Kode_Perusahaan and b.Kode_Barang=c.Kode_Barang and b.Kode_Stock_Owner=c.Kode_Stock_Owner "
            SQL = SQL & "and a.kode_Perusahaan ='" & KodePerusahaan & "' and a.No_faktur='" & TxtNo_Loading.Text & "' "


            SQL = SQL & "and b.kode_Barang in( "

            SQL = SQL & "Select distinct b.Kode_Barang from EMI_Timbang_Unloading a, EMI_Timbang_Unloading_PO_Det b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_Faktur = b.No_Faktur And a.status Is null "
            SQL = SQL & "And a.Kode_Perusahaan='" & KodePerusahaan & "' and a.No_Faktur='" & Txt_NoFaktur.Text & "' "

            SQL = SQL & ") "

            SQL = SQL & "group by a.Kode_Perusahaan, a.no_faktur,b.Kode_Barang, c.nama, b.Satuan, b.Satuan_Per_Bag, "
            SQL = SQL & "b.Jumlah_Per_Bag,c.Berat_Bags, c.Satuan_Berat_Bags "

            SQL = SQL & "Order By c.nama "
            Using ds = BindingTrans(SQL)
                With ds.Tables("MyTable")
                    For id2 = 0 To .Rows.Count - 1

                        Dim berat_bags As Double
                        SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & .Rows(id2).Item("Kode_Barang") & "',"
                        SQL = SQL & "'" & .Rows(id2).Item("Satuan_Berat_Bags") & "','" & CmbSatuan.Text & "',"
                        SQL = SQL & "" & .Rows(id2).Item("Jumlah_Bags") * .Rows(id2).Item("Berat_Bags") & ") as Hasil "
                        Using dr3 = OpenTrans(SQL)
                            If dr3.Read Then
                                If General_Class.CekNULL(dr3("Hasil")) <> "" Then
                                    berat_bags = dr3("Hasil")
                                Else
                                    MessageBox.Show("Satuan " & .Rows(id2).Item("Satuan_Berat_Bags") & " Ke " & CmbSatuan.Text & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End If
                        End Using

                        Dim berat_barang As Double
                        SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & .Rows(id2).Item("Kode_Barang") & "',"
                        SQL = SQL & "'" & .Rows(id2).Item("Satuan") & "','" & CmbSatuan.Text & "',"
                        SQL = SQL & "" & .Rows(id2).Item("Jumlah_Barang") & ") as Hasil "
                        Using dr3 = OpenTrans(SQL)
                            If dr3.Read Then
                                If General_Class.CekNULL(dr3("Hasil")) <> "" Then
                                    berat_barang = dr3("Hasil")
                                Else
                                    MessageBox.Show("Satuan " & .Rows(id2).Item("Satuan_Berat_Bags") & " Ke " & CmbSatuan.Text & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End If
                        End Using

                        DgvTimbang.Rows.Add(1)
                        DgvTimbang.Rows(id2).Cells(ItemTimbangKdBarang).Value = .Rows(id2).Item("Kode_Barang")
                        DgvTimbang.Rows(id2).Cells(ItemTimbangNmBarang).Value = .Rows(id2).Item("Nama")
                        DgvTimbang.Rows(id2).Cells(ItemTimbangSatuan).Value = .Rows(id2).Item("Satuan")
                        DgvTimbang.Rows(id2).Cells(ItemTimbangJmlBags).Value = Format(.Rows(id2).Item("Jumlah_Bags"), "N0")
                        DgvTimbang.Rows(id2).Cells(ItemTimbangBeratBags).Value = Format(berat_bags, "N2")
                        DgvTimbang.Rows(id2).Cells(ItemTimbangJmlPallet).Value = Format(.Rows(id2).Item("Jumlah_Pallet"), "N0")
                        DgvTimbang.Rows(id2).Cells(ItemTimbangJmlBarang).Value = Format(.Rows(id2).Item("Jumlah_Barang"), "N2")
                        DgvTimbang.Rows(id2).Cells(ItemTimbangBeratBarang).Value = Format(berat_barang, "N2")

                    Next
                End With
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        getSumOfBerat()
    End Sub


    Public Sub Hitung_Netto()


        If Txt_Timbang2.Text.Trim <> "" Then


            Txt_Netto.Text = Format(Math.Max(0, Val(HilangkanTanda(Txt_Timbang1.Text)) - Val(HilangkanTanda(Txt_Timbang2.Text))), "N0")

            If CmbJenisMuatan.SelectedIndex <> -1 And DgvTimbang.RowCount <> 0 Then
                Dim indexSelected As Integer = CmbJenisMuatan.SelectedIndex
                Dim metodeTruckScale As String = arrMetodeTruckScale(indexSelected).ToString.ToUpper.Trim

                If metodeTruckScale = "TRUCK SCALE" Then
                    DgvTimbang.Rows(0).Cells(ItemTimbangJmlBarang).Value = Val(HilangkanTanda(Txt_Netto.Text))
                    DgvTimbang.Rows(0).Cells(ItemTimbangBeratBarang).Value = Val(HilangkanTanda(Txt_Netto.Text))
                End If

                getSumOfBerat()
            End If


        End If
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs)
        If e.KeyChar = Chr(13) Then Txt_Supir.Focus()
    End Sub

    Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs)
        If e.KeyChar = Chr(13) Then Btn_Simpan.Focus()
    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        kosong()
    End Sub

    Private Sub Txt_Timbang1_TextChanged(sender As Object, e As EventArgs) Handles Txt_Timbang1.TextChanged
        Hitung_Netto()

    End Sub



    Private Sub Txt_Timbang2_TextChanged(sender As Object, e As EventArgs) Handles Txt_Timbang2.TextChanged
        Hitung_Netto()
    End Sub

    Private Sub DataGridView1_CellEndEdit_1(sender As Object, e As DataGridViewCellEventArgs) Handles DgvPO.CellEndEdit
        If IsNumeric(DgvPO.CurrentRow.Cells(ItemJumlahMasuk).Value) = False Then
            DgvPO.CurrentRow.Cells(ItemJumlahMasuk).Value = ""
        End If
    End Sub

    Private Sub LblSatuan_Click(sender As Object, e As EventArgs) Handles LblSatuan.Click

    End Sub

    Private Sub CmbSatuan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbSatuan.SelectedIndexChanged

    End Sub

    Private Sub Txt_Timbang1_Leave(sender As Object, e As EventArgs) Handles Txt_Timbang1.Leave
        For i As Integer = 0 To DgvPO.RowCount - 1
            DgvPO.Rows(i).Cells(ItemJumlahMasuk).Value = 0
        Next
    End Sub

    Private Sub Txt_Timbang2_Leave(sender As Object, e As EventArgs) Handles Txt_Timbang2.Leave
        For i As Integer = 0 To DgvPO.RowCount - 1
            DgvPO.Rows(i).Cells(ItemJumlahMasuk).Value = 0
        Next
    End Sub

    Private Sub CmbJenisMuatan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbJenisMuatan.SelectedIndexChanged

        Dim indexSelected As Integer = CmbJenisMuatan.SelectedIndex
        Dim metodeTruckScale As String = arrMetodeTruckScale(indexSelected).ToString.ToUpper.Trim

        CmbBarang.SelectedIndex = -1
        If metodeTruckScale = "TRUCK SCALE" Then
            CmbBarang.Enabled = True


        Else
            CmbBarang.Enabled = False

        End If

    End Sub

    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click

        '''If Val(Txt_Timbangan.Text) = 0 Then
        '''    MessageBox.Show("TOLONG DIBUATIN LANGUAGE timbangan masih kosong", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        '''    Exit Sub
        '''ElseIf StreamPlayerControl1.IsPlaying = False Then
        '''    MessageBox.Show("TOLONG DIBUATIN LANGUAGE terjadi kesalahan pada kamera", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        '''    Exit Sub
        '''ElseIf StreamPlayerControl2.IsPlaying = False Then
        '''    MessageBox.Show("TOLONG DIBUATIN LANGUAGE terjadi kesalahan pada kamera", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        '''    Exit Sub
        '''End If

        get_jam()

        If CmbJenisMuatan.SelectedIndex = -1 Then
            MessageBox.Show("jenis Muatan Harus di isi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
            CmbJenisMuatan.Focus()
            Exit Sub
        ElseIf Txt_Supplier.Text.Trim.Length = 0 Then
            MessageBox.Show("Supplier Tidak ada", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Dim indexSelected As Integer = CmbJenisMuatan.SelectedIndex
        Dim metodeTruckScale As String = arrMetodeTruckScale(indexSelected).ToString.ToUpper.Trim




        Dim Init_Akhir As String = ""
        If jenisMasuk = "MASUK" Then
            Init_Akhir = "_BR"
        Else
            Init_Akhir = "_TR"
        End If

        '''Dim Image_1 As Bitmap = StreamPlayerControl1.GetCurrentFrame()
        '''Dim ImageCompress_1 As New Bitmap(Image_1, 640, 640) ' 1024, 768)

        '''Dim Image_2 As Bitmap = StreamPlayerControl2.GetCurrentFrame()
        '''Dim ImageCompress_2 As New Bitmap(Image_2, 640, 640)

        '''Dim TempFolder As String = System.IO.Path.GetTempPath()
        '''

        '''Dim BlobName_1 As String = Nama_File_1
        '''Dim FilePath_1 As String = TempFolder & Nama_File_1

        '''Dim BlobName_2 As String = Nama_File_2
        '''Dim FilePath_2 As String = TempFolder & Nama_File_2

        '''Dim Container As BlobContainerClient = New BlobContainerClient(ConnectionStringAzure, ContainerName)

        '''ImageCompress_1.Save(FilePath_1) 'simpan ke lokal
        '''ImageCompress_2.Save(FilePath_2) 'simpan ke lokal




        'JANGAN LUPA DI UNCOMMENT
        'Dim Nama_File_1 As String = Txt_NoFaktur.Text.Trim & "_" & Format(CDate(FormDevleopment.ToolStripStatusLabel3.Text), "yyyyMMddHHmmss") & Init_Akhir & "_A.jpg"
        'Dim Nama_File_2 As String = Txt_NoFaktur.Text.Trim & "_" & Format(CDate(FormDevleopment.ToolStripStatusLabel3.Text), "yyyyMMddHHmmss") & Init_Akhir & "_B.jpg"

        Dim Nama_File_1 As String = Txt_NoFaktur.Text.Trim & "_" & Format(CDate(FormDevleopment.ToolStripStatusLabel3.Text), "yyyyMMddHHmmss") & Init_Akhir & "_A.jpg"
        Dim Nama_File_2 As String = Txt_NoFaktur.Text.Trim & "_" & Format(CDate(FormDevleopment.ToolStripStatusLabel3.Text), "yyyyMMddHHmmss") & Init_Akhir & "_B.jpg"


        Try

            If jenisMasuk = "MASUK" Then

                If metodeTruckScale = "TRUCK SCALE" Then
                    If CmbBarang.SelectedIndex = -1 Then
                        MessageBox.Show("Barang Harus di isi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        CmbBarang.Focus()
                        Exit Sub
                    End If
                End If


                OpenConn()

                get_no_faktur()
                get_jam()

                No_Faktur = Txt_NoFaktur.Text

                Cmd.Transaction = Cn.BeginTransaction

                SQL = "Insert into EMI_Timbang_Unloading (Kode_Perusahaan, "
                SQL = SQL & "No_Faktur, No_Loading, Timbang_Masuk, Tgl_Timbang_Masuk, Jam_Timbang_Masuk, Foto_Timbang_Masuk_1, "
                SQL = SQL & "Foto_Timbang_Masuk_2, id_jenis_muatan, Satuan) values('" & KodePerusahaan & "', '" & Txt_NoFaktur.Text & "','" & TxtNo_Loading.Text & "', "
                SQL = SQL & "'" & HilangkanTanda(Txt_Timbang1.Text) & "', '" & Format(DTP_1.Value, "yyyy-MM-dd") & "', "
                SQL = SQL & "'" & Format(CDate(DTP_Bruto.Value), "HH:mm:ss") & "', '" & Nama_File_1 & "', '" & Nama_File_2 & "', '" & arrIdJenisMuatan.Item(CmbJenisMuatan.SelectedIndex) & "', '" & CmbSatuan.Text & "')"
                ExecuteTrans(SQL)

                '''SIMPAN Unloading PO
                Dim noFaktur As String = ""
                Dim noSuratJalan As String = ""
                Dim noPO As String = ""

                'For i As Integer = 0 To ListView2.Items.Count - 1
                For i As Integer = 0 To DgvPO.RowCount - 1
                    Get_Isi_DataGridView(i)


                    If LvNoPO <> noPO Then
                        SQL = "Insert into EMI_Timbang_Unloading_PO ("
                        SQL = SQL & "Kode_Perusahaan, No_Faktur, No_PO)"
                        SQL = SQL & "Values('" & KodePerusahaan & "', '" & Txt_NoFaktur.Text & "', "
                        SQL = SQL & "'" & LvNoPO & "') "
                        ExecuteTrans(SQL)

                        noPO = LvNoPO

                    End If

                    SQL = "Insert into EMI_Timbang_Unloading_PO_Det ("
                    SQL = SQL & "Kode_Perusahaan, No_Faktur, No_PO, Urut_Loading, Kode_Barang)"
                    SQL = SQL & "Values('" & KodePerusahaan & "', '" & Txt_NoFaktur.Text & "', "
                    SQL = SQL & "'" & LvNoPO & "', '" & LvUrutLoading & "', '" & LvKdBarang & "') "
                    ExecuteTrans(SQL)

                    SQL = "update EMI_Pembelian_Loading_Detail set flag_timbang_masuk='Y' where No_Faktur='" & TxtNo_Loading.Text & "' "
                    SQL = SQL & "and urut_oto='" & LvUrutLoading & "' and kode_barang='" & LvKdBarang & "'"
                    ExecuteTrans(SQL)

                Next

                'FLAGING BRUTO
                SQL = "update EMI_Pembelian_Loading set "
                SQL = SQL & "ID_Jenis_Muatan=" & arrIdJenisMuatan.Item(CmbJenisMuatan.SelectedIndex) & ", "
                SQL = SQL & "flag_proses_loading = 'Y' "
                SQL = SQL & "where No_faktur='" & TxtNo_Loading.Text & "' and Kode_Perusahaan='" & KodePerusahaan & "' "
                ExecuteTrans(SQL)


                SQL = "select Kode_Perusahaan from EMI_Pembelian_Loading_detail where "
                SQL = SQL & "No_faktur='" & TxtNo_Loading.Text & "' and Kode_Perusahaan='" & KodePerusahaan & "' "
                SQL = SQL & "and Flag_Timbang_masuk is null "
                Using dr = OpenTrans(SQL)
                    If Not dr.Read Then
                        dr.Close()
                        SQL = "update EMI_Pembelian_Loading set Flag_Timbang ='Y' "
                        SQL = SQL & "where No_faktur='" & TxtNo_Loading.Text & "' and Kode_Perusahaan='" & KodePerusahaan & "' "
                        ExecuteTrans(SQL)

                    End If
                End Using


                '''Dim Blob_1 As BlobClient = Container.GetBlobClient(BlobName_1)
                '''Blob_1.Upload(FilePath_1, New BlobHttpHeaders With {.ContentType = "image/jpeg"})

                '''Dim Blob_2 As BlobClient = Container.GetBlobClient(BlobName_2)
                '''Blob_2.Upload(FilePath_2, New BlobHttpHeaders With {.ContentType = "image/jpeg"})

                Cmd.Transaction.Commit()
                CloseConn()
                MessageBox.Show(Base_Language.Lang_Global_Sukses_Simpan, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)





                kosong()
                EMI_Display_Timbang.kosong()
                Me.Close()
                'Exit Sub
            ElseIf jenisMasuk = "KELUAR" Then

                'If  Then

                'End If
                getSumOfBerat()
                Dim totalJumlah As Double = 0

                For i As Integer = 0 To DgvTimbang.RowCount - 1
                    Get_Isi_DataGridViewTimbang(i)

                    Dim nilai As Double = Val(HilangkanTanda(LvTimbangjmlBarang))
                    Dim totalPO As Double = 0
                    Dim kd_barang = LvTimbangKdBarang

                    For index = 0 To DgvPO.RowCount - 1
                        Get_Isi_DataGridView(index)

                        If kd_barang = LvKdBarang Then
                            totalPO = totalPO + Val(HilangkanTanda(LvJumlahMasuk))
                        End If

                    Next

                    If nilai <> totalPO Then
                        MessageBox.Show(LvTimbangNmBarang & " Berbeda dengan PO")
                        Exit Sub
                    End If
                Next


                OpenConn()
                Cmd.Transaction = Cn.BeginTransaction

                No_Faktur = Txt_NoFaktur.Text

                Dim inisial_faktur_dari As String = ""
                Dim lokasi_Barang As String = ""

                SQL = "select kode_stock_owner from EMI_Barang_Masuk_Perpallet a where "
                SQL = SQL & "Kode_Perusahaan='" & KodePerusahaan & "' and "
                SQL = SQL & "No_Pembelian_Loading='" & TxtNo_Loading.Text & "' and status is null and Flag_Timbang_Keluar is null "
                Using dr = OpenTrans(SQL)
                    If dr.Read Then
                        lokasi_Barang = dr("kode_stock_owner")
                    Else
                        dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data Tidak ditemukan . . ! !")
                        Exit Sub
                    End If
                End Using

                SQL = "select inisial_faktur from stock_owner_gudang "
                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & lokasi_Barang & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        'akun_persediaan_dari = Dr("persediaan")
                        inisial_faktur_dari = Dr("inisial_faktur")

                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                Dim Kode_voucher As String = ""
                Kode_voucher = GetLastNumberJurnal(Format(tgl_skg, "yyyyMM"), "JS" & inisial_faktur_dari, KodePerusahaan)
                Dim pagenumber As Integer = 1

                SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
                SQL = SQL & "Keterangan, JudulBank, KetDK, userid) values("
                SQL = SQL & "'" & Kode_voucher & "', "
                SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', "
                SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
                SQL = SQL & "'" & KodeProyek & "', 'Barang Masuk " & Txt_NoFaktur.Text & "', '', "
                SQL = SQL & "'-', '" & UserID & "')"
                ExecuteTrans(SQL)


                SQL = "Update EMI_Timbang_Unloading "
                SQL = SQL & "Set Timbang_Keluar = '" & HilangkanTanda(Txt_Timbang2.Text) & "', "
                SQL = SQL & "Tgl_Timbang_Keluar = '" & Format(DTP_Tara.Value, "yyyy-MM-dd") & "', "
                SQL = SQL & "Jam_Timbang_Keluar = '" & Format(CDate(DTP_Tara.Value), "HH:mm:ss") & "', "
                SQL = SQL & "User_Timbang_Keluar = '" & UserID & "', "
                SQL = SQL & "Foto_Timbang_Keluar_1 = '" & Nama_File_1 & "', "
                SQL = SQL & "Foto_Timbang_Keluar_2 = '" & Nama_File_2 & "', "
                SQL = SQL & "Netto = '" & HilangkanTanda(Txt_Netto.Text) & "', "
                SQL = SQL & "flag_Selesai = 'Y', "
                SQL = SQL & "Kode_Voucher = '" & Kode_voucher & "', "
                SQL = SQL & "Jumlah_Bags = " & HilangkanTanda(Tot_Bags.Text) & " "
                SQL = SQL & "Where Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and No_Faktur = '" & Txt_NoFaktur.Text & "' "
                ExecuteTrans(SQL)


                'For i As Integer = 0 To ListView2.Items.Count - 1
                Dim sat_brg As String = ""
                Dim Total_HPP_PO
                For i As Integer = 0 To DgvPO.RowCount - 1

                    Get_Isi_DataGridView(i)

                    Dim Harga As Double = 0
                    Dim PPN As Double = 0
                    SQL = "Select (case when a.flag_refraksi Is null then b.Harga_barang else a.Harga_Refraksi end) as harga, c.PPN, a.Flag_Permintaan_Refraksi "
                    SQL = SQL & "From EMI_Pembelian_Loading_Detail a, EMI_Pembelian_PO_Detail b, EMI_Pembelian_PO c Where "
                    SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan And a.Urut_PO = b.No_Urut And "
                    SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan And b.No_Faktur = c.No_Faktur and c.status is null And "
                    SQL = SQL & "Urut_Oto = '" & LvUrutLoading & "' "
                    Using dr = OpenTrans(SQL)
                        If dr.Read Then

                            If General_Class.CekNULL(dr("Flag_Permintaan_Refraksi")) = "Y" Then
                                dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Terdapat Data yang Harus di Refraksi . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                            Harga = dr("Harga")
                            PPN = dr("PPN")
                        Else
                            dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("PO Tidak ditemukan . . ! !")
                            Exit Sub
                        End If
                    End Using

                    'If Val(HilangkanTanda(LvJumlahMasuk)) = 0 Then
                    '    MessageBox.Show("Jumlah masuk harus di isi")
                    '    CloseTrans()
                    '    CloseConn()
                    '    Exit Sub
                    'End If

                    Dim jmlhMasuk As Double = Val(LvJumlahMasuk)
                    Dim Satuan As String = LvSatuan

                    Dim jumlah_masuk_Barang As Double = 0
                    Dim Satuan_Barang As String = ""

                    SQL = "select distinct Satuan from Barang where "
                    SQL = SQL & "Kode_Barang='" & LvKdBarang & "' and Kode_Perusahaan='" & KodePerusahaan & "' "
                    Using dr2 = OpenTrans(SQL)
                        If dr2.Read Then
                            Satuan_Barang = dr2("Satuan")
                        Else
                            dr2.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Barang Tidak ditemukan . . ! !")
                            Exit Sub
                        End If
                    End Using
                    sat_brg = Satuan_Barang

                    'UBAH KE SATUAN PO
                    SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & LvKdBarang & "',"
                    SQL = SQL & "'" & Satuan & "','" & Satuan_Barang & "',"
                    SQL = SQL & "" & jmlhMasuk & ") as Hasil "
                    Using dr3 = OpenTrans(SQL)
                        If dr3.Read Then
                            If General_Class.CekNULL(dr3("Hasil")) <> "" Then
                                jumlah_masuk_Barang = dr3("Hasil")
                            Else
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Satuan " & Satuan & " Ke " & Satuan_Barang & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        End If
                    End Using

                    Dim TotalHPP As Double = Math.Round(jumlah_masuk_Barang * Harga)
                    Dim Nilai_PPN As Double = Math.Round(TotalHPP * PPN / 100)
                    Total_HPP_PO += TotalHPP

                    'UPDATE PEMBELIAN LOADING DETAIL

                    SQL = "update EMI_Pembelian_Loading_Detail set Jumlah_Masuk = Jumlah_Masuk + " & jumlah_masuk_Barang & " "
                    SQL = SQL & ", flag_timbang_keluar='Y'"
                    SQL = SQL & "where No_Faktur='" & TxtNo_Loading.Text & "' and Urut_Oto='" & LvUrutLoading & "'"
                    ExecuteTrans(SQL)


                    SQL = "update EMI_Timbang_Unloading_PO_Det set "
                    SQL = SQL & "jumlah ='" & jmlhMasuk & "', satuan ='" & Satuan & "', "
                    SQL = SQL & "nilai_barang ='" & jumlah_masuk_Barang & "', "
                    SQL = SQL & "Satuan_Barang ='" & Satuan_Barang & "', "
                    SQL = SQL & "Harga='" & Harga & "' "
                    SQL = SQL & "where No_Faktur='" & Txt_NoFaktur.Text & "' "
                    SQL = SQL & "and Urut_Loading='" & LvUrutLoading & "' "
                    ExecuteTrans(SQL)

                    '========================================================================================================
                    'Penjurnalan  LOKAL



                    Dim fRaw_Material_dari As String = ""
                    Dim fFinished_Good_dari As String = ""
                    Dim fSemi_FG_dari As String = ""
                    Dim fScrap_dari As String = ""
                    Dim fPackaging_dari As String = ""

                    Dim akun_persediaan_dari As String = ""
                    Dim akun_ppn As String = ""
                    Dim akun_hutang As String = ""

                    SQL = "select a.Flag_Raw_Material,a.Flag_Finished_Good,a.Flag_Semi_FG,a.Flag_Scrap, a.Flag_Packaging "
                    SQL = SQL & "from Barang b,EMI_Group_Jenis a where a.Kode_Perusahaan = b.Kode_Perusahaan "
                    SQL = SQL & "and a.Id_Group_Jenis = b.Id_Group_Jenis and b.Kode_Perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "and b.kode_stock_owner = '" & lokasi_Barang & "' and b.Kode_Barang='" & LvKdBarang & "' "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            fRaw_Material_dari = Dr("Flag_Raw_Material")
                            fFinished_Good_dari = Dr("Flag_Finished_Good")
                            fSemi_FG_dari = Dr("Flag_Semi_FG")
                            fScrap_dari = Dr("Flag_Scrap")
                            fPackaging_dari = Dr("Flag_Packaging")
                        Else
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using

                    SQL = "select inisial_faktur,Persediaan_Bahan_Baku,Persediaan, "
                    SQL = SQL & "Persediaan_Bahan_Setengah_Jadi,Persediaan_Scrap, "
                    SQL = SQL & "Persediaan_Packaging, hutang, PPN_Pembelian "
                    SQL = SQL & "from stock_owner_gudang "
                    SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & lokasi_Barang & "' "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            'akun_persediaan_dari = Dr("persediaan")
                            If fRaw_Material_dari = "Y" Then
                                akun_persediaan_dari = Dr("Persediaan_Bahan_Baku")
                            ElseIf fFinished_Good_dari = "Y" Then
                                akun_persediaan_dari = Dr("Persediaan")
                            ElseIf fSemi_FG_dari = "Y" Then
                                akun_persediaan_dari = Dr("Persediaan_Bahan_Setengah_Jadi")
                            ElseIf fScrap_dari = "Y" Then
                                akun_persediaan_dari = Dr("Persediaan_Scrap")
                            ElseIf fPackaging_dari = "Y" Then
                                akun_persediaan_dari = Dr("Persediaan_Packaging")
                            Else
                                Dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If

                            akun_hutang = Dr("hutang")
                            akun_ppn = Dr("PPN_Pembelian")

                        Else
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using

                    SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                    SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akun_persediaan_dari & "' "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            Dr.Close()
                            'update 

                            SQL = "update detail_jurnal set debit = debit+ " & TotalHPP & " where "
                            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                            SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                            SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akun_persediaan_dari & "' "
                            ExecuteTrans(SQL)
                        Else
                            Dr.Close()
                            'insert

                            SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_persediaan_dari, 1),
                                  Strings.Mid(akun_persediaan_dari, 2, 1),
                                  Strings.Mid(Ganti(akun_persediaan_dari), 3),
                                  KodePerusahaan, KodeProyek, "Persedian " & Txt_NoFaktur.Text, TotalHPP, "0", pagenumber, "TSSS")
                            ExecuteTrans(SQL)
                            pagenumber = pagenumber + 1

                        End If
                    End Using

                    SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                    SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akun_ppn & "' "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            Dr.Close()
                            'update 

                            SQL = "update detail_jurnal set debit = debit+ " & Nilai_PPN & " where "
                            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                            SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                            SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akun_ppn & "' "
                            ExecuteTrans(SQL)
                        Else
                            Dr.Close()
                            'insert

                            SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_ppn, 1),
                                  Strings.Mid(akun_ppn, 2, 1),
                                  Strings.Mid(Ganti(akun_ppn), 3),
                                  KodePerusahaan, KodeProyek, "PPN " & Txt_NoFaktur.Text, Nilai_PPN, "0", pagenumber, "TSSS")
                            ExecuteTrans(SQL)
                            pagenumber = pagenumber + 1

                        End If
                    End Using

                    SQL = "select kode_perusahaan from detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                    SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akun_hutang & "' "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            Dr.Close()
                            'update 

                            SQL = "update detail_jurnal set kredit = kredit+ " & Nilai_PPN + TotalHPP & " where "
                            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                            SQL = SQL & "kode_voucher = '" & Kode_voucher & "' and "
                            SQL = SQL & "kode_master_acc + kode_acc + kode_detail_acc = '" & akun_hutang & "' "
                            ExecuteTrans(SQL)
                        Else
                            Dr.Close()
                            'insert

                            SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_hutang, 1),
                                  Strings.Mid(akun_hutang, 2, 1),
                                  Strings.Mid(Ganti(akun_hutang), 3),
                                  KodePerusahaan, KodeProyek, "Hutang " & Txt_NoFaktur.Text, "0", Nilai_PPN + TotalHPP, pagenumber, "TSSS")
                            ExecuteTrans(SQL)
                            pagenumber = pagenumber + 1

                        End If
                    End Using


                Next

                SQL = "select sum(debit) as debit, sum(kredit) as kredit from detail_jurnal where "
                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "kode_voucher = '" & Kode_voucher & "'"
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        If Dr("debit") <> Dr("kredit") Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Jurnal salah!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data jurnal tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using




                Dim jumlah_masuk_BarangTimbang As Double = 0
                If metodeTruckScale = "TRUCK SCALE" Then

                    SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & LvKdBarang & "',"
                    SQL = SQL & "'" & CmbSatuan.Text & "','" & sat_brg & "',"
                    SQL = SQL & "" & HilangkanTanda(Txt_Netto.Text) & ") as Hasil "
                    Using dr3 = OpenTrans(SQL)
                        If dr3.Read Then
                            If General_Class.CekNULL(dr3("Hasil")) <> "" Then
                                jumlah_masuk_BarangTimbang = dr3("Hasil")
                            Else
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Satuan " & CmbSatuan.Text & " Ke " & sat_brg & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        End If
                    End Using

                    SQL = "Update EMI_Barang_Masuk_Perpallet set "
                    SQL = SQL & "Flag_Timbang = 'Y', "
                    SQL = SQL & "jumlah = '" & HilangkanTanda(Txt_Netto.Text) & "', "
                    SQL = SQL & "Nilai_Barang = '" & jumlah_masuk_BarangTimbang & "', "
                    SQL = SQL & "tanggal_Timbang = '" & Format(CDate(tgl_skg), "yyyy-MM-dd") & "', "
                    SQL = SQL & "jam_Timbang = '" & Format(CDate(tgl_skg), "HH:mm:ss") & "', "
                    SQL = SQL & "user_Timbang = '" & UserID & "' "

                    SQL = SQL & "Where Kode_Perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "and No_pembelian_loading = '" & TxtNo_Loading.Text & "' "
                    SQL = SQL & "and Metode_timbang = 'TRUCK SCALE' "
                    ExecuteTrans(SQL)

                End If

                Dim total_hpp As Double = 0

                SQL = "select Flag_angkut, Selesai, Sdh_Cetak, no_faktur, "
                SQL = SQL & "kode_perusahaan, kode_stock_owner, kode_barang, "
                SQL = SQL & "serial_number, Nilai_Barang as jumlah, Tgl_Produksi_Real as Tgl_Produksi, "
                SQL = SQL & "Tgl_Expired_Real as Tgl_Expired, Id_Warehouse, "
                SQL = SQL & "id_Susunan,  Kode_Unik_Asal, Kode_Unik_Berjalan, "
                SQL = SQL & "Jumlah_Bags, Qr_Code, Batch_Number, warna from EMI_Barang_Masuk_Perpallet a where "
                SQL = SQL & "Kode_Perusahaan='" & KodePerusahaan & "' and "
                SQL = SQL & "No_Pembelian_Loading='" & TxtNo_Loading.Text & "' and status is null and Flag_Timbang_Keluar is null "
                Using ds = BindingTrans(SQL)
                    With ds.Tables("MyTable")
                        If .Rows.Count <> 0 Then
                            For index = 0 To .Rows.Count - 1

                                If General_Class.CekNULL(.Rows(index).Item("Sdh_Cetak")) = "" Then
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Proses tidak bisa dilanjutkan, barang Belum Selesai Bongkar !!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If


                                Dim harga As Double = 0
                                SQL = "Select Top(1)(case when "
                                SQL = SQL & "a.flag_refraksi Is null then b.Harga_barang else a.Harga_Refraksi end) As harga "
                                SQL = SQL & "From EMI_Pembelian_Loading_Detail a, EMI_Pembelian_PO_Detail b, "
                                SQL = SQL & "EMI_Barang_Masuk_Perpallet_Detail c Where "
                                SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan And a.Urut_PO = b.No_Urut And "
                                SQL = SQL & "a.Kode_Perusahaan = c.Kode_Perusahaan And a.Urut_Oto = c.Urut_Loading "
                                SQL = SQL & "and c.kode_Perusahaan='" & KodePerusahaan & "' and c.no_faktur='" & .Rows(index).Item("no_faktur") & "' "
                                Using dr = OpenTrans(SQL)
                                    If dr.Read Then
                                        harga = dr("Harga")
                                    Else
                                        dr.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("PO Tidak ditemukan . . ! !")
                                        Exit Sub
                                    End If
                                End Using

                                total_hpp += (harga * .Rows(index).Item("jumlah"))

                                Dim Random As New Random()
                                Dim str As String = Format(Random.Next(0, 999), "000") & Format(CDate(FormDevleopment.ToolStripStatusLabel3.Text), "HHmmss")
                                Dim Kode_Unik As String = str.Substring(0, 5) & "BB" & Chr(64 + str.Substring(6, 1)) & str.Substring(6, Len(str) - 6)
                                Dim SN_Baru As String = Kode_Unik & Tanda_SN & "01" & Tanda_SN & harga & Tanda_SN & "02" & Tanda_SN & Format(DateTime.Now, "yyyy-MM-dd")


                                SQL = "Update barang Set "
                                SQL = SQL & "good_stock = good_stock + " & .Rows(index).Item("jumlah") & ", "
                                SQL = SQL & "Jumlah_Bags = Jumlah_Bags +" & .Rows(index).Item("Jumlah_Bags") & " where "
                                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' And "
                                SQL = SQL & "kode_stock_owner = '" & .Rows(index).Item("kode_stock_owner") & "' And "
                                SQL = SQL & "kode_barang = '" & .Rows(index).Item("kode_barang") & "' "
                                ExecuteTrans(SQL)

                                ''GET ID_WAREHOUSE YG KOSONG
                                Dim available_Id_Warehouse As String = ""
                                Dim available_NoPallet As String = ""

                                SQL = "select top(1) a.id_wms_warehouse_position, b.nomor_urut from "
                                SQL = SQL & "view_warehouse_position a, view_warehouse_position_detail b "
                                SQL = SQL & "where a.Id_WMS_Warehouse_Position=b.Id_WMS_Warehouse_Position "
                                SQL = SQL & " And a.kode_Perusahaan = b.kode_Perusahaan And a.kode_Perusahaan ='" & KodePerusahaan & "' "
                                SQL = SQL & "and a.Kode_Stock_Owner='" & .Rows(index).Item("kode_stock_owner") & "' and b.Kode_Barang is null"
                                Using Dr2 = OpenTrans(SQL)
                                    Do While Dr2.Read
                                        available_Id_Warehouse = Dr2("id_wms_warehouse_position")
                                        available_NoPallet = Dr2("nomor_urut")
                                    Loop
                                End Using


                                SQL = "insert into Barang_SN(kode_perusahaan, kode_stock_owner, kode_barang, "
                                SQL = SQL & "serial_number, jumlah, Tgl_Produksi, Tgl_Expired, Id_Warehouse, "
                                SQL = SQL & "id_Susunan, Nomor_Pallet, Kode_Unik_Asal, Kode_Unik_Berjalan, "
                                SQL = SQL & "Jumlah_Bags, Qr_Code, Batch_Number, warna) "
                                SQL = SQL & "Values( "
                                SQL = SQL & "'" & KodePerusahaan & "','" & .Rows(index).Item("kode_stock_owner") & "', "
                                SQL = SQL & "'" & .Rows(index).Item("kode_barang") & "','" & SN_Baru & "', "
                                SQL = SQL & "'" & .Rows(index).Item("jumlah") & "','" & .Rows(index).Item("Tgl_Produksi") & "', "
                                SQL = SQL & "'" & .Rows(index).Item("Tgl_Expired") & "','" & available_Id_Warehouse & "', "
                                SQL = SQL & "'" & .Rows(index).Item("id_Susunan") & "','" & available_NoPallet & "', "
                                SQL = SQL & "'" & .Rows(index).Item("Kode_Unik_Asal") & "','" & .Rows(index).Item("Kode_Unik_Berjalan") & "', "
                                SQL = SQL & "'" & .Rows(index).Item("Jumlah_Bags") & "','" & .Rows(index).Item("Qr_Code") & "', "
                                SQL = SQL & "'" & .Rows(index).Item("Batch_Number") & "','" & .Rows(index).Item("warna") & "') "
                                ExecuteTrans(SQL)

                                SQL = "SELECT round(SUM(good_stock),2) AS good_stock, isnull((select round(sum(jumlah),2) from Barang_sn x "
                                SQL = SQL & "where a.kode_Barang=x.kode_Barang and a.Kode_Stock_Owner=x.kode_Stock_Owner "
                                SQL = SQL & "and a.kode_Perusahaan=x.kode_Perusahaan ),0) as Jumlah_sn, "
                                SQL = SQL & "isnull(round(SUM(jumlah_bags), 2), 0) AS jumlah_bags_barang, "
                                SQL = SQL & "isnull((select round(sum(Jumlah_Bags), 2) from Barang_sn y "
                                SQL = SQL & "where a.kode_Barang=y.kode_Barang and a.Kode_Stock_Owner=y.kode_Stock_Owner and a.kode_Perusahaan=y.kode_Perusahaan ), 0) as jumlah_bags_sn "
                                SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner = '" & .Rows(index).Item("kode_stock_owner") & "' "
                                SQL = SQL & "AND a.Kode_Barang = '" & .Rows(index).Item("kode_barang") & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
                                SQL = SQL & "group by a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan "
                                Using Ds4 = BindingTrans(SQL)

                                    If Ds4.Tables("MyTable").Rows.Count <> 0 Then
                                        If Ds4.Tables("MyTable").Rows(0).Item("good_stock") <> Ds4.Tables("MyTable").Rows(0).Item("Jumlah_sn") Or Ds4.Tables("MyTable").Rows(0).Item("jumlah_bags_barang") <> Ds4.Tables("MyTable").Rows(0).Item("jumlah_bags_sn") Then
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("Terjadi Kesalahan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If
                                    Else
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Data tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If

                                End Using

                                SQL = "Update EMI_Barang_Masuk_Perpallet set "
                                SQL = SQL & "serial_number_awal = '" & SN_Baru & "', "
                                SQL = SQL & "Flag_Timbang_Keluar = 'Y' "
                                SQL = SQL & "Where Kode_Perusahaan = '" & KodePerusahaan & "' and "
                                SQL = SQL & "no_faktur='" & .Rows(index).Item("no_faktur") & "' "
                                ExecuteTrans(SQL)

                            Next
                        Else

                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("data Tidak ditemukan . . ! !")
                            Exit Sub

                        End If
                    End With
                End Using

                If total_hpp <> Total_HPP_PO Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data Tidak Sinkron")
                    Exit Sub
                End If


                SQL = "Update EMI_Pembelian_Loading "
                SQL = SQL & "Set Flag_Proses_loading = null "
                SQL = SQL & "Where No_Faktur = '" & TxtNo_Loading.Text & "' "
                ExecuteTrans(SQL)

                'CEK APAKAH PO TERPENUHI
                SQL = "select * from EMI_Pembelian_Loading_Detail where No_Faktur='" & TxtNo_Loading.Text & "' "
                SQL = SQL & "and Flag_Timbang_Keluar is null"
                Using dr = OpenTrans(SQL)
                    If Not dr.Read Then
                        dr.Close()
                        SQL = "Update EMI_Pembelian_Loading "
                        SQL = SQL & "Set Flag_Timbang_Keluar = 'Y', "
                        SQL = SQL & "Flag_Proses_loading = 'Y' "
                        SQL = SQL & "Where No_Faktur = '" & TxtNo_Loading.Text & "' "
                        ExecuteTrans(SQL)
                    End If
                End Using















                '''Dim Blob_1 As BlobClient = Container.GetBlobClient(BlobName_1)
                '''Blob_1.Upload(FilePath_1, New BlobHttpHeaders With {.ContentType = "image/jpeg"})

                '''Dim Blob_2 As BlobClient = Container.GetBlobClient(BlobName_2)
                '''Blob_2.Upload(FilePath_2, New BlobHttpHeaders With {.ContentType = "image/jpeg"})
                'Btn_Simpan.Tag = "&SimpanBruto"
                'Btn_Simpan.Text = "&Simpan Bruto"
                ' kosong()
                Cmd.Transaction.Commit()
                CloseConn()

                MessageBox.Show(Base_Language.Lang_Global_Sukses_Simpan, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)


                'Exit Sub

            End If

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


        '=====================
        '=       CETAK       =
        '=====================
        Try
            OpenConn()

            Dim CrDoc As New Object


            ''REPORT

            If jenisMasuk = "MASUK" Then

                SQL = "select top 1 No_Faktur from EMI_Timbang_Unloading_PO_Det where no_Faktur='" & No_Faktur & "'"
                Using Ds = BindingTrans(SQL)
                    If Ds.Tables("MyTable").Rows.Count <> 0 Then
                        CrDoc = New Rpt_Surat_Perintah_Bongkar
                        With A_Place_For_Printing2
                            CrDoc.SetDataSource(Ds)
                            CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                            CrDoc.PrintOptions.PrinterName = ""
                            CrDoc.RecordSelectionFormula = "{EMI_Timbang_Unloading_PO_Det.Kode_Perusahaan} = '" & KodePerusahaan & "' and {EMI_Timbang_Unloading_PO_Det.No_Faktur}='" & No_Faktur & "' "
                            CrDoc.SummaryInfo.ReportTitle = "Surat Perintah Bongkar"
                            .Text = "Surat Perintah Bongkar"
                            .CrystalReportViewer1.ReportSource = CrDoc
                            .Refresh()
                            .Show()
                        End With
                    End If
                End Using

            ElseIf jenisMasuk = "KELUAR" Then

                SQL = "select top 1 No_Faktur from EMI_Timbang_Unloading_PO_Det where no_Faktur='" & No_Faktur & "'"
                Using Ds = BindingTrans(SQL)
                    If Ds.Tables("MyTable").Rows.Count <> 0 Then
                        CrDoc = New Rpt_Bukti_Penerimaan_Barang
                        With A_Place_For_Printing2
                            CrDoc.SetDataSource(Ds)
                            CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                            CrDoc.PrintOptions.PrinterName = ""
                            CrDoc.RecordSelectionFormula = "{EMI_Timbang_Unloading_PO_Det.Kode_Perusahaan} = '" & KodePerusahaan & "' and {EMI_Timbang_Unloading_PO_Det.No_Faktur}='" & No_Faktur & "' "
                            CrDoc.SummaryInfo.ReportTitle = "Surat Bukti Penerimaan Barang"
                            .Text = "Surat Bukti Penerimaan Barang"
                            .CrystalReportViewer1.ReportSource = CrDoc
                            .Refresh()
                            .Show()
                        End With
                    End If
                End Using

                SQL = "select top 1 No_Faktur from EMI_Timbang_Unloading where no_Faktur='" & No_Faktur & "'"
                Using Ds = BindingTrans(SQL)
                    If Ds.Tables("MyTable").Rows.Count <> 0 Then
                        CrDoc = New Rpt_Bukti_Timbang
                        With A_Place_For_Printing3
                            CrDoc.SetDataSource(Ds)
                            CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                            CrDoc.PrintOptions.PrinterName = ""
                            CrDoc.RecordSelectionFormula = "{EMI_Timbang_Unloading.Kode_Perusahaan} = '" & KodePerusahaan & "' and {EMI_Timbang_Unloading.No_Faktur}='" & No_Faktur & "' "
                            CrDoc.SummaryInfo.ReportTitle = "Surat Bukti Penerimaan Barang"
                            .Text = "Surat Bukti Timbang"
                            .CrystalReportViewer1.ReportSource = CrDoc
                            .Refresh()
                            .Show()
                        End With
                    End If
                End Using

            End If

            kosong()
            EMI_Display_Timbang.kosong()
            Me.Close()

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub Txt_Timbang1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Timbang1.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub Txt_Timbang2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Timbang2.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    'Private Sub DataGridView1_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles DataGridView1.EditingControlShowing
    '    AddHandler e.Control.KeyPress, AddressOf TextBoxColumn3_KeyPress
    'End Sub

    'Private Sub TextBoxColumn3_KeyPress(sender As Object, e As KeyPressEventArgs)
    '    If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
    '        e.Handled = True
    '    End If
    'End Sub

    Private Sub DataGridView1_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs)

        Get_Isi_DataGridView(DgvPO.CurrentRow.Index)

        If IsNumeric(LvJumlahMasuk) = False Or Val(LvJumlahMasuk) < 0 Then
            DgvPO.CurrentRow.Cells(ItemJumlahMasuk).Value = 0
            Exit Sub
        End If


        'If getSumOfJumlah() > Val(HilangkanTanda(Txt_Netto.Text)) Then

        '    MessageBox.Show("Jumlah Berlebih dari berat Netto", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '    DgvPO.CurrentRow.Cells(ItemJumlahMasuk).Value = 0
        '    Exit Sub
        'End If


    End Sub

    Private Sub getSumOfJumlah()
        Dim totalJumlah As Double = 0

        For i As Integer = 0 To DgvTimbang.RowCount - 1
            Get_Isi_DataGridViewTimbang(i)

            Dim nilai As Double = Val(HilangkanTanda(LvTimbangjmlBarang))
            Dim totalPO As Double = 0
            Dim kd_barang = LvTimbangKdBarang

            For index = 0 To DgvPO.RowCount - 1
                Get_Isi_DataGridView(index)

                If kd_barang = LvKdBarang Then
                    totalPO = totalPO + Val(HilangkanTanda(LvJumlahMasuk))
                End If

            Next

            If nilai <> totalPO Then
                MessageBox.Show(LvTimbangNmBarang & " Berbeda dengan PO")
                Exit Sub
            End If
        Next


    End Sub
    Private Sub getSumOfBerat()
        Dim totalJumlah As Double = 0
        Dim totalBags As Double = 0

        For i As Integer = 0 To DgvTimbang.RowCount - 1
            Get_Isi_DataGridViewTimbang(i)

            Dim nilai As Double = Val(HilangkanTanda(LvTimbangBeratBarang)) - Val(HilangkanTanda(LvTimbangBeratBags))
            totalJumlah = totalJumlah + nilai

            totalBags = totalBags + Val(HilangkanTanda(LvTimbangJmlBags))

        Next

        TxtTotalBeratBarang.Text = Format(totalJumlah, "N2")
        Tot_Bags.Text = Format(totalBags, "N2")
    End Sub

    Private Sub Tot_Bags_TextChanged(sender As Object, e As EventArgs) Handles Tot_Bags.TextChanged

    End Sub

    Private Sub DgvTimbang_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvTimbang.CellContentClick

    End Sub

    Private Sub Transaksi_Timbang_Unloading_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub CmbBarang_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbBarang.SelectedIndexChanged
        Get_DGVMasuk()
    End Sub

    Private Sub Jurnal_Import()


        Dim id_rencana As Integer
        Dim Lokasi_Jurnal As Integer
        SQL = "select Lokasi, No_Fak_HPP, b.ID_Rencana "
        SQL = SQL & "from emi_pembelian_loading a, HPP_Import b where "
        SQL = SQL & "a.Kode_Perusahaan=b.kode_perusahaan and a.No_Fak_HPP=b.No_Faktur "
        SQL = SQL & "and a.status is null and b.status is null "
        SQL = SQL & "and a.No_Faktur = '" & TxtNo_Loading.Text & "' "
        SQL = SQL & "and a.Kode_Perusahaan='" & KodePerusahaan & "' "
        Using dr = OpenTransSQL(SQL)
            If dr.Read Then
                id_rencana = dr("ID_Rencana")
                Lokasi_Jurnal = dr("Lokasi")
            Else
                dr.Close()
                CloseTrans()
                CloseConn()
                MessageBox.Show("No Faktur Tidak ditemukan . . ! ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
        End Using

        'inser jurnal ard
        Dim coa_Hutang_Dalam_Proses As String = ""
        Dim coa_Selisih_Hutang_Import As String = ""
        Dim coa_Billing As String = ""
        Dim coa_freigt As String = ""
        Dim coa_Storage As String = ""
        Dim coa_Tot_Pot_Stock_IDR As String = ""
        Dim coa_Tdk_Pot_Stock_IDR As String = ""
        Dim coa_Tdk_Pot_Stock_Hutang_IDR_Utama As String = ""
        Dim coa_Tdk_Pot_Stock_Hutang_IDR_Penolong As String = ""
        Dim coa_pph As String = ""
        Dim coa_pib As String = ""
        Dim coa_selisih_pib As String = ""
        Dim coa_pph_billing As String = ""
        Dim coa_hutang_pph_billing As String = ""
        Dim coa_Selisih_AVG_Import As String = ""
        Dim coa_Selisih_PO As String = ""
        Dim coa_Selisih_PO_Biaya As String = ""
        Dim coa_selisih_new
        Dim Metode_Hitung_Konte As String = ""

        
        SQL = "select hutang_pph_billing, pph_billing, Metode_Hitung_Konte, Hutang_Dalam_Proses, Selisih_Hutang_Import, Hutang_Billing_Import, "
        SQL = SQL & "Hutang_Storage_Import, Hutang_Freight_Import, Akun_Tot_Pot_Stock, "
        SQL = SQL & "Akun_Tdk_Pot_Stock, Akun_Tdk_Pot_Stock_Hutang_Utama, "
        SQL = SQL & "Akun_Tdk_Pot_Stock_Hutang_Penolong, Akun_pph, akun_pib, akun_selisih_pib, Akun_Selisih_AVG_Import, Akun_Selisih_PO, Akun_Selisih_PO_Biaya from stock_Owner "
        SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and Kode_stock_Owner = '" & Lokasi_Jurnal & "' "
        Using dr = OpenTrans(SQL)
            If dr.Read Then
                Metode_Hitung_Konte = dr("Metode_Hitung_Konte")
                coa_Hutang_Dalam_Proses = dr("Hutang_Dalam_Proses")
                coa_Selisih_Hutang_Import = dr("Selisih_Hutang_Import")
                coa_Billing = dr("Hutang_Billing_Import")
                coa_freigt = dr("Hutang_Freight_Import")
                coa_Storage = dr("Hutang_Storage_Import")
                coa_Tot_Pot_Stock_IDR = dr("Akun_Tot_Pot_Stock")
                coa_Tdk_Pot_Stock_IDR = dr("Akun_Tdk_Pot_Stock")
                coa_Tdk_Pot_Stock_Hutang_IDR_Utama = dr("Akun_Tdk_Pot_Stock_Hutang_Utama")
                coa_Tdk_Pot_Stock_Hutang_IDR_Penolong = dr("Akun_Tdk_Pot_Stock_Hutang_Penolong")
                coa_pph = dr("Akun_pph")
                coa_pib = dr("akun_pib")
                coa_selisih_pib = dr("akun_selisih_pib")
                coa_pph_billing = dr("pph_billing")
                coa_hutang_pph_billing = dr("hutang_pph_billing")
                coa_Selisih_AVG_Import = dr("Akun_Selisih_AVG_Import")
                coa_Selisih_PO = dr("Akun_Tdk_Pot_Stock_Hutang_Utama")
                coa_Selisih_PO_Biaya = dr("Akun_Tdk_Pot_Stock_Hutang_Utama")
                coa_selisih_new = dr("Akun_Selisih_PO")
            Else
                dr.Close()
                CloseTrans()
                CloseConn()
                MessageBox.Show("Lokasi Tidak ditemukan")
                Exit Sub
            End If
        End Using

        Dim Flag_Average_Sup As String = ""
        SQL = "select Flag_average "
        SQL = SQL & "from Transaksi_Biaya_Import a where  a.Id_rencana = '" & id_rencana & "' and status is null"
        Using Dr = OpenTrans(SQL)
            If Dr.Read Then
                Flag_Average_Sup = Dr("Flag_average")
            Else
                Dr.Close()
                CloseTrans()
                CloseConn()
                MessageBox.Show("Supplier Tidak ditemukan")
                Exit Sub
            End If
        End Using

        Dim Flag_Average_Sup3 As String = ""
        SQL = "select Flag_average "
        SQL = SQL & "from Transaksi_Biaya_Import3 a where  a.Id_rencana = '" & id_rencana & "' and status is null"
        Using Dr = OpenTrans(SQL)
            If Dr.Read Then
                Flag_Average_Sup3 = Dr("Flag_average")
            Else
                Dr.Close()
                CloseTrans()
                CloseConn()
                MessageBox.Show("Supplier Tidak ditemukan")
                Exit Sub
            End If
        End Using





        Dim Arr_Biaya_Import_Master As New ArrayList
        Dim Arr_Biaya_Import As New ArrayList
        Dim Arr_Biaya_Import_AVG As New ArrayList
        Dim Arr_Akun1 As New ArrayList
        Dim Arr_Akun2 As New ArrayList
        Dim Arr_Biaya_Import_Kategori As New ArrayList

        Dim Arr_Biaya_Bongkar_Import_Master As New ArrayList
        Dim Arr_Biaya_Bongkar_Import As New ArrayList
        Dim Arr_Lokasi_Bongkar_Import As New ArrayList
        Dim Arr_Akun1_Bongkar As New ArrayList
        Dim Arr_Akun2_Bongkar As New ArrayList
        Dim Arr_Biaya_Bongkar_Import_Kategori As New ArrayList

        Dim Biaya_Import_AVG As Double = 0
        Dim Selisih_Import_AVG As Double = 0
        Dim Biaya_Import_Total As Double = 0
        Dim Hutang_Dalam_Proses As Double = 0
        Dim Selisih_Hutang As Double = 0
        Dim Biaya_PPN As Double = 0
        Dim Billing As Double = 0
        Dim pib As Double = 0
        Dim pph_billing As Double = 0
        Dim Selisih_PO As Double = 0
        Dim Selisih_PO_Biaya As Double = 0

        Dim freigt As Double = 0
        Dim Storage As Double = 0
        Dim Tot_Pot_Stock_IDR As Double = 0
        Dim Tdk_Pot_Stock_IDR As Double = 0
        Dim Tdk_Pot_Stock_Hutang_IDR_Utama As Double = 0
        Dim Tdk_Pot_Stock_Hutang_IDR_Penolong As Double = 0
        Dim pph_pakai_persentase As Double = 0

        For index = 0 To DgvPO.Rows.Count - 1
            Get_Isi_DataGridView(index)
            SQL = "select a.No_faktur, a.ID_Rencana, b.Kode_stock_owner, b.Kode_barang, b.jumlah,"
            SQL = SQL & "b.Nilai_Pot_Stock/Jumlah as Nilai_Pot_Stock, Nilai_Tdk_Pot_stock_LNS/Jumlah as Nilai_Tdk_Pot_stock_LNS,"
            SQL = SQL & "b.Nilai_tdk_pot_stock_htg_utama/Jumlah as Nilai_tdk_pot_stock_htg_utama,"
            SQL = SQL & "b.Nilai_Tdk_Pot_Stock_HTG_Penolong/Jumlah as Nilai_Tdk_Pot_Stock_HTG_Penolong,"
            SQL = SQL & "b.PPH29/Jumlah as PPH29,Biaya_Billing/Jumlah as Biaya_Billing,Biaya_Kontainer/Jumlah as Biaya_Kontainer, b.Nilai_Selisih_PO/Jumlah as Nilai_Selisih_PO, b.Nilai_Selisih_PO_Biaya/Jumlah as Nilai_Selisih_PO_Biaya "
            SQL = SQL & "from HPP_Import a, Detail_HPP_Import b where "
            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_Faktur = b.No_faktur And a.Status Is null And id_rencana ='" & id_rencana & "' AND B.Kode_Barang='" & LvKdBarang & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Storage = Storage + (Dr("biaya_kontainer") * LvJumlah)
                    Billing = Billing + (Dr("Biaya_Billing") * LvJumlah)
                    Tot_Pot_Stock_IDR = Tot_Pot_Stock_IDR + (Dr("Nilai_Pot_Stock") * LvJumlah)
                    Tdk_Pot_Stock_IDR = Tdk_Pot_Stock_IDR + (Dr("Nilai_Tdk_Pot_stock_LNS") * LvJumlah)
                    Tdk_Pot_Stock_Hutang_IDR_Utama = Tdk_Pot_Stock_Hutang_IDR_Utama + (Dr("Nilai_tdk_pot_stock_htg_utama") * LvJumlah)
                    Tdk_Pot_Stock_Hutang_IDR_Penolong = Tdk_Pot_Stock_Hutang_IDR_Penolong + (Dr("Nilai_Tdk_Pot_Stock_HTG_Penolong") * LvJumlah)
                    pph_pakai_persentase = pph_pakai_persentase + (Dr("PPH29") * LvJumlah)
                    Selisih_PO = Selisih_PO + (Dr("Nilai_Selisih_PO") * LvJumlah)
                    Selisih_PO_Biaya = Selisih_PO_Biaya + (Dr("Nilai_Selisih_PO_Biaya") * LvJumlah)
                End If
            End Using

            SQL = "select b.kode_stock_owner, b.Kode_Barang, jumlah, b.Nilai_PPH/Jumlah as Nilai_PPH, Nilai_PPN/Jumlah as Nilai_PPN "
            SQL = SQL & "from Total_Billing a, Detail_Total_Billing b where a.Kode_Perusahaan=b.Kode_perusahaan and a.No_Faktur=b.No_Faktur "
            SQL = SQL & "and a.ID_Rencana='" & id_rencana & "' and a.Status is nulL AND B.Kode_Barang='" & LvKdBarang & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    pib = pib + (Dr("Nilai_PPN") * LvJumlah)
                    pph_billing = pph_billing + (Dr("Nilai_PPH") * LvJumlah)

                End If
            End Using

            '1
            SQL = "select b.Kode_Master_Kategori_Biaya_Import, b.kode_kategori_biaya_import, c.Flag_Masuk_Jurnal, "
            SQL = SQL & "round(sum(b.total), 0) as Biaya, "

            SQL = SQL & "isnull(("
            SQL = SQL & "select akun_1 from detail_account_master X where X.Kode_Perusahaan = b.Kode_Perusahaan and "
            SQL = SQL & "x.Kode_Master_Kategori_Biaya_import = b.Kode_Master_Kategori_Biaya_import and "
            SQL = SQL & "x.lokasi = '" & Lokasi_Jurnal & "' "
            SQL = SQL & "),0) as Akun_1, "

            SQL = SQL & "isnull(("
            SQL = SQL & "select akun_2 from detail_account_master X where X.Kode_Perusahaan = b.Kode_Perusahaan and "
            SQL = SQL & "x.Kode_Master_Kategori_Biaya_import = b.Kode_Master_Kategori_Biaya_import and "
            SQL = SQL & "x.lokasi = '" & Lokasi_Jurnal & "' "
            SQL = SQL & "),0) as Akun_2 "

            SQL = SQL & "from transaksi_biaya_import a, detail_transaksi_biaya_import b, Master_Kategori_Biaya_Import c where "
            SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan And "
            SQL = SQL & "a.no_faktur = b.no_faktur And a.status Is null and "
            SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Master_Kategori_Biaya_import = c.Kode_Master_Kategori_Biaya_Import and "
            SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.id_rencana = '" & id_rencana & "' " 'and C.Flag_Gabungan = 'Y' "
            SQL = SQL & "group by b.Kode_Perusahaan, b.Kode_Master_Kategori_Biaya_Import, b.kode_kategori_biaya_import, c.Flag_Masuk_Jurnal "
            SQL = SQL & "order by b.Kode_Master_Kategori_Biaya_Import, b.kode_kategori_biaya_import"
            Using ds = BindingTrans(SQL)
                With ds.Tables("MyTable")
                    For index3 As Integer = 0 To .Rows.Count - 1

                        Dim cek As Boolean = False
                        SQL = "select a.No_Faktur, b.Kode_Stock_Owner, b.Kode_Barang, b.Jumlah,"
                        SQL = SQL & "c.Kode_Kategori_Biaya_Import, c.Biaya/Jumlah as Biaya, c.Biaya_AVG/Jumlah as Biaya_AVG, c.Flag_Average "
                        SQL = SQL & "from hpp_import a, detail_hpp_import b,detail_hpp_import_biaya c where "
                        SQL = SQL & "a.kode_perusahaan=b.Kode_Perusahaan and a.No_faktur=b.No_Faktur and b.no_faktur=c.No_Faktur "
                        SQL = SQL & "and b.Kode_Barang=c.Kode_Barang and b.Kode_stock_owner=c.Kode_stock_owner and "
                        SQL = SQL & "c.Kode_kategori_biaya_import ='" & .Rows(index3).Item("Kode_kategori_biaya_import") & "' and c.Kode_Barang='" & LvKdBarang & "' and a.Status Is null And a.id_rencana ='" & id_rencana & "' "
                        Using dr = OpenTrans(SQL)
                            If dr.Read Then


                                For index1 As Integer = 0 To Arr_Biaya_Import_Kategori.Count - 1


                                    If Arr_Biaya_Import_Kategori.Item(index1) = .Rows(index3).Item("Kode_kategori_biaya_import") Then

                                        Arr_Biaya_Import.Item(index1) += Val(HilangkanTanda(Format(dr("Biaya") * LvJumlah, "N0")))

                                        Biaya_Import_Total = Biaya_Import_Total + Val(HilangkanTanda(Format(dr("Biaya") * LvJumlah, "N0")))
                                        Biaya_Import_AVG = Biaya_Import_AVG + Val(HilangkanTanda(Format(dr("Biaya_AVG") * LvJumlah, "N0")))
                                        cek = True
                                    End If

                                Next

                                If cek = False Then
                                    Arr_Biaya_Import_Master.Add(.Rows(index3).Item("Kode_Master_Kategori_Biaya_Import"))
                                    Arr_Biaya_Import_Kategori.Add(dr("Kode_Kategori_Biaya_Import"))
                                    Arr_Biaya_Import.Add(Val(HilangkanTanda(Format(dr("Biaya") * LvJumlah, "N0"))))
                                    Arr_Akun1.Add(.Rows(index3).Item("Akun_1"))
                                    Arr_Akun2.Add(.Rows(index3).Item("Akun_2"))

                                    Biaya_Import_Total = Biaya_Import_Total + Val(HilangkanTanda(Format(dr("Biaya") * LvJumlah, "N0")))
                                    Biaya_Import_AVG = Biaya_Import_AVG + Val(HilangkanTanda(Format(dr("Biaya_AVG") * LvJumlah, "N0")))
                                End If

                            End If
                        End Using


                        ' ''SQL = "select a.No_Faktur, b.Kode_Stock_Owner, b.Kode_Barang, b.Jumlah, "
                        ' ''SQL = SQL & "c.Kode_Kategori_Biaya_Import, c.Biaya2/Jumlah as Biaya2, c.Biaya_AVG2/Jumlah as Biaya_AVG2, "
                        ' ''SQL = SQL & "c.Biayawetdry/Jumlah as Biayawetdry, c.Biayawetdry_AVG/Jumlah as Biayawetdry_AVG, c.Flag_Average "
                        ' ''SQL = SQL & "from hpp_import a, detail_hpp_import2 b,detail_hpp_import2_biaya c where "
                        ' ''SQL = SQL & "a.kode_perusahaan=b.Kode_Perusahaan and a.No_faktur=b.No_Faktur and b.no_faktur=c.No_Faktur "
                        ' ''SQL = SQL & "and b.Kode_Barang=c.Kode_Barang and b.Kode_stock_owner=c.Kode_stock_owner and b.Lokasi_Tujuan=c.lokasi_tujuan and "
                        ' ''SQL = SQL & "c.Kode_kategori_biaya_import ='" & .Rows(index3).Item("Kode_kategori_biaya_import") & "' and c.Kode_Barang='" & LvKdbarang & "' and c.Lokasi_tujuan ='" & LvSO & "'  and a.Status Is null And a.id_rencana ='" & id_rencana & "' "
                        ' ''Using dr = OpenTrans(SQL)
                        ' ''    If dr.Read Then


                        ' ''        For index1 As Integer = 0 To Arr_Biaya_Import_Kategori.Count - 1


                        ' ''            If Arr_Biaya_Import_Kategori.Item(index1) = .Rows(index3).Item("Kode_kategori_biaya_import") Then

                        ' ''                Arr_Biaya_Import.Item(index1) += Val(HilangkanTanda(Format((dr("Biaya2") + dr("Biayawetdry")) * LvJumlah, "N0")))

                        ' ''                Biaya_Import_Total = Biaya_Import_Total + Val(HilangkanTanda(Format((dr("Biaya2") + dr("Biayawetdry")) * LvJumlah, "N0")))
                        ' ''                Biaya_Import_AVG = Biaya_Import_AVG + Val(HilangkanTanda(Format((dr("Biaya_AVG2") + dr("Biayawetdry_AVG")) * LvJumlah, "N0")))
                        ' ''                cek = True
                        ' ''            End If

                        ' ''        Next

                        ' ''        If cek = False Then
                        ' ''            Arr_Biaya_Import_Master.Add(.Rows(index3).Item("Kode_Master_Kategori_Biaya_Import"))
                        ' ''            Arr_Biaya_Import_Kategori.Add(dr("Kode_Kategori_Biaya_Import"))
                        ' ''            Arr_Biaya_Import.Add(Val(HilangkanTanda(Format((dr("Biaya2") + dr("Biayawetdry")) * LvJumlah, "N0"))))
                        ' ''            Arr_Akun1.Add(.Rows(index3).Item("Akun_1"))
                        ' ''            Arr_Akun2.Add(.Rows(index3).Item("Akun_2"))

                        ' ''            Biaya_Import_Total = Biaya_Import_Total + Val(HilangkanTanda(Format((dr("Biaya2") + dr("Biayawetdry")) * LvJumlah, "N0")))
                        ' ''            Biaya_Import_AVG = Biaya_Import_AVG + Val(HilangkanTanda(Format((dr("Biaya_AVG2") + dr("Biayawetdry_AVG")) * LvJumlah, "N0")))
                        ' ''        End If

                        ' ''    End If
                        ' ''End Using


                    Next
                End With


            End Using


            SQL = "select b.Kode_Master_Kategori_Biaya_Import, b.kode_kategori_biaya_import, c.Flag_Masuk_Jurnal, B.Kode_Stock_Owner, "
            SQL = SQL & "round(sum(b.total), 0) as Biaya, "

            SQL = SQL & "isnull(("
            SQL = SQL & "select akun_1 from detail_account_master X where X.Kode_Perusahaan = b.Kode_Perusahaan and "
            SQL = SQL & "x.Kode_Master_Kategori_Biaya_import = b.Kode_Master_Kategori_Biaya_import and "
            SQL = SQL & "x.lokasi = '" & Lokasi_Jurnal & "' "
            SQL = SQL & "),0) as Akun_1, "

            SQL = SQL & "isnull(("
            SQL = SQL & "select akun_2 from detail_account_master X where X.Kode_Perusahaan = b.Kode_Perusahaan and "
            SQL = SQL & "x.Kode_Master_Kategori_Biaya_import = b.Kode_Master_Kategori_Biaya_import and "
            SQL = SQL & "x.lokasi = '" & Lokasi_Jurnal & "' "
            SQL = SQL & "),0) as Akun_2 "

            SQL = SQL & "from transaksi_biaya_import a, detail_transaksi_biaya_import b, Master_Kategori_Biaya_Import c where "
            SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan And "
            SQL = SQL & "a.no_faktur = b.no_faktur And a.status Is null and "
            SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Master_Kategori_Biaya_import = c.Kode_Master_Kategori_Biaya_Import and "
            SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.id_rencana = '" & id_rencana & "' and b.kode_stock_owner='" & Lokasi_Jurnal & "' " ' and C.Flag_Gabungan = 'T' "
            SQL = SQL & "group by b.Kode_Perusahaan, b.Kode_Master_Kategori_Biaya_Import, b.kode_kategori_biaya_import, c.Flag_Masuk_Jurnal, B.Kode_Stock_Owner "
            SQL = SQL & "order by b.Kode_Master_Kategori_Biaya_Import, b.kode_kategori_biaya_import"
            Using ds = BindingTrans(SQL)
                With ds.Tables("MyTable")
                    For index3 As Integer = 0 To .Rows.Count - 1

                        Dim cek As Boolean = False

                        SQL = "select a.No_Faktur, b.Kode_Stock_Owner, b.Kode_Barang, b.Jumlah, "
                        SQL = SQL & "c.Kode_Kategori_Biaya_Import, c.Biaya2/Jumlah as Biaya2, c.Biaya_AVG2/Jumlah as Biaya_AVG2, "
                        SQL = SQL & "c.Biayawetdry/Jumlah as Biayawetdry, c.Biayawetdry_AVG/Jumlah as Biayawetdry_AVG, c.Flag_Average "
                        SQL = SQL & "from hpp_import a, detail_hpp_import2 b,detail_hpp_import2_biaya c where "
                        SQL = SQL & "a.kode_perusahaan=b.Kode_Perusahaan and a.No_faktur=b.No_Faktur and b.no_faktur=c.No_Faktur "
                        SQL = SQL & "and b.Kode_Barang=c.Kode_Barang and b.Kode_stock_owner=c.Kode_stock_owner and b.Lokasi_Tujuan=c.lokasi_tujuan and "
                        SQL = SQL & "c.Kode_kategori_biaya_import ='" & .Rows(index3).Item("Kode_kategori_biaya_import") & "' and c.Kode_Barang='" & LvKdBarang & "' and c.Lokasi_tujuan ='" & Lokasi_Jurnal & "'  and a.Status Is null And a.id_rencana ='" & id_rencana & "' "
                        Using dr = OpenTrans(SQL)
                            If dr.Read Then


                                For index1 As Integer = 0 To Arr_Biaya_Bongkar_Import.Count - 1


                                    If Arr_Biaya_Bongkar_Import_Kategori.Item(index1) = .Rows(index3).Item("Kode_kategori_biaya_import") Then

                                        Arr_Biaya_Bongkar_Import.Item(index1) += Val(HilangkanTanda(Format((dr("Biaya2") + dr("Biayawetdry")) * LvJumlah, "N0")))

                                        Biaya_Import_Total = Biaya_Import_Total + Val(HilangkanTanda(Format((dr("Biaya2") + dr("Biayawetdry")) * LvJumlah, "N0")))
                                        Biaya_Import_AVG = Biaya_Import_AVG + Val(HilangkanTanda(Format((dr("Biaya_AVG2") + dr("Biayawetdry_AVG")) * LvJumlah, "N0")))
                                        cek = True
                                    End If

                                Next

                                If cek = False Then

                                    Arr_Biaya_Bongkar_Import_Master.Add(.Rows(index3).Item("Kode_Master_Kategori_Biaya_Import"))
                                    Arr_Biaya_Bongkar_Import_Kategori.Add(.Rows(index3).Item("Kode_Kategori_Biaya_Import"))
                                    Arr_Biaya_Bongkar_Import.Add(Val(HilangkanTanda(Format((dr("Biaya2") + dr("Biayawetdry")) * LvJumlah, "N0"))))
                                    Arr_Lokasi_Bongkar_Import.Add(Lokasi_Jurnal)
                                    Arr_Akun1_Bongkar.Add(.Rows(index3).Item("Akun_1"))
                                    Arr_Akun2_Bongkar.Add(.Rows(index3).Item("Akun_2"))

                                    Biaya_Import_Total = Biaya_Import_Total + Val(HilangkanTanda(Format((dr("Biaya2") + dr("Biayawetdry")) * LvJumlah, "N0")))
                                    Biaya_Import_AVG = Biaya_Import_AVG + Val(HilangkanTanda(Format((dr("Biaya_AVG2") + dr("Biayawetdry_AVG")) * LvJumlah, "N0")))
                                End If

                            End If
                        End Using


                    Next
                End With


            End Using



            SQL = "select b.Kode_Master_Kategori_Biaya_Import, b.kode_kategori_biaya_import, c.Flag_Masuk_Jurnal "

            SQL = SQL & "from transaksi_biaya_import3 a, detail_transaksi_biaya_import3 b, Master_Kategori_Biaya_Import c where "
            SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan And "
            SQL = SQL & "a.no_faktur = b.no_faktur And a.status Is null and "
            SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Master_Kategori_Biaya_import = c.Kode_Master_Kategori_Biaya_Import and "
            SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.id_rencana = '" & id_rencana & "' "
            SQL = SQL & "group by b.Kode_Perusahaan, b.Kode_Master_Kategori_Biaya_Import, b.kode_kategori_biaya_import, c.Flag_Masuk_Jurnal "
            SQL = SQL & "order by b.Kode_Master_Kategori_Biaya_Import, b.kode_kategori_biaya_import"
            Using ds = BindingTrans(SQL)
                With ds.Tables("MyTable")
                    For index3 As Integer = 0 To .Rows.Count - 1

                        SQL = "select a.No_Faktur, b.Kode_Stock_Owner, b.Kode_Barang, b.Jumlah, "
                        SQL = SQL & "c.Kode_Kategori_Biaya_Import, c.Biaya/Jumlah as Biaya, c.Biaya_AVG/Jumlah as Biaya_AVG, c.Flag_Average "
                        SQL = SQL & "from hpp_import a, detail_hpp_import b,detail_hpp_import_biaya c where "
                        SQL = SQL & "a.kode_perusahaan=b.Kode_Perusahaan and a.No_faktur=b.No_Faktur and b.no_faktur=c.No_Faktur "
                        SQL = SQL & "and b.Kode_Barang=c.Kode_Barang and b.Kode_stock_owner=c.Kode_stock_owner and "
                        SQL = SQL & "c.Kode_kategori_biaya_import ='" & .Rows(index3).Item("Kode_kategori_biaya_import") & "' and c.Kode_Barang='" & LvKdBarang & "'  and a.Status Is null And a.id_rencana ='" & id_rencana & "' "
                        Using dr = OpenTrans(SQL)
                            If dr.Read Then

                                freigt = freigt + Val(HilangkanTanda(Format(dr("Biaya") * LvJumlah, "N0")))
                                Biaya_Import_AVG = Biaya_Import_AVG + Val(HilangkanTanda(Format(dr("Biaya_AVG") * LvJumlah, "N0")))

                            End If
                        End Using


                    Next
                End With


            End Using
        Next


        pib = Val(HilangkanTanda(Format(pib, "N0")))

        pph_billing = Val(HilangkanTanda(Format(pph_billing, "N0")))

        Storage = Val(HilangkanTanda(Format(Storage, "N0")))

        Billing = Val(HilangkanTanda(Format(Billing, "N0")))

        Tot_Pot_Stock_IDR = Val(HilangkanTanda(Format(Tot_Pot_Stock_IDR, "N0")))

        Tdk_Pot_Stock_IDR = Val(HilangkanTanda(Format(Tdk_Pot_Stock_IDR, "N0")))

        Tdk_Pot_Stock_Hutang_IDR_Utama = Val(HilangkanTanda(Format(Tdk_Pot_Stock_Hutang_IDR_Utama, "N0")))

        Tdk_Pot_Stock_Hutang_IDR_Penolong = Val(HilangkanTanda(Format(Tdk_Pot_Stock_Hutang_IDR_Penolong, "N0")))

        pph_pakai_persentase = Val(HilangkanTanda(Format(pph_pakai_persentase, "N0")))

        Selisih_PO = Val(HilangkanTanda(Format(Selisih_PO, "N0")))

        Selisih_PO_Biaya = Val(HilangkanTanda(Format(Selisih_PO_Biaya, "N0")))

        'SQL = "select sum(Jumlah * Harga) as Biaya from Detail_Pembelian_New "
        'SQL = SQL & "where No_Faktur = '" & TxtFaktur.Text.Trim & "'"
        'Using Dr = OpenTrans(SQL)
        '    If Dr.Read Then
        '        Hutang_Dalam_Proses = Dr("Biaya")
        '    Else
        '        Dr.Close()
        '        CloseTrans()
        '        CloseConn()
        '        MessageBox.Show(" Nilai Hutang Dalam Proses Tidak ditemukan")
        '        Exit Sub
        '    End If
        'End Using






        'For index As Integer = 0 To Arr_Biaya_Import_Master.Count - 1

        '    SQL = "select* from Detail_Account_Master where "
        '    SQL = SQL & "Lokasi = '" & ComboBox4.Text & "' and Kode_master_Kategori_biaya_import = '" & Arr_Biaya_Import_Master.Item(index) & "' "
        '    SQL = SQL & "and Akun_1 ='" & Arr_Akun1.Item(index) & "'  and Akun_2 = '" & Arr_Akun2.Item(index) & "' and Kode_Perusahaan = '" & KodePerusahaan & "' "
        '    Using Dr = OpenTrans(SQL)
        '        If Not Dr.Read Then
        '            Dr.Close()
        '            CloseTrans()
        '            CloseConn()
        '            MessageBox.Show("Akun " & Arr_Biaya_Import_Master.Item(index) & " Tidak Ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '            Exit Sub
        '        End If
        '    End Using

        'Next

        'For index As Integer = 0 To Arr_Biaya_Bongkar_Import_Master.Count - 1

        '    SQL = "select* from Detail_Account_Master where "
        '    SQL = SQL & "Lokasi = '" & ComboBox4.Text & "' and Kode_master_Kategori_biaya_import = '" & Arr_Biaya_Bongkar_Import_Master.Item(index) & "' "
        '    SQL = SQL & "and Akun_1 ='" & Arr_Akun1_Bongkar.Item(index) & "'  and Akun_2 = '" & Arr_Akun2_Bongkar.Item(index) & "' and Kode_Perusahaan = '" & KodePerusahaan & "' "
        '    Using Dr = OpenTrans(SQL)
        '        If Not Dr.Read Then
        '            Dr.Close()
        '            CloseTrans()
        '            CloseConn()
        '            MessageBox.Show("Akun " & Arr_Biaya_Bongkar_Import_Master.Item(index) & " Tidak Ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '            Exit Sub
        '        End If
        '    End Using

        'Next

    End Sub
End Class
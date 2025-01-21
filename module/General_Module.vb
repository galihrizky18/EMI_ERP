
Imports System.Data.SqlClient
Imports System.Diagnostics.Eventing.Reader
Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions
Imports ZXing.QrCode

Module General_Module

    Public lblLoading As Label

    Public tgl_skg As DateTime
    Public Bahasa_Pilihan As String = "ID"
    Public fPurchaseRequisition As String = "PR"
    Public FRefraksi As String = "FR"
    Public fRequestMaterial As String = "RQM"
    Public fsb As String = "SB"
    Public fab As String = "FAB"
    Public fTransTimbanganKosong As String = ""
    Public IPPORT_IPCAM_1 As String = ""
    Public IPPORT_IPCAM_2 As String = ""
    Public User_IPCAM_1 As String = ""
    Public Pass_IPCAM_1 As String = ""
    Public User_IPCAM_2 As String = ""
    Public Pass_IPCAM_2 As String = ""
    Public Url_WA_Business As String = ""
    Public Token_WA_Business As String = ""
    Public Data_User_App2 As New ArrayList

    Public fPO_EMI As String = "PO"
    Public fLokasi_PO As String = "LP"
    Public Jumlah_Digit As Integer = 4
    Public fCHPP As String = "CP"

    Public fHProduksi As String = "HP"
    Public fPBB As String = "PBB"
    Public fProduksi As String = "PRD"
    Public fbb As String = "BB"
    Public fbs As String = "BRG"
    Public FRencanaProduksiBarang As String = "FRP"
    Public fBudgetingCostCenter As String = "BCC"


    '=====================
    '=    PRINTER NAME   =
    '=====================
    Public PrinterNameSPB As String = "EPSON LX-310 ESC/P"
    Public PrinterNameBPB As String = "EPSON LX-310 ESC/P"
    Public PrinterNameBuktiTimbang As String = "EPSON LX-310 ESC/P"
    Public PrinterName2 As String = "EPSON LX-310 ESC/P"
    Public PrinterName As String = "EPSON LX-310 ESC/P"
    Public PrinterNameTS As String = "EPSON LX-310 ESC/P"

    Public PrinterBarcode As String = "TSC TE210"
    Public PrinterQC As String = ""


    Public Cn As SqlConnection
    Public Cn1 As SqlClient.SqlConnection
    Public Cn2 As SqlClient.SqlConnection
    Public CnBizOff As SqlClient.SqlConnection
    Public Cmd As SqlClient.SqlCommand
    Public Cmd2 As SqlClient.SqlCommand
    Public CmdBizOff As SqlClient.SqlCommand
    Public Da As SqlClient.SqlDataAdapter
    Public Dr As SqlClient.SqlDataReader
    Public Ds As DataSet
    Public fJurnalPengajuan As String
    Public CDatabaseTetangga As String


    Public path_inv_pdf As String
    Public user_ftp As String
    Public pass_ftp As String
    Public url_ftp As String
    Public url_web_inv As String
    Public url_web_logo As String
    Public FBM As String

    Public FPaket As String = "PKT"
    Public Kd_Jurnal As String = "JE01"
    Public fMasterPenawaran As String = "PNW"
    Public FPembelian As String = "PM"

    Public FBMSementara As String = ""
    Public FReturMTSementara As String = "RMT"
    Public fPOChina = ""
    Public FLB As String = "LB-"
    Public setN As String = "N5"

    Public FSubmitPO As String = "SP"

    Public CRQuery, CRSF, CRRT, CRName As String
    Public StyleBB As String = "P"

    Public KodePerusahaan As String = "001"
    Public NamaPerusahaan As String '= "PT.TESTING"
    Public Ket_Lokasi_HO As String = "HEAD OFFICE"
    Public Ket_User_HO As String = "HEAD OFFICE"
    Public nilai_penjualan As Integer = 100000

    Public Tanggal_Sekarang As DateTime

    Public CServer As String = "team311.dyndns.info"
    Public Const CDatabase As String = "emi_tm_demo"
    'Public Const CDatabase As String = "grahaweb_tm"
    Public Const CUserId As String = "sqlserver"
    Public Const CPassword As String = "**H0L4H0L4hola**"

    'Public CServer As String = "35.240.215.51,59114\team"
    ''Public Const CDatabaseSQL As String = "tes_absen"
    'Public Const CDatabase As String = "grahaweb_tm"
    'Public Const CUserId As String = "sa2"
    'Public Const CPassword As String = "P@ssword99000"

    Public UserID As String = "Art Di"
    'Public UserID As String = "BAYA"
    'Public UserID As String = "garix"
    'Public UserID As String = "emi"
    'Public Lokasi As String = "HEAD OFFICE"
    Public MainMenuID As String = ""

    Public UserName As String '= "devi"
    Public UserLevel As String
    Public Judul As String = "Perhatian !!!"
    Public FHPP As String

    Public SQL As String
    Public xSplit() As String
    Public xSplit2() As String
    '---------------------------------------------
    Public KatCustomer As String '= "00001"
    Public KodeCustomer As String '= "00001"
    Public NamaCustomer As String
    Public DiscP As Integer = 0
    Public DiscRP As Double = 0
    Public mata_uang As String = "koma"
    Public _gabung As String = "T"
    Public Pemilik As String = "1"
    Public JumlahDigit As Integer = 4
    Public JumlahDigit2 As Integer = 3
    Public f1 As String = "SK"
    Public f2 As String = "JL-"
    Public f3 As String = "FB"
    Public f4 As String = "PO-"

    Public fj As String = "EV"
    Public fjbb As String = "EV"
    Public fjMK As String = "EV"
    Public fjMKbb As String = "EV"
    Public frtr_jual As String = "RT"

    Public fValPenj As String = "PJ"
    Public fTransFormula As String = "FRM"
    Public fTransFormulaBinding As String = "FRMB"
    Public fValPemb As String = "PB"
    Public TBiaya_Import As String = "BI"
    Public fValPenjTunai As String = "PT"
    Public fDONew As String = "DE"
    Public fValDiskonCash As String = "DC"
    Public fRebate As String = "RB"
    Public selisihjam As Integer = "0"

    Public fValDOTunai As String = "DT"
    Public fValDOKredit As String = "DK"
    Public fValBiaya As String = "VL"

    Public fFlever As String = "FL"

    Public fsalon As String = "SL"

    Public fOpname As String = "OP"
    Public fDO As String = "DR"
    Public fDONEW2 As String = "DN"

    Public fb As String = "PM-"
    Public fbbb As String = "PMB-"
    Public fbMK As String = "PMA-"
    Public fbMKbb As String = "PMA-"
    Public fbSementara As String = "XM-"
    Public TC As String = "TC-"

    Public FTS As String = "TS-"
    Public FTS_IN As String = "In-"
    Public FAdj As String = "AJ-"
    Public FTQ As String = "TQ-"
    Public FTFP As String = "TPQ-"
    Public fPOCab As String = "PC"
    Public fPOCabOtomatis As String = "PX"
    Public fPermintaanKeluar As String = "PK"
    Public fPermintaanAgency As String = "PY"
    Public frab As String = "RB"
    Public fpop As String = "PO"
    Public fRBG As String = "RG"
    Public fPenj_pry As String = "PB"

    Public fjDaftar As String = "DF-"
    Public fGaji As String = "GJ-"
    Public fAsuransi As String = "RG-"

    Public Rb As String = "RB"
    Public Rj As String = "RJ"
    Public RbMK As String = "RBA-"
    Public RjMK As String = "RJA-"

    Public Rj_DO As String = "RD"
    Public Rj_DO_S As String = "RS"

    Public Adj As String = "MS-"
    Public AdjMK As String = "MA-"

    Public FPO As String = "PO-"
    Public TT As String = "TT-"

    Public fpromo As String = "PR-"
    Public fpeng As String = "KL-"
    Public fbTagihan As String = "TH-"
    Public FSJLN As String = "RT-"

    Public fUM As String = "UM"
    Public fUK As String = "UK"

    Public fJU As String = "JZ"
    Public fJurnalJual As String = "J"
    '=================
    Public Jatuh_Tempo_Pembelian As Integer = 20
    Public Jatuh_Tempo_Penjualan As Integer = 20
    Public No_Fak As String = ""
    Public Jenis As String = ""
    Public Field As String = ""

    Public JmlBrg As Integer = 11
    Public JmlBrgReturBeli As Integer = 16
    Public JmlBrgPO As Integer = 16
    Public desimal As String = "Y"
    Public BolehNegatif As Integer = 0

    Public jumlah_tampil_barang As String = " TOP(200) "
    Public format_tgl As String = "yyyy-MM-dd 23:59:59" '"dd-MM-yyyy"
    Public format_tgl2 As String = "yyyy-MM-dd"
    Public EditHJ As String = "T"
    Public PPN As Double = 10
    Public list_pembeda As String = "'MS', 'MK', 'Unikey', 'Prohex'"
    Public pakai_point As String = "Y"

    Public Lokasi_Import As String = "GUDANG A"
    Public HPP As String = "HP-"
    Public FTB As String = "TB-"
    Public X_Kas As String = ""
    Public X_Modal As String = ""
    Public X_Persediaan As String = ""
    Public X_Hutang As String = ""
    Public X_Diskon_Pembelian As String = ""
    Public X_HPP As String = "100125"
    Public X_Penjualan As String = ""
    Public X_Diskon_Penjualan As String = ""
    Public X_Piutang As String = ""
    Public X_PPN_Pembelian As String = ""
    Public X_PPN_Penjualan As String = ""
    Public X_Pendapatan_Salon As String = ""
    Public X_Pendapatan_Member As String = ""
    Public X_Pending_Persediaan As String = ""
    Public X_Retur_Penjualan As String = ""
    Public X_Piutang_Cabang_Sendiri As String = ""
    Public X_Biaya_Flever As String = ""
    Public X_Penjualan_Tk_Sdr As String = ""
    Public X_Penjualan_Sementara_Tk_Sdr As String = ""
    Public X_Penjualan_Sementara_Agency As String = ""
    Public X_HPP_Tk_Sdr As String = ""
    Public X_HPP_Sementara_Tk_Sdr As String = ""
    Public X_PPN_Penjualan_Sementara As String = ""
    Public X_Piutang_Sementara_Cabang_Sendiri As String = ""
    Public X_Persediaan_Sementara As String = ""
    Public X_Piutang_Sementara_Agency As String = ""
    Public X_Penjualan_Agency As String = ""
    Public X_PPN_Penjualan_Sementara_Agency As String = ""
    Public X_Piutang_Agency As String = ""
    Public X_Persediaan_Sementara_Agency As String = ""
    Public X_HPP_Agency As String = ""
    Public X_HPP_Sementara_Agency As String = ""
    Public X_Penjualan_Lainnya As String = ""
    Public X_Retur_Jual_Lainnya As String = ""
    Public X_Persediaan_Brg_Blm_Krm As String = ""
    Public X_Brg_Blm_Krm As String = ""
    Public X_Pelunasan_Dimuka As String = ""
    Public X_Uang_Masuk_Global As String = ""
    Public X_Biaya_EDC As String = ""
    Public X_Hutang_Sementara As String = ""
    Public X_PPN_Pembelian_Sementara As String = ""
    Public H_Penjualan_Sementara_Tk_Sdr As String = ""
    Public H_PPN_Penjualan_Sementara As String = ""
    Public H_Piutang_Sementara_Cabang_Sendiri As String = ""
    Public H_Piutang_Cabang_Sendiri As String = ""
    Public H_Penjualan_Tk_Sdr As String = ""
    Public H_PPN_Penjualan As String = ""
    Public H_Persediaan_Sementara As String = ""
    Public H_HPP_Tk_Sdr As String = ""
    Public H_HPP_Sementara_Tk_Sdr As String = ""
    Public H_Persediaan As String = ""

    Public fJU2 As String = ""
    Public fJU3 As String = ""
    Public fJU4 As String = ""
    Public fJU_Tetangga As String = ""
    Public fJU_Tetangga2 As String = ""
    Public fJU_Tetangga3 As String = ""

    Public no_faktur_1 As String = ""
    Public no_faktur_2 As String = ""
    Public no_faktur_3 As String = ""
    Public no_faktur_4 As String = ""

    Public KodeProyek As String = "0001"

    Public Tanda_SN As String = "#"
    Public Metode As String = " ASC"

    Public UseDotmatrix As String = "Y"

    Public mm_email As String = ""
    Public mm_email_pass As String = ""
    Public mm_email_port As String = ""
    Public mm_email_smtp As String = ""

    Public fDOOPM As String = ""
    Public msg_simpan_do As String = ""
    Public fj_white As String = ""

    Public fNB As String = "NB"
    Public fNBS As String = "NS"
    Public CnCSR As SqlClient.SqlConnection
    Public CmdCSR As SqlClient.SqlCommand

    Public Function JSON_Notif_Karyawan_Baru(Tes1 As String, Tes2 As String, Tes3 As String, Tes4 As String, Tes5 As String) As String

        Return ""

    End Function

    'Public _UserRole As String
    'Public UserID As String
    'Public UserName As String

    'Public Function SimpanPenjualanHariIni(ByVal telpon As String, ByVal keterangan As String, ByVal dari As String) As String
    '    Dim MMM As String = ""
    '    MMM = "insert into notifikasi_penjualan(kode_perusahaan,telpon,keterangan,tgl,jam,dari, "
    '    MMM = MMM & "User_Id) values("
    '    MMM = MMM & "'" & KodePerusahaan & "', '" & telpon & "' , '" & keterangan & "',  "
    '    MMM = MMM & "'" & Format(CDate(fmenu.ToolStripStatusLabel3.Text), "yyyy-MM-dd HH:mm:ss") & "', "
    '    MMM = MMM & "'" & Format(CDate(fmenu.ToolStripStatusLabel3.Text), "HH:mm:ss") & "', "
    '    MMM = MMM & "'" & dari & "', '" & UserID & "')"

    '    Return MMM
    'End Function

    Public Sub OpenConnCsr()
        General_Class.SetConnectionString(CServer, CDatabase, CUserId, CPassword)
        Cn = New SqlClient.SqlConnection
        Cn.ConnectionString = "Data Source=" & CServer & ";Initial Catalog=" & CDatabase &
                        ";integrated security = true"
        Cn.Open()
        Cmd = New SqlClient.SqlCommand
        Cmd.Connection = Cn
        Cmd.CommandType = CommandType.Text
        Cmd.CommandTimeout = 300000
    End Sub

    Public Sub OpenConnBizOff()
        General_Class.SetConnectionString(CServer, CDatabase, CUserId, CPassword)
        CnBizOff = New SqlClient.SqlConnection
        CnBizOff.ConnectionString = "Data Source=" & CServer & ";Initial Catalog=" & CDatabase &
                        ";User Id=" & CUserId & ";Password=" & CPassword & ";" &
                        ";Connect Timeout=800;Max Pool Size=400"
        CnBizOff.Open()
        CmdBizOff = New SqlClient.SqlCommand
        CmdBizOff.Connection = CnBizOff
        CmdBizOff.CommandType = CommandType.Text
        CmdBizOff.CommandTimeout = 300000
    End Sub

    Public Sub CloseConnCsR()
        If Not Cn Is Nothing Then
            Cn.Close()
            Cn = Nothing
        End If
    End Sub

    Public Function OpenTransCsR(ByVal Query As String) As SqlClient.SqlDataReader
        Cmd.CommandText = Query
        Return Cmd.ExecuteReader
    End Function

    Public Sub CloseTransCsr()
        If Not (Cmd.Transaction Is Nothing) Then
            Cmd.Transaction.Rollback()
        End If
    End Sub

    Public Sub ExecuteTransCsr(ByVal Query As String)
        Cmd.CommandText = Query
        Cmd.ExecuteNonQuery()
        'Cmd = Nothing
    End Sub

    Public Function Simpan_Status_Rencana_Order(ByVal _id_rencana As Integer, ByVal _status As String, ByVal _no_transaksi As String) As String
        Dim qry As String = ""
        qry = "insert into rencana_order_status(kode_perusahaan, id_rencana, hasil, no_transaksi) values("
        qry = qry & "'" & KodePerusahaan & "', '" & _id_rencana & "', '" & _status & "', "
        qry = qry & "'" & _no_transaksi & "')"
        Return qry
    End Function

    Public Function Hitung_Subtotal(ByVal _hrg As Double, ByVal _disc As Double, ByVal _jml As Double) As Double
        Return (Val(HilangkanTanda(Format((_hrg - (_hrg * _disc / 100)), "N0")))) * _jml
    End Function

    Public fValDeclare As String = "PD"
    Public fValDeclareSmntr As String = "PS"

    Public Function Cek_Flag(ByVal KP As String, ByVal Jns As String, ByVal Lks As String) As ArrayList
        Dim Flg As String = ""
        Dim vl As String = ""

        SQL = "select flag_opname, " & Jns & " from stock_owner "
        SQL = SQL & "where kode_perusahaan = '" & KP & "' and "
        SQL = SQL & "kode_stock_owner = '" & Lks & "'" '
        Using Dr = OpenTrans(SQL)
            If Dr.Read Then
                If Dr("flag_opname") = "Y" Then
                    'If Dr(Jns) = 0 Then
                    '    Flg = "X"
                    '    vl = "X"

                    'ElseIf Dr(Jns) > 0 Then
                    '    SQL = "update stock_owner set " & Jns & " = " & Jns & " - 1 "
                    '    SQL = SQL & "where kode_perusahaan = '" & KP & "' and "
                    '    SQL = SQL & "kode_stock_owner = '" & Lks & "'"
                    '    Dr.Close()
                    '    ExecuteTrans(SQL)

                    Flg = "Flag_Opm"
                    vl = "'Y'"
                    'Else
                    '    Flg = "X"
                    '    vl = "X"
                    'End If
                Else
                    Flg = "Flag_Opm"
                    vl = "Null"
                End If
            End If

        End Using

        Dim Data_Flag As New ArrayList
        Data_Flag.Clear()
        Data_Flag.Add(Flg)
        Data_Flag.Add(vl)

        Return Data_Flag
    End Function

    Public err_msg_opname As String = "error"

    Public DB_Agency = ""
    Public DB_Tetangga = ""

    Public Function tgl(ByVal x As String) As String
        Return x '"convert(varchar(10), " & x & ", 105)"
    End Function

    '-------------------------------------------------
    Public Sub OpenConn()
        General_Class.SetConnectionString(CServer, CDatabase, CUserId, CPassword)
        Cn = New SqlClient.SqlConnection
        Cn.ConnectionString = "Data Source=" & CServer & ";Initial Catalog=" & CDatabase &
                        ";User Id=" & CUserId & ";Password=" & CPassword & ";" &
                        ";Connect Timeout=30;Max Pool Size=400"
        Cn.Open()
        Cmd = New SqlClient.SqlCommand
        Cmd.Connection = Cn
        Cmd.CommandType = CommandType.Text
        Cmd.CommandTimeout = 300000
    End Sub

    Public Sub OpenConn2(ByVal db As String)
        Cn2 = New SqlClient.SqlConnection
        Cn2.ConnectionString = "Data Source=" & CServer & ";Initial Catalog=" & db &
                        ";User Id=" & CUserId & ";Password=" & CPassword & ";" &
                        ";Connect Timeout=30;Max Pool Size=400"
        Cn2.Open()
        Cmd2 = New SqlClient.SqlCommand
        Cmd2.Connection = Cn2
        Cmd2.CommandType = CommandType.Text
    End Sub

    Public Sub CloseConn()
        If Not Cn Is Nothing Then
            Cn.Close()
            Cn = Nothing
        End If
    End Sub

    Public Sub CloseConnBizOff()
        If Not Cn Is Nothing Then
            CnBizOff.Close()
            CnBizOff = Nothing
        End If
    End Sub

    Public Sub CloseConn2()
        If Not Cn2 Is Nothing Then
            Cn2.Close()
            Cn2 = Nothing
        End If
    End Sub

    Public Sub OpenConnRestore()
        General_Class.SetConnectionString(CServer, CDatabase, CUserId, CPassword)
        Cn1 = New SqlClient.SqlConnection
        Cn1.ConnectionString = "Data Source=" & CServer & ";Initial Catalog=Master" &
                        ";User Id=" & CUserId & ";Password=" & CPassword & ";" &
                        ";Connect Timeout=30;Max Pool Size=400"
        Cn1.Open()
        Cmd = New SqlClient.SqlCommand
        Cmd.Connection = Cn1
        Cmd.CommandType = CommandType.Text
    End Sub

    Public Sub CloseConnRestore()
        If Not Cn1 Is Nothing Then
            Cn1.Close()
            Cn1 = Nothing
        End If
    End Sub

    Public Sub ExecuteTrans(ByVal Query As String)
        Cmd.CommandText = Query
        Cmd.ExecuteNonQuery()
        'Cmd = Nothing
    End Sub

    Public Sub ExecuteTransBizOff(ByVal Query As String)
        CmdBizOff.CommandText = Query
        CmdBizOff.ExecuteNonQuery()
        'Cmd = Nothing
    End Sub

    Public Sub ExecuteRestore(ByVal Query As String)
        Try
            Cmd = New SqlClient.SqlCommand
            Cmd.Connection = Cn1
            Cmd.CommandType = CommandType.Text
            Cmd.CommandText = Query
            Cmd.ExecuteNonQuery()
            Cmd = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Function OpenTrans(ByVal Query As String) As SqlClient.SqlDataReader
        Cmd.CommandText = Query
        Return Cmd.ExecuteReader
    End Function

    Public Function OpenTransBizOff(ByVal Query As String) As SqlClient.SqlDataReader
        CmdBizOff.CommandText = Query
        Return CmdBizOff.ExecuteReader
    End Function

    Public Function OpenTrans2(ByVal Query As String) As SqlClient.SqlDataReader
        Cmd2.CommandText = Query
        Return Cmd2.ExecuteReader
    End Function

    Public Sub CloseTrans()
        If Not (Cmd.Transaction Is Nothing) Then
            Cmd.Transaction.Rollback()
        End If
    End Sub

    Public Sub CloseTransBizOff()
        If Not (CmdBizOff.Transaction Is Nothing) Then
            CmdBizOff.Transaction.Rollback()
        End If
    End Sub

    Public Sub CloseDr()
        If Not Dr Is Nothing Then
            Dr.Close()
            Dr = Nothing
        End If
    End Sub

    Public Sub Faktur_1(ByVal lks As String, ByVal tanggal As Date)
        Dim insial_faktur As String = ""
        SQL = "select inisial_format_1 from stock_owner where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & lks & "'"
        Using Dr = OpenTrans(SQL)
            If Dr.Read Then
                insial_faktur = Dr("inisial_format_1")
            End If
        End Using
        no_faktur_1 = f1 & insial_faktur & Format(tanggal, "yyMMdd")
        '& _
        '                                     General_Class.Get_Last_Number2("format_faktur", "format_faktur_1", JumlahDigit, _
        '                                     "Kode_perusahaan", KodePerusahaan, _
        '                                     "And", "substring(format_faktur_1,1," & Len(f1) + Len(insial_faktur) + 8 & ")", f1 & insial_faktur & Format(tanggal, "yyyyMMdd"))

        SQL = "select lokasi,format_faktur_1 from format_faktur where kode_perusahaan = '" & KodePerusahaan & "' and lokasi = '" & lks & "'"
        Using Dr = OpenTrans(SQL)
            If Dr.Read Then
                Dr.Close()
                SQL = "update format_faktur set format_faktur_1 = '" & no_faktur_1 & "' where kode_perusahaan = '" & KodePerusahaan & "' and lokasi = '" & lks & "'"
                ExecuteTrans(SQL)
            Else
                Dr.Close()
                SQL = "insert into format_faktur(kode_perusahaan,lokasi,format_faktur_1) "
                SQL = SQL & "values('" & KodePerusahaan & "','" & lks & "', '" & no_faktur_1 & "')"
                ExecuteTrans(SQL)
            End If
        End Using
    End Sub

    Public Sub Faktur_2(ByVal lks As String, ByVal tanggal As Date)
        Dim insial_faktur As String = ""
        'SQL = "select inisial_format_2 from stock_owner where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & lks & "'"
        'Using Dr = OpenTrans(SQL)
        '    If Dr.Read Then
        '        insial_faktur = Dr("inisial_format_1")
        '    End If
        'End Using
        no_faktur_2 = f2 & Format(tanggal, "yyMMdd") &
                                     General_Class.Get_Last_Number2("format_faktur", "format_faktur_2", JumlahDigit2,
                                     "Kode_perusahaan", KodePerusahaan,
                                     "And", "substring(format_faktur_2,1," & Len(f2) + 6 & ")", "JL-" & Format(tanggal, "yyMMdd"))

        SQL = "select lokasi,format_faktur_2 from format_faktur where kode_perusahaan = '" & KodePerusahaan & "' and lokasi = '" & lks & "'"
        Using Dr = OpenTrans(SQL)
            If Dr.Read Then
                Dr.Close()
                SQL = "update format_faktur set format_faktur_2 = '" & no_faktur_2 & "' where kode_perusahaan = '" & KodePerusahaan & "' and lokasi = '" & lks & "'"
                ExecuteTrans(SQL)
            Else
                Dr.Close()
                SQL = "insert into format_faktur(kode_perusahaan,lokasi,format_faktur_2) "
                SQL = SQL & "values('" & KodePerusahaan & "','" & lks & "', '" & no_faktur_2 & "')"
                ExecuteTrans(SQL)
            End If
        End Using
    End Sub

    Public Sub Faktur_3(ByVal lks As String, ByVal tanggal As Date)
        Dim insial_faktur As String = ""
        'SQL = "select inisial_format_2 from stock_owner where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & lks & "'"
        'Using Dr = OpenTrans(SQL)
        '    If Dr.Read Then
        '        insial_faktur = Dr("inisial_format_1")
        '    End If
        'End Using
        no_faktur_3 = f3 & Format(tanggal, "yyyyMMdd") &
                                     General_Class.Get_Last_Number2("format_faktur", "format_faktur_3", JumlahDigit2,
                                     "Kode_perusahaan", KodePerusahaan,
                                     "And", "substring(format_faktur_3,1," & Len(f3) + 8 & ")", f3 & Format(tanggal, "yyyyMMdd"))

        SQL = "select lokasi,format_faktur_3 from format_faktur where kode_perusahaan = '" & KodePerusahaan & "' and lokasi = '" & lks & "'"
        Using Dr = OpenTrans(SQL)
            If Dr.Read Then
                Dr.Close()
                SQL = "update format_faktur set format_faktur_3 = '" & no_faktur_3 & "' where kode_perusahaan = '" & KodePerusahaan & "' and lokasi = '" & lks & "'"
                ExecuteTrans(SQL)
            Else
                Dr.Close()
                SQL = "insert into format_faktur(kode_perusahaan,lokasi,format_faktur_3) "
                SQL = SQL & "values('" & KodePerusahaan & "','" & lks & "', '" & no_faktur_3 & "')"
                ExecuteTrans(SQL)
            End If
        End Using
    End Sub

    Public Sub Faktur_4(ByVal lks As String, ByVal tanggal As Date)
        Dim insial_faktur As String = ""
        SQL = "select inisial_format_4 from stock_owner where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & lks & "'"
        Using Dr = OpenTrans(SQL)
            If Dr.Read Then
                insial_faktur = Dr("inisial_format_4")
            End If
        End Using
        no_faktur_4 = f4 & Format(tanggal, "ddMMyyyy") & "-" & insial_faktur
        'General_Class.Get_Last_Number2("format_faktur", "format_faktur_3", JumlahDigit2, _
        '"Kode_perusahaan", KodePerusahaan, _
        '"And", "substring(format_faktur_3,1," & Len(f3) + 8 & ")", f3 & Format(tanggal, "yyyyMMdd"))

        SQL = "select lokasi,format_faktur_4 from format_faktur where kode_perusahaan = '" & KodePerusahaan & "' and lokasi = '" & lks & "'"
        Using Dr = OpenTrans(SQL)
            If Dr.Read Then
                Dr.Close()
                SQL = "update format_faktur set format_faktur_4 = '" & no_faktur_4 & "' where kode_perusahaan = '" & KodePerusahaan & "' and lokasi = '" & lks & "'"
                ExecuteTrans(SQL)
            Else
                Dr.Close()
                SQL = "insert into format_faktur(kode_perusahaan,lokasi,format_faktur_4) "
                SQL = SQL & "values('" & KodePerusahaan & "','" & lks & "', '" & no_faktur_4 & "')"
                ExecuteTrans(SQL)
            End If
        End Using
    End Sub

    Public Function HasilDiskon(ByVal hrg As Double, ByVal jml As Integer, ByVal DiscPersen As Integer, ByVal DiscRp As Integer, ByVal x As String) As Double
        If x = "PERSEN" Then
            Return (hrg * jml) - (hrg * jml * DiscPersen / 100)
        ElseIf x = "RP" Then
            Return (hrg - DiscRp) * jml
        ElseIf x = "TPERSEN" Then
            Return (hrg * DiscPersen / 100)
        ElseIf x = "TRP" Then
            Return hrg - DiscRp
        End If
    End Function

    Public Function TampilanDesimal(ByVal obj As Object) As String
        If desimal = "Y" Then
            Return Format(obj, "Standard")
        Else
            Return Format(obj, "N0")
        End If
    End Function

    Public Function BindingTrans(ByVal Query As String) As DataSet
        Cmd.CommandText = Query
        Da = New SqlClient.SqlDataAdapter
        Da.SelectCommand = Cmd
        BindingTrans = New DataSet
        Da.Fill(BindingTrans, "MyTable")
    End Function

    Public Function BindingTransBizOff(ByVal Query As String) As DataSet
        CmdBizOff.CommandText = Query
        Da = New SqlClient.SqlDataAdapter
        Da.SelectCommand = CmdBizOff
        BindingTransBizOff = New DataSet
        Da.Fill(BindingTransBizOff, "MyTable")
    End Function

    Public Function ChangeDotToNothing(ByVal x As String) As Double
        ChangeDotToNothing = Val(Replace(x, ".", ""))
    End Function

    Public Function ChangeCommaToDot(ByVal x As String) As String
        ChangeCommaToDot = Replace(x, ",", ".")
    End Function

    Public Function HilangkanTanda(ByVal x As String) As String
        Dim hasil As String = ""
        If mata_uang.ToUpper = "KOMA" Then
            hasil = Replace(x, ",", "")
        ElseIf mata_uang.ToUpper = "TITIK" Then
            hasil = Replace(x, ".", "")
        End If
        Return hasil
    End Function

    Public Function GetBulanMinSatu(ByVal Tanggal As Date) As String
        If Month(Tanggal) = 1 Then
            Return "12" & Year(Tanggal) - 1
        Else
            Return Month(Tanggal) - 1 & Year(Tanggal)
        End If
    End Function

    Public Function GetBulanMinSatu(ByVal Bulan As Integer, ByVal Tahun As String) As String
        If Bulan = 1 Then
            Return "12" & Val(Tahun) - 1
        Else
            Return Bulan - 1 & Tahun
        End If
    End Function

    Public Function Ganti(ByVal x As String) As Double
        Ganti = Val(Replace(x, ".", ""))
    End Function

    Public Function Ganti_lama(ByVal x As String) As String
        Ganti_lama = Val(Replace(x, ".", ""))
        'Ganti = Replace(x, "", "")
    End Function

    Public Function Ubah(ByVal x As String) As String
        Ubah = Replace(x, ",", ".")
    End Function


    Public Function CekButtonRole(ByVal namabutton As String) As String
        SQL = "select kode_perusahaan from role_button where "
        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and userid = '" & UserID & "' and "
        SQL = SQL & "buttonname = '" & namabutton & "'"
        Using Dr = OpenTrans(SQL)
            If Not (Dr.Read) Then
                Dr.Close()
                CloseTrans()
                CloseConn()
                Return "T"
            Else
                Return "Y"
            End If
        End Using
    End Function

    Public Function CekKotaRole() As String
        Dim xKota As String = ""

        SQL = "select kode_kota from role_kota where "
        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and userid = '" & UserID & "' order by kode_kota"
        Using Dr = OpenTrans(SQL)
            Do While Dr.Read
                xKota = xKota & Dr("kode_kota") & ", "
            Loop
        End Using

        xKota = Strings.Left(xKota, Len(xKota) - 2)

        Return xKota
    End Function

    Public Sub Get_Data_Acc()
        SQL = "select * from stock_owner where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & Lokasi & "'"
        Using Dr = OpenTrans(SQL)
            If Dr.Read Then
                X_Kas = Dr("kas")
                X_Modal = Dr("modal")

                X_Persediaan = Dr("persediaan")
                X_Persediaan_Sementara = Dr("Persediaan_Sementara")
                X_Persediaan_Sementara_Agency = Dr("Persediaan_Sementara_Agency")

                X_Hutang = Dr("hutang")
                X_Diskon_Pembelian = Dr("Diskon_Pembelian")

                X_HPP = Dr("hpp")
                X_HPP_Tk_Sdr = Dr("HPP_Tk_Sdr")
                X_HPP_Sementara_Tk_Sdr = Dr("HPP_Sementara_Tk_Sdr")
                X_HPP_Agency = Dr("HPP_Agency")
                X_HPP_Sementara_Agency = Dr("HPP_Sementara_Agency")

                X_Penjualan = Dr("Penjualan")
                X_Penjualan_Tk_Sdr = Dr("Penjualan_Tk_Sdr")
                X_Penjualan_Agency = Dr("Penjualan_Agency")
                X_Penjualan_Sementara_Tk_Sdr = Dr("Penjualan_Sementara_Tk_Sdr")
                X_Penjualan_Sementara_Agency = Dr("Penjualan_Sementara_Agency")

                X_Diskon_Penjualan = Dr("diskon_penjualan")

                X_PPN_Pembelian = Dr("ppn_pembelian")
                X_PPN_Penjualan = Dr("ppn_penjualan")
                X_PPN_Penjualan_Sementara = Dr("PPN_Penjualan_Sementara")
                X_PPN_Penjualan_Sementara_Agency = Dr("PPN_Penjualan_Sementara_Agency")

                X_Pendapatan_Salon = Dr("pendapatan_salon")
                X_Pendapatan_Member = Dr("pendapatan_member")
                X_Pending_Persediaan = Dr("pending_persediaan")
                X_Retur_Penjualan = Dr("retur_penjualan")

                X_Piutang = Dr("piutang")
                X_Piutang_Cabang_Sendiri = Dr("piutang_cabang_sendiri")
                X_Piutang_Agency = Dr("piutang_agency")

                X_Piutang_Sementara_Cabang_Sendiri = Dr("Piutang_Sementara_Cabang_Sendiri")
                X_Piutang_Sementara_Agency = Dr("Piutang_Sementara_Agency")

                X_Biaya_Flever = Dr("biaya_flever")
                X_Penjualan_Lainnya = Dr("penjualan_lainnya")
                X_Retur_Jual_Lainnya = Dr("retur_lainnya")

                X_Persediaan_Brg_Blm_Krm = Dr("Persediaan_Brg_Blm_Krm")
                X_Brg_Blm_Krm = Dr("Brg_Blm_Krm")
                X_Pelunasan_Dimuka = Dr("pelunasan_dimuka")
                X_Uang_Masuk_Global = Dr("Uang_Masuk_Global")

            Else
                X_Kas = ""
                X_Modal = ""
                X_Persediaan = ""
                X_Persediaan_Sementara = ""
                X_Persediaan_Sementara_Agency = ""

                X_Hutang = ""
                X_Diskon_Pembelian = ""

                X_HPP = ""
                X_HPP_Tk_Sdr = ""
                X_HPP_Sementara_Tk_Sdr = ""
                X_HPP_Agency = ""
                X_HPP_Sementara_Agency = ""

                X_Penjualan = ""
                X_Penjualan_Tk_Sdr = ""
                X_Penjualan_Agency = ""
                X_Penjualan_Sementara_Tk_Sdr = ""
                X_Penjualan_Sementara_Agency = ""

                X_Diskon_Penjualan = ""

                X_PPN_Pembelian = ""
                X_PPN_Penjualan = ""
                X_PPN_Penjualan_Sementara = ""
                X_PPN_Penjualan_Sementara_Agency = ""

                X_Pendapatan_Salon = ""
                X_Pendapatan_Member = ""
                X_Pending_Persediaan = ""
                X_Retur_Penjualan = ""

                X_Piutang = ""
                X_Piutang_Cabang_Sendiri = ""
                X_Piutang_Agency = ""

                X_Piutang_Sementara_Cabang_Sendiri = ""
                X_Piutang_Sementara_Agency = ""

                X_Biaya_Flever = ""
                X_Penjualan_Lainnya = ""
                X_Retur_Jual_Lainnya = ""

                X_Persediaan_Brg_Blm_Krm = ""
                X_Brg_Blm_Krm = ""
                X_Pelunasan_Dimuka = ""
                X_Uang_Masuk_Global = ""

            End If
        End Using
    End Sub

    Public Sub Get_Data_Acc_Lokasi(ByVal lks As String)
        SQL = "select * from stock_owner where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & lks & "'"
        Using Dr = OpenTrans(SQL)
            If Dr.Read Then
                X_Kas = Dr("kas")
                X_Modal = Dr("modal")

                X_Persediaan = Dr("persediaan")
                X_Persediaan_Sementara = Dr("Persediaan_Sementara")
                X_Persediaan_Sementara_Agency = Dr("Persediaan_Sementara_Agency")

                X_Hutang = Dr("hutang")
                X_Diskon_Pembelian = Dr("Diskon_Pembelian")

                X_HPP = Dr("hpp")
                X_HPP_Tk_Sdr = Dr("HPP_Tk_Sdr")
                X_HPP_Sementara_Tk_Sdr = Dr("HPP_Sementara_Tk_Sdr")
                X_HPP_Agency = Dr("HPP_Agency")
                X_HPP_Sementara_Agency = Dr("HPP_Sementara_Agency")

                X_Penjualan = Dr("Penjualan")
                X_Penjualan_Tk_Sdr = Dr("Penjualan_Tk_Sdr")
                X_Penjualan_Agency = Dr("Penjualan_Agency")
                X_Penjualan_Sementara_Tk_Sdr = Dr("Penjualan_Sementara_Tk_Sdr")
                X_Penjualan_Sementara_Agency = Dr("Penjualan_Sementara_Agency")

                X_Diskon_Penjualan = Dr("diskon_penjualan")

                X_PPN_Pembelian = Dr("ppn_pembelian")
                X_PPN_Penjualan = Dr("ppn_penjualan")
                X_PPN_Penjualan_Sementara = Dr("PPN_Penjualan_Sementara")
                X_PPN_Penjualan_Sementara_Agency = Dr("PPN_Penjualan_Sementara_Agency")

                X_Pendapatan_Salon = Dr("pendapatan_salon")
                X_Pendapatan_Member = Dr("pendapatan_member")
                X_Pending_Persediaan = Dr("pending_persediaan")
                X_Retur_Penjualan = Dr("retur_penjualan")

                X_Piutang = Dr("piutang")
                X_Piutang_Cabang_Sendiri = Dr("piutang_cabang_sendiri")
                X_Piutang_Agency = Dr("piutang_agency")

                X_Piutang_Sementara_Cabang_Sendiri = Dr("Piutang_Sementara_Cabang_Sendiri")
                X_Piutang_Sementara_Agency = Dr("Piutang_Sementara_Agency")

                X_Biaya_Flever = Dr("biaya_flever")
                X_Penjualan_Lainnya = Dr("penjualan_lainnya")
                X_Retur_Jual_Lainnya = Dr("retur_lainnya")

                X_Persediaan_Brg_Blm_Krm = Dr("Persediaan_Brg_Blm_Krm")
                X_Brg_Blm_Krm = Dr("Brg_Blm_Krm")

                X_Pelunasan_Dimuka = Dr("pelunasan_dimuka")

            Else
                X_Kas = ""
                X_Modal = ""
                X_Persediaan = ""
                X_Persediaan_Sementara = ""
                X_Persediaan_Sementara_Agency = ""

                X_Hutang = ""
                X_Diskon_Pembelian = ""

                X_HPP = ""
                X_HPP_Tk_Sdr = ""
                X_HPP_Sementara_Tk_Sdr = ""
                X_HPP_Agency = ""
                X_HPP_Sementara_Agency = ""

                X_Penjualan = ""
                X_Penjualan_Tk_Sdr = ""
                X_Penjualan_Agency = ""
                X_Penjualan_Sementara_Tk_Sdr = ""
                X_Penjualan_Sementara_Agency = ""

                X_Diskon_Penjualan = ""

                X_PPN_Pembelian = ""
                X_PPN_Penjualan = ""
                X_PPN_Penjualan_Sementara = ""
                X_PPN_Penjualan_Sementara_Agency = ""

                X_Pendapatan_Salon = ""
                X_Pendapatan_Member = ""
                X_Pending_Persediaan = ""
                X_Retur_Penjualan = ""

                X_Piutang = ""
                X_Piutang_Cabang_Sendiri = ""
                X_Piutang_Agency = ""

                X_Piutang_Sementara_Cabang_Sendiri = ""
                X_Piutang_Sementara_Agency = ""

                X_Biaya_Flever = ""
                X_Penjualan_Lainnya = ""
                X_Retur_Jual_Lainnya = ""

                X_Persediaan_Brg_Blm_Krm = ""
                X_Brg_Blm_Krm = ""

                X_Pelunasan_Dimuka = ""
            End If
        End Using
    End Sub

    Public Function GetBulanIndo(ByVal tgl As String) As String
        Dim xxxxx As String = Format(CDate(tgl), "MM")

        Dim hasil As String = ""
        If Format(CDate(tgl), "MM") = "01" Then
            hasil = "Januari"
        ElseIf Format(CDate(tgl), "MM") = "02" Then
            hasil = "Feb"
        ElseIf Format(CDate(tgl), "MM") = "03" Then
            hasil = "Mar"
        ElseIf Format(CDate(tgl), "MM") = "04" Then
            hasil = "Apr"
        ElseIf Format(CDate(tgl), "MM") = "05" Then
            hasil = "Mei"
        ElseIf Format(CDate(tgl), "MM") = "06" Then
            hasil = "Jun"
        ElseIf Format(CDate(tgl), "MM") = "07" Then
            hasil = "Jul"
        ElseIf Format(CDate(tgl), "MM") = "08" Then
            hasil = "Ags"
        ElseIf Format(CDate(tgl), "MM") = "09" Then
            hasil = "Sept"
        ElseIf Format(CDate(tgl), "MM") = "10" Then
            hasil = "Okt"
        ElseIf Format(CDate(tgl), "MM") = "11" Then
            hasil = "Nov"
        ElseIf Format(CDate(tgl), "MM") = "12" Then
            hasil = "Des"
        End If

        'Return Format(CDate(tgl), "dd " & hasil & " yyyy")
        Return Format(CDate(tgl), "dd ") & hasil & Format(CDate(tgl), " yyyy")
    End Function




    Public Function SN_Tanggal(ByVal kolom As String, Optional ByVal sortby As String = "ASC") As String
        'Dim hasil As String = "SUBSTRING(Serial_Number, CHARINDEX('#02#', " & kolom & ") + 4, LEN(" & kolom & "))"

        'Return hasil

        'Dim hasil As String = "SUBSTRING(Serial_Number, CHARINDEX('#02#', " & kolom & ") + 4, LEN(" & kolom & "))"

        'Return hasil & " ASC, Tgl_Expired ASC "

        Dim hasil As String = "SUBSTRING(Serial_Number, CHARINDEX('#02#', " & kolom & ") + 4, LEN(" & kolom & "))"

        'Return hasil & sortby
        Return hasil
    End Function

    Public Function SN_Disassembly(ByVal data As String, ByVal nomor As Integer, ByVal ke As String) As String
        Dim hasil As String = ""
        xSplit = Split(data, Tanda_SN & ke & Tanda_SN, , CompareMethod.Text)
        hasil = xSplit(nomor)

        Return hasil
    End Function

    Public Function Get_Harga_SN(ByVal sn As String) As Double
        Dim hasil As String = ""
        hasil = Strings.Mid(sn, sn.IndexOf(Tanda_SN & "01" & Tanda_SN) + 5, sn.IndexOf(Tanda_SN & "02" & Tanda_SN) + 1 - sn.IndexOf(Tanda_SN & "01" & Tanda_SN) + 1 - 6)

        Return hasil
    End Function


    Public Function GetLastNumberTrans(ByVal Tanggal As String,
                                 ByVal KodeJurnal As String,
                                 ByVal Kode_Perusahaan As String) As String

        Dim StrLastNumber As String = ""

        SQL = "select top 1 * from jurnal where kode_perusahaan = '" & Kode_Perusahaan & "' and left(kode_voucher,3) = '" & KodeJurnal & "' and substring(kode_voucher,4,6) = '" & Tanggal & "' order by kode_voucher desc"
        Dr = OpenTrans(SQL)
        If Dr.Read Then
            Dim NoTerakhir As Integer = Strings.Right(Dr("kode_voucher"), 3) + 1
            Select Case NoTerakhir
                Case 1 To 9
                    StrLastNumber = KodeJurnal & Tanggal & "00" & NoTerakhir
                Case 10 To 99
                    StrLastNumber = KodeJurnal & Tanggal & "0" & NoTerakhir
                Case Else
                    StrLastNumber = KodeJurnal & Tanggal & NoTerakhir
            End Select
        Else
            StrLastNumber = KodeJurnal & Tanggal & "001"
        End If

        Dr.Close()

        Return StrLastNumber
    End Function

    Public Function Get_Last_Number_PO(ByVal TableName As String,
          ByVal FieldToFind As String,
          ByVal DigitCount As Integer,
          Optional ByVal FieldParameter1 As String = "",
          Optional ByVal Parameter1 As String = "",
          Optional ByVal xOperator As String = "",
          Optional ByVal FieldParameter2 As String = "",
          Optional ByVal Parameter2 As String = "") _
  As String

        Dim LastNumber As Integer = 1
        Dim StrLastNumber As String
        Dim SQL As String

        'OpenConn()

        SQL = "Select top 1 " & FieldToFind & " from " & TableName & " "
        If Not FieldParameter1 = "" Then
            SQL = SQL & "Where " & FieldParameter1 & " = '" & Parameter1 & "' "
        End If
        If Not FieldParameter2 = "" Then
            SQL = SQL & xOperator & " " & FieldParameter2 & " = '" & Parameter2 & "' "
        End If

        SQL = SQL & "order by LEFT(" & FieldToFind & ", 4) desc"

        Dr = OpenTrans(SQL)

        Dim xxx As String 'No Terakhir
        If Dr.Read Then

            xxx = Strings.Left(Dr("" & FieldToFind & ""), DigitCount)
            LastNumber = Val(xxx) + 1
            Select Case LastNumber
                Case Is <= 10
                    StrLastNumber = ("0000000" & Trim(Str(LastNumber)))
                Case Is <= 100
                    StrLastNumber = ("000000" & Trim(Str(LastNumber)))
                Case Is <= 1000
                    StrLastNumber = ("00000" & Trim(Str(LastNumber)))
                Case Is <= 10000
                    StrLastNumber = ("0000" & Trim(Str(LastNumber)))
                Case Is <= 100000
                    StrLastNumber = ("000" & Trim(Str(LastNumber)))
                Case Is <= 1000000
                    StrLastNumber = ("00" & Trim(Str(LastNumber)))
                Case Is <= 10000000
                    StrLastNumber = ("0" & Trim(Str(LastNumber)))
                Case Else
                    StrLastNumber = Trim((Str(LastNumber)))
            End Select
        Else
            StrLastNumber = "00000001"
        End If
        Dr.Close()

        'CloseConn()

        Return Right(StrLastNumber, DigitCount)
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Return ""
        'End Try
    End Function


    Public Sub GetTime()
        Try
            OpenConn()

            SQL = " Select FORMAT(DATEADD(hh, Selisih_jam, getdate()), 'yyyy-MM-dd HH:mm:ss') as Tanggal_Sekarang from Init "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    Tanggal_Sekarang = dr("Tanggal_Sekarang")
                Else
                    CloseConn()
                    MessageBox.Show("Tanggal dan waktu tidak akurat!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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

    Public Sub get_jam()
        Try
            OpenConn()

            SQL = "declare @ab int; declare @ac int; select @ab = Selisih_Jam, @ac= expired_proforma from Init; "
            SQL = SQL & " Select FORMAT(DATEADD(hh, @ab, getdate()), 'yyyy-MM-dd HH:mm:ss')  as Tanggal_Sekarang , @ac as expired"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    tgl_skg = dr("Tanggal_Sekarang")

                Loop
            End Using

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Function SearchFile(ByVal folderPath As String, ByVal fileName As String) As Boolean
        ' Periksa setiap file di folder saat ini
        Dim files As String() = Directory.GetFiles(folderPath, fileName, SearchOption.AllDirectories)
        If files.Length > 0 Then
            ' Jika file ditemukan, tampilkan path-nya
            For Each file In files
                Console.WriteLine($"File ditemukan: {file}")
            Next
            Return True
        End If

        ' Jika tidak ada file yang ditemukan, kembalikan False
        Return False
    End Function
    Public Function Open(ByVal Query As String) As SqlClient.SqlDataReader
        Try
            Cmd = New SqlClient.SqlCommand
            Cmd.Connection = Cn
            Cmd.CommandType = CommandType.Text
            Cmd.CommandText = Query

            Return Cmd.ExecuteReader
            Cmd = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function

    Public Function GetLastNumberJurnal(ByVal Tanggal As String,
                                   ByVal KodeJurnal As String,
                                   ByVal Kode_Perusahaan As String) As String
        SQL = "select top 1 * from jurnal where kode_perusahaan = '" & Kode_Perusahaan & "' and left(kode_voucher,4) = '" & KodeJurnal & "' and substring(kode_voucher,5,6) = '" & Tanggal & "' order by kode_voucher desc"
        Using Dr = OpenTrans(SQL)
            If Dr.Read Then
                Dim NoTerakhir As Integer = Strings.Right(Dr("kode_voucher"), 4) + 1
                Select Case NoTerakhir
                    Case 1 To 9
                        Return KodeJurnal & Tanggal & "000" & NoTerakhir
                    Case 10 To 99
                        Return KodeJurnal & Tanggal & "00" & NoTerakhir
                    Case 100 To 999
                        Return KodeJurnal & Tanggal & "0" & NoTerakhir
                    Case Else
                        Return KodeJurnal & Tanggal & NoTerakhir
                End Select
            Else
                Return KodeJurnal & Tanggal & "0001"
            End If
        End Using
    End Function

    Public Function Get_Detail_Jurnal(ByVal kode_voucher As String, ByVal kode_master_acc As String, ByVal kode_acc As String, ByVal kode_detail_acc As String, ByVal Kode_perusahaan As String, ByVal Kode_Proyek As String, ByVal Keterangan As String, ByVal debit As String, ByVal kredit As String, ByVal pagenumber As String, Optional ByVal _lokasi_per_akun As String = "BELUM") As String
        Dim MMM As String = ""
        MMM = "Insert Into Detail_Jurnal(Kode_perusahaan, kode_voucher, kode_master_acc, kode_acc, "
        MMM = MMM & "kode_detail_acc, kode_proyek, keterangan, debit, kredit, pagenumber, Lokasi_Detail, kode_account) Values('" & Kode_perusahaan & "', "
        MMM = MMM & "'" & kode_voucher & "', '" & kode_master_acc & " ', '" & kode_acc & "', "
        MMM = MMM & "'" & kode_detail_acc & "', '" & Kode_Proyek & "', "
        MMM = MMM & "'" & Keterangan & "', '" & debit & "', '" & kredit & "', '" & pagenumber & "', '" & _lokasi_per_akun & "', '" & kode_master_acc & kode_acc & kode_detail_acc & "') "

        Return MMM
    End Function

    Public Function Binding(ByVal Query As String) As DataSet
        Try
            Cmd = New SqlClient.SqlCommand
            Cmd.Connection = Cn
            Cmd.CommandType = CommandType.Text
            Cmd.CommandText = Query

            Da = New SqlClient.SqlDataAdapter
            Da.SelectCommand = Cmd
            Binding = New DataSet
            Da.Fill(Binding, "MyTable")
            Da = Nothing
            Cmd = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function

    Public Function Get_Last_Number(ByVal TableName As String,
                ByVal FieldToFind As String,
                ByVal DigitCount As Integer,
                Optional ByVal FieldParameter1 As String = "",
                Optional ByVal Parameter1 As String = "",
                Optional ByVal xOperator As String = "And",
                Optional ByVal FieldParameter2 As String = "",
                Optional ByVal Parameter2 As String = "") _
        As String

        Dim LastNumber As Integer = 1
        Dim StrLastNumber As String
        Dim SQL As String

        'OpenConn()

        SQL = "Select top 1 " & FieldToFind & " from " & TableName & " "
        If Not FieldParameter1 = "" Then
            SQL = SQL & "Where " & FieldParameter1 & " = '" & Parameter1 & "' "
        End If
        If Not FieldParameter2 = "" Then
            SQL = SQL & xOperator & " " & FieldParameter2 & " = '" & Parameter2 & "' "
        End If

        SQL = SQL & "order by LEFT(" & FieldToFind & ", 4) desc"

        Dr = OpenTrans(SQL)

        Dim xxx As String 'No Terakhir
        If Dr.Read Then

            xxx = Strings.Left(Dr("" & FieldToFind & ""), DigitCount)
            LastNumber = Val(xxx) + 1
            Select Case LastNumber
                Case Is <= 10
                    StrLastNumber = ("0000000" & Trim(Str(LastNumber)))
                Case Is <= 100
                    StrLastNumber = ("000000" & Trim(Str(LastNumber)))
                Case Is <= 1000
                    StrLastNumber = ("00000" & Trim(Str(LastNumber)))
                Case Is <= 10000
                    StrLastNumber = ("0000" & Trim(Str(LastNumber)))
                Case Is <= 100000
                    StrLastNumber = ("000" & Trim(Str(LastNumber)))
                Case Is <= 1000000
                    StrLastNumber = ("00" & Trim(Str(LastNumber)))
                Case Is <= 10000000
                    StrLastNumber = ("0" & Trim(Str(LastNumber)))
                Case Else
                    StrLastNumber = Trim((Str(LastNumber)))
            End Select
        Else
            StrLastNumber = "00000001"
        End If
        Dr.Close()

        'CloseConn()

        Return Right(StrLastNumber, DigitCount)
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Return ""
        'End Try
    End Function



    Public Function Approval_Hierarchy2(ByVal _jenis As String, ByVal _idlevel As Integer, ByVal _iddivisi As Integer) As Integer
        Data_User_App.Clear()

        Dim ada_data As Integer = 0
        SQL = "select a.UserID_Approval,b.ID_Level_Jabatan,b.ID_Divisi_Sub_Divisi,d.Keterangan "
        SQL = SQL & "from HRIS_UserID_Approval_Karyawan a,Karyawan b,HRIS_Level_Jabatan c,HRIS_Level d "
        SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.UserID_Approval = b.UserID "
        SQL = SQL & "and b.ID_Level_Jabatan = c.ID_Level_Jabatan and c.ID_Level = d.ID_Level "
        SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
        SQL = SQL & "and a.Jenis_Approval = '" & _jenis & "' "
        SQL = SQL & "and a.ID_Level = '" & _idlevel & "' "
        SQL = SQL & "and a.ID_Divisi = '" & _iddivisi & "' order by d.Level_Hierarchy "
        Using Dr = OpenTrans(SQL)
            Do While Dr.Read
                '================================
                'KALAU KETEMU SET ADA_DATA JADI 1
                '================================
                ada_data = 1
                '================================
                Data_User_App.Add(Dr("UserID_Approval"))
            Loop
        End Using

        If ada_data > 0 Then
            Return 1
        Else
            Return 0
        End If
    End Function


    Public Function CekValidEmail(email As String) As Boolean
        ' Pola RegEx untuk memvalidasi format email
        Dim pattern As String = "^[^@\s]+@[^@\s]+\.[^@\s]+$"
        Dim regex As New Regex(pattern)

        ' Mengembalikan true jika format email valid, false jika tidak
        Return regex.IsMatch(email)
    End Function

    Function GenerateKodeUnik() As String
        Dim chars As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"
        Dim result As String = ""
        Dim rand As New Random()

        ' Loop untuk menghasilkan 8 karakter acak
        For i As Integer = 1 To 8
            Dim idx As Integer = rand.Next(0, chars.Length)
            result &= chars(idx)
        Next

        Return result
    End Function



    '========================================'========================================'========================================
    '========================================'========================================'========================================
    Public Function Generate_Random_Kode(ByVal length As Integer) As String
        Dim chars As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"
        Dim result As New StringBuilder()
        Dim Random As New Random()

        For i As Integer = 1 To length
            Dim index As Integer = Random.Next(0, chars.Length)
            result.Append(chars(index))
        Next

        Return result.ToString()
    End Function

    Public Function Generate_QR(ByVal isi As String)
        Dim options As New QrCodeEncodingOptions

        options.DisableECI = True
        options.CharacterSet = "UTF-8"
        'options.Width = 80
        'options.Height = 80

        Dim qr As New ZXing.BarcodeWriter()
        'qr.Options = options
        qr.Options.Width = 80
        qr.Options.Height = 80

        qr.Format = ZXing.BarcodeFormat.QR_CODE

        Dim result As New Bitmap(qr.Write(isi))
        'result.SetResolution(50, 50)

        Return result
    End Function

    Public Function Generate_Batch_FG(ByVal productionDate As String, ByVal lineCode As String, ByVal expDate As String, ByVal Tahun_MulaiProduksi As String) As String

        Dim productionTime As Date = Date.Parse(productionDate)
        Dim Produksi_Tanggal As String = productionTime.Day.ToString
        Dim Produksi_Bulan As String = productionTime.Month.ToString
        Dim Produksi_Tahun As String = If((productionTime.Year - Tahun_MulaiProduksi) Mod 9 = 0, 1, (productionTime.Year - Tahun_MulaiProduksi) Mod 9)
        Dim exp_date As String = Format(Date.Parse(expDate), "ddMMyy")

        Dim NumberToChar As New ArrayList From {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L",
                                     "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"}
        Dim finalBatch As String = ""
        finalBatch = Produksi_Tanggal & NumberToChar(Produksi_Bulan - 1) & Produksi_Tahun & lineCode & exp_date


        Return finalBatch

    End Function

    Public Function Generate_QR_Batch(ByVal MaterialCode As String, ByVal BatchCode As String) As String

        Dim Qr As String = ""
        Qr = MaterialCode & "-" & BatchCode

        Return Qr
    End Function

    Public Function Ubah_Satuan(ByVal kdBarang As String, ByVal jmlhUbah As String, ByVal satuanAwal As String, ByVal satuanAkhir As String, ByVal jenis As String) As Double

        Dim Result As Double = 0

        SQL = "select dbo.ubah_satuan('" & KodePerusahaan & "', '" & jenis & "','" & kdBarang & "', '" & satuanAwal & "',"
        SQL = SQL & "'" & satuanAkhir & "', '" & HilangkanTanda(jmlhUbah) & "' ) as hasil"
        Using Dr1 = OpenTrans(SQL)
            If Dr1.Read Then
                If General_Class.CekNULL(Dr1("hasil")) = "" Then
                    Dr1.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("data konversi satuan kirim tidak ada ")
                    Return Nothing
                End If

                Result = Dr1("hasil")
            Else
                Dr1.Close()
                CloseTrans()
                CloseConn()
                MessageBox.Show("data konversi satuan kirim tidak ada ")
                Return Nothing
            End If
        End Using


        Return Result

    End Function

    Public Function Generate_Batch_Bahan(ByVal SupCode As String, ByVal tgl_kedatangan As Integer, ByVal BulanKedatangan As Integer, ByVal tahunKedatangan As Integer,
 ByVal supplierBatchOrder As Integer, ByVal exp As String) As String

        BulanKedatangan = BulanKedatangan + 1
        supplierBatchOrder = supplierBatchOrder + 1


        Dim NumberToChar As New ArrayList From {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L",
                                     "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"}

        Dim finalBatch As String = ""
        finalBatch = SupCode & tgl_kedatangan & NumberToChar((BulanKedatangan - 1) Mod 26) & tahunKedatangan & NumberToChar((supplierBatchOrder - 1) Mod 26) & exp

        Return finalBatch
    End Function


    'CONTROL LOADING
    Public Sub Start_Loading(targetForm As Form)
        ' INITIALISASI LABEL LOADING
        lblLoading = New Label()

        ' SET PROPERTI DASAR LABEL
        lblLoading.Name = "lblLoading"
        lblLoading.Text = "Please Wait..."
        lblLoading.Font = New Font("Arial", 14, FontStyle.Bold)

        ' SET UKURAN LABEL
        lblLoading.Size = New Size(200, 40)

        ' SET STYLE LABEL
        lblLoading.BackColor = Color.White
        lblLoading.ForeColor = Color.Red
        'lblLoading.BorderStyle = BorderStyle.FixedSingle '
        lblLoading.TextAlign = ContentAlignment.MiddleCenter

        ' ADD LABEL KE FORM
        targetForm.Controls.Add(lblLoading)

        ' SET LABEL DI POSISI TENGAH FORM
        lblLoading.Left = (targetForm.ClientSize.Width - lblLoading.Width) / 2
        lblLoading.Top = (targetForm.ClientSize.Height - lblLoading.Height) / 2

        'SET LABEL BERADA DI ATAS
        lblLoading.BringToFront()

        ' UPDATE UI DAN BUAT AGAR TETAP RESPONSIF
        Application.DoEvents()

        ' DISABLEKAN SEMUA CONTROL PADA FORM
        DisableControlsLoading(targetForm)
    End Sub

    Public Sub End_Loading(targetForm As Form)

        If lblLoading IsNot Nothing Then
            lblLoading.Dispose()
        End If

        ' AKTIFKAN LAGI SEMUA CONTROL DI FORM
        'For Each ctrl As Control In targetForm.Controls
        '    ctrl.Enabled = True
        'Next

        ' SET CURSOR MENJADI NORMAL
        targetForm.Cursor = Cursors.Default

        ' UPDATE UI DAN BUAT AGAR TETAP RESPONSIF
        Application.DoEvents()

    End Sub

    Public Sub DisableControlsLoading(targetForm As Form)

        'MENONAKTIFKAN SEMUA CONTROL DI FORM
        'For Each ctrl As Control In targetForm.Controls
        '    ' DISABLE SEMUMA CONTROL KECUALI LABEL LOADING
        '    If Not (TypeOf ctrl Is Label AndAlso ctrl.Name = "lblLoading") Then
        '        ctrl.Enabled = False
        '    End If
        'Next

        ' SET CURSOR MENJADI LOADING
        targetForm.Cursor = Cursors.WaitCursor
    End Sub

    Public Sub Automation_Forecast_Release()

        get_jam()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            '============================================
            '=     CEK APAKAH SUDAH PERNAH RELEASE?     =
            '============================================
            SQL = "SELECT Kode_Perusahaan FROM log_forecast_release_harian "
            SQL = SQL & "WHERE tanggal = '" & Format(tgl_skg, "yyyy-MM-dd") & "' and Kode_Perusahaan = '" & KodePerusahaan & "'"
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count = 0 Then

                        '==============================================
                        '=     INSERT LOG RELEASE FORECAST HARIAN     =
                        '==============================================
                        SQL = "insert into log_forecast_release_harian (Kode_Perusahaan, Tanggal, Jam) values "
                        SQL = SQL & "('" & KodePerusahaan & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "')"
                        ExecuteTrans(SQL)

                        '================================
                        '=     GET DATA JATUH TEMPO     =
                        '================================
                        Dim Tanggal_Release_Sistem As String = ""
                        Dim Tanggal_UnRelease_Sistem As String = ""
                        SQL = "select tanggal_release_forecast, tanggal_unrelease_forecast from Init where Kode_Perusahaan = '" & KodePerusahaan & "' "
                        Using Dr = OpenTrans(SQL)
                            If Dr.Read Then
                                Tanggal_Release_Sistem = Dr("tanggal_release_forecast")
                                Tanggal_UnRelease_Sistem = Dr("tanggal_unrelease_forecast")
                            Else
                                Dr.Close()
                                CloseTrans()
                                CloseConn()
                                Exit Sub
                            End If
                        End Using

                        '===================================================================
                        '=     CEK APAKAH SUDAH RELEASE DALAM RENTANG WAKTU DITENTUKAN     =
                        '===================================================================
                        SQL = "select Kode_Perusahaan, Tanggal, Jam, jenis from log_forecast_release "
                        SQL = SQL & "WHERE tanggal BETWEEN '" & Format(tgl_skg, "yyyy-MM-01") & "' AND '" & Format(tgl_skg, "yyyy-MM-dd") & "' "
                        SQL = SQL & "and Kode_Perusahaan = '" & KodePerusahaan & "'"
                        Using Ds2 = BindingTrans(SQL)
                            If Ds2.Tables("MyTable").Rows.Count = 0 Then

                                If Val(tgl_skg.Day.ToString) = Val(Tanggal_Release_Sistem) Then

                                    '============================
                                    '=     RELEASE FORECAST     =
                                    '============================
                                    Dim Bulan_Skrng As String = tgl_skg.Month
                                    Dim Tahun_Skrng As String = tgl_skg.Year

                                    SQL = "update EMI_Transaksi_Sales_Forecasting set "
                                    SQL = SQL & "Flag_Validasi = 'Y' "
                                    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                    SQL = SQL & "and bulan BETWEEN " & Bulan_Skrng & " "
                                    If Val(Bulan_Skrng) + 5 > 12 Then
                                        SQL = SQL & "AND 12 "
                                    Else
                                        SQL = SQL & "AND " & Val(Bulan_Skrng) + 5 & " "
                                    End If
                                    SQL = SQL & "AND tahun = '" & Tahun_Skrng & "'"
                                    ExecuteTrans(SQL)

                                    If Val(Bulan_Skrng) + 5 > 12 Then
                                        SQL = "update EMI_Transaksi_Sales_Forecasting set "
                                        SQL = SQL & "Flag_Validasi = 'Y' "
                                        SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                        SQL = SQL & "and bulan BETWEEN 1 AND " & Val(Bulan_Skrng) - 12 & " AND tahun = '" & Val(Tahun_Skrng) + 1 & "'"
                                        ExecuteTrans(SQL)
                                    End If

                                    '=======================================
                                    '=     INSERT LOG RELEASE FORECAST     =
                                    '=======================================
                                    SQL = "insert into log_forecast_release (Kode_Perusahaan, Tanggal, Jam, Jenis) values "
                                    SQL = SQL & "('" & KodePerusahaan & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', "
                                    SQL = SQL & "'RELEASE')"
                                    ExecuteTrans(SQL)

                                ElseIf Val(tgl_skg.Day.ToString) = Val(Tanggal_UnRelease_Sistem) Then

                                    '==============================
                                    '=     UNRELEASE FORECAST     =
                                    '==============================
                                    Dim Bulan_Skrng As String = tgl_skg.Month
                                    Dim Tahun_Skrng As String = tgl_skg.Year

                                    SQL = "update EMI_Transaksi_Sales_Forecasting set "
                                    SQL = SQL & "Flag_Validasi = Null, Flag_Validasi_PPIC = Null "
                                    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                    SQL = SQL & "and bulan BETWEEN " & Bulan_Skrng & " "
                                    If Val(Bulan_Skrng) + 5 > 12 Then
                                        SQL = SQL & "AND 12 "
                                    Else
                                        SQL = SQL & "AND " & Val(Bulan_Skrng) + 5 & " "
                                    End If
                                    SQL = SQL & "AND tahun = '" & Tahun_Skrng & "'"
                                    ExecuteTrans(SQL)

                                    If Val(Bulan_Skrng) + 5 > 12 Then
                                        SQL = "update EMI_Transaksi_Sales_Forecasting set "
                                        SQL = SQL & "Flag_Validasi = Null, Flag_Validasi_PPIC = Null "
                                        SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                        SQL = SQL & "and bulan BETWEEN 1 AND " & Val(Bulan_Skrng) - 12 & " AND tahun = '" & Val(Tahun_Skrng) + 1 & "'"
                                        ExecuteTrans(SQL)
                                    End If

                                    '=======================================
                                    '=     INSERT LOG RELEASE FORECAST     =
                                    '=======================================
                                    SQL = "insert into log_forecast_release (Kode_Perusahaan, Tanggal, Jam, Jenis) values "
                                    SQL = SQL & "('" & KodePerusahaan & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', "
                                    SQL = SQL & "'UNRELEASE')"
                                    ExecuteTrans(SQL)

                                End If

                            Else

                                Dim dataRelease As Integer = 0
                                Dim dataUnRelease As Integer = 0

                                For i As Integer = 0 To Ds2.Tables("MyTable").Rows.Count - 1
                                    If Ds2.Tables("MyTable").Rows(i).Item("jenis") = "RELEASE" Then
                                        dataRelease = dataRelease + 1
                                    ElseIf Ds2.Tables("MyTable").Rows(i).Item("jenis") = "UNRELEASE" Then
                                        dataUnRelease = dataUnRelease + 1
                                    End If
                                Next

                                If dataRelease <> 0 And dataUnRelease <> 0 Then
                                    CloseTrans()
                                    CloseConn()
                                    Exit Sub
                                End If

                                If dataRelease = 0 And dataUnRelease <> 0 Then
                                    '============================
                                    '=     RELEASE FORECAST     =
                                    '============================
                                    Dim Bulan_Skrng As String = tgl_skg.Month
                                    Dim Tahun_Skrng As String = tgl_skg.Year

                                    SQL = "update EMI_Transaksi_Sales_Forecasting set "
                                    SQL = SQL & "Flag_Validasi = 'Y' "
                                    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                    SQL = SQL & "and bulan BETWEEN " & Bulan_Skrng & " "
                                    If Val(Bulan_Skrng) + 5 > 12 Then
                                        SQL = SQL & "AND 12 "
                                    Else
                                        SQL = SQL & "AND " & Val(Bulan_Skrng) + 5 & " "
                                    End If
                                    SQL = SQL & "AND tahun = '" & Tahun_Skrng & "'"
                                    ExecuteTrans(SQL)

                                    If Val(Bulan_Skrng) + 5 > 12 Then
                                        SQL = "update EMI_Transaksi_Sales_Forecasting set "
                                        SQL = SQL & "Flag_Validasi = 'Y' "
                                        SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                        SQL = SQL & "and bulan BETWEEN 1 AND " & Val(Bulan_Skrng) - 12 & " AND tahun = '" & Val(Tahun_Skrng) + 1 & "'"
                                        ExecuteTrans(SQL)
                                    End If

                                    '=======================================
                                    '=     INSERT LOG RELEASE FORECAST     =
                                    '=======================================
                                    SQL = "insert into log_forecast_release (Kode_Perusahaan, Tanggal, Jam, Jenis) values "
                                    SQL = SQL & "('" & KodePerusahaan & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', "
                                    SQL = SQL & "'RELEASE')"
                                    ExecuteTrans(SQL)

                                Else

                                    '==============================
                                    '=     UNRELEASE FORECAST     =
                                    '==============================
                                    Dim Bulan_Skrng As String = tgl_skg.Month
                                    Dim Tahun_Skrng As String = tgl_skg.Year

                                    SQL = "update EMI_Transaksi_Sales_Forecasting set "
                                    SQL = SQL & "Flag_Validasi = Null, Flag_Validasi_PPIC = Null "
                                    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                    SQL = SQL & "and bulan BETWEEN " & Bulan_Skrng & " "
                                    If Val(Bulan_Skrng) + 5 > 12 Then
                                        SQL = SQL & "AND 12 "
                                    Else
                                        SQL = SQL & "AND " & Val(Bulan_Skrng) + 5 & " "
                                    End If
                                    SQL = SQL & "AND tahun = '" & Tahun_Skrng & "'"
                                    ExecuteTrans(SQL)

                                    If Val(Bulan_Skrng) + 5 > 12 Then
                                        SQL = "update EMI_Transaksi_Sales_Forecasting set "
                                        SQL = SQL & "Flag_Validasi = Null, Flag_Validasi_PPIC = Null "
                                        SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                        SQL = SQL & "and bulan BETWEEN 1 AND " & Val(Bulan_Skrng) - 12 & " AND tahun = '" & Val(Tahun_Skrng) + 1 & "'"
                                        ExecuteTrans(SQL)
                                    End If

                                    '=======================================
                                    '=     INSERT LOG RELEASE FORECAST     =
                                    '=======================================
                                    SQL = "insert into log_forecast_release (Kode_Perusahaan, Tanggal, Jam, Jenis) values "
                                    SQL = SQL & "('" & KodePerusahaan & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', "
                                    SQL = SQL & "'UNRELEASE')"
                                    ExecuteTrans(SQL)

                                End If




                            End If
                        End Using

                    End If
                End With
            End Using

            CloseTrans()
            CloseConn()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub




End Module

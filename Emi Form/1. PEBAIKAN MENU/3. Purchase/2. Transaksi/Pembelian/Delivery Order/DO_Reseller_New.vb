Imports System.IO
Imports System.Net

Public Class DO_Reseller_New
    Dim Arr1, Arr2, Arr3, ArrCust, ArrEkspedisi As New ArrayList
    Dim Batal As Color = Color.Black
    Dim putih As Color = Color.White
    Dim abu As Color = Color.LightGray
    Dim CrDoc As Object
    Dim tgl_skg As DateTime
    Dim Id_Transaksi As String
    Dim lama_expire_opname As Integer

    Dim LvSO As String
    Dim LvKB As String
    Dim LvNm As String
    Dim LvXJmlOrder As String
    Dim LvXRtr As String
    Dim LvXSdgKrm As String
    Dim LvXAppKrg As String
    Dim LvXSisa As String
    Dim LvJmlOrder As String
    Dim LvRtr As String
    Dim LvSdgKrm As String
    Dim LvAppKrg As String
    Dim LvSisa As String
    Dim LvJmlKrm As String
    Dim LvUrut As String
    Dim LvXSdhKrm As String
    Dim LvSdhKrm As String
    Dim LvHrgMuat As String
    Dim LvTtlMuat As String
    Dim LvIsiBsr As String
    Dim LvHrg As String
    Dim LvDiscP As String
    Dim LvSubttl As String


    Dim LvFlagBudgeting1 As String
    Dim LvFlagBudgetingMbl As String
    Dim LvFlagBudgeting2 As String
    Dim LvFlagBudgeting3 As String
    Dim LvFlagBudgeting4 As String
    Dim LvHrgAgen As String
    Dim LvHrgTerendah As String
    Dim LvKategori2 As String
    Dim LvMetPer As String
    Dim LvFlagBudgetingNew As String

    Dim LvLokasiTujuan As String
    Dim LvIdGudang As String
    Dim LvNoPenjualan As String
    'Dim LvOpname As String
    ' Dim LvUrutProforma As String

    Dim CellSO As Integer
    Dim CellKB As Integer
    Dim CellNm As Integer
    Dim CellXJmlOrder As Integer
    Dim CellXRtr As Integer
    Dim CellXSdgKrm As Integer

    Dim CellXAppKrg As Integer
    Dim CellXSisa As Integer
    Dim CellJmlOrder As Integer
    Dim CellRtr As Integer

    Dim CellSdgKrm As Integer
    Dim CellAppKrg As Integer
    Dim CellSisa As Integer
    Dim CellJmlKrm As Integer

    Dim CellUrut As Integer
    Dim CellXSdhKrm As Integer
    Dim CellSdhKrm As Integer
    Dim CellHrgMuat As Integer
    Dim CellTtlMuat As Integer
    Dim CellIsiBsr As Integer

    Dim CellHrg As Integer
    Dim CellDiscP As Integer
    Dim CellSubttl As Integer

    Dim CellFlagBudgeting1 As Integer
    Dim CellFlagBudgetingMbl As Integer
    Dim CellFlagBudgeting2 As Integer
    Dim CellFlagBudgeting3 As Integer
    Dim CellFlagBudgeting4 As Integer
    Dim CellHrgAgen As Integer
    Dim CellHrgTerendah As Integer
    Dim CellKategori2 As Integer
    Dim CellMetPer As Integer
    Dim CellFlagBudgetingNew As Integer

    Dim CellLokasiTujuan As Integer
    Dim CellIdGudang As Integer

    'Dim CellOpname As Integer
    'Dim CellUrutProforma As Integer

    Dim item_Gudang As Integer = 0
    Dim item_KdBarang As Integer = 1
    Dim item_Nama As Integer = 2
    Dim item_JmlhOrder1 As Integer = 3
    Dim item_Retur1 As Integer = 4
    Dim item_SdgKirim1 As Integer = 5
    Dim item_ApproveKurang1 As Integer = 6
    Dim item_Sisa1 As Integer = 7
    Dim item_JmlhOrder As Integer = 8
    Dim item_Retur As Integer = 8
    Dim item_SdgKirim As Integer = 10
    Dim item_ApproveKurang As Integer = 11
    Dim item_Sisa As Integer = 12
    Dim item_JmlhKirim As Integer = 13
    Dim item_Urut As Integer = 14
    Dim item_SdhKirim1 As Integer = 15
    Dim item_SdhKirim As Integer = 16
    Dim item_HrgMuat As Integer = 17
    Dim item_TotHrgMuat As Integer = 18
    Dim item_IsiBesar As Integer = 19
    Dim item_Harga As Integer = 20
    Dim item_Diskon As Integer = 21
    Dim item_Total As Integer = 22
    Dim item_FlagBudgeting As Integer = 23
    Dim item_BudgetingMobil As Integer = 24
    Dim item_Flagbudgeting2 As Integer = 25
    Dim item_FlagBudgeting3 As Integer = 26
    Dim item_FlagBudgeting4 As Integer = 27
    Dim item_HrgAgen As Integer = 28
    Dim item_HrgTerendah As Integer = 29
    Dim item_KdKategiru2 As Integer = 30
    Dim item_MetodePerhitungan As Integer = 31
    Dim item_FlagBudgetingNew As Integer = 32
    Dim item_LokasiTujuan As Integer = 33
    Dim item_IdGudang As Integer = 34
    Dim item_NoPenjualan As Integer = 35



    Public Sub Get_Isi_Listview(ByVal No_Index As Integer)
        LvSO = DataGridView1.Rows(No_Index).Cells(0).Value.ToString : CellSO = 0
        LvKB = DataGridView1.Rows(No_Index).Cells(1).Value.ToString : CellKB = 1
        LvNm = DataGridView1.Rows(No_Index).Cells(2).Value.ToString : CellNm = 2
        LvXJmlOrder = DataGridView1.Rows(No_Index).Cells(3).Value.ToString : CellXJmlOrder = 3
        LvXRtr = DataGridView1.Rows(No_Index).Cells(4).Value.ToString : CellXRtr = 4
        LvXSdgKrm = DataGridView1.Rows(No_Index).Cells(5).Value.ToString : CellXSdgKrm = 5

        LvXAppKrg = DataGridView1.Rows(No_Index).Cells(6).Value.ToString : CellXAppKrg = 6
        LvXSisa = DataGridView1.Rows(No_Index).Cells(7).Value.ToString : CellXSisa = 7
        LvJmlOrder = DataGridView1.Rows(No_Index).Cells(8).Value.ToString : CellJmlOrder = 8
        LvRtr = DataGridView1.Rows(No_Index).Cells(9).Value.ToString : CellRtr = 9

        LvSdgKrm = DataGridView1.Rows(No_Index).Cells(10).Value.ToString : CellSdgKrm = 10
        LvAppKrg = DataGridView1.Rows(No_Index).Cells(11).Value.ToString : CellAppKrg = 11
        LvSisa = DataGridView1.Rows(No_Index).Cells(12).Value.ToString : CellSisa = 12
        LvJmlKrm = DataGridView1.Rows(No_Index).Cells(13).Value.ToString : CellJmlKrm = 13

        LvUrut = DataGridView1.Rows(No_Index).Cells(14).Value.ToString : CellUrut = 14
        LvXSdhKrm = DataGridView1.Rows(No_Index).Cells(15).Value.ToString : CellXSdhKrm = 15
        LvSdhKrm = DataGridView1.Rows(No_Index).Cells(16).Value.ToString : CellSdhKrm = 16
        LvHrgMuat = DataGridView1.Rows(No_Index).Cells(17).Value.ToString : CellHrgMuat = 17
        LvTtlMuat = DataGridView1.Rows(No_Index).Cells(18).Value.ToString : CellTtlMuat = 18
        LvIsiBsr = DataGridView1.Rows(No_Index).Cells(19).Value.ToString : CellIsiBsr = 19

        LvHrg = DataGridView1.Rows(No_Index).Cells(20).Value.ToString : CellHrg = 20
        LvDiscP = DataGridView1.Rows(No_Index).Cells(21).Value.ToString : CellDiscP = 21
        LvSubttl = DataGridView1.Rows(No_Index).Cells(22).Value.ToString : CellSubttl = 22

        LvFlagBudgeting1 = DataGridView1.Rows(No_Index).Cells(23).Value.ToString : CellFlagBudgeting1 = 23
        LvFlagBudgetingMbl = DataGridView1.Rows(No_Index).Cells(24).Value.ToString : CellFlagBudgetingMbl = 24
        LvFlagBudgeting2 = DataGridView1.Rows(No_Index).Cells(25).Value.ToString : CellFlagBudgeting2 = 25
        LvFlagBudgeting3 = DataGridView1.Rows(No_Index).Cells(26).Value.ToString : CellFlagBudgeting3 = 26
        LvFlagBudgeting4 = DataGridView1.Rows(No_Index).Cells(27).Value.ToString : CellFlagBudgeting4 = 27
        LvHrgAgen = DataGridView1.Rows(No_Index).Cells(28).Value.ToString : CellHrgAgen = 28
        LvHrgTerendah = DataGridView1.Rows(No_Index).Cells(29).Value.ToString : CellHrgTerendah = 29

        LvKategori2 = DataGridView1.Rows(No_Index).Cells(30).Value.ToString : CellKategori2 = 30
        LvMetPer = DataGridView1.Rows(No_Index).Cells(31).Value.ToString : CellMetPer = 31
        LvFlagBudgetingNew = DataGridView1.Rows(No_Index).Cells(32).Value.ToString : CellFlagBudgetingNew = 32

        LvLokasiTujuan = DataGridView1.Rows(No_Index).Cells(33).Value.ToString : CellLokasiTujuan = 33
        LvIdGudang = DataGridView1.Rows(No_Index).Cells(34).Value.ToString : CellIdGudang = 34
        LvNoPenjualan = DataGridView1.Rows(No_Index).Cells(item_NoPenjualan).Value.ToString

        'LvOpname = DataGridView1.Rows(No_Index).Cells(31).Value.ToString : CellOpname = 31
        'LvUrutProforma = DataGridView1.Rows(No_Index).Cells(32).Value.ToString : CellUrutProforma = 32
    End Sub

    Public Sub laporan(ByVal formula As String, ByVal cr_title As String, ByVal form_title As String)
        CrDoc.RecordSelectionFormula = formula
        CrDoc.SummaryInfo.ReportTitle = cr_title
        A_Place_For_Printing2.Text = form_title
    End Sub

    'Private Function get_no_faktur(ByVal initfaktur As String) As String
    '    'Dim nofak As String = ""
    '    'Dim LastNumber As Integer = 1
    '    'Dim StrLastNumber As String

    '    ''nofak = fDONew & initfaktur & "-" & Format(tgl_skg, "MM/yy") & "-" & _
    '    ''                     General_Class.Get_Last_Number2("do_new_list", "no_do", JumlahDigit, _
    '    ''                     "Kode_perusahaan", KodePerusahaan, _
    '    ''                     "And", "substring(no_do,1," & Len(fDONew) + Len(initfaktur) + 5 & ")", fDONew & initfaktur & "-" & Format(tgl_skg, "MM/yy"))


    '    'SQL = "Select top 1 no_do, ke from do_new_list Where "
    '    'SQL = SQL & "Kode_perusahaan = '" & KodePerusahaan & "' and "
    '    'SQL = SQL & "substring(no_do,1," & Len(fDONew) + Len(initfaktur) + 5 & ") = '" & fDONew & initfaktur & "-" & Format(tgl_skg, "MM/yy") & "'"
    '    'SQL = SQL & "order by no_do desc"

    '    'Dr = OpenTrans(SQL)

    '    'Dim xxx As String 'No Terakhir
    '    'If Dr.Read Then

    '    '    xxx = Strings.Right(Dr("no_do"), JumlahDigit)
    '    '    LastNumber = Val(xxx) + 1
    '    '    Select Case LastNumber
    '    '        Case Is <= 10
    '    '            StrLastNumber = ("0000000" & Trim(Str(LastNumber)))
    '    '        Case Is <= 100
    '    '            StrLastNumber = ("000000" & Trim(Str(LastNumber)))
    '    '        Case Is <= 1000
    '    '            StrLastNumber = ("00000" & Trim(Str(LastNumber)))
    '    '        Case Is <= 10000
    '    '            StrLastNumber = ("0000" & Trim(Str(LastNumber)))
    '    '        Case Is <= 100000
    '    '            StrLastNumber = ("000" & Trim(Str(LastNumber)))
    '    '        Case Is <= 1000000
    '    '            StrLastNumber = ("00" & Trim(Str(LastNumber)))
    '    '        Case Is <= 10000000
    '    '            StrLastNumber = ("0" & Trim(Str(LastNumber)))
    '    '        Case Else
    '    '            StrLastNumber = Trim((Str(LastNumber)))
    '    '    End Select
    '    'Else
    '    '    StrLastNumber = "00000001"
    '    'End If
    '    'nofak = StrLastNumber

    '    Return nofak
    'End Function

    Private Sub Cek_Member()


        Dim Plafond_Kode As New ArrayList
        Dim Plafond_Dari As New ArrayList
        Dim Plafond_Sampai As New ArrayList
        Dim Plafond_Persen As New ArrayList
        Dim Plafon_Blacklist As New ArrayList

        Dim Persen_Penurunan_Nol As Integer = 0

        SQL = "select Kode_Plafond, Hari_Dari, Hari_Sampai, Persen_Penurunan, flag_blacklist "
        SQL = SQL & "from member_penurunan_plafond order by Kode_Plafond "
        Using Dr = OpenTrans(SQL)
            Do While Dr.Read
                If Dr("Persen_Penurunan") = 0 Then
                    Persen_Penurunan_Nol = Dr("Hari_Dari")
                End If
                Plafon_Blacklist.Add(Dr("flag_blacklist"))
                Plafond_Kode.Add(Dr("Kode_Plafond"))
                Plafond_Dari.Add(Dr("Hari_Dari"))
                Plafond_Sampai.Add(Dr("Hari_Sampai"))
                Plafond_Persen.Add(Dr("Persen_Penurunan"))
            Loop
        End Using
        ''2020-03-22'
        '" & Format(Tanggal_Sekarang, "yyyy-MM-dd") & "'

        SQL = "select a.No_DO, a.tanggal_DO, datediff(day,tanggal_DO,'" & Format(tgl_skg, "yyyy-MM-dd") & "') as hari,"
        SQL = SQL & " a.Total_baru, a.Kode_Customer, b.cek_Member "
        SQL = SQL & "from Rekap_Sub_Invoice a, do_new b where "
        SQL = SQL & "a.kode_perusahaan = b.Kode_Perusahaan And a.no_do = b.No_DO and "
        SQL = SQL & "(a.total_baru_dikurang_diskon + a.nilai_ppn_baru) - (a.retur_baru_dikurang_diskon + a.nilai_ppn_retur_baru) - "
        SQL = SQL & "(a.retur_baru_beda_bulan_dikurang_diskon + a.nilai_ppn_retur_baru_beda_bulan) - a.sudah_dilunasi > 0 "
        SQL = SQL & "and a.flag_lunas_do is null and datediff(day,tanggal_DO,'" & Format(tgl_skg, "yyyy-MM-dd") & "') >= " & Persen_Penurunan_Nol & " " 'and a.no_do='DNCCR-08/20-0007'"
        SQL = SQL & "order by b.id"
        Using Ds = BindingTrans(SQL)
            With Ds.Tables("MyTable")
                For index As Integer = 0 To .Rows.Count - 1
                    Dim cek_member As Integer = 0
                    If General_Class.CekNULL(.Rows(index).Item("cek_Member")) = "" Then
                        cek_member = 0
                    Else
                        cek_member = .Rows(index).Item("cek_Member")
                    End If

                    If .Rows(index).Item("hari") >= Persen_Penurunan_Nol Then

                        For index2 As Integer = 0 To Plafond_Kode.Count - 1


                            If .Rows(index).Item("hari") >= Plafond_Dari.Item(index2) And
                            .Rows(index).Item("hari") <= Plafond_Sampai.Item(index2) And
                            cek_member <> Plafond_Kode.Item(index2) Then

                                Dim plafon As Double = 0
                                Dim Pengurangan_Plafon As Double = 0
                                Dim Number_Member As Integer = 0
                                Dim Plafon_Sebelum As Double = 0
                                Dim MemberSebelum As String = ""
                                Dim KategoriMemberSebelum As String = ""

                                SQL = "select Plafon, Number_Member, Kode_Member, Kode_Kategori_Member  from customers "
                                SQL = SQL & "where Kode_customer = '" & .Rows(index).Item("Kode_Customer") & "'"
                                Using Dr = OpenTrans(SQL)
                                    If Dr.Read Then
                                        plafon = Dr("Plafon") ' + Dr("plafon_awal")
                                        Plafon_Sebelum = Dr("Plafon")
                                        Number_Member = Dr("Number_Member")
                                        MemberSebelum = Dr("Kode_Member")
                                        KategoriMemberSebelum = Dr("Kode_Kategori_Member")
                                    End If
                                End Using

                                Pengurangan_Plafon = HilangkanTanda(Format((plafon * Plafond_Persen.Item(index2)) / 100, "N0"))

                                plafon = plafon - Pengurangan_Plafon


                                SQL = "insert into member_log_check_per_do "
                                SQL = SQL & "(Kode_Perusahaan, Kode_Customer, No_Do, Kode_Check, Tanggal_Check, Jam_Check, Urut_Customer) "
                                SQL = SQL & " Values('" & KodePerusahaan & "', '" & .Rows(index).Item("Kode_Customer") & "', '" & .Rows(index).Item("No_DO") & "', '" & Plafond_Kode.Item(index2) & "', "
                                SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', '" & Number_Member & "') "
                                ExecuteTrans(SQL)



                                SQL = "Update Do_New set Cek_Member = '" & Plafond_Kode.Item(index2) & "' "
                                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and No_DO ='" & .Rows(index).Item("No_DO") & "' "
                                ExecuteTrans(SQL)

                                If Plafon_Blacklist.Item(index2) = "Y" Then
                                    SQL = "Update customers set blacklist = 'Y' "
                                    SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_customer ='" & .Rows(index).Item("Kode_Customer") & "' "
                                    ExecuteTrans(SQL)
                                End If

                                SQL = "select Kode_Member, Kode_Kategori, Plafond_Dari, "
                                SQL = SQL & "Plafond_Sampai, Hari_Dari, Hari_Sampai, Urutan from Member "
                                SQL = SQL & "order by Urutan "
                                Using Ds2 = BindingTrans(SQL)
                                    For index3 As Integer = 0 To Ds2.Tables("MyTable").Rows.Count - 1

                                        If plafon >= Ds2.Tables("MyTable").Rows(index3).Item("Plafond_Dari") And
                                        plafon <= Ds2.Tables("MyTable").Rows(index3).Item("Plafond_Sampai") And
                                        .Rows(index).Item("hari") >= Ds2.Tables("MyTable").Rows(index3).Item("Hari_Dari") And
                                        .Rows(index).Item("hari") <= Ds2.Tables("MyTable").Rows(index3).Item("Hari_Sampai") Then


                                            SQL = "insert into member_log_check_all "
                                            SQL = SQL & "(Kode_Perusahaan, Tanggal, Jam, Plafond_Tambahan, Kode_Customer, Nilai_Plafond, Kode_Member, "
                                            SQL = SQL & "Kode_Kategori, Plafond_Dari, Plafond_Sampai, Hari_Dari, Hari_Sampai, Urutan_Member, No_Faktur, Jenis, Total_Plafond, Plafon_Sebelum, Member_Sebelum, Kode_Kategori_Sebelum, Urut_Customer) "
                                            SQL = SQL & "Values('" & KodePerusahaan & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', "
                                            SQL = SQL & "'" & (-1 * Pengurangan_Plafon) & "', '" & .Rows(index).Item("Kode_Customer") & "', '" & (-1 * Plafond_Persen.Item(index2)) & "', "
                                            SQL = SQL & "'" & Ds2.Tables("MyTable").Rows(index3).Item("Kode_Member") & "', '" & Ds2.Tables("MyTable").Rows(index3).Item("Kode_Kategori") & "', "
                                            SQL = SQL & "'" & Ds2.Tables("MyTable").Rows(index3).Item("Plafond_Dari") & "', '" & Ds2.Tables("MyTable").Rows(index3).Item("Plafond_Sampai") & "', "
                                            SQL = SQL & "'" & Ds2.Tables("MyTable").Rows(index3).Item("Hari_Dari") & "', '" & Ds2.Tables("MyTable").Rows(index3).Item("Hari_Sampai") & "', "
                                            SQL = SQL & "'" & Ds2.Tables("MyTable").Rows(index3).Item("Urutan") & "', '" & .Rows(index).Item("No_DO") & "', 'DO','" & plafon & "', "
                                            SQL = SQL & "'" & Plafon_Sebelum & "', '" & MemberSebelum & "', '" & KategoriMemberSebelum & "','" & Number_Member & "') "
                                            ExecuteTrans(SQL)

                                            Number_Member += 1

                                            SQL = "Update Customers set Kode_Member ='" & Ds2.Tables("MyTable").Rows(index3).Item("Kode_Member") & "', "
                                            SQL = SQL & "Kode_Kategori_Member = '" & Ds2.Tables("MyTable").Rows(index3).Item("Kode_Kategori") & "', "
                                            SQL = SQL & "Plafon = '" & plafon & "', Number_Member = '" & Number_Member & "' "
                                            SQL = SQL & "where Kode_customer ='" & .Rows(index).Item("Kode_Customer") & "' "
                                            ExecuteTrans(SQL)
                                            Exit For
                                        End If

                                    Next
                                End Using

                                Exit For

                            End If

                        Next
                    End If


                Next

            End With

        End Using

        SQL = "insert into member_log_Harian "
        SQL = SQL & "(Kode_Perusahaan, Tanggal, Jam) "
        SQL = SQL & " Values('" & KodePerusahaan & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "')"
        ExecuteTrans(SQL)


    End Sub

    Private Sub Kosong()
        CmbJnsMbl.Items.Clear()
        CmbJnsMbl.Items.Add("Sendiri")
        CmbJnsMbl.Items.Add("Ekspedisi")
        CmbJnsMbl.SelectedIndex = 0

        DataGridView1.Columns(33).DisplayIndex = 33

        TxtMbl.Visible = False
        TxtMbl.Text = ""
        TxtMbl.Location = New Point(237, 12)
        CmbEkspedisi.Location = New Point(173, 12)
        CmbJnsDriver.Items.Clear()
        CmbJnsDriver.Items.Add("Sendiri")
        CmbJnsDriver.Items.Add("Lain")
        CmbJnsDriver.SelectedIndex = 0

        CmbEkspedisi.Visible = False

        TxtDriver.Visible = False
        TxtDriver.Text = ""
        TxtDriver.Location = New Point(173, 37)

        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""

        CmbMbl.Visible = True
        CmbDriver.Visible = True
        CmbMbl.Location = New Point(173, 12)

        CmbMbl.Items.Clear()
        CmbDriver.Items.Clear()
        CmbHelper.Items.Clear()
        CmbHelper.Enabled = True

        CheckBox4.Checked = False
        CheckBox4.Enabled = False
        ComboBox3.Items.Clear()
        ComboBox3.Enabled = False
        ArrCust.Clear()

        DataGridView1.Rows.Clear()

        TextBox17.Text = "0"
        TextBox18.Text = "0"
        TextBox19.Text = "0"
        TxtTotal.Text = "0"



        Try
            OpenConn()

            SQL = "select kode_mobil from kendaraan where kode_perusahaan = '" & KodePerusahaan & "' order by kode_mobil"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    CmbMbl.Items.Add(Dr("kode_mobil"))
                Loop
            End Using

            SQL = "select kode_karyawan from karyawan where kode_perusahaan = '" & KodePerusahaan & "' and jenis = 'D' order by kode_karyawan"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    CmbDriver.Items.Add(Dr("kode_karyawan"))
                Loop
            End Using

            CmbEkspedisi.Items.Clear() : ArrEkspedisi.Clear()
            SQL = "select Id_Ekspedisi, kode_Ekspedisi from EMI_Master_Ekspedisi where kode_perusahaan = '" & KodePerusahaan & "' order by kode_Ekspedisi"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    CmbEkspedisi.Items.Add(Dr("kode_Ekspedisi")) : ArrEkspedisi.Add(Dr("Id_Ekspedisi"))
                Loop
            End Using
            'SQL = "declare @ab int; select @ab = Selisih_Jam from Init; "
            'SQL = SQL & " Select FORMAT(DATEADD(hh, @ab, getdate()), 'yyyy-MM-dd HH:mm:ss') as Tanggal_Sekarang "
            'Using dr = OpenTrans(SQL)
            '    Do While dr.Read
            '        tgl_skg = dr("Tanggal_Sekarang")
            '    Loop
            'End Using

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        get_jam()


        Try
            OpenConn()

            Cmd.Transaction = Cn.BeginTransaction



            Dim Validasi_Member As Boolean = False
            SQL = "select * from member_log_Harian where Tanggal = '" & Format(tgl_skg, "yyyy-MM-dd") & "' "
            Using Dr = OpenTrans(SQL)
                If Not Dr.Read Then
                    Validasi_Member = True
                End If
            End Using

            If Validasi_Member = True Then
                Cek_Member()

                SQL = "select kode_customer,nama,jenis_trans,blacklist,lokasi,lokasi_gudang , unblacklist, blacklist from customers where kode_perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and blacklist = 'Y' and unblacklist = 'Y' "
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        If Ds.Tables("MyTable").Rows.Count <> 0 Then
                            For index As Integer = 0 To .Rows.Count - 1

                                SQL = "update customers set blacklist = 'T' where "
                                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and kode_customer = '" & .Rows(index).Item("kode_customer") & "'"
                                ExecuteTrans(SQL)

                                SQL = "Insert Into Logs_new(tanggal,kode_perusahaan,kode_customer,nama,jenis_transaksi,blacklist,lokasi,gudang,"
                                SQL = SQL & "keterangan, userid) "
                                SQL = SQL & "Values('" & Format(tgl_skg, "yyyy-MM-dd HH:mm:ss") & "','" & KodePerusahaan & "','" & .Rows(index).Item("kode_customer") & "','"
                                SQL = SQL & .Rows(index).Item("nama") & "','" & .Rows(index).Item("jenis_trans") & "','T','"
                                SQL = SQL & .Rows(index).Item("lokasi") & "','" & .Rows(index).Item("lokasi_gudang") & "', "
                                SQL = SQL & "'Update Blacklist customers', '" & UserID & "')"
                                ExecuteTrans(SQL)
                            Next



                        End If
                    End With
                End Using


            End If

            Cmd.Transaction.Commit()

            CloseConn()

        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        '=================================




        Try
            OpenConn()


            Dim checkNilaiMemenuhi As Boolean = False
            Dim nilaiAkhir As Integer = 0
            Dim pesan As String = ""

            Dim selisih_tanggal As Integer = 0

            SQL = "select top(1) tanggal_cek from log_cek_penjualan "
            'SQL = SQL & "where format(tanggal_cek, 'yyyy-MM-dd') = format(getdate(),'yyyy-MM-dd') "
            SQL = SQL & "order by tanggal_cek desc "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    If Format(Dr("tanggal_cek"), "yyyy-MM-dd") = Format(tgl_skg, "yyyy-MM-dd") Then
                        Dr.Close()
                        CloseConn()
                        ' MessageBox.Show("hari ini sudah dicheck")
                        Exit Sub
                    Else
                        selisih_tanggal = DateDiff("d", Dr("tanggal_cek"), tgl_skg)
                    End If
                End If
            End Using

            SQL = "insert into log_cek_penjualan(tanggal_cek) "
            SQL = SQL & "values('" & Format(tgl_skg, "yyyy-MM-dd HH:mm:ss") & "')"
            ExecuteTrans(SQL)

            For indexSelisih As Integer = 1 To selisih_tanggal
                pesan = ""
                pesan = "Hallo Bapak/Ibu %0A" & ControlChars.NewLine
                pesan = pesan & "Berikut Invoice Penjualan yang melebihi dari nilai *" & Format(nilai_penjualan, "N0") & "*  %0A " & Format(tgl_skg.AddDays(-indexSelisih), "dd MMM yyyy") & "  %0A"
                pesan = pesan & ControlChars.NewLine

                SQL = ";with cte_hitung_N_grand as ( "
                SQL = SQL & "select a.kode_perusahaan, c.kode_customer, d.nama as nama_customer, a.NGrand as grand_do, "

                SQL = SQL & "isnull(("
                SQL = SQL & "select sum(x.NGrand) "
                SQL = SQL & "from Retur_DO x where x.Kode_Perusahaan = a.Kode_Perusahaan  "
                SQL = SQL & "and x.No_DO = a.No_DO and x.status is null"
                SQL = SQL & "),0) as grand_retur "

                SQL = SQL & "from do_new a, penjualan c, customers d where "
                SQL = SQL & "a.kode_perusahaan = c.kode_perusahaan And "
                SQL = SQL & "c.kode_perusahaan = d.kode_perusahaan And "
                SQL = SQL & "a.no_Faktur = c.no_faktur and c.kode_customer = d.kode_customer and "
                SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and a.Status is null and "
                SQL = SQL & "c.Status is null and a.tanggal = format(getdate() -  " & indexSelisih & ", 'yyyy-MM-dd') " ' format(getdate() - 1, 'yyyy-MM-dd') "
                SQL = SQL & " group by a.kode_perusahaan, c.Kode_Perusahaan, c.kode_customer, a.No_DO, d.nama, a.NGrand "
                SQL = SQL & "), "

                SQL = SQL & "cte_total_bersih as ("
                SQL = SQL & "select kode_perusahaan, kode_customer, nama_customer, sum(grand_do - grand_retur)as hasil from cte_hitung_n_grand "
                SQL = SQL & "group by Kode_Perusahaan, Kode_Customer, nama_customer "
                SQL = SQL & ") "

                SQL = SQL & "select * from cte_total_bersih "
                SQL = SQL & "where hasil >= " & nilai_penjualan



                Using Dr = OpenTrans(SQL)
                    Do While Dr.Read
                        'nilaiAkhir = nilaiAkhir + Dr("hasil")
                        checkNilaiMemenuhi = True
                        pesan = pesan & "*" & Dr("nama_customer") & "* %0A"
                        pesan = pesan & "Rp. " & Format(Dr("hasil"), "N0") & " %0A %0A"
                    Loop

                End Using


                If checkNilaiMemenuhi = True Then
                    SQL = "select nama,no_wa from whatsapp_user"
                    SQL = SQL & " where flag_aktif = 'Y' "
                    Using Ds = BindingTrans(SQL)
                        With Ds.Tables("MyTable")
                            For index As Integer = 0 To .Rows.Count - 1
                                ExecuteTrans(General_Module.SimpanPenjualanHariIni(.Rows(index).Item("no_wa"), pesan, "Penjualan"))
                            Next
                        End With
                    End Using
                End If




                'If checkNilaiMemenuhi = False Then
                '    'CloseConn()
                '    'MessageBox.Show("Tidak ada penjualan yang melibihi 30jt")
                '    'Exit Sub
                'Else
                '    SQL = "select nama,no_wa from whatsapp_user"
                '    SQL = SQL & " where flag_aktif = 'Y' "
                '    Using Ds = BindingTrans(SQL)
                '        With Ds.Tables("MyTable")
                '            For index As Integer = 0 To .Rows.Count - 1
                '                ExecuteTrans(General_Module.SimpanPenjualanHariIni(.Rows(index).Item("no_wa"), pesan, "Penjualan"))
                '            Next
                '        End With
                '    End Using

                'End If
            Next


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try



        'INI UTK DI WA BARU

        'Try

        '    OpenConn()

        '    Dim telpon As String = ""


        '    SQL = "select  id, telpon,keterangan from notifikasi_penjualan where "
        '    SQL = SQL & "flag_sudah_kirim is null and tgl_sudah_kirim is null "
        '    SQL = SQL & "and jam_sudah_kirim is null "
        '    Using Ds = BindingTrans(SQL)

        '        With Ds.Tables("MyTable")
        '            If .Rows.Count <> 0 Then
        '                For index As Integer = 0 To .Rows.Count - 1

        '                    telpon = .Rows(index).Item("telpon")

        '                    If Strings.Left(telpon, 1) = "0" Then
        '                        telpon = "62" & Strings.Mid(telpon, 2, 20)
        '                    End If


        '                    General_Module.SendMessageWhatsapp(telpon, .Rows(index).Item("keterangan"))


        '                    SQL = "update notifikasi_penjualan set flag_sudah_kirim = 'Y', "
        '                    SQL = SQL & "tgl_sudah_kirim = '" & Format(CDate(fmenu.ToolStripStatusLabel3.Text), "yyyy-MM-dd HH:mm:ss") & "', "
        '                    SQL = SQL & "jam_sudah_kirim = '" & Format(CDate(fmenu.ToolStripStatusLabel3.Text), "HH:mm:ss") & "' "
        '                    SQL = SQL & "where id = '" & .Rows(index).Item("id") & "' "
        '                    ExecuteTrans(SQL)

        '                Next
        '            Else
        '                CloseTrans()
        '                'MessageBox.Show("tidak ada notifikasi yang bisa dikirim")
        '                Exit Sub
        '            End If
        '        End With

        '    End Using


        '    CloseConn()

        'Catch ex As Exception
        '    CloseConn()
        '    MessageBox.Show(ex.Message)
        '    Exit Sub
        'End Try

        'Try
        '    OpenConn()

        '    Dim checkNilaiMemenuhi As Boolean = False
        '    Dim nilaiAkhir As Integer = 0
        '    Dim pesan As String = "Hallo Bapak/Ibu %0A" & ControlChars.NewLine
        '    pesan = pesan & "Berikut Invoice Penjualan yang melebihi dari nilai *" & Format(nilai_penjualan, "N0") & "*  %0A Periode 3 Juli 2023 " & ControlChars.NewLine
        '    pesan = pesan & ControlChars.NewLine

        '    SQL = "select tanggal_cek from log_cek_penjualan "
        '    SQL = SQL & "where format(tanggal_cek, 'yyyy-MM-dd') = format(getdate(),'yyyy-MM-dd') "
        '    SQL = SQL & "order by tanggal_cek desc "
        '    Using Dr = OpenTrans(SQL)
        '        If Dr.Read Then
        '            Dr.Close()
        '            CloseConn()
        '            'MessageBox.Show("hari ini sudah dicheck")
        '            Exit Sub
        '        End If
        '    End Using





        '    SQL = ";with cte_hitung_N_grand as ( "
        '    SQL = SQL & "select a.kode_perusahaan, c.kode_customer, d.nama as nama_customer, a.NGrand as grand_do, "

        '    SQL = SQL & "isnull(("
        '    SQL = SQL & "select sum(x.NGrand) "
        '    SQL = SQL & "from Retur_DO x where x.Kode_Perusahaan = a.Kode_Perusahaan  "
        '    SQL = SQL & "and x.No_DO = a.No_DO and x.status is null"
        '    SQL = SQL & "),0) as grand_retur "

        '    SQL = SQL & "from do_new a, penjualan c, customers d where "
        '    SQL = SQL & "a.kode_perusahaan = c.kode_perusahaan And "
        '    SQL = SQL & "c.kode_perusahaan = d.kode_perusahaan And "
        '    SQL = SQL & "a.no_Faktur = c.no_faktur and c.kode_customer = d.kode_customer and "
        '    SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and a.Status is null and "
        '    SQL = SQL & "c.Status is null and format(a.tanggal,'yyyy-MM-dd') = '2023-07-31' " ' format(getdate() - 1, 'yyyy-MM-dd') "
        '    SQL = SQL & "group by a.kode_perusahaan, c.Kode_Perusahaan, c.kode_customer, a.No_DO, d.nama, a.NGrand "
        '    SQL = SQL & "), "

        '    SQL = SQL & "cte_total_bersih as ("
        '    SQL = SQL & "select kode_perusahaan, kode_customer, nama_customer, sum(grand_do - grand_retur)as hasil from cte_hitung_n_grand "
        '    SQL = SQL & "group by Kode_Perusahaan, Kode_Customer, nama_customer "
        '    SQL = SQL & ") "

        '    SQL = SQL & "select * from cte_total_bersih "
        '    SQL = SQL & "where hasil >= " & nilai_penjualan

        '    Using Dr = OpenTrans(SQL)
        '        Do While Dr.Read
        '            'nilaiAkhir = nilaiAkhir + Dr("hasil")
        '            checkNilaiMemenuhi = True
        '            pesan = pesan & "*" & Dr("nama_customer") & "* %0A"
        '            pesan = pesan & "Rp. " & Format(Dr("hasil"), "N0") & " %0A %0A"
        '        Loop

        '    End Using

        '    SQL = "insert into log_cek_penjualan(tanggal_cek) "
        '    SQL = SQL & "values('" & Format(CDate(fmenu.ToolStripStatusLabel3.Text), "yyyy-MM-dd HH:mm:ss") & "')"
        '    ExecuteTrans(SQL)

        '    If checkNilaiMemenuhi = False Then
        '        CloseConn()
        '        'MessageBox.Show("Tidak ada penjualan yang melibihi 30jt")
        '        Exit Sub
        '    Else
        '        SQL = "select nama, no_wa from whatsapp_user"
        '        SQL = SQL & " where flag_aktif = 'Y' "
        '        Using Ds = BindingTrans(SQL)
        '            With Ds.Tables("MyTable")
        '                For index As Integer = 0 To .Rows.Count - 1
        '                    ExecuteTrans(SimpanPenjualanHariIni(.Rows(index).Item("no_wa"), pesan, "Penjualan"))
        '                Next
        '            End With
        '        End Using

        '    End If

        '    CloseConn()
        'Catch ex As Exception
        '    CloseConn()
        '    MessageBox.Show(ex.Message)
        '    Exit Sub
        'End Try

        'Try

        '    OpenConn()

        '    Dim telpon As String = ""


        '    SQL = "select top(1) id, telpon,keterangan from notifikasi_penjualan where "
        '    SQL = SQL & "flag_sudah_kirim is null and tgl_sudah_kirim is null "
        '    SQL = SQL & "and jam_sudah_kirim is null "
        '    Using Ds = BindingTrans(SQL)

        '        With Ds.Tables("MyTable")
        '            For index As Integer = 0 To .Rows.Count - 1

        '                telpon = .Rows(index).Item("telpon")

        '                If Strings.Left(telpon, 1) = "0" Then
        '                    telpon = "62" & Strings.Mid(telpon, 2, 20)
        '                End If

        '                SendMessageWhatsapp(telpon, .Rows(index).Item("keterangan"))

        '                SQL = "update notifikasi_penjualan set flag_sudah_kirim = 'Y', "
        '                SQL = SQL & "tgl_sudah_kirim = '" & Format(CDate(fmenu.ToolStripStatusLabel3.Text), "yyyy-MM-dd HH:mm:ss") & "', "
        '                SQL = SQL & "jam_sudah_kirim = '" & Format(CDate(fmenu.ToolStripStatusLabel3.Text), "HH:mm:ss") & "' "
        '                SQL = SQL & "where id = '" & .Rows(index).Item("id") & "' "
        '                ExecuteTrans(SQL)

        '            Next
        '        End With

        '    End Using


        '    CloseConn()

        'Catch ex As Exception
        '    CloseConn()
        '    MessageBox.Show(ex.Message)
        '    Exit Sub
        'End Try


    End Sub

    Private Sub get_jam()
        Try
            OpenConn()

            SQL = "declare @ab int; declare @ac int; select @ab = Selisih_Jam, @ac= expired_proforma from Init; "
            SQL = SQL & " Select FORMAT(DATEADD(hh, @ab, getdate()), 'yyyy-MM-dd HH:mm:ss')  as Tanggal_Sekarang , @ac as expired"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    tgl_skg = dr("Tanggal_Sekarang")
                    lama_expire_opname = dr("expired")
                Loop
            End Using

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub cetak(ByVal nodo As String, ByVal flag_by_muat As String)
        Try

            OpenConn()

            Dim CrDoc As New Object
            Dim kertas As String = ""

            SQL = "select kode_perusahaan from detail_do_new where kode_perusahaan = '" & KodePerusahaan & "' and no_do = '" & nodo & "' "
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then

                    CrDoc = New Faktur_DO_Reseller
                    kertas = "Faktur"

                    CrDoc.SetDataSource(Ds)
                    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    CrDoc.PrintOptions.PrinterName = PrinterName
                    CrDoc.RecordSelectionFormula = "{detail_do_new.Kode_Perusahaan} = '" & KodePerusahaan & "' and {detail_do_new.no_do} = '" & nodo & "'  "
                    'CrDoc.SummaryInfo.ReportTitle = "Halaman : " & min & "/" & max

                    Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                    doctoprint.PrinterSettings.PrinterName = PrinterName
                    Dim rawKind As Integer
                    CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                    For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                        If doctoprint.PrinterSettings.PaperSizes(i).PaperName = kertas Then
                            rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
                            CrDoc.PrintOptions.PaperSize = rawKind
                            Exit For
                        End If
                    Next

                    CrDoc.PrintOptions.PaperSize = CType(rawKind, CrystalDecisions.Shared.PaperSize)
                    CrDoc.PrintToPrinter(1, False, 1, 99)

                    If flag_by_muat = "Y" Then
                        CrDoc = New Faktur_DO_Reseller_Hrg_Muat
                        kertas = "Faktur"

                        CrDoc.SetDataSource(Ds)
                        CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        CrDoc.PrintOptions.PrinterName = PrinterName
                        CrDoc.RecordSelectionFormula = "{detail_do_new.Kode_Perusahaan} = '" & KodePerusahaan & "' and {detail_do_new.no_do} = '" & nodo & "'  "
                        'CrDoc.SummaryInfo.ReportTitle = "Halaman : " & min & "/" & max

                        'Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                        doctoprint.PrinterSettings.PrinterName = PrinterName
                        'Dim rawKind As Integer
                        CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                        For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                            If doctoprint.PrinterSettings.PaperSizes(i).PaperName = kertas Then
                                rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
                                CrDoc.PrintOptions.PaperSize = rawKind
                                Exit For
                            End If
                        Next

                        CrDoc.PrintOptions.PaperSize = CType(rawKind, CrystalDecisions.Shared.PaperSize)
                        CrDoc.PrintToPrinter(1, False, 1, 99)
                    End If
                End If
            End Using

            CloseConn()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub kosong2()
        ComboBox6.Enabled = False
        Try
            OpenConn()

            ComboBox6.Items.Clear()
            ComboBox6.Items.Add("-- Seluruh --")

            xSplit = CekKotaRole().Split(",")

            SQL = "Select kode_stock_owner From "
            SQL = SQL & "stock_owner where kode_perusahaan = '" & KodePerusahaan & "' and kode_kota in("
            For i As Integer = 0 To xSplit.Count - 1
                SQL = SQL & "'" & xSplit(i).Trim & "', "
            Next
            SQL = Strings.Left(SQL, Len(SQL) - 2)

            SQL = SQL & ") "
            SQL = SQL & "order by kode_stock_owner"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    ComboBox6.Items.Add(dr("kode_stock_owner"))
                Loop
            End Using

            ComboBox6.Text = Lokasi

            If CekButtonRole("Ganti_Lokasi_DO_Reseller") = "T" Then
                ComboBox6.Enabled = False
            Else
                ComboBox6.Enabled = True
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        ComboBox1.Enabled = False : ComboBox2.Enabled = False
        DateTimePicker1.Enabled = False : DateTimePicker2.Enabled = False
        TextBox1.Enabled = False

    End Sub

    Private Sub DO_Reseller_New_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub Display_Data_Penjualan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        ListView1.Columns.Clear()
        ListView2.Columns.Clear()
        DataGridView1.Rows.Clear()

        ListView1.Columns.Add("No Faktur", 105, HorizontalAlignment.Center)
        ListView1.Columns.Add("Tanggal", 85, HorizontalAlignment.Center)
        ListView1.Columns.Add("Jam", 60, HorizontalAlignment.Center)
        ListView1.Columns.Add("Kode Customer", 120, HorizontalAlignment.Left)
        ListView1.Columns.Add("Customer", 480, HorizontalAlignment.Left)
        'ListView1.Columns.Add("Total Jml", 80, HorizontalAlignment.Right)
        ListView1.Columns.Add("No. ", 40, HorizontalAlignment.Right).DisplayIndex = 0
        ListView1.Columns.Add("#", 0, HorizontalAlignment.Right)
        ListView1.Columns.Add("PPN", 0, HorizontalAlignment.Right)
        ListView1.Columns.Add("Lokasi", 160, HorizontalAlignment.Left)
        ListView1.Columns.Add("Flag Helper", 0, HorizontalAlignment.Left)
        ListView1.Columns.Add("##", 0, HorizontalAlignment.Right)
        ListView1.View = View.Details

        ListView2.Columns.Add("No Faktur", 120, HorizontalAlignment.Center)
        ListView2.Columns.Add("Validasi", 200, HorizontalAlignment.Left)
        ListView2.Columns.Add("Error Message", 300, HorizontalAlignment.Left)
        ListView2.Columns.Add("Flag Validasi", 0, HorizontalAlignment.Center)
        ListView2.Columns.Add("Flag kirim", 0, HorizontalAlignment.Center)
        ListView2.Columns.Add("Nama_Customer", 120, HorizontalAlignment.Left)
        ListView2.Columns.Add("No Sementara", 100, HorizontalAlignment.Center)
        ListView2.View = View.Details

        ListView2.Visible = False
        ListView1.Size = New Size(1070, 239)
        Label12.Visible = False

        CheckBox1.Checked = False : CheckBox2.Checked = False
        ComboBox1.Items.Clear() : ComboBox1.Text = "" : Arr1.Clear()
        ComboBox1.Items.Add("Tanggal") : Arr1.Add("b.Tanggal")

        ComboBox2.Items.Clear() : ComboBox2.Text = "" : Arr2.Clear()
        ComboBox2.Items.Add("No Faktur") : Arr2.Add("b.No_Faktur")
        ComboBox2.Items.Add("Kode Customer") : Arr2.Add("b.Kode_customer")
        ComboBox2.Items.Add("Nama Customer") : Arr2.Add("d.Nama")
        ComboBox2.Items.Add("Kode Barang") : Arr2.Add("c.Kode_Barang")
        ComboBox2.Items.Add("Nama Barang") : Arr2.Add("a.Nama")

        Kosong()
        kosong2()

        Button1_Click(Me, e)
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        CheckBox3.Checked = True
        CheckBox3.Checked = False
        CheckBox1.Checked = False
        CheckBox2.Checked = False
        ComboBox2.SelectedIndex = -1
        TextBox1.Text = ""
        ComboBox2.Enabled = False
        TextBox1.Enabled = False

        cari("Y")
        Kosong()
        Cek_Sementara()
    End Sub

    Private Sub cari(ByVal semua As String)
        Kosong()

        Try
            OpenConn()

            SQL = "select e.tampil_helper_di_do, b.lokasi, '11' as ppn, b.init_custm, cast(b.rv as bigint) as rvx, b.no_do, sum(c.jumlah) as tot_jml, "
            SQL = SQL & "b.kode_perusahaan, b.no_faktur, b.tanggal, b.jam, "
            SQL = SQL & "b.status, c.kode_stock_owner, b.kode_customer, d.nama, e.flag_opname "
            SQL = SQL & "from barang a, Penjualan b, detail_penjualan c, Customers d, stock_owner e "
            SQL = SQL & "where a.kode_perusahaan = b.kode_perusahaan and b.kode_perusahaan = c.kode_perusahaan and "
            SQL = SQL & "c.kode_perusahaan = d.kode_perusahaan And d.kode_perusahaan = e.kode_perusahaan And "
            SQL = SQL & "a.kode_barang = c.kode_barang and a.kode_stock_owner = c.kode_stock_owner and b.no_faktur = c.no_faktur and "
            SQL = SQL & "b.lokasi = e.kode_stock_owner and a.kode_perusahaan = '" & KodePerusahaan & "' and b.kode_customer = d.kode_customer and "
            SQL = SQL & "b.status is null and b.flag_do_selesai is null and b.flag_cabang_sendiri = 'T' "

            If ComboBox6.SelectedIndex = 0 Then
                SQL = SQL & " and b.lokasi in("
                Dim list_kota As String = ""
                For x As Integer = 1 To ComboBox6.Items.Count - 1
                    list_kota = list_kota & "'" & ComboBox6.Items(x).ToString & "', "
                Next

                list_kota = Strings.Left(list_kota, Len(list_kota) - 2)

                SQL = SQL & list_kota & ")"
            Else
                SQL = SQL & " and b.lokasi = '" & ComboBox6.Text & "'"
            End If

            If semua = "T" Then
                If CheckBox3.Checked Then
                    'Pasang And
                    If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                    SQL = SQL & " b.tanggal between '"
                    SQL = SQL & Format(tgl_skg, "yyyy-MM-dd") & "' and '" & Format(tgl_skg, "yyyy-MM-dd") & "' "
                End If

                If CheckBox1.Checked Then
                    'Pasang And
                    If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                    SQL = SQL & Arr1.Item(ComboBox1.SelectedIndex) & " between '"
                    SQL = SQL & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' and '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' "
                End If

                If CheckBox2.Checked Then
                    'Pasang And
                    If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                    SQL = SQL & Arr2.Item(ComboBox2.SelectedIndex) & " like '%" & Trim(TextBox1.Text) & "%' "
                End If
            End If

            SQL = SQL & "group by e.tampil_helper_di_do, b.lokasi, b.ppn, b.lokasi, b.init_custm, b.rv, b.no_do, "
            SQL = SQL & "b.kode_perusahaan, b.no_faktur, b.tanggal, b.jam, "
            SQL = SQL & "b.status, c.kode_stock_owner, b.kode_customer, d.nama, e.Flag_Opname "
            SQL = SQL & "Order by b.tanggal + b.jam Desc "

            ListView1.Items.Clear() : DataGridView1.Rows.Clear()
            Dim Lvw As ListViewItem
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    For i As Integer = 0 To .Rows.Count - 1
                        Lvw = ListView1.Items.Add(.Rows(i).Item("no_faktur"))
                        Lvw.SubItems.Add(Format(.Rows(i).Item("tanggal"), "dd-MMM-yyyy"))
                        Lvw.SubItems.Add(.Rows(i).Item("jam"))
                        Lvw.SubItems.Add(.Rows(i).Item("kode_customer"))
                        Lvw.SubItems.Add(.Rows(i).Item("nama"))
                        ' Lvw.SubItems.Add(Format(.Rows(i).Item("tot_jml"), "N0"))
                        Lvw.SubItems.Add(i + 1)
                        Lvw.SubItems.Add(.Rows(i).Item("rvx"))
                        Lvw.SubItems.Add(.Rows(i).Item("ppn"))
                        Lvw.SubItems.Add(.Rows(i).Item("lokasi"))
                        Lvw.SubItems.Add(General_Class.CekNULL(.Rows(i).Item("tampil_helper_di_do")))
                        Lvw.SubItems.Add(.Rows(i).Item("Flag_Opname"))
                    Next
                End With
            End Using


            SQL = "Select Flag_Mulai_DO_Opm "
            SQL = SQL & "from Stock_Owner where "
            If ComboBox6.SelectedIndex = 0 Then
                SQL = SQL & " Kode_Stock_Owner in("
                Dim list_kota As String = ""
                For x As Integer = 1 To ComboBox6.Items.Count - 1
                    list_kota = list_kota & "'" & ComboBox6.Items(x).ToString & "', "
                Next

                list_kota = Strings.Left(list_kota, Len(list_kota) - 2)

                SQL = SQL & list_kota & ")"
            Else
                SQL = SQL & " Kode_Stock_Owner = '" & ComboBox6.Text & "'"
            End If
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    If General_Class.CekNULL(Dr("Flag_Mulai_DO_Opm")) = "Y" Then
                        ListView2.Visible = True
                        ListView1.Size = New Size(751, 239)
                        ListView1.Location = New Point(3, 14)
                        ListView2.Size = New Size(395, 239)
                        ListView2.Location = New Point(758, 14)
                        Label12.Visible = True
                    Else
                        ListView2.Visible = False
                        ListView1.Size = New Size(1150, 239)
                        Label12.Visible = False
                    End If

                End If
            End Using


            'SQL = "select A.No_faktur, C.Nama from do_New_sementara A, Penjualan B, Customers C "
            'SQL = SQL & "where A.Kode_Perusahaan = B.Kode_Perusahaan And A.No_Faktur = B.No_Faktur "
            'SQL = SQL & "and B.Kode_Perusahaan = C.Kode_Perusahaan and B.Kode_Customer = C.Kode_Customer "
            'SQL = SQL & "and B.Lokasi = '" & ComboBox6.Text & "' and A.Flag_Sudah_Validasi_Auditor is null and A.Batal is null"
            'ListView2.Items.Clear()
            'Dim Lvw2 As ListViewItem
            'Using Ds = BindingTrans(SQL)
            '    With Ds.Tables("MyTable")
            '        For i As Integer = 0 To .Rows.Count - 1
            '            Lvw2 = ListView2.Items.Add(.Rows(i).Item("No_faktur"))
            '            Lvw2.SubItems.Add(.Rows(i).Item("Nama"))
            '        Next

            '    End With
            'End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Cek_Sementara()
        Try
            OpenConn()

            Application.DoEvents()

            SQL = " select A.No_faktur, C.Nama, A.no_sementara, A.Flag_Sudah_Validasi_Auditor, A.Flag_Sudah_kirim,  "
            SQL = SQL & "isnull((select top(1) Error_message from log_do_new_sementara x where x.no_sementara = A.No_Sementara "
            SQL = SQL & "and A.Flag_Sudah_Kirim = 'T' order by Urut desc ),'-') as error from do_New_sementara A, Penjualan B, Customers C "
            SQL = SQL & "where A.Kode_Perusahaan = B.Kode_Perusahaan And A.No_Faktur = B.No_Faktur "
            SQL = SQL & "and B.Kode_Perusahaan = C.Kode_Perusahaan and B.Kode_Customer = C.Kode_Customer "
            SQL = SQL & "and A.Flag_mengetahui is null and A.Batal is null "

            If ComboBox6.SelectedIndex = 0 Then
                SQL = SQL & " and b.lokasi in("
                Dim list_kota As String = ""
                For x As Integer = 1 To ComboBox6.Items.Count - 1
                    list_kota = list_kota & "'" & ComboBox6.Items(x).ToString & "', "
                Next

                list_kota = Strings.Left(list_kota, Len(list_kota) - 2)

                SQL = SQL & list_kota & ")"
            Else
                SQL = SQL & " and b.lokasi = '" & ComboBox6.Text & "'"
            End If
            SQL = SQL & " order by No_Sementara "
            ListView2.Items.Clear()
            Dim Lvw2 As ListViewItem
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    For i As Integer = 0 To .Rows.Count - 1
                        Application.DoEvents()


                        Lvw2 = ListView2.Items.Add(.Rows(i).Item("No_faktur"))

                        If IsDBNull(.Rows(i).Item("Flag_Sudah_Validasi_Auditor")) Then
                            Lvw2.SubItems.Add("Belum di Validasi")

                        ElseIf .Rows(i).Item("Flag_Sudah_Validasi_Auditor") = "Y" And IsDBNull(.Rows(i).Item("Flag_Sudah_Kirim")) Then
                            Lvw2.SubItems.Add("Sudah di Validasi, menunggu di proses sistem")
                            ListView2.Items(i).BackColor = Color.LightGreen

                        ElseIf .Rows(i).Item("Flag_Sudah_Validasi_Auditor") = "Y" And .Rows(i).Item("Flag_Sudah_Kirim") = "T" Then
                            Lvw2.SubItems.Add("Sudah di Validasi, gagal di proses sistem")
                            ListView2.Items(i).BackColor = Color.Tomato

                        ElseIf .Rows(i).Item("Flag_Sudah_Validasi_Auditor") = "Y" And .Rows(i).Item("Flag_Sudah_Kirim") = "Y" Then
                            Lvw2.SubItems.Add("Sudah di Validasi, Berhasil di proses sistem")
                            ListView2.Items(i).BackColor = Color.LightBlue

                        Else
                            Lvw2.SubItems.Add("Terjadi kesalahan!")
                        End If
                        Lvw2.SubItems.Add(.Rows(i).Item("error"))
                        Lvw2.SubItems.Add(General_Class.CekNULL(.Rows(i).Item("Flag_Sudah_Validasi_Auditor")))
                        Lvw2.SubItems.Add(General_Class.CekNULL(.Rows(i).Item("Flag_Sudah_Kirim")))
                        Lvw2.SubItems.Add(.Rows(i).Item("Nama"))
                        Lvw2.SubItems.Add(.Rows(i).Item("no_sementara"))
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

    Private Sub ListView1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.Click
        If ListView1.Items.Count = 0 Then Exit Sub

        Column1.ReadOnly = False
        Column1.DefaultCellStyle.BackColor = abu

        Dim cust_induk As String = ""
        CheckBox4.Checked = False
        CheckBox4.Enabled = False
        ComboBox3.Items.Clear()
        ComboBox3.Enabled = False

        TextBox17.Text = "0"
        TextBox18.Text = ListView1.FocusedItem.SubItems(7).Text
        TextBox19.Text = "0"
        TxtTotal.Text = "0"

        Dim y_tampil_helper As String = ""
        CmbHelper.SelectedIndex = -1 : CmbHelper.Enabled = False
        CmbHelper.Items.Clear()

        Id_Transaksi = ""

        get_jam()

        Try

            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            SQL = "Select Kode_Unik "
            SQL = SQL & "from schedule_opname where Lokasi='" & ListView1.FocusedItem.SubItems(8).Text & "'"
            SQL = SQL & "and mulai = 'Y' and selesai = 'T'  "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Id_Transaksi = Dr("Kode_Unik")
                End If
            End Using


            'SQL = "SELECT top(1) datediff(mi, Tanggal+Jam,'" & Format(tgl_skg, "yyyy-MM-dd HH:mm:ss") & "') as selisih from detail_proforma_saat_opname "
            'SQL = SQL & "where No_proforma = '" & ListView1.FocusedItem.Text & "' and Kode_Perusahaan = '" & KodePerusahaan & "' and Id_Transaksi = '" & Id_Transaksi & "'"
            'Using Dr = OpenTrans(SQL)
            '    If Dr.Read Then
            '        If Dr("Selisih") > 10 Then
            '            Dr.Close()
            '            CloseTrans()
            '            CloseConn()
            '            MessageBox.Show("Terjadi Kesalahan, Silahkan Refresh!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '            Exit Sub
            '        End If
            '    Else
            '        Dr.Close()
            '        CloseTrans()
            '        CloseConn()
            '        MessageBox.Show("Terjadi Kesalahan, Silahkan Refresh!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '        Exit Sub
            '    End If
            'End Using

            SQL = "SELECT no_sementara, No_Faktur, datediff(mi, Tanggal+Jam,'" & Format(tgl_skg, "yyyy-MM-dd HH:mm:ss") & "') as selisih from Do_New_Sementara "
            SQL = SQL & "where No_Faktur = '" & ListView1.FocusedItem.Text & "' and Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "batal is null and Flag_Sudah_Validasi_Auditor is null "
            '  SQL = SQL & "and Tanggal+Jam not between '" & Format(tgl_skg.AddMinutes(-10), "yyyy-MM-dd HH:mm:ss") & "' and '" & Format(tgl_skg.AddMinutes(10), "yyyy-MM-dd HH:mm:ss") & "' "
            SQL = SQL & "Group by no_sementara, No_Faktur, tanggal, jam"

            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    For index As Integer = 0 To .Rows.Count - 1
                        If .Rows(index).Item("selisih") > lama_expire_opname Then
                            SQL = "Update Do_New_Sementara Set Batal = 'Y'  "
                            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
                            SQL = SQL & "no_sementara = '" & .Rows(index).Item("no_sementara") & "' "
                            ExecuteTrans(SQL)

                            SQL = "Update penjualan set Flag_Sementara_Saat_Opm = null where "
                            SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
                            SQL = SQL & "No_Faktur = '" & .Rows(index).Item("No_Faktur") & "' "
                            ExecuteTrans(SQL)
                        End If
                    Next
                End With
            End Using

            Cmd.Transaction.Commit()
            CloseConn()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Try

            OpenConn()

            DataGridView1.Rows.Clear()
            Dim no As Integer = 0

            SQL = "Select a.flag_budgeting_new, d.flag_opname, d.tampil_helper_di_do, a.harga, a.persen_diskon, a.subtotal, c.kode_customer, b.hrg_muat, b.Satuan, b.Kode_Satuan_Besar, "
            SQL = SQL & "b.Isi_Satuan_Besar, a.kode_stock_owner, a.Kode_barang, b.nama, a.keterangan, "
            SQL = SQL & "a.jumlah, b.satuan, a.no_urut, "
            SQL = SQL & "a.flag_budgeting, a.flag_budgeting_mbl, a.flag_budgeting_2, a.flag_budgeting_3, a.flag_budgeting_4, a.harga_agen, a.harga_terendah, b.kode_kategori2, "

            'SQL = SQL & "isnull((select sum(y.good_stock + y.bad_stock) from "
            'SQL = SQL & "retur_penjualan x, detail_r_penjualan y where "
            'SQL = SQL & "x.kode_perusahaan = y.kode_perusahaan and x.no_retur_jual = y.no_retur_jual and "
            'SQL = SQL & "x.kode_perusahaan = a.kode_perusahaan And "
            'SQL = SQL & "x.status is null and y.urut = a.no_urut and x.no_faktur_jual = a.no_faktur), 0) as rtr, "

            'SQL = SQL & "isnull((select sum(y.jumlah) from "
            'SQL = SQL & "do_new x, detail_do_new y where "
            'SQL = SQL & "x.kode_perusahaan = y.kode_perusahaan and x.no_do = y.no_do and "
            'SQL = SQL & "x.kode_perusahaan = a.kode_perusahaan And "
            'SQL = SQL & "x.status is null and y.hasil is null and y.no_urut = a.no_urut and x.no_faktur = a.no_faktur), 0) as sedang_kirim, "

            'SQL = SQL & "isnull((select sum((case when y.hasil in('2', '3', '4') then y.jumlah else y.jml_terima end)) from "
            'SQL = SQL & "do_new x, detail_do_new y where "
            'SQL = SQL & "x.kode_perusahaan = y.kode_perusahaan and x.no_do = y.no_do and "
            'SQL = SQL & "x.kode_perusahaan = a.kode_perusahaan And "
            'SQL = SQL & "x.status is null and "
            'SQL = SQL & "y.hasil is not null and y.no_urut = a.no_urut and x.no_faktur = a.no_faktur), 0) as sdh_selesai_validasi, "

            'SQL = SQL & "isnull((select -sum(y.jml_terima - y.jumlah) from "
            'SQL = SQL & "do_new x, detail_do_new y where "
            'SQL = SQL & "x.kode_perusahaan = y.kode_perusahaan and x.no_do = y.no_do and "
            'SQL = SQL & "x.kode_perusahaan = a.kode_perusahaan And "
            'SQL = SQL & "x.status is null and y.jml_terima - y.jumlah < 0 and "
            'SQL = SQL & "y.hasil = 1 and y.no_urut = a.no_urut and x.no_faktur = a.no_faktur), 0) as kurang_kirim_approve "







            SQL = SQL & "isnull((select sum(y.good_stock + y.bad_stock) from "
            SQL = SQL & "retur_penjualan x, detail_r_penjualan y where "
            SQL = SQL & "x.kode_perusahaan = y.kode_perusahaan and x.no_retur_jual = y.no_retur_jual and "
            SQL = SQL & "x.kode_perusahaan = a.kode_perusahaan And "
            SQL = SQL & "x.status is null and y.urut = a.no_urut and x.no_faktur_jual = a.no_faktur and "
            SQL = SQL & "x.No_Retur_DO is null and x.No_DO_Dari_Validasi is null), 0) as rtr, "

            SQL = SQL & "isnull((select sum(y.jumlah) from "
            SQL = SQL & "do_new x, detail_do_new y where "
            SQL = SQL & "x.kode_perusahaan = y.kode_perusahaan and x.no_do = y.no_do and "
            SQL = SQL & "x.kode_perusahaan = a.kode_perusahaan And "
            SQL = SQL & "x.status is null and y.hasil is null and y.no_urut = a.no_urut and x.no_faktur = a.no_faktur), 0) as sedang_kirim, "

            SQL = SQL & "isnull((select -sum(y.jml_terima - y.jumlah) from "
            SQL = SQL & "do_new x, detail_do_new y where "
            SQL = SQL & "x.kode_perusahaan = y.kode_perusahaan and x.no_do = y.no_do and "
            SQL = SQL & "x.kode_perusahaan = a.kode_perusahaan And "
            SQL = SQL & "x.status is null and y.jml_terima - y.jumlah < 0 and "
            SQL = SQL & "y.hasil = 1 and y.no_urut = a.no_urut and x.no_faktur = a.no_faktur), 0) as kurang_kirim_approve, "

            SQL = SQL & "isnull((select -sum(y.jml_terima - y.jumlah) from "
            SQL = SQL & "do_new x, detail_do_new y where "
            SQL = SQL & "x.kode_perusahaan = y.kode_perusahaan and x.no_do = y.no_do and "
            SQL = SQL & "x.kode_perusahaan = a.kode_perusahaan And "
            SQL = SQL & "x.status is null and y.jml_terima - y.jumlah < 0 and "
            SQL = SQL & "y.hasil = 6 and y.no_urut = a.no_urut and x.no_faktur = a.no_faktur), 0) as retur_do, "

            SQL = SQL & "isnull((select sum((case when y.hasil in('2', '3', '4') then y.jumlah else y.jml_terima end)) from "
            SQL = SQL & "do_new x, detail_do_new y where "
            SQL = SQL & "x.kode_perusahaan = y.kode_perusahaan and x.no_do = y.no_do and "
            SQL = SQL & "x.kode_perusahaan = a.kode_perusahaan And "
            SQL = SQL & "x.status is null and "
            SQL = SQL & "y.hasil is not null and y.no_urut = a.no_urut and x.no_faktur = a.no_faktur), 0) as sdh_selesai_validasi, a.Metode_Perhitungan "

            SQL = SQL & ",isnull((select  z.nama_kabupaten_kota + ' - ' + LEFT(x.Alamat_Penerima, 20) from Emi_Customer_Gudang x, tbl_provinsi y, "
            SQL = SQL & "tbl_kabupaten_kota z, tbl_kecamatan v, tbl_kelurahan w where x.kode_perusahaan=a.Kode_Perusahaan and "
            SQL = SQL & "x.Urut_Oto=a.Id_Gudang and x.Id_Provinsi=y.Id_Provinsi and x.Id_Kabupaten_Kota=z.id_kabupaten_kota "
            SQL = SQL & "and x.Id_Kecamatan=v.id_kecamatan and x.Id_Kelurahan=w.id_kelurahan),'-') as Lokasi_Tujuan, a.Id_Gudang "


            'SQL = SQL & "isnull((select top(1)((case when Y.Lapis_Sudah_Opname is not null then 'Sudah Hitung' else 'Belum Hitung' end)) "
            'SQL = SQL & "from Detail_Proforma_Saat_Opname Y where a.no_faktur = Y.No_Proforma and a.kode_Barang= Y.kode_barang "
            'SQL = SQL & "and Y.No_Urut_Penjualan = A.No_Urut and Y.Lokasi = d.Kode_Stock_Owner and Y.id_transaksi = '" & Id_Transaksi & "' and "
            'SQL = SQL & "Y.Flag_Sudah_Dipakai is null and Y.Batal is null and D.Flag_Opname = 'Y'), 0) as Data_Opname, "

            'SQL = SQL & "isnull((select top(1) Qty "
            'SQL = SQL & "from Detail_Proforma_Saat_Opname Y where a.no_faktur = Y.No_Proforma and a.kode_Barang= Y.kode_barang "
            'SQL = SQL & "and Y.No_Urut_Penjualan = A.No_Urut and Y.Lokasi = d.Kode_Stock_Owner and Y.id_transaksi = '" & Id_Transaksi & "' and "
            'SQL = SQL & "Y.Flag_Sudah_Dipakai is null and Y.Batal is null and D.Flag_Opname = 'Y'), 0) as Qty, "

            'SQL = SQL & "isnull((select top(1) Urut_Oto "
            'SQL = SQL & "from Detail_Proforma_Saat_Opname Y where a.no_faktur = Y.No_Proforma and a.kode_Barang= Y.kode_barang "
            'SQL = SQL & "and Y.No_Urut_Penjualan = A.No_Urut and Y.Lokasi = d.Kode_Stock_Owner and Y.id_transaksi = '" & Id_Transaksi & "' and "
            'SQL = SQL & "Y.Flag_Sudah_Dipakai is null and Y.Batal is null and d.Flag_Opname = 'Y'), 0) as Urut_Proforma_Saat_Opname "
            '1= untuk jml terima kurang dr jml kirim & di setujui
            '2= untuk jml terima kurang dr jml kirim & tdk setuju
            '3=untuk jml terima lebih dr jml kirim &
            SQL = SQL & "from detail_penjualan a, barang b, penjualan c, stock_owner d where "
            SQL = SQL & "a.kode_perusahaan = b.kode_Perusahaan and "
            SQL = SQL & "b.kode_perusahaan = c.kode_Perusahaan and "
            SQL = SQL & "c.kode_perusahaan = d.kode_Perusahaan and "
            SQL = SQL & "a.no_faktur = c.no_faktur and "
            SQL = SQL & "a.kode_barang = b.kode_barang and "
            SQL = SQL & "a.kode_stock_owner = b.kode_stock_owner and "
            SQL = SQL & "c.lokasi = d.kode_stock_owner and "
            SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "a.no_faktur = '" & ListView1.FocusedItem.Text & "' order by b.nama"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    DataGridView1.Rows.Add(1)

                    cust_induk = Dr("kode_customer")
                    y_tampil_helper = General_Class.CekNULL(Dr("tampil_helper_di_do"))

                    '  If Dr("jumlah") - Dr("rtr") - Dr("sedang_kirim") - Dr("sdh_selesai_validasi") - Dr("retur_do") - Val(lvjmlkrm) < 0 Then
                    '
                    Dim jml_besar As Double = Math.Floor(Dr("jumlah") / Dr("Isi_Satuan_Besar"))
                    Dim sat_besar As String = If(jml_besar = 0, "", Dr("Kode_Satuan_Besar"))
                    Dim jml_kecil As String = If((Dr("jumlah")) - (jml_besar * Dr("Isi_Satuan_Besar")) = 0, "", (Dr("jumlah")) - (jml_besar * Dr("Isi_Satuan_Besar")))
                    Dim sat_kecil As String = If(jml_kecil = "", "", Dr("Satuan"))

                    Dim jml_retur_besar As Double = Math.Floor(Dr("rtr") / Dr("Isi_Satuan_Besar"))
                    Dim sat_retur_besar As String = If(jml_retur_besar = 0, "", Dr("Kode_Satuan_Besar"))
                    Dim jml_retur_kecil As String = If((Dr("rtr")) - (jml_retur_besar * Dr("Isi_Satuan_Besar")) = 0, "", (Dr("rtr")) - (jml_retur_besar * Dr("Isi_Satuan_Besar")))
                    Dim sat_retur_kecil As String = If(jml_retur_kecil = "", "", Dr("Satuan"))
                    Dim tampilan_retur As String = If(jml_retur_besar = 0 And jml_retur_kecil = "", "0" & Dr("Satuan"), jml_retur_besar & " " & sat_retur_besar & " " & jml_retur_kecil & " " & sat_retur_kecil)

                    Dim jml_kirim_besar As Double = Math.Floor(Dr("sedang_kirim") / Dr("Isi_Satuan_Besar"))
                    Dim sat_kirim_besar As String = If(jml_kirim_besar = 0, "", Dr("Kode_Satuan_Besar"))
                    Dim jml_kirim_kecil As String = If((Dr("sedang_kirim")) - (jml_kirim_besar * Dr("Isi_Satuan_Besar")) = 0, "", (Dr("sedang_kirim")) - (jml_kirim_besar * Dr("Isi_Satuan_Besar")))
                    Dim sat_kirim_kecil As String = If(jml_kirim_kecil = "", "", Dr("Satuan"))
                    Dim tampilan_kirim As String = If(jml_kirim_besar = 0 And jml_kirim_kecil = "", "0" & Dr("Satuan"), jml_kirim_besar & " " & sat_kirim_besar & " " & jml_kirim_kecil & " " & sat_kirim_kecil)

                    Dim jml_approve_besar As Double = Math.Floor(Dr("kurang_kirim_approve") / Dr("Isi_Satuan_Besar"))
                    Dim sat_approve_besar As String = If(jml_approve_besar = 0, "", Dr("Kode_Satuan_Besar"))
                    Dim jml_approve_kecil As String = If((Dr("kurang_kirim_approve")) - (jml_approve_besar * Dr("Isi_Satuan_Besar")) = 0, "", (Dr("kurang_kirim_approve")) - (jml_approve_besar * Dr("Isi_Satuan_Besar")))
                    Dim sat_approve_kecil As String = If(jml_approve_kecil = "", "", Dr("Satuan"))
                    Dim tampilan_approve As String = If(jml_approve_besar = 0 And jml_approve_kecil = "", "0" & Dr("Satuan"), jml_approve_besar & " " & sat_approve_besar & " " & jml_approve_kecil & " " & sat_approve_kecil)

                    Dim jml_sisa_besar As Double = Math.Floor((Dr("jumlah") - Dr("rtr") - Dr("sedang_kirim") + Dr("kurang_kirim_approve")) / Dr("Isi_Satuan_Besar"))
                    Dim sat_sisa_besar As String = If(jml_sisa_besar = 0, "", Dr("Kode_Satuan_Besar"))
                    Dim jml_sisa_kecil As String = If(((Dr("jumlah") - Dr("rtr") - Dr("sedang_kirim") + Dr("kurang_kirim_approve"))) - (jml_sisa_besar * Dr("Isi_Satuan_Besar")) = 0, "", ((Dr("jumlah") - Dr("rtr") - Dr("sedang_kirim") + Dr("kurang_kirim_approve"))) - (jml_sisa_besar * Dr("Isi_Satuan_Besar")))
                    Dim sat_sisa_kecil As String = If(jml_sisa_kecil = "", "", Dr("Satuan"))
                    Dim tampilan_sisa As String = If(jml_sisa_besar = 0 And jml_sisa_kecil = "", "0" & Dr("Satuan"), jml_sisa_besar & " " & sat_sisa_besar & " " & jml_sisa_kecil & " " & sat_sisa_kecil)

                    DataGridView1.Rows.Item(no).Cells(0).Value = Dr("kode_stock_owner")
                    DataGridView1.Rows.Item(no).Cells(1).Value = Dr("kode_barang")
                    DataGridView1.Rows.Item(no).Cells(2).Value = Dr("nama")

                    DataGridView1.Rows.Item(no).Cells(3).Value = Dr("jumlah")
                    DataGridView1.Rows.Item(no).Cells(4).Value = Dr("rtr")
                    DataGridView1.Rows.Item(no).Cells(5).Value = Dr("sedang_kirim")
                    DataGridView1.Rows.Item(no).Cells(6).Value = Dr("kurang_kirim_approve")
                    DataGridView1.Rows.Item(no).Cells(7).Value = Dr("jumlah") - Dr("sdh_selesai_validasi") - Dr("rtr") - Dr("sedang_kirim") ' + Dr("kurang_kirim_approve")

                    DataGridView1.Rows.Item(no).Cells(8).Value = Dr("jumlah")
                    DataGridView1.Rows.Item(no).Cells(9).Value = Dr("rtr")
                    DataGridView1.Rows.Item(no).Cells(10).Value = Dr("sedang_kirim")
                    DataGridView1.Rows.Item(no).Cells(11).Value = Dr("kurang_kirim_approve")
                    DataGridView1.Rows.Item(no).Cells(12).Value = Dr("jumlah") - Dr("sdh_selesai_validasi") - Dr("rtr") - Dr("sedang_kirim") ' + Dr("kurang_kirim_approve")
                    DataGridView1.Rows.Item(no).Cells(13).Value = "0"

                    'If Dr("flag_opname") = "Y" Then
                    '    'DataGridView1.Cells(13).ReadOnly = True
                    '    Column1.ReadOnly = True
                    '    Column1.DefaultCellStyle.BackColor = putih
                    'Else
                    '    Column1.ReadOnly = False
                    '    Column1.DefaultCellStyle.BackColor = Color.LightGray
                    'End If


                    DataGridView1.Rows.Item(no).Cells(14).Value = Dr("no_urut")
                    DataGridView1.Rows.Item(no).Cells(15).Value = Dr("sdh_selesai_validasi")
                    DataGridView1.Rows.Item(no).Cells(16).Value = Dr("sdh_selesai_validasi")
                    DataGridView1.Rows.Item(no).Cells(17).Value = Dr("hrg_muat")
                    DataGridView1.Rows.Item(no).Cells(18).Value = 0
                    DataGridView1.Rows.Item(no).Cells(19).Value = Dr("isi_satuan_besar")

                    DataGridView1.Rows.Item(no).Cells(20).Value = Format(Dr("harga"), "N0")
                    DataGridView1.Rows.Item(no).Cells(21).Value = Dr("persen_diskon")
                    DataGridView1.Rows.Item(no).Cells(22).Value = "0"

                    If General_Class.CekNULL(Dr("flag_budgeting")) = "" Then
                        DataGridView1.Rows.Item(no).Cells(23).Value = ""
                    Else
                        DataGridView1.Rows.Item(no).Cells(23).Value = Dr("flag_budgeting")
                    End If

                    If General_Class.CekNULL(Dr("flag_budgeting_mbl")) = "" Then
                        DataGridView1.Rows.Item(no).Cells(24).Value = ""
                    Else
                        DataGridView1.Rows.Item(no).Cells(24).Value = Dr("flag_budgeting_mbl")
                    End If

                    If General_Class.CekNULL(Dr("flag_budgeting_2")) = "" Then
                        DataGridView1.Rows.Item(no).Cells(25).Value = ""
                    Else
                        DataGridView1.Rows.Item(no).Cells(25).Value = Dr("flag_budgeting_2")
                    End If

                    If General_Class.CekNULL(Dr("flag_budgeting_3")) = "" Then
                        DataGridView1.Rows.Item(no).Cells(26).Value = ""
                    Else
                        DataGridView1.Rows.Item(no).Cells(26).Value = Dr("flag_budgeting_3")
                    End If

                    If General_Class.CekNULL(Dr("flag_budgeting_4")) = "" Then
                        DataGridView1.Rows.Item(no).Cells(27).Value = ""
                    Else
                        DataGridView1.Rows.Item(no).Cells(27).Value = Dr("flag_budgeting_4")
                    End If
                    DataGridView1.Rows.Item(no).Cells(28).Value = Format(Dr("harga_agen"), "N0")
                    DataGridView1.Rows.Item(no).Cells(29).Value = Format(Dr("harga_terendah"), "N0")
                    DataGridView1.Rows.Item(no).Cells(30).Value = Dr("kode_kategori2")

                    If General_Class.CekNULL(Dr("Metode_Perhitungan")) = "" Then
                        DataGridView1.Rows.Item(no).Cells(31).Value = "A"
                    Else
                        DataGridView1.Rows.Item(no).Cells(31).Value = Dr("Metode_Perhitungan")
                    End If

                    If General_Class.CekNULL(Dr("flag_budgeting_new")) = "" Then
                        DataGridView1.Rows.Item(no).Cells(32).Value = ""
                    Else
                        DataGridView1.Rows.Item(no).Cells(32).Value = Dr("flag_budgeting_new")
                    End If
                    'DataGridView1.Rows.Item(no).Cells(31).Value = Dr("Data_Opname")
                    'DataGridView1.Rows.Item(no).Cells(32).Value = Dr("Urut_Proforma_Saat_Opname")
                    'DataGridView1.CurrentCell = DataGridView1.Rows(no).Cells(1)
                    'DataGridView1_CellEndEdit(ListView1, Nothing)

                    DataGridView1.Rows.Item(no).Cells(33).Value = Dr("Lokasi_Tujuan")
                    DataGridView1.Rows.Item(no).Cells(34).Value = Dr("Id_Gudang")
                    DataGridView1.Rows.Item(no).Cells(item_NoPenjualan).Value = ListView1.FocusedItem.Text


                    DataGridView1.Rows.Item(no).Cells(item_JmlhKirim).ReadOnly = True

                    no = no + 1

                Loop
            End Using

            CloseConn()



            OpenConn()

            CheckBox4.Checked = False
            CheckBox4.Enabled = False
            ComboBox3.Items.Clear()
            ComboBox3.Enabled = False
            ArrCust.Clear()

            Dim jml As Integer = 0
            SQL = "select kode_customer, nama from customers where kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "parent = '" & cust_induk & "' order by nama"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    ComboBox3.Items.Add(Dr("nama")) : ArrCust.Add(Dr("kode_customer"))
                    jml = jml + 1
                Loop
            End Using

            If jml = 0 Then
                CheckBox4.Checked = False
                CheckBox4.Enabled = False
                ComboBox3.Items.Clear()
                ComboBox3.Enabled = False
                ArrCust.Clear()
            Else
                CheckBox4.Checked = True
                CheckBox4.Enabled = True
                ComboBox3.SelectedIndex = -1
                ComboBox3.Enabled = True
            End If

            OpenConn()

            ' If ListView1.FocusedItem.SubItems(9).Text = "Y" Then

            If ListView1.FocusedItem.SubItems(9).Text = "Y" Then
                CmbHelper.Enabled = True
                CmbHelper.Items.Clear()

                OpenConn()

                SQL = "select kode_helper from helper where kode_perusahaan = '" & KodePerusahaan & "' and aktif = 'Y' and lokasi = '" & ListView1.FocusedItem.SubItems(8).Text & "' order by kode_helper"
                Using Dr = OpenTrans(SQL)
                    Do While Dr.Read
                        CmbHelper.Items.Add(Dr("kode_helper"))
                    Loop
                End Using

                CloseConn()
            Else

                CmbHelper.Items.Clear()
                CmbHelper.SelectedIndex = -1 : CmbHelper.Enabled = False
            End If

            CloseConn()

            If CmbJnsMbl.SelectedIndex = 0 Then 'sendiri
                CmbMbl.SelectedIndex = -1
                CmbMbl.Visible = True

                TxtMbl.Text = ""
                TxtMbl.Visible = False

                If ListView1.SelectedItems.Count = 0 Then
                    CmbHelper.Enabled = True
                    CmbHelper.SelectedIndex = -1
                Else
                    If ListView1.FocusedItem.SubItems(9).Text = "Y" Then
                        CmbHelper.Enabled = True
                        CmbHelper.SelectedIndex = -1
                    Else
                        CmbHelper.Enabled = False
                        CmbHelper.SelectedIndex = -1
                    End If
                End If

            Else
                CmbMbl.SelectedIndex = -1
                CmbMbl.Visible = False

                TxtMbl.Text = ""
                TxtMbl.Visible = True

                CmbHelper.Enabled = False
                CmbHelper.SelectedIndex = -1

            End If

            CloseConn()


        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub ListView1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListView1.SelectedIndexChanged
        DataGridView1.Rows.Clear()

        Try
            OpenConn()

            SQL = "delete Emi_DO_Pallet_Sementara where Kode_Perusahaan = '" & KodePerusahaan & "' and userid = '" & UserID & "' "
            ExecuteTrans(SQL)

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Display_Data_Transfer_Stock_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SizeChanged
        Label1.Size = New Point(Me.Width, 33)
    End Sub

    Private Sub ComboBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles CmbMbl.KeyPress

        If e.KeyChar = Chr(13) Then
            CmbJnsDriver.Focus()
        End If
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbMbl.SelectedIndexChanged

    End Sub

    Private Sub ComboBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles CmbJnsMbl.KeyPress
        If e.KeyChar = Chr(13) Then
            If CmbJnsMbl.SelectedIndex = 0 Then 'sendiri
                CmbMbl.Focus()
            Else
                CmbEkspedisi.Focus()
            End If
        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbJnsMbl.SelectedIndexChanged
        If CmbJnsMbl.SelectedIndex = 0 Then 'sendiri
            CmbMbl.SelectedIndex = -1
            CmbMbl.Visible = True
            CmbEkspedisi.Visible = False
            TxtMbl.Text = ""
            TxtMbl.Visible = False

            If ListView1.SelectedItems.Count = 0 Then
                CmbHelper.Enabled = True
                CmbHelper.SelectedIndex = -1
            Else
                If ListView1.FocusedItem.SubItems(9).Text = "Y" Then
                    CmbHelper.Enabled = True
                    CmbHelper.SelectedIndex = -1
                Else
                    CmbHelper.Enabled = False
                    CmbHelper.SelectedIndex = -1
                End If
            End If

        Else
            CmbMbl.SelectedIndex = -1
            CmbMbl.Visible = False

            TxtMbl.Text = ""
            TxtMbl.Visible = True

            CmbHelper.Enabled = False
            CmbHelper.SelectedIndex = -1
            CmbEkspedisi.SelectedIndex = -1
            CmbEkspedisi.Visible = True
        End If
    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtMbl.KeyPress
        If e.KeyChar = Chr(13) Then
            CmbHelper.Focus()
        End If
    End Sub

    Private Sub TextBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtDriver.KeyPress
        If e.KeyChar = Chr(13) Then
            TextBox3.Focus()
        End If
    End Sub

    Private Sub TextBox3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox3.KeyPress
        If e.KeyChar = Chr(13) Then
            TextBox5.Focus()
        End If
    End Sub

    Private Sub TextBox4_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox4.KeyPress
        If e.KeyChar = Chr(13) Then
            CmbHelper.Focus()
        End If
    End Sub

    Private Sub TextBox5_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox5.KeyPress
        If e.KeyChar = Chr(13) Then
            If CheckBox4.Checked = True Then
                CheckBox4.Focus()
            Else
                Button5.Focus()
            End If
        End If
    End Sub

    Private Sub CmbDriver_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles CmbDriver.KeyPress
        If e.KeyChar = Chr(13) Then
            TextBox4.Focus()
        End If
    End Sub

    Private Sub ComboBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox3.KeyPress
        If e.KeyChar = Chr(13) Then
            Button5.Focus()
        End If
    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbDriver.SelectedIndexChanged

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If CmbJnsMbl.SelectedIndex = -1 Then
            MessageBox.Show("Jenis kendaraan belum diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbJnsMbl.Focus()
            Exit Sub
        End If

        If CmbJnsMbl.SelectedIndex = 0 Then 'sendiri
            If CmbMbl.SelectedIndex = -1 Then
                MessageBox.Show("Mobil belum diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                CmbMbl.Focus()
                Exit Sub
            End If
        Else
            If TxtMbl.Text.Trim.Length = 0 Then
                MessageBox.Show("Mobil belum diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                TxtMbl.Focus()
                Exit Sub
            End If

            If CmbEkspedisi.SelectedIndex = -1 Then
                MessageBox.Show("Ekspedisi belum diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                CmbEkspedisi.Focus()
                Exit Sub
            End If
        End If

        If CmbJnsDriver.SelectedIndex = -1 Then
            MessageBox.Show("Jenis driver belum diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CmbJnsDriver.Focus()
            Exit Sub
        End If

        If CmbJnsDriver.SelectedIndex = 0 Then 'sendiri
            If CmbDriver.SelectedIndex = -1 Then
                MessageBox.Show("Driver belum diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                CmbDriver.Focus()
                Exit Sub
            End If
        Else
            If TxtDriver.Text.Trim.Length = 0 Then
                MessageBox.Show("Driver belum diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                TxtDriver.Focus()
                Exit Sub
            End If
        End If

        If TextBox3.Text.Trim.Length = 0 Then
            MessageBox.Show("Tujuan belum diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox3.Focus()
            Exit Sub
        ElseIf TextBox4.Text.Trim.Length = 0 Then
            MessageBox.Show("HP belum diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox4.Focus()
            Exit Sub
        ElseIf TextBox5.Text.Trim.Length = 0 Then
            MessageBox.Show("Keterangan belum diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox5.Focus()
            Exit Sub
        End If

        If CmbJnsMbl.SelectedIndex = 0 And ListView1.FocusedItem.SubItems(9).Text = "Y" Then 'sendiri
            If CmbHelper.SelectedIndex = -1 Then
                MessageBox.Show("Helper belum diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                CmbHelper.Focus()
                Exit Sub
            End If
        End If

        If CheckBox4.Checked = True And ComboBox3.Enabled = True Then
            If ComboBox3.SelectedIndex = -1 Then
                MessageBox.Show("Customer belum diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                TextBox5.Focus()
                Exit Sub
            End If
        End If

        Dim ada_selisih As Integer = 0
        Dim total_kirim As Double = 0

        Dim Id_Gudang As String = ""
        Dim indexAdaBarang As Integer = 0
        For i As Integer = 0 To DataGridView1.RowCount - 1
            Get_Isi_Listview(i)

            total_kirim = total_kirim + Val(LvJmlKrm)


            If LvJmlKrm.ToString = "" Then
                MessageBox.Show("Masih ada jml kirim yang belum diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            If IsNumeric(LvJmlKrm) = False Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Jml kirim yg diisi bukan angka!", Judul, MessageBoxButtons.OK)
                Exit Sub
            End If

            If Val(LvJmlKrm) > Val(LvSisa) Then
                MessageBox.Show("Jml kirim lebih besar dari sisa!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            If Val(LvJmlKrm) <> 0 Then
                If indexAdaBarang = 0 Then
                    Id_Gudang = LvIdGudang
                End If

                If LvIdGudang <> Id_Gudang Then
                    MessageBox.Show("Lokasi Tujuan Tidak Boleh Berbeda!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
                indexAdaBarang += 1
            End If
        Next

        If total_kirim = 0 Then
            CloseTrans()
            CloseConn()
            MessageBox.Show("Total kirim harus diisi!", Judul, MessageBoxButtons.OK)
            Exit Sub
        End If

        Dim nofakdo As String = ""
        Dim nofakdoOPM As String = ""
        Dim tanggal_pi As String = ""
        ' Dim kebrp As Integer = 0
        Dim by_muat As String = ""

        Dim boleh_jual_rugi As String = ""

        Try
            OpenConn()

            If CekButtonRole("jual_rugi") = "T" Then
                boleh_jual_rugi = "T"
            Else
                boleh_jual_rugi = "Y"
            End If

            CloseConn()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


        Try
            OpenConn()

            Cmd.Transaction = Cn.BeginTransaction
            '----------------------------------------
            'cek penjualan kalo tgl proforma di atas bulan 2 baru masuk pengecekan barang do
            Dim cek_tanggal As String
            Dim Cek_Tgl As String 'stenly

            SQL = "select tanggal from penjualan "
            SQL = SQL & "where kode_perusahaan ='" & KodePerusahaan & "' and No_Faktur ='" & ListView1.FocusedItem.Text & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    tanggal_pi = Dr("tanggal")

                    'cek_faktur = Dr("tanggal")
                    'If Month(Dr("tanggal")) > 2 And Year(Dr("tanggal")) = Year(tgl_skg) Then

                    'End If
                    If Dr("tanggal") < "2023-02-01" Then
                        ' Dr.Close()
                        cek_tanggal = "Y"
                    Else
                        'Dr.Close()
                        cek_tanggal = "T"
                    End If

                    If Dr("tanggal") < "2024-02-01" Then 'awal stenly
                        Dr.Close()
                        Cek_Tgl = "Y"
                    Else
                        Dr.Close()
                        Cek_Tgl = "T"
                    End If 'akhir stenly
                Else

                    Dr.Close()
                    cek_tanggal = "T"
                    Cek_Tgl = "T" 'stenly
                    tanggal_pi = Dr("tanggal")
                End If
            End Using

            SQL = "delete barang_banned_do_log where kode_perusahaan='" & KodePerusahaan & "' and no_faktur ='" & ListView1.FocusedItem.Text & "'"
            ExecuteTrans(SQL)

            For i As Integer = 0 To DataGridView1.RowCount - 1
                Get_Isi_Listview(i)
                'insert ke barang_banned_do_log
                If LvJmlKrm <> 0 Then
                    SQL = "insert into barang_banned_do_log (kode_perusahaan,kode_stock_owner,no_faktur,kode_barang)"
                    SQL = SQL & " values('" & KodePerusahaan & "','" & LvSO & "','" & ListView1.FocusedItem.Text & "','" & LvKB & "') "
                    ExecuteTrans(SQL)
                End If
                'cek barang yang ada di barang_banned_do
            Next
            If cek_tanggal = "Y" Then

                SQL = "select a.Kode_Stock_owner, b.Kode_Kategori_Besar, b.Kode_Kategori_Kecil from barang_banned_do_log a , barang b where "
                SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_barang = b.Kode_Barang and "
                SQL = SQL & "a.Kode_Stock_owner = b.Kode_Stock_Owner and a.No_Faktur ='" & ListView1.FocusedItem.Text & "'"
                SQL = SQL & "group by a.Kode_Stock_owner, b.Kode_Kategori_Besar, b.Kode_Kategori_Kecil"
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")

                        For index As Integer = 0 To .Rows.Count - 1

                            SQL = "select Jumlah from barang_banned_do "
                            SQL = SQL & "where kode_perusahaan ='" & KodePerusahaan & "' and "
                            SQL = SQL & "kode_stock_owner = '" & .Rows(index).Item("kode_stock_owner") & "' and "
                            SQL = SQL & "Kode_Kategori_Besar ='" & .Rows(index).Item("Kode_Kategori_Besar") & "' and "
                            SQL = SQL & "Kode_Kategori_Kecil ='" & .Rows(index).Item("Kode_Kategori_Kecil") & "' "
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then

                                    If Dr("jumlah") > 0 Then
                                        Dr.Close()
                                        SQL = "update barang_banned_do set jumlah = jumlah - 1 where kode_perusahaan ='" & KodePerusahaan & "' and "
                                        SQL = SQL & "kode_stock_owner = '" & .Rows(index).Item("kode_stock_owner") & "' and "
                                        SQL = SQL & "Kode_Kategori_Besar ='" & .Rows(index).Item("Kode_Kategori_Besar") & "' and "
                                        SQL = SQL & "Kode_Kategori_Kecil ='" & .Rows(index).Item("Kode_Kategori_Kecil") & "' "
                                        ExecuteTrans(SQL)
                                    Else
                                        Dr.Close()
                                        CloseConn()
                                        CloseTrans()
                                        MessageBox.Show("Life Cat Dry & Life Cat 400Gr harus di ACC Pak Dimas!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                        Exit Sub
                                    End If
                                    'Else
                                    '    Dr.Close()
                                    '    CloseTrans()
                                    '    CloseConn()
                                    '    MessageBox.Show("Barang di Lokasi ini tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    '    Exit Sub
                                End If
                            End Using
                        Next
                    End With
                End Using
            End If

            'awal stenly
            SQL = "delete barang_banned_do_log2 where kode_perusahaan='" & KodePerusahaan & "' and no_faktur ='" & ListView1.FocusedItem.Text & "'"
            ExecuteTrans(SQL)

            For i As Integer = 0 To DataGridView1.RowCount - 1
                Get_Isi_Listview(i)
                'insert ke barang_banned_do_log
                If LvJmlKrm <> 0 Then
                    SQL = "insert into barang_banned_do_log2 (kode_perusahaan,kode_stock_owner,no_faktur,kode_barang)"
                    SQL = SQL & " values('" & KodePerusahaan & "','" & LvSO & "','" & ListView1.FocusedItem.Text & "','" & LvKB & "') "
                    ExecuteTrans(SQL)
                End If
                'cek barang yang ada di barang_banned_do
            Next
            If Cek_Tgl = "Y" Then

                SQL = "select a.Kode_Stock_owner, b.Kode_Kategori_Besar, b.Kode_Kategori_Kecil from barang_banned_do_log2 a , barang b where "
                SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_barang = b.Kode_Barang and "
                SQL = SQL & "a.Kode_Stock_owner = b.Kode_Stock_Owner and a.No_Faktur ='" & ListView1.FocusedItem.Text & "'"
                SQL = SQL & "group by a.Kode_Stock_owner, b.Kode_Kategori_Besar, b.Kode_Kategori_Kecil"
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")

                        For index As Integer = 0 To .Rows.Count - 1

                            SQL = "select Jumlah from barang_banned_do2 "
                            SQL = SQL & "where kode_perusahaan ='" & KodePerusahaan & "' and "
                            SQL = SQL & "kode_stock_owner = '" & .Rows(index).Item("kode_stock_owner") & "' and "
                            SQL = SQL & "Kode_Kategori_Besar ='" & .Rows(index).Item("Kode_Kategori_Besar") & "' and "
                            SQL = SQL & "Kode_Kategori_Kecil ='" & .Rows(index).Item("Kode_Kategori_Kecil") & "' "
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then

                                    If Dr("jumlah") > 0 Then
                                        Dr.Close()
                                        SQL = "update barang_banned_do2 set jumlah = jumlah - 1 where kode_perusahaan ='" & KodePerusahaan & "' and "
                                        SQL = SQL & "kode_stock_owner = '" & .Rows(index).Item("kode_stock_owner") & "' and "
                                        SQL = SQL & "Kode_Kategori_Besar ='" & .Rows(index).Item("Kode_Kategori_Besar") & "' and "
                                        SQL = SQL & "Kode_Kategori_Kecil ='" & .Rows(index).Item("Kode_Kategori_Kecil") & "' "
                                        ExecuteTrans(SQL)
                                    Else
                                        Dr.Close()
                                        CloseConn()
                                        CloseTrans()
                                        MessageBox.Show("LIFE CAT FOOD 20KG & ORI CAT FOOD 20KG harus di ACC Pak Dimas!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                        Exit Sub
                                    End If
                                End If
                            End Using
                        Next
                    End With
                End Using
            End If
            'akhir stenly

            Cmd.Transaction.Commit()

            CloseConn()

        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        get_jam()

        Dim kirim_fcm As Integer = 0

        Try

            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            Dim tampil_msg As Integer = 0
            Dim init_faktur As String = ""
            Dim jns_penjualan As String = ""
            Dim lksi_invoice As String = ""
            Dim lksi_gudang As String = ""

            Dim nama_cst As String = ""
            Dim lama_jt_kredit As Integer = 0
            Dim cust_grup As String = "NULL"
            Dim metode_pot_stock As String = ""
            Dim metode_budgeting As String = ""
            Dim jns_trans As String = ""

            Dim flag_ppn As String = ""
            Dim flag_audit As String = ""
            Dim f_jenis_trans As String = ""
            Dim f_plafon As String = ""
            Dim f_lama_jt As Integer = 0
            Dim f_kd_customer As String = ""
            Dim f_plafon_tunai As Double = 0
            Dim f_cabang_sendiri As String = ""

            Dim kode_helper As String = ""
            If CmbHelper.Enabled = True Then
                kode_helper = "'" & CmbHelper.Text & "'"
            Else
                kode_helper = "NULL"
            End If

            Dim kode_Ekspedisi As String = ""
            Dim Id_Ekspedisi As String = ""
            Dim kode_mbl As String = ""
            Dim kode_driver As String = ""

            If CmbJnsMbl.SelectedIndex = 0 Then 'sendiri
                kode_mbl = CmbMbl.Text
                kode_Ekspedisi = "NULL"
                Id_Ekspedisi = "NULL"
            Else
                kode_mbl = TxtMbl.Text.Trim
                kode_Ekspedisi = "'" & CmbEkspedisi.Text & "'"
                Id_Ekspedisi = "'" & ArrEkspedisi.Item(CmbEkspedisi.SelectedIndex) & "'"
            End If

            If CmbJnsDriver.SelectedIndex = 0 Then 'sendiri
                kode_driver = CmbDriver.Text
            Else
                kode_driver = TxtDriver.Text.Trim
            End If

            If CheckBox4.Checked = True Then
                cust_grup = "'" & ArrCust.Item(ComboBox3.SelectedIndex) & "'"
            Else
                cust_grup = "NULL"
            End If
            ''''DO_New_Sementara

            SQL = "select inisial_faktur from stock_owner  "
            SQL = SQL & "where Kode_Stock_Owner ='" & ListView1.FocusedItem.SubItems(8).Text & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    init_faktur = Dr("inisial_faktur")
                End If
            End Using

            ''----------------------------------------
            ''cek penjualan kalo tgl proforma di atas bulan 2 baru masuk pengecekan barang do
            'Dim cek_tanggal As String
            'SQL = "select tanggal from penjualan "
            'SQL = SQL & "where kode_perusahaan ='" & KodePerusahaan & "' and No_Faktur ='" & ListView1.FocusedItem.Text & "'"
            'Using Dr = OpenTrans(SQL)
            '    If Dr.Read Then

            '        'cek_faktur = Dr("tanggal")
            '        'If Month(Dr("tanggal")) > 2 And Year(Dr("tanggal")) = Year(tgl_skg) Then

            '        'End If
            '        If Dr("tanggal") < "2023-02-01" Then
            '            Dr.Close()
            '            cek_tanggal = "Y"
            '        Else
            '            Dr.Close()
            '            cek_tanggal = "T"
            '        End If
            '    Else
            '        Dr.Close()
            '        cek_tanggal = "T"
            '    End If
            'End Using

            'SQL = "delete barang_banned_do_log where kode_perusahaan='" & KodePerusahaan & "' and no_faktur ='" & ListView1.FocusedItem.Text & "'"
            'ExecuteTrans(SQL)

            'For i As Integer = 0 To DataGridView1.RowCount - 1
            '    Get_Isi_Listview(i)

            '    'insert ke barang_banned_do_log

            '    If LvJmlKrm <> 0 Then

            '        SQL = "insert into barang_banned_do_log (kode_perusahaan,kode_stock_owner,no_faktur,kode_barang)"
            '        SQL = SQL & " values('" & KodePerusahaan & "','" & LvSO & "','" & ListView1.FocusedItem.Text & "','" & LvKB & "') "
            '        ExecuteTrans(SQL)

            '    End If


            '    'cek barang yang ada di barang_banned_do

            'Next

            'If cek_tanggal = "Y" Then
            '    SQL = "select b.Kode_Kategori_Besar, b.Kode_Kategori_Kecil, b.Kode_Stock_Owner from barang_banned_do_log a , barang b where "
            '    SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_barang = b.Kode_Barang and "
            '    SQL = SQL & "a.Kode_Stock_owner = b.Kode_Stock_Owner and a.No_Faktur ='" & ListView1.FocusedItem.Text & "'"
            '    SQL = SQL & "group by b.Kode_Kategori_Besar, b.Kode_Kategori_Kecil, b.Kode_Stock_Owner"
            '    Using Ds = BindingTrans(SQL)
            '        With Ds.Tables("MyTable")

            '            For index As Integer = 0 To .Rows.Count - 1

            '                SQL = "select Jumlah from barang_banned_do "
            '                SQL = SQL & "where kode_perusahaan ='" & KodePerusahaan & "' and "
            '                SQL = SQL & "kode_stock_owner = '" & .Rows(index).Item("kode_stock_owner") & "' and "
            '                SQL = SQL & "Kode_Kategori_Besar ='" & .Rows(index).Item("Kode_Kategori_Besar") & "' and "
            '                SQL = SQL & "Kode_Kategori_Kecil ='" & .Rows(index).Item("Kode_Kategori_Kecil") & "' "
            '                Using Dr = OpenTrans(SQL)
            '                    If Dr.Read Then

            '                        If Dr("jumlah") > 0 Then
            '                            Dr.Close()
            '                            SQL = "update barang_banned_do set jumlah = jumlah - 1 where kode_perusahaan ='" & KodePerusahaan & "' and "
            '                            SQL = SQL & "kode_stock_owner = '" & .Rows(index).Item("kode_stock_owner") & "' and "
            '                            SQL = SQL & "Kode_Kategori_Besar ='" & .Rows(index).Item("Kode_Kategori_Besar") & "' and "
            '                            SQL = SQL & "Kode_Kategori_Kecil ='" & .Rows(index).Item("Kode_Kategori_Kecil") & "' "
            '                            ExecuteTrans(SQL)
            '                        Else
            '                            Dr.Close()
            '                            CloseConn()
            '                            CloseTrans()
            '                            MessageBox.Show("Life Cat Dry & Life Cat 400Gr harus di ACC Pak Dimas!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
            '                            Exit Sub
            '                        End If
            '                        'Else
            '                        '    Dr.Close()
            '                        '    CloseTrans()
            '                        '    CloseConn()
            '                        '    MessageBox.Show("Barang di Lokasi ini tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '                        '    Exit Sub
            '                    End If
            '                End Using

            '            Next

            '        End With
            '    End Using

            'End If
            ''----------------------------

            Dim flag_lagi_opname As String = ""
            SQL = "select flag_opname from stock_owner  "
            SQL = SQL & "where Kode_Stock_Owner ='" & ListView1.FocusedItem.SubItems(8).Text & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    flag_lagi_opname = Dr("flag_opname")
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Lokasi tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            'Pengecekan Member
            'xxxxxxxxxxxxx



            'ListView1.FocusedItem.SubItems(10).Text'
            If flag_lagi_opname = "Y" Then
                Dim bypass_plafon As String = "T"

                SQL = "select c.bypass_plafon, c.lama_jt_new, c.plafon, c.lama_jt, c.jenis_trans, b.plafon_tunai, c.blacklist, a.tanggal, c.flag_audit, a.jenis_transaksi, a.metode_pot_stock, a.metode_budgeting, a.kode_customer, c.max_lama_jt_do, c.nama, a.lokasi, "
                SQL = SQL & "b.flag_by_muat, a.flag_cabang_sendiri, b.inisial_faktur, a.jenis, a.status, "
                SQL = SQL & "a.flag_do_selesai, cast(a.rv as bigint) as rvx "
                SQL = SQL & "from penjualan a, stock_owner b, customers c where "
                SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan And "
                SQL = SQL & "b.kode_perusahaan = c.kode_perusahaan And "
                SQL = SQL & "a.kode_customer = c.kode_customer and "
                SQL = SQL & "a.lokasi = b.kode_stock_owner and a.kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "a.no_faktur = '" & ListView1.FocusedItem.Text & "'"
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Dim cek_blacklist As String = Dr("blacklist")
                        bypass_plafon = Dr("bypass_plafon")

                        lama_jt_kredit = Dr("max_lama_jt_do")
                        init_faktur = Dr("inisial_faktur")
                        jns_penjualan = Dr("jenis")
                        by_muat = Dr("flag_by_muat")
                        lksi_invoice = Dr("lokasi")

                        flag_audit = Dr("flag_audit")
                        nama_cst = Dr("nama")
                        metode_pot_stock = Dr("metode_pot_stock")
                        metode_budgeting = Dr("metode_budgeting")

                        jns_trans = Dr("jenis_transaksi")

                        f_kd_customer = Dr("kode_customer")
                        f_jenis_trans = Dr("jenis_trans")
                        f_plafon = Dr("plafon")
                        f_lama_jt = Dr("lama_jt")
                        f_plafon_tunai = Dr("plafon_tunai")
                        f_cabang_sendiri = Dr("flag_cabang_sendiri")

                        If Dr("jenis") = "R" Then
                            flag_ppn = "T"
                        ElseIf Dr("jenis") = "C" Then
                            flag_ppn = "Y"
                        End If



                        If General_Class.CekNULL(Dr("status")) = "Y" Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan karena transaksi ini sudah dibatalkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        ElseIf General_Class.CekNULL(Dr("flag_do_selesai")) = "Y" Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan karena transaksi ini sudah selesai dibuat DO!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        ElseIf Dr("flag_cabang_sendiri") = "Y" Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan karena transaksi ini bukan reseller!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        ElseIf Dr("rvx") <> Val(ListView1.FocusedItem.SubItems(6).Text) Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan karena faktur ini sudah pernah diubah sebelumnya! Ulangi transaksi ini lagi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                            'ElseIf Format(Dr("tanggal"), "yyyy-MM-dd") <= "2021-12-07" Then
                            '    Dr.Close()
                            '    CloseTrans()
                            '    CloseConn()
                            '    MessageBox.Show("Proses tidak dapat dilanjutkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            '    Exit Sub
                        ElseIf Dr("blacklist") = "Y" Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan karena reseller ini sudah di blacklist!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        End If
                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("No faktur tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If
                End Using

                If f_cabang_sendiri = "T" Then

                    Dim f_ttl_jual_kredit As Double = 0
                    SQL = "select isnull(sum(a.ngrand), 0) as ttl "
                    SQL = SQL & "from do_new a, penjualan b where "
                    SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan and a.no_faktur = b.no_faktur and "
                    SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "b.kode_customer = '" & f_kd_customer & "' and b.jenis_transaksi = 'N' and "
                    SQL = SQL & "a.flag_lunas_do is null and a.status is null"
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            f_ttl_jual_kredit = Dr("ttl")
                        End If
                    End Using

                    Dim f_ttl_retur_kredit As Double = 0
                    SQL = "select isnull(sum(a.ngrand), 0) as ttl from retur_do a, do_new b, penjualan c where "
                    SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and "
                    SQL = SQL & "a.no_do = b.no_do and b.no_faktur = c.no_faktur and a.kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "c.kode_customer = '" & f_kd_customer & "' and c.jenis_transaksi = 'N' and "
                    SQL = SQL & "b.flag_lunas_do is null and a.status is null and b.status is null"
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            f_ttl_retur_kredit = Dr("ttl")
                        End If
                    End Using

                    Dim f_ttl_validasi_kredit As Double = 0
                    'SQL = "select isnull(sum(y.byr), 0) as ttl from val_penj x, detail_val_penj y, penjualan z where "
                    'SQL = SQL & "x.kode_perusahaan = y.kode_perusahaan and y.kode_perusahaan = z.kode_perusahaan and "
                    'SQL = SQL & "x.no_val = y.no_val and y.No_Faktur = z.no_faktur and "
                    'SQL = SQL & "x.kode_perusahaan = '" & KodePerusahaan & "' and "
                    'SQL = SQL & "z.kode_customer = '" & f_kd_customer & "' and "
                    'SQL = SQL & "z.jenis_transaksi = 'N' and "
                    'SQL = SQL & "z.flag_lunas is null and x.status is null"
                    'Using Dr = OpenTrans(SQL)
                    '    If Dr.Read Then
                    '        f_ttl_validasi_kredit = Dr("ttl")
                    '    End If
                    'End Using

                    Dim f_ttl_validasi_do_kredit As Double = 0

                    SQL = "select isnull(sum(y.byr), 0) as ttl from val_do x, detail_val_do y, penjualan z, do_new r where "
                    SQL = SQL & "x.kode_perusahaan = y.kode_perusahaan and y.kode_perusahaan = z.kode_perusahaan and z.kode_perusahaan = r.kode_perusahaan and "
                    SQL = SQL & "x.no_val = y.no_val and y.No_Faktur = r.no_do and z.no_faktur = r.no_faktur and "
                    SQL = SQL & "x.kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "z.kode_customer = '" & f_kd_customer & "' and "
                    SQL = SQL & "z.jenis_transaksi = 'N' and "
                    SQL = SQL & "z.flag_lunas is null and r.Flag_Lunas_DO is null and x.status is null"
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            f_ttl_validasi_do_kredit = Dr("ttl")
                        End If
                    End Using

                    '========tunai 

                    Dim f_ttl_jual_tunai As Double = 0
                    SQL = "select isnull(sum(a.ngrand), 0) as ttl "
                    SQL = SQL & "from do_new a, penjualan b where "
                    SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan and a.no_faktur = b.no_faktur and "
                    SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "b.kode_customer = '" & f_kd_customer & "' and b.jenis_transaksi = 'T' and "
                    SQL = SQL & "a.flag_lunas_do is null and a.status is null"
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            f_ttl_jual_tunai = Dr("ttl")
                        End If
                    End Using

                    Dim f_ttl_retur_tunai As Double = 0
                    SQL = "select isnull(sum(a.ngrand), 0) as ttl from retur_do a, do_new b, penjualan c where "
                    SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and "
                    SQL = SQL & "a.no_do = b.no_do and b.no_faktur = c.no_faktur and a.kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "c.kode_customer = '" & f_kd_customer & "' and c.jenis_transaksi = 'T' and "
                    SQL = SQL & "b.flag_lunas_do is null and a.status is null and b.status is null"
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            f_ttl_retur_tunai = Dr("ttl")
                        End If
                    End Using

                    Dim f_ttl_validasi_tunai As Double = 0
                    'SQL = "select isnull(sum(y.byr + y.disc_cash), 0) as ttl from val_penj_tunai x, detail_val_penj_tunai y, penjualan z where "
                    'SQL = SQL & "x.kode_perusahaan = y.kode_perusahaan and y.kode_perusahaan = z.kode_perusahaan and "
                    'SQL = SQL & "x.no_val = y.no_val and y.No_Faktur = z.no_faktur and "
                    'SQL = SQL & "x.kode_perusahaan = '" & KodePerusahaan & "' and "
                    'SQL = SQL & "z.kode_customer = '" & f_kd_customer & "' and "
                    'SQL = SQL & "z.jenis_transaksi = 'T' and "
                    'SQL = SQL & "z.flag_lunas_tunai is null and x.status is null"
                    'Using Dr = OpenTrans(SQL)
                    '    If Dr.Read Then
                    '        f_ttl_validasi_tunai = Dr("ttl")
                    '    End If
                    'End Using

                    Dim f_ttl_validasi_do_tunai As Double = 0
                    SQL = "select isnull(sum(y.byr + y.disc_cash), 0) as ttl from val_do_tunai x, detail_val_do_tunai y, penjualan z, do_new r where "
                    SQL = SQL & "x.kode_perusahaan = y.kode_perusahaan and y.kode_perusahaan = z.kode_perusahaan and z.kode_perusahaan = r.kode_perusahaan and "
                    SQL = SQL & "x.no_val = y.no_val and y.No_Faktur = r.no_do and z.no_faktur = r.no_faktur and "
                    SQL = SQL & "x.kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "z.kode_customer = '" & f_kd_customer & "' and "
                    SQL = SQL & "z.jenis_transaksi = 'T' and "
                    SQL = SQL & "z.flag_lunas_tunai is null and r.Flag_Lunas_DO is null and x.status is null"
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            f_ttl_validasi_do_tunai = Dr("ttl")
                        End If
                    End Using

                    '============
                    Dim xxxxxxx As Double = (f_ttl_jual_kredit - f_ttl_retur_kredit - f_ttl_validasi_kredit - f_ttl_validasi_do_kredit) + (f_ttl_jual_tunai - f_ttl_retur_tunai - f_ttl_validasi_tunai - f_ttl_validasi_do_tunai) + Val(HilangkanTanda(TxtTotal.Text))

                    If (f_ttl_jual_kredit - f_ttl_retur_kredit - f_ttl_validasi_kredit - f_ttl_validasi_do_kredit) + (f_ttl_jual_tunai - f_ttl_retur_tunai - f_ttl_validasi_tunai - f_ttl_validasi_do_tunai) + Val(HilangkanTanda(TxtTotal.Text)) > f_plafon Then

                        If bypass_plafon = "T" Then
                            SQL = "select Total_Transaksi, Urut, Validasi_ACC from Plafon_ACC_DO where "
                            SQL = SQL & "No_Faktur = '" & ListView1.FocusedItem.Text & "' and (Validasi_ACC is null or Validasi_ACC <>'T') and Pakai is null "
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then

                                    If General_Class.CekNULL(Dr("Validasi_ACC")) = "" Then
                                        Dr.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Plafon Masih Menunggu DI ACC!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    ElseIf General_Class.CekNULL(Dr("Validasi_ACC")) = "T" Then
                                        Dr.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Plafon Tidak DI ACC!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    ElseIf Dr("Total_Transaksi") <> HilangkanTanda(Format((f_ttl_jual_kredit - f_ttl_retur_kredit - f_ttl_validasi_kredit - f_ttl_validasi_do_kredit) + (f_ttl_jual_tunai - f_ttl_retur_tunai - f_ttl_validasi_tunai - f_ttl_validasi_do_tunai) + Val(HilangkanTanda(TxtTotal.Text)), "N0")) Then

                                        SQL = "Update Plafon_Acc_DO set Pakai = 'T' "
                                        SQL = SQL & "Where Kode_Perusahaan = '" & KodePerusahaan & "' and Urut = '" & Dr("Urut") & "' "
                                        Dr.Close()
                                        CloseTrans()
                                        ExecuteTrans(SQL)
                                        CloseConn()
                                        MessageBox.Show("Terdapat perbedaan Total Dalam List ACC! Silahkan ACC Ulang.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If

                                    SQL = "Update Plafon_Acc_DO set Pakai = 'Y' "
                                    SQL = SQL & "Where Kode_Perusahaan = '" & KodePerusahaan & "' and Urut = '" & Dr("Urut") & "' "
                                    Dr.Close()
                                    ExecuteTrans(SQL)
                                Else
                                    Dr.Close()
                                    CloseTrans()

                                    'MessageBox.Show("Customer ini masih ada PIUTANG TUNAI yang belum dilunasi! Lunasi dahulu agar transaksi bisa dilanjutkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                                    'Dim tny As String = MessageBox.Show("Ajukan Validasi Plafon? ", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
                                    'If tny = vbNo Then
                                    '    Exit Sub
                                    'Else
                                    '    OpenConn()
                                    Cmd.Transaction = Cn.BeginTransaction

                                    SQL = "Insert Into Plafon_Acc_DO (Kode_Perusahaan, No_Faktur, Lokasi, Kode_Customer, Tanggal, Jam, UserID, Plafon, Total_Transaksi, Kekurangan) "
                                    SQL = SQL & "Values ('" & KodePerusahaan & "', '" & ListView1.FocusedItem.Text & "', '" & lksi_invoice & "', '" & f_kd_customer & "', "
                                    SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "','" & UserID & "', "
                                    SQL = SQL & "" & HilangkanTanda(Format(Val(f_plafon), "N0")) & ", " & HilangkanTanda(Format((f_ttl_jual_kredit - f_ttl_retur_kredit - f_ttl_validasi_kredit - f_ttl_validasi_do_kredit) + (f_ttl_jual_tunai - f_ttl_retur_tunai - f_ttl_validasi_tunai - f_ttl_validasi_do_tunai) + Val(HilangkanTanda(TxtTotal.Text)), "N0")) & ", "
                                    SQL = SQL & "" & HilangkanTanda(Format((f_ttl_jual_kredit - f_ttl_retur_kredit - f_ttl_validasi_kredit - f_ttl_validasi_do_kredit) + (f_ttl_jual_tunai - f_ttl_retur_tunai - f_ttl_validasi_tunai - f_ttl_validasi_do_tunai) + Val(HilangkanTanda(TxtTotal.Text)) - Val(f_plafon), "N0")) & ") "
                                    ExecuteTrans(SQL)

                                    Cmd.Transaction.Commit()
                                    CloseConn()

                                    MessageBox.Show("Customer ini masih ada piutang TUNAI + KREDIT yang belum dilunasi! Lunasi dahulu atau Minta ACC HO agar transaksi bisa dilanjutkan! " & Chr(13) _
                                                   & "Tunai = Rp. " & Format((f_ttl_jual_tunai - f_ttl_retur_tunai - f_ttl_validasi_tunai - f_ttl_validasi_do_tunai), "N0") & Chr(13) _
                                                   & "Kredit = Rp. " & Format((f_ttl_jual_kredit - f_ttl_retur_kredit - f_ttl_validasi_kredit - f_ttl_validasi_do_kredit), "N0") & Chr(13) _
                                                   & "Transaksi Ini = " & Format(Val(HilangkanTanda(TxtTotal.Text)), "N0") & Chr(13) _
                                                   & "Total = " & Format((f_ttl_jual_kredit - f_ttl_retur_kredit - f_ttl_validasi_kredit - f_ttl_validasi_do_kredit) + (f_ttl_jual_tunai - f_ttl_retur_tunai - f_ttl_validasi_tunai - f_ttl_validasi_do_tunai) + Val(HilangkanTanda(TxtTotal.Text)), "N0") & Chr(13) _
                                                   & "Plafon = " & Format(Val(f_plafon), "N0") & Chr(13) _
                                                   & "Kekurangan = " & Format((f_ttl_jual_kredit - f_ttl_retur_kredit - f_ttl_validasi_kredit - f_ttl_validasi_do_kredit) + (f_ttl_jual_tunai - f_ttl_retur_tunai - f_ttl_validasi_tunai - f_ttl_validasi_do_tunai) + Val(HilangkanTanda(TxtTotal.Text)) - Val(f_plafon), "N0") _
                                                   , Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                                    Exit Sub
                                    'End If
                                End If
                            End Using
                        End If
                    End If

                    '    Else

                    '        CloseTrans()
                    '        CloseConn()
                    '        MessageBox.Show("Ada kesalahan pada jenis transaksi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '        Exit Sub
                    '    End If
                    'End If
                End If

                SQL = "select inisial_faktur from stock_owner  "
                SQL = SQL & "where Kode_Stock_Owner ='" & ListView1.FocusedItem.SubItems(8).Text & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        init_faktur = Dr("inisial_faktur")
                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Lokasi tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using


                nofakdoOPM = fDOOPM & init_faktur & "-" & Format(tgl_skg, "MM/yy") & "-" &
                                           General_Class.Get_Last_Number2("do_new_sementara", "no_sementara", JumlahDigit,
                                           "Kode_perusahaan", KodePerusahaan,
                                           "And", "substring(no_sementara,1," & Len(fDOOPM) + Len(init_faktur) + 6 & ")", fDOOPM & init_faktur & "-" & Format(tgl_skg, "MM/yy"))


                SQL = "SELECT top(1) datediff(mi, Tanggal+Jam,'" & Format(tgl_skg, "yyyy-MM-dd HH:mm:ss") & "') as selisih from do_new_sementara "
                SQL = SQL & "where No_Faktur = '" & ListView1.FocusedItem.Text & "' and Kode_Perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "batal is null and Flag_Sudah_Validasi_Auditor is null "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        If Dr("Selisih") > lama_expire_opname Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Terjadi Kesalahan, Silahkan Refresh!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End If
                End Using

                SQL = "select flag_sementara_saat_opm, Flag_Do_Selesai from penjualan "
                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "No_faktur = '" & ListView1.FocusedItem.Text & "'" '
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        If Not IsDBNull(Dr("flag_sementara_saat_opm")) Or Not IsDBNull(Dr("Flag_Do_Selesai")) Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Terjadi Kesalahan, Silahkan Refresh!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Terjadi Kesalahan, Silahkan Refresh!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                'kode_helper = "1"
                SQL = "select Flag_Sudah_Validasi_Auditor, Batal from Do_New_Sementara "
                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "No_Faktur = '" & ListView1.FocusedItem.Text & "' and "
                SQL = SQL & "batal is null and Flag_Sudah_Validasi_Auditor is null "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Terjadi Kesalahan, Silahkan Refresh!!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                SQL = "insert into do_new_sementara(kode_perusahaan, no_sementara, kode_helper, tanggal, jam, userid, "
                SQL = SQL & "no_faktur, jenis_kendaraan, kode_kendaraan, jenis_driver, "
                SQL = SQL & "kode_driver, tujuan, hp, keterangan, Kode_Cust_Group, x_termxx, Kode_Ekspedisi, Id_Gudang, Id_Ekspedisi) Values("
                SQL = SQL & "'" & KodePerusahaan & "', '" & nofakdoOPM & "', " & kode_helper & ", "
                SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', "
                SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "', "
                SQL = SQL & "'" & UserID & "', '" & ListView1.FocusedItem.Text & "', "
                SQL = SQL & "'" & CmbJnsMbl.Text & "', '" & kode_mbl & "', '" & CmbJnsDriver.Text & "', "
                SQL = SQL & "'" & kode_driver & "', '" & TextBox3.Text.Trim & "', "
                SQL = SQL & "'" & TextBox4.Text.Trim & "', '" & TextBox5.Text.Trim & "', " & cust_grup & ", 'x'," & kode_Ekspedisi & ", '" & Id_Gudang & "', " & Id_Ekspedisi & ")"
                ExecuteTrans(SQL)

                For i As Integer = 0 To DataGridView1.RowCount - 1
                    Get_Isi_Listview(i)
                    If Val(HilangkanTanda(LvJmlKrm)) <> 0 Then
                        SQL = "insert into detail_do_new_sementara(kode_perusahaan, no_sementara, no_urut, "
                        SQL = SQL & "kode_stock_owner, kode_barang, jumlah, hrg_muat, total_muat) Values( "
                        SQL = SQL & "'" & KodePerusahaan & "', '" & nofakdoOPM & "', "
                        SQL = SQL & "'" & LvUrut & "', "
                        SQL = SQL & "'" & LvSO & "', "
                        SQL = SQL & "'" & LvKB & "', "
                        SQL = SQL & "'" & HilangkanTanda(LvJmlKrm) & "', "
                        SQL = SQL & "'" & HilangkanTanda(LvHrgMuat) & "', "
                        SQL = SQL & "'" & HilangkanTanda(LvTtlMuat) & "') "
                        ExecuteTrans(SQL)
                    End If
                Next

                'SQL = "select B.Nama, Sum(A.Jumlah) as Jumlah, B.Good_Stock from detail_DO_New_sementara A, Barang B where "
                'SQL = SQL & "A.Kode_Perusahaan = B.Kode_Perusahaan And A.Kode_Barang = B.Kode_Barang And A.Kode_Stock_Owner = B.Kode_Stock_Owner and "
                'SQL = SQL & "A.Kode_Perusahaan = '" & KodePerusahaan & "' and no_sementara =  '" & nofakdoOPM & "' group by B.Nama, B.Good_Stock "
                'Using Dr = OpenTrans(SQL)
                '    Do While Dr.Read
                '        If Dr("Jumlah") > Dr("Good_Stock") Then
                '            Dr.Close()
                '            CloseTrans()
                '            CloseConn()
                '            MessageBox.Show("Jml kirim lebih besar dari stock!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '            Exit Sub
                '        End If
                '    Loop
                'End Using

                tampil_msg = 1
                kirim_fcm = 1

            Else

                '''''''''''''''''''''''''''''


                Dim lama_jt_new As Integer = 0
                Dim bypass_plafon As String = "T"

                SQL = "select c.bypass_plafon, c.lama_jt_new, c.plafon, c.lama_jt, c.jenis_trans, b.plafon_tunai, c.blacklist, a.tanggal, "
                SQL = SQL & "c.flag_audit, a.jenis_transaksi, a.metode_pot_stock, a.metode_budgeting, "
                SQL = SQL & "a.kode_customer, c.max_lama_jt_do, c.nama, a.lokasi, "
                SQL = SQL & "b.flag_by_muat, a.flag_cabang_sendiri, b.inisial_faktur, a.jenis, a.status, "
                SQL = SQL & "a.flag_do_selesai, cast(a.rv as bigint) as rvx "
                SQL = SQL & "from penjualan a, stock_owner b, customers c where "
                SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan And "
                SQL = SQL & "b.kode_perusahaan = c.kode_perusahaan And "
                SQL = SQL & "a.kode_customer = c.kode_customer and "
                SQL = SQL & "a.lokasi = b.kode_stock_owner and a.kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "a.no_faktur = '" & ListView1.FocusedItem.Text & "'"
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Dim cek_blacklist As String = Dr("blacklist")
                        bypass_plafon = Dr("bypass_plafon")

                        lama_jt_kredit = Dr("max_lama_jt_do")
                        init_faktur = Dr("inisial_faktur")
                        jns_penjualan = Dr("jenis")
                        by_muat = Dr("flag_by_muat")
                        lksi_invoice = Dr("lokasi")
                        lama_jt_new = Dr("lama_jt_new")

                        flag_audit = Dr("flag_audit")
                        nama_cst = Dr("nama")
                        metode_pot_stock = Dr("metode_pot_stock")
                        metode_budgeting = Dr("metode_budgeting")

                        jns_trans = Dr("jenis_transaksi")

                        f_kd_customer = Dr("kode_customer")
                        f_jenis_trans = Dr("jenis_trans")
                        f_plafon = Dr("plafon")
                        f_lama_jt = Dr("lama_jt")
                        f_plafon_tunai = Dr("plafon_tunai")
                        f_cabang_sendiri = Dr("flag_cabang_sendiri")

                        If Dr("jenis") = "R" Then
                            flag_ppn = "T"
                        ElseIf Dr("jenis") = "C" Then
                            flag_ppn = "Y"
                        End If



                        If General_Class.CekNULL(Dr("status")) = "Y" Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan karena transaksi ini sudah dibatalkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        ElseIf General_Class.CekNULL(Dr("flag_do_selesai")) = "Y" Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan karena transaksi ini sudah selesai dibuat DO!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        ElseIf Dr("flag_cabang_sendiri") = "Y" Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan karena transaksi ini bukan reseller!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        ElseIf Dr("rvx") <> Val(ListView1.FocusedItem.SubItems(6).Text) Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan karena faktur ini sudah pernah diubah sebelumnya! Ulangi transaksi ini lagi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                            'ElseIf Format(Dr("tanggal"), "yyyy-MM-dd") <= "2021-12-07" Then
                            '    Dr.Close()
                            '    CloseTrans()
                            '    CloseConn()
                            '    MessageBox.Show("Proses tidak dapat dilanjutkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            '    Exit Sub
                        ElseIf Dr("blacklist") = "Y" Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan karena reseller ini sudah di blacklist!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        End If
                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("No faktur tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If
                End Using

                Dim total As Double = 0
                Dim nilai_ppn As Double = 0
                Dim grandttl As Double = 0



                For i As Integer = 0 To DataGridView1.RowCount - 1
                    Get_Isi_Listview(i)

                    SQL = "Select b.Satuan, b.Kode_Satuan_Besar, b.Isi_Satuan_Besar, a.kode_stock_owner, a.Kode_barang, b.nama, a.keterangan, a.jumlah, b.satuan, a.no_urut, "

                    SQL = SQL & "isnull((select sum(y.good_stock + y.bad_stock) from "
                    SQL = SQL & "retur_penjualan x, detail_r_penjualan y where "
                    SQL = SQL & "x.kode_perusahaan = y.kode_perusahaan and x.no_retur_jual = y.no_retur_jual and "
                    SQL = SQL & "x.kode_perusahaan = a.kode_perusahaan And "
                    SQL = SQL & "x.status is null and y.urut = a.no_urut and x.no_faktur_jual = a.no_faktur and "
                    SQL = SQL & "x.No_Retur_DO is null and x.No_DO_Dari_Validasi is null), 0) as rtr, "

                    SQL = SQL & "isnull((select sum(y.jumlah) from "
                    SQL = SQL & "do_new x, detail_do_new y where "
                    SQL = SQL & "x.kode_perusahaan = y.kode_perusahaan and x.no_do = y.no_do and "
                    SQL = SQL & "x.kode_perusahaan = a.kode_perusahaan And "
                    SQL = SQL & "x.status is null and y.hasil is null and y.no_urut = a.no_urut and x.no_faktur = a.no_faktur), 0) as sedang_kirim, "

                    SQL = SQL & "isnull((select -sum(y.jml_terima - y.jumlah) from "
                    SQL = SQL & "do_new x, detail_do_new y where "
                    SQL = SQL & "x.kode_perusahaan = y.kode_perusahaan and x.no_do = y.no_do and "
                    SQL = SQL & "x.kode_perusahaan = a.kode_perusahaan And "
                    SQL = SQL & "x.status is null and y.jml_terima - y.jumlah < 0 and "
                    SQL = SQL & "y.hasil = 1 and y.no_urut = a.no_urut and x.no_faktur = a.no_faktur), 0) as kurang_kirim_approve, "

                    SQL = SQL & "isnull((select -sum(y.jml_terima - y.jumlah) from "
                    SQL = SQL & "do_new x, detail_do_new y where "
                    SQL = SQL & "x.kode_perusahaan = y.kode_perusahaan and x.no_do = y.no_do and "
                    SQL = SQL & "x.kode_perusahaan = a.kode_perusahaan And "
                    SQL = SQL & "x.status is null and y.jml_terima - y.jumlah < 0 and "
                    SQL = SQL & "y.hasil = 6 and y.no_urut = a.no_urut and x.no_faktur = a.no_faktur), 0) as retur_do, "

                    SQL = SQL & "isnull((select sum((case when y.hasil in('2', '3', '4') then y.jumlah else y.jml_terima end)) from "
                    SQL = SQL & "do_new x, detail_do_new y where "
                    SQL = SQL & "x.kode_perusahaan = y.kode_perusahaan and x.no_do = y.no_do and "
                    SQL = SQL & "x.kode_perusahaan = a.kode_perusahaan And "
                    SQL = SQL & "x.status is null and "
                    SQL = SQL & "y.hasil is not null and y.no_urut = a.no_urut and x.no_faktur = a.no_faktur), 0) as sdh_selesai_validasi "

                    '1= untuk jml terima kurang dr jml kirim & di setujui
                    '2= untuk jml terima kurang dr jml kirim & tdk setuju
                    '3=untuk jml terima lebih dr jml kirim & cetak invoice
                    '4=untuk jml terima lebih dr jml kirim & input tgl tarik balik
                    '5=beres
                    SQL = SQL & "from detail_penjualan a, barang b where "
                    SQL = SQL & "a.kode_perusahaan = b.kode_Perusahaan and a.kode_barang = b.kode_barang and "
                    SQL = SQL & "a.kode_stock_owner = b.kode_stock_owner and "
                    SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "a.no_faktur = '" & ListView1.FocusedItem.Text & "' and "
                    SQL = SQL & "a.no_urut = '" & LvUrut & "' order by b.nama"
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            lksi_gudang = Dr("kode_stock_owner")

                            'If Dr("jumlah") - Dr("rtr") - Dr("sedang_kirim") - Dr("sdh_selesai_validasi") - Dr("retur_do") - Val(lvjmlkrm) < 0 Then
                            If Dr("jumlah") - Dr("rtr") - Dr("sedang_kirim") - Dr("sdh_selesai_validasi") - Dr("retur_do") - Val(LvJmlKrm) < 0 Then
                                Dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show(LvNm & " melebihi batas pengiriman!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        Else
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Detail barang tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using

                    total = total + Val(HilangkanTanda(LvSubttl))
                Next

                TextBox17.Text = Format(total, "N0")
                nilai_ppn = total * Val(TextBox18.Text) / 100
                nilai_ppn = Val(HilangkanTanda(Format(nilai_ppn, "N0")))
                TextBox19.Text = Format(nilai_ppn, "N0")

                grandttl = total + nilai_ppn
                TxtTotal.Text = Format(grandttl, "N0")


                '=====================


                If f_cabang_sendiri = "T" Then

                    Dim f_ttl_jual_kredit As Double = 0
                    SQL = "select isnull(sum(a.ngrand), 0) as ttl "
                    SQL = SQL & "from do_new a, penjualan b where "
                    SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan and a.no_faktur = b.no_faktur and "
                    SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "b.kode_customer = '" & f_kd_customer & "' and b.jenis_transaksi = 'N' and "
                    SQL = SQL & "a.flag_lunas_do is null and a.status is null"
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            f_ttl_jual_kredit = Dr("ttl")
                        End If
                    End Using

                    Dim f_ttl_retur_kredit As Double = 0
                    SQL = "select isnull(sum(a.ngrand), 0) as ttl from retur_do a, do_new b, penjualan c where "
                    SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and "
                    SQL = SQL & "a.no_do = b.no_do and b.no_faktur = c.no_faktur and a.kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "c.kode_customer = '" & f_kd_customer & "' and c.jenis_transaksi = 'N' and "
                    SQL = SQL & "b.flag_lunas_do is null and a.status is null and b.status is null"
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            f_ttl_retur_kredit = Dr("ttl")
                        End If
                    End Using

                    Dim f_ttl_validasi_kredit As Double = 0
                    'SQL = "select isnull(sum(y.byr), 0) as ttl from val_penj x, detail_val_penj y, penjualan z where "
                    'SQL = SQL & "x.kode_perusahaan = y.kode_perusahaan and y.kode_perusahaan = z.kode_perusahaan and "
                    'SQL = SQL & "x.no_val = y.no_val and y.No_Faktur = z.no_faktur and "
                    'SQL = SQL & "x.kode_perusahaan = '" & KodePerusahaan & "' and "
                    'SQL = SQL & "z.kode_customer = '" & f_kd_customer & "' and "
                    'SQL = SQL & "z.jenis_transaksi = 'N' and "
                    'SQL = SQL & "z.flag_lunas is null and x.status is null"
                    'Using Dr = OpenTrans(SQL)
                    '    If Dr.Read Then
                    '        f_ttl_validasi_kredit = Dr("ttl")
                    '    End If
                    'End Using

                    Dim f_ttl_validasi_do_kredit As Double = 0

                    SQL = "select isnull(sum(y.byr), 0) as ttl from val_do x, detail_val_do y, penjualan z, do_new r where "
                    SQL = SQL & "x.kode_perusahaan = y.kode_perusahaan and y.kode_perusahaan = z.kode_perusahaan and z.kode_perusahaan = r.kode_perusahaan and "
                    SQL = SQL & "x.no_val = y.no_val and y.No_Faktur = r.no_do and z.no_faktur = r.no_faktur and "
                    SQL = SQL & "x.kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "z.kode_customer = '" & f_kd_customer & "' and "
                    SQL = SQL & "z.jenis_transaksi = 'N' and "
                    SQL = SQL & "z.flag_lunas is null and r.Flag_Lunas_DO is null and x.status is null"
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            f_ttl_validasi_do_kredit = Dr("ttl")
                        End If
                    End Using

                    '========tunai 

                    Dim f_ttl_jual_tunai As Double = 0
                    SQL = "select isnull(sum(a.ngrand), 0) as ttl "
                    SQL = SQL & "from do_new a, penjualan b where "
                    SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan and a.no_faktur = b.no_faktur and "
                    SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "b.kode_customer = '" & f_kd_customer & "' and b.jenis_transaksi = 'T' and "
                    SQL = SQL & "a.flag_lunas_do is null and a.status is null"
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            f_ttl_jual_tunai = Dr("ttl")
                        End If
                    End Using

                    Dim f_ttl_retur_tunai As Double = 0
                    SQL = "select isnull(sum(a.ngrand), 0) as ttl from retur_do a, do_new b, penjualan c where "
                    SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and "
                    SQL = SQL & "a.no_do = b.no_do and b.no_faktur = c.no_faktur and a.kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "c.kode_customer = '" & f_kd_customer & "' and c.jenis_transaksi = 'T' and "
                    SQL = SQL & "b.flag_lunas_do is null and a.status is null and b.status is null"
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            f_ttl_retur_tunai = Dr("ttl")
                        End If
                    End Using

                    Dim f_ttl_validasi_tunai As Double = 0
                    'SQL = "select isnull(sum(y.byr + y.disc_cash), 0) as ttl from val_penj_tunai x, detail_val_penj_tunai y, penjualan z where "
                    'SQL = SQL & "x.kode_perusahaan = y.kode_perusahaan and y.kode_perusahaan = z.kode_perusahaan and "
                    'SQL = SQL & "x.no_val = y.no_val and y.No_Faktur = z.no_faktur and "
                    'SQL = SQL & "x.kode_perusahaan = '" & KodePerusahaan & "' and "
                    'SQL = SQL & "z.kode_customer = '" & f_kd_customer & "' and "
                    'SQL = SQL & "z.jenis_transaksi = 'T' and "
                    'SQL = SQL & "z.flag_lunas_tunai is null and x.status is null"
                    'Using Dr = OpenTrans(SQL)
                    '    If Dr.Read Then
                    '        f_ttl_validasi_tunai = Dr("ttl")
                    '    End If
                    'End Using

                    Dim f_ttl_validasi_do_tunai As Double = 0
                    SQL = "select isnull(sum(y.byr + y.disc_cash), 0) as ttl from val_do_tunai x, detail_val_do_tunai y, penjualan z, do_new r where "
                    SQL = SQL & "x.kode_perusahaan = y.kode_perusahaan and y.kode_perusahaan = z.kode_perusahaan and z.kode_perusahaan = r.kode_perusahaan and "
                    SQL = SQL & "x.no_val = y.no_val and y.No_Faktur = r.no_do and z.no_faktur = r.no_faktur and "
                    SQL = SQL & "x.kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "z.kode_customer = '" & f_kd_customer & "' and "
                    SQL = SQL & "z.jenis_transaksi = 'T' and "
                    SQL = SQL & "z.flag_lunas_tunai is null and r.Flag_Lunas_DO is null and x.status is null"
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            f_ttl_validasi_do_tunai = Dr("ttl")
                        End If
                    End Using

                    '============
                    If (f_ttl_jual_kredit - f_ttl_retur_kredit - f_ttl_validasi_kredit - f_ttl_validasi_do_kredit) + (f_ttl_jual_tunai - f_ttl_retur_tunai - f_ttl_validasi_tunai - f_ttl_validasi_do_tunai) + Val(HilangkanTanda(TxtTotal.Text)) > f_plafon Then
                        If bypass_plafon = "T" Then

                            SQL = "select Total_Transaksi, Urut, Validasi_ACC from Plafon_ACC_DO where "
                            SQL = SQL & "No_Faktur = '" & ListView1.FocusedItem.Text & "' and (Validasi_ACC is null or Validasi_ACC <>'T') and Pakai is null "
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then

                                    If General_Class.CekNULL(Dr("Validasi_ACC")) = "" Then
                                        Dr.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Plafon Masih Menunggu DI ACC!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    ElseIf General_Class.CekNULL(Dr("Validasi_ACC")) = "T" Then
                                        Dr.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Plafon Tidak DI ACC!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    ElseIf Dr("Total_Transaksi") <> HilangkanTanda(Format((f_ttl_jual_kredit - f_ttl_retur_kredit - f_ttl_validasi_kredit - f_ttl_validasi_do_kredit) + (f_ttl_jual_tunai - f_ttl_retur_tunai - f_ttl_validasi_tunai - f_ttl_validasi_do_tunai) + Val(HilangkanTanda(TxtTotal.Text)), "N0")) Then

                                        SQL = "Update Plafon_Acc_DO set Pakai = 'T' "
                                        SQL = SQL & "Where Kode_Perusahaan = '" & KodePerusahaan & "' and Urut = '" & Dr("Urut") & "' "
                                        Dr.Close()
                                        CloseTrans()
                                        ExecuteTrans(SQL)
                                        CloseConn()
                                        MessageBox.Show("Terdapat perbedaan Total Dalam List ACC! Silahkan ACC Ulang.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If

                                    SQL = "Update Plafon_Acc_DO set Pakai = 'Y' "
                                    SQL = SQL & "Where Kode_Perusahaan = '" & KodePerusahaan & "' and Urut = '" & Dr("Urut") & "' "
                                    Dr.Close()
                                    ExecuteTrans(SQL)
                                Else
                                    Dr.Close()
                                    CloseTrans()

                                    'MessageBox.Show("Customer ini masih ada PIUTANG TUNAI yang belum dilunasi! Lunasi dahulu agar transaksi bisa dilanjutkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                                    'Dim tny As String = MessageBox.Show("Ajukan Validasi Plafon? ", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
                                    'If tny = vbNo Then
                                    '    Exit Sub
                                    'Else
                                    '    OpenConn()
                                    Cmd.Transaction = Cn.BeginTransaction

                                    SQL = "Insert Into Plafon_Acc_DO (Kode_Perusahaan, No_Faktur, Lokasi, Kode_Customer, Tanggal, Jam, UserID, Plafon, Total_Transaksi, Kekurangan) "
                                    SQL = SQL & "Values ('" & KodePerusahaan & "', '" & ListView1.FocusedItem.Text & "', '" & lksi_invoice & "', '" & f_kd_customer & "', "
                                    SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "','" & UserID & "', "
                                    SQL = SQL & "" & HilangkanTanda(Format(Val(f_plafon), "N0")) & ", " & HilangkanTanda(Format((f_ttl_jual_kredit - f_ttl_retur_kredit - f_ttl_validasi_kredit - f_ttl_validasi_do_kredit) + (f_ttl_jual_tunai - f_ttl_retur_tunai - f_ttl_validasi_tunai - f_ttl_validasi_do_tunai) + Val(HilangkanTanda(TxtTotal.Text)), "N0")) & ", "
                                    SQL = SQL & "" & HilangkanTanda(Format((f_ttl_jual_kredit - f_ttl_retur_kredit - f_ttl_validasi_kredit - f_ttl_validasi_do_kredit) + (f_ttl_jual_tunai - f_ttl_retur_tunai - f_ttl_validasi_tunai - f_ttl_validasi_do_tunai) + Val(HilangkanTanda(TxtTotal.Text)) - Val(f_plafon), "N0")) & ") "
                                    ExecuteTrans(SQL)

                                    Cmd.Transaction.Commit()
                                    CloseConn()

                                    MessageBox.Show("Customer ini masih ada piutang TUNAI + KREDIT yang belum dilunasi! Lunasi dahulu atau Minta ACC HO agar transaksi bisa dilanjutkan! " & Chr(13) _
                                                   & "Tunai = Rp. " & Format((f_ttl_jual_tunai - f_ttl_retur_tunai - f_ttl_validasi_tunai - f_ttl_validasi_do_tunai), "N0") & Chr(13) _
                                                   & "Kredit = Rp. " & Format((f_ttl_jual_kredit - f_ttl_retur_kredit - f_ttl_validasi_kredit - f_ttl_validasi_do_kredit), "N0") & Chr(13) _
                                                   & "Transaksi Ini = " & Format(Val(HilangkanTanda(TxtTotal.Text)), "N0") & Chr(13) _
                                                   & "Total = " & Format((f_ttl_jual_kredit - f_ttl_retur_kredit - f_ttl_validasi_kredit - f_ttl_validasi_do_kredit) + (f_ttl_jual_tunai - f_ttl_retur_tunai - f_ttl_validasi_tunai - f_ttl_validasi_do_tunai) + Val(HilangkanTanda(TxtTotal.Text)), "N0") & Chr(13) _
                                                   & "Plafon = " & Format(Val(f_plafon), "N0") & Chr(13) _
                                                   & "Kekurangan = " & Format((f_ttl_jual_kredit - f_ttl_retur_kredit - f_ttl_validasi_kredit - f_ttl_validasi_do_kredit) + (f_ttl_jual_tunai - f_ttl_retur_tunai - f_ttl_validasi_tunai - f_ttl_validasi_do_tunai) + Val(HilangkanTanda(TxtTotal.Text)) - Val(f_plafon), "N0") _
                                                   , Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                                    Exit Sub
                                    'End If
                                End If
                            End Using
                        End If
                    End If

                    '    Else

                    '        CloseTrans()
                    '        CloseConn()
                    '        MessageBox.Show("Ada kesalahan pada jenis transaksi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '        Exit Sub
                    '    End If
                    'End If
                End If

                '=====================


                Dim kebrp As Integer = 0

                SQL = "Select top 1 ke from do_new_list_2 Where "
                SQL = SQL & "Kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "no_faktur = '" & ListView1.FocusedItem.Text & "' "
                SQL = SQL & "order by no_faktur"
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        ' sdh_ada = "Y"
                        ' nofakdo = Dr("no_do")
                        kebrp = Dr("ke") + 1

                        Dr.Close()

                        SQL = "update do_new_list_2 set ke = " & kebrp & " where kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "no_faktur = '" & ListView1.FocusedItem.Text & "'"
                        ExecuteTrans(SQL)
                    Else
                        Dr.Close()
                        kebrp = 1
                        SQL = "insert into do_new_list_2(kode_perusahaan, no_faktur, ke) values("
                        SQL = SQL & "'" & KodePerusahaan & "', '" & ListView1.FocusedItem.Text & "', 1)"
                        ExecuteTrans(SQL)
                    End If
                End Using

                nofakdo = fDONEW2 & jns_penjualan & init_faktur & "-" & Format(tgl_skg, "MM/yy") & "-" &
                                               General_Class.Get_Last_Number2("do_new", "no_do", JumlahDigit,
                                               "Kode_perusahaan", KodePerusahaan,
                                               "And", "substring(no_do,1," & Len(fDONEW2 & jns_penjualan) + Len(init_faktur) + 6 & ")", fDONEW2 & jns_penjualan & init_faktur & "-" & Format(tgl_skg, "MM/yy"))

                Dim tgl_jt As String = Format(DateAdd(DateInterval.Day, lama_jt_kredit, tgl_skg), "yyyy-MM-dd")
                Dim new_lama_jt As Integer = 0

                If Val(Strings.Right(tgl_jt, 2)) = 31 Then
                    If Val(Strings.Mid(tgl_jt, 6, 2)) = Val(Format(tgl_skg, "MM")) Then
                        new_lama_jt = lama_jt_kredit + 1
                    Else
                        new_lama_jt = lama_jt_kredit
                    End If
                Else
                    new_lama_jt = lama_jt_kredit
                End If

                'CEK DATA
                Dim flag_opname As String = ""
                SQL = "select flag_opname from stock_owner "
                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "kode_stock_owner = '" & ListView1.FocusedItem.SubItems(8).Text & "'" '
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        flag_opname = Dr("flag_Opname")

                        If flag_opname <> ListView1.FocusedItem.SubItems(10).Text Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Terjadi Kesalahan, Silahkan Refresh", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If

                        'If General_Class.CekNULL(flag_opname) <> "Y" Then
                        '    Dr.Close()
                        '    CloseTrans()
                        '    CloseConn()
                        '    MessageBox.Show("Terjadi Kesalahan, Silahkan Refresh!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        '    Exit Sub
                        'End If
                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Terjadi Kesalahan, Silahkan Refresh", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                If flag_opname = "Y" Then

                    SQL = "Select Kode_Unik "
                    SQL = SQL & "from schedule_opname where Lokasi='" & ListView1.FocusedItem.SubItems(8).Text & "'"
                    SQL = SQL & "and mulai = 'Y' and selesai = 'T'  "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            If Dr("Kode_Unik") <> Id_Transaksi Then
                                Dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Terjadi Kesalahan, Silahkan Refresh!!!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        Else
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Terjadi Kesalahan, Silahkan Refresh!!!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using

                    SQL = "SELECT top(1) datediff(mi, Tanggal+Jam,'" & Format(tgl_skg, "yyyy-MM-dd HH:mm:ss") & "') as selisih from do_new_sementara "
                    SQL = SQL & "where No_Faktur = '" & ListView1.FocusedItem.Text & "' and Kode_Perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "batal is null "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            If Dr("Selisih") > lama_expire_opname Then
                                Dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Terjadi Kesalahan, Silahkan Refresh!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        Else
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Terjadi Kesalahan, Silahkan Refresh!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using

                    SQL = "select flag_sementara_saat_opm from penjualan "
                    SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "No_faktur = '" & ListView1.FocusedItem.Text & "'" '
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            If General_Class.CekNULL(Dr("flag_sementara_saat_opm")) <> "Y" Then
                                Dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Terjadi Kesalahan, Silahkan Refresh!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        Else
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Terjadi Kesalahan, Silahkan Refresh!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using


                    SQL = "select Batal from Do_New_Sementara "
                    SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "No_Faktur = '" & ListView1.FocusedItem.Text & "' and "
                    SQL = SQL & "batal is null "
                    Using Dr = OpenTrans(SQL)
                        If Not Dr.Read Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Terjadi Kesalahan, Silahkan Refresh!!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using

                End If
                'awal coding stenly

                Dim flag_opm As New ArrayList
                flag_opm = Cek_Flag(KodePerusahaan, "Buka_DO", ListView1.FocusedItem.SubItems(8).Text)

                If flag_opm.Item(0) = "X" Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show(err_msg_opname, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                'akhir coding stenly

                'awal coding reza
                Dim nilai_satu_poin As Integer = 0
                Dim nilai_poin_dari_ngrand As Integer = 0



                ' If tanggal_pi > "2023-08-31" Then
                SQL = "select poin from poin where kode_perusahaan = '" & KodePerusahaan & "'"
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        nilai_satu_poin = Dr("poin")
                        nilai_poin_dari_ngrand = Math.Floor(Val(HilangkanTanda(TxtTotal.Text)) / Dr("poin"))
                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Nilai POIN tidak terdedia", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using
                ' End If


                'akhir coding reza

                SQL = "insert into do_new(kode_perusahaan, no_do, tanggal, jam, userid, "
                SQL = SQL & "no_faktur, jenis_kendaraan, kode_kendaraan, jenis_driver, "
                SQL = SQL & "kode_driver, tujuan, hp, keterangan, ke_brp, "
                SQL = SQL & "validasi_terima, tanggal_terima, jam_terima, user_terima, nama_penerima, "
                SQL = SQL & "tgl_diterima, validasi_hasil, x, lama_jt, Kode_Cust_Group, metode_pot_stock, "
                SQL = SQL & "NTotal, NPPN, NNilai_PPN, NGrand, hrs_updatee, metode_budgeting, xtermxxZ, kode_helper, VXYZ, " & flag_opm.Item(0) & ", id_transaksi,nilai_satu_poin, total_poin, Kode_Ekspedisi, Id_Gudang, Id_Ekspedisi) values("
                SQL = SQL & "'" & KodePerusahaan & "', '" & nofakdo & "', "
                SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', "
                SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "', "
                SQL = SQL & "'" & UserID & "', '" & ListView1.FocusedItem.Text & "', "
                SQL = SQL & "'" & CmbJnsMbl.Text & "', '" & kode_mbl & "', '" & CmbJnsDriver.Text & "', "
                SQL = SQL & "'" & kode_driver & "', '" & TextBox3.Text.Trim & "', "
                SQL = SQL & "'" & TextBox4.Text.Trim & "', '" & TextBox5.Text.Trim & "', '" & kebrp & "', "
                SQL = SQL & "'Y', "
                SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', "
                SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "', "
                SQL = SQL & "'" & UserID & "', '-', "
                SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', "
                SQL = SQL & "'Y', 'Y', '" & lama_jt_new & "', " & cust_grup & ", '" & metode_pot_stock & "', "
                SQL = SQL & "'" & HilangkanTanda(TextBox17.Text) & "', "
                SQL = SQL & "'" & HilangkanTanda(TextBox18.Text) & "', "
                SQL = SQL & "'" & HilangkanTanda(TextBox19.Text) & "', "
                SQL = SQL & "'" & HilangkanTanda(TxtTotal.Text) & "', 'Y', '" & metode_budgeting & "', 'Y', " & kode_helper & ", 'R', " & flag_opm.Item(1) & ", '" & Id_Transaksi & "', "
                SQL = SQL & "" & nilai_satu_poin & ", " & nilai_poin_dari_ngrand & "," & kode_Ekspedisi & ",'" & Id_Gudang & "'," & Id_Ekspedisi & ")"
                ExecuteTrans(SQL)


                ' awal Koding Reza

                '   If tanggal_pi > "2023-08-31" Then
                SQL = "select kode_perusahaan from customers where "
                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and kode_customer = '" & f_kd_customer & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Dr.Close()

                        SQL = "update customers set poin = poin + " & nilai_poin_dari_ngrand & " "
                        SQL = SQL & "where kode_customer = '" & f_kd_customer & "' "
                        ExecuteTrans(SQL)

                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Customer tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using
                'End If

                ' akhir koding Reza


                SQL = "insert into member_Data_Masuk "
                SQL = SQL & "(Kode_Perusahaan, No_Faktur, Jenis) "
                SQL = SQL & " Values('" & KodePerusahaan & "', '" & nofakdo & "','DO') "
                ExecuteTrans(SQL)

                Dim total_hpp_metode_B As Double = 0
                Dim TotalBarang As Double = 0
                Dim TotalBarangSN As Double = 0

                'TODO : Loop DGV
                For i As Integer = 0 To DataGridView1.RowCount - 1

                    Get_Isi_Listview(i)

                    If Val(HilangkanTanda(LvJmlKrm)) <> 0 Then

                        Dim xflag_budgeting1 As String = "NULL"
                        Dim xflag_budgetingMbl As String = "NULL"
                        Dim xflag_budgeting2 As String = "NULL"
                        Dim xflag_budgeting3 As String = "NULL"
                        Dim xflag_budgeting4 As String = "NULL"
                        Dim xflag_budgetingNew As String = "NULL"

                        If LvFlagBudgeting1 = "" Then
                            xflag_budgeting1 = "NULL"
                        Else
                            xflag_budgeting1 = "'" & LvFlagBudgeting1 & "'"
                        End If
                        If LvFlagBudgetingMbl = "" Then
                            xflag_budgetingMbl = "NULL"
                        Else
                            xflag_budgetingMbl = "'" & LvFlagBudgetingMbl & "'"
                        End If
                        If LvFlagBudgeting2 = "" Then
                            xflag_budgeting2 = "NULL"
                        Else
                            xflag_budgeting2 = "'" & LvFlagBudgeting2 & "'"
                        End If
                        If LvFlagBudgeting3 = "" Then
                            xflag_budgeting3 = "NULL"
                        Else
                            xflag_budgeting3 = "'" & LvFlagBudgeting3 & "'"
                        End If
                        If LvFlagBudgeting4 = "" Then
                            xflag_budgeting4 = "NULL"
                        Else
                            xflag_budgeting4 = "'" & LvFlagBudgeting4 & "'"
                        End If
                        If LvFlagBudgetingNew = "" Then
                            xflag_budgetingNew = "NULL"
                        Else
                            xflag_budgetingNew = "'" & LvFlagBudgetingNew & "'"
                        End If

                        SQL = "insert into detail_do_new(kode_perusahaan, no_do, no_urut, "
                        SQL = SQL & "kode_stock_owner, kode_barang, jumlah, hrg_muat, total_muat, "
                        SQL = SQL & "jml_terima, hasil, Nharga, npersen_diskon, nsubtotal, "
                        SQL = SQL & "nflag_budgeting, nflag_budgeting_mbl, "
                        SQL = SQL & "nflag_budgeting_2, nflag_budgeting_3, nflag_budgeting_4, "
                        SQL = SQL & "nharga_agen, nHarga_Terendah,Flag_Sudah_Hitung, Urut_Proforma_Saat_Opname, Metode_Perhitungan, nflag_budgeting_new) values("
                        SQL = SQL & "'" & KodePerusahaan & "', '" & nofakdo & "', "
                        SQL = SQL & "'" & LvUrut & "', "
                        SQL = SQL & "'" & LvSO & "', "
                        SQL = SQL & "'" & LvKB & "', "
                        SQL = SQL & "'" & HilangkanTanda(LvJmlKrm) & "', "
                        SQL = SQL & "'" & HilangkanTanda(LvHrgMuat) & "', "
                        SQL = SQL & "'" & HilangkanTanda(LvTtlMuat) & "', "
                        SQL = SQL & "'" & HilangkanTanda(LvJmlKrm) & "', "
                        SQL = SQL & "'5', "
                        SQL = SQL & "'" & HilangkanTanda(LvHrg) & "', "
                        SQL = SQL & "'" & HilangkanTanda(LvDiscP) & "', "
                        SQL = SQL & "'" & HilangkanTanda(LvSubttl) & "', "
                        SQL = SQL & "" & xflag_budgeting1 & ", " & xflag_budgetingMbl & ", "
                        SQL = SQL & "" & xflag_budgeting2 & ", " & xflag_budgeting3 & ", " & xflag_budgeting4 & ", "
                        SQL = SQL & "'" & HilangkanTanda(LvHrgAgen) & "', "
                        SQL = SQL & "'" & HilangkanTanda(LvHrgTerendah) & "', "

                        'If flag_opname = "Y" Then
                        '    If LvOpname.ToUpper = "SUDAH HITUNG" Then
                        '        SQL = SQL & "'Y', "
                        '    ElseIf LvOpname.ToUpper = "BELUM HITUNG" Then
                        '        SQL = SQL & "'T', "
                        '    Else
                        '        SQL = SQL & "Null, "
                        '    End If
                        '    SQL = SQL & "" & LvUrutProforma & ") "
                        'Else
                        SQL = SQL & "Null,0, '" & LvMetPer & "' , " & xflag_budgetingNew & ") "
                        'End If
                        ExecuteTrans(SQL)



                        If metode_budgeting = "B" And LvFlagBudgeting1 = "Y" Then
                            For hk As Integer = 0 To 2
                                Dim persen_budget As Double = 0

                                If hk = 0 Then
                                    persen_budget = 70
                                ElseIf hk = 1 Then
                                    persen_budget = 10
                                ElseIf hk = 2 Then
                                    persen_budget = 20
                                End If

                                Dim nilai_terendah As Double = Val(HilangkanTanda(LvHrgTerendah)) * Val(HilangkanTanda(LvJmlKrm))
                                Dim nilai_agen As Double = Val(HilangkanTanda(LvHrgAgen)) * Val(HilangkanTanda(LvJmlKrm))

                                Dim nilai_bdgt As Double = nilai_agen - nilai_terendah
                                nilai_bdgt = nilai_bdgt + (nilai_bdgt * Val(TextBox18.Text) / 100)

                                If nilai_bdgt < 0 Then
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Nilai tidak boleh dibawah nol!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If

                                SQL = "insert into do_budgeting(kode_perusahaan, no_do, "
                                SQL = SQL & "kode_kategori2, nilai, kode_budget, persen, hasil, jenis) values("
                                SQL = SQL & "'" & KodePerusahaan & "', '" & nofakdo & "', "
                                SQL = SQL & "'" & LvKategori2 & "', "
                                SQL = SQL & "'" & HilangkanTanda(Format(nilai_bdgt, "N0")) & "', "
                                SQL = SQL & "'PROMO_BS', "
                                SQL = SQL & "'" & persen_budget & "', "
                                SQL = SQL & "'" & HilangkanTanda(Format(nilai_bdgt * persen_budget / 100, "N0")) & "', '" & hk + 1 & "')"
                                ExecuteTrans(SQL)
                            Next
                        End If


                        If metode_budgeting = "B" And LvFlagBudgetingMbl = "Y" Then
                            If LvHrgAgen - LvHrgTerendah = 0 Then
                                Dim nilai_bdgt As Double = Val(HilangkanTanda(LvSubttl))
                                nilai_bdgt = nilai_bdgt + (nilai_bdgt * Val(TextBox18.Text) / 100)
                                Dim persen_budget As Double = 5

                                If nilai_bdgt < 0 Then
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Nilai tidak boleh dibawah nol!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If

                                SQL = "insert into do_budgeting_mbl(kode_perusahaan, no_do, "
                                SQL = SQL & "kode_kategori2, nilai, kode_budget, persen, hasil, jenis) values("
                                SQL = SQL & "'" & KodePerusahaan & "', '" & nofakdo & "', "
                                SQL = SQL & "'" & LvKategori2 & "', "
                                SQL = SQL & "'" & HilangkanTanda(Format(nilai_bdgt, "N0")) & "', "
                                SQL = SQL & "'PROMO_MBL', "
                                SQL = SQL & "'" & persen_budget & "', "
                                SQL = SQL & "'" & HilangkanTanda(Format(nilai_bdgt * persen_budget / 100, "N0")) & "', '1')"
                                ExecuteTrans(SQL)

                            Else
                                Dim nilai_terendah As Double = Val(HilangkanTanda(LvHrgTerendah)) * Val(HilangkanTanda(LvJmlKrm))
                                Dim nilai_agen As Double = Val(HilangkanTanda(LvHrgAgen)) * Val(HilangkanTanda(LvJmlKrm))

                                Dim nilai_bdgt As Double = nilai_agen - nilai_terendah
                                nilai_bdgt = nilai_bdgt + (nilai_bdgt * Val(TextBox18.Text) / 100)

                                If nilai_bdgt < 0 Then
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Nilai tidak boleh dibawah nol!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If

                                SQL = "insert into do_budgeting_mbl(kode_perusahaan, no_do, "
                                SQL = SQL & "kode_kategori2, nilai, kode_budget, persen, hasil, jenis) values("
                                SQL = SQL & "'" & KodePerusahaan & "', '" & nofakdo & "', "
                                SQL = SQL & "'" & LvKategori2 & "', "
                                SQL = SQL & "'" & HilangkanTanda(Format(nilai_bdgt, "N0")) & "', "
                                SQL = SQL & "'PROMO_MBL', "
                                SQL = SQL & "'100', "
                                SQL = SQL & "'" & HilangkanTanda(Format(nilai_bdgt, "N0")) & "', '1')"
                                ExecuteTrans(SQL)
                            End If
                        End If

                        If metode_budgeting = "B" And LvFlagBudgetingNew = "Y" Then
                            Dim nilai_bdgt_new As Double = Val(HilangkanTanda(LvSubttl))
                            nilai_bdgt_new = nilai_bdgt_new + (nilai_bdgt_new * Val(TextBox18.Text) / 100)
                            Dim persen_budget_new As Double = 0.6

                            If nilai_bdgt_new < 0 Then
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Nilai tidak boleh dibawah nol!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If

                            SQL = "insert into do_budgeting_new(kode_perusahaan, no_do, "
                            SQL = SQL & "kode_kategori2, nilai, kode_budget, persen, hasil, jenis) values("
                            SQL = SQL & "'" & KodePerusahaan & "', '" & nofakdo & "', "
                            SQL = SQL & "'" & LvKategori2 & "', "
                            SQL = SQL & "'" & HilangkanTanda(Format(nilai_bdgt_new, "N0")) & "', "
                            SQL = SQL & "'PROMO_LIBURAN', "
                            SQL = SQL & "'" & persen_budget_new & "', "
                            SQL = SQL & "'" & HilangkanTanda(Format(nilai_bdgt_new * persen_budget_new / 100, "N0")) & "', '1')"
                            ExecuteTrans(SQL)
                        End If

                        If metode_budgeting = "B" And LvFlagBudgeting2 = "Y" Then
                            For hk As Integer = 0 To 2
                                Dim persen_budget As Double = 0

                                If hk = 0 Then
                                    persen_budget = 70
                                ElseIf hk = 1 Then
                                    persen_budget = 10
                                ElseIf hk = 2 Then
                                    persen_budget = 20
                                End If


                                Dim nilai_bdgt As Double = (Val(HilangkanTanda(LvHrgAgen)) - Val(HilangkanTanda(LvHrgTerendah))) * Val(HilangkanTanda(LvJmlKrm))
                                nilai_bdgt = nilai_bdgt + (nilai_bdgt * Val(TextBox18.Text) / 100)

                                If nilai_bdgt < 0 Then
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Nilai tidak boleh dibawah nol!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If

                                SQL = "insert into do_budgeting_2(kode_perusahaan, no_do, "
                                SQL = SQL & "kode_kategori2, nilai, kode_budget, persen, hasil, jenis) values("
                                SQL = SQL & "'" & KodePerusahaan & "', '" & nofakdo & "', "
                                SQL = SQL & "'" & LvKategori2 & "', "
                                SQL = SQL & "'" & HilangkanTanda(Format(nilai_bdgt, "N0")) & "', "
                                SQL = SQL & "'PRM_BS', "
                                SQL = SQL & "'" & persen_budget & "', "
                                SQL = SQL & "'" & HilangkanTanda(Format(nilai_bdgt * persen_budget / 100, "N0")) & "', '" & hk + 1 & "')"
                                ExecuteTrans(SQL)
                            Next
                        End If

                        If metode_budgeting = "B" And LvFlagBudgeting3 = "Y" Then
                            For hk As Integer = 0 To 2
                                Dim persen_budget As Double = 0

                                If hk = 0 Then
                                    persen_budget = 70
                                ElseIf hk = 1 Then
                                    persen_budget = 10
                                ElseIf hk = 2 Then
                                    persen_budget = 20
                                End If

                                Dim nilai_bdgt As Double = (Val(HilangkanTanda(LvHrgAgen)) - Val(HilangkanTanda(LvHrgTerendah))) * Val(HilangkanTanda(LvJmlKrm))
                                nilai_bdgt = nilai_bdgt + (nilai_bdgt * Val(TextBox18.Text) / 100)

                                If nilai_bdgt < 0 Then
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Nilai tidak boleh dibawah nol!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If

                                SQL = "insert into do_budgeting_3(kode_perusahaan, no_do, "
                                SQL = SQL & "kode_kategori2, nilai, kode_budget, persen, hasil, jenis) values("
                                SQL = SQL & "'" & KodePerusahaan & "', '" & nofakdo & "', "
                                SQL = SQL & "'" & LvKategori2 & "', "
                                SQL = SQL & "'" & HilangkanTanda(Format(nilai_bdgt, "N0")) & "', "
                                SQL = SQL & "'PRM_BS', "
                                SQL = SQL & "'" & persen_budget & "', "
                                SQL = SQL & "'" & HilangkanTanda(Format(nilai_bdgt * persen_budget / 100, "N0")) & "', '" & hk + 1 & "')"
                                ExecuteTrans(SQL)
                            Next
                        End If

                        If metode_budgeting = "B" And LvFlagBudgeting4 = "Y" Then
                            Dim nilai_bdgt As Double = (Val(HilangkanTanda(LvHrgAgen)) - Val(HilangkanTanda(LvHrgTerendah))) * Val(HilangkanTanda(LvJmlKrm))
                            nilai_bdgt = nilai_bdgt + (nilai_bdgt * Val(TextBox18.Text) / 100)

                            If nilai_bdgt < 0 Then
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Nilai tidak boleh dibawah nol!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If

                            SQL = "insert into do_budgeting_4(kode_perusahaan, no_do, "
                            SQL = SQL & "kode_kategori2, nilai, kode_budget, persen, hasil, jenis) values("
                            SQL = SQL & "'" & KodePerusahaan & "', '" & nofakdo & "', "
                            SQL = SQL & "'" & LvKategori2 & "', "
                            SQL = SQL & "'" & HilangkanTanda(Format(nilai_bdgt, "N0")) & "', "
                            SQL = SQL & "'PRM_BS', "
                            SQL = SQL & "'100', "
                            SQL = SQL & "'" & HilangkanTanda(Format(nilai_bdgt, "N0")) & "', '1')"
                            ExecuteTrans(SQL)
                        End If


                        Dim x_no_urut_det_do As Integer = 0
                        SQL = "select IDENT_CURRENT('detail_do_new') as urutan"
                        Using Dr = OpenTrans(SQL)
                            If Dr.Read Then
                                x_no_urut_det_do = Dr("urutan")
                            End If
                        End Using

                        SQL = "select no_urut from detail_do_new where kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "no_do = '" & nofakdo & "' and urut_oto = '" & x_no_urut_det_do & "'"
                        Using Dr = OpenTrans(SQL)
                            If Not (Dr.Read) Then
                                Dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Harap ulangi transaksi ini lagi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        End Using

                        If metode_pot_stock = "A" Then
                            SQL = "select Stock_Blm_Kirim from barang where "
                            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                            SQL = SQL & "kode_stock_owner = '" & LvSO & "' and "
                            SQL = SQL & "kode_barang = '" & LvKB & "'"
                            Using Ds = BindingTrans(SQL)
                                With Ds.Tables("MyTable")
                                    If .Rows.Count <> 0 Then

                                        If .Rows(0).Item("Stock_Blm_Kirim") - Val(HilangkanTanda(LvJmlKrm)) < BolehNegatif Then
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("Proses membuat stock blm terkirim menjadi negatif untuk barang " & LvKB & ". " & Chr(13) & "Proses tidak dapat dilanjutkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        Else
                                            SQL = "Update barang set Stock_Blm_Kirim = Stock_Blm_Kirim - " & HilangkanTanda(LvJmlKrm) & " where "
                                            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                            SQL = SQL & "kode_stock_owner = '" & LvSO & "' and "
                                            SQL = SQL & "kode_barang = '" & LvKB & "'"
                                            ExecuteTrans(SQL)
                                        End If
                                    Else
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Barang tidak ditemukan." & Chr(13) & "Proses tidak dapat dilanjutkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Stop)
                                        Exit Sub
                                    End If
                                End With
                            End Using
                        Else

                            SQL = "select Kd_SO, Kd_Barang, Serial_Number, Bags, Jumlah "
                            SQL = SQL & "from Emi_DO_Pallet_Sementara "
                            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_FakturPenjualan = '" & LvNoPenjualan & "' "
                            SQL = SQL & "and Kd_SO = '" & LvSO & "' and Kd_Barang = '" & LvKB & "' and userid = '" & UserID & "' "
                            Using Ds7 = BindingTrans(SQL)
                                If Ds7.Tables("MyTable").Rows.Count <> 0 Then
                                    For j As Integer = 0 To Ds7.Tables("MyTable").Rows.Count - 1

                                        Dim Potong = Ds7.Tables("MyTable").Rows(j).Item("Jumlah")

                                        TotalBarang += Potong


                                        SQL = "select good_stock, flag_ppn from barang where "
                                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                        SQL = SQL & "kode_stock_owner = '" & Ds7.Tables("MyTable").Rows(j).Item("Kd_SO") & "' and "
                                        SQL = SQL & "kode_barang = '" & Ds7.Tables("MyTable").Rows(j).Item("Kd_Barang") & "' "
                                        Using Ds = BindingTrans(SQL)
                                            With Ds.Tables("MyTable")
                                                If .Rows.Count <> 0 Then

                                                    If .Rows(0).Item("good_stock") - Potong < BolehNegatif Then
                                                        CloseTrans()
                                                        CloseConn()
                                                        MessageBox.Show("Proses membuat stock menjadi negatif untuk barang " & LvNm & ". " & Chr(13) & "Proses tidak dapat dilanjutkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                        Exit Sub
                                                    ElseIf .Rows(0).Item("flag_ppn") <> flag_ppn Then
                                                        CloseTrans()
                                                        CloseConn()
                                                        MessageBox.Show("" & LvNm & " bukan barang flag PPN = " & flag_ppn & " Proses tidak dapat dilanjutkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                        Exit Sub
                                                    Else

                                                        'TODO : Update Barang
                                                        SQL = "Update barang set good_stock = good_stock - " & Potong & ", "
                                                        SQL = SQL & "Jumlah_Bags = Jumlah_Bags - " & HilangkanTanda(Ds7.Tables("MyTable").Rows(j).Item("Bags")) & " "
                                                        SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
                                                        SQL = SQL & "kode_stock_owner = '" & Ds7.Tables("MyTable").Rows(j).Item("Kd_SO") & "' and "
                                                        SQL = SQL & "kode_barang = '" & Ds7.Tables("MyTable").Rows(j).Item("Kd_Barang") & "' "
                                                        ExecuteTrans(SQL)
                                                    End If
                                                Else
                                                    CloseTrans()
                                                    CloseConn()
                                                    MessageBox.Show("Barang tidak ditemukan." & Chr(13) & "Proses tidak dapat dilanjutkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Stop)
                                                    Exit Sub
                                                End If
                                            End With
                                        End Using

                                    Next
                                End If
                            End Using





                            Dim lewatin As String = "T"
                            SQL = "select isnull(sum(jumlah), 0) as stock from barang_sn where "
                            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                            SQL = SQL & "kode_stock_owner = '" & LvSO & "' and "
                            SQL = SQL & "kode_barang = '" & LvKB & "' and jumlah <> 0 "
                            'SQL = SQL & "order by " & SN_Tanggal("serial_number") & Metode
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then
                                    If Dr("stock") < Val(HilangkanTanda(LvJmlKrm)) Then
                                        lewatin = "Y"
                                    Else
                                        lewatin = "T"
                                    End If
                                End If
                            End Using

                            If lewatin = "T" Then

                                Dim sisa As Double = 0
                                sisa = HilangkanTanda(LvJmlKrm)

                                SQL = "select Kd_SO, Kd_Barang, Serial_Number, Bags, Jumlah "
                                SQL = SQL & "from Emi_DO_Pallet_Sementara "
                                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_FakturPenjualan = '" & LvNoPenjualan & "' "
                                SQL = SQL & "and Kd_SO = '" & LvSO & "' and Kd_Barang = '" & LvKB & "' and userid = '" & UserID & "' "
                                Using Ds8 = BindingTrans(SQL)
                                    If Ds8.Tables("MyTable").Rows.Count <> 0 Then
                                        For j As Integer = 0 To Ds8.Tables("MyTable").Rows.Count - 1

                                            Dim Potong As Double = Ds8.Tables("MyTable").Rows(j).Item("Jumlah")

                                            TotalBarangSN += Potong

                                            SQL = "select kode_stock_owner, kode_barang, serial_number, jumlah from barang_sn where "
                                            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                            SQL = SQL & "kode_stock_owner = '" & Ds8.Tables("MyTable").Rows(j).Item("Kd_SO") & "' and "
                                            SQL = SQL & "kode_barang = '" & Ds8.Tables("MyTable").Rows(j).Item("Kd_Barang") & "' and jumlah <> 0 "
                                            SQL = SQL & "and serial_number = '" & Ds8.Tables("MyTable").Rows(j).Item("Serial_Number") & "' "
                                            SQL = SQL & "order by " & SN_Tanggal("serial_number") & Metode
                                            Using Ds = BindingTrans(SQL)
                                                With Ds.Tables("MyTable")
                                                    If .Rows.Count <> 0 Then


                                                        For h As Integer = 0 To .Rows.Count - 1
                                                            If sisa = 0 Then
                                                                Exit For
                                                            ElseIf sisa < 0 Then
                                                                CloseTrans()
                                                                CloseConn()
                                                                MessageBox.Show("Sisa < 0", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                                Exit Sub
                                                            End If

                                                            'TODO Ambnil serial number darti table smeentara

                                                            If sisa < Potong Or sisa = Potong Then

                                                                SQL = "Update barang_sn set jumlah = jumlah - " & sisa & ", "
                                                                SQL = SQL & "Jumlah_Bags = Jumlah_Bags - " & Val(HilangkanTanda(Ds8.Tables("MyTable").Rows(j).Item("Bags"))) & " "
                                                                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
                                                                SQL = SQL & "kode_stock_owner = '" & .Rows(h).Item("kode_stock_owner") & "' and "
                                                                SQL = SQL & "kode_barang = '" & .Rows(h).Item("kode_barang") & "' and "
                                                                SQL = SQL & "serial_number = '" & .Rows(h).Item("serial_number") & "'"
                                                                ExecuteTrans(SQL)

                                                                SQL = "insert into det_do_new(kode_perusahaan, no_faktur, "
                                                                SQL = SQL & "kode_stock_owner, kode_barang, serial_number, no_urut_do, "
                                                                SQL = SQL & "jumlah, no_urut_det_penj) values('" & KodePerusahaan & "', "
                                                                SQL = SQL & "'" & nofakdo & "', "
                                                                SQL = SQL & "'" & .Rows(h).Item("kode_stock_owner") & "', "
                                                                SQL = SQL & "'" & .Rows(h).Item("kode_barang") & "', "
                                                                SQL = SQL & "'" & .Rows(h).Item("serial_number") & "', "
                                                                SQL = SQL & "" & x_no_urut_det_do & ", '" & sisa & "', '" & LvUrut & "')"
                                                                ExecuteTrans(SQL)

                                                                total_hpp_metode_B = total_hpp_metode_B + (sisa * Get_Harga_SN(.Rows(h).Item("serial_number")))

                                                                sisa = 0
                                                            ElseIf sisa > Potong Then

                                                                SQL = "insert into det_do_new(kode_perusahaan, no_faktur, "
                                                                SQL = SQL & "kode_stock_owner, kode_barang, serial_number, no_urut_do, "
                                                                SQL = SQL & "jumlah, no_urut_det_penj) values('" & KodePerusahaan & "', "
                                                                SQL = SQL & "'" & nofakdo & "', "
                                                                SQL = SQL & "'" & .Rows(h).Item("kode_stock_owner") & "', "
                                                                SQL = SQL & "'" & .Rows(h).Item("kode_barang") & "', "
                                                                SQL = SQL & "'" & .Rows(h).Item("serial_number") & "', "
                                                                SQL = SQL & "" & x_no_urut_det_do & ", "
                                                                SQL = SQL & "'" & Potong & "', '" & LvUrut & "')"
                                                                ExecuteTrans(SQL)

                                                                SQL = "Update barang_sn set jumlah = jumlah - " & HilangkanTanda(Potong) & " where "
                                                                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                                                SQL = SQL & "kode_stock_owner = '" & .Rows(h).Item("kode_stock_owner") & "' and "
                                                                SQL = SQL & "kode_barang = '" & .Rows(h).Item("kode_barang") & "' and "
                                                                SQL = SQL & "serial_number = '" & .Rows(h).Item("serial_number") & "'"
                                                                ExecuteTrans(SQL)

                                                                total_hpp_metode_B = total_hpp_metode_B + (Potong * Get_Harga_SN(.Rows(h).Item("serial_number")))

                                                                sisa = sisa - Potong
                                                            Else
                                                                CloseTrans()
                                                                CloseConn()
                                                                MessageBox.Show("Barang SN terjadi kesalahan untuk barang " & LvNm & "!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                                Exit Sub
                                                            End If

                                                            'If sisa <> 0 And h = .Rows.Count - 1 Then
                                                            '    CloseTrans()
                                                            '    CloseConn()
                                                            '    MessageBox.Show("Jumlah stock tidak mencukupi untuk barang " & LvNm & "!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                            '    Exit Sub
                                                            'End If

                                                            If Val(HilangkanTanda(LvSubttl)) <> 0 Then
                                                                If Val(Get_Harga_SN(.Rows(h).Item("serial_number"))) + (Val(Get_Harga_SN(.Rows(h).Item("serial_number"))) * 5 / 100) > Val(HilangkanTanda(Format(Val(HilangkanTanda(LvSubttl)) / Val(HilangkanTanda(LvJmlKrm)), "N0"))) Then
                                                                    If boleh_jual_rugi = "T" Then
                                                                        CloseTrans()
                                                                        CloseConn()
                                                                        MessageBox.Show("Barang " & LvNm & " harus diinput pusat!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                                        Exit Sub
                                                                    End If
                                                                End If
                                                            End If
                                                        Next
                                                    Else
                                                        CloseTrans()
                                                        CloseConn()
                                                        MessageBox.Show("SN untuk barang " & LvNm & " tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                        Exit Sub
                                                    End If
                                                End With
                                            End Using


                                        Next
                                    Else
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Terjadi Kesalahan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Stop)
                                        Exit Sub
                                    End If
                                End Using






                            Else

                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("SN untuk barang " & LvNm & " tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub

                                SQL = "insert into det_do_new(kode_perusahaan, no_faktur, "
                                SQL = SQL & "kode_stock_owner, kode_barang, serial_number, no_urut_do, "
                                SQL = SQL & "jumlah, no_urut_det_penj) values('" & KodePerusahaan & "', "
                                SQL = SQL & "'" & nofakdo & "', "
                                SQL = SQL & "'" & LvSO & "', "
                                SQL = SQL & "'" & LvKB & "', "
                                SQL = SQL & "'-', "
                                SQL = SQL & "" & x_no_urut_det_do & ", '0', '" & LvUrut & "')"
                                ExecuteTrans(SQL)
                            End If

                        End If
                    End If


                    'TODO : Akhir Loop

                    'CEK Apakah sama
                    If Not TotalBarang = HilangkanTanda(LvJmlKrm) Or Not TotalBarangSN = HilangkanTanda(LvJmlKrm) Then
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Terjadi Kesalahan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Stop)
                        Exit Sub
                    End If

                Next

                'SQL = "update detail_proforma_saat_opname set flag_sudah_dipakai = 'Y' "
                'SQL = SQL & "where id_transaksi = '" & Id_Transaksi & "' and No_Proforma = '" & ListView1.FocusedItem.Text & "' and "
                'SQL = SQL & "flag_sudah_dipakai is null and batal is null "
                'ExecuteTrans(SQL)

                SQL = "update Penjualan set flag_sementara_saat_opm = null "
                SQL = SQL & "where No_Faktur ='" & ListView1.FocusedItem.Text & "' "
                ExecuteTrans(SQL)



                If metode_pot_stock = "A" Then
                    Dim arrJns, arrNoFaktur, arrNoDO, arrKodeSO, arrKodeBrg, arrJmlDO, arrNoUrut As New ArrayList


                    SQL = "select 'do' as jenis, a.tanggal + a.jam as tgl, a.no_do, a.no_faktur, b.kode_stock_owner, b.kode_barang, b.jumlah, b.no_urut from "
                    SQL = SQL & "do_new a, detail_do_new b where "
                    SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan and "
                    SQL = SQL & "a.no_do = b.no_do and "
                    SQL = SQL & "a.status is null and "
                    SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "a.no_do = '" & nofakdo & "' "
                    Using Ds = BindingTrans(SQL)
                        With Ds.Tables("MyTable")
                            If .Rows.Count <> 0 Then
                                For yy As Integer = 0 To .Rows.Count - 1
                                    arrNoFaktur.Add(.Rows(yy).Item("no_faktur"))
                                    arrNoDO.Add(.Rows(yy).Item("no_do"))
                                    arrKodeSO.Add(.Rows(yy).Item("kode_stock_owner"))
                                    arrKodeBrg.Add(.Rows(yy).Item("kode_barang"))
                                    arrJmlDO.Add(.Rows(yy).Item("jumlah"))
                                    arrNoUrut.Add(.Rows(yy).Item("no_urut"))
                                    arrJns.Add(.Rows(yy).Item("jenis"))
                                Next
                            Else
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("DO tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        End With
                    End Using

                    Dim total_hpp As Double = 0

                    For rt As Integer = 0 To arrNoFaktur.Count - 1


                        Dim sisa As Double = 0
                        SQL = "select kode_stock_owner, kode_barang, serial_number, jumlah_do, "
                        SQL = SQL & "jumlah, no_urut from det_penj where "
                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_stock_owner = '" & arrKodeSO.Item(rt) & "' and "
                        SQL = SQL & "kode_barang = '" & arrKodeBrg.Item(rt) & "' and "
                        SQL = SQL & "no_urut = '" & arrNoUrut.Item(rt) & "' and jumlah <> 0 "
                        SQL = SQL & "order by " & SN_Tanggal("serial_number") & Metode
                        Using Ds = BindingTrans(SQL)
                            With Ds.Tables("MyTable")
                                If .Rows.Count <> 0 Then
                                    sisa = HilangkanTanda(arrJmlDO.Item(rt))

                                    For h As Integer = 0 To .Rows.Count - 1
                                        If sisa = 0 Then
                                            Exit For
                                        ElseIf sisa < 0 Then
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("Sisa < 0", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If

                                        If sisa < .Rows(h).Item("jumlah_do") Or sisa = .Rows(h).Item("jumlah_do") Then
                                            SQL = "Update det_penj set jumlah_do = jumlah_do - " & sisa & " where "
                                            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                            SQL = SQL & "kode_stock_owner = '" & .Rows(h).Item("kode_stock_owner") & "' and "
                                            SQL = SQL & "kode_barang = '" & .Rows(h).Item("kode_barang") & "' and "
                                            SQL = SQL & "no_urut = '" & .Rows(h).Item("no_urut") & "' and "
                                            SQL = SQL & "serial_number = '" & .Rows(h).Item("serial_number") & "'"
                                            ExecuteTrans(SQL)

                                            SQL = "insert into det_do(kode_perusahaan, no_faktur, "
                                            SQL = SQL & "kode_stock_owner, kode_barang, serial_number, no_urut, "
                                            SQL = SQL & "jumlah) values('" & KodePerusahaan & "', "
                                            SQL = SQL & "'" & arrNoDO.Item(rt) & "', "
                                            SQL = SQL & "'" & .Rows(h).Item("kode_stock_owner") & "', "
                                            SQL = SQL & "'" & .Rows(h).Item("kode_barang") & "', "
                                            SQL = SQL & "'" & .Rows(h).Item("serial_number") & "', "
                                            SQL = SQL & "" & .Rows(h).Item("no_urut") & ", '" & sisa & "')"
                                            ExecuteTrans(SQL)

                                            total_hpp = total_hpp + (sisa * Get_Harga_SN(.Rows(h).Item("serial_number")))

                                            sisa = 0
                                        ElseIf sisa > .Rows(h).Item("jumlah_do") Then
                                            SQL = "insert into det_do(kode_perusahaan, no_faktur, "
                                            SQL = SQL & "kode_stock_owner, kode_barang, serial_number, no_urut, "
                                            SQL = SQL & "jumlah) values('" & KodePerusahaan & "', "
                                            SQL = SQL & "'" & arrNoDO.Item(rt) & "', "
                                            SQL = SQL & "'" & .Rows(h).Item("kode_stock_owner") & "', "
                                            SQL = SQL & "'" & .Rows(h).Item("kode_barang") & "', "
                                            SQL = SQL & "'" & .Rows(h).Item("serial_number") & "', "
                                            SQL = SQL & "" & .Rows(h).Item("no_urut") & ", "
                                            SQL = SQL & "'" & .Rows(h).Item("jumlah_do") & "')"
                                            ExecuteTrans(SQL)

                                            SQL = "Update det_penj set jumlah_do = jumlah_do - jumlah_do where "
                                            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                            SQL = SQL & "kode_stock_owner = '" & .Rows(h).Item("kode_stock_owner") & "' and "
                                            SQL = SQL & "kode_barang = '" & .Rows(h).Item("kode_barang") & "' and "
                                            SQL = SQL & "no_urut = '" & .Rows(h).Item("no_urut") & "' and "
                                            SQL = SQL & "serial_number = '" & .Rows(h).Item("serial_number") & "'"
                                            ExecuteTrans(SQL)

                                            total_hpp = total_hpp + (.Rows(h).Item("jumlah_do") * Get_Harga_SN(.Rows(h).Item("serial_number")))

                                            sisa = sisa - .Rows(h).Item("jumlah_do")
                                        Else
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("Barang SN terjadi kesalahan untuk barang " & .Rows(h).Item("kode_barang") & "!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If

                                        If sisa <> 0 And h = .Rows.Count - 1 Then
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("Jumlah stock tidak mencukupi untuk barang " & .Rows(h).Item("kode_barang") & "!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If
                                    Next
                                Else
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("SN tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End With
                        End Using
                    Next

                    Dim coa_persediaan_Brg_Blm_Krm As String = ""
                    Dim coa_Brg_Blm_Krm As String = ""

                    SQL = "select top(1) persediaan_Brg_Blm_Krm, Brg_Blm_Krm, persediaan, "
                    SQL = SQL & "persediaan_sementara, persediaan_sementara_agency from stock_owner where "
                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "kode_Stock_owner = '" & lksi_gudang & "'"
                    Using dr = OpenTrans(SQL)
                        If dr.Read Then
                            coa_persediaan_Brg_Blm_Krm = dr("persediaan_Brg_Blm_Krm")
                            coa_Brg_Blm_Krm = dr("Brg_Blm_Krm")
                        Else
                            dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Data lokasi tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using

                    '__________________________________________________ KOMEN DULU ______________________________________
                    'Dim pagenumber As Integer = 0
                    'Dim Kode_Voucher2x As String = GetLastNumberJurnal(Format(tgl_skg, "yyyyMM"), fJurnalJual & jns_penjualan & init_faktur, KodePerusahaan)

                    'pagenumber = 1

                    'SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
                    'SQL = SQL & "Keterangan, JudulBank, KetDK, userid, lokasi) values("
                    'SQL = SQL & "'" & Kode_Voucher2x & "', "
                    'SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', "
                    'SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
                    'SQL = SQL & "'" & KodeProyek & "', 'DO " & nofakdo & " ; " & nama_cst & "', '', "
                    'SQL = SQL & "'-', '" & UserID & "', '" & lksi_invoice & "')"
                    'ExecuteTrans(SQL)


                    'SQL = Get_Detail_Jurnal(Kode_Voucher2x, Strings.Left(coa_Brg_Blm_Krm, 1),
                    '              Strings.Mid(coa_Brg_Blm_Krm, 2, 1),
                    '              Strings.Mid(Ganti(coa_Brg_Blm_Krm), 3),
                    '              KodePerusahaan, KodeProyek, "DO " & nofakdo & " ; " & nama_cst, total_hpp, "0", pagenumber, lksi_gudang)
                    'ExecuteTrans(SQL)
                    'pagenumber = pagenumber + 1

                    'SQL = Get_Detail_Jurnal(Kode_Voucher2x, Strings.Left(coa_persediaan_Brg_Blm_Krm, 1),
                    '              Strings.Mid(coa_persediaan_Brg_Blm_Krm, 2, 1),
                    '              Strings.Mid(Ganti(coa_persediaan_Brg_Blm_Krm), 3),
                    '              KodePerusahaan, KodeProyek, "DO " & nofakdo & " ; " & nama_cst, "0", total_hpp, pagenumber, lksi_gudang)
                    'ExecuteTrans(SQL)
                    'pagenumber = pagenumber + 1



                    'SQL = "select sum(debit) as debit, sum(kredit) as kredit from detail_jurnal where "
                    'SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    'SQL = SQL & "kode_voucher = '" & Kode_Voucher2x & "'"
                    'Using Dr = OpenTrans(SQL)
                    '    If Dr.Read Then
                    '        If Dr("debit") <> Dr("kredit") Then
                    '            Dr.Close()
                    '            CloseTrans()
                    '            CloseConn()
                    '            MessageBox.Show("Jurnal 1 salah!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '            Exit Sub
                    '        End If
                    '    Else
                    '        Dr.Close()
                    '        CloseTrans()
                    '        CloseConn()
                    '        MessageBox.Show("Data jurnal 1 tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '        Exit Sub
                    '    End If
                    'End Using

                    ''''SQL = "select kode_voucher from do_new where "
                    ''''SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    ''''SQL = SQL & "no_do = '" & nofakdo & "'"
                    ''''Using dr = OpenTrans(SQL)
                    ''''    If dr.Read Then
                    ''''        If General_Class.CekNULL(dr("kode_voucher")) <> "" Then
                    ''''            dr.Close()
                    ''''            CloseTrans()
                    ''''            CloseConn()
                    ''''            MessageBox.Show("DO ini sdh ada vouchernya tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    ''''            Exit Sub
                    ''''        End If
                    ''''    Else
                    ''''        dr.Close()
                    ''''        CloseTrans()
                    ''''        CloseConn()
                    ''''        MessageBox.Show("DO. tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    ''''        Exit Sub
                    ''''    End If
                    ''''End Using

                    'SQL = "update do_new set kode_voucher = '" & Kode_Voucher2x & "' "
                    'SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
                    'SQL = SQL & "no_do = '" & nofakdo & "'"
                    'ExecuteTrans(SQL)

                Else 'reseller metode baru

                    Dim coa_persediaan_ As String = ""
                    SQL = "select top(1) persediaan_Brg_Blm_Krm, Brg_Blm_Krm, persediaan, "
                    SQL = SQL & "persediaan_sementara, persediaan_sementara_agency from stock_owner_gudang where "
                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "kode_Stock_owner = '" & lksi_gudang & "'"
                    Using dr = OpenTrans(SQL)
                        If dr.Read Then
                            coa_persediaan_ = dr("persediaan")
                        Else
                            dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Data lokasi tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using

                    Dim xcoa_kas As String = ""
                    Dim xcoa_piutang As String = ""
                    Dim xcoa_penjualan_lainnya As String = ""
                    Dim xcoa_penjualan As String = ""
                    Dim xcoa_ppn_penjualan As String = ""
                    Dim xcoa_hpp As String = ""

                    SQL = "select top(1) kas, piutang, penjualan_lainnya, Penjualan, ppn_penjualan, hpp from stock_owner where "
                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "kode_Stock_owner = '" & lksi_invoice & "'"
                    Using dr = OpenTrans(SQL)
                        If dr.Read Then
                            xcoa_kas = dr("kas")
                            xcoa_piutang = dr("piutang")
                            xcoa_penjualan_lainnya = dr("penjualan_lainnya")
                            xcoa_penjualan = dr("penjualan")
                            xcoa_ppn_penjualan = dr("ppn_penjualan")
                            xcoa_hpp = dr("hpp")
                        Else
                            dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Data lokasi tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using


                    Dim pagenumber As Integer = 0
                    Dim Kode_Voucher3 As String = GetLastNumberJurnal(Format(tgl_skg, "yyyyMM"), fJurnalJual & jns_penjualan & init_faktur, KodePerusahaan)
                    Dim akun_kas As String = ""
                    Dim akun_piutang As String = ""
                    Dim akun_piutang_sementara As String = ""

                    pagenumber = 1

                    SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
                    SQL = SQL & "Keterangan, JudulBank, KetDK, userid, lokasi) values("
                    SQL = SQL & "'" & Kode_Voucher3 & "', "
                    SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', "
                    SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
                    SQL = SQL & "'" & KodeProyek & "', 'DO_ " & nofakdo & " ; " & ListView1.FocusedItem.Text & " ; " & nama_cst & "', '', "
                    SQL = SQL & "'-', '" & UserID & "', '" & lksi_invoice & "')"
                    ExecuteTrans(SQL)

                    If jns_trans = "T" Then 'tunai
                        SQL = Get_Detail_Jurnal(Kode_Voucher3, Strings.Left(xcoa_kas, 1),
                                   Strings.Mid(xcoa_kas, 2, 1),
                                   Strings.Mid(Ganti(xcoa_kas), 3),
                                   KodePerusahaan, KodeProyek, "DO_ " & nofakdo & " ; " & ListView1.FocusedItem.Text & " ; " & nama_cst, HilangkanTanda(TxtTotal.Text), "0", pagenumber, lksi_invoice, Cost_center:=Ket_Cost_Center_HO)
                        ExecuteTrans(SQL)
                        pagenumber = pagenumber + 1

                        akun_kas = "'" & xcoa_kas & "'"
                        akun_piutang = "NULL"
                        akun_piutang_sementara = "NULL"

                    Else 'kalo kredit

                        SQL = Get_Detail_Jurnal(Kode_Voucher3, Strings.Left(xcoa_piutang, 1),
                                  Strings.Mid(xcoa_piutang, 2, 1),
                                  Strings.Mid(Ganti(xcoa_piutang), 3),
                                  KodePerusahaan, KodeProyek, "DO_ " & nofakdo & " ; " & ListView1.FocusedItem.Text & " ; " & nama_cst, HilangkanTanda(TxtTotal.Text), "0", pagenumber, lksi_invoice, Cost_center:=Ket_Cost_Center_HO)
                        ExecuteTrans(SQL)
                        pagenumber = pagenumber + 1

                        akun_kas = "NULL"
                        akun_piutang = "'" & xcoa_piutang & "'"
                        akun_piutang_sementara = "NULL"
                    End If

                    If flag_audit = "Y" Then
                        SQL = Get_Detail_Jurnal(Kode_Voucher3, Strings.Left(xcoa_penjualan_lainnya, 1),
                            Strings.Mid(xcoa_penjualan_lainnya, 2, 1),
                            Strings.Mid(Ganti(xcoa_penjualan_lainnya), 3),
                            KodePerusahaan, KodeProyek, "DO_ " & nofakdo & " ; " & ListView1.FocusedItem.Text & " ; " & nama_cst, "0", HilangkanTanda(TextBox17.Text), pagenumber, lksi_invoice, Cost_center:=Ket_Cost_Center_HO)
                        ExecuteTrans(SQL)
                        pagenumber = pagenumber + 1
                    Else
                        SQL = Get_Detail_Jurnal(Kode_Voucher3, Strings.Left(xcoa_penjualan, 1),
                            Strings.Mid(xcoa_penjualan, 2, 1),
                            Strings.Mid(Ganti(xcoa_penjualan), 3),
                            KodePerusahaan, KodeProyek, "DO_ " & nofakdo & " ; " & ListView1.FocusedItem.Text & " ; " & nama_cst, "0", HilangkanTanda(TextBox17.Text), pagenumber, lksi_invoice, Cost_center:=Ket_Cost_Center_HO)
                        ExecuteTrans(SQL)
                        pagenumber = pagenumber + 1
                    End If

                    If Val(HilangkanTanda(TextBox19.Text)) <> 0 Then
                        SQL = Get_Detail_Jurnal(Kode_Voucher3, Strings.Left(xcoa_ppn_penjualan, 1),
                                Strings.Mid(xcoa_ppn_penjualan, 2, 1),
                                Strings.Mid(Ganti(xcoa_ppn_penjualan), 3),
                                KodePerusahaan, KodeProyek, "PPN DO_ " & nofakdo & " ; " & ListView1.FocusedItem.Text & " ; " & nama_cst, "0", HilangkanTanda(TextBox19.Text), pagenumber, Ket_Lokasi_HO, Cost_center:=Ket_Cost_Center_HO)
                        ExecuteTrans(SQL)
                        pagenumber = pagenumber + 1
                    End If

                    Dim Kode_Voucher4 As String = GetLastNumberJurnal(Format(tgl_skg, "yyyyMM"), fJurnalJual & jns_penjualan & init_faktur, KodePerusahaan)
                    pagenumber = 1

                    SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
                    SQL = SQL & "Keterangan, JudulBank, KetDK, userid, lokasi) values("
                    SQL = SQL & "'" & Kode_Voucher4 & "', "
                    SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', "
                    SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
                    SQL = SQL & "'" & KodeProyek & "', 'DO " & nofakdo & " ; " & ListView1.FocusedItem.Text & " ; " & nama_cst & " ', '', "
                    SQL = SQL & "'-', '" & UserID & "', '" & lksi_invoice & "')"
                    ExecuteTrans(SQL)


                    SQL = Get_Detail_Jurnal(Kode_Voucher4, Strings.Left(xcoa_hpp, 1),
                                  Strings.Mid(xcoa_hpp, 2, 1),
                                  Strings.Mid(Ganti(xcoa_hpp), 3),
                                  KodePerusahaan, KodeProyek, "HPP DO_ " & nofakdo & " ; " & ListView1.FocusedItem.Text & " ; " & nama_cst, total_hpp_metode_B, "0", pagenumber, lksi_invoice, Cost_center:=Ket_Cost_Center_HO)
                    ExecuteTrans(SQL)
                    pagenumber = pagenumber + 1
                    'vv
                    SQL = Get_Detail_Jurnal(Kode_Voucher4, Strings.Left(coa_persediaan_, 1),
                                  Strings.Mid(coa_persediaan_, 2, 1),
                                  Strings.Mid(Ganti(coa_persediaan_), 3),
                                  KodePerusahaan, KodeProyek, "HPP DO_ " & nofakdo & " ; " & ListView1.FocusedItem.Text & " ; " & nama_cst, "0", total_hpp_metode_B, pagenumber, lksi_gudang, Cost_center:=Ket_Cost_Center_HO)
                    ExecuteTrans(SQL)
                    pagenumber = pagenumber + 1

                    'SQL = Get_Detail_Jurnal(Kode_Voucher3, Strings.Left(coa_Brg_Blm_Krm, 1), _
                    '              Strings.Mid(coa_Brg_Blm_Krm, 2, 1), _
                    '              Strings.Mid(Ganti(coa_Brg_Blm_Krm), 3), _
                    '              KodePerusahaan, KodeProyek, "DO " & nofakdo & " ; " & ListView1.FocusedItem.Text & " ; " & nama_cst, total_hpp_metode_B, "0", pagenumber)
                    'ExecuteTrans(SQL)
                    'pagenumber = pagenumber + 1

                    'SQL = Get_Detail_Jurnal(Kode_Voucher3, Strings.Left(coa_persediaan_Brg_Blm_Krm, 1), _
                    '              Strings.Mid(coa_persediaan_Brg_Blm_Krm, 2, 1), _
                    '              Strings.Mid(Ganti(coa_persediaan_Brg_Blm_Krm), 3), _
                    '              KodePerusahaan, KodeProyek, "DO " & nofakdo & " ; " & ListView1.FocusedItem.Text & " ; " & nama_cst, "0", total_hpp_metode_B, pagenumber)
                    'ExecuteTrans(SQL)
                    'pagenumber = pagenumber + 1



                    SQL = "select sum(debit) as debit, sum(kredit) as kredit from detail_jurnal where "
                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "kode_voucher = '" & Kode_Voucher3 & "'"
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            If Dr("debit") <> Dr("kredit") Then
                                Dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Jurnal 1 salah!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        Else
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Data jurnal 1 tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using

                    SQL = "select sum(debit) as debit, sum(kredit) as kredit from detail_jurnal where "
                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "kode_voucher = '" & Kode_Voucher4 & "'"
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            If Dr("debit") <> Dr("kredit") Then
                                Dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Jurnal 1 salah!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        Else
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Data jurnal 1 tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using

                    SQL = "update do_new set kode_voucher3 = '" & Kode_Voucher3 & "', kode_voucher4 = '" & Kode_Voucher4 & "', "
                    SQL = SQL & "akun_kas = " & akun_kas & ", "
                    SQL = SQL & "akun_piutang = " & akun_piutang & ", akun_piutang_sementara = " & akun_piutang_sementara & " "
                    SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "no_do = '" & nofakdo & "'"
                    ExecuteTrans(SQL)

                End If

                If metode_budgeting = "B" Then

                    ''''Dim xAkunBiayaPromo As String = ""
                    ''''Dim xAkunBiayaHut1 As String = ""
                    ''''Dim xAkunBiayaHut2 As String = ""
                    ''''Dim xAkunBiayaHut3 As String = ""
                    ''''Dim xAkunBiayaPromo2 As String = ""
                    ''''Dim xAkunBiayaHut4 As String = ""
                    ''''Dim xAkunBiayaMbl As String = ""
                    ''''Dim xAkunBiayaHutMbl As String = ""
                    ''''Dim xAkunBiayaNew As String = ""
                    ''''Dim xAkunBiayaHutNew As String = ""

                    ''''SQL = "Select biaya_promo_mbl, hutang_promo_mbl, "
                    ''''SQL = SQL & "biaya_promo, hutang_promo_1, hutang_promo_2, hutang_promo_3, "
                    ''''SQL = SQL & "biaya_promo2, hutang_promo_4,biaya_promo_new, hutang_Promo_New From "
                    ''''SQL = SQL & "stock_owner where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & lksi_invoice & "'"
                    ''''Using dr = OpenTrans(SQL)
                    ''''    Do While dr.Read
                    ''''        'flag_reseller.Add(dr("flag_reseller"))
                    ''''        'arrKategoriPenggantiReseller.Add(General_Class.CekNULL(dr("kategori_pengganti_reseller")))
                    ''''        'arrInisialFaktur.Add(dr("inisial_faktur"))
                    ''''        'arrPersenBrgOrg.Add(dr("persen_brg_org"))
                    ''''        'arrPersenBrgSdr.Add(dr("persen_brg_sdr"))

                    ''''        'arrFlagBudgetingBS.Add(dr("flag_budgeting_bs"))

                    ''''        xAkunBiayaPromo = dr("biaya_promo")
                    ''''        xAkunBiayaHut1 = dr("hutang_promo_1")
                    ''''        xAkunBiayaHut2 = dr("hutang_promo_2")
                    ''''        xAkunBiayaHut3 = dr("hutang_promo_3")

                    ''''        xAkunBiayaPromo2 = dr("biaya_promo2")
                    ''''        xAkunBiayaHut4 = dr("hutang_promo_4")

                    ''''        ' arrFlagBudgetingMbl.Add(dr("flag_budgeting_mbl"))
                    ''''        xAkunBiayaMbl = dr("biaya_promo_mbl")
                    ''''        xAkunBiayaHutMbl = dr("hutang_promo_mbl")

                    ''''        xAkunBiayaNew = dr("biaya_promo_new")
                    ''''        xAkunBiayaHutNew = dr("hutang_Promo_New")
                    ''''        'arrFlagDiskonCash.Add(dr("flag_diskon_cash"))
                    ''''        ' arrFlagKunciInv.Add(dr("flag_kunci_inv"))
                    ''''        'arrJmlKunciInv.Add(dr("jml_kunci_inv"))

                    ''''        'arrPlafonTunai.Add(dr("plafon_tunai"))
                    ''''        'arrMetodePotStock.Add(dr("metode_pot_stock"))

                    ''''        'If dr("flag_default") = "Y" Then
                    ''''        '    ComboBox4.Text = dr("kode_stock_owner")
                    ''''        'End If
                    ''''    Loop
                    ''''End Using

                    ''''Dim pagenumber As Integer = 0

                    ''''Dim Kode_Voucher5 As String = ""
                    ''''Dim __Kode_Voucher5 As String = "NULL"
                    ''''Dim ket_di_jurnal_promo As String = "DO " & nofakdo & ";" & nama_cst & ";" & HilangkanTanda(LvJmlKrm) & "_" & LvNm

                    ''''SQL = "select jenis, sum(hasil) as hasil from do_budgeting where "
                    ''''SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    ''''SQL = SQL & "no_do = '" & nofakdo & "' group by jenis order by jenis"
                    ''''Using Ds = BindingTrans(SQL)
                    ''''    With Ds.Tables("MyTable")
                    ''''        If .Rows.Count <> 0 Then
                    ''''            Kode_Voucher5 = GetLastNumberJurnal(Format(tgl_skg, "yyyyMM"), fJurnalJual & jns_penjualan & init_faktur, KodePerusahaan)
                    ''''            __Kode_Voucher5 = "'" & Kode_Voucher5 & "'"

                    ''''            SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
                    ''''            SQL = SQL & "Keterangan, JudulBank, KetDK, userid, lokasi) values("
                    ''''            SQL = SQL & "'" & Kode_Voucher5 & "', "
                    ''''            SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', "
                    ''''            SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
                    ''''            SQL = SQL & "'" & KodeProyek & "', '" & Strings.Left(ket_di_jurnal_promo, 80) & "', '', "
                    ''''            SQL = SQL & "'-', '" & UserID & "', '" & lksi_invoice & "')"
                    ''''            ExecuteTrans(SQL)

                    ''''            pagenumber = 1
                    ''''            Dim nilai_debit As Double = 0

                    ''''            For b As Integer = 0 To .Rows.Count - 1
                    ''''                Dim akunhutpromo As String = ""

                    ''''                If b = 0 Then
                    ''''                    akunhutpromo = xAkunBiayaHut1
                    ''''                ElseIf b = 1 Then
                    ''''                    akunhutpromo = xAkunBiayaHut2
                    ''''                ElseIf b = 2 Then
                    ''''                    akunhutpromo = xAkunBiayaHut3
                    ''''                Else
                    ''''                    akunhutpromo = "x"
                    ''''                End If

                    ''''                SQL = Get_Detail_Jurnal(Kode_Voucher5, Strings.Left(akunhutpromo, 1),
                    ''''                              Strings.Mid(akunhutpromo, 2, 1),
                    ''''                              Strings.Mid(Ganti(akunhutpromo), 3),
                    ''''                              KodePerusahaan, KodeProyek, Strings.Left(ket_di_jurnal_promo, 80), "0", .Rows(b).Item("hasil"), pagenumber, lksi_invoice)
                    ''''                ExecuteTrans(SQL)
                    ''''                pagenumber = pagenumber + 1

                    ''''                nilai_debit = nilai_debit + .Rows(b).Item("hasil")
                    ''''            Next

                    ''''            SQL = Get_Detail_Jurnal(Kode_Voucher5, Strings.Left(xAkunBiayaPromo, 1),
                    ''''                             Strings.Mid(xAkunBiayaPromo, 2, 1),
                    ''''                             Strings.Mid(Ganti(xAkunBiayaPromo), 3),
                    ''''                             KodePerusahaan, KodeProyek, Strings.Left(ket_di_jurnal_promo, 80), nilai_debit, "0", pagenumber, lksi_invoice)
                    ''''            ExecuteTrans(SQL)
                    ''''            pagenumber = pagenumber + 1

                    ''''            SQL = "select sum(debit) as debit, sum(kredit) as kredit from detail_jurnal where "
                    ''''            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    ''''            SQL = SQL & "kode_voucher = '" & Kode_Voucher5 & "'"
                    ''''            Using Dr = OpenTrans(SQL)
                    ''''                If Dr.Read Then
                    ''''                    If Dr("debit") <> Dr("kredit") Then
                    ''''                        Dr.Close()
                    ''''                        CloseTrans()
                    ''''                        CloseConn()
                    ''''                        MessageBox.Show("Jurnal 1 salah!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    ''''                        Exit Sub
                    ''''                    End If
                    ''''                Else
                    ''''                    Dr.Close()
                    ''''                    CloseTrans()
                    ''''                    CloseConn()
                    ''''                    MessageBox.Show("Data jurnal 1 tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    ''''                    Exit Sub
                    ''''                End If
                    ''''            End Using
                    ''''        End If
                    ''''    End With
                    ''''End Using


                    '''''=============================================

                    '''''=============================================

                    ''''Dim Kode_Voucher6 As String = ""
                    ''''Dim __Kode_Voucher6 As String = "NULL"
                    ''''Dim ket_di_jurnal_promo_mbl As String = "DO " & nofakdo & ";" & nama_cst & ";" & HilangkanTanda(LvJmlKrm) & "_" & LvNm


                    ''''SQL = "select jenis, sum(hasil) as hasil from do_budgeting_mbl where "
                    ''''SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    ''''SQL = SQL & "no_do = '" & nofakdo & "' group by jenis order by jenis"
                    ''''Using Ds = BindingTrans(SQL)
                    ''''    With Ds.Tables("MyTable")
                    ''''        If .Rows.Count <> 0 Then
                    ''''            Kode_Voucher6 = GetLastNumberJurnal(Format(tgl_skg, "yyyyMM"), fJurnalJual & jns_penjualan & init_faktur, KodePerusahaan)
                    ''''            __Kode_Voucher6 = "'" & Kode_Voucher6 & "'"

                    ''''            SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
                    ''''            SQL = SQL & "Keterangan, JudulBank, KetDK, userid, lokasi) values("
                    ''''            SQL = SQL & "'" & Kode_Voucher6 & "', "
                    ''''            SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', "
                    ''''            SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
                    ''''            SQL = SQL & "'" & KodeProyek & "', '" & Strings.Left(ket_di_jurnal_promo_mbl, 80) & "', '', "
                    ''''            SQL = SQL & "'-', '" & UserID & "', '" & lksi_invoice & "')"
                    ''''            ExecuteTrans(SQL)

                    ''''            pagenumber = 1

                    ''''            SQL = Get_Detail_Jurnal(Kode_Voucher6, Strings.Left(xAkunBiayaMbl, 1),
                    ''''                             Strings.Mid(xAkunBiayaMbl, 2, 1),
                    ''''                             Strings.Mid(Ganti(xAkunBiayaMbl), 3),
                    ''''                             KodePerusahaan, KodeProyek, Strings.Left(ket_di_jurnal_promo_mbl, 80), .Rows(0).Item("hasil"), "0", pagenumber, lksi_invoice)
                    ''''            ExecuteTrans(SQL)
                    ''''            pagenumber = pagenumber + 1

                    ''''            SQL = Get_Detail_Jurnal(Kode_Voucher6, Strings.Left(xAkunBiayaHutMbl, 1),
                    ''''                              Strings.Mid(xAkunBiayaHutMbl, 2, 1),
                    ''''                              Strings.Mid(Ganti(xAkunBiayaHutMbl), 3),
                    ''''                              KodePerusahaan, KodeProyek, Strings.Left(ket_di_jurnal_promo_mbl, 80), "0", .Rows(0).Item("hasil"), pagenumber, lksi_invoice)
                    ''''            ExecuteTrans(SQL)
                    ''''            pagenumber = pagenumber + 1

                    ''''            SQL = "select sum(debit) as debit, sum(kredit) as kredit from detail_jurnal where "
                    ''''            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    ''''            SQL = SQL & "kode_voucher = '" & Kode_Voucher6 & "'"
                    ''''            Using Dr = OpenTrans(SQL)
                    ''''                If Dr.Read Then
                    ''''                    If Dr("debit") <> Dr("kredit") Then
                    ''''                        Dr.Close()
                    ''''                        CloseTrans()
                    ''''                        CloseConn()
                    ''''                        MessageBox.Show("Jurnal 1 salah!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    ''''                        Exit Sub
                    ''''                    End If
                    ''''                Else
                    ''''                    Dr.Close()
                    ''''                    CloseTrans()
                    ''''                    CloseConn()
                    ''''                    MessageBox.Show("Data jurnal 1 tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    ''''                    Exit Sub
                    ''''                End If
                    ''''            End Using
                    ''''        End If
                    ''''    End With
                    ''''End Using


                    '''''xxxxxxx

                    ''''Dim Kode_Voucher10 As String = ""
                    ''''Dim __Kode_Voucher10 As String = "NULL"
                    ''''Dim ket_di_jurnal_promo_new As String = "DO_ " & nofakdo & ";" & nama_cst & ";" & HilangkanTanda(LvJmlKrm) & "_" & LvNm


                    ''''SQL = "select jenis, sum(hasil) as hasil from do_budgeting_new where "
                    ''''SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    ''''SQL = SQL & "no_do = '" & nofakdo & "' group by jenis order by jenis"
                    ''''Using Ds = BindingTrans(SQL)
                    ''''    With Ds.Tables("MyTable")
                    ''''        If .Rows.Count <> 0 Then
                    ''''            Kode_Voucher10 = GetLastNumberJurnal(Format(tgl_skg, "yyyyMM"), fJurnalJual & jns_penjualan & init_faktur, KodePerusahaan)
                    ''''            __Kode_Voucher10 = "'" & Kode_Voucher10 & "'"

                    ''''            SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
                    ''''            SQL = SQL & "Keterangan, JudulBank, KetDK, userid, lokasi) values("
                    ''''            SQL = SQL & "'" & Kode_Voucher10 & "', "
                    ''''            SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', "
                    ''''            SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
                    ''''            SQL = SQL & "'" & KodeProyek & "', '" & Strings.Left(ket_di_jurnal_promo_new, 80) & "', '', "
                    ''''            SQL = SQL & "'-', '" & UserID & "', '" & lksi_invoice & "')"
                    ''''            ExecuteTrans(SQL)

                    ''''            pagenumber = 1

                    ''''            SQL = Get_Detail_Jurnal(Kode_Voucher10, Strings.Left(xAkunBiayaNew, 1),
                    ''''                             Strings.Mid(xAkunBiayaNew, 2, 1),
                    ''''                             Strings.Mid(Ganti(xAkunBiayaNew), 3),
                    ''''                             KodePerusahaan, KodeProyek, Strings.Left(ket_di_jurnal_promo_new, 80), .Rows(0).Item("hasil"), "0", pagenumber, lksi_invoice)
                    ''''            ExecuteTrans(SQL)
                    ''''            pagenumber = pagenumber + 1

                    ''''            SQL = Get_Detail_Jurnal(Kode_Voucher10, Strings.Left(xAkunBiayaHutNew, 1),
                    ''''                              Strings.Mid(xAkunBiayaHutNew, 2, 1),
                    ''''                              Strings.Mid(Ganti(xAkunBiayaHutNew), 3),
                    ''''                              KodePerusahaan, KodeProyek, Strings.Left(ket_di_jurnal_promo_new, 80), "0", .Rows(0).Item("hasil"), pagenumber, lksi_invoice)
                    ''''            ExecuteTrans(SQL)
                    ''''            pagenumber = pagenumber + 1

                    ''''            SQL = "select sum(debit) as debit, sum(kredit) as kredit from detail_jurnal where "
                    ''''            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    ''''            SQL = SQL & "kode_voucher = '" & Kode_Voucher10 & "'"
                    ''''            Using Dr = OpenTrans(SQL)
                    ''''                If Dr.Read Then
                    ''''                    If Dr("debit") <> Dr("kredit") Then
                    ''''                        Dr.Close()
                    ''''                        CloseTrans()
                    ''''                        CloseConn()
                    ''''                        MessageBox.Show("Jurnal 1 salah!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    ''''                        Exit Sub
                    ''''                    End If
                    ''''                Else
                    ''''                    Dr.Close()
                    ''''                    CloseTrans()
                    ''''                    CloseConn()
                    ''''                    MessageBox.Show("Data jurnal 1 tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    ''''                    Exit Sub
                    ''''                End If
                    ''''            End Using
                    ''''        End If
                    ''''    End With
                    ''''End Using

                    '''''xxxxxxxxx

                    ''''Dim Kode_Voucher7 As String = ""
                    ''''Dim __Kode_Voucher7 As String = "NULL"
                    ''''Dim ket_di_jurnal_promo_2 As String = "DO BS " & nofakdo & ";" & nama_cst & ";" & HilangkanTanda(LvJmlKrm) & "_" & LvNm


                    ''''SQL = "select jenis, sum(hasil) as hasil from do_budgeting_2 where "
                    ''''SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    ''''SQL = SQL & "no_do = '" & nofakdo & "' group by jenis order by jenis"
                    ''''Using Ds = BindingTrans(SQL)
                    ''''    With Ds.Tables("MyTable")
                    ''''        If .Rows.Count <> 0 Then
                    ''''            Kode_Voucher7 = GetLastNumberJurnal(Format(tgl_skg, "yyyyMM"), fJurnalJual & jns_penjualan & init_faktur, KodePerusahaan)
                    ''''            __Kode_Voucher7 = "'" & Kode_Voucher7 & "'"


                    ''''            SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
                    ''''            SQL = SQL & "Keterangan, JudulBank, KetDK, userid, lokasi) values("
                    ''''            SQL = SQL & "'" & Kode_Voucher7 & "', "
                    ''''            SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', "
                    ''''            SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
                    ''''            SQL = SQL & "'" & KodeProyek & "', '" & Strings.Left(ket_di_jurnal_promo_2, 80) & "', '', "
                    ''''            SQL = SQL & "'-', '" & UserID & "', '" & lksi_invoice & "')"
                    ''''            ExecuteTrans(SQL)

                    ''''            pagenumber = 1
                    ''''            Dim nilai_debit As Double = 0

                    ''''            For b As Integer = 0 To .Rows.Count - 1
                    ''''                Dim akunhutpromo As String = ""

                    ''''                If b = 0 Then
                    ''''                    akunhutpromo = xAkunBiayaHut1
                    ''''                ElseIf b = 1 Then
                    ''''                    akunhutpromo = xAkunBiayaHut2
                    ''''                ElseIf b = 2 Then
                    ''''                    akunhutpromo = xAkunBiayaHut3
                    ''''                Else
                    ''''                    akunhutpromo = "x"
                    ''''                End If

                    ''''                SQL = Get_Detail_Jurnal(Kode_Voucher7, Strings.Left(akunhutpromo, 1),
                    ''''                              Strings.Mid(akunhutpromo, 2, 1),
                    ''''                              Strings.Mid(Ganti(akunhutpromo), 3),
                    ''''                              KodePerusahaan, KodeProyek, Strings.Left(ket_di_jurnal_promo_2, 80), "0", .Rows(b).Item("hasil"), pagenumber, lksi_invoice)
                    ''''                ExecuteTrans(SQL)
                    ''''                pagenumber = pagenumber + 1

                    ''''                nilai_debit = nilai_debit + .Rows(b).Item("hasil")
                    ''''            Next

                    ''''            SQL = Get_Detail_Jurnal(Kode_Voucher7, Strings.Left(xAkunBiayaPromo, 1),
                    ''''                             Strings.Mid(xAkunBiayaPromo, 2, 1),
                    ''''                             Strings.Mid(Ganti(xAkunBiayaPromo), 3),
                    ''''                             KodePerusahaan, KodeProyek, Strings.Left(ket_di_jurnal_promo_2, 80), nilai_debit, "0", pagenumber, lksi_invoice)
                    ''''            ExecuteTrans(SQL)
                    ''''            pagenumber = pagenumber + 1



                    ''''            SQL = "select sum(debit) as debit, sum(kredit) as kredit from detail_jurnal where "
                    ''''            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    ''''            SQL = SQL & "kode_voucher = '" & Kode_Voucher7 & "'"
                    ''''            Using Dr = OpenTrans(SQL)
                    ''''                If Dr.Read Then
                    ''''                    If Dr("debit") <> Dr("kredit") Then
                    ''''                        Dr.Close()
                    ''''                        CloseTrans()
                    ''''                        CloseConn()
                    ''''                        MessageBox.Show("Jurnal 1 salah!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    ''''                        Exit Sub
                    ''''                    End If
                    ''''                Else
                    ''''                    Dr.Close()
                    ''''                    CloseTrans()
                    ''''                    CloseConn()
                    ''''                    MessageBox.Show("Data jurnal 1 tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    ''''                    Exit Sub
                    ''''                End If
                    ''''            End Using
                    ''''        End If
                    ''''    End With
                    ''''End Using

                    '''''==========

                    ''''Dim Kode_Voucher8 As String = ""
                    ''''Dim __Kode_Voucher8 As String = "NULL"
                    ''''Dim ket_di_jurnal_promo_3 As String = "DO TOTO " & nofakdo & ";" & nama_cst & ";" & HilangkanTanda(LvJmlKrm) & "_" & LvNm


                    ''''SQL = "select jenis, sum(hasil) as hasil from do_budgeting_3 where "
                    ''''SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    ''''SQL = SQL & "no_do = '" & nofakdo & "' group by jenis order by jenis"
                    ''''Using Ds = BindingTrans(SQL)
                    ''''    With Ds.Tables("MyTable")
                    ''''        If .Rows.Count <> 0 Then
                    ''''            Kode_Voucher8 = GetLastNumberJurnal(Format(tgl_skg, "yyyyMM"), fJurnalJual & jns_penjualan & init_faktur, KodePerusahaan)
                    ''''            __Kode_Voucher8 = "'" & Kode_Voucher8 & "'"


                    ''''            SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
                    ''''            SQL = SQL & "Keterangan, JudulBank, KetDK, userid, lokasi) values("
                    ''''            SQL = SQL & "'" & Kode_Voucher8 & "', "
                    ''''            SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', "
                    ''''            SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
                    ''''            SQL = SQL & "'" & KodeProyek & "', '" & Strings.Left(ket_di_jurnal_promo_3, 80) & "', '', "
                    ''''            SQL = SQL & "'-', '" & UserID & "', '" & lksi_invoice & "')"
                    ''''            ExecuteTrans(SQL)

                    ''''            pagenumber = 1
                    ''''            Dim nilai_debit As Double = 0

                    ''''            For b As Integer = 0 To .Rows.Count - 1
                    ''''                Dim akunhutpromo As String = ""

                    ''''                If b = 0 Then
                    ''''                    akunhutpromo = xAkunBiayaHut1
                    ''''                ElseIf b = 1 Then
                    ''''                    akunhutpromo = xAkunBiayaHut2
                    ''''                ElseIf b = 2 Then
                    ''''                    akunhutpromo = xAkunBiayaHut3
                    ''''                Else
                    ''''                    akunhutpromo = "x"
                    ''''                End If

                    ''''                SQL = Get_Detail_Jurnal(Kode_Voucher8, Strings.Left(akunhutpromo, 1),
                    ''''                              Strings.Mid(akunhutpromo, 2, 1),
                    ''''                              Strings.Mid(Ganti(akunhutpromo), 3),
                    ''''                              KodePerusahaan, KodeProyek, Strings.Left(ket_di_jurnal_promo_3, 80), "0", .Rows(b).Item("hasil"), pagenumber, lksi_invoice)
                    ''''                ExecuteTrans(SQL)
                    ''''                pagenumber = pagenumber + 1

                    ''''                nilai_debit = nilai_debit + .Rows(b).Item("hasil")
                    ''''            Next

                    ''''            SQL = Get_Detail_Jurnal(Kode_Voucher8, Strings.Left(xAkunBiayaPromo, 1),
                    ''''                             Strings.Mid(xAkunBiayaPromo, 2, 1),
                    ''''                             Strings.Mid(Ganti(xAkunBiayaPromo), 3),
                    ''''                             KodePerusahaan, KodeProyek, Strings.Left(ket_di_jurnal_promo_3, 80), nilai_debit, "0", pagenumber, lksi_invoice)
                    ''''            ExecuteTrans(SQL)
                    ''''            pagenumber = pagenumber + 1



                    ''''            SQL = "select sum(debit) as debit, sum(kredit) as kredit from detail_jurnal where "
                    ''''            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    ''''            SQL = SQL & "kode_voucher = '" & Kode_Voucher8 & "'"
                    ''''            Using Dr = OpenTrans(SQL)
                    ''''                If Dr.Read Then
                    ''''                    If Dr("debit") <> Dr("kredit") Then
                    ''''                        Dr.Close()
                    ''''                        CloseTrans()
                    ''''                        CloseConn()
                    ''''                        MessageBox.Show("Jurnal 1 salah!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    ''''                        Exit Sub
                    ''''                    End If
                    ''''                Else
                    ''''                    Dr.Close()
                    ''''                    CloseTrans()
                    ''''                    CloseConn()
                    ''''                    MessageBox.Show("Data jurnal 1 tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    ''''                    Exit Sub
                    ''''                End If
                    ''''            End Using
                    ''''        End If
                    ''''    End With
                    ''''End Using

                    '''''==========

                    ''''Dim Kode_Voucher9 As String = ""
                    ''''Dim __Kode_Voucher9 As String = "NULL"
                    ''''Dim ket_di_jurnal_promo_4 As String = "DO BIOCRM " & nofakdo & ";" & nama_cst & ";" & HilangkanTanda(LvJmlKrm) & "_" & LvNm


                    ''''SQL = "select jenis, sum(hasil) as hasil from do_budgeting_4 where "
                    ''''SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    ''''SQL = SQL & "no_do = '" & nofakdo & "' group by jenis order by jenis"
                    ''''Using Ds = BindingTrans(SQL)
                    ''''    With Ds.Tables("MyTable")
                    ''''        If .Rows.Count <> 0 Then
                    ''''            Kode_Voucher9 = GetLastNumberJurnal(Format(tgl_skg, "yyyyMM"), fJurnalJual & jns_penjualan & init_faktur, KodePerusahaan)
                    ''''            __Kode_Voucher9 = "'" & Kode_Voucher9 & "'"


                    ''''            SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
                    ''''            SQL = SQL & "Keterangan, JudulBank, KetDK, userid, lokasi) values("
                    ''''            SQL = SQL & "'" & Kode_Voucher9 & "', "
                    ''''            SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', "
                    ''''            SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
                    ''''            SQL = SQL & "'" & KodeProyek & "', '" & Strings.Left(ket_di_jurnal_promo_4, 80) & "', '', "
                    ''''            SQL = SQL & "'-', '" & UserID & "', '" & lksi_invoice & "')"
                    ''''            ExecuteTrans(SQL)

                    ''''            pagenumber = 1
                    ''''            Dim nilai_debit As Double = 0

                    ''''            'For b As Integer = 0 To .Rows.Count - 1
                    ''''            Dim akunhutpromo As String = xAkunBiayaHut4


                    ''''            SQL = Get_Detail_Jurnal(Kode_Voucher9, Strings.Left(akunhutpromo, 1),
                    ''''                          Strings.Mid(akunhutpromo, 2, 1),
                    ''''                          Strings.Mid(Ganti(akunhutpromo), 3),
                    ''''                          KodePerusahaan, KodeProyek, Strings.Left(ket_di_jurnal_promo_4, 80), "0", .Rows(0).Item("hasil"), pagenumber, lksi_invoice)
                    ''''            ExecuteTrans(SQL)
                    ''''            pagenumber = pagenumber + 1

                    ''''            nilai_debit = nilai_debit + .Rows(0).Item("hasil")
                    ''''            ' Next

                    ''''            SQL = Get_Detail_Jurnal(Kode_Voucher9, Strings.Left(xAkunBiayaPromo2, 1),
                    ''''                             Strings.Mid(xAkunBiayaPromo2, 2, 1),
                    ''''                             Strings.Mid(Ganti(xAkunBiayaPromo2), 3),
                    ''''                             KodePerusahaan, KodeProyek, Strings.Left(ket_di_jurnal_promo_4, 80), nilai_debit, "0", pagenumber, lksi_invoice)
                    ''''            ExecuteTrans(SQL)
                    ''''            pagenumber = pagenumber + 1



                    ''''            SQL = "select sum(debit) as debit, sum(kredit) as kredit from detail_jurnal where "
                    ''''            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    ''''            SQL = SQL & "kode_voucher = '" & Kode_Voucher9 & "'"
                    ''''            Using Dr = OpenTrans(SQL)
                    ''''                If Dr.Read Then
                    ''''                    If Dr("debit") <> Dr("kredit") Then
                    ''''                        Dr.Close()
                    ''''                        CloseTrans()
                    ''''                        CloseConn()
                    ''''                        MessageBox.Show("Jurnal 1 salah!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    ''''                        Exit Sub
                    ''''                    End If
                    ''''                Else
                    ''''                    Dr.Close()
                    ''''                    CloseTrans()
                    ''''                    CloseConn()
                    ''''                    MessageBox.Show("Data jurnal 1 tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    ''''                    Exit Sub
                    ''''                End If
                    ''''            End Using
                    ''''        End If
                    ''''    End With
                    ''''End Using
                    '''''==========


                    ''''SQL = "update do_new set "
                    ''''SQL = SQL & "kode_voucher_5 = " & __Kode_Voucher5 & ", "
                    ''''SQL = SQL & "kode_voucher_6 = " & __Kode_Voucher6 & ", "
                    ''''SQL = SQL & "kode_voucher_7 = " & __Kode_Voucher7 & ", "
                    ''''SQL = SQL & "kode_voucher_8 = " & __Kode_Voucher8 & ", "
                    ''''SQL = SQL & "kode_voucher_9 = " & __Kode_Voucher9 & ", "
                    ''''SQL = SQL & "kode_voucher_10 = " & __Kode_Voucher10 & " "
                    ''''SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
                    ''''SQL = SQL & "no_do = '" & nofakdo & "'"
                    ''''ExecuteTrans(SQL)
                End If


                SQL = "select kode_perusahaan from Penjualan_Blm_Selesai_Kirim where "
                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "no_faktur = '" & ListView1.FocusedItem.Text & "' and "
                'SQL = SQL & "jumlah - rtr <> sdh_selesai_validasi + retur_val_do  - retur_sub_inv "
                SQL = SQL & "jumlah - rtr - sdh_selesai_validasi - retur_val_do <> 0 "
                Using dr = OpenTrans(SQL)
                    If Not (dr.Read) Then
                        dr.Close()

                        SQL = "update penjualan set flag_do_selesai = 'Y' where "
                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "no_faktur = '" & ListView1.FocusedItem.Text & "'"
                        ExecuteTrans(SQL)
                    End If
                End Using

                '===============================
                'cash diskon
                '===============================

                Dim arr_kode_promo As New ArrayList

                SQL = "select a.no_faktur from Master_Promo a, Master_Promo_Lokasi b where "
                SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan and a.no_faktur = b.no_faktur and "
                SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "status is null and a.periode_akhir >= '" & Format(tgl_skg, "yyyy-MM-dd") & "' AND a.flag_customers_seluruh = 'Y' and "
                SQL = SQL & "Jenis_Promo IN('Cash Discount', 'Add Cash Discount') and b.kode_stock_owner = '" & lksi_invoice & "' and a.Flag_Validasi_ACC = 'Y' and a.Flag_Validasi_HO = 'Y' "

                SQL = SQL & "union " ' jgn pake union all

                SQL = SQL & "select a.no_faktur from Master_Promo a, Master_Promo_customers b where "
                SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan and a.no_faktur = b.no_faktur and "
                SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "status is null and a.periode_akhir >= '" & Format(tgl_skg, "yyyy-MM-dd") & "' AND a.flag_customers_seluruh is null and "
                SQL = SQL & "Jenis_Promo IN('Cash Discount', 'Add Cash Discount') and b.kode_customer = '" & f_kd_customer & "' and a.Flag_Validasi_ACC = 'Y' and a.Flag_Validasi_HO = 'Y' "
                Using Dr = OpenTrans(SQL)
                    Do While Dr.Read
                        arr_kode_promo.Add(Dr("no_faktur"))
                    Loop
                End Using



                For z As Integer = 0 To arr_kode_promo.Count - 1
                    Dim flag_seluruh_kat_bsr As String = ""
                    Dim flag_seluruh_kat_kcl As String = ""
                    Dim flag_seluruh_brg As String = ""

                    Dim flag_seluruh_kat_bsr_klaim As String = ""
                    Dim flag_seluruh_kat_kcl_klaim As String = ""
                    Dim flag_seluruh_brg_klaim As String = ""

                    Dim jns_promo As String = ""
                    Dim Flag_Eleminasi_Promo_Induk As String = ""
                    Dim No_Faktur_Untuk_Di_Eleminasi As String = ""

                    SQL = "delete from Master_Promo_Kategori_sementara where "
                    SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "no_do = '" & nofakdo & "'"
                    ExecuteTrans(SQL)

                    SQL = "delete from Master_Promo_kategori_kecil_sementara where "
                    SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "no_do = '" & nofakdo & "'"
                    ExecuteTrans(SQL)

                    SQL = "delete from Master_Promo_Barang_Sementara where "
                    SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "no_do = '" & nofakdo & "'"
                    ExecuteTrans(SQL)

                    '------------
                    'klaim 
                    '------------

                    SQL = "delete from Master_Promo_Kategori_hitung_klaim_sementara where "
                    SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "no_do = '" & nofakdo & "'"
                    ExecuteTrans(SQL)

                    SQL = "delete from Master_Promo_kategori_kecil_hitung_klaim_sementara where "
                    SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "no_do = '" & nofakdo & "'"
                    ExecuteTrans(SQL)

                    SQL = "delete from Master_Promo_Barang_hitung_klaim_Sementara where "
                    SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "no_do = '" & nofakdo & "'"
                    ExecuteTrans(SQL)

                    SQL = "select No_Faktur, Flag_Customers_Seluruh, Flag_Merk_Seluruh, Flag_Kategori_Besar_Seluruh, Flag_Kategori_Kecil_Seluruh, Flag_Barang_seluruh, "
                    SQL = SQL & "Flag_Kategori_Merk_Seluruh_Klaim, Flag_Kategori_Besar_Seluruh_Klaim, Flag_Kategori_Kecil_Seluruh_Klaim, Flag_Kategori_Barang_Seluruh_Klaim, "
                    SQL = SQL & "jenis_promo, Flag_Eliminasi_Promo_Induk, No_Faktur_Untuk_Di_Eliminasi "
                    SQL = SQL & "from Master_Promo where "
                    SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "no_faktur = '" & arr_kode_promo.Item(z) & "'"
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            flag_seluruh_kat_bsr = General_Class.CekNULL(Dr("Flag_Kategori_Besar_Seluruh"))
                            flag_seluruh_kat_kcl = General_Class.CekNULL(Dr("Flag_Kategori_kecil_Seluruh"))
                            flag_seluruh_brg = General_Class.CekNULL(Dr("Flag_Barang_seluruh"))

                            flag_seluruh_kat_bsr_klaim = General_Class.CekNULL(Dr("Flag_Kategori_Besar_Seluruh_Klaim"))
                            flag_seluruh_kat_kcl_klaim = General_Class.CekNULL(Dr("Flag_Kategori_Kecil_Seluruh_Klaim"))
                            flag_seluruh_brg_klaim = General_Class.CekNULL(Dr("Flag_Kategori_Barang_Seluruh_Klaim"))


                            jns_promo = General_Class.CekNULL(Dr("jenis_promo"))
                            Flag_Eleminasi_Promo_Induk = General_Class.CekNULL(Dr("Flag_Eliminasi_Promo_Induk"))
                            No_Faktur_Untuk_Di_Eleminasi = General_Class.CekNULL(Dr("No_Faktur_Untuk_Di_Eliminasi"))

                        Else
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Promo tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using

                    If flag_seluruh_kat_bsr = "Y" Then
                        SQL = "insert into Master_Promo_Kategori_sementara(Kode_Perusahaan, No_Faktur, No_DO, Kode_Kategori_Besar, Kode_Merk) "
                        SQL = SQL & "select a.Kode_Perusahaan, b.no_faktur, '" & nofakdo & "', a.kode_kategori_besar, a.kode_merk from "
                        SQL = SQL & "Kategori_Besar a, master_promo b where "
                        SQL = SQL & "a.Kode_Perusahaan = b.kode_perusahaan and "
                        SQL = SQL & "b.Flag_Kategori_Besar_Seluruh = 'Y' and "
                        SQL = SQL & "a.Flag_Tampil_Master_Rebate = 'Y' and "
                        SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "b.no_faktur = '" & arr_kode_promo.Item(z) & "' and "
                        SQL = SQL & "b.Status is null and a.Kode_Merk in("
                        SQL = SQL & "select x.Kode_Merk from Master_Promo_Merk x where x.Kode_Perusahaan = a.Kode_Perusahaan and "
                        SQL = SQL & "x.No_Faktur = b.no_faktur and "
                        SQL = SQL & "x.Kode_Perusahaan = a.kode_perusahaan"
                        SQL = SQL & ")"
                        ExecuteTrans(SQL)
                    Else
                        SQL = "insert into Master_Promo_Kategori_sementara(Kode_Perusahaan, No_Faktur, No_DO, Kode_Kategori_Besar, Kode_Merk) "
                        SQL = SQL & "select Kode_Perusahaan, No_Faktur, '" & nofakdo & "', Kode_Kategori_Besar, Kode_Merk from master_promo_kategori where "
                        SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "no_faktur = '" & arr_kode_promo.Item(z) & "' "
                        ExecuteTrans(SQL)
                    End If

                    If flag_seluruh_kat_kcl = "Y" Then
                        SQL = "insert into Master_Promo_kategori_kecil_sementara(Kode_Perusahaan, No_Faktur, No_DO, Kode_Kategori_Besar, Kode_Kategori_Kecil) "
                        SQL = SQL & "select a.Kode_Perusahaan, b.no_faktur, '" & nofakdo & "', a.kode_kategori_besar, a.kode_kategori_kecil from "
                        SQL = SQL & "Kategori_Kecil a, master_promo b where "
                        SQL = SQL & "a.Kode_Perusahaan = b.kode_perusahaan and "
                        SQL = SQL & "b.flag_kategori_kecil_seluruh = 'Y' and "
                        '  SQL = SQL & "a.Flag_Tampil_Master_Rebate = 'Y' and "
                        SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "b.no_faktur = '" & arr_kode_promo.Item(z) & "' "
                        SQL = SQL & "and b.Status is null and a.Kode_Kategori_Besar in("
                        SQL = SQL & "select x.Kode_Kategori_Besar from Master_Promo_Kategori_Sementara x where x.Kode_Perusahaan = a.Kode_Perusahaan and "
                        SQL = SQL & "x.No_Faktur = b.no_faktur and "
                        SQL = SQL & "x.No_DO = '" & nofakdo & "' and "
                        SQL = SQL & "x.Kode_Perusahaan = a.kode_perusahaan"
                        SQL = SQL & ")"
                        ExecuteTrans(SQL)
                    Else
                        SQL = "insert into Master_Promo_kategori_kecil_sementara(Kode_Perusahaan, No_Faktur, No_DO, Kode_Kategori_Besar, Kode_Kategori_Kecil) "
                        SQL = SQL & "select Kode_Perusahaan, No_Faktur, '" & nofakdo & "', Kode_Kategori_Besar, Kode_Kategori_Kecil from master_promo_kategori_kecil where "
                        SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "no_faktur = '" & arr_kode_promo.Item(z) & "' "
                        ExecuteTrans(SQL)
                    End If

                    If flag_seluruh_brg = "Y" Then
                        SQL = "insert into Master_Promo_Barang_Sementara(Kode_Perusahaan, No_Faktur, No_DO, Kode_Barang) "
                        SQL = SQL & "select a.Kode_Perusahaan, b.no_faktur, '" & nofakdo & "', a.kode_barang from "
                        SQL = SQL & "barang a, Master_Promo_kategori_kecil_sementara b where "
                        SQL = SQL & "a.Kode_Perusahaan = b.kode_perusahaan and "
                        SQL = SQL & "a.kode_kategori_besar = b.kode_kategori_besar and a.kode_kategori_kecil = b.kode_kategori_kecil and "
                        SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "b.no_faktur = '" & arr_kode_promo.Item(z) & "' and "
                        SQL = SQL & "b.No_DO = '" & nofakdo & "' and a.kode_stock_owner = '" & lksi_invoice & "'"
                        ExecuteTrans(SQL)
                    Else
                        SQL = "insert into Master_Promo_Barang_Sementara(Kode_Perusahaan, No_Faktur, No_DO, Kode_Barang) "
                        SQL = SQL & "select Kode_Perusahaan, No_Faktur, '" & nofakdo & "', kode_barang from master_promo_kategori_barang where "
                        SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "no_faktur = '" & arr_kode_promo.Item(z) & "' "
                        ExecuteTrans(SQL)
                    End If

                    '----------------------
                    'klaim   Flag_Kategori_Besar_Seluruh_Klaim, Flag_Kategori_Kecil_Seluruh_Klaim, Flag_Kategori_Barang_Seluruh_Klaim, 
                    '----------------------

                    If flag_seluruh_kat_bsr_klaim = "Y" Then
                        SQL = "insert into Master_Promo_Kategori_hitung_klaim_sementara(Kode_Perusahaan, No_Faktur, No_DO, Kode_Kategori_Besar, Kode_Merk) "
                        SQL = SQL & "select a.Kode_Perusahaan, b.no_faktur, '" & nofakdo & "', a.kode_kategori_besar, a.kode_merk from "
                        SQL = SQL & "Kategori_Besar a, master_promo b where "
                        SQL = SQL & "a.Kode_Perusahaan = b.kode_perusahaan and "
                        SQL = SQL & "b.Flag_Kategori_Besar_Seluruh_Klaim = 'Y' and "
                        SQL = SQL & "a.Flag_Tampil_Master_Rebate = 'Y' and "
                        SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "b.no_faktur = '" & arr_kode_promo.Item(z) & "' and "
                        SQL = SQL & "b.Status is null and a.Kode_Merk in("
                        SQL = SQL & "select x.Kode_Merk from Master_Promo_Merk x where x.Kode_Perusahaan = a.Kode_Perusahaan and "
                        SQL = SQL & "x.No_Faktur = b.no_faktur and "
                        SQL = SQL & "x.Kode_Perusahaan = a.kode_perusahaan"
                        SQL = SQL & ")"
                        ExecuteTrans(SQL)
                    Else
                        SQL = "insert into Master_Promo_Kategori_hitung_klaim_sementara(Kode_Perusahaan, No_Faktur, No_DO, Kode_Kategori_Besar, Kode_Merk) "
                        SQL = SQL & "select Kode_Perusahaan, No_Faktur, '" & nofakdo & "', Kode_Kategori_Besar, Kode_Merk from master_promo_kategori_hitung_klaim where "
                        SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "no_faktur = '" & arr_kode_promo.Item(z) & "' "
                        ExecuteTrans(SQL)
                    End If

                    If flag_seluruh_kat_kcl_klaim = "Y" Then
                        SQL = "insert into Master_Promo_kategori_kecil_hitung_klaim_sementara(Kode_Perusahaan, No_Faktur, No_DO, Kode_Kategori_Besar, Kode_Kategori_Kecil) "
                        SQL = SQL & "select a.Kode_Perusahaan, b.no_faktur, '" & nofakdo & "', a.kode_kategori_besar, a.kode_kategori_kecil from "
                        SQL = SQL & "Kategori_Kecil a, master_promo b where "
                        SQL = SQL & "a.Kode_Perusahaan = b.kode_perusahaan and "
                        SQL = SQL & "b.Flag_Kategori_Kecil_Seluruh_Klaim = 'Y' and "
                        '  SQL = SQL & "a.Flag_Tampil_Master_Rebate = 'Y' and "
                        SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "b.no_faktur = '" & arr_kode_promo.Item(z) & "' "
                        SQL = SQL & "and b.Status is null and a.Kode_Kategori_Besar in("
                        SQL = SQL & "select x.Kode_Kategori_Besar from Master_Promo_Kategori_hitung_klaim_Sementara x where x.Kode_Perusahaan = a.Kode_Perusahaan and "
                        SQL = SQL & "x.No_Faktur = b.no_faktur and "
                        SQL = SQL & "x.No_DO = '" & nofakdo & "' and "
                        SQL = SQL & "x.Kode_Perusahaan = a.kode_perusahaan"
                        SQL = SQL & ")"
                        ExecuteTrans(SQL)
                    Else
                        SQL = "insert into Master_Promo_kategori_kecil_hitung_klaim_sementara(Kode_Perusahaan, No_Faktur, No_DO, Kode_Kategori_Besar, Kode_Kategori_Kecil) "
                        SQL = SQL & "select Kode_Perusahaan, No_Faktur, '" & nofakdo & "', Kode_Kategori_Besar, Kode_Kategori_Kecil from master_promo_kategori_kecil_hitung_klaim where "
                        SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "no_faktur = '" & arr_kode_promo.Item(z) & "' "
                        ExecuteTrans(SQL)
                    End If

                    If flag_seluruh_brg_klaim = "Y" Then
                        SQL = "insert into Master_Promo_Barang_hitung_klaim_Sementara(Kode_Perusahaan, No_Faktur, No_DO, Kode_Barang) "
                        SQL = SQL & "select a.Kode_Perusahaan, b.no_faktur, '" & nofakdo & "', a.kode_barang from "
                        SQL = SQL & "barang a, Master_Promo_kategori_kecil_hitung_klaim_sementara b where "
                        SQL = SQL & "a.Kode_Perusahaan = b.kode_perusahaan and "
                        SQL = SQL & "a.kode_kategori_besar = b.kode_kategori_besar and a.kode_kategori_kecil = b.kode_kategori_kecil and "
                        SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "b.no_faktur = '" & arr_kode_promo.Item(z) & "' and "
                        SQL = SQL & "b.No_DO = '" & nofakdo & "' and a.kode_stock_owner = '" & lksi_invoice & "'"
                        ExecuteTrans(SQL)
                    Else
                        SQL = "insert into Master_Promo_Barang_hitung_klaim_Sementara(Kode_Perusahaan, No_Faktur, No_DO, Kode_Barang) "
                        SQL = SQL & "select Kode_Perusahaan, No_Faktur, '" & nofakdo & "', Kode_Kategori_Besar, Kode_Kategori_Kecil,kode_barang from master_promo_kategori_barang_hitung_klaim where "
                        SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "no_faktur = '" & arr_kode_promo.Item(z) & "' "
                        ExecuteTrans(SQL)
                    End If

                    SQL = "declare @selisih_jam int; "
                    SQL = SQL & "select @selisih_jam = selisih_jam from init; "

                    SQL = SQL & ";with cte as ( "
                    SQL = SQL & "select a.kode_perusahaan, a.No_DO, a.Tanggal, a.No_Faktur, e.Kode_Customer, e.nama as nama_cust, "
                    SQL = SQL & "d.Lokasi, b.Kode_Stock_Owner as Lokasi_Gudang, b.urut_oto, b.Kode_Barang, c.nama, h.kode_merk, c.Kode_Kategori_Besar, c.Kode_Kategori_Kecil, "
                    SQL = SQL & "b.Jumlah, "
                    SQL = SQL & "isnull(( "
                    SQL = SQL & "select 'Y' from "
                    SQL = SQL & "Master_Promo x , Master_Promo_Barang_Sementara y where "
                    SQL = SQL & "x.Kode_Perusahaan = y.Kode_Perusahaan and "
                    SQL = SQL & "x.no_faktur = y.No_Faktur and "
                    SQL = SQL & "y.no_do = a.no_do and "
                    SQL = SQL & "y.kode_barang = b.kode_barang and "
                    SQL = SQL & "x.kode_perusahaan = a.kode_perusahaan and "
                    SQL = SQL & "x.No_Faktur = '" & arr_kode_promo.Item(z) & "' "
                    SQL = SQL & "), 'T') as memenuhi, "

                    SQL = SQL & "isnull(("
                    SQL = SQL & "select persen_cash_diskon from "
                    'SQL = SQL & "(case "
                    'SQL = SQL & "when dateadd(d, Jumlah_Hari, a.tanggal) >= dateadd(d, @selisih_jam, getdate()) then jumlah_hari "
                    'SQL = SQL & "else 0 "
                    'SQL = SQL & "end) as Jml_Hari from "
                    SQL = SQL & "Master_Promo x , Master_Promo_Barang_Hitung_Klaim_Sementara y where "
                    SQL = SQL & "x.Kode_Perusahaan = y.Kode_Perusahaan and "
                    SQL = SQL & "x.no_faktur = y.No_Faktur and "
                    SQL = SQL & "y.no_do = a.no_do and "
                    SQL = SQL & "y.kode_barang = b.kode_barang and "
                    SQL = SQL & "x.kode_perusahaan = a.kode_perusahaan and "
                    SQL = SQL & "x.No_Faktur = '" & arr_kode_promo.Item(z) & "' "
                    SQL = SQL & "), 0) as Persen_Disc_cash, "

                    SQL = SQL & "isnull(("
                    SQL = SQL & "select Jumlah_Hari from "
                    'SQL = SQL & "(case "
                    'SQL = SQL & "when dateadd(d, Jumlah_Hari, a.tanggal) >= dateadd(d, @selisih_jam, getdate()) then jumlah_hari "
                    'SQL = SQL & "else 0 "
                    'SQL = SQL & "end) as Jml_Hari from "
                    SQL = SQL & "Master_Promo x , Master_Promo_Barang_Hitung_Klaim_Sementara y where "
                    SQL = SQL & "x.Kode_Perusahaan = y.Kode_Perusahaan and "
                    SQL = SQL & "x.no_faktur = y.No_Faktur and "
                    SQL = SQL & "y.no_do = a.no_do and "
                    SQL = SQL & "y.kode_barang = b.kode_barang and "
                    SQL = SQL & "x.kode_perusahaan = a.kode_perusahaan and "
                    SQL = SQL & "x.No_Faktur = '" & arr_kode_promo.Item(z) & "' "
                    SQL = SQL & "), 0) as Jumlah_Hari "

                    SQL = SQL & "from do_new a, detail_do_new b, barang c, penjualan d, customers e, Kategori_Kecil f, Kategori_Besar g, merk h where "
                    SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = C.Kode_Perusahaan and "
                    SQL = SQL & "c.Kode_Perusahaan = d.Kode_Perusahaan and d.Kode_Perusahaan = e.Kode_Perusahaan and "
                    SQL = SQL & "e.Kode_Perusahaan = f.Kode_Perusahaan and f.Kode_Perusahaan = g.Kode_Perusahaan and "
                    SQL = SQL & "g.Kode_Perusahaan = h.Kode_Perusahaan and "
                    SQL = SQL & "a.No_DO = b.No_DO and "
                    SQL = SQL & "b.Kode_Stock_Owner = c.Kode_Stock_Owner and "
                    SQL = SQL & "b.Kode_Barang = c.Kode_Barang and "
                    SQL = SQL & "a.No_Faktur = d.no_faktur and d.Kode_Customer = e.Kode_Customer and "
                    SQL = SQL & "c.Kode_Kategori_Besar = f.Kode_Kategori_Besar and c.Kode_Kategori_Kecil = f.Kode_Kategori_Kecil and "
                    SQL = SQL & "f.Kode_Kategori_Besar = g.Kode_Kategori_Besar and g.Kode_Merk = h.Kode_Merk and "
                    SQL = SQL & "a.status is null and "
                    SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "a.No_DO = '" & nofakdo & "'"
                    SQL = SQL & ")"
                    SQL = SQL & "insert into do_new_cash_diskon(Kode_Perusahaan, No_DO, No_Faktur_Promo, Urut_DO, Kode_Barang, kode_merk, Kode_Kategori_Besar, Kode_Kategori_Kecil, memenuhi, Persen_Disc_cash, Lama_Hari_Disc_Cash, Flag_Eliminasi_Promo_Induk, No_Faktur_Untuk_Di_Eliminasi)"
                    SQL = SQL & "select Kode_Perusahaan, '" & nofakdo & "', '" & arr_kode_promo.Item(z) & "', Urut_oto, Kode_Barang, kode_merk, Kode_Kategori_Besar, Kode_Kategori_Kecil, memenuhi, Persen_Disc_cash, Jumlah_Hari, '" & Flag_Eleminasi_Promo_Induk & "', '" & No_Faktur_Untuk_Di_Eleminasi & "' from cte"
                    ExecuteTrans(SQL)


                Next

                SQL = "delete from do_new_cash_diskon where "
                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "No_DO = '" & nofakdo & "' and no_faktur_promo in("
                SQL = SQL & "select No_Faktur_Untuk_Di_Eliminasi from do_new_cash_diskon where "
                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "No_DO = '" & nofakdo & "' and Flag_Eliminasi_Promo_Induk = 'Y'"
                SQL = SQL & ")"
                ExecuteTrans(SQL)

                SQL = "delete from do_new_cash_diskon where "
                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "No_DO = '" & nofakdo & "' and "
                SQL = SQL & "(memenuhi = 'T' or persen_disc_cash = 0)"
                ExecuteTrans(SQL)

                SQL = ";with cte as("
                SQL = SQL & "select a.kode_perusahaan, a.No_DO, b.Tanggal, b.nppn, a.nharga, a.NPersen_Diskon, a.metode_perhitungan, a.Jml_Terima, "

                SQL = SQL & "isnull(( "
                SQL = SQL & "select sum(y.good_stock) from retur_do x, Detail_R_DO y where "
                SQL = SQL & "x.kode_perusahaan = y.kode_perusahaan and "
                SQL = SQL & "x.No_Retur_Jual = y.no_retur_jual and "
                SQL = SQL & "x.no_do = a.no_do and "
                SQL = SQL & "y.urut_do = a.urut_oto and "
                SQL = SQL & "x.kode_perusahaan = a.kode_perusahaan and "
                SQL = SQL & "x.status is null "
                SQL = SQL & "), 0) as retur_do, "

                SQL = SQL & "isnull(( "
                SQL = SQL & "select sum(persen_disc_cash) from DO_New_Cash_Diskon x where "
                SQL = SQL & "x.kode_perusahaan = a.kode_perusahaan and "
                SQL = SQL & "x.no_do = a.no_do And x.urut_do = a.Urut_Oto "
                SQL = SQL & "), 0) total_persen "

                SQL = SQL & "from detail_do_new a, do_new b where "
                SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and "
                SQL = SQL & "a.No_DO = b.no_do and "
                SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and a.no_do = '" & nofakdo & "' "
                SQL = SQL & "), "

                SQL = SQL & "cte_b as( "
                SQL = SQL & "select  *, (Jml_Terima - retur_do) as qty_bersih from cte "
                SQL = SQL & "), "

                SQL = SQL & "cte_c as( "
                SQL = SQL & "select *, ( "
                SQL = SQL & "case "
                SQL = SQL & "when Metode_Perhitungan = 'A' Then (nharga * qty_bersih) - (nharga * qty_bersih * NPersen_Diskon / 100) "
                SQL = SQL & "when Metode_Perhitungan = 'B' Then round((nharga - (nharga * NPersen_Diskon / 100)), 0) * qty_bersih "
                SQL = SQL & "Else -999999 "
                SQL = SQL & "end) dpp_bersih "

                SQL = SQL & "from cte_b "
                SQL = SQL & "), "

                SQL = SQL & "cte_d as( "
                SQL = SQL & "select dpp_bersih + round((dpp_bersih * NPPN /100), 0) as total_bersih_plus_ppn, * from cte_c "
                SQL = SQL & ") "
                SQL = SQL & "select isnull(sum(round(total_bersih_plus_ppn * total_persen / 100, 0)), 0) as total_rp_disc_cash from cte_d"

                Using dr = OpenTrans(SQL)
                    If dr.Read Then
                        SQL = "update do_new set Rp_Disc_Cash_Estimasi = " & dr("total_rp_disc_cash") & " where "
                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and no_do = '" & nofakdo & "'"

                        dr.Close()
                        ExecuteTrans(SQL)
                    End If
                End Using

            End If


            SQL = "delete Emi_DO_Pallet_Sementara where Kode_Perusahaan = '" & KodePerusahaan & "' and userid = '" & UserID & "' "
            ExecuteTrans(SQL)

            Dim batas As String = "asdasdas"



            Cmd.Transaction.Commit()
            CloseConn()

            If tampil_msg = 1 Then
                MessageBox.Show(msg_simpan_do, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If

        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        If kirim_fcm = 1 Then
            Dim client_web As WebClient = New WebClient
            Dim api_fcm_url = "https://opm.basecloud.app/api/"
            Dim subscribe As String = "stockopname"
            Try
                Dim noDo = ListView1.FocusedItem.Text

                Dim Request As HttpWebRequest
                Dim Response As HttpWebResponse
                Dim responseReader As StreamReader
                Dim result As String
                Dim custom_uid As String = ""
                Dim param_wa As String = ""
                param_wa = api_fcm_url & "send_to_fcm/to=" & subscribe
                param_wa = param_wa & "&title=Acc Auditor " & ListView1.FocusedItem.SubItems(4).Text & "&body=" & ListView1.FocusedItem.SubItems(8).Text & "-> " & Replace(noDo, "/", "-")
                Request = HttpWebRequest.Create(param_wa)
                Request.Method = "post"
                Request.ContentType = "Application/JSon"
                Request.ContentLength = 0
                Response = Request.GetResponse
                responseReader = New StreamReader(Response.GetResponseStream())
                result = responseReader.ReadToEnd()

            Catch ex As Exception

            End Try
        End If

        cetak(nofakdo, by_muat)
        Kosong()
    End Sub

    Private Sub CmbJnsDriver_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles CmbJnsDriver.KeyPress
        If e.KeyChar = Chr(13) Then
            If CmbJnsDriver.SelectedIndex = 0 Then 'sendiri
                CmbDriver.Focus()
            Else
                TxtDriver.Focus()
            End If
        End If
    End Sub

    Private Sub CmbJnsDriver_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbJnsDriver.SelectedIndexChanged
        If CmbJnsDriver.SelectedIndex = 0 Then 'sendiri
            CmbDriver.SelectedIndex = -1
            CmbDriver.Visible = True

            TxtDriver.Text = ""
            TxtDriver.Visible = False
        Else
            CmbDriver.SelectedIndex = -1
            CmbDriver.Visible = False

            TxtDriver.Text = ""
            TxtDriver.Visible = True
        End If
    End Sub

    Private Sub TxtMbl_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtMbl.TextChanged

    End Sub

    Private Sub DataGridView1_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellEndEdit
        Get_Isi_Listview(DataGridView1.CurrentRow.Index)
        'MessageBox.Show(DataGridView1.CurrentRow.Index)
        If IsNumeric(LvHrgMuat) = False Or Val(LvHrgMuat) < 0 Then
            DataGridView1.CurrentRow.Cells(CellHrgMuat).Value = 0
        ElseIf IsNumeric(LvJmlKrm) = False Or Val(LvJmlKrm) < 0 Then
            DataGridView1.CurrentRow.Cells(CellJmlKrm).Value = 0
        End If


        If Math.Ceiling(Val(LvHrgMuat)) <> Val(LvHrgMuat) Then
            DataGridView1.CurrentRow.Cells(CellHrgMuat).Value = 0
        ElseIf Math.Floor(Val(LvHrgMuat)) <> Val(LvHrgMuat) Then
            DataGridView1.CurrentRow.Cells(CellHrgMuat).Value = 0
        End If

        If Math.Ceiling(Val(LvJmlKrm)) <> Val(LvJmlKrm) Then
            DataGridView1.CurrentRow.Cells(CellJmlKrm).Value = 0
        ElseIf Math.Floor(Val(LvJmlKrm)) <> Val(LvJmlKrm) Then
            DataGridView1.CurrentRow.Cells(CellJmlKrm).Value = 0
        End If

        Get_Isi_Listview(DataGridView1.CurrentRow.Index)

        Dim sat_besar As Double = 0
        sat_besar = Math.Floor(Val(LvJmlKrm) / Val(LvIsiBsr))

        Dim sat_kecil As Double = 0
        sat_kecil = Val(LvJmlKrm) - (Math.Floor((Val(LvJmlKrm) / Val(LvIsiBsr)) * Val(LvIsiBsr)))

        Dim hrg_sat_besar As Double = Val(LvHrgMuat) * sat_besar
        Dim hrg_sat_kecil As Double = Val(HilangkanTanda(Format(Val(LvHrgMuat) / Val(LvIsiBsr), "N0"))) * sat_kecil

        DataGridView1.CurrentRow.Cells(CellTtlMuat).Value = hrg_sat_besar + hrg_sat_kecil
        ' total = (Val(HilangkanTanda(hrg.Text)) * Val(HilangkanTanda(jml.Text))) - (Val(HilangkanTanda(hrg.Text)) * Val(HilangkanTanda(jml.Text)) * Val(disc.Text) / 100)

        Dim y_hrg As Double = Val(HilangkanTanda(LvHrg))
        Dim y_disc As Double = Val(HilangkanTanda(Format(Val(LvDiscP), "N2")))
        Dim y_jml As Double = Val(HilangkanTanda(LvJmlKrm))

        Dim subttl As Double

        If LvMetPer = "A" Then
            subttl = (Val(HilangkanTanda(LvHrg)) * Val(HilangkanTanda(LvJmlKrm))) - (Val(HilangkanTanda(LvHrg)) * Val(HilangkanTanda(LvJmlKrm)) * Val(LvDiscP) / 100)
            DataGridView1.CurrentRow.Cells(CellSubttl).Value = Format(subttl, "N0")

        ElseIf LvMetPer = "B" Then
            subttl = Hitung_Subtotal(y_hrg, y_disc, y_jml)

        Else
            MessageBox.Show("error perhitungan")

        End If


        DataGridView1.CurrentRow.Cells(CellSubttl).Value = Format(subttl, "N0")

        Dim total As Double = 0
        Dim nilai_ppn As Double = 0
        Dim grandttl As Double = 0

        For i As Integer = 0 To DataGridView1.Rows.Count - 1
            Get_Isi_Listview(i)

            total = total + Val(HilangkanTanda(LvSubttl))
        Next
        TextBox17.Text = Format(total, "N0")
        nilai_ppn = total * Val(TextBox18.Text) / 100
        nilai_ppn = Val(HilangkanTanda(Format(nilai_ppn, "N0")))
        TextBox19.Text = Format(nilai_ppn, "N0")

        grandttl = total + nilai_ppn
        TxtTotal.Text = Format(grandttl, "N0")

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        If CheckBox1.Checked = False And CheckBox2.Checked = False And CheckBox3.Checked = False Then
            MessageBox.Show("Pilih terlebih dahulu parameter pencarian data!", Judul)
            CheckBox1.Focus() : Exit Sub
        End If

        If CheckBox1.Checked Then
            If ComboBox1.SelectedIndex = -1 Then
                MessageBox.Show("Parameter pencarian per tanggal harus diisi!", Judul)
                ComboBox1.Focus() : Exit Sub
            ElseIf DateTimePicker1.Value > DateTimePicker2.Value Then
                MessageBox.Show("Periode I tidak boleh lebih dari periode II!", Judul)
                DateTimePicker1.Value = tgl_skg : DateTimePicker2.Value = tgl_skg
                Exit Sub
            End If
        End If
        If CheckBox2.Checked Then
            If ComboBox2.SelectedIndex = -1 Then
                MessageBox.Show("Parameter lain harus diisi!", Judul)
                ComboBox2.Focus() : Exit Sub
            ElseIf TextBox1.Text.Trim.Length = 0 Then
                MessageBox.Show("Value parameter lain harus diisi!", Judul)
                TextBox1.Focus() : Exit Sub
            End If
        End If

        cari("T")
        Kosong()
    End Sub

    Private Sub CheckBox3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.Checked = True Then
            CheckBox1.Checked = False
            Button4_Click(CheckBox3, e)
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then
            ComboBox1.Enabled = True : DateTimePicker1.Enabled = True : DateTimePicker2.Enabled = True
            CheckBox3.Checked = False
        Else
            ComboBox1.Enabled = False : DateTimePicker1.Enabled = False : DateTimePicker2.Enabled = False
            ComboBox1.SelectedIndex = -1 : DateTimePicker1.Value = tgl_skg : DateTimePicker2.Value = tgl_skg
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked Then
            ComboBox2.Enabled = True : TextBox1.Enabled = True
        Else
            ComboBox2.Enabled = False : TextBox1.Enabled = False
            ComboBox2.SelectedIndex = -1 : TextBox1.Text = ""
        End If
    End Sub

    Private Sub ComboBox1_KeyPress1(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox1.KeyPress
        If e.KeyChar = Chr(13) Then DateTimePicker1.Focus()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged

    End Sub

    Private Sub DateTimePicker1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles DateTimePicker1.KeyPress
        If e.KeyChar = Chr(13) Then DateTimePicker2.Focus()
    End Sub

    Private Sub DateTimePicker2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles DateTimePicker2.KeyPress
        If e.KeyChar = Chr(13) Then Button4_Click(DateTimePicker2, e)
    End Sub

    Private Sub ComboBox2_KeyPress1(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox2.KeyPress
        If e.KeyChar = Chr(13) Then TextBox1.Focus()
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged

    End Sub

    Private Sub TextBox1_KeyPress1(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then Button4_Click(TextBox1, e)
    End Sub

    Private Sub CheckBox4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox4.CheckedChanged
        If CheckBox4.Checked = True Then
            ComboBox3.SelectedIndex = -1
            ComboBox3.Enabled = True
        Else
            ComboBox3.SelectedIndex = -1
            ComboBox3.Enabled = False
        End If
    End Sub

    Private Sub DataGridView1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles DataGridView1.KeyPress
        '  If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub GroupBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Cek_Sementara()
    End Sub

    Private Sub CmbHelper_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbHelper.SelectedIndexChanged

    End Sub

    Private Sub MengetahuiToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MengetahuiToolStripMenuItem.Click
        If ListView2.Items.Count = 0 Or ListView2.SelectedItems.Count = 0 Then
            MessageBox.Show("Pilih dahulu No Faktur!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
            'ElseIf ListView2.FocusedItem.SubItems(3).Text <> "Y" Or ListView2.FocusedItem.SubItems(4).Text <> "Y" Then
            '    MessageBox.Show("No Faktur Belum / Tidak selesai melakukan proses!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
        End If

        get_jam()

        Try
            OpenConn()

            Cmd.Transaction = Cn.BeginTransaction


            SQL = "select kode_perusahaan, flag_sudah_validasi_auditor, batal, flag_sudah_kirim from do_new_sementara where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "No_sementara = '" & ListView2.FocusedItem.SubItems(6).Text & "'"
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    'flag_sudah_validasi_auditor = 'Y' and batal is null and flag_sudah_kirim ='Y'
                    If General_Class.CekNULL(dr("flag_sudah_validasi_auditor")) <> "Y" Then
                        dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Proses tidak dapat dilanjutkan karena belum di validasi auditor!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    ElseIf General_Class.CekNULL(dr("batal")) <> "" Then
                        dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Proses tidak dapat dilanjutkan karena sudah dibatalkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    ElseIf General_Class.CekNULL(dr("flag_sudah_kirim")) = "" Then
                        dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Proses tidak dapat dilanjutkan karena belum di proses sistem!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                Else
                    dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            SQL = "update Do_new_sementara set flag_mengetahui = 'Y', user_mengetahui = '" & UserID & "', "
            SQL = SQL & "Tanggal_mengetahui = '" & Format(tgl_skg, "yyyy-MM-dd") & "', Jam_mengetahui = '" & Format(tgl_skg, "HH:mm:ss") & "' "
            SQL = SQL & "where No_sementara = '" & ListView2.FocusedItem.SubItems(6).Text & "' and flag_sudah_validasi_auditor = 'Y' and batal is null and flag_sudah_kirim is not null "
            ExecuteTrans(SQL)

            Cmd.Transaction.Commit()
            CloseConn()

            MessageBox.Show("Data Berhasil Di Update!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            ListView2.FocusedItem.Remove()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
        End Try


    End Sub

    Private Sub Button5_Click_1(sender As Object, e As EventArgs) Handles Button5.Click
        TabControl1.SelectedTab = TabPage2
    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        get_jam()

        Try
            OpenConn()

            Cmd.Transaction = Cn.BeginTransaction


            SQL = "select No_sementara from DO_New_Sementara a, penjualan b where a.No_Faktur = b.No_Faktur and Flag_Sudah_Kirim is not null and "
            SQL = SQL & "Flag_Mengetahui is null and a.tanggal + a.jam < '" & Format(DateAdd(DateInterval.Hour, -1, tgl_skg), "yyyy-MM-dd HH:mm:ss") & "' "

            If ComboBox6.SelectedIndex = 0 Then
                SQL = SQL & " and b.lokasi in("
                Dim list_kota As String = ""
                For x As Integer = 1 To ComboBox6.Items.Count - 1
                    list_kota = list_kota & "'" & ComboBox6.Items(x).ToString & "', "
                Next

                list_kota = Strings.Left(list_kota, Len(list_kota) - 2)

                SQL = SQL & list_kota & ")"
            Else
                SQL = SQL & " and b.lokasi = '" & ComboBox6.Text & "'"
            End If

            SQL = SQL & "order by a.tanggal + a.jam desc"
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    For i As Integer = 0 To .Rows.Count - 1

                        SQL = "update Do_new_sementara set flag_mengetahui = 'Y', user_mengetahui = '" & UserID & "', "
                        SQL = SQL & "Tanggal_mengetahui = '" & Format(tgl_skg, "yyyy-MM-dd") & "', Jam_mengetahui = '" & Format(tgl_skg, "HH:mm:ss") & "' "
                        SQL = SQL & "where No_sementara = '" & .Rows(i).Item("no_sementara") & "' and Flag_Sudah_Kirim is not null and "
                        SQL = SQL & "Flag_Mengetahui is null and tanggal + jam < '" & Format(DateAdd(DateInterval.Hour, -1, tgl_skg), "yyyy-MM-dd HH:mm:ss") & "' "
                        ExecuteTrans(SQL)
                    Next
                End With
            End Using



            Cmd.Transaction.Commit()
            CloseConn()

        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
        End Try
    End Sub



    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        If DataGridView1.Rows.Count = 0 Or DataGridView1.CurrentRow.Index = -1 Then Exit Sub

        If DataGridView1.CurrentCell.ColumnIndex = item_JmlhKirim Then

            SD_Pallet_DO.Txt_NoPenjualan.Text = DataGridView1.CurrentRow.Cells(item_NoPenjualan).Value
            SD_Pallet_DO.Txt_KdSO.Text = DataGridView1.CurrentRow.Cells(item_Gudang).Value
            SD_Pallet_DO.Txt_KdBarang.Text = DataGridView1.CurrentRow.Cells(item_KdBarang).Value
            SD_Pallet_DO.Txt_NmBarang.Text = DataGridView1.CurrentRow.Cells(item_Nama).Value

            SD_Pallet_DO.Txt_JmlhReq.Text = Format(Val(HilangkanTanda(DataGridView1.CurrentRow.Cells(item_JmlhOrder).Value)), "N2")
            SD_Pallet_DO.Txt_Sisa.Text = Format(Val(HilangkanTanda(DataGridView1.CurrentRow.Cells(item_Sisa).Value)), "N2")
            SD_Pallet_DO.kosong()
            SD_Pallet_DO.ShowDialog()

        End If


    End Sub

    Private Sub GantiVarianToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GantiVarianToolStripMenuItem.Click
        If ListView1.Items.Count = 0 Or ListView1.SelectedItems.Count = 0 Then
            MessageBox.Show("Pilih dahulu no faktur yang mau diganti varian!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If


        Try
            OpenConn()

            If CekButtonRole("ganti_varian") = "T" Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Anda tidak memiliki akses untuk memproses transaksi ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            CloseConn()

        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
        End Try

        DataGridView1.Rows.Clear()
        '''SEMENTARAHIDE Display_Ganti_Varian.ShowDialog()
    End Sub



    Private Sub CmbEkspedisi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CmbEkspedisi.KeyPress
        If e.KeyChar = Chr(13) Then TxtMbl.Focus()
    End Sub

    Private Sub CmbHelper_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CmbHelper.KeyPress
        If e.KeyChar = Chr(13) Then
            TextBox3.Focus()
        End If
    End Sub

    Private Sub CheckBox4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CheckBox4.KeyPress
        If e.KeyChar = Chr(13) Then
            ComboBox3.Focus()
        End If
    End Sub

    Public Sub GetJumlahKirim(ByVal Gudang As String, ByVal KdBarang As String)

        Dim indexDgv As Integer = -1



        For i As Integer = 0 To DataGridView1.Rows.Count - 1
            Dim gudangValue As String = DataGridView1.Rows(i).Cells(item_Gudang).Value.ToString().Trim().ToUpper()
            Dim kodebarangValue As String = DataGridView1.Rows(i).Cells(item_KdBarang).Value.ToString().Trim().ToUpper()

            If gudangValue = Gudang.ToUpper AndAlso kodebarangValue = KdBarang.ToUpper Then
                indexDgv = i
                Exit For
            End If
        Next

        If Not indexDgv = -1 Then

            Try
                OpenConn()

                Dim NoFaktur As String = DataGridView1.Rows(indexDgv).Cells(item_NoPenjualan).Value
                Dim KdSo As String = DataGridView1.Rows(indexDgv).Cells(item_Gudang).Value
                Dim KodeBarang As String = DataGridView1.Rows(indexDgv).Cells(item_KdBarang).Value

                '=============================
                '=     GET SUM SEMENTARA     =
                '=============================
                SQL = "select ISNULL(sum(Jumlah), 0) as Jumlah from Emi_DO_Pallet_Sementara "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_FakturPenjualan = '" & NoFaktur & "' "
                SQL = SQL & "and kd_so = '" & KdSo & "' and Kd_Barang = '" & KodeBarang & "' and userid = '" & UserID & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        DataGridView1.Rows(indexDgv).Cells(item_JmlhKirim).Value = HilangkanTanda(Dr("Jumlah"))
                    End If
                End Using



                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try

        Else
            DataGridView1.Rows(indexDgv).Cells(item_JmlhKirim).Value = 0
        End If


    End Sub


    Private Sub DO_Reseller_New_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing


        '=================================
        '=     HAPUS TABEL SEMENTARA     =
        '=================================
        Try
            OpenConn()

            SQL = "delete Emi_DO_Pallet_Sementara where Kode_Perusahaan = '" & KodePerusahaan & "' and userid = '" & UserID & "' "
            ExecuteTrans(SQL)

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try







    End Sub


End Class
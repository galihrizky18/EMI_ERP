
Public Class Display_Rencana_Order_Lain_Lain
    Dim Arr1, Arr2, Arr3, arrbatal As New ArrayList
    Dim Clr_Selesai As Color = Color.LightBlue
    Dim Clr_Batal As Color = Color.Black
    Dim Clr_Default As Color = Color.Blue
    Dim Clr_Gabungan As Color = Color.GreenYellow
    Public dari_mana As String
    Public filter_tambahan As String

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Display_Data_Pembelian_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LvRencanaOrder.Columns.Clear()
        LvRencanaOrder.Columns.Add("ID Rencana", 100, HorizontalAlignment.Left)
        LvRencanaOrder.Columns.Add("Lokasi", 120, HorizontalAlignment.Left)
        LvRencanaOrder.Columns.Add("Kode Supplier", 0, HorizontalAlignment.Left)
        LvRencanaOrder.Columns.Add("Nama Supplier", 200, HorizontalAlignment.Left)
        LvRencanaOrder.Columns.Add("No. PO", 240, HorizontalAlignment.Left)
        LvRencanaOrder.Columns.Add("Tgl PO", 100, HorizontalAlignment.Left)
        LvRencanaOrder.Columns.Add("UserID", 120, HorizontalAlignment.Left)
        LvRencanaOrder.Columns.Add("RV", 0, HorizontalAlignment.Left)
        LvRencanaOrder.Columns.Add("Kode Kontainer", 120, HorizontalAlignment.Left)
        LvRencanaOrder.Columns.Add("Total Persen", 100, HorizontalAlignment.Left)
        LvRencanaOrder.Columns.Add("No. PO Pembelian", 150, HorizontalAlignment.Left) 
        LvRencanaOrder.View = View.Details

        LvDetailRencanaOrder.Columns.Clear()
        LvDetailRencanaOrder.Columns.Add("Kode Barang", 250, HorizontalAlignment.Left)
        LvDetailRencanaOrder.Columns.Add("Nama Barang", 300, HorizontalAlignment.Left)
        LvDetailRencanaOrder.Columns.Add("Jumlah PO", 120, HorizontalAlignment.Right)
        LvDetailRencanaOrder.View = View.Details

        Try
            OpenConn()

            SQL = "SELECT ro.id_rencana, ro.lokasi, ro.kode_supplier, s.nama nama_supplier, "
            SQL = SQL & "ro.no_po, ro.tanggal_po, ro.userid, CAST(ro.rv AS BIGINT) rv, "
            SQL = SQL & "ro.kode_kontainer, ro.total_persen, ro.no_po_pembelian, "
            SQL = SQL & "ro.status, ro.selesai, ro.Flag_Gabungan "
            SQL = SQL & "FROM rencana_order ro, suppliers s "
            SQL = SQL & "WHERE ro.kode_perusahaan = s.kode_perusahaan AND "
            SQL = SQL & "ro.kode_supplier = s.kode_supplier AND "
            SQL = SQL & "ro.kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "ro.status is null and ro.selesai is null " & filter_tambahan
            SQL = SQL & "Order BY ro.lokasi, ro.tanggal_po Desc "
            LvRencanaOrder.Items.Clear() : LvDetailRencanaOrder.Items.Clear()
            Dim Lvw As ListViewItem
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    For i As Integer = 0 To .Rows.Count - 1
                        Lvw = LvRencanaOrder.Items.Add(.Rows(i).Item("id_rencana"))
                        Lvw.SubItems.Add(.Rows(i).Item("lokasi"))
                        Lvw.SubItems.Add("")
                        Lvw.SubItems.Add(.Rows(i).Item("nama_supplier"))
                        Lvw.SubItems.Add(.Rows(i).Item("no_po"))
                        Lvw.SubItems.Add(Format(.Rows(i).Item("tanggal_po"), "dd-MMM-yyyy"))
                        Lvw.SubItems.Add(.Rows(i).Item("userid"))
                        Lvw.SubItems.Add(.Rows(i).Item("rv"))
                        Lvw.SubItems.Add(.Rows(i).Item("kode_kontainer"))
                        Lvw.SubItems.Add(.Rows(i).Item("total_persen"))
                        Lvw.SubItems.Add(General_Class.CekNULL(.Rows(i).Item("no_po_pembelian")))

                        LvRencanaOrder.Items(i).ForeColor = Clr_Default

                        If General_Class.CekNULL(.Rows(i).Item("selesai")) = "Y" Then
                            LvRencanaOrder.Items(i).BackColor = Clr_Selesai
                        End If

                        If dari_mana = "TRANSAKSI_BIAYA" Or dari_mana = "VALIDASI_BIAYA" Or dari_mana = "HITUNG_HPP" Or dari_mana = "HITUNG_BILLING" Or dari_mana = "TRANSAKSI_BIAYA3" Or dari_mana = "PEMBELIAN" Then
                            If General_Class.CekNULL(.Rows(i).Item("Flag_Gabungan")) = "Y" Then
                                LvRencanaOrder.Items(i).BackColor = Clr_Gabungan
                            End If
                        End If
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

    Private Sub LvRencanaOrder_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles LvRencanaOrder.DoubleClick
        Try
            If LvRencanaOrder.Items.Count = 0 Then Exit Sub

            If dari_mana = "UBAH_STATUS_OTW" Then
                '    Ubah_Status_OTW.Kosong()
                'ElseIf dari_mana = "UBAH_KETERANGAN" Then
                '    Ubah_Status_Tracking_Dokumen.Kosong()
                'ElseIf dari_mana = "KAPAL_TIBA" Then
                '    Ubah_Keterangan_Bongkar.Kosong()
                'ElseIf dari_mana = "PENJALURAN" Then
                '    Ubah_Keterangan_Penjaluran.Kosong()
                'ElseIf dari_mana = "SPPB" Then
                '    Ubah_Keterangan_Sppb.Kosong()
                'ElseIf dari_mana = "BONGKAR" Then
                '    Ubah_Keterangan_Bongkar.Kosong()
            ElseIf dari_mana = "HITUNG_HPP" Then
                Hitung_HPP_Import.Kosong()
                'ElseIf dari_mana = "TRANSAKSI_BIAYA" Then
                '    Transaksi_Biaya_import.Kosong()
                'ElseIf dari_mana = "TARIK_KONTAINER" Then
                '    Ubah_Keterangan_Tarik_Kontainer.Kosong()
                'ElseIf dari_mana = "HITUNG_BILLING" Then
                '    Hitung_Total_Billing.Kosong()
                'ElseIf dari_mana = "TRANSAKSI_BIAYA3" Then
                '    Transaksi_Biaya_import3.Kosong()
                'ElseIf dari_mana = "LOKASI TUJUAN" Then
                '    Lokasi_Tujuan_Per_Container.Kosong()
                'ElseIf dari_mana = "PEMBELIAN" Then
                '    Pembelian_New3.BersihSeluruh()
                'ElseIf dari_mana = "VALIDASI_BIAYA" Then
                '    Biaya_Import_Per_PO.Kosong()
            End If


            OpenConn()

            SQL = "SELECT ro.id_rencana, ro.lokasi, ro.kode_supplier, s.nama nama_supplier, "
            SQL = SQL & "ro.no_po, ro.tanggal_po, ro.userid, CAST(ro.rv AS BIGINT) rv, "
            SQL = SQL & "ro.kode_kontainer, ro.total_persen, ro.no_po_pembelian, "
            SQL = SQL & "ro.status, ro.selesai, ro.Flag_Gabungan "
            SQL = SQL & "FROM rencana_order ro, suppliers s "
            SQL = SQL & "WHERE ro.kode_perusahaan = s.kode_perusahaan AND "
            SQL = SQL & "ro.kode_supplier = s.kode_supplier AND "
            SQL = SQL & "ro.kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "ro.status is null and ro.selesai is null and "
            SQL = SQL & "ro.id_rencana = '" & LvRencanaOrder.FocusedItem.Text & "' " & filter_tambahan
            SQL = SQL & "Order BY ro.lokasi, ro.tanggal_po Desc "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    For i As Integer = 0 To .Rows.Count - 1
                        Dim kontainer As Integer = (.Rows(i).Item("total_persen")) / 100
                        Dim jumlah As Integer = kontainer * 100
                        Dim selisih As Integer = (.Rows(i).Item("total_persen")) - jumlah

                        If dari_mana = "UBAH_STATUS_OTW" Then
                            'Ubah_Status_OTW.Kosong()
                            'Ubah_Status_OTW.TxtContainer.Text = (.Rows(i).Item("Kode_kontainer"))
                            'Ubah_Status_OTW.CmbLokasi.Text = .Rows(i).Item("lokasi")

                            'If selisih = 0 Or selisih <= 99 Then
                            '    'Ubah_Status_OTW.TxtJumlah_conte.Text = kontainer
                            'Else
                            '    Ubah_Status_OTW.TxtJumlah_conte.Text = kontainer + 1
                            'End If

                            'Ubah_Status_OTW.DtTanggal_Po.Value = (Format(.Rows(i).Item("tanggal_po"), "dd-MMM-yyyy"))
                            'Ubah_Status_OTW.TxtSupplier.Text = (.Rows(i).Item("nama_supplier"))
                            'Ubah_Status_OTW.TextBox1.Text = (.Rows(i).Item("kode_supplier"))
                            ''Ubah_Keterangan.TextBox2.Text = (.Rows(i).Item("Kode_Stock_Owner_import"))
                            'Ubah_Status_OTW.TxtId_Rencana.Text = (.Rows(i).Item("id_rencana"))
                            'Ubah_Status_OTW.TxtId_Rencana_Leave(LvRencanaOrder, e)

                        ElseIf dari_mana = "UBAH_KETERANGAN" Then
                            'TAMBAH SYNTAX BARU

                            'Ubah_Status_Tracking_Dokumen.Kosong()
                            'Ubah_Status_Tracking_Dokumen.TxtContainer.Text = (.Rows(i).Item("Kode_kontainer"))
                            'Ubah_Status_Tracking_Dokumen.CmbLokasi.Text = .Rows(i).Item("lokasi")

                            'If selisih = 0 Or selisih <= 99 Then
                            '    Ubah_Status_Tracking_Dokumen.TxtJumlah_conte.Text = kontainer
                            'Else
                            '    Ubah_Status_Tracking_Dokumen.TxtJumlah_conte.Text = kontainer + 1
                            'End If

                            'Ubah_Status_Tracking_Dokumen.DtTanggal_Po.Value = (Format(.Rows(i).Item("tanggal_po"), "dd-MMM-yyyy"))
                            'Ubah_Status_Tracking_Dokumen.TxtSupplier.Text = (.Rows(i).Item("nama_supplier"))
                            'Ubah_Status_Tracking_Dokumen.TextBox1.Text = (.Rows(i).Item("kode_supplier"))
                            ''Ubah_Keterangan.TextBox2.Text = (.Rows(i).Item("Kode_Stock_Owner_import"))
                            'Ubah_Status_Tracking_Dokumen.TxtId_Rencana.Text = (.Rows(i).Item("id_rencana"))
                            'Ubah_Status_Tracking_Dokumen.TxtId_Rencana_Leave(LvRencanaOrder, e)

                            'SQL = "SELECT BL,HC,Form_E,Flag_Final,Final_Keterangan,No_Resi,Scan_Telex,ETA_Dokumen,Penerima "
                            'SQL = SQL & "from tracking_dokumen where id_rencana = '" & LvRencanaOrder.FocusedItem.Text & "'"
                            'Using Dt = BindingTrans(SQL)
                            '    With Dt.Tables("MyTable")
                            '        For a As Integer = 0 To .Rows.Count - 1
                            '            Dim ValBL As String = General_Class.CekNULL((.Rows(0).Item("Bl")))
                            '            Dim ValHc As String = General_Class.CekNULL((.Rows(0).Item("HC")))
                            '            Dim ValForm_E As String = General_Class.CekNULL((.Rows(0).Item("Form_E")))
                            '            Dim ValFlag_Final As String = General_Class.CekNULL((.Rows(0).Item("Flag_Final")))
                            '            Dim ValFinal_Ket As String = General_Class.CekNULL((.Rows(0).Item("Final_Keterangan")))
                            '            Dim ValNo_Resi As String = General_Class.CekNULL((.Rows(0).Item("No_Resi")))
                            '            Dim ValScan_Telex As String = General_Class.CekNULL((.Rows(0).Item("Scan_Telex")))
                            '            Dim ValETA_Dokumen As String = General_Class.CekNULL((.Rows(0).Item("ETA_Dokumen")))

                            '            If ValBL = "Y" Then
                            '                Ubah_Status_Tracking_Dokumen.CheckBox1.Checked = True
                            '            Else
                            '                Ubah_Status_Tracking_Dokumen.CheckBox1.Checked = False
                            '            End If

                            '            If ValHc = "Y" Then
                            '                Ubah_Status_Tracking_Dokumen.CheckBox2.Checked = True
                            '            Else
                            '                Ubah_Status_Tracking_Dokumen.CheckBox2.Checked = False
                            '            End If

                            '            If ValForm_E = "Y" Then
                            '                Ubah_Status_Tracking_Dokumen.CheckBox3.Checked = True
                            '            Else
                            '                Ubah_Status_Tracking_Dokumen.CheckBox3.Checked = False
                            '            End If

                            '            If ValFlag_Final = "Y" Then
                            '                Ubah_Status_Tracking_Dokumen.CheckBox4.Checked = True
                            '                Ubah_Status_Tracking_Dokumen.TextBox4.Enabled = False
                            '                Ubah_Status_Tracking_Dokumen.TextBox4.Text = ""
                            '            Else
                            '                Ubah_Status_Tracking_Dokumen.CheckBox4.Checked = False
                            '                Ubah_Status_Tracking_Dokumen.TextBox4.Enabled = True
                            '                Ubah_Status_Tracking_Dokumen.TextBox4.Text = General_Class.CekNULL((.Rows(i).Item("Final_Keterangan")))
                            '            End If

                            '            Ubah_Status_Tracking_Dokumen.ComboBox2.Text = General_Class.CekNULL((.Rows(i).Item("Scan_Telex")))
                            '            If ValScan_Telex = "" Or ValScan_Telex = "Scan Telex" Then
                            '                Ubah_Status_Tracking_Dokumen.TextBox2.Enabled = False
                            '                Ubah_Status_Tracking_Dokumen.DateTimePicker1.Enabled = False
                            '            Else
                            '                Ubah_Status_Tracking_Dokumen.TextBox2.Text = ValNo_Resi
                            '                Ubah_Status_Tracking_Dokumen.DateTimePicker1.Text = ValETA_Dokumen
                            '            End If

                            '            Ubah_Status_Tracking_Dokumen.TextBox3.Text = General_Class.CekNULL((.Rows(i).Item("Penerima")))

                            '        Next
                            '    End With

                            'End Using
                        ElseIf dari_mana = "PENJALURAN" Then
                            'Ubah_Keterangan_Penjaluran.Kosong()
                            'Ubah_Keterangan_Penjaluran.TxtContainer.Text = (.Rows(i).Item("Kode_kontainer"))
                            'Ubah_Keterangan_Penjaluran.CmbLokasi.Text = .Rows(i).Item("lokasi")

                            'If selisih = 0 Or selisih <= 99 Then
                            '    Ubah_Keterangan_Penjaluran.TxtJumlah_conte.Text = kontainer
                            'Else
                            '    Ubah_Keterangan_Penjaluran.TxtJumlah_conte.Text = kontainer + 1
                            'End If

                            'Ubah_Keterangan_Penjaluran.DtTanggal_Po.Value = (Format(.Rows(i).Item("tanggal_po"), "dd-MMM-yyyy"))
                            'Ubah_Keterangan_Penjaluran.TxtSupplier.Text = (.Rows(i).Item("nama_supplier"))
                            'Ubah_Keterangan_Penjaluran.TextBox1.Text = (.Rows(i).Item("kode_supplier"))
                            'Ubah_Keterangan_Penjaluran.TxtId_Rencana.Text = (.Rows(i).Item("id_rencana"))
                            'Ubah_Keterangan_Penjaluran.TxtId_Rencana_Leave(LvRencanaOrder, e)

                            'CloseConn()
                            'OpenConn()
                            'SQL = "select count(id_rencana) as count from penjaluran_import where id_rencana ='" & Ubah_Keterangan_Penjaluran.TxtId_Rencana.Text & "' and kode_perusahaan ='" & KodePerusahaan & "'"
                            'Using Dr = OpenTrans(SQL)
                            '    If Dr.Read Then
                            '        If Dr("count") <> 0 Then
                            '            Dr.Close()
                            '            CloseConn()
                            '            OpenConn()
                            '            SQL = "SELECT warna,jml_kontainer,no_pib,jml_kontainer_Karantina "
                            '            SQL = SQL & "from penjaluran_import where id_rencana = '" & LvRencanaOrder.FocusedItem.Text & "'"
                            '            Using Dt = BindingTrans(SQL)
                            '                With Dt.Tables("MyTable")
                            '                    For a As Integer = 0 To .Rows.Count - 1
                            '                        Dim ValWarna As String = General_Class.CekNULL((.Rows(0).Item("warna")))
                            '                        Dim ValJml_Konte As String = General_Class.CekNULL((.Rows(0).Item("jml_kontainer")))
                            '                        Dim ValNo_PIB As String = General_Class.CekNULL((.Rows(0).Item("no_pib")))
                            '                        Dim ValJml_Konte_Ass As String = General_Class.CekNULL((.Rows(0).Item("jml_kontainer_Karantina")))

                            '                        Ubah_Keterangan_Penjaluran.ComboBox1.Text = ValWarna
                            '                        Ubah_Keterangan_Penjaluran.ComboBox2.Text = ValJml_Konte
                            '                        Ubah_Keterangan_Penjaluran.TextBox2.Text = ValNo_PIB

                            '                        If .Rows(0).Item("jml_kontainer_Karantina") = 0 Then
                            '                            Ubah_Keterangan_Penjaluran.CheckBox1.Checked = False
                            '                            Ubah_Keterangan_Penjaluran.ComboBox3.Enabled = False
                            '                        Else
                            '                            Ubah_Keterangan_Penjaluran.CheckBox1.Checked = True
                            '                            Ubah_Keterangan_Penjaluran.ComboBox3.Enabled = True
                            '                            Ubah_Keterangan_Penjaluran.ComboBox3.Text = ValJml_Konte_Ass
                            '                        End If

                            '                    Next
                            '                End With
                            '            End Using

                            '            Ubah_Keterangan_Penjaluran.BtnSimpan.Text = "&Update"
                            '        Else
                            '            Dr.Close()
                            '            Ubah_Keterangan_Penjaluran.BtnSimpan.Text = "&Simpan"
                            '        End If

                            '    Else
                            '        Dr.Close()

                            '    End If
                            'End Using

                        ElseIf dari_mana = "SPPB" Then
                            'Ubah_Keterangan_Sppb.Kosong()
                            'Ubah_Keterangan_Sppb.TxtContainer.Text = (.Rows(i).Item("Kode_kontainer"))
                            'Ubah_Keterangan_Sppb.CmbLokasi.Text = .Rows(i).Item("lokasi")

                            'If selisih = 0 Or selisih <= 99 Then
                            '    Ubah_Keterangan_Sppb.TxtJumlah_conte.Text = kontainer
                            'Else
                            '    Ubah_Keterangan_Sppb.TxtJumlah_conte.Text = kontainer + 1
                            'End If

                            'Ubah_Keterangan_Sppb.DtTanggal_Po.Value = (Format(.Rows(i).Item("tanggal_po"), "dd-MMM-yyyy"))
                            'Ubah_Keterangan_Sppb.TxtSupplier.Text = (.Rows(i).Item("nama_supplier"))
                            'Ubah_Keterangan_Sppb.TextBox1.Text = (.Rows(i).Item("kode_supplier"))
                            'Ubah_Keterangan_Sppb.TxtId_Rencana.Text = (.Rows(i).Item("id_rencana"))
                            'Ubah_Keterangan_Sppb.TxtId_Rencana_Leave(LvRencanaOrder, e)

                            'CloseConn()
                            'OpenConn()
                            'SQL = "select count(id_rencana) as count,ok,nhi,hico from sppb_import where id_rencana ='" & Ubah_Keterangan_Sppb.TxtId_Rencana.Text & "' and kode_perusahaan = '" & KodePerusahaan & "'  group by ID_Rencana,nhi,hico,ok "
                            'Using Dr = OpenTrans(SQL)
                            '    If Dr.Read Then
                            '        If Dr("count") <> 0 Then
                            '            'Dr.Close()
                            '            If General_Class.CekNULL(Dr("hico")) = "Y" Then
                            '                CloseConn()
                            '                OpenConn()


                            '                SQL = "SELECT kode_gudang,hico,tanggal_hico,biaya_hico "
                            '                SQL = SQL & "from sppb_import where kode_perusahaan = '" & KodePerusahaan & "' and id_rencana = '" & LvRencanaOrder.FocusedItem.Text & "'"
                            '                Using Dt = BindingTrans(SQL)
                            '                    With Dt.Tables("MyTable")
                            '                        For a As Integer = 0 To .Rows.Count - 1
                            '                            'Dim Val1 As String = General_Class.CekNULL((.Rows(0).Item("nhi")))
                            '                            Dim Val2 As String = General_Class.CekNULL((.Rows(0).Item("tanggal_hico")))
                            '                            Dim Val3 As String = General_Class.CekNULL((.Rows(0).Item("biaya_hico")))
                            '                            Dim Val4 As String = General_Class.CekNULL((.Rows(0).Item("kode_gudang")))

                            '                            Ubah_Keterangan_Sppb.ComboBox1.Text = "HICO"
                            '                            Ubah_Keterangan_Sppb.ComboBox2.Text = Val4
                            '                            Ubah_Keterangan_Sppb.TextBox2.Text = Val3
                            '                            Ubah_Keterangan_Sppb.DateTimePicker1.Value = Val2

                            '                        Next
                            '                    End With
                            '                End Using

                            '                Ubah_Keterangan_Sppb.BtnSimpan.Text = "&Update"

                            '            ElseIf General_Class.CekNULL(Dr("nhi")) = "Y" Then
                            '                CloseConn()
                            '                OpenConn()

                            '                SQL = "SELECT kode_gudang,nhi,tanggal_nhi,biaya_nhi "
                            '                SQL = SQL & "from sppb_import where kode_perusahaan = '" & KodePerusahaan & "' and id_rencana = '" & LvRencanaOrder.FocusedItem.Text & "'"
                            '                Using Dt = BindingTrans(SQL)
                            '                    With Dt.Tables("MyTable")
                            '                        For a As Integer = 0 To .Rows.Count - 1
                            '                            'Dim Val1 As String = General_Class.CekNULL((.Rows(0).Item("nhi")))
                            '                            Dim Val2 As String = General_Class.CekNULL((.Rows(0).Item("tanggal_nhi")))
                            '                            Dim Val3 As String = General_Class.CekNULL((.Rows(0).Item("biaya_nhi")))
                            '                            Dim Val4 As String = General_Class.CekNULL((.Rows(0).Item("kode_gudang")))

                            '                            Ubah_Keterangan_Sppb.ComboBox1.Text = "NHI"
                            '                            Ubah_Keterangan_Sppb.ComboBox2.Text = Val4
                            '                            Ubah_Keterangan_Sppb.TextBox2.Text = Val3
                            '                            Ubah_Keterangan_Sppb.DateTimePicker1.Value = Val2

                            '                        Next
                            '                    End With
                            '                End Using

                            '                Ubah_Keterangan_Sppb.BtnSimpan.Text = "&Update"
                            '            End If

                            '        Else
                            '            Dr.Close()
                            '            Ubah_Keterangan_Sppb.BtnSimpan.Text = "&Simpan"
                            '        End If

                            '    Else
                            '        Dr.Close()

                            '    End If
                            'End Using
                        ElseIf dari_mana = "KAPAL_TIBA" Then
                            'Ubah_Keterangan_Kapal_Tiba.Kosong()
                            'Ubah_Keterangan_Kapal_Tiba.TxtContainer.Text = (.Rows(i).Item("Kode_kontainer"))
                            'Ubah_Keterangan_Kapal_Tiba.CmbLokasi.Text = .Rows(i).Item("lokasi")

                            'If selisih = 0 Or selisih <= 99 Then
                            '    Ubah_Keterangan_Kapal_Tiba.TxtJumlah_conte.Text = kontainer
                            'Else
                            '    Ubah_Keterangan_Kapal_Tiba.TxtJumlah_conte.Text = kontainer + 1
                            'End If

                            'Ubah_Keterangan_Kapal_Tiba.DtTanggal_Po.Value = (Format(.Rows(i).Item("tanggal_po"), "dd-MMM-yyyy"))
                            'Ubah_Keterangan_Kapal_Tiba.TxtSupplier.Text = (.Rows(i).Item("nama_supplier"))
                            'Ubah_Keterangan_Kapal_Tiba.TextBox1.Text = (.Rows(i).Item("kode_supplier"))
                            'Ubah_Keterangan_Kapal_Tiba.TxtId_Rencana.Text = (.Rows(i).Item("id_rencana"))
                            'Ubah_Keterangan_Kapal_Tiba.TxtId_Rencana_Leave(LvRencanaOrder, e)

                            'CloseConn()
                            'OpenConn()
                            'SQL = "select count(ID_Rencana) as count, eta from Ubah_Status_OTW where id_rencana ='" & Ubah_Keterangan_Kapal_Tiba.TxtId_Rencana.Text & "' and kode_perusahaan ='" & KodePerusahaan & "' group by id_rencana,eta"
                            'Using Dr = OpenTrans(SQL)
                            '    If Dr.Read Then
                            '        If Dr("count") <> 0 Then
                            '            Dr.Close()
                            '            CloseConn()
                            '            OpenConn()

                            '            SQL = "SELECT eta "
                            '            SQL = SQL & "from ubah_status_otw where id_rencana = '" & LvRencanaOrder.FocusedItem.Text & "' and kode_perusahaan ='" & KodePerusahaan & "'"
                            '            Using Dt = BindingTrans(SQL)
                            '                With Dt.Tables("MyTable")
                            '                    For a As Integer = 0 To .Rows.Count - 1
                            '                        Dim ValETA As String = General_Class.CekNULL((.Rows(0).Item("eta")))

                            '                        Ubah_Keterangan_Kapal_Tiba.DateTimePicker1.Value = ValETA

                            '                        Ubah_Keterangan_Kapal_Tiba.BtnSimpan.Text = "&Simpan"
                            '                    Next
                            '                End With
                            '            End Using
                            '            'Dim ValETA_OTW As String = Dr("eta")

                            '            'Ubah_Keterangan_Bongkar.DateTimePicker1.Value = ValETA_OTW

                            '        End If

                            '    Else
                            '        Dr.Close()

                            '    End If
                            'End Using
                        ElseIf dari_mana = "BONGKAR" Then
                            'Ubah_Keterangan_Bongkar.Kosong()
                            'Ubah_Keterangan_Bongkar.TxtContainer.Text = (.Rows(i).Item("Kode_kontainer"))
                            'Ubah_Keterangan_Bongkar.CmbLokasi.Text = .Rows(i).Item("lokasi")

                            'If selisih = 0 Or selisih <= 99 Then
                            '    Ubah_Keterangan_Bongkar.TxtJumlah_conte.Text = kontainer
                            'Else
                            '    Ubah_Keterangan_Bongkar.TxtJumlah_conte.Text = kontainer + 1
                            'End If

                            'Ubah_Keterangan_Bongkar.DtTanggal_Po.Value = (Format(.Rows(i).Item("tanggal_po"), "dd-MMM-yyyy"))
                            'Ubah_Keterangan_Bongkar.TxtSupplier.Text = (.Rows(i).Item("nama_supplier"))
                            'Ubah_Keterangan_Bongkar.TextBox1.Text = (.Rows(i).Item("kode_supplier"))
                            'Ubah_Keterangan_Bongkar.TxtId_Rencana.Text = (.Rows(i).Item("id_rencana"))
                            'Ubah_Keterangan_Bongkar.TxtId_Rencana_Leave(LvRencanaOrder, e)

                            'CloseConn()
                            'OpenConn()
                            'SQL = "select count(ID_Rencana) as count, eta from Ubah_Status_OTW where id_rencana ='" & Ubah_Keterangan_Bongkar.TxtId_Rencana.Text & "' and kode_perusahaan ='" & KodePerusahaan & "' group by id_rencana,eta"
                            'Using Dr = OpenTrans(SQL)
                            '    If Dr.Read Then
                            '        If Dr("count") <> 0 Then
                            '            Dr.Close()
                            '            CloseConn()
                            '            OpenConn()

                            '            SQL = "SELECT eta "
                            '            SQL = SQL & "from ubah_status_otw where id_rencana = '" & LvRencanaOrder.FocusedItem.Text & "' and kode_perusahaan ='" & KodePerusahaan & "'"
                            '            Using Dt = BindingTrans(SQL)
                            '                With Dt.Tables("MyTable")
                            '                    For a As Integer = 0 To .Rows.Count - 1
                            '                        Dim ValETA As String = General_Class.CekNULL((.Rows(0).Item("eta")))

                            '                        Ubah_Keterangan_Bongkar.DateTimePicker1.Value = ValETA


                            '                    Next
                            '                End With
                            '            End Using


                            '            SQL = "SELECT Tanggal_Tiba "
                            '            SQL = SQL & "from kapal_tiba_import where id_rencana = '" & LvRencanaOrder.FocusedItem.Text & "' and kode_perusahaan ='" & KodePerusahaan & "'"
                            '            Using Dt = BindingTrans(SQL)
                            '                With Dt.Tables("MyTable")
                            '                    For a As Integer = 0 To .Rows.Count - 1
                            '                        Dim ValTanggal As String = General_Class.CekNULL((.Rows(0).Item("tanggal_tiba")))

                            '                        Ubah_Keterangan_Bongkar.DateTimePicker3.Value = ValTanggal

                            '                    Next
                            '                End With
                            '            End Using
                            '            'Dim ValETA_OTW As String = Dr("eta")

                            '            Ubah_Keterangan_Bongkar.BtnSimpan.Text = "&Simpan"
                            '            'Ubah_Keterangan_Bongkar.DateTimePicker1.Value = ValETA_OTW

                            '        End If

                            '    Else
                            '        Dr.Close()

                            '    End If
                            'End Using

                        ElseIf dari_mana = "HITUNG_HPP" Then
                            Hitung_HPP_Import.Kosong()
                            Hitung_HPP_Import.TxtContainer.Text = (.Rows(i).Item("Kode_kontainer"))

                            If selisih = 0 Or selisih <= 99 Then
                                Hitung_HPP_Import.TxtJumlah_conte.Text = kontainer
                            Else
                                Hitung_HPP_Import.TxtJumlah_conte.Text = kontainer + 1
                            End If

                            Hitung_HPP_Import.CmbLokasi.Text = .Rows(i).Item("lokasi")
                            Hitung_HPP_Import.DtTanggal_Po.Value = (Format(.Rows(i).Item("tanggal_po"), "dd-MMM-yyyy"))
                            Hitung_HPP_Import.TxtSupplier.Text = (.Rows(i).Item("nama_supplier"))
                            Hitung_HPP_Import.TextBox1.Text = (.Rows(i).Item("kode_supplier"))
                            'Ubah_Keterangan.TextBox2.Text = (.Rows(i).Item("Kode_Stock_Owner_import"))
                            Hitung_HPP_Import.TxtId_Rencana.Text = (.Rows(i).Item("id_rencana"))
                            Hitung_HPP_Import.TxtFlag_Gabungan.Text = General_Class.CekNULL((.Rows(0).Item("Flag_Gabungan")))
                            Hitung_HPP_Import.TxtId_Rencana_Leave(LvRencanaOrder, e)
                        ElseIf dari_mana = "TRANSAKSI_BIAYA" Then

                            'Transaksi_Biaya_import.CmbLokasi.Text = (.Rows(i).Item("lokasi"))
                            'Transaksi_Biaya_import.TxtContainer.Text = (.Rows(i).Item("Kode_kontainer"))
                            'Transaksi_Biaya_import.TxtId_Rencana.Text = (.Rows(i).Item("id_rencana"))

                            ''Transaksi_Biaya_import.TxtJumlah_conte.Text = kontainer
                            'If selisih = 0 Or selisih <= 99 Then
                            '    Transaksi_Biaya_import.TxtJumlah_conte.Text = kontainer
                            'Else
                            '    Transaksi_Biaya_import.TxtJumlah_conte.Text = kontainer + 1
                            'End If
                            'Transaksi_Biaya_import.TxtNo_PO.Text = (.Rows(i).Item("No_PO"))
                            'Transaksi_Biaya_import.DtTanggal_Po.Value = (Format(.Rows(i).Item("tanggal_po"), "dd-MMM-yyyy"))
                            'Transaksi_Biaya_import.TxtSupplier.Text = (.Rows(i).Item("nama_supplier"))
                            'Transaksi_Biaya_import.TxtFlag_Group.Text = General_Class.CekNULL((.Rows(0).Item("Flag_Gabungan")))

                            'If General_Class.CekNULL((.Rows(0).Item("Flag_Gabungan"))) = "Y" Then
                            '    Transaksi_Biaya_import.Tampil_Data_Group()
                            'Else
                            '    Transaksi_Biaya_import.Tampil_Data()
                            'End If

                        ElseIf dari_mana = "VALIDASI_BIAYA" Then

                            'Biaya_Import_Per_PO.CmbLokasi.Text = (.Rows(i).Item("lokasi"))
                            'Biaya_Import_Per_PO.TxtContainer.Text = (.Rows(i).Item("Kode_kontainer"))
                            'Biaya_Import_Per_PO.TxtId_Rencana.Text = (.Rows(i).Item("id_rencana"))

                            ''Transaksi_Biaya_import.TxtJumlah_conte.Text = kontainer
                            'If selisih = 0 Or selisih <= 99 Then
                            '    Biaya_Import_Per_PO.TxtJumlah_conte.Text = kontainer
                            'Else
                            '    Biaya_Import_Per_PO.TxtJumlah_conte.Text = kontainer + 1
                            'End If
                            'Biaya_Import_Per_PO.TxtNo_PO.Text = (.Rows(i).Item("No_PO"))
                            'Biaya_Import_Per_PO.DtTanggal_Po.Value = (Format(.Rows(i).Item("tanggal_po"), "dd-MMM-yyyy"))
                            'Biaya_Import_Per_PO.TxtSupplier.Text = (.Rows(i).Item("nama_supplier"))
                            'Biaya_Import_Per_PO.TxtFlag_Group.Text = General_Class.CekNULL((.Rows(0).Item("Flag_Gabungan")))

                            'If General_Class.CekNULL((.Rows(0).Item("Flag_Gabungan"))) = "Y" Then
                            '    Biaya_Import_Per_PO.Tampil_Data_Group()
                            'Else
                            '    Biaya_Import_Per_PO.Tampil_Data()
                            'End If

                        ElseIf dari_mana = "TARIK_KONTAINER" Then
                            'Ubah_Keterangan_Tarik_Kontainer.Kosong()
                            'Ubah_Keterangan_Tarik_Kontainer.TxtContainer.Text = (.Rows(i).Item("Kode_kontainer"))
                            'Ubah_Keterangan_Tarik_Kontainer.CmbLokasi.Text = .Rows(i).Item("lokasi")

                            'If selisih = 0 Or selisih <= 99 Then
                            '    Ubah_Keterangan_Tarik_Kontainer.TxtJumlah_conte.Text = kontainer
                            'Else
                            '    Ubah_Keterangan_Tarik_Kontainer.TxtJumlah_conte.Text = kontainer + 1
                            'End If

                            'Ubah_Keterangan_Tarik_Kontainer.DtTanggal_Po.Value = (Format(.Rows(i).Item("tanggal_po"), "dd-MMM-yyyy"))
                            'Ubah_Keterangan_Tarik_Kontainer.TxtSupplier.Text = (.Rows(i).Item("nama_supplier"))
                            'Ubah_Keterangan_Tarik_Kontainer.TextBox1.Text = (.Rows(i).Item("kode_supplier"))
                            'Ubah_Keterangan_Tarik_Kontainer.TxtId_Rencana.Text = (.Rows(i).Item("id_rencana"))
                            'Ubah_Keterangan_Tarik_Kontainer.TextBoxRV.Text = (.Rows(i).Item("rv"))
                            'Ubah_Keterangan_Tarik_Kontainer.TxtId_Rencana_Leave(LvRencanaOrder, e)
                        ElseIf dari_mana = "TRANSAKSI_BIAYA3" Then
                            'Transaksi_Biaya_import3.CmbLokasi.Text = (.Rows(i).Item("lokasi"))
                            'Transaksi_Biaya_import3.TxtContainer.Text = (.Rows(i).Item("Kode_kontainer"))
                            'Transaksi_Biaya_import3.TxtId_Rencana.Text = (.Rows(i).Item("id_rencana"))

                            ''Transaksi_Biaya_import.TxtJumlah_conte.Text = kontainer
                            'If selisih = 0 Or selisih <= 99 Then
                            '    Transaksi_Biaya_import3.TxtJumlah_conte.Text = kontainer
                            'Else
                            '    Transaksi_Biaya_import3.TxtJumlah_conte.Text = kontainer + 1
                            'End If
                            'Transaksi_Biaya_import3.TxtNo_PO.Text = (.Rows(i).Item("No_PO"))
                            'Transaksi_Biaya_import3.DtTanggal_Po.Value = (Format(.Rows(i).Item("tanggal_po"), "dd-MMM-yyyy"))
                            'Transaksi_Biaya_import3.TxtSupplier.Text = (.Rows(i).Item("nama_supplier"))
                            'Transaksi_Biaya_import3.Txt_Flag_Group.Text = General_Class.CekNULL((.Rows(0).Item("Flag_Gabungan")))
                            'Transaksi_Biaya_import3.Tampil_Data()
                        ElseIf dari_mana = "HITUNG_BILLING" Then
                            'Hitung_Total_Billing.Kosong()
                            'Hitung_Total_Billing.TxtContainer.Text = (.Rows(i).Item("Kode_kontainer"))
                            'Hitung_Total_Billing.CmbLokasi.Text = .Rows(i).Item("lokasi")

                            'If selisih = 0 Or selisih <= 99 Then
                            '    Hitung_Total_Billing.TxtJumlah_conte.Text = kontainer
                            'Else
                            '    Hitung_Total_Billing.TxtJumlah_conte.Text = kontainer + 1
                            'End If

                            'Hitung_Total_Billing.DtTanggal_Po.Value = (Format(.Rows(i).Item("tanggal_po"), "dd-MMM-yyyy"))
                            'Hitung_Total_Billing.TxtSupplier.Text = (.Rows(i).Item("nama_supplier"))
                            'Hitung_Total_Billing.TextBox1.Text = (.Rows(i).Item("kode_supplier"))
                            ''Hitung_Total_Billing.TextBox2.Text = (.Rows(i).Item("Kode_Stock_Owner_import"))
                            'Hitung_Total_Billing.TxtId_Rencana.Text = (.Rows(i).Item("id_rencana"))
                            'Hitung_Total_Billing.TxtFlag_group.Text = General_Class.CekNULL((.Rows(0).Item("Flag_Gabungan")))
                            'Hitung_Total_Billing.TxtId_Rencana_Leave(LvRencanaOrder, e)
                        ElseIf dari_mana = "LOKASI TUJUAN" Then
                            'Lokasi_Tujuan_Per_Container.Kosong()
                            'Lokasi_Tujuan_Per_Container.TxtContainer.Text = (.Rows(i).Item("Kode_kontainer"))
                            'Lokasi_Tujuan_Per_Container.CmbLokasi.Text = .Rows(i).Item("lokasi")

                            'If selisih = 0 Or selisih <= 99 Then
                            '    Lokasi_Tujuan_Per_Container.TxtJumlah_conte.Text = kontainer
                            'Else
                            '    Lokasi_Tujuan_Per_Container.TxtJumlah_conte.Text = kontainer + 1
                            'End If

                            'Lokasi_Tujuan_Per_Container.DtTanggal_Po.Value = (Format(.Rows(i).Item("tanggal_po"), "dd-MMM-yyyy"))
                            'Lokasi_Tujuan_Per_Container.TxtSupplier.Text = (.Rows(i).Item("nama_supplier"))
                            'Lokasi_Tujuan_Per_Container.TextBox1.Text = (.Rows(i).Item("kode_supplier"))
                            'Lokasi_Tujuan_Per_Container.TxtId_Rencana.Text = (.Rows(i).Item("id_rencana"))
                            'Lokasi_Tujuan_Per_Container.TextBoxRV.Text = (.Rows(i).Item("rv"))
                            'Lokasi_Tujuan_Per_Container.TxtId_Rencana_Leave(LvRencanaOrder, e)
                        ElseIf dari_mana = "PEMBELIAN" Then

                            'Pembelian_New3.TextBox11.Text = (.Rows(i).Item("nama_supplier"))
                            'Pembelian_New3.TextBox10.Text = (.Rows(i).Item("kode_supplier"))

                            'Pembelian_New3.txtidrencana.Text = (.Rows(i).Item("id_rencana"))
                            'Pembelian_New3.ComboBox4.SelectedIndex = -1
                            'Pembelian_New3.ComboBox4.Text = .Rows(i).Item("lokasi")

                            'Pembelian_New3.cari_rencana()
                        ElseIf dari_mana = "RENCANA_ORDER_GABUNGAN" Then
                            'Dim Lvw As ListViewItem
                            'Lvw = Rencana_Order_Gabungan.LvRencanaOrder1.Items.Add(.Rows(i).Item("id_rencana"))
                            'Lvw.SubItems.Add(.Rows(i).Item("lokasi"))
                            'Lvw.SubItems.Add("")
                            'Lvw.SubItems.Add(.Rows(i).Item("nama_supplier"))
                            'Lvw.SubItems.Add(.Rows(i).Item("no_po"))
                            'Lvw.SubItems.Add(Format(.Rows(i).Item("tanggal_po"), "dd-MMM-yyyy"))
                            'Lvw.SubItems.Add(.Rows(i).Item("userid"))
                            'Lvw.SubItems.Add(.Rows(i).Item("rv"))
                            'Lvw.SubItems.Add(.Rows(i).Item("kode_kontainer"))
                            'Lvw.SubItems.Add(.Rows(i).Item("total_persen"))
                            'Lvw.SubItems.Add(General_Class.CekNULL(.Rows(i).Item("no_po_pembelian")))
                        End If


                    Next
                End With
            End Using

            Me.Close()

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub ListView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles LvRencanaOrder.SelectedIndexChanged
        Try
            If LvRencanaOrder.Items.Count = 0 Then Exit Sub

            OpenConn()

            LvDetailRencanaOrder.Items.Clear()
            SQL = "SELECT dro.kode_barang, b.nama as nama_barang, dro.jumlah_po / dro.isi_satuan_besar jumlah_po "
            SQL = SQL & "FROM detail_rencana_order dro, barang b WHERE "
            SQL = SQL & "dro.kode_perusahaan = b.kode_Perusahaan AND dro.kode_stock_owner = b.kode_stock_owner AND dro.kode_barang = b.kode_barang AND "
            SQL = SQL & "dro.kode_perusahaan = '" & KodePerusahaan & "' AND dro.id_rencana = '" & LvRencanaOrder.FocusedItem.Text & "' and dro.jumlah_po <> 0 "
            SQL = SQL & "ORDER BY b.nama"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lvw As ListViewItem
                    Lvw = LvDetailRencanaOrder.Items.Add(Dr("kode_barang"))
                    Lvw.SubItems.Add(Dr("nama_barang"))
                    Lvw.SubItems.Add(Format(Dr("jumlah_po"), "N0"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

End Class
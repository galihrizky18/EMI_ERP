
Imports System.Xml

Public Class Import_Product_XML
    Dim No_Faktur As String
    Dim No_Faktur_Binding As String
    Dim arrcariProductXML As New ArrayList

    Private Sub Import_Product_XML_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        kosongProductXML()
        ComboBox1.Items.Clear()
        ComboBox1.Items.Add("50")
        ComboBox1.Items.Add("100")
        ComboBox1.Items.Add("200")
        ComboBox1.Items.Add("SELURUH")
        ComboBox1.Text = "SELURUH"
    End Sub

    Private Sub Import_Product_XML_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SizeChanged
        Label1.Size = New Point(592, 33)
    End Sub

    Private Sub get_no_faktur()
        No_Faktur = fTransFormula & Format(tgl_skg, "MMyy") & "-" &
                             General_Class.Get_Last_Number2("Emi_Transaksi_Formulator", "no_Faktur", 5,
                             "Kode_perusahaan", KodePerusahaan,
                             "And", "substring(no_Faktur, 1, " & Len(fTransFormula) + 4 & ")", fTransFormula & Format(tgl_skg, "MMyy"))
    End Sub

    Private Sub get_no_faktur_binding()
        No_Faktur_Binding = fTransFormulaBinding & Format(tgl_skg, "MMyy") & "-" &
                             General_Class.Get_Last_Number2("Emi_Transaksi_Formulator_Binding", "No_Faktur", 5,
                             "Kode_perusahaan", KodePerusahaan,
                             "And", "substring(no_Faktur, 1, " & Len(fTransFormulaBinding) + 4 & ")", fTransFormulaBinding & Format(tgl_skg, "MMyy"))
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        OpenFileDialog1.Filter = "XML | *.xml"
        OpenFileDialog1.FileName = ""
        OpenFileDialog1.DefaultExt = "XML"
        OpenFileDialog1.ShowDialog()
        TextBox1.Text = OpenFileDialog1.FileName
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim tanya As String = MessageBox.Show("Yakin akan mengupload data di file product xml ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If tanya = vbNo Then Exit Sub

        GetTime()
        get_jam()
        Try
            OpenConn()
            get_no_faktur()
            get_no_faktur_binding()


            Cmd.Transaction = Cn.BeginTransaction

            Dim xmlDoc As New XmlDocument()
            xmlDoc.Load(TextBox1.Text.Trim)
            Dim nodes As XmlNodeList = xmlDoc.DocumentElement.SelectNodes("/Transaction/TransactionHeader")

            SQL = "Delete From TmpProductXML_TransactionHeader "
            ExecuteTrans(SQL)

            SQL = "Delete From TmpProductXML_ProductHeader "
            ExecuteTrans(SQL)

            SQL = "Delete From TmpProductXML_Language "
            ExecuteTrans(SQL)

            SQL = "Delete From TmpProductXML_Material "
            ExecuteTrans(SQL)

            For Each node As XmlNode In nodes
                With node
                    SQL = "Insert Into TmpProductXML_TransactionHeader("
                    SQL = SQL & "MessageType, MessageVersion, MessageDate, MessageCreatedBy, ApplicationName, "
                    SQL = SQL & "DestinationName, DataLanguage, DatabaseName) "
                    SQL = SQL & "Values('" & .SelectSingleNode("MessageType").InnerText & "', "
                    SQL = SQL & "'" & .SelectSingleNode("MessageVersion").InnerText & "', "
                    SQL = SQL & "'" & .SelectSingleNode("MessageDate").InnerText & "', "
                    SQL = SQL & "'" & .SelectSingleNode("MessageCreatedBy").InnerText & "', "
                    SQL = SQL & "'" & .SelectSingleNode("ApplicationName").InnerText & "', "
                    SQL = SQL & "'" & .SelectSingleNode("DestinationName").InnerText & "', "
                    SQL = SQL & "'" & .SelectSingleNode("DataLanguage").InnerText & "', "
                    SQL = SQL & "'" & .SelectSingleNode("DatabaseName").InnerText & "') "
                    ExecuteTrans(SQL)
                End With
            Next

            Dim xmlDoc2 As New XmlDocument()
            xmlDoc2.Load(TextBox1.Text.Trim)
            Dim nodes2 As XmlNodeList = xmlDoc2.DocumentElement.SelectNodes("/Transaction/ProductCommunications/ProductCommunication/ProductHeader")

            Dim Kode_Barang As String
            Dim Satuan_Hasil As String
            Dim keberapa As Integer = 0

            Dim Kode_Stock_Owner As String
            Dim Lokasi As String

            Kode_Stock_Owner = ""
            SQL = "select Top 1 Kode_Stock_Owner from stock_owner_gudang where kode_perusahaan = '" & KodePerusahaan & "' "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Kode_Stock_Owner = dr("Kode_Stock_Owner")
                Loop
            End Using

            Lokasi = ""
            SQL = "select Top 1 Kode_Stock_Owner from stock_owner where kode_perusahaan = '" & KodePerusahaan & "' "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Lokasi = dr("Kode_Stock_Owner")
                Loop
            End Using

            Satuan_Hasil = ""
            For Each node As XmlNode In nodes2
                With node
                    Kode_Barang = .SelectSingleNode("ProductCode").InnerText
                    Satuan_Hasil = .SelectSingleNode("WeightUnitCode").InnerText
                    SQL = "SELECT Kode_Barang FROM Barang WHERE Kode_Perusahaan = '" & KodePerusahaan & "' AND kode_barang_inq = '" & .SelectSingleNode("ProductCode").InnerText & "' "
                    Using Ds As DataSet = BindingTrans(SQL)
                        If Ds.Tables("MyTable").Rows.Count = 0 Then

                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Kode Barang " & .SelectSingleNode("ProductCode").InnerText & " Tidak Ada !!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                            'Else
                            '    Kode_Barang = Ds.Tables("MyTable").Rows(0).Item("kode_barang")
                        End If
                    End Using

                    If .SelectSingleNode("WeightUnitCode").InnerText.ToString.ToUpper <> "KG" Then
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Satuan Hasil Harus KG !!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                        Exit Sub
                    End If

                    SQL = "Insert Into TmpProductXML_ProductHeader("
                    SQL = SQL & "ProductType, Code, CompoundCode, ProductCode, Description, "
                    SQL = SQL & "DescriptionLong, PlantCode, ProductVersion, ProductNumber, CentralArticleCode, "
                    SQL = SQL & "AnimalTypeCode, ValidFrom, TotalWeight, WeightUnitCode, Status, "
                    SQL = SQL & "InclusionRate, InclusionRateOnDM, OptimisationBase, Archive, "
                    'SQL = SQL & "ProductFormCode, "
                    SQL = SQL & "Active, ProductionCost, PricelistCode, ProductPrice, PriceUnitCode"
                    SQL = SQL & ") "
                    SQL = SQL & "Values('" & .SelectSingleNode("ProductType").InnerText & "', "
                    SQL = SQL & "'" & .SelectSingleNode("Code").InnerText & "', "
                    SQL = SQL & "'" & .SelectSingleNode("CompoundCode").InnerText & "', "
                    SQL = SQL & "'" & .SelectSingleNode("ProductCode").InnerText & "', "
                    SQL = SQL & "'" & .SelectSingleNode("Description").InnerText & "', "
                    SQL = SQL & "'" & .SelectSingleNode("DescriptionLong").InnerText & "', "
                    SQL = SQL & "'" & .SelectSingleNode("PlantCode").InnerText & "', "
                    SQL = SQL & "'" & .SelectSingleNode("ProductVersion").InnerText & "',"
                    SQL = SQL & "'" & .SelectSingleNode("ProductNumber").InnerText & "',"
                    SQL = SQL & "'" & .SelectSingleNode("CentralArticleCode").InnerText & "',"
                    SQL = SQL & "'" & .SelectSingleNode("AnimalTypeCode").InnerText & "',"
                    SQL = SQL & "'" & .SelectSingleNode("ValidFrom").InnerText & "',"
                    SQL = SQL & "'" & .SelectSingleNode("TotalWeight").InnerText & "',"
                    SQL = SQL & "'" & .SelectSingleNode("WeightUnitCode").InnerText & "',"
                    SQL = SQL & "'" & .SelectSingleNode("Status").InnerText & "',"
                    SQL = SQL & "'" & .SelectSingleNode("InclusionRate").InnerText & "',"
                    SQL = SQL & "'" & .SelectSingleNode("InclusionRateOnDM").InnerText & "',"
                    SQL = SQL & "'" & .SelectSingleNode("OptimisationBase").InnerText & "',"
                    SQL = SQL & "'" & .SelectSingleNode("Archive").InnerText & "',"
                    '     SQL = SQL & "'" & .SelectSingleNode("ProductFormCode").InnerText & "',"
                    SQL = SQL & "'" & .SelectSingleNode("Active").InnerText & "',"
                    SQL = SQL & "'" & .SelectSingleNode("ProductionCost").InnerText & "',"
                    SQL = SQL & "'" & .SelectSingleNode("PricelistCode").InnerText & "',"
                    SQL = SQL & "'" & .SelectSingleNode("ProductPrice").InnerText & "',"
                    SQL = SQL & "'" & .SelectSingleNode("PriceUnitCode").InnerText & "'"
                    SQL = SQL & ") "
                    ExecuteTrans(SQL)

                    SQL = "Insert Into Emi_Transaksi_Formulator(Kode_Perusahaan,No_Faktur,Tanggal,Jam,UserID,Kode_Stock_Owner,Kode_Barang,Hasil,Satuan_Hasil,Lokasi,Penanggung_Jawab) "
                    SQL = SQL & "Values('" & KodePerusahaan & "','" & No_Faktur & "','" & Format(Tanggal_Sekarang, "yyyy-MM-dd") & "','" & Format(tgl_skg, "HH:mm:ss") & "',"
                    SQL = SQL & "'" & UserID & "','" & Kode_Stock_Owner & "','" & .SelectSingleNode("ProductCode").InnerText & "'," & .SelectSingleNode("TotalWeight").InnerText & ", "
                    SQL = SQL & "'" & .SelectSingleNode("WeightUnitCode").InnerText.ToUpper & "','" & Lokasi & "','20') "
                    ExecuteTrans(SQL)

                    SQL = "Insert Into Emi_Log_Product_XML(Kode_Barang,filename,userid,tanggal,jam,No_Faktur) "
                    SQL = SQL & "Values('" & Kode_Barang & "','" & TextBox1.Text.Trim & "','" & UserID & "','" & Format(Tanggal_Sekarang, "yyyy-MM-dd") & "','" & Format(tgl_skg, "HH:mm:ss") & "','" & No_Faktur & "') "
                    ExecuteTrans(SQL)

                    '===============================
                    '=     SET BINDING FORMULA     =
                    '===============================
                    ''Binding 
                    If keberapa <> 0 Then
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Terjadi kesalahan harap ulangi proses!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If

                    SQL = "update EMI_Transaksi_Formulator_Binding set aktif = 'T' "
                    SQL = SQL & "where Kode_Barang = '" & .SelectSingleNode("ProductCode").InnerText & "' and Kode_Perusahaan = '" & KodePerusahaan & "' and aktif='Y' "
                    ExecuteTrans(SQL)

                    SQL = "Insert into EMI_Transaksi_Formulator_Binding ("
                    SQL = SQL & "Kode_Perusahaan, No_faktur, "
                    SQL = SQL & "Kode_Customer, No_Inquiry, "
                    SQL = SQL & "Tanggal, Jam, UserID, Kode_barang, Kode_formula, Aktif) "
                    SQL = SQL & "Values('" & KodePerusahaan & "', '" & No_Faktur_Binding & "', "
                    SQL = SQL & "NULL, NULL, "
                    SQL = SQL & "'" & Format(Tanggal_Sekarang, "yyyy-MM-dd") & "', '" & Format(CDate(tgl_skg), "HH:mm:ss") & "', "
                    SQL = SQL & "'" & UserID & "','" & .SelectSingleNode("ProductCode").InnerText & "', '" & No_Faktur & "','Y')"
                    ExecuteTrans(SQL)

                    keberapa = keberapa + 1
                End With
            Next

            Dim xmlDoc3 As New XmlDocument()
            xmlDoc3.Load(TextBox1.Text.Trim)
            Dim nodes3 As XmlNodeList = xmlDoc3.DocumentElement.SelectNodes("/Transaction/ProductCommunications/ProductCommunication/Descriptions/Language")

            For Each node As XmlNode In nodes3
                With node
                    SQL = "Insert Into TmpProductXML_Language("
                    SQL = SQL & "LanguageCode, Description, DescriptionLong) "
                    SQL = SQL & "Values('" & .SelectSingleNode("LanguageCode").InnerText & "', "
                    SQL = SQL & "'" & .SelectSingleNode("Description").InnerText & "', "
                    SQL = SQL & "'" & .SelectSingleNode("DescriptionLong").InnerText & "') "
                    ExecuteTrans(SQL)
                End With
            Next

            Dim xmlDoc5 As New XmlDocument()
            xmlDoc5.Load(TextBox1.Text.Trim)
            Dim nodes5 As XmlNodeList = xmlDoc5.DocumentElement.SelectNodes("/Transaction/ProductCommunications/ProductCommunication/BillOfMaterials/Material")

            For Each node As XmlNode In nodes5
                With node
                    SQL = "SELECT Kode_Barang FROM Barang WHERE Kode_Perusahaan = '" & KodePerusahaan & "' AND Kode_Barang = '" & .SelectSingleNode("MaterialCode").InnerText & "' "
                    Using Ds As DataSet = BindingTrans(SQL)
                        If Ds.Tables("MyTable").Rows.Count = 0 Then
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Kode Barang " & .SelectSingleNode("MaterialCode").InnerText & " Tidak Ada !!!")
                            Exit Sub
                        End If
                    End Using

                    SQL = "SELECT Satuan FROM Barang WHERE Kode_Perusahaan = '" & KodePerusahaan & "' AND Kode_Barang = '" & .SelectSingleNode("MaterialCode").InnerText & "' And Satuan = '" & .SelectSingleNode("WeightUnitCode").InnerText & "' "
                    Using Ds As DataSet = BindingTrans(SQL)
                        If Ds.Tables("MyTable").Rows.Count = 0 Then
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Satuan " & .SelectSingleNode("WeightUnitCode").InnerText & " Untuk Barang " & .SelectSingleNode("MaterialCode").InnerText & " Tidak Ada !!!")
                            Exit Sub
                        End If
                    End Using

                    SQL = "Insert Into TmpProductXML_Material("
                    SQL = SQL & "MaterialOrder, MaterialType, MaterialCode, MaterialDescription, CentralArticleCode, "
                    SQL = SQL & "Percentage, Weight, WeightUnitCode, IngoingAmount, IngId) "
                    SQL = SQL & "Values('" & .SelectSingleNode("MaterialOrder").InnerText & "', "
                    SQL = SQL & "'" & .SelectSingleNode("MaterialType").InnerText & "', "
                    SQL = SQL & "'" & .SelectSingleNode("MaterialCode").InnerText & "', "
                    SQL = SQL & "'" & .SelectSingleNode("MaterialDescription").InnerText & "', "
                    SQL = SQL & "'" & .SelectSingleNode("CentralArticleCode").InnerText & "', "
                    SQL = SQL & "'" & .SelectSingleNode("Percentage").InnerText & "', "
                    SQL = SQL & "'" & .SelectSingleNode("Weight").InnerText & "', "
                    SQL = SQL & "'" & .SelectSingleNode("WeightUnitCode").InnerText & "', "
                    SQL = SQL & "'" & .SelectSingleNode("IngoingAmount").InnerText & "', "
                    SQL = SQL & "'" & .SelectSingleNode("IngId").InnerText & "') "
                    ExecuteTrans(SQL)

                    Dim Kode_Bahan As String
                    Dim Qty_Bahan As String
                    Dim Satuan_Bahan As String
                    Dim Pengali As Decimal
                    Dim Nilai_Bahan As Decimal

                    Kode_Bahan = .SelectSingleNode("MaterialCode").InnerText.ToUpper
                    Qty_Bahan = .SelectSingleNode("Weight").InnerText
                    Satuan_Bahan = .SelectSingleNode("WeightUnitCode").InnerText.ToUpper

                    Pengali = 0
                    SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & Kode_Bahan & "',"
                    SQL = SQL & "'" & Satuan_Bahan & "','" & Satuan_Hasil & "',"
                    SQL = SQL & "'1') as Hasil "
                    Using dr = OpenTrans(SQL)
                        If dr.Read Then
                            If General_Class.CekNULL(dr("Hasil")) <> "" Then
                                If dr("Hasil") = 0 Then
                                    CloseConn()
                                    MessageBox.Show("Satuan " & Satuan_Bahan & " Ke " & Satuan_Hasil & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                Else
                                    Pengali = dr("hasil")
                                End If
                            Else
                                CloseConn()
                                MessageBox.Show("Satuan " & Satuan_Bahan & " Ke " & Satuan_Hasil & " Tidak ditemukan . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        End If
                    End Using

                    Nilai_Bahan = Val(Str(Qty_Bahan)) * Pengali
                    SQL = "Insert Into Emi_Transaksi_Formulator_Detail_Bahan(Kode_Perusahaan,No_Faktur,Kode_Stock_Owner,Kode_Barang,Persentase,Jumlah,Satuan,Nilai_Pengali,Satuan_Barang,Nilai_Barang) "
                    SQL = SQL & "Values('" & KodePerusahaan & "','" & No_Faktur & "','" & Kode_Stock_Owner & "','" & .SelectSingleNode("MaterialCode").InnerText & "'," & .SelectSingleNode("Percentage").InnerText & ", "
                    SQL = SQL & "" & Format(Qty_Bahan, "N4") & ",'" & Satuan_Bahan & "'," & Pengali & ",'" & .SelectSingleNode("WeightUnitCode").InnerText.ToUpper & "', "
                    SQL = SQL & "" & Nilai_Bahan & ") "
                    ExecuteTrans(SQL)
                End With
            Next

            Cmd.Transaction.Commit()

            CloseConn()

            MessageBox.Show("Data berhasil di upload!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            Call kosongProductXML()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub kosongProductXML()
        TextBox1.Text = ""
        GetTime()
        ComboBox1.Items.Clear()
        CmbProductXML_Kolom.Items.Clear() : arrcariProductXML.Clear()
        CmbProductXML_Kolom.Items.Add("Tanggal") : arrcariProductXML.Add("Tanggal")
        CmbProductXML_Kolom.Items.Add("Jam") : arrcariProductXML.Add("Jam")
        CmbProductXML_Kolom.Items.Add("Kode Barang") : arrcariProductXML.Add("Kode_Barang")
        CmbProductXML_Kolom.Items.Add("Filename") : arrcariProductXML.Add("Filename")
        CmbProductXML_Kolom.Items.Add("UserID") : arrcariProductXML.Add("UserID")
        CmbProductXML_Kolom.Items.Add("No Faktur") : arrcariProductXML.Add("No_Faktur")
        TxtProductXML_Value.Text = ""

        Try
            OpenConn()

            Dim Tgl_Dari As Date
            Dim Tgl_Sampai As Date
            Dim NoFaktur As String

            Tgl_Dari = Tanggal_Sekarang.AddDays(-7)
            Tgl_Sampai = Tanggal_Sekarang

            LvwProductXML_Data.Items.Clear()
            SQL = "Select Kode_Barang, Filename, UserID, Tanggal, Jam, No_Faktur "
            SQL = SQL & "From Emi_Log_Product_XML "
            SQL = SQL & "Where Tanggal >= '" & Format(Tgl_Dari, "yyyy-MM-dd") & "' And Tanggal <= '" & Format(Tgl_Sampai, "yyyy-MM-dd") & "' "
            SQL = SQL & "order by Tanggal desc, jam desc "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim Lvw As ListViewItem
                    Lvw = LvwProductXML_Data.Items.Add(Format(dr("Tanggal"), "dd-MMM-yyyy"))
                    Lvw.SubItems.Add(dr("Jam"))
                    Lvw.SubItems.Add(dr("Kode_Barang"))
                    Lvw.SubItems.Add(dr("Filename"))
                    Lvw.SubItems.Add(dr("UserID"))
                    If IsDBNull(dr("No_Faktur")) Then
                        NoFaktur = ""
                    Else
                        NoFaktur = dr("No_Faktur")
                    End If
                    Lvw.SubItems.Add(No_Faktur)
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub BtnProductXML_Cari_Click(sender As Object, e As EventArgs) Handles BtnProductXML_Cari.Click
        If CmbProductXML_Kolom.Text.Trim.Length = 0 Then Exit Sub
        If TxtProductXML_Value.Text.Trim.Length = 0 Then Exit Sub

        If CmbProductXML_Kolom.SelectedIndex = 0 Then
            If IsDate(TxtProductXML_Value.Text) = False Then
                MessageBox.Show("Pencarian harus diisi tanggal !!!")
                Exit Sub
            End If
        End If

        CariProductXML("T")
    End Sub

    Private Sub CmbProductXML_Kolom_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CmbProductXML_Kolom.KeyPress
        If e.KeyChar = Chr(13) Then TxtProductXML_Value.Focus()
    End Sub

    Private Sub CariProductXML(ByVal semua As String)
        Try

            OpenConn()

            Dim StrTop As String
            Dim NoFaktur As String

            If ComboBox1.Text.ToString.ToUpper = "SELURUH" Then
                StrTop = ""
            Else
                StrTop = "TOP " & Val(ComboBox1.Text)
            End If

            LvwProductXML_Data.Items.Clear()
            SQL = "Select " & StrTop & " Kode_Barang, Filename, UserID, Tanggal, Jam, No_Faktur From Emi_Log_Product_XML "
            If semua = "T" Then
                If CmbProductXML_Kolom.SelectedIndex = 0 Then
                    Dim inputDate As String = Replace(TxtProductXML_Value.Text, "-", "/")
                    Dim formatString As String = "dd/MM/yyyy"
                    Dim tanggal As DateTime

                    tanggal = DateTime.ParseExact(inputDate, formatString, System.Globalization.CultureInfo.InvariantCulture)

                    SQL = SQL & "where " & arrcariProductXML.Item(CmbProductXML_Kolom.SelectedIndex) & " = '" & tanggal.ToString("yyyy/MM/dd") & "' "
                Else
                    SQL = SQL & "where " & arrcariProductXML.Item(CmbProductXML_Kolom.SelectedIndex) & " like '%" & TxtProductXML_Value.Text & "%' "
                End If
            End If
            SQL = SQL & "order by tanggal, jam desc "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim Lvw As ListViewItem
                    Lvw = LvwProductXML_Data.Items.Add(dr("Tanggal"))
                    Lvw.SubItems.Add(dr("Jam"))
                    Lvw.SubItems.Add(dr("Kode_Barang"))
                    Lvw.SubItems.Add(dr("Filename"))
                    Lvw.SubItems.Add(dr("UserID"))
                    If IsDBNull(dr("No_Faktur")) Then
                        NoFaktur = ""
                    Else
                        NoFaktur = dr("No_Faktur")
                    End If
                    Lvw.SubItems.Add(NoFaktur)
                Loop
            End Using

            CloseConn()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub BtnProductXML_Refresh_Click(sender As Object, e As EventArgs) Handles BtnProductXML_Refresh.Click
        Call kosongProductXML()
    End Sub

    Private Sub Import_Product_XML_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = Chr(13) Then Button2.Focus()
    End Sub
End Class
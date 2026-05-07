Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class N_EMI_Transaksi_Binding_Formula_Trial
    Private SelectedKodeBarang As String = ""
    Private Sub N_EMI_Formulator_Binding_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        With LvBarang
            .View = View.Details
            .FullRowSelect = True
            .GridLines = True
            .MultiSelect = False
            .HideSelection = False
            .Columns.Clear()

            .Columns.Add("Kode Barang", 100)
            .Columns.Add("Nama Barang", 300)
            .Columns.Add("Total Formula", 80, HorizontalAlignment.Center)
        End With

        With LvFormulaTersedia
            .View = View.Details
            .FullRowSelect = True
            .GridLines = True
            .MultiSelect = False
            .HideSelection = False
            .Columns.Clear()

            .Columns.Add("No Formula", 100)
            .Columns.Add("Tanggal Validasi", 100, HorizontalAlignment.Center)
            .Columns.Add("User Validasi", 100, HorizontalAlignment.Center)
        End With

        With LvBindingFormula
            .View = View.Details
            .FullRowSelect = True
            .GridLines = True
            .MultiSelect = False
            .HideSelection = False
            .Columns.Clear()

            .Columns.Add("No Formula", 100)
            .Columns.Add("Posisi", 120)
            .Columns.Add("Keterangan", 250)
        End With

        RbAktif.Checked = True

        Fetch_LvBarang()
    End Sub

    Private Sub Fetch_LvBarang()
        Try
            OpenConn()

            Dim filterBinding As String = ""
            Dim filterCari As String = ""

            If RbSudahBinding.Checked Then
                filterBinding = "AND F.Kode_Barang IS NOT NULL"
            ElseIf RbBelumBinding.Checked Then
                filterBinding = "AND F.Kode_Barang IS NULL"
            End If

            If TxtCariBarang.Text.Trim <> "" Then
                filterCari = $"AND (Brg.Kode_Barang_Inq LIKE '%{TxtCariBarang.Text.Trim}%' 
                   OR Brg.Nama_Inq LIKE '%{TxtCariBarang.Text.Trim}%')"
            Else
                filterCari = ""
            End If

            SQL = $"
                WITH BarangFG AS (
                    SELECT 
                        b.Kode_Perusahaan,
                        b.Kode_Barang_Inq,
                        MAX(b.Nama_Inq) AS Nama_Inq
                    FROM Barang b
                    JOIN EMI_Group_Jenis g 
                        ON g.Kode_Perusahaan = b.Kode_Perusahaan
                        AND g.Id_Group_Jenis = b.Id_Group_Jenis
                        AND (g.flag_finished_good='Y' or g.Flag_semi_Fg='Y')
                    WHERE b.Kode_Perusahaan = '{KodePerusahaan}'
                    GROUP BY 
                        b.Kode_Perusahaan,
                        b.Kode_Barang_Inq
                ),
                BindingTerakhir AS (
                    SELECT *
                    FROM (
                        SELECT 
                            B.*,
                            ROW_NUMBER() OVER (
                                PARTITION BY B.Kode_Perusahaan, B.Kode_Barang 
                                ORDER BY B.Tanggal DESC, B.Jam DESC
                            ) AS rn
                        FROM N_EMI_Transaksi_Formulator_Binding B
                        WHERE B.Kode_Perusahaan = '{KodePerusahaan}' 
                          AND B.Status IS NULL AND B.Flag_Validasi_Main <> 'T'
                    ) x
                    WHERE rn = 1
                )
                SELECT 
                    Brg.Kode_Barang_Inq,
                    Brg.Nama_Inq AS Nama_Barang,
                    COUNT(DISTINCT F.No_Faktur) AS Total_Formula,
                    BT.Flag_Validasi_Main,
                    CASE 
                        WHEN COUNT(DISTINCT F.No_Faktur) = 0
                            THEN 'Tidak Ada Formula'
                        WHEN BT.Flag_Validasi_Main IS NULL
                            THEN 'Sedang Validasi'
                        WHEN COUNT(DISTINCT F.No_Faktur) > 0 AND BT.Kode_Barang IS NULL
                            THEN 'Ada Formula Baru'
                        WHEN BT.Flag_Validasi_Main = 'Y'
                            THEN 'Sudah Validasi'
                        ELSE 'Tidak Ada Formula'
                    END AS Status
                FROM BarangFG Brg
                LEFT JOIN Emi_Transaksi_Formulator F
                    ON F.Kode_Perusahaan = Brg.Kode_Perusahaan
                    AND F.Kode_Barang = Brg.Kode_Barang_Inq
                    AND F.Flag_Validasi = 'Y'
                    AND F.Status IS NULL 
                    AND F.Flag_Validasi_Formula_Produksi = 'Y'
                LEFT JOIN BindingTerakhir BT
                    ON BT.Kode_Perusahaan = Brg.Kode_Perusahaan
                    AND BT.Kode_Barang = Brg.Kode_Barang_Inq
                WHERE 1=1
                    {filterBinding}
                    {filterCari}
                GROUP BY 
                    Brg.Kode_Barang_Inq,
                    Brg.Nama_Inq,
                    BT.Flag_Validasi_Main,
                    BT.Kode_Barang
                ORDER BY 
                    COUNT(DISTINCT F.No_Faktur) DESC
            "

            LvBarang.BeginUpdate()
            LvBarang.Items.Clear()

            Using Dr = OpenTrans(SQL)
                While Dr.Read()
                    Dim kode As String = General_Class.CekNULL(Dr("Kode_Barang_Inq"))
                    Dim nama As String = General_Class.CekNULL(Dr("Nama_Barang"))
                    Dim status As String = General_Class.CekNULL(Dr("Status"))
                    Dim total As Integer = If(String.IsNullOrEmpty(General_Class.CekNULL(Dr("Total_Formula"))), 0, Convert.ToInt32(Dr("Total_Formula")))

                    Dim item As New ListViewItem(kode)
                    item.SubItems.Add(nama)
                    item.SubItems.Add(total.ToString("N0"))

                    Select Case status
                        Case "Sudah Validasi"
                            item.BackColor = Color.LightGreen

                        Case "Tidak Ada Formula"
                            item.BackColor = Color.LightCoral

                        Case "Sedang Validasi"
                            item.BackColor = Color.LightYellow

                        Case "Ada Formula Baru"
                            item.BackColor = Color.LightGray
                    End Select

                    LvBarang.Items.Add(item)
                End While
            End Using

            LvBarang.EndUpdate()
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Fetch_LvFormulaTersedia(kodeBarang As String)
        If RbAktif.Checked Then
            Try
                OpenConn()
                SQL = $"
                    SELECT 
                        F.No_Faktur,
                        F.Tanggal_Validasi,
                        F.Jam_Validasi,
                        F.User_Validasi
                    FROM Emi_Transaksi_Formulator F
                    WHERE 
                        F.Kode_Perusahaan = '{KodePerusahaan}'
                        AND F.Kode_Barang = '{kodeBarang}'
                        AND F.Flag_Validasi = 'Y'
                        AND F.Status IS NULL
                        
                        AND (
                            F.Flag_Validasi_Formula_Produksi = 'Y'
                            OR (
                                F.Flag_Validasi_Main = 'Y'
                                AND F.Kode_Hierarki IS NOT NULL
                            )
                        )

                        AND NOT EXISTS (
                            SELECT 1
                            FROM N_EMI_Transaksi_Formulator_Binding B
                            INNER JOIN N_EMI_Transaksi_Formulator_Binding_Detail D
                                ON D.Kode_Perusahaan = B.Kode_Perusahaan
                                AND D.No_Faktur = B.No_Faktur
                            WHERE 
                                B.Kode_Perusahaan = F.Kode_Perusahaan
                                AND B.Kode_Barang = F.Kode_Barang
                                AND B.Status IS NULL
                                AND D.No_Formulator = F.No_Faktur
                                AND B.No_Faktur = (
                                    SELECT TOP 1 No_Faktur
                                    FROM N_EMI_Transaksi_Formulator_Binding
                                    WHERE 
                                        Kode_Perusahaan = '{KodePerusahaan}'
                                        AND Kode_Barang = '{kodeBarang}'
                                        AND Status IS NULL
                                    ORDER BY Tanggal DESC, Jam DESC
                                )
                        )
                    ORDER BY 
                        F.Tanggal DESC,
                        F.Jam DESC,
                        F.Tanggal_Validasi DESC,
                        F.Jam_Validasi DESC
                "
                LvFormulaTersedia.BeginUpdate()
                LvFormulaTersedia.Items.Clear()

                Using Dr = OpenTrans(SQL)
                    While Dr.Read()
                        Dim noFaktur As String = General_Class.CekNULL(Dr("No_Faktur"))
                        Dim tanggalRaw As String = General_Class.CekNULL(Dr("Tanggal_Validasi"))
                        Dim tanggal As String = If(tanggalRaw = "", "", Convert.ToDateTime(Dr("Tanggal_Validasi")).ToString("dd MMM yy"))
                        Dim user As String = General_Class.CekNULL(Dr("User_Validasi"))

                        Dim item As New ListViewItem(noFaktur)
                        item.SubItems.Add(tanggal)
                        item.SubItems.Add(user)
                        LvFormulaTersedia.Items.Add(item)
                    End While
                End Using

                LvFormulaTersedia.EndUpdate()
                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        End If

        If RbRiwayat.Checked Then
            Try
                OpenConn()
                SQL = $"
                    SELECT 
                        B.No_Faktur,
                        B.Tanggal,
                        B.Jam,
                        B.UserID,
                        B.Flag_Validasi_Main
                    FROM N_EMI_Transaksi_Formulator_Binding B
                    WHERE 
                        B.Kode_Perusahaan = '{KodePerusahaan}'
                        AND B.Kode_Barang = '{kodeBarang}'
                        AND B.Status IS NULL
                    ORDER BY 
                        B.Tanggal DESC,
                        B.Jam DESC
                "

                LvFormulaTersedia.BeginUpdate()
                LvFormulaTersedia.Items.Clear()

                Dim index As Integer = 0

                Using Dr = OpenTrans(SQL)
                    Dim sudahKetemuY As Boolean = False

                    While Dr.Read()
                        Dim noFaktur As String = General_Class.CekNULL(Dr("No_Faktur"))
                        Dim tanggalRaw As String = General_Class.CekNULL(Dr("Tanggal"))
                        Dim tanggal As String = If(tanggalRaw = "", "", Convert.ToDateTime(Dr("Tanggal")).ToString("dd MMM yy"))
                        Dim user As String = General_Class.CekNULL(Dr("UserID"))
                        Dim flagValidasi As String = General_Class.CekNULL(Dr("Flag_Validasi_Main"))

                        Dim item As New ListViewItem(noFaktur)
                        item.SubItems.Add(tanggal)
                        item.SubItems.Add(user)

                        If flagValidasi = "T" Then
                            item.BackColor = Color.LightCoral

                        ElseIf flagValidasi = "Y" AndAlso Not sudahKetemuY Then
                            item.BackColor = Color.LightGreen
                            sudahKetemuY = True

                        Else
                            If Not sudahKetemuY Then
                                item.BackColor = Color.LightYellow
                            Else
                                item.BackColor = Color.Gainsboro
                            End If
                        End If

                        LvFormulaTersedia.Items.Add(item)
                    End While
                End Using

                LvFormulaTersedia.EndUpdate()
                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        End If
    End Sub

    Private Sub Fetch_LvBindingFormula(FilterID As String)
        If RbAktif.Checked Then
            Try
                OpenConn()

                Dim noFakturAktif As String = ""

                SQL = $"
                    SELECT TOP 1 No_Faktur
                    FROM N_EMI_Transaksi_Formulator_Binding
                    WHERE 
                        Kode_Perusahaan = '{KodePerusahaan}'
                        AND Kode_Barang = '{FilterID}'
                        AND Status IS NULL
                        AND Flag_Validasi_Main = 'Y'
                    ORDER BY Tanggal DESC, Jam DESC
                "

                Using drFaktur = OpenTrans(SQL)
                    If drFaktur.Read() Then
                        noFakturAktif = General_Class.CekNULL(drFaktur("No_Faktur"))
                    End If
                End Using

                If noFakturAktif <> "" Then
                    GbBindingFormula.Text = $"Faktur Binding Aktif: {noFakturAktif}"
                Else
                    GbBindingFormula.Text = "Binding Formula"
                End If

                SQL = $"
                    SELECT 
                        D.No_Faktur,
                        D.No_Formulator,
                        D.No_Prioritas,
                        D.Kode_Hierarki,
                        D.Keterangan
                    FROM N_EMI_Transaksi_Formulator_Binding_Detail D
                    WHERE 
                        D.Kode_Perusahaan = '{KodePerusahaan}'
                        AND D.No_Faktur = '{noFakturAktif}'
                    ORDER BY D.No_Prioritas
                "

                LvBindingFormula.BeginUpdate()
                LvBindingFormula.Items.Clear()

                Using Dr = OpenTrans(SQL)
                    While Dr.Read()
                        Dim noFormula As String = General_Class.CekNULL(Dr("No_Formulator"))
                        Dim keterangan As String = General_Class.CekNULL(Dr("Keterangan"))
                        Dim kodeHierarki As String = General_Class.CekNULL(Dr("Kode_Hierarki"))

                        Dim item As New ListViewItem(noFormula)
                        item.SubItems.Add(kodeHierarki)
                        item.SubItems.Add(keterangan)

                        LvBindingFormula.Items.Add(item)
                    End While
                End Using

                If LvBindingFormula.Items.Count > 0 Then
                    LvBindingFormula.Items(0).BackColor = Color.LightGreen
                End If

                LvBindingFormula.EndUpdate()
                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        End If

        If RbRiwayat.Checked Then
            Try
                OpenConn()

                SQL = $"
                    SELECT 
                        D.No_Formulator,
                        D.No_Prioritas,
                        D.Kode_Hierarki,
                        D.Keterangan
                    FROM N_EMI_Transaksi_Formulator_Binding_Detail D
                    WHERE 
                        D.Kode_Perusahaan = '{KodePerusahaan}'
                        AND D.No_Faktur = '{FilterID}'
                    ORDER BY D.No_Prioritas
                "

                LvBindingFormula.BeginUpdate()
                LvBindingFormula.Items.Clear()

                Using Dr = OpenTrans(SQL)
                    While Dr.Read()
                        Dim noFormula As String = General_Class.CekNULL(Dr("No_Formulator"))
                        Dim keterangan As String = General_Class.CekNULL(Dr("Keterangan"))
                        Dim kodeHierarki As String = General_Class.CekNULL(Dr("Kode_Hierarki"))

                        Dim item As New ListViewItem(noFormula)
                        item.SubItems.Add(kodeHierarki)
                        item.SubItems.Add(keterangan)

                        LvBindingFormula.Items.Add(item)
                    End While
                End Using

                If LvBindingFormula.Items.Count > 0 _
                   AndAlso LvFormulaTersedia.SelectedItems.Count > 0 _
                   AndAlso LvFormulaTersedia.SelectedItems(0).Index = 0 Then

                    LvBindingFormula.Items(0).BackColor = Color.LightGreen

                End If

                LvBindingFormula.EndUpdate()
                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        End If
    End Sub

    Private Sub LvBarang_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LvBarang.SelectedIndexChanged
        If LvBarang.SelectedItems.Count > 0 Then
            SelectedKodeBarang = LvBarang.SelectedItems(0).Text

            Fetch_LvFormulaTersedia(SelectedKodeBarang)
            Fetch_LvBindingFormula(SelectedKodeBarang)
        Else
            SelectedKodeBarang = ""
        End If
    End Sub

    Private Sub LvFormulaTersedia_MouseDown(sender As Object, e As MouseEventArgs) Handles LvFormulaTersedia.MouseDown
        If e.Button = MouseButtons.Right Then
            Dim item As ListViewItem = LvFormulaTersedia.GetItemAt(e.X, e.Y)

            If item IsNot Nothing Then
                item.Selected = True
            Else
                LvFormulaTersedia.SelectedItems.Clear()
            End If
        End If
    End Sub

    Private Sub UbahPosisiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UbahPosisiToolStripMenuItem.Click
        Dim ListNoFakturBinding As New List(Of String)
        Dim ListHierarki As New List(Of String)
        Dim ListKeterangan As New List(Of String)

        For Each item As ListViewItem In LvBindingFormula.Items
            ListNoFakturBinding.Add(item.Text)
            ListHierarki.Add(item.SubItems(1).Text)
            ListKeterangan.Add(item.SubItems(2).Text)
        Next

        Dim SD As New N_EMI_SD_Compare_Formulator With {
            .NoFaktur = LvBindingFormula.SelectedItems(0).Text,
            .ArrNoFaktur = ListNoFakturBinding,
            .ArrHierarki = ListHierarki,
            .ArrKeterangan = ListKeterangan
        }

        If SD.ShowDialog() = DialogResult.OK Then
            LvBindingFormula.Items.Clear()

            For i As Integer = 0 To SD.ArrNoFaktur.Count - 1
                Dim item As New ListViewItem(SD.ArrNoFaktur(i))
                item.SubItems.Add(SD.ArrHierarki(i))
                item.SubItems.Add(SD.ArrKeterangan(i))

                LvBindingFormula.Items.Add(item)
            Next
        End If
    End Sub

    Private Sub BindingFormulaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BindingFormulaToolStripMenuItem.Click
        Dim ListNoFakturBinding As New List(Of String)
        Dim ListHierarki As New List(Of String)
        Dim ListKeterangan As New List(Of String)

        For Each item As ListViewItem In LvBindingFormula.Items
            ListNoFakturBinding.Add(item.Text)
            ListHierarki.Add(item.SubItems(1).Text)
            ListKeterangan.Add(item.SubItems(2).Text)
        Next

        Dim SD As New N_EMI_SD_Compare_Formulator With {
            .NoFaktur = LvFormulaTersedia.SelectedItems(0).Text,
            .ArrNoFaktur = ListNoFakturBinding,
            .ArrHierarki = ListHierarki,
            .ArrKeterangan = ListKeterangan
        }

        If SD.ShowDialog() = DialogResult.OK Then
            LvBindingFormula.Items.Clear()

            For i As Integer = 0 To SD.ArrNoFaktur.Count - 1
                Dim item As New ListViewItem(SD.ArrNoFaktur(i))
                item.SubItems.Add(SD.ArrHierarki(i))
                item.SubItems.Add(SD.ArrKeterangan(i))

                LvBindingFormula.Items.Add(item)
            Next

            Dim removeItem = LvFormulaTersedia.SelectedItems(0)
            LvFormulaTersedia.Items.Remove(removeItem)
        End If
    End Sub

    Private Sub DetailFormulaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DetailFormulaToolStripMenuItem.Click
        If LvFormulaTersedia.SelectedItems.Count = 0 Then
            MessageBox.Show("Pilih formula terlebih dahulu.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Dim SD As New N_EMI_SD_Detail_Formulator With {
            .NoFaktur = LvFormulaTersedia.SelectedItems(0).Text
        }
        SD.ShowDialog()
    End Sub

    Private Sub RbAktif_CheckedChanged(sender As Object, e As EventArgs) Handles RbAktif.CheckedChanged
        If RbAktif.Checked Then
            PnlAktif.Visible = False
            LbAktif.Visible = False
            PnlDiajukan.Visible = False
            LbDiajukan.Visible = False
            PnlDitolak.Visible = False
            LbDitolak.Visible = False
            PnlTidakAktif.Visible = False
            LbTidakAktif.Visible = False

            BtnSimpanBinding.Enabled = True

            GbFormulaTersedia.Text = "Formula Tersedia"

            LvFormulaTersedia.Clear()
            LvBindingFormula.Items.Clear()
            LvFormulaTersedia.ContextMenuStrip = CmsFormulaTersedia

            LvBindingFormula.Clear()

            With LvFormulaTersedia
                .View = View.Details
                .FullRowSelect = True
                .GridLines = True
                .MultiSelect = False
                .HideSelection = False
                .Columns.Clear()

                .Columns.Add("No Formula", 100)
                .Columns.Add("Tanggal Validasi", 100, HorizontalAlignment.Center)
                .Columns.Add("User Validasi", 100, HorizontalAlignment.Center)
            End With

            With LvBindingFormula
                .View = View.Details
                .FullRowSelect = True
                .GridLines = True
                .MultiSelect = False
                .HideSelection = False
                .Columns.Clear()

                .Columns.Add("No Formula", 100)
                .Columns.Add("Hierarki", 120)
                .Columns.Add("Keterangan", 250)
            End With

            If Not String.IsNullOrEmpty(SelectedKodeBarang) Then
                Fetch_LvFormulaTersedia(SelectedKodeBarang)
                Fetch_LvBindingFormula(SelectedKodeBarang)
            Else
                LvFormulaTersedia.Items.Clear()
                LvBindingFormula.Items.Clear()
            End If
        End If
    End Sub

    Private Sub RbRiwayat_CheckedChanged(sender As Object, e As EventArgs) Handles RbRiwayat.CheckedChanged
        If RbRiwayat.Checked Then
            PnlAktif.Visible = True
            LbAktif.Visible = True
            PnlDiajukan.Visible = True
            LbDiajukan.Visible = True
            PnlDitolak.Visible = True
            LbDitolak.Visible = True
            PnlTidakAktif.Visible = True
            LbTidakAktif.Visible = True

            BtnSimpanBinding.Enabled = False

            GbFormulaTersedia.Text = "Formula Binding"
            GbBindingFormula.Text = "Binding Formula"

            LvFormulaTersedia.Clear()
            LvBindingFormula.Items.Clear()
            LvFormulaTersedia.ContextMenuStrip = Nothing

            LvBindingFormula.Clear()

            With LvFormulaTersedia
                .View = View.Details
                .FullRowSelect = True
                .GridLines = True
                .MultiSelect = False
                .HideSelection = False
                .Columns.Clear()

                .Columns.Add("No Binding", 100)
                .Columns.Add("Tanggal", 100, HorizontalAlignment.Center)
                .Columns.Add("User", 100, HorizontalAlignment.Center)
            End With

            With LvBindingFormula
                .View = View.Details
                .FullRowSelect = True
                .GridLines = True
                .MultiSelect = False
                .HideSelection = False
                .Columns.Clear()

                .Columns.Add("No Formula", 100)
                .Columns.Add("Hierarki", 120)
                .Columns.Add("Keterangan", 250)
            End With

            If Not String.IsNullOrEmpty(SelectedKodeBarang) Then
                Fetch_LvFormulaTersedia(SelectedKodeBarang)
            Else
                LvFormulaTersedia.Items.Clear()
                LvBindingFormula.Items.Clear()
            End If
        End If
    End Sub

    Private Sub RbBelumBinding_CheckedChanged(sender As Object, e As EventArgs) Handles RbBelumBinding.CheckedChanged
        TxtCariBarang.Text = ""
        SelectedKodeBarang = ""

        LvFormulaTersedia.Items.Clear()
        LvBindingFormula.Items.Clear()

        Fetch_LvBarang()
    End Sub

    Private Sub RbSudahBinding_CheckedChanged(sender As Object, e As EventArgs) Handles RbSudahBinding.CheckedChanged
        TxtCariBarang.Text = ""
        SelectedKodeBarang = ""

        LvFormulaTersedia.Items.Clear()
        LvBindingFormula.Items.Clear()

        Fetch_LvBarang()
    End Sub

    Private Sub BtnCariBarang_Click(sender As Object, e As EventArgs) Handles BtnCariBarang.Click
        Fetch_LvBarang()
    End Sub

    Private Function get_no_faktur() As String
        Dim NoFaktur = fBindingFormula & Format(tgl_skg, "MMyy") & "-" &
                             General_Class.Get_Last_Number2("N_EMI_Transaksi_Formulator_Binding", "No_Faktur", 5,
                             "Kode_perusahaan", KodePerusahaan,
                             "And", "substring(No_Faktur, 1, " & Len(fBindingFormula) + 4 & ")", fBindingFormula & Format(tgl_skg, "MMyy"))

        Return NoFaktur
    End Function

    Private Sub BtnSimpanBinding_Click(sender As Object, e As EventArgs) Handles BtnSimpanBinding.Click
        If LvBindingFormula.Items.Count = 0 Then
            MessageBox.Show("Minimal harus ada satu formula yang di binding.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        If String.IsNullOrEmpty(SelectedKodeBarang) Then
            MessageBox.Show("Pilih barang terlebih dahulu.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        get_jam()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            Dim NoFaktur = get_no_faktur()
            Dim KodeBarang = SelectedKodeBarang
            Dim Tanggal = Format(tgl_skg, "yyyy-MM-dd")
            Dim Jam = Format(tgl_skg, "HH:mm:ss")

            SQL = $"
                SELECT 
                    kode_perusahaan
                FROM N_EMI_Transaksi_Formulator_Binding
                WHERE 
                    Kode_Perusahaan = '{KodePerusahaan}'
                    AND kode_barang = '{KodeBarang}' and status is null and flag_validasi_main is null
            "

            Using Dr = OpenTrans(SQL)
                If Dr.Read() Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Binding Sudah di input, Silahkan Menunggu Validasi ! ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            SQL = $"
                INSERT INTO N_EMI_Transaksi_Formulator_Binding
                (Kode_Perusahaan, No_Faktur, Kode_Barang, Tanggal, Jam, UserID, Status)
                VALUES
                ('{KodePerusahaan}', '{NoFaktur}', '{KodeBarang}', '{Tanggal}', '{Jam}', '{UserID}', NULL)
            "
            ExecuteTrans(SQL)

            For i = 0 To LvBindingFormula.Items.Count - 1
                Dim item = LvBindingFormula.Items(i)
                Dim NoFormulator = item.SubItems(0).Text
                Dim KodeHierarki = item.SubItems(1).Text
                Dim Keterangan = item.SubItems(2).Text
                Dim Prioritas = i + 1

                SQL = $"
                    INSERT INTO N_EMI_Transaksi_Formulator_Binding_Detail
                    (Kode_Perusahaan, No_Faktur, No_Formulator, No_Prioritas, Kode_Hierarki, Tanggal, Jam, UserID, Keterangan)
                    VALUES
                    ('{KodePerusahaan}', '{NoFaktur}', '{NoFormulator}', '{Prioritas}', '{KodeHierarki}', '{Tanggal}', '{Jam}', '{UserID}', '{Keterangan}')
                "
                ExecuteTrans(SQL)
            Next

            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()

            Fetch_LvBarang()

            MessageBox.Show("Data binding berhasil disimpan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub DetailFormulaToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles DetailFormulaToolStripMenuItem1.Click
        Dim SD As New N_EMI_SD_Detail_Formulator With {
            .NoFaktur = LvBindingFormula.SelectedItems(0).Text
        }
        SD.ShowDialog()
    End Sub

    Private Sub BatalkanFormulaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BatalkanFormulaToolStripMenuItem.Click
        If LvBindingFormula.SelectedItems.Count = 0 Then Exit Sub

        Dim item = LvBindingFormula.SelectedItems(0)
        Dim noFaktur As String = item.SubItems(0).Text

        LvBindingFormula.Items.Remove(item)

        Try
            OpenConn()

            SQL = $"
                SELECT 
                    No_Faktur,
                    Tanggal_Validasi,
                    User_Validasi
                FROM Emi_Transaksi_Formulator
                WHERE 
                    Kode_Perusahaan = '{KodePerusahaan}'
                    AND No_Faktur = '{noFaktur}'
            "

            Using Dr = OpenTrans(SQL)
                If Dr.Read() Then
                    Dim tanggalRaw As String = General_Class.CekNULL(Dr("Tanggal_Validasi"))
                    Dim tanggal As String = If(tanggalRaw = "", "", Convert.ToDateTime(Dr("Tanggal_Validasi")).ToString("dd MMM yy"))
                    Dim user As String = General_Class.CekNULL(Dr("User_Validasi"))

                    Dim newItem As New ListViewItem(noFaktur)
                    newItem.SubItems.Add(tanggal)
                    newItem.SubItems.Add(user)

                    LvFormulaTersedia.Items.Add(newItem)
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub LvFormulaTersedia_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LvFormulaTersedia.SelectedIndexChanged
        If RbRiwayat.Checked Then
            If LvFormulaTersedia.SelectedItems.Count > 0 Then
                Fetch_LvBindingFormula(LvFormulaTersedia.SelectedItems(0).Text)
            Else
                LvBindingFormula.Items.Clear()
            End If
        End If
    End Sub

    Private Sub BtnRefresh_Click(sender As Object, e As EventArgs) Handles BtnRefresh.Click
        SelectedKodeBarang = ""
        TxtCariBarang.Text = ""
        RbSudahBinding.Checked = False
        RbBelumBinding.Checked = False
        RbAktif.Checked = True
        Fetch_LvBarang()

        LvFormulaTersedia.Items.Clear()
        LvBindingFormula.Items.Clear()
    End Sub
End Class
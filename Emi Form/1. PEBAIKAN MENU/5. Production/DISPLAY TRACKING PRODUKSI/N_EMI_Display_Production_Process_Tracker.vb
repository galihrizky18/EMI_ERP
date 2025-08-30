Public Class N_EMI_Display_Production_Process_Tracker

    Dim arrMenu As New List(Of (StepName As String, IdMenu As String)) From {
            ("All", "ALL"),
            ("Start Produksi", "MULAIPRODUKSI"),
            ("Pengeluaran Bahan", "GI"),
            ("Production Finish Good", "PRODUCTIONFG"),
            ("Lab Analysis", "LABANALYSIS"),
            ("Military Sampling I", "MILITARYSAMPLING1"),
            ("Sorting & Packing", "SORTING"),
            ("Military Sampling II", "MILITARYSAMPLING2"),
            ("Sorting & Final Inspection", "SORTINGFINAL")
        }

    Dim selectedStep As Panel = Nothing
    Dim selectedCard As Panel = Nothing

    Dim SelectedMenu As String = ""
    Dim SelectedPO As String = ""

    Private Sub N_EMI_Display_Production_Process_Tracker_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.Dock = DockStyle.Fill

        Kosong()

    End Sub

    Private Sub Kosong()

        Tgl1.Value = Now.Date
        Tgl2.Value = Now.Date

        SelectedMenu = ""

        LoadStep()

        Dim FirstPanel As Panel = FlowPanel_Step.Controls(0)
        HandleMenuClick(FirstPanel, New EventArgs)

        Tgl1.Focus()

        LoadDataPO(True)
    End Sub

    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click

        Dim FirstPanel As Panel = FlowPanel_Step.Controls(0)
        HandleMenuClick(FirstPanel, e)

        LoadDataPO()

    End Sub

    Private Sub LoadStep()

        FlowPanel_Step.Controls.Clear()

        If arrMenu.Count = 0 Then Exit Sub

        For i As Integer = 0 To arrMenu.Count - 1

            Dim NamaStep As String = arrMenu(i).StepName
            Dim IdMenu As String = arrMenu(i).IdMenu

            Dim cardMenu As New Panel()
            cardMenu.AutoSize = True
            cardMenu.BackColor = Color.White
            'cardMenu.BorderStyle = BorderStyle.FixedSingle
            cardMenu.Cursor = Cursors.Hand
            cardMenu.BackgroundImageLayout = ImageLayout.Stretch
            cardMenu.Tag = IdMenu
            cardMenu.Margin = New Padding(5)
            AddHandler cardMenu.Click, AddressOf HandleMenuClick
            AddHandler cardMenu.MouseEnter, AddressOf HandleMouseEnter
            AddHandler cardMenu.MouseLeave, AddressOf HandleMouseLeave

            '=====================================
            '=     TAMBAH KOMPONEN PADA CARD     =
            '=====================================
            ' Label Nama Step
            Dim LblNameStep As New Label()
            With LblNameStep
                .Text = NamaStep
                .Font = New Font("Work Sans", 9, FontStyle.Regular)
                .Location = New Point(5, 2)
                .AutoSize = True
                .BackColor = Color.Transparent
            End With
            AddHandler LblNameStep.Click, AddressOf HandleItemMenuClick
            AddHandler LblNameStep.MouseEnter, AddressOf HandleMouseEnter
            AddHandler LblNameStep.MouseLeave, AddressOf HandleMouseLeave
            cardMenu.Controls.Add(LblNameStep)

            Dim panelBottomLine As New Panel()
            With panelBottomLine
                .Height = 2
                .Width = LblNameStep.Width + 5
                .BackColor = Color.LightGray
                .BorderStyle = BorderStyle.None
                .Location = New Point(5, 23)
            End With
            AddHandler panelBottomLine.Click, AddressOf HandleItemMenuClick
            AddHandler panelBottomLine.MouseEnter, AddressOf HandleMouseEnter
            AddHandler panelBottomLine.MouseLeave, AddressOf HandleMouseLeave
            cardMenu.Controls.Add(panelBottomLine)

            FlowPanel_Step.Controls.Add(cardMenu)

        Next

    End Sub

    Private Sub LoadDataPO(Optional all As Boolean = False)

        Try
            OpenConn()

            CloseAllChildForms()
            SelectedMenu = ""

            FL_DataPO.Controls.Clear()
            SQL = "select distinct b.No_Faktur, b.Keterangan, b.Tanggal_Produksi, b.Jam_Produksi, b.keterangan, b.UserId_Release "
            SQL = SQL & "from Emi_Split_Production_Order a, emi_order_produksi b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.No_PO = b.No_Faktur "
            SQL = SQL & "and a.Status is null and b.status is null "
            SQL = SQL & "and a.Kode_Perusahaan= '" & KodePerusahaan & "' "
            If Not all Then
                SQL = SQL & "and a.Tanggal between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "
            End If
            SQL = SQL & "order by b.Tanggal_Produksi "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then

                        Dim PanelSizeWidth As Double = FL_DataPO.Width

                        For i As Integer = 0 To .Rows.Count - 1

                            Dim card As New Panel()
                            card.Tag = .Rows(i).Item("No_Faktur")
                            card.AutoSize = True
                            card.Size = New Size(100, PanelSizeWidth)
                            card.BackColor = Color.White
                            card.BorderStyle = BorderStyle.FixedSingle
                            card.Cursor = Cursors.Hand
                            card.BackgroundImageLayout = ImageLayout.Stretch
                            card.Dock = DockStyle.Top
                            'card.Margin = New Padding(5)
                            card.Padding = New Padding(0, 0, 0, 10)
                            card.Margin = New Padding(5, 3, 5, 3)
                            AddHandler card.Click, AddressOf HandleSplitClick

                            '=====================================
                            '=     TAMBAH KOMPONEN PADA CARD     =
                            '=====================================
                            ' Label No Faktur
                            Dim lblNoFaktur As New Label()
                            lblNoFaktur.Text = .Rows(i).Item("No_Faktur")
                            lblNoFaktur.Font = New Font("Work Sans", 9, FontStyle.Bold)
                            lblNoFaktur.Location = New Point(5, 10)
                            lblNoFaktur.AutoSize = True
                            lblNoFaktur.BackColor = Color.Transparent
                            card.Controls.Add(lblNoFaktur)
                            AddHandler lblNoFaktur.Click, AddressOf HandleSplitItemClick

                            ' Label Tanggal
                            Dim lblTgl As New Label()
                            lblTgl.Text = Char.ConvertFromUtf32(&H1F4C5) & " Tanggal Validasi: " & If(General_Class.CekNULL(.Rows(i).Item("Tanggal_Produksi")) = "", "-", Format(.Rows(i).Item("Tanggal_Produksi"), "dd MMM yyyy"))
                            lblTgl.AutoSize = True
                            lblTgl.MaximumSize = New Size(200, 0)
                            lblTgl.Location = New Point(5, 35)
                            lblTgl.Font = New Font("Work Sans", 7, FontStyle.Regular)
                            lblTgl.BackColor = Color.Transparent
                            AddHandler lblTgl.Click, AddressOf HandleSplitItemClick
                            card.Controls.Add(lblTgl)

                            ' Label User
                            Dim LblUser As New Label()
                            LblUser.Text = Char.ConvertFromUtf32(&H1F464) & " User ID : " & If(General_Class.CekNULL(.Rows(i).Item("UserId_Release")) = "", "-", .Rows(i).Item("UserId_Release"))
                            LblUser.Font = New Font("Work Sans", 7, FontStyle.Bold)
                            LblUser.Location = New Point(5, 53)
                            LblUser.AutoSize = True
                            LblUser.BackColor = Color.Transparent
                            card.Controls.Add(LblUser)
                            AddHandler LblUser.Click, AddressOf HandleSplitItemClick

                            ' Label Keterangan
                            Dim Keterangan As New Label()
                            Keterangan.Text = Char.ConvertFromUtf32(&H1F4DD) & " Keterangan : "
                            Keterangan.AutoSize = True
                            Keterangan.MinimumSize = New Size(185, 0)
                            Keterangan.MaximumSize = New Size(185, 0)
                            Keterangan.Location = New Point(5, 68)
                            Keterangan.Font = New Font("Work Sans", 7, FontStyle.Regular)
                            Keterangan.BackColor = Color.Transparent
                            AddHandler Keterangan.Click, AddressOf HandleSplitItemClick
                            card.Controls.Add(Keterangan)

                            ' Label Keterangan
                            Dim Keterangan2 As New Label()
                            Keterangan2.Text = If(General_Class.CekNULL(.Rows(i).Item("keterangan")) = "", "-", .Rows(i).Item("keterangan"))
                            Keterangan2.AutoSize = True
                            Keterangan2.MinimumSize = New Size(185, 0)
                            Keterangan2.MaximumSize = New Size(185, 0)
                            Keterangan2.Location = New Point(22, 83)
                            Keterangan2.Font = New Font("Work Sans", 7, FontStyle.Regular)
                            Keterangan2.BackColor = Color.Transparent
                            AddHandler Keterangan2.Click, AddressOf HandleSplitItemClick
                            card.Controls.Add(Keterangan2)

                            FL_DataPO.Controls.Add(card)

                        Next
                    End If
                End With
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    '=================================================================================================================================================================================================================
    '=     HANDLE STEP SEMUA
    '=================================================================================================================================================================================================================

    Private Sub LoadStep(ByVal IdStep As String)
        Panel_Isi.Controls.Clear()

        LoadDataStep(SelectedPO)

    End Sub

    Private Sub HandleSplitClick(sender As Object, e As EventArgs)
        Dim clickedCard As Panel = CType(sender, Panel)

        ' SET SEMUA CARD MENJADI TIDAK ACTIVED
        For Each ctrl As Control In FL_DataPO.Controls
            If TypeOf ctrl Is Panel Then
                CType(ctrl, Panel).BackColor = Color.White
            End If
        Next

        clickedCard.BackColor = Color.LightBlue

        ' Tandai card aktif
        selectedCard = clickedCard

        ' Muat data sesuai ID di Tag
        Dim NoFaktur As String = selectedCard.Tag.ToString()
        SelectedPO = NoFaktur
        LoadDataStep(NoFaktur)

    End Sub

    Private Sub HandleSplitItemClick(sender As Object, e As EventArgs)
        Dim panelInduk As Panel = TryCast(sender.Parent, Panel)
        Dim clickedCard As Panel = CType(panelInduk, Panel)

        ' SET SEMUA CARD MENJADI TIDAK ACTIVED
        For Each ctrl As Control In FL_DataPO.Controls
            If TypeOf ctrl Is Panel Then
                CType(ctrl, Panel).BackColor = Color.White
            End If
        Next

        clickedCard.BackColor = Color.LightBlue

        ' Tandai card aktif
        selectedCard = clickedCard

        ' Muat data sesuai ID di Tag
        Dim NoFaktur As String = selectedCard.Tag.ToString()
        SelectedPO = NoFaktur
        LoadDataStep(NoFaktur)
    End Sub

    Private Sub HandleMenuClick(sender As Object, e As EventArgs)
        Dim clickedStep As Panel = CType(sender, Panel)

        ' SET SEMUA STEP MENJADI TIDAK ACTIVED
        For Each ctrl As Control In FlowPanel_Step.Controls

            Dim Lbls As Label = ctrl.Controls.OfType(Of Label)().FirstOrDefault()
            If Lbls IsNot Nothing Then
                CType(Lbls, Label).Font = New Font("Work Sans", 9, FontStyle.Regular)
                Lbls.ForeColor = Color.Black
                'pnl.Width += 10
            End If

            Dim borderBottom As Panel = ctrl.Controls.OfType(Of Panel)().FirstOrDefault()
            If borderBottom IsNot Nothing Then
                borderBottom.BackColor = Color.LightGray
                'pnl.Width += 10
            End If

        Next

        ' SET SEMUA CARD MENJADI TIDAK ACTIVED
        'For Each ctrl As Control In FL_DataPO.Controls
        '    If TypeOf ctrl Is Panel Then
        '        CType(ctrl, Panel).BackColor = Color.White
        '    End If
        'Next

        Dim lbl As Label = clickedStep.Controls.OfType(Of Label)().FirstOrDefault()
        If lbl IsNot Nothing Then
            lbl.Font = New Font("Work Sans", 9, FontStyle.Bold)
            lbl.ForeColor = ColorHighlight
        End If

        Dim pnl As Panel = clickedStep.Controls.OfType(Of Panel)().FirstOrDefault()
        If pnl IsNot Nothing Then
            pnl.BackColor = ColorHighlight
            'pnl.Width += 10
        End If

        ' Tandai card aktif
        selectedStep = clickedStep

        ' Muat data sesuai ID di Tag
        Dim NoFaktur As String = selectedStep.Tag.ToString()
        SelectedMenu = NoFaktur.ToUpper
        'SelectedPO = ""
        LoadStep(NoFaktur)
    End Sub

    Private Sub HandleItemMenuClick(sender As Object, e As EventArgs)
        Dim panelInduk As Panel = TryCast(sender.Parent, Panel)
        Dim clickedStep As Panel = CType(panelInduk, Panel)

        ' SET SEMUA STEP MENJADI TIDAK ACTIVED
        For Each ctrl As Control In FlowPanel_Step.Controls

            Dim Lbls As Label = ctrl.Controls.OfType(Of Label)().FirstOrDefault()
            If Lbls IsNot Nothing Then
                CType(Lbls, Label).Font = New Font("Work Sans", 9, FontStyle.Regular)
                Lbls.ForeColor = Color.Black
                'pnl.Width += 10
            End If

            Dim borderBottom As Panel = ctrl.Controls.OfType(Of Panel)().FirstOrDefault()
            If borderBottom IsNot Nothing Then
                borderBottom.BackColor = Color.LightGray
                'pnl.Width += 10
            End If

        Next

        ' SET SEMUA CARD MENJADI TIDAK ACTIVED
        'For Each ctrl As Control In FL_DataPO.Controls
        '    If TypeOf ctrl Is Panel Then
        '        CType(ctrl, Panel).BackColor = Color.White
        '    End If
        'Next

        Dim lbl As Label = clickedStep.Controls.OfType(Of Label)().FirstOrDefault()
        If lbl IsNot Nothing Then
            lbl.Font = New Font("Work Sans", 9, FontStyle.Bold)
            lbl.ForeColor = ColorHighlight
        End If

        Dim pnl As Panel = clickedStep.Controls.OfType(Of Panel)().FirstOrDefault()
        If pnl IsNot Nothing Then
            pnl.BackColor = ColorHighlight
            'pnl.Width += 10
        End If

        ' Tandai card aktif
        selectedStep = clickedStep

        ' Muat data sesuai ID di Tag
        Dim NoFaktur As String = selectedStep.Tag.ToString()
        SelectedMenu = NoFaktur.ToUpper
        'SelectedPO = ""
        LoadStep(NoFaktur)

    End Sub

    '==========================================================================================================================================================================
    '     HANDLE LOAD STEP
    '==========================================================================================================================================================================
    Private Sub LoadDataStep(ByVal SelectedPO As String)
        If SelectedMenu = "" Then Exit Sub
        If FL_DataPO.Controls.Count = 0 Then Exit Sub

        For Each ctrl As Control In Panel_Isi.Controls
            Dim frm As Form = TryCast(ctrl, Form)
            If frm IsNot Nothing Then
                frm.Close()
                frm.Dispose()
            End If
        Next

        Panel_Isi.Controls.Clear()

        Select Case SelectedMenu
            Case "ALL"
                Dim frm As New N_EMI_SD_Production_Tracker_All()
                frm.SelectedPO = SelectedPO
                frm.TopLevel = False
                frm.FormBorderStyle = FormBorderStyle.None
                frm.Dock = DockStyle.Fill
                frm.Visible = True

                'Panel_Isi.Controls.Clear()
                Panel_Isi.Controls.Add(frm)
                frm.Kosong()
                frm.Show()

            Case "MULAIPRODUKSI"
                Dim frm As New N_EMI_SD_Production_Tracker_Start()
                frm.SelectedPO = SelectedPO
                frm.TopLevel = False
                frm.FormBorderStyle = FormBorderStyle.None
                frm.Dock = DockStyle.Fill
                frm.Visible = True

                'Panel_Isi.Controls.Clear()
                Panel_Isi.Controls.Add(frm)
                frm.Kosong()
                frm.Show()

            Case "GI"
                Dim frm As New N_EMI_SD_Production_Tracker_GI()
                frm.SelectedPO = SelectedPO
                frm.TopLevel = False
                frm.FormBorderStyle = FormBorderStyle.None
                frm.Dock = DockStyle.Fill
                frm.Visible = True

                'Panel_Isi.Controls.Clear()
                Panel_Isi.Controls.Add(frm)
                frm.Kosong()
                frm.Show()

            Case "PRODUCTIONFG"
                Dim frm As New N_EMI_SD_Production_Tracker_GR_1()
                frm.SelectedPO = SelectedPO
                frm.TopLevel = False
                frm.FormBorderStyle = FormBorderStyle.None
                frm.Dock = DockStyle.Fill
                frm.Visible = True

                'Panel_Isi.Controls.Clear()
                Panel_Isi.Controls.Add(frm)
                frm.Kosong()
                frm.Show()

            Case "LABANALYSIS"
                Dim frm As New N_EMI_SD_Production_Tracker_Lab_Analysis()
                frm.SelectedPO = SelectedPO
                frm.TopLevel = False
                frm.FormBorderStyle = FormBorderStyle.None
                frm.Dock = DockStyle.Fill
                frm.Visible = True

                'Panel_Isi.Controls.Clear()
                Panel_Isi.Controls.Add(frm)
                frm.Kosong()
                frm.Show()

            Case "MILITARYSAMPLING1"
                Dim frm As New N_EMI_SD_Production_Tracker_Military_Sampling()
                frm.SelectedPO = SelectedPO
                frm.TopLevel = False
                frm.FormBorderStyle = FormBorderStyle.None
                frm.Dock = DockStyle.Fill
                frm.Visible = True

                'Panel_Isi.Controls.Clear()
                Panel_Isi.Controls.Add(frm)
                frm.Kosong()
                frm.Show()

            Case "SORTING"
                Dim frm As New N_EMI_SD_Production_Tracker_GR_2()
                frm.SelectedPO = SelectedPO
                frm.TopLevel = False
                frm.FormBorderStyle = FormBorderStyle.None
                frm.Dock = DockStyle.Fill
                frm.Visible = True

                'Panel_Isi.Controls.Clear()
                Panel_Isi.Controls.Add(frm)
                frm.Kosong()
                frm.Show()
            Case "MILITARYSAMPLING2"
                Dim frm As New N_EMI_SD_Production_Tracker_Military_Sampling_2()
                frm.SelectedPO = SelectedPO
                frm.TopLevel = False
                frm.FormBorderStyle = FormBorderStyle.None
                frm.Dock = DockStyle.Fill
                frm.Visible = True

                'Panel_Isi.Controls.Clear()
                Panel_Isi.Controls.Add(frm)
                frm.Kosong()
                frm.Show()
            Case "SORTINGFINAL"
                Dim frm As New N_EMI_SD_Production_Tracker_GR_3()
                frm.SelectedPO = SelectedPO
                frm.TopLevel = False
                frm.FormBorderStyle = FormBorderStyle.None
                frm.Dock = DockStyle.Fill
                frm.Visible = True

                'Panel_Isi.Controls.Clear()
                Panel_Isi.Controls.Add(frm)
                frm.Kosong()
                frm.Show()

        End Select

    End Sub

    Private Sub HandleMouseEnter(sender As Object, e As EventArgs)
        If TypeOf sender Is Panel Then
            Dim focusedStep As Panel = CType(sender, Panel)
            If focusedStep.Tag Is Nothing Then Exit Sub
            If focusedStep.Tag.ToString.ToUpper = SelectedMenu.ToString.ToUpper Then Exit Sub
        Else
            Dim focusedStep As Panel = CType(sender.parent, Panel)
            If focusedStep.Tag Is Nothing Then Exit Sub
            If focusedStep.Tag.ToString.ToUpper = SelectedMenu.ToString.ToUpper Then Exit Sub
        End If

        Dim parentCard = If(TypeOf sender Is Control,
                       FindParentCard(DirectCast(sender, Control)),
                       Nothing)

        Dim lbl As Label = parentCard.Controls.OfType(Of Label)().FirstOrDefault()
        If lbl IsNot Nothing Then
            lbl.Font = New Font("Work Sans", 9, FontStyle.Bold)
            lbl.ForeColor = ColorHighlight
        End If

        Dim pnl As Panel = parentCard.Controls.OfType(Of Panel)().FirstOrDefault()
        If pnl IsNot Nothing Then
            pnl.BackColor = ColorHighlight
            'pnl.Width += 10
        End If

    End Sub

    Private Sub HandleMouseLeave(sender As Object, e As EventArgs)

        If TypeOf sender Is Panel Then
            Dim focusedStep As Panel = CType(sender, Panel)
            If focusedStep.Tag Is Nothing Then Exit Sub
            If focusedStep.Tag.ToString.ToUpper = SelectedMenu.ToString.ToUpper Then Exit Sub
        Else
            Dim focusedStep As Panel = CType(sender.parent, Panel)
            If focusedStep.Tag Is Nothing Then Exit Sub
            If focusedStep.Tag.ToString.ToUpper = SelectedMenu.ToString.ToUpper Then Exit Sub
        End If

        Dim parentCard = If(TypeOf sender Is Control,
                       FindParentCard(DirectCast(sender, Control)),
                       Nothing)

        Dim lbl As Label = parentCard.Controls.OfType(Of Label)().FirstOrDefault()
        If lbl IsNot Nothing Then
            lbl.Font = New Font("Work Sans", 9, FontStyle.Regular)
            lbl.ForeColor = Color.Black
        End If

        Dim pnl As Panel = parentCard.Controls.OfType(Of Panel)().FirstOrDefault()
        If pnl IsNot Nothing Then
            pnl.BackColor = Color.LightGray
            'pnl.Width -= 10
        End If

    End Sub

    Private Function FindParentCard(ctrl As Control) As Panel
        While ctrl IsNot Nothing
            If TypeOf ctrl Is Panel AndAlso DirectCast(ctrl, Panel).Tag IsNot Nothing Then
                Return DirectCast(ctrl, Panel)
            End If
            ctrl = ctrl.Parent
        End While
        Return Nothing
    End Function

    '=================================================================================================================================================================================================================
    '=     HANDLE KEYPRESS
    '=================================================================================================================================================================================================================
    Private Sub Tgl1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl1.KeyPress
        If e.KeyChar = Chr(13) Then Tgl2.Focus()
    End Sub

    Private Sub Tgl2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl2.KeyPress
        If e.KeyChar = Chr(13) Then Btn_Cari.Focus()
    End Sub

    Private Sub N_EMI_Display_Production_Process_Tracker_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        'CloseAllChildForms(Panel_Isi)
        CloseAllChildForms()
    End Sub

    'Private Sub CloseAllChildForms(parent As Control)
    '    For Each ctrl As Control In parent.Controls
    '        If TypeOf ctrl Is Form Then
    '            Dim f As Form = CType(ctrl, Form)
    '            CloseAllChildForms(f)
    '            f.Close()
    '        ElseIf ctrl.HasChildren Then
    '            CloseAllChildForms(ctrl)
    '        End If
    '    Next
    'End Sub

    Private Sub CloseAllChildForms()
        'For Each ctrl As Control In Panel_Isi.Controls
        '    If TypeOf ctrl Is Form Then
        '        Dim f As Form = DirectCast(ctrl, Form)
        '        f.Close()
        '    End If
        'Next
        For Each ctrl As Control In Panel_Isi.Controls
            Dim frm As Form = TryCast(ctrl, Form)
            If frm IsNot Nothing Then
                frm.Close()
                frm.Dispose()
            End If
        Next

        Panel_Isi.Controls.Clear()
    End Sub

End Class
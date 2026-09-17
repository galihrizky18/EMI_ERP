Imports System.Collections.Concurrent
Imports System.Drawing.Drawing2D
Imports System.IO
Imports System.Net.Http

Public Class Main_Menu

	Private Shared ReadOnly httpClient As New HttpClient()
	Private Shared ReadOnly ImageRamCache As New ConcurrentDictionary(Of String, Image)()
	Private Shared ReadOnly LocalCacheFolder As String = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Cache", "MenuIcons")

	Private placeholderImage As Bitmap
	Private ActiveHoverCard As Panel = Nothing

	'=======================================================================================================================================
	'= KONFIGURASI JUMLAH MAKSIMUM MENU PER BARIS HORISONTAL (Ubah variabel ini untuk mengatur banyak menu dalam 1 baris)
	'=======================================================================================================================================
	Private MaxCardsPerRow As Integer = 7

	Private Async Sub Main_Menu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		' 1. Monitor Setup (Multimonitor support)
		If Screen.AllScreens.Length > 1 Then
			Me.StartPosition = FormStartPosition.Manual
			Dim secondaryScreen As Screen = Screen.AllScreens(1)
			Me.WindowState = FormWindowState.Normal
			Me.Location = secondaryScreen.Bounds.Location
			Me.WindowState = FormWindowState.Maximized
		Else
			Me.StartPosition = FormStartPosition.CenterScreen
			Me.WindowState = FormWindowState.Maximized
		End If

		' Set user info greeting
		If Not String.IsNullOrWhiteSpace(UserID) Then
			Lbl_UserGreeting.Text = $"👤 User: {UserID}  |  ERP Executive Control"
		End If

		' Buat folder disk cache jika belum ada
		Try
			If Not Directory.Exists(LocalCacheFolder) Then
				Directory.CreateDirectory(LocalCacheFolder)
			End If
		Catch ex As Exception
		End Try

		' Inisialisasi placeholder icon (58x58)
		placeholderImage = New Bitmap(58, 58)
		Using g As Graphics = Graphics.FromImage(placeholderImage)
			g.SmoothingMode = SmoothingMode.AntiAlias
			g.Clear(Color.Transparent)
			Using brush As New SolidBrush(Color.FromArgb(241, 245, 249))
				g.FillRectangle(brush, 2, 2, 54, 54)
			End Using
			Using pen As New Pen(Color.FromArgb(203, 213, 225), 1.5F)
				g.DrawRectangle(pen, 2, 2, 54, 54)
			End Using
		End Using

		CenterFlowLayout()

		' 2. Fetch data dari database secara async
		Dim DataMenu As DataTable = Await Task.Run(Function() GetLoadMenuFromDB(UserID))

		' 3. Render UI Menu secara paralel & cepat
		Await LoadMenuAsync(DataMenu)
	End Sub

	Private Sub Main_Menu_Resize(sender As Object, e As EventArgs) Handles Me.Resize
		CenterFlowLayout()
	End Sub

	Private Sub CenterFlowLayout()
		If FlowLayoutMenu_Main Is Nothing OrElse PanelSubHeader Is Nothing Then Exit Sub

		Dim topOffset As Integer = Panel1.Height + PanelSubHeader.Height
		Dim cardSlotWidth As Integer = 122 ' Card 110px + Margin 6px left + Margin 6px right
		Dim extraPad As Integer = 44 ' 24px internal padding (12 left + 12 right) + 20px scrollbar reserve

		' Hitung lebar maksimal area form yang tersedia
		Dim availableWidth As Integer = Math.Max(300, Me.ClientSize.Width - 40)

		' Tentukan jumlah kolom yang muat (dibatasi oleh MaxCardsPerRow)
		Dim colCount As Integer = Math.Max(1, Math.Min(MaxCardsPerRow, (availableWidth - extraPad) \ cardSlotWidth))

		' Hitung lebar presisi FlowLayoutPanel agar tepat menampung colCount kartu dalam 1 baris tanpa terpotong
		Dim requiredWidth As Integer = (colCount * cardSlotWidth) + extraPad

		FlowLayoutMenu_Main.Width = Math.Min(availableWidth, requiredWidth)
		FlowLayoutMenu_Main.Left = Math.Max(10, (Me.ClientSize.Width - FlowLayoutMenu_Main.Width) \ 2)

		Dim availableHeight As Integer = Me.ClientSize.Height - topOffset - 20
		FlowLayoutMenu_Main.Height = Math.Max(300, availableHeight)
		FlowLayoutMenu_Main.Top = topOffset + Math.Max(10, (availableHeight - FlowLayoutMenu_Main.Height) \ 2)
	End Sub

	Private Function GetLoadMenuFromDB(ByVal idUser As String) As DataTable
		Dim data As New DataTable

		Try
			OpenConn()
			Dim safeUserID As String = idUser.Replace("'", "''")

			Dim sqlQuery As String =
				"SELECT a.MainMenuID, a.ImagePath, a.Title, " &
				$"ISNULL((SELECT TOP 1 'Y' FROM RoleMainMenus z WHERE a.MainMenuID = z.MainMenuID AND z.UserID = '{safeUserID}'), 'T') AS Akses " &
				"FROM mainmenu a " &
				"ORDER BY Akses DESC, urut"

			Using dr = OpenTrans(sqlQuery)
				If dr.HasRows Then
					data.Load(dr)
				Else
					MessageBox.Show("Menu Utama Tidak Ditemukan", "ERP System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
				End If
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
		End Try

		Return data
	End Function

	Private Async Function LoadMenuAsync(ByVal DataMenu As DataTable) As Task
		Dim ParentPanel As Panel = FlowLayoutMenu_Main

		ParentPanel.SuspendLayout()
		ParentPanel.Controls.Clear()

		If DataMenu.Rows.Count > 0 Then
			Lbl_ModuleCountBadge.Text = $"✨ {DataMenu.Rows.Count} Modul"

			Dim tasks As New List(Of Task)()

			For Each data As DataRow In DataMenu.Rows
				Dim isAccess As Boolean = (data("Akses").ToString() = "Y")
				Dim imgPath As String = data("ImagePath").ToString()
				Dim titleStr As String = data("Title").ToString()
				Dim mainID As String = data("MainMenuID").ToString()

				tasks.Add(CreateNewPanelMenuAsync(ParentPanel, imgPath, titleStr, UserID, mainID, isAccess))
			Next

			Await Task.WhenAll(tasks)
		Else
			Lbl_ModuleCountBadge.Text = "✨ 0 Modul"
		End If

		ParentPanel.ResumeLayout(True)
		CenterFlowLayout()
	End Function

	Private Async Function CreateNewPanelMenuAsync(ByVal ParentPanel As Panel, ByVal ImagePath As String, ByVal Title As String, ByVal _UserID As String, ByVal _MainMenuID As String, ByVal isAccess As Boolean) As Task
		' Compact Proportional Card (110px x 118px, Margin 6px)
		Dim newPanel As New Panel() With {
			.Width = 110,
			.Height = 118,
			.Margin = New Padding(6),
			.Tag = isAccess,
			.BackColor = Color.Transparent
		}

		' Large Sharp Icon PictureBox (58px x 58px)
		Dim picBox As New PictureBox() With {
			.SizeMode = PictureBoxSizeMode.Zoom,
			.Width = 58,
			.Height = 58,
			.Top = 10,
			.Left = 26,
			.BackColor = Color.Transparent
		}

		' Title Label (Center Aligned, Compact Text)
		Dim titleLabel As New Label() With {
			.Text = Title,
			.Font = New Font("Work Sans", 8.25!, FontStyle.Bold),
			.ForeColor = If(isAccess, Color.FromArgb(30, 41, 59), Color.FromArgb(148, 163, 184)),
			.TextAlign = ContentAlignment.TopCenter,
			.AutoSize = False,
			.Top = 72,
			.Left = 4,
			.Width = 102,
			.Height = 42,
			.BackColor = Color.Transparent
		}

		newPanel.Controls.Add(picBox)
		newPanel.Controls.Add(titleLabel)

		' Indicator lock icon if no access
		If Not isAccess Then
			Dim lockBadge As New Label() With {
				.Text = "🔒",
				.Font = New Font("Segoe UI Emoji", 8.0!, FontStyle.Regular),
				.ForeColor = Color.FromArgb(148, 163, 184),
				.AutoSize = True,
				.Top = 4,
				.Left = 90,
				.BackColor = Color.Transparent
			}
			newPanel.Controls.Add(lockBadge)
		End If

		ParentPanel.Controls.Add(newPanel)

		' Paint Event Handler for smooth rounded card
		AddHandler newPanel.Paint, AddressOf CardPanel_Paint

		' Event Handlers for Interactivity & Flicker-Free Hover
		If isAccess Then
			newPanel.Cursor = Cursors.Hand

			Dim clickAction = Sub(sender As Object, e As EventArgs) OpenMenuForm(_UserID, _MainMenuID)
			AddHandler newPanel.Click, clickAction
			AddHandler picBox.Click, clickAction
			AddHandler titleLabel.Click, clickAction

			AddHandler newPanel.MouseEnter, AddressOf Card_MouseEnter
			AddHandler newPanel.MouseLeave, AddressOf Card_MouseLeave
			AddHandler picBox.MouseEnter, AddressOf Card_MouseEnter
			AddHandler picBox.MouseLeave, AddressOf Card_MouseLeave
			AddHandler titleLabel.MouseEnter, AddressOf Card_MouseEnter
			AddHandler titleLabel.MouseLeave, AddressOf Card_MouseLeave
		Else
			newPanel.Cursor = Cursors.Default
			Dim noAccessAction = Sub(sender As Object, e As EventArgs)
									 MessageBox.Show($"Anda tidak memiliki hak akses untuk menu '{Title}'.", "Akses Dibatasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
								 End Sub
			AddHandler newPanel.Click, noAccessAction
			AddHandler picBox.Click, noAccessAction
			AddHandler titleLabel.Click, noAccessAction
		End If

		' FAST CACHED ASYNC IMAGE LOADING
		Dim iconImage As Image = Await GetOrFetchImageAsync(ImagePath)
		picBox.Image = iconImage
	End Function

	Private Sub CardPanel_Paint(sender As Object, e As PaintEventArgs)
		Dim pnl As Panel = DirectCast(sender, Panel)
		Dim isAccess As Boolean = TypeOf pnl.Tag Is Boolean AndAlso CBool(pnl.Tag)
		Dim isHovered As Boolean = (ActiveHoverCard Is pnl)

		e.Graphics.SmoothingMode = SmoothingMode.AntiAlias
		e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality
		e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit

		Dim rect As New Rectangle(2, 2, pnl.Width - 5, pnl.Height - 5)
		Dim cornerRadius As Integer = 12

		Using path As GraphicsPath = GetRoundedRectPath(rect, cornerRadius)
			' Ambient Drop Shadow
			If isAccess Then
				Dim shadowAlpha As Integer = If(isHovered, 30, 10)
				Dim shadowOffset As Integer = If(isHovered, 3, 1)
				Using shadowPath As GraphicsPath = GetRoundedRectPath(New Rectangle(rect.X, rect.Y + shadowOffset, rect.Width, rect.Height), cornerRadius)
					Using shadowBrush As New SolidBrush(Color.FromArgb(shadowAlpha, 2, 132, 199))
						e.Graphics.FillPath(shadowBrush, shadowPath)
					End Using
				End Using
			End If

			' Card Background Gradient
			Dim bgGradientColor1 As Color = If(isAccess, If(isHovered, Color.FromArgb(240, 249, 255), Color.White), Color.FromArgb(248, 250, 252))
			Dim bgGradientColor2 As Color = If(isAccess, If(isHovered, Color.FromArgb(224, 242, 254), Color.White), Color.FromArgb(241, 245, 249))

			Using bgBrush As New LinearGradientBrush(rect, bgGradientColor1, bgGradientColor2, LinearGradientMode.Vertical)
				e.Graphics.FillPath(bgBrush, path)
			End Using

			' Border Line
			Dim borderColor As Color = Color.FromArgb(226, 232, 240) '#E2E8F0
			If isAccess AndAlso isHovered Then
				borderColor = Color.FromArgb(2, 132, 199) '#0284C7
			End If

			Using pen As New Pen(borderColor, If(isHovered, 1.8F, 1.0F))
				e.Graphics.DrawPath(pen, path)
			End Using
		End Using
	End Sub

	Private Function GetRoundedRectPath(ByVal rect As Rectangle, ByVal radius As Integer) As GraphicsPath
		Dim path As New GraphicsPath()
		Dim diameter As Integer = radius * 2
		Dim size As New Size(diameter, diameter)
		Dim arc As New Rectangle(rect.Location, size)

		path.AddArc(arc, 180, 90)

		arc.X = rect.Right - diameter
		path.AddArc(arc, 270, 90)

		arc.Y = rect.Bottom - diameter
		path.AddArc(arc, 0, 90)

		arc.X = rect.Left
		path.AddArc(arc, 90, 90)

		path.CloseFigure()
		Return path
	End Function

	Private Sub Card_MouseEnter(sender As Object, e As EventArgs)
		Dim pnl As Panel = GetParentCardPanel(sender)
		If pnl IsNot Nothing AndAlso TypeOf pnl.Tag Is Boolean AndAlso CBool(pnl.Tag) Then
			If ActiveHoverCard IsNot pnl Then
				Dim oldCard As Panel = ActiveHoverCard
				ActiveHoverCard = pnl
				If oldCard IsNot Nothing Then UpdateCardHoverState(oldCard, False)
				UpdateCardHoverState(pnl, True)
			End If
		End If
	End Sub

	Private Sub Card_MouseLeave(sender As Object, e As EventArgs)
		Dim pnl As Panel = GetParentCardPanel(sender)
		If pnl IsNot Nothing Then
			' Cek apakah kursor mouse benar-benar sudah keluar dari area luar kartu
			Dim mousePos As Point = pnl.PointToClient(Cursor.Position)
			If Not pnl.ClientRectangle.Contains(mousePos) Then
				If ActiveHoverCard Is pnl Then
					ActiveHoverCard = Nothing
					UpdateCardHoverState(pnl, False)
				End If
			End If
		End If
	End Sub

	Private Sub UpdateCardHoverState(ByVal pnl As Panel, ByVal isHovered As Boolean)
		If pnl Is Nothing Then Exit Sub

		For Each c As Control In pnl.Controls
			If TypeOf c Is Label AndAlso c.Text <> "🔒" Then
				c.ForeColor = If(isHovered, Color.FromArgb(2, 132, 199), Color.FromArgb(30, 41, 59))
			End If
		Next

		pnl.Invalidate()
	End Sub

	Private Function GetParentCardPanel(sender As Object) As Panel
		If TypeOf sender Is Panel Then
			Dim p As Panel = DirectCast(sender, Panel)
			If p.Parent Is FlowLayoutMenu_Main Then Return p
		End If
		If TypeOf sender Is Control Then
			Dim parentCtrl As Control = CType(sender, Control).Parent
			If TypeOf parentCtrl Is Panel Then
				Dim p As Panel = DirectCast(parentCtrl, Panel)
				If p.Parent Is FlowLayoutMenu_Main Then Return p
			End If
		End If
		Return Nothing
	End Function

	'=======================================================================================================================================
	'=     SISTEM IMAGE CACHING TINGKAT TINGGI (RAM & DISK CACHE UNTUK PERFORMA SUPER CEPAT)
	'=======================================================================================================================================
	Private Async Function GetOrFetchImageAsync(ByVal imagePath As String) As Task(Of Image)
		If String.IsNullOrWhiteSpace(imagePath) Then Return placeholderImage

		Dim cacheKey As String = imagePath.Trim()

		' 1. Cek Memory Cache (RAM) - Respon 0ms
		Dim cachedImg As Image = Nothing
		If ImageRamCache.TryGetValue(cacheKey, cachedImg) Then
			Return cachedImg
		End If

		' 2. Cek Disk Cache (Local Storage) - Respon <1ms
		Dim fileName As String = GetSafeFileName(cacheKey)
		Dim localDiskPath As String = Path.Combine(LocalCacheFolder, fileName)

		If File.Exists(localDiskPath) Then
			Try
				Dim bytes As Byte() = File.ReadAllBytes(localDiskPath)
				Using ms As New MemoryStream(bytes)
					Dim loadedImg As Image = Image.FromStream(ms)
					ImageRamCache.TryAdd(cacheKey, loadedImg)
					Return loadedImg
				End Using
			Catch ex As Exception
			End Try
		End If

		' 3. Cek File Lokal di Sistem
		If File.Exists(imagePath) Then
			Try
				Dim bytes As Byte() = File.ReadAllBytes(imagePath)
				Using ms As New MemoryStream(bytes)
					Dim loadedImg As Image = Image.FromStream(ms)
					ImageRamCache.TryAdd(cacheKey, loadedImg)
					Return loadedImg
				End Using
			Catch ex As Exception
			End Try
		End If

		' 4. Ambil via Network HTTP jika merupakan URL
		If imagePath.StartsWith("http://", StringComparison.OrdinalIgnoreCase) OrElse imagePath.StartsWith("https://", StringComparison.OrdinalIgnoreCase) Then
			Try
				Dim imageBytes As Byte() = Await httpClient.GetByteArrayAsync(imagePath)
				Using ms As New MemoryStream(imageBytes)
					Dim netImg As Image = Image.FromStream(ms)

					' Simpan ke disk cache untuk penggunaan berikutnya
					Try
						File.WriteAllBytes(localDiskPath, imageBytes)
					Catch ex As Exception
					End Try

					ImageRamCache.TryAdd(cacheKey, netImg)
					Return netImg
				End Using
			Catch ex As Exception
				Return placeholderImage
			End Try
		End If

		Return placeholderImage
	End Function

	Private Function GetSafeFileName(ByVal input As String) As String
		Dim invalidChars As Char() = Path.GetInvalidFileNameChars()
		Dim cleanName As String = input
		For Each c As Char In invalidChars
			cleanName = cleanName.Replace(c, "_"c)
		Next
		cleanName = cleanName.Replace(":", "_").Replace("/", "_").Replace("\", "_")
		If cleanName.Length > 60 Then cleanName = cleanName.Substring(cleanName.Length - 60)
		Return "icon_" & cleanName & ".png"
	End Function

	Private Sub OpenMenuForm(ByVal _UserID As String, ByVal _MainMenuID As String)
		UserID = _UserID
		MainMenuID = _MainMenuID

		Dim existingFMenu As Form = Application.OpenForms.OfType(Of FMenu)().FirstOrDefault()

		If existingFMenu IsNot Nothing Then
			existingFMenu.StartPosition = FormStartPosition.CenterScreen
			existingFMenu.Show()
			existingFMenu.BringToFront()
			existingFMenu.Focus()
		Else
			FMenu.StartPosition = FormStartPosition.CenterScreen
			FMenu.Show()
			FMenu.Focus()
		End If

		Me.Hide()
	End Sub

	Private Sub Main_Menu_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
		If placeholderImage IsNot Nothing Then placeholderImage.Dispose()
		End
	End Sub

End Class
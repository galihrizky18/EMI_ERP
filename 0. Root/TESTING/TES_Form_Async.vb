Public Class TES_Form_Async

	Dim sw As New Diagnostics.Stopwatch

	Private Sub TES_Form_Async_Load(sender As Object, e As EventArgs) Handles MyBase.Load

		With Lv_1
			.Columns.Clear()
			.Columns.Add("Kode Barang", 100, HorizontalAlignment.Left)
			.Columns.Add("Nama Barang", 100, HorizontalAlignment.Left)
			.View = View.Details
		End With

		With Lv_2
			.Columns.Clear()
			.Columns.Add("Kode Barang", 100, HorizontalAlignment.Left)
			.Columns.Add("Nama Barang", 100, HorizontalAlignment.Left)
			.View = View.Details
		End With

	End Sub

	Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click, RoundedButton1.Click

		sw.Start()
		Timer1.Start() ' Menjalankan pembaruan teks Label1

		Try
			OpenConn()

			Lv_1.Items.Clear() : Lv_2.Items.Clear()
			Lv_1.BeginUpdate()
			SQL = $"select Kode_Barang, Nama from Barang_Lain"
			Using Dr = OpenTrans(SQL)
				Do While Dr.Read
					Dim Lv As ListViewItem
					Lv = Lv_1.Items.Add(Dr("Kode_Barang"))
					Lv.SubItems.Add(Dr("Nama"))
				Loop
			End Using
			Lv_1.EndUpdate()

			PB_1.Visible = True
			PB_1.MarqueeAnimationSpeed = 10

			Task.Run(Sub()

						 Threading.Thread.Sleep(3000)

						 Try
							 OpenConn2(CDatabase)

							 Dim listData As New List(Of ListViewItem)
							 SQL = "select Kode_Barang, Nama from Barang_Lain "
							 Using Dr2 = OpenTrans2(SQL)
								 Do While Dr2.Read
									 Dim item As New ListViewItem(Dr2("Kode_Barang").ToString())
									 item.SubItems.Add(Dr2("Nama").ToString())
									 listData.Add(item)
								 Loop
							 End Using

							 Me.BeginInvoke(Sub()
												PB_1.Visible = False
												Lv_2.BeginUpdate()

												Lv_2.Items.AddRange(listData.ToArray())

												Lv_2.EndUpdate()

											End Sub)

							 CloseConn2()
						 Catch ex As Exception
							 CloseConn2()
							 Me.BeginInvoke(Sub()
												PB_1.Visible = False
												Lv_2.Items.Clear()
												MessageBox.Show("Error Background: " & ex.Message)
											End Sub)
						 End Try
					 End Sub)

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try
	End Sub

	Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
		sw.Stop()
		Timer1.Stop() ' Menghentikan pembaruan teks
	End Sub

	Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
		' Memperbarui tampilan Label1 setiap interval Timer
		Label1.Text = sw.Elapsed.ToString("hh\:mm\:ss\.ff")
	End Sub

	Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
		Button3.Enabled = False
		Pb_Cetak.MarqueeAnimationSpeed = 10
		Pb_Cetak.Visible = True

		Dim cts As New Threading.CancellationTokenSource(50000)

		Task.Factory.StartNew(Sub()
								  Try
									  ' Jalankan fungsi cetak dengan mengirimkan Token
									  PROSES_CETAK_FAKTUR(cts.Token)

									  Me.BeginInvoke(Sub()
														 MessageBox.Show("Cetak Berhasil!")
														 Button3.Enabled = True
														 Pb_Cetak.Visible = False
													 End Sub)
								  Catch ex As OperationCanceledException
									  ' Ini otomatis terpanggil jika waktu 5 detik habis
									  Me.BeginInvoke(Sub()
														 MessageBox.Show("Waktu habis! Proses dihentikan paksa.")
														 Button3.Enabled = True
														 Pb_Cetak.Visible = False
													 End Sub)
								  Catch ex As Exception
									  Me.BeginInvoke(Sub()
														 MessageBox.Show("Error: " & ex.Message)
														 Button3.Enabled = True
														 Pb_Cetak.Visible = False
													 End Sub)
								  End Try
							  End Sub, cts.Token, TaskCreationOptions.LongRunning Or TaskCreationOptions.PreferFairness, TaskScheduler.Default)

	End Sub

	Private Sub PROSES_CETAK_FAKTUR(token As Threading.CancellationToken)
		' --- CEK 1: Sebelum buka koneksi ---
		token.ThrowIfCancellationRequested()

		Try
			'OpenConn2()
			' Simulasi proses looping data atau render laporan
			For i As Integer = 1 To 100
				' --- CEK 2: Di dalam loop (Sangat Penting) ---
				' Jika waktu habis, baris ini akan melempar error dan keluar dari Sub
				token.ThrowIfCancellationRequested()

				' Simulasi beban kerja
				Threading.Thread.Sleep(100)
			Next
		Finally
			' Pastikan koneksi selalu tertutup baik sukses maupun batal
			CloseConn2()
		End Try
	End Sub

End Class
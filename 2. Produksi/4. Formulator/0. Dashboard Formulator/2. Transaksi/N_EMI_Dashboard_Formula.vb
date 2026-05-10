Public Class N_EMI_Dashboard_Formula

	Dim UserPosition As String = ""

	Dim Status_HeadDept As String() = {"Belum Diproses", "Selesai Trial Kitchen", "Selesai Trial Produksi"}
	Dim Status_BOD As String() = {"Menunggu Validasi BOD", "Proses Produksi Komersial"}

	Dim DgvParent_NoFormula, DgvParent_TanggalFormula, DgvParent_KdBarang, DgvParent_NmBarang, DgvParent_HPPMin, DgvParent_HPPMax, DgvParent_Jumlah, DgvParent_Satuan,
		DgvParent_JenisFormula, DgvParent_PosisiBinding, DgvParent_StatusFormula, DgvParent_Deskripsi, DgvParent_BtnValiasi As String

	Dim CellParent_NoFormula As Integer = 0
	Dim CellParent_TanggalFormula As Integer = 1
	Dim CellParent_KdBarang As Integer = 2
	Dim CellParent_NmBarang As Integer = 3
	Dim CellParent_HPPMin As Integer = 4
	Dim CellParent_HPPMax As Integer = 5
	Dim CellParent_Jumlah As Integer = 6
	Dim CellParent_Satuan As Integer = 7
	Dim CellParent_JenisFormula As Integer = 8
	Dim CellParent_PosisiBinding As Integer = 9
	Dim CellParent_StatusFormula As Integer = 10
	Dim CellParent_Deskripsi As Integer = 11
	Dim CellParent_BtnValidasi As Integer = 12

	Private Sub N_EMI_Dashboard_Formula_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		Me.Dock = DockStyle.Fill

		'==============================
		'=     GET USER POSISITON     =
		'==============================

		CekRoleUser("User_Formula_Position_Staff", "STAFF")
		CekRoleUser("User_Formula_Position_HeadDept", "HEADDEPT")
		CekRoleUser("User_Formula_Position_CLevel", "CLEVEL")

		If String.IsNullOrWhiteSpace(UserPosition) Then
			MessageBox.Show("Terjadi Kesalahan, Posisi User Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
			Me.Close()
		End If

		Kosong()

	End Sub

	Public Sub Kosong()

		Load_Data_DGV_Parent()
	End Sub

	Private Sub Get_Data_DGV_Parent(ByVal index As Integer)

		DgvParent_NoFormula = DGV_Formula.Rows(index).Cells(CellParent_NoFormula).Value
		DgvParent_TanggalFormula = DGV_Formula.Rows(index).Cells(CellParent_TanggalFormula).Value
		DgvParent_KdBarang = DGV_Formula.Rows(index).Cells(CellParent_KdBarang).Value
		DgvParent_NmBarang = DGV_Formula.Rows(index).Cells(CellParent_NmBarang).Value
		DgvParent_HPPMin = DGV_Formula.Rows(index).Cells(CellParent_HPPMin).Value
		DgvParent_HPPMax = DGV_Formula.Rows(index).Cells(CellParent_HPPMax).Value
		DgvParent_Jumlah = DGV_Formula.Rows(index).Cells(CellParent_Jumlah).Value
		DgvParent_Satuan = DGV_Formula.Rows(index).Cells(CellParent_Satuan).Value
		DgvParent_JenisFormula = DGV_Formula.Rows(index).Cells(CellParent_JenisFormula).Value
		DgvParent_PosisiBinding = DGV_Formula.Rows(index).Cells(CellParent_PosisiBinding).Value
		DgvParent_StatusFormula = DGV_Formula.Rows(index).Cells(CellParent_StatusFormula).Value
		DgvParent_Deskripsi = DGV_Formula.Rows(index).Cells(CellParent_Deskripsi).Value
		DgvParent_BtnValiasi = DGV_Formula.Rows(index).Cells(CellParent_BtnValidasi).Value

	End Sub

	Private Sub Load_Data_DGV_Parent()
		Try
			OpenConn()

			DGV_Formula.Rows.Clear()
			SQL = $"
                SELECT
                    a.No_Faktur,
                    FORMAT(a.Tanggal_Validasi, 'dd MMM yyyy') AS Tanggal_Validasi,
                    b.Kode_Barang_Inq,
                    b.Nama,
                    0 AS HPP_Min,
                    0 AS HPP_Max,
                    a.Hasil,
                    a.Satuan_Hasil,
                    a.Kode_Hierarki,
                    '-' AS Posisi_Binding,

                    CASE
						WHEN a.Flag_Lanjut_Produksi = 'Y' AND a.Flag_Validasi_Formula_Produksi_BOD = 'Y' THEN 'Produksi'
						WHEN a.Flag_Lanjut_Produksi = 'Y' AND a.Flag_Validasi_Formula_Produksi_BOD IS NULL THEN 'Menunggu Validasi BOD'
						WHEN a.Flag_Selesai_Produksi = 'Y' THEN 'Selesai Produksi Komersial'
						WHEN a.Flag_Lanjut_Produksi = 'Y' THEN 'Proses Produksi Komersial'
						WHEN a.Flag_Selesai_Trial_Produksi = 'Y' THEN 'Selesai Trial Produksi'
						WHEN a.Flag_Lanjut_Trial_Produksi = 'Y' THEN 'Proses Trial Produksi'
						WHEN a.Flag_Selesai_Trial_Kitchen = 'Y' THEN 'Selesai Trial Kitchen'
						WHEN a.Flag_Lanjut_Trial_Kitchen = 'Y' THEN 'Proses Trial Kitchen'
						ELSE 'Belum Diproses'
					END AS Status_Formula,

                    '-' AS Deskripsi,
                    'Validasi' AS Validasi,

					a.Flag_Validasi_Formula_Produksi_BOD,
					a.Flag_Lanjut_Produksi,
					a.Flag_Selesai_Trial_Produksi,
					a.Flag_Lanjut_Trial_Produksi,
					a.Flag_Selesai_Trial_Kitchen,
					a.Flag_Lanjut_Trial_Kitchen

                FROM EMI_Transaksi_Formulator a

                OUTER APPLY (
                    SELECT TOP 1 *
                    FROM Barang b
                    WHERE b.Kode_Barang_Inq = a.Kode_Barang
                    ORDER BY b.Nama DESC
                ) b

                WHERE
                    a.Kode_Perusahaan = '{KodePerusahaan}'
                    AND a.Flag_Validasi = 'Y'
                    AND a.Status IS NULL

                ORDER BY a.Tanggal_Validasi DESC
            "
			Using Ds = BindingTrans(SQL)
				With Ds.Tables("MyTable")
					If .Rows.Count <> 0 Then
						For i As Integer = 0 To .Rows.Count - 1

							DGV_Formula.Rows.Add()
							DGV_Formula.Rows(i).Cells(CellParent_NoFormula).Value = .Rows(i).Item("No_Faktur")
							DGV_Formula.Rows(i).Cells(CellParent_TanggalFormula).Value = .Rows(i).Item("Tanggal_Validasi")
							DGV_Formula.Rows(i).Cells(CellParent_KdBarang).Value = .Rows(i).Item("Kode_Barang_Inq")
							DGV_Formula.Rows(i).Cells(CellParent_NmBarang).Value = .Rows(i).Item("Nama")
							DGV_Formula.Rows(i).Cells(CellParent_HPPMin).Value = Format(.Rows(i).Item("HPP_Min"), "N2")
							DGV_Formula.Rows(i).Cells(CellParent_HPPMax).Value = Format(.Rows(i).Item("HPP_Max"), "N2")
							DGV_Formula.Rows(i).Cells(CellParent_Jumlah).Value = Format(.Rows(i).Item("Hasil"), "N3")
							DGV_Formula.Rows(i).Cells(CellParent_Satuan).Value = .Rows(i).Item("Satuan_Hasil")
							DGV_Formula.Rows(i).Cells(CellParent_JenisFormula).Value = If(General_Class.CekNULL(.Rows(i).Item("Kode_Hierarki")) = "", "", .Rows(i).Item("Kode_Hierarki"))
							DGV_Formula.Rows(i).Cells(CellParent_PosisiBinding).Value = .Rows(i).Item("Posisi_Binding")
							DGV_Formula.Rows(i).Cells(CellParent_StatusFormula).Value = .Rows(i).Item("Status_Formula")
							DGV_Formula.Rows(i).Cells(CellParent_Deskripsi).Value = .Rows(i).Item("Deskripsi")
							DGV_Formula.Rows(i).Cells(CellParent_BtnValidasi).Value = .Rows(i).Item("Validasi")

							If UserPosition.Trim = "STAFF" Then
								DGV_Formula.Rows(i).Cells(CellParent_BtnValidasi) = New DataGridViewTextBoxCell()
								DGV_Formula.Rows(i).Cells(CellParent_BtnValidasi).Style.BackColor = Color.White
								DGV_Formula.Rows(i).Cells(CellParent_BtnValidasi).Style.ForeColor = Color.Black
								DGV_Formula.Rows(i).Cells(CellParent_BtnValidasi).Value = ""
								DGV_Formula.Rows(i).Cells(CellParent_BtnValidasi).ReadOnly = True
							Else
								Dim btnCell As New DataGridViewButtonCell()
								btnCell.FlatStyle = FlatStyle.Flat

								Dim currentStatus As String = .Rows(i).Item("Status_Formula").ToString.Trim

								Dim isButton As Boolean = False
								Dim userPos As String = UserPosition.Trim

								If userPos = "HEADDEPT" Then
									isButton = Status_HeadDept.Contains(currentStatus)
								ElseIf userPos = "CLEVEL" Then
									isButton = Status_BOD.Contains(currentStatus)
								End If

								If isButton Then
									DGV_Formula.Rows(i).Cells(CellParent_BtnValidasi) = btnCell
									DGV_Formula.Rows(i).Cells(CellParent_BtnValidasi).Value = .Rows(i).Item("Validasi")

									With DGV_Formula.Rows(i).Cells(CellParent_BtnValidasi).Style
										.BackColor = Color.FromArgb(15, 86, 122)
										.ForeColor = Color.White
									End With
									DGV_Formula.Rows(i).Cells(CellParent_BtnValidasi).ReadOnly = False
								Else
									DGV_Formula.Rows(i).Cells(CellParent_BtnValidasi) = New DataGridViewTextBoxCell()

									With DGV_Formula.Rows(i).Cells(CellParent_BtnValidasi).Style
										.BackColor = Color.White
										.ForeColor = Color.Black
									End With

									DGV_Formula.Rows(i).Cells(CellParent_BtnValidasi).Value = ""
									DGV_Formula.Rows(i).Cells(CellParent_BtnValidasi).ReadOnly = True
								End If

							End If

						Next
					End If
				End With
			End Using

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show("Gagal mendapatkan data formula: " & ex.Message, Judul, MessageBoxButtons.OK, MessageBoxIcon.Error)
		End Try
	End Sub

	Private Sub DGV_Formula_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGV_Formula.CellContentClick
		If e.RowIndex < 0 Then Exit Sub

		If e.ColumnIndex = CellParent_BtnValidasi Then
			Get_Data_DGV_Parent(e.RowIndex)

			Dim currentStatus As String = DgvParent_StatusFormula.Trim

			Dim userPos As String = UserPosition.Trim

			If userPos = "HEADDEPT" Then
				If Status_HeadDept.Contains(currentStatus) Then

					ShowFormHeadDept(DgvParent_NoFormula, DgvParent_StatusFormula)

				End If
			ElseIf userPos = "CLEVEL" Then
				If Status_BOD.Contains(currentStatus) Then
					ShowFormBOD(DgvParent_NoFormula)
				End If
			End If

		End If
	End Sub

	Private Sub ShowFormHeadDept(ByVal NoFaktur As String, ByVal Status As String)

		N_EMI_SD_Transaksi_Validasi_Formula_Gabungan.Kosong()
		N_EMI_SD_Transaksi_Validasi_Formula_Gabungan.TxtFormulator_NoFaktur.Text = NoFaktur
		N_EMI_SD_Transaksi_Validasi_Formula_Gabungan.StatusDariList = Status
		N_EMI_SD_Transaksi_Validasi_Formula_Gabungan.TxtFormulator_NoFaktur_Leave(DGV_Formula, New EventArgs)
		N_EMI_SD_Transaksi_Validasi_Formula_Gabungan.ShowDialog()

	End Sub

	Private Sub ShowFormBOD(ByVal NoFaktur As String)

		Dim frm As New N_EMI_SD_Transaksi_Validasi_Formula_BOD With {
			.No_Faktur = NoFaktur
		}
		frm.ShowDialog()
	End Sub

	Private Sub CekRoleUser(ByVal RoleName As String, ByVal Value As String)

		Try
			OpenConn()

			If CekButtonRole(RoleName) = "Y" Then
				UserPosition = Value
				CloseTrans()
				CloseConn()
				Exit Try
			End If

			CloseConn()
		Catch ex As Exception
			CloseConn()
			MessageBox.Show(ex.Message)
			Exit Sub
		End Try

	End Sub

End Class
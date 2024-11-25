Imports System.Data.SqlClient
Imports System.Reflection
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button

Public Class EMI_Binding_Cost_Center

    Dim arrCostCenter As New ArrayList

    Dim cell_KodeAkun As Integer = 0
    Dim cell_Keterangan As Integer = 1
    Dim cell_IndexDB As Integer = 2
    Dim cell_IndexInputUser As Integer = 3

    Private Sub EMI_Binding_Cost_Center_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub EMI_Binding_Cost_Center_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        Try
            OpenConn()
            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Btn_Simpan.Text = Base_Language.Lang_Global_Simpan

        kosong()

    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        kosong()
    End Sub

    Private Sub kosong()
        Try
            OpenConn()

            arrCostCenter.Clear()

            '==============================
            '=     CREATE KOLOM UTAMA     =
            '==============================
            DataGridView1.Columns.Clear()
            Dim column1 As New DataGridViewTextBoxColumn()
            column1.Name = "kode_akun"
            column1.HeaderText = "Kode Akun"
            column1.ReadOnly = True
            column1.Width = 150
            DataGridView1.Columns.Add(column1)

            Dim column2 As New DataGridViewTextBoxColumn()
            column2.Name = "keterangan"
            column2.HeaderText = "Keterangan"
            column2.ReadOnly = True
            column2.Width = 200
            DataGridView1.Columns.Add(column2)

            Dim column3 As New DataGridViewTextBoxColumn()
            column3.Name = "indexDB"
            column3.HeaderText = "indexDB"
            column3.ReadOnly = True
            column3.Width = 100
            column3.Visible = True
            DataGridView1.Columns.Add(column3)

            Dim column4 As New DataGridViewTextBoxColumn()
            column4.Name = "indexInput"
            column4.HeaderText = "indexInput"
            column4.ReadOnly = True
            column4.Width = 100
            column4.Visible = False
            DataGridView1.Columns.Add(column4)


            '================================
            '=     CREATE KOLOM DINAMIS     =
            '================================
            SQL = "select Id_Cost_Center, Kode_Cost_Center from EMI_Master_Cost_Center where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "order by Id_Cost_Center "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read

                    Dim jumlahHuruf As String = Dr("Kode_Cost_Center").ToString.Trim.Length
                    Dim lebarKolom As Integer = jumlahHuruf * 10

                    Dim checkBoxColumn As New DataGridViewCheckBoxColumn()
                    checkBoxColumn.Name = Dr("Kode_Cost_Center")
                    checkBoxColumn.HeaderText = Dr("Kode_Cost_Center")
                    checkBoxColumn.Width = lebarKolom
                    checkBoxColumn.TrueValue = True
                    checkBoxColumn.FalseValue = False
                    DataGridView1.Columns.Add(checkBoxColumn)

                    arrCostCenter.Add(Dr("Id_Cost_Center"))

                Loop
            End Using


            '=========================
            '=     ISI DATA AKUN     =
            '=========================
            DataGridView1.Rows.Clear()

            SQL = "select Kode_Account,Keterangan from Detail_Account where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and Flag_Biaya = 'Y' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    For i As Integer = 0 To .Rows.Count - 1
                        DataGridView1.Rows.Add(1)
                        DataGridView1.Rows(i).Cells(cell_KodeAkun).Value = .Rows(i).Item("Kode_Account")
                        DataGridView1.Rows(i).Cells(cell_Keterangan).Value = .Rows(i).Item("Keterangan")

                        Dim indexDB As String = ""
                        Dim dbCostCenters As New List(Of String)

                        '=====================
                        '=     ISI KOLOM     =
                        '=====================
                        SQL = "select id_cost_Center from Emi_Bindig_Cost_Center_Account where kode_perusahaan='" & KodePerusahaan & "' and kode_account='" & .Rows(i).Item("Kode_Account") & "' "
                        Using Dr = OpenTrans(SQL)
                            Do While Dr.Read

                                dbCostCenters.Add(Dr("id_cost_Center"))

                                For j As Integer = 0 To arrCostCenter.Count - 1
                                    If arrCostCenter(j) = Dr("id_cost_Center") Then
                                        DataGridView1.Rows(i).Cells(j + 4).Value = True
                                    End If
                                Next
                            Loop

                        End Using

                        For Each costCenter In arrCostCenter
                            If dbCostCenters.Contains(costCenter.ToString()) Then
                                indexDB &= "1"
                            Else
                                indexDB &= "0"
                            End If
                        Next

                        DataGridView1.Rows(i).Cells(2).Value = indexDB.ToString

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



    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
        'Dim flag_simpan As Boolean = False
        'For a As Integer = 0 To DataGridView1.Rows.Count - 1
        '    If flag_simpan = True Then
        '        Exit For
        '    End If

        '    'Minimal 1 Checklist
        '    For i As Integer = 0 To arrCostCenter.Count - 1
        '        If DataGridView1.Rows(a).Cells(i + 4).Value = "true" Then
        '            flag_simpan = True
        '            Exit For
        '        End If
        '    Next

        'Next

        'If flag_simpan = False Then
        '    MessageBox.Show("Tidak Ada Account yang Dicentang", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '    Exit Sub
        'End If


        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            For indexAkun As Integer = 0 To DataGridView1.Rows.Count - 1


                '===================================
                '=     PEMETAAN CHECKLIST USER     =
                '===================================
                Dim indexInput As String = ""

                For i As Integer = 0 To arrCostCenter.Count - 1

                    If DataGridView1.Rows(indexAkun).Cells(i + 4).Value = "true" Then
                        indexInput = indexInput & "1"
                    Else
                        indexInput = indexInput & "0"
                    End If
                Next


                '============================================
                '=     CEK JIKA INDEX DB != INDEX INPUT     =
                '============================================
                If Not DataGridView1.Rows(indexAkun).Cells(cell_IndexDB).Value = indexInput Then

                    SQL = "DELETE from Emi_Bindig_Cost_Center_Account where kode_perusahaan = '" & KodePerusahaan & "' and kode_account = '" & DataGridView1.Rows(indexAkun).Cells(cell_KodeAkun).Value & "' "
                    ExecuteTrans(SQL)

                    For i As Integer = 0 To arrCostCenter.Count - 1

                        '================================
                        '=     INSERT / UPDATE DATA     =
                        '================================

                        If DataGridView1.Rows(indexAkun).Cells(i + 4).Value = "true" Then

                            SQL = "insert into Emi_Bindig_Cost_Center_Account(Kode_Perusahaan, Id_Cost_Center, Kode_Account) "
                            SQL = SQL & "values('" & KodePerusahaan & "', '" & arrCostCenter(i) & "', "
                            SQL = SQL & "'" & DataGridView1.Rows(indexAkun).Cells(cell_KodeAkun).Value & "')"
                            ExecuteTrans(SQL)
                        End If

                    Next

                End If

            Next

            Cmd.Transaction.Commit()
            CloseConn()
            MessageBox.Show("Data Berhasil diSimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            kosong()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

End Class
Namespace My

	Partial Friend Class MyApplication

		Private Sub MyApplication_Startup(sender As Object, e As ApplicationServices.StartupEventArgs) Handles Me.Startup
			' Lisensi EPPlus cukup dipanggil satu kali di sini
			OfficeOpenXml.ExcelPackage.License.SetNonCommercialOrganization("Evo")
		End Sub

	End Class

End Namespace
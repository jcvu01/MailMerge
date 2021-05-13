using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using iText.Forms;
using iText.Kernel.Pdf;

namespace MailMerge
{
	public class GenerateForm
	{
		public static bool Execute(string templateFile, List<FieldValue> data, string outputFolder)
		{
			try
			{
				if (!Directory.Exists(outputFolder))
				{
					Directory.CreateDirectory(outputFolder);
				}

				var filnameColumn = data.FirstOrDefault(x =>
					string.Equals(x.FieldName, "filename", StringComparison.CurrentCultureIgnoreCase));
				var fileName = filnameColumn!=null ? $"{filnameColumn.Value}.pdf" : $"{Guid.NewGuid():N}.pdf";
				
				var reader = new PdfReader(templateFile);
				var writer = new PdfWriter($@"{outputFolder}\{fileName}");
				var pdfDoc = new PdfDocument(reader, writer);
				var form = PdfAcroForm.GetAcroForm(pdfDoc, true);
				foreach (var fieldValue in data)
				{
					if (form.GetField(fieldValue.FieldName) != null)
					{
						form.GetField(fieldValue.FieldName).SetValue(fieldValue.Value);
					}

				}
				form.FlattenFields();
				pdfDoc.Close();
				reader.Close();
				writer.Close();
				return true;
			}
			catch (Exception e)
			{
				File.AppendAllText("error.log",$"Error: {e}\n{data.First().Value}");
				return false;
			}
			
		}
	}
}

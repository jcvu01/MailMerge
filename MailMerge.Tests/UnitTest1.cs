using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace MailMerge.Tests
{
	public class UnitTest1
	{
		[Fact(Skip = "manual test")]
		public void LoadDataSuccess()
		{
			var path = @"c:\temp\mailmerge\data.txt";
			var result = LoadData.Execute(path);
			result.Count.Should().Be(1);
			result.First().Count.Should().Be(4);
			result.First()[3].FieldName.Should().Be("col4");
			result.First()[3].Value.Should().Be("4");
		}

		[Fact (Skip = "manual test")]
		public void GeneratePdf_Success()
		{
			var path = @"c:\temp\mailmerge\1.pdf";
			var data = new List<FieldValue>()
			{
				new()
				{
					FieldName = "ACCOUNT_NUMBER_MASKED",
					Value = "000011111"
				},
				new()
				{
					FieldName = "PRIMARY_PERSON_DEFAULT_ADDRESS_ADDRESS_LINE_1",
					Value = "123 main st"
				},
				new()
				{
					FieldName = "NAME",
					Value = "john doe"
				}

			};
			var result = GenerateForm.Execute(path, data,"c:\\temp\\mailmerge");
			result.Should().BeTrue();
		}
	}
}

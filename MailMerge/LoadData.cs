using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MailMerge
{
	public class LoadData
	{
		public static List<List<FieldValue>> Execute(string dataFile)
		{
			var lines = File.ReadAllLines(dataFile);
			if (lines.Length < 2)
			{
				throw new ArgumentException($"Invalid datafile {dataFile}");
			}

			var data = new List<List<FieldValue>>();
			var headers = lines.First().Split(new[] {"\t"}, StringSplitOptions.RemoveEmptyEntries);
			for (var i = 1; i < lines.Length; i++)
			{
				if (string.IsNullOrEmpty(lines[i]))
				{
					continue;
				}
				var record = ParseRecordLine(lines[i], headers);
				data.Add(record);
			}

			return data;
		}

		private static List<FieldValue> ParseRecordLine(string line, string[] headers)
		{
			var values = line.Split('\t');
			if (headers.Length > values.Length)
			{
				throw new InvalidDataException($"header and value length do not match {line} | {headers}");
			}

			return headers.Select((t, i) => new FieldValue {FieldName = t, Value = values[i]}).ToList();
		}
	}
}

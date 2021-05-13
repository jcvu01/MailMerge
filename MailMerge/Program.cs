using System;
using System.IO;

namespace MailMerge
{
	class Program
	{
		static int Main(string[] args)
		{
			if (args.Length != 3)
			{
				Console.WriteLine("Usage Syntax: MailMerge.exe [template.pdf] [DataFile.csv] [outputFolder]");
				Console.WriteLine($"Press ENTER to exit {args.Length}");
				Console.ReadLine();
				return 0;
			}

			if (!File.Exists(args[0]))
			{
				Console.WriteLine($"file {args[0]} not found");
				return 1;
			}

			if (!File.Exists(args[1]))
			{
				Console.WriteLine($"file {args[1]} not found");
				return 1;
			}
			var inputData = LoadData.Execute(args[1]);
			foreach (var record in inputData)
			{
				GenerateForm.Execute(args[0], record, args[2]);
			}

			return 0;
		}
		
	}
}

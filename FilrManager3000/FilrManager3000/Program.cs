using System;
using System.IO;
using System.Linq;
using BetterConsoleTables;

namespace FilrManager3000
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var directoryPath = ReadValue("Enter directory", @"C:\Windows\System32");
			var files = Directory.GetFiles(directoryPath);
			var fileInfos = files.Select(x => new FileInfo(x)).ToList();

			var extensions = ReadValue("Enter extensions", "");
			var extensionList = extensions.Select(x => x.).ToList();

		}

		private static string ReadValue(string label, string defaultValue)
		{
			Console.Write($"{label}, (default: {defaultValue}): ");
			string value = Console.ReadLine();
			if (value == "")
				return defaultValue;

			return value;
		}
	}
}

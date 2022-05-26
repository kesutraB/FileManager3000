using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BetterConsoleTables;

namespace FileManager3000
{
	public class ExtensionStatistics
	{
		public string Name { get; set; }
		public long Count { get; set; }
		public long Sum { get; set; }

		public ExtensionStatistics(string name, long count, long sum)
		{
			Name = name;
			Count = count;
			Sum = sum;
		}
	}
	internal class Program
	{
		public const string Separator = ";";
		static void Main(string[] args)
		{
			var directoryPath = ReadValue("Enter directory", @"C:\Windows\System32");
			DirectoryDoesntExistMessage(directoryPath);
			var extension = ReadValue("Enter extension", "");
			var files = Directory.GetFiles(directoryPath).Select(x => new FileInfo(x)).ToList();
			if (extension == string.Empty || string.IsNullOrEmpty(extension))
			{
				var distinctExtensions = files.Select(x => x.Extension.ToLower()).Distinct().ToList();
				List<ExtensionStatistics> statistics = new List<ExtensionStatistics>();
				distinctExtensions.ForEach(d =>
				{
					var filesPerExtension = files.Where(f => f.Extension.ToLower() == d).ToList();
					var filesCount = filesPerExtension.Count;
					var filesSum = filesPerExtension.Sum(f => f.Length);
					var stat = new ExtensionStatistics(d, filesCount, filesSum);
					statistics.Add(stat);
				});
			}
			
			var trimmedExtensions = extension.Split(Separator).Select(x => x.Trim()).ToList();
			var filteredFiles = files.Where(x => trimmedExtensions.Contains(x.Extension)).ToList();
			PrintFileTable(filteredFiles);
		}

		#region Checking directory validity

		private static void DirectoryDoesntExistMessage(string directory)
		{
			var checkDirectory = CheckIfDirectoryExists(directory);
			if (!checkDirectory)
			{
				Console.ForegroundColor = ConsoleColor.DarkRed;
				Console.WriteLine($"Directory: {directory} does not exist.");
			}
			Console.ResetColor();
		}

		private static bool CheckIfDirectoryExists(string directory)
		{
			if (Directory.Exists(directory))
				return true;

			return false;
		}

		#endregion

		#region Getting file size

		private static string ReturnFileSize(long size)
		{
			return $"{GetFileSize(size):F} MB";
		}

		private static long GetFileSize(long size)
		{
			return size / 1024 / 1024;
		}

		#endregion

		private static string ReadValue(string label, string defaultValue)
		{
			Console.ForegroundColor = ConsoleColor.DarkGreen;
			Console.Write($"{label}, (default: {defaultValue}): ");
			string value = Console.ReadLine();
			Console.ResetColor();
			if (value == "")
				return defaultValue;

			return value;
		}

		private static void PrintFileTable(List<FileInfo> files)
		{
			ColumnHeader[] fileTableHeaders = new[]
			{
				new ColumnHeader("No.", Alignment.Center, Alignment.Center),
				new ColumnHeader("File Name", Alignment.Center, Alignment.Center),
				new ColumnHeader("File Extension", Alignment.Center, Alignment.Center),
				new ColumnHeader("File Size", Alignment.Center, Alignment.Center)
			};

			var i = 1;
			var fileTable = new Table(fileTableHeaders);
			fileTable.Config = TableConfiguration.UnicodeAlt();

			foreach (var file in files)
			{
				fileTable.AddRow(i, file.Name, file.Extension, ReturnFileSize(file.Length));
				i++;
			}

			Console.ForegroundColor = ConsoleColor.DarkBlue;
			Console.WriteLine("\n File Table: \n");
			Console.WriteLine(fileTable.ToString());
			Console.ResetColor();
			Environment.Exit(0);
		}

		private static void PrintExtensionsTable(List<FileInfo> extensions)
		{
			ColumnHeader[] extensionsTableHeaders = new[]
			{
				new ColumnHeader("Extension Name", Alignment.Center, Alignment.Center),
				new ColumnHeader("Files with Extension", Alignment.Right, Alignment.Center),
				new ColumnHeader("Total Size of Files", Alignment.Right, Alignment.Center)
			};

			var extensionsTable = new Table 
		}
	}
}

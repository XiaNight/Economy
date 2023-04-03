using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace DataManaging
{
	public class DataManager
	{
		public static readonly string persistentPath = "";

		static DataManager()
		{
			persistentPath = Application.persistentDataPath;
		}

		public static string[] ListAllFileInFolder(string path)
		{
			string searchingPath = Path.Combine(persistentPath, path);
			Debug.Log($"Searching in {searchingPath}");
			string[] files = Directory.GetFiles(searchingPath);

			// Removing path from search
			for (int i = 0; i < files.Length; i++)
			{
				files[i] = Path.GetFileName(files[i]);
			}
			Debug.Log($"Found {files.Length} files");
			return files;
		}

		public static void WriteToFile(string path, string data)
		{
			if (path == "") throw new System.Exception("Path cannot be empty");
			string writingPath = Path.Combine(persistentPath, path);
			Debug.Log($"writing to {writingPath}");

			using StreamWriter writer = new StreamWriter(writingPath);
			writer.Write(data);
		}

		public static bool ReadFileToEnd(string path, out string readings)
		{
			string readingPath = Path.Combine(persistentPath, path);

			Debug.Log($"reading file from {path}");
			if (File.Exists(readingPath))
			{
				using StreamReader reader = new StreamReader(readingPath);
				readings = reader.ReadToEnd();
				return true;
			}
			else
			{
				readings = "";
				return false;
			}
		}
	}
}
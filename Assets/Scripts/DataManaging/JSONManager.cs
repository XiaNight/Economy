using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace DataManaging
{
	public class JSONManager
	{
		public static void SaveData<Data>(Data data, string fileName = "NewSave.json", string folder = "")
		{
			Debug.Log($"Saving JSON '{fileName}' to '{folder}'");
			string savingPath = fileName;
			if (folder != "")
			{
				savingPath = Path.Combine(folder, fileName);
			}

			string json = JsonUtility.ToJson(data, true);
			Debug.Log(json);

			DataManager.WriteToFile(savingPath, json);
		}

		public static bool LoadData<Data>(string fileName, out Data data, string folder = "")
		{
			Debug.Log($"Loading JSON '{fileName}' from '{folder}'");
			string readingPath = Path.Combine(folder, fileName);

			if (DataManager.ReadFileToEnd(readingPath, out string json))
			{
				Debug.Log(json);
				data = JsonUtility.FromJson<Data>(json);
				return true;
			}
			else
			{
				data = default;
				return false;
			}

		}
	}
}
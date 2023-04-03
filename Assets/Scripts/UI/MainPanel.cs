using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Economy;
using System;
using DataManaging;

namespace Economy.UI
{
	public class MainPanel : MonoBehaviour
	{
		[Header("UI References")]
		public DataForm createDataPanel;
		public TextfieldAlert textfieldAlert;
		public SearchAndSelectAlert loadAccountPanel;

		[Header("Account Info")]
		public Data data;

		public event UnityAction<Data> onDataLoaded;
		public event UnityAction<Data> onDataChanged;

		public void DataChanged()
		{
			onDataChanged?.Invoke(data);
		}

		public void LoadData()
		{
			loadAccountPanel.OpenNew((string value) =>
			{
				if (JSONManager.LoadData(value, out Data account))
				{
					SetData(account);
					loadAccountPanel.Close();
					return 0;
				}
				else
				{
					textfieldAlert.OpenNew((string[] values) =>
					{
						Debug.Log("Closing");
						loadAccountPanel.Close();
						return 0;
					}, title: "Account does not exist.");
					Debug.Log("Account does not exist.");
					return -1;
				}
			}, DataManager.ListAllFileInFolder(""));
		}

		public void SetData(Data account)
		{
			this.data = account;
			account.Update();
		}

		public void SaveAccount()
		{
			JSONManager.SaveData(data, data.name);
		}

		public void CreateData()
		{
			createDataPanel.OpenNew((Data acc) =>
			{
				SetData(acc);
				return 0;
			});
		}
	}
}
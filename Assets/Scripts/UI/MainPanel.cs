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

		public const string DATA_PATH = "userdata.json";

		private void Start()
		{
			LoadData();
		}

		public void DataChanged()
		{
			SaveAccount();
			onDataChanged?.Invoke(data);
		}

		public void LoadData()
		{
			if (JSONManager.LoadData(DATA_PATH, out Data account))
			{
				SetData(account);
				onDataLoaded?.Invoke(data);
			}
			else
			{
				data = new Data();
				SaveAccount();
				onDataLoaded?.Invoke(data);
			}
		}

		public void SetData(Data account)
		{
			this.data = account;
			account.Update();
		}

		public void SaveAccount()
		{
			JSONManager.SaveData(data, DATA_PATH);
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
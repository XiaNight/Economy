using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using Economy;

namespace Economy.UI
{
	public class SearchAndSelectAlert : Form<string>
	{
		public List<Button> selections = new List<Button>();
		public Transform selectionsFolder;
		public InputField searchInput;
		public GameObject selectionsPrefab;

		public void OpenNew(OnDoneHandler handler, string[] selections)
		{
			base.OpenNew(handler);
			if (selections != null)
			{
				for (int i = 0; i < selections.Length; i++)
				{
					Button button = Instantiate(selectionsPrefab).GetComponent<Button>();
					button.transform.SetParent(selectionsFolder);
					button.GetComponentInChildren<Text>().text = selections[i];
					this.selections.Add(button);
					button.onClick.AddListener(() =>
					{
						searchInput.text = button.GetComponentInChildren<Text>().text;
						Done();
					});
				}
			}
		}

		public void FilterSelections()
		{
			for (int i = 0; i < selections.Count; i++)
			{
				string selectionValue = selections[i].GetComponentInChildren<Text>().text;
				if (Regex.IsMatch(selectionValue, searchInput.text, RegexOptions.IgnoreCase))
				{
					selections[i].gameObject.SetActive(true);
				}
				else
				{
					selections[i].gameObject.SetActive(false);
				}
			}
		}

		public override void UpdateValue()
		{
			value = searchInput.text;
		}

		public override void Clear()
		{
			base.Clear();
			for (int i = selections.Count - 1; i >= 0; i--)
			{
				Destroy(selections[i].gameObject);
			}
			selections.Clear();
			searchInput.text = "";
		}

		public override void SetReadOnly(bool state)
		{
			searchInput.interactable = state;
		}
	}
}
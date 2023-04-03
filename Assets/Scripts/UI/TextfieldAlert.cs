using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Economy.UI
{
	public class TextfieldAlert : Form<string[]>
	{
		public List<InputField> inputs = new List<InputField>();
		public Transform inputFieldFolder;
		public GameObject inputFieldPrefab;
		public Text titleMessage;

		public Text doneButtonText;
		public Text cancelButtonText;

		public void SetTitle(string title)
		{
			titleMessage.text = title;
		}

		public override void UpdateValue()
		{
			value = new string[inputs.Count];
			for (int i = 0; i < value.Length; i++)
			{
				value[i] = inputs[i].text;
			}
		}

		public void OpenNew(OnDoneHandler handler = null, string[] value = null, string title = "Info")
		{
			base.OpenNew(handler);
			titleMessage.text = title;
			if (value != null)
			{
				for (int i = 0; i < value.Length; i++)
				{
					InputField inputField = Instantiate(inputFieldPrefab).GetComponent<InputField>();
					inputField.transform.SetParent(inputFieldFolder);
					inputs.Add(inputField);
					inputField.text = value[i];
				}
			}
		}

		public override void SetValue(params string[] value)
		{
			base.SetValue(value);
		}

		public override void Clear()
		{
			base.Clear();
			for (int i = inputs.Count - 1; i >= 0; i--)
			{
				Destroy(inputs[i].gameObject);
			}
			inputs.Clear();
		}

		public override void SetReadOnly(bool state)
		{
			throw new System.NotImplementedException();
		}
	}
}
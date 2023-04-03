using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Economy;
using UnityEngine.UI;

namespace Economy.UI
{
	public class DataForm : Form<Data>
	{
		public InputField nameField;
		public TextfieldAlert textfieldAlert;

		public override void Clear()
		{
			base.Clear();
			value = new Data();
			nameField.text = "";
		}

		public override void SetValue(Data value)
		{
			if (value == null) value = new Data();
			base.SetValue(value);
			nameField.text = value.name;
		}

		public override void UpdateValue()
		{
			value.name = nameField.text;
		}

		public void CreateFromJson()
		{
			textfieldAlert.OpenNew((string[] value) =>
			{
				SetValue(JsonUtility.FromJson<Data>(value[0]));
				Done(true);
				return 0;
			}, new string[1]);
		}

		public override void SetReadOnly(bool readOnly)
		{
			nameField.interactable = !readOnly;
		}
	}
}
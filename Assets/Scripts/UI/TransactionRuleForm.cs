using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Economy;

namespace Economy.UI
{
	public class TransactionRuleForm : Form<TransactionRule>
	{
		[SerializeField] private InputField nameText;
		[SerializeField] private InputField amount;
		[SerializeField] private Dropdown typeDropdown;
		[SerializeField] private DateTimeField initialDate;

		public override void SetValue(TransactionRule value)
		{
			base.SetValue(value);
			nameText.text = value.name;
			amount.text = value.amount.ToString();
			typeDropdown.value = (int)value.type;
			typeDropdown.ClearOptions();
			typeDropdown.AddOptions(new List<string>(Enum.GetNames(typeof(TransactionRule.Type))));
			initialDate.SetValue(value.initialDate);
		}

		public override void UpdateValue()
		{
			value.name = nameText.text;
			value.amount = int.Parse(amount.text);
			value.type = (TransactionRule.Type)typeDropdown.value;
			value.initialDate = initialDate.GetDateTime();
		}

		public override void SetReadOnly(bool state)
		{
			nameText.interactable = !state;
			amount.interactable = !state;
			typeDropdown.interactable = !state;
			initialDate.SetReadOnly(state);
		}
	}
}
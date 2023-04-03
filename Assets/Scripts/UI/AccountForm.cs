using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Economy;

namespace Economy.UI
{
	public class AccountForm : Form<Account>
	{
		public InputField accountNameText;
		public InputField accountAmountText;

		public override void SetValue(Account value)
		{
			if (value == null) value = new Account();
			base.SetValue(value);
			accountNameText.text = value.name;
			accountAmountText.text = value.amount.ToString();
		}
		public override void UpdateValue()
		{
			Debug.Log($"Updating value, name: {accountNameText.text}, amount: {accountAmountText.text}");
			value.name = accountNameText.text;
			value.amount = int.Parse(accountAmountText.text);
		}

		public override void SetReadOnly(bool readOnly)
		{
			accountNameText.interactable = !readOnly;
			accountAmountText.interactable = !readOnly;
		}
	}
}
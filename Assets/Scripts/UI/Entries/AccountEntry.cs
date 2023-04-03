using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Economy.UI.BaseClass;
using Economy;

namespace Economy.UI
{
	public class AccountEntry : EntryBase<Account>
	{
		[SerializeField] private Text nameText;
		[SerializeField] private Text amount;
		[SerializeField] private long uuid;

		public override void SetValue(Account value)
		{
			base.SetValue(value);
			nameText.text = value.name;
			amount.text = value.amount.ToString();
			uuid = value.uuid;
		}
	}
}
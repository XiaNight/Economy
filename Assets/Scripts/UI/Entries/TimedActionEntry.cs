using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Economy.UI.BaseClass;
using UnityEngine.UI;
using Economy;

namespace Economy.UI
{
	public class TimedActionEntry : EntryBase<TimedAction>
	{
		[SerializeField] private Text nameText;
		[SerializeField] private Text amount;
		[SerializeField] private Text frequenct;
		[SerializeField] private Text type;

		public override void SetValue(TimedAction value)
		{
			base.SetValue(value);
			nameText.text = value.name;
			amount.text = value.Amount.ToString();
			frequenct.text = value.Frequency.ToString();
			type.text = value.type.ToString();
		}
	}
}
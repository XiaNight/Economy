using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

namespace Economy
{
	[Serializable]
	public class Account
	{
		public string name;
		public int amount;
		public long uuid;

		public Account(string name = "New Account", int amount = 0, bool isMain = false)
		{
			this.name = name;
			this.amount = amount;
			if (isMain)
			{
				uuid = 0;
			}
			else
			{
				uuid = DateTime.Now.ToTimestamp();
			}
		}
	}
}
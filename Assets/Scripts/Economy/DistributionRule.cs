using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Economy
{
	[Serializable]
	public class DistributionRule
	{
		[SerializeField] private string description;
		public int value;
		public Type type;
		public long targetUUID;

		public DistributionRule(string description, int splitAmount, Type type, long targetUUID)
		{
			this.description = description;
			this.value = splitAmount;
			this.type = type;
			this.targetUUID = targetUUID;
		}

		[Serializable]
		public enum Type
		{
			Constant,
			Percentage
		}
	}
}
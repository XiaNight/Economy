using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

namespace Economy
{
	[Serializable]
	public class TimedAction : ISerializationCallbackReceiver
	{
		// Important Variables
		public int amount;
		public UpdateFrequency frequency;
		private DateTime predictedTransferDate;

		// Record Variables
		public DateTime initialDate;
		private DateTime lastUpdateDate;

		public string name;
		[SerializeField] private int counter; // how many timed actions has been made.

		public List<DistributionRule> distributionRules = new List<DistributionRule>();
		public Type type;

		public int Amount { get => amount; }
		public UpdateFrequency Frequency { get => frequency; }

		public TimedAction() : this(DateTime.Now, UpdateFrequency.Monthly, 0, Type.Income, "New Timed Action")
		{

		}

		public TimedAction(DateTime initialDate, UpdateFrequency frequency, int amount, Type type, string description = "")
		{
			DateTime localDate = DateTime.Now;

			Debug.Log(initialDate);
			Debug.Log(localDate);

			if (localDate >= initialDate)
			{
				//throw new Exception("initial date must be greater than current time");
			}

			this.amount = amount;
			this.frequency = frequency;
			this.initialDate = initialDate;
			this.name = description;
			lastUpdateDate = localDate;

			predictedTransferDate = initialDate;
			this.type = type;
		}

		public string Describe()
		{
			return $"Amount: {amount}\n" +
				$"Frequency: {GetReadableFrequency()}\n" +
				$"InitialDate: {initialDate.ToString("d")}\n" +
				$"Name: {name}\n" +
				$"Last Update: {lastUpdateDate}\n" +
				$"Next Time: {predictedTransferDate.ToString("d")}\n" +
				$"Type: {type.ToString()}\n";
		}

		public bool IsUpdated(out TimeSpan distance)
		{
			DateTime localDate = DateTime.Now;
			if (localDate < PredictedTransferDate)
			{
				distance = TimeSpan.Zero;
				return true;
			}
			distance = localDate - PredictedTransferDate;
			return false;
		}

		public void AddSplitRules(string description, int splitAmount, DistributionRule.Type type, long targetUUID)
		{
			distributionRules.Add(new DistributionRule(description, splitAmount, type, targetUUID));
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="account"></param>
		/// <returns>remaining of the transaction after distribution.</returns>
		public int InitiateTimedAction(Data account)
		{
			if (IsUpdated(out _)) return 0;
			int remaining = amount;

			foreach (DistributionRule distribution in distributionRules)
			{
				switch (distribution.type)
				{
					case DistributionRule.Type.Constant:
						account.DepositToAccount(distribution.targetUUID, distribution.value);
						remaining -= distribution.value;
						break;
					case DistributionRule.Type.Percentage:
						account.DepositToAccount(distribution.targetUUID, distribution.value * amount);
						remaining -= distribution.value * amount;
						break;
				}
			}
			predictedTransferDate = NextTransferDate(predictedTransferDate);
			account.AddReceipt(new TransactionReceipt(predictedTransferDate, amount, name));

			DateTime localDate = DateTime.Now;
			lastUpdateDate = localDate;
			counter++;

			return remaining;
		}

		public DateTime NextTransferDate(DateTime fromDate)
		{
			switch (frequency)
			{
				case UpdateFrequency.Daily:
					return initialDate.AddDays(counter);
				case UpdateFrequency.Monthly:
					return initialDate.AddMonths(counter);
				case UpdateFrequency.Yearly:
					return initialDate.AddYears(counter);
				default:
					throw new NullReferenceException("frequency cannot be null");
			}
		}

		#region Serialzation

		public DateTime PredictedTransferDate { get => predictedTransferDate; }
		[SerializeField] private long PredictedTransferDateTimeStamp;
		[SerializeField] private long InitialDateTimestamp;
		[SerializeField] private long LastUpdateTimestamp;

		public void OnBeforeSerialize()
		{
			PredictedTransferDateTimeStamp = predictedTransferDate.ToTimestamp();
			InitialDateTimestamp = initialDate.ToTimestamp();
			LastUpdateTimestamp = lastUpdateDate.ToTimestamp();
		}

		public void OnAfterDeserialize()
		{
			predictedTransferDate = PredictedTransferDateTimeStamp.DeSerialize();
			initialDate = InitialDateTimestamp.DeSerialize();
			lastUpdateDate = LastUpdateTimestamp.DeSerialize();
		}

		#endregion

		[Serializable]
		public enum Type
		{
			Income = 1,
			Expend = 2
		}

		public string GetReadableFrequency()
		{
			return frequency switch
			{
				UpdateFrequency.Daily => "Daily",
				UpdateFrequency.Monthly => $"Each month from {initialDate.Day}",
				UpdateFrequency.Yearly => $"Each year from {initialDate.Month}/{initialDate.Day}",
				_ => "None",
			};
		}

		[Serializable]
		public enum UpdateFrequency
		{
			Daily = 1,
			Monthly = 30,
			Yearly = 365
		}

		[Serializable]
		public class TransactionReceipt : ISerializationCallbackReceiver
		{
			private DateTime time;
			public int amount;
			public string description;

			// Serialization
			public long timestamp;

			public TransactionReceipt(DateTime time, int amount, string description)
			{
				this.time = time;
				this.amount = amount;
				this.description = description;
			}

			public void OnAfterDeserialize()
			{
				time = timestamp.DeSerialize();
			}

			public void OnBeforeSerialize()
			{
				timestamp = time.Serialize();
			}
		}
	}
}
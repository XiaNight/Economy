using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Economy
{
	[Serializable]
	public class Data
	{
		public string name = "New Account";
		public List<TimedAction> timedActions = new List<TimedAction>();
		public List<Account> accounts = new List<Account>();
		public List<TimedAction.TransactionReceipt> receipts = new List<TimedAction.TransactionReceipt>();

		public Data(string name = "New Account")
		{
			this.name = name;
			accounts.Add(new Account("Main", 0, true));
		}

		public void Update()
		{
			int whilebreaker = 0;
			while (IsAllTransactionUpdated() == false)
			{
				TimedAction rule = GetNextTransactionRule();
				int amount = rule.Amount;
				int remaining = rule.InitiateTimedAction(this);

				switch (rule.type)
				{
					case TimedAction.Type.Income:
						DepositToAccount(0, remaining);
						break;
					case TimedAction.Type.Expend:
						WithdrawWithAccount(0, amount, true);
						break;
				}
				if (whilebreaker++ > 1000)
				{
					throw new Exception("Exceed maximum while loop");
				}
			}
		}

		public void AddReceipt(TimedAction.TransactionReceipt receipt)
		{
			receipts.Add(receipt);
		}

		public TimedAction GetNextTransactionRule()
		{
			if (IsAllTransactionUpdated()) return null;
			TimedAction mostDistancedRule = null;
			TimeSpan distance = TimeSpan.Zero;
			foreach (TimedAction rule in timedActions)
			{
				if (!rule.IsUpdated(out TimeSpan span))
				{
					if (span > distance)
					{
						mostDistancedRule = rule;
						distance = span;
					}
				}
			}
			return mostDistancedRule;
		}

		public bool IsAllTransactionUpdated()
		{
			foreach (TimedAction rule in timedActions)
			{
				if (rule.IsUpdated(out _) == false) return false;
			}
			return true;
		}

		public void DepositToAccount(long uuid, int amount)
		{
			if (GetAccount(uuid, out Account account))
			{
				account.amount += amount;
			}
			else
			{
				throw new Exception($"Account not found: {uuid}");
			}
		}

		public string Describe()
		{
			string output = "";
			string prefix = $"Name: {name}\n";
			output += prefix;
			for (int i = 0; i < timedActions.Count; i++)
			{
				output += $"{{{timedActions[i].Describe()}}}\n";
			}
			return output;
		}

		/// <summary>
		/// Withdraw money from a account
		/// </summary>
		/// <param name="uuid"></param>
		/// <param name="amount"></param>
		/// <param name="force"></param>
		/// <returns>if not forced, returns false if account have not enough money, 
		/// if forced, returns true id account is not empty</returns>
		public bool WithdrawWithAccount(long uuid, int amount, bool force = false)
		{
			if (GetAccount(uuid, out Account account))
			{
				if (force == false)
				{
					if (account.amount - amount < 0)
					{
						return false;
					}
					account.amount -= amount;
					return true;
				}
				else
				{
					account.amount -= amount;
					return account.amount >= 0;
				}
			}
			else
			{
				throw new Exception($"Depo not found: {uuid}");
			}
		}

		public bool GetAccount(long uuid, out Account output)
		{
			foreach (Account account in accounts)
			{
				if (account.uuid == uuid)
				{
					output = account;
					return true;
				}
			}
			output = null;
			return false;
		}

		public Account Mainaccount { get { GetAccount(0, out Account account); return account; } }

	}
}
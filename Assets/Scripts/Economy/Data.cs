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
		public List<TransactionRule> transactionRules = new List<TransactionRule>();
		public List<Account> accounts = new List<Account>();
		public List<TransactionRule.TransactionReceipt> receipts = new List<TransactionRule.TransactionReceipt>();

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
				TransactionRule rule = GetNextTransactionRule();
				int amount = rule.Amount;
				int remaining = rule.InitiateTransaction(this);

				switch (rule.type)
				{
					case TransactionRule.Type.Income:
						DepositToAccount(0, remaining);
						break;
					case TransactionRule.Type.Expend:
						WithdrawWithAccount(0, amount, true);
						break;
				}
				if (whilebreaker++ > 1000)
				{
					throw new Exception("Exceed maximum while loop");
				}
			}
		}

		public void AddReceipt(TransactionRule.TransactionReceipt receipt)
		{
			receipts.Add(receipt);
		}

		public TransactionRule GetNextTransactionRule()
		{
			if (IsAllTransactionUpdated()) return null;
			TransactionRule mostDistancedRule = null;
			TimeSpan distance = TimeSpan.Zero;
			foreach (TransactionRule rule in transactionRules)
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
			foreach (TransactionRule rule in transactionRules)
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
			for (int i = 0; i < transactionRules.Count; i++)
			{
				output += $"{{{transactionRules[i].Describe()}}}\n";
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
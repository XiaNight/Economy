using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace Economy.UI
{
	public class TransactionRuleList : DataReferenceBehaviour
	{
		[Header("Transaction Rule")]
		[SerializeField] protected Transform transactionContainer;
		[SerializeField] protected TransactionEntry transactionEntryPrefab;
		[SerializeField] protected TransactionRuleForm transactionForm;
		[SerializeField] protected Button addBtn;
		[SerializeField] protected List<TransactionEntry> spawnedRuleEntries = new List<TransactionEntry>();

		protected override void Awake()
		{
			base.Awake();
			addBtn.onClick.AddListener(AddTransaction);
		}

		protected override void Setup(Data data)
		{
			// Transaction Rule
			for (int i = spawnedRuleEntries.Count - 1; i >= 0; i--)
			{
				Destroy(spawnedRuleEntries[i].gameObject);
				spawnedRuleEntries.RemoveAt(i);
			}
			for (int i = 0; i < data.transactionRules.Count; i++)
			{
				TransactionEntry newEntry = Instantiate(transactionEntryPrefab).GetComponent<TransactionEntry>();
				newEntry.transform.SetParent(transactionContainer);
				newEntry.SetValue(data.transactionRules[i]);
				newEntry.onClick += EditTransaction;
				spawnedRuleEntries.Add(newEntry);
			}
		}

		protected void AddTransaction()
		{
			transactionForm.OpenNew((TransactionRule rule) =>
			{
				mainPanelReference.data.transactionRules.Add(rule);
				return 0;
			}, new TransactionRule());
		}

		protected void EditTransaction(TransactionRule rule)
		{
			transactionForm.OpenNew((TransactionRule rule) =>
			{
				return 0;
			}, rule);
		}
	}
}
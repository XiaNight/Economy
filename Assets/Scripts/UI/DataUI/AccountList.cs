using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace Economy.UI
{
	public class AccountList : DataReferenceBehaviour
	{
		[SerializeField] protected Transform accountContainer;
		[SerializeField] protected AccountEntry accountEntryPrefab;
		[SerializeField] protected AccountForm accountForm;
		[SerializeField] protected Button addAccountBtn;
		[SerializeField] protected List<AccountEntry> spawnedAccountEntries = new List<AccountEntry>();

		protected override void Awake()
		{
			base.Awake();
			addAccountBtn.onClick.AddListener(AddAccount);
		}

		protected override void Setup(Data data)
		{
			// Account
			for (int i = spawnedAccountEntries.Count - 1; i >= 0; i--)
			{
				Destroy(spawnedAccountEntries[i].gameObject);
				spawnedAccountEntries.RemoveAt(i);
			}
			for (int i = 0; i < data.accounts.Count; i++)
			{
				Debug.Log(data.accounts[i].name);
				AccountEntry newEntry = Instantiate(accountEntryPrefab).GetComponent<AccountEntry>();
				newEntry.transform.SetParent(accountContainer);
				newEntry.SetValue(data.accounts[i]);
				newEntry.onClick += EditAccount;
				spawnedAccountEntries.Add(newEntry);
			}
		}

		protected void AddAccount()
		{
			accountForm.OpenNew((Account account) =>
			{
				mainPanelReference.data.accounts.Add(account);
				return 0;
			}, new Account());
		}

		protected void EditAccount(Account account)
		{
			accountForm.OpenNew((Account account) =>
			{
				return 0;
			}, account);
		}
	}
}
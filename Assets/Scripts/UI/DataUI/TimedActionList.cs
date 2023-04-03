using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace Economy.UI
{
	public class TimedActionList : DataReferenceBehaviour
	{
		[Header("Timed Action Rule")]
		[SerializeField] protected Transform timedActionContainer;
		[SerializeField] protected TimedActionEntry timedActionEntryPrefab;
		[SerializeField] protected TimedActionForm timedActionForm;
		[SerializeField] protected Button addBtn;
		[SerializeField] protected List<TimedActionEntry> spawnedEntries = new List<TimedActionEntry>();

		protected override void Awake()
		{
			base.Awake();
			addBtn.onClick.AddListener(AddTimedAction);
		}

		protected override void Setup(Data data)
		{
			// Transaction Rule
			for (int i = spawnedEntries.Count - 1; i >= 0; i--)
			{
				Destroy(spawnedEntries[i].gameObject);
				spawnedEntries.RemoveAt(i);
			}
			for (int i = 0; i < data.timedActions.Count; i++)
			{
				TimedActionEntry newEntry = Instantiate(timedActionEntryPrefab).GetComponent<TimedActionEntry>();
				newEntry.transform.SetParent(timedActionContainer);
				newEntry.SetValue(data.timedActions[i]);
				newEntry.onClick += EditTimedAction;
				spawnedEntries.Add(newEntry);
			}
		}

		protected void AddTimedAction()
		{
			timedActionForm.OpenNew((TimedAction timedAction) =>
			{
				mainPanelReference.data.timedActions.Add(timedAction);
				return 0;
			}, new TimedAction());
		}

		protected void EditTimedAction(TimedAction rule)
		{
			timedActionForm.OpenNew((TimedAction rule) =>
			{
				return 0;
			}, rule);
		}
	}
}
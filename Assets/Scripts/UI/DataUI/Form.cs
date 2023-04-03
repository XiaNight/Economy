using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Economy.UI.BaseClass;

namespace Economy.UI
{
	public abstract class Form<T> : DataReferenceBehaviour, IUpdatableUIValue, ISetableUIValue<T>
	{
		public delegate int OnDoneHandler(T post);
		public event OnDoneHandler OnDone;
		public event UnityAction OnCancel;
		[SerializeField] private Button doneBtn;

		public T value;

		protected override void Awake()
		{
			base.Awake();
			doneBtn.onClick.AddListener(Done);
		}

		public void Done()
		{
			Done(false);
		}

		public void Done(bool skipUpdate = false)
		{
			if (skipUpdate == false) UpdateValue();
			if (OnDone?.Invoke(value) == 0)
			{
				Close();
			}
			mainPanelReference.DataChanged();
		}

		public void Cancel()
		{
			OnCancel?.Invoke();
			Close();
		}

		public virtual void OpenNew(OnDoneHandler handler = null, T value = default, bool readOnly = false)
		{
			gameObject.SetActive(true);
			Clear();
			SetValue(value);
			SetReadOnly(readOnly);
			OnDone += handler;
			Debug.Log("Opening new form");
		}

		public virtual void SetValue(T value)
		{
			this.value = value;
		}

		public abstract void UpdateValue();

		public virtual void Close()
		{
			gameObject.SetActive(false);
			Clear();
		}

		public abstract void SetReadOnly(bool readOnly);

		public virtual void Clear()
		{
			value = default;
			OnDone = null;
			OnCancel = null;
			Debug.Log("Clearing form");
		}
	}
}
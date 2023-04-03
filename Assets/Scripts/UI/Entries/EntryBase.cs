using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Economy.UI.BaseClass;
using Economy;

namespace Economy.UI
{
	public abstract class EntryBase<T> : MonoBehaviour, ISetableUIValue<T>
	{
		[SerializeField] Button button;
		public event UnityAction<T> onClick;
		private T currentValue;
		protected virtual void Awake()
		{
			button.onClick.AddListener(() => onClick?.Invoke(currentValue));
		}
		public virtual void SetValue(T value)
		{
			currentValue = value;
		}
	}
}
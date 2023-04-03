using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Economy.UI.BaseClass
{
	public interface ISetableUIValue<T>
	{
		public abstract void SetValue(T value);
	}
}
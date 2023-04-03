using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace Economy.UI
{
	public abstract class DataReferenceBehaviour : MonoBehaviour
	{
		protected MainPanel mainPanelReference;

		protected virtual void Awake()
		{
			mainPanelReference = FindObjectOfType<MainPanel>();
			mainPanelReference.onDataLoaded += Setup;
			mainPanelReference.onDataChanged += Setup;
		}

		protected virtual void Setup(Data data) { }
	}
}
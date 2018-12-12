using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.UI;

namespace GameCore
{
	[RequireComponent (typeof (ExtensionsToggleGroup))]
	public class TabsOnTogglesExtensionsController : MonoBehaviour
	{
		[SerializeField] ExtensionsToggle lastToggle;
		[SerializeField] List<ExtensionsToggle> toggles;
		private void Start ()
		{
			if (lastToggle)
				lastToggle.IsOn = true;
		}
		public void OnChangeSelectedItem (int index)
		{
			// group.SetAllTogglesOff ();
			lastToggle.IsOn = false;
			int i = index % toggles.Count;
			Debug.LogFormat ("i = {0}", i);
			lastToggle = toggles[i];
			lastToggle.IsOn = true;
		}

		public void OnVaueChangeToggle (ExtensionsToggle toggle)
		{

			bool isLast = toggle.Equals (lastToggle);
			if (isLast) { lastToggle.IsOn = true; return; }
			int indexOfSelected = toggles.IndexOf (toggle);
			lastToggle = toggle;
		}
		private void OnValidate ()
		{
			toggles = GetComponentsInChildren<ExtensionsToggle> ().ToList ();
		}
	}
}

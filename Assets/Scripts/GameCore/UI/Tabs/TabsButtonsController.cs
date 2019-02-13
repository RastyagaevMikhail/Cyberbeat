﻿using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace GameCore
{
	public class TabsButtonsController : MonoBehaviour
	{
		List<TabButton> tabs = new List<TabButton> ();
		[SerializeField] TabButton lastSelcted;
		private void Start ()
		{
			if (lastSelcted) lastSelcted.Select();
		}
		public bool IsSelected (TabButton tabButton)
		{
			if (lastSelcted)
				return lastSelcted.Equals (tabButton);

			return false;
		}

		public void Swith (TabButton tabButton)
		{
			if (lastSelcted) lastSelcted.DeSelect ();
			lastSelcted = tabButton;
			lastSelcted.Select ();
		}
		public void RegistryTabButton (TabButton tabButton)
		{
			if (!tabs.Contains (tabButton))
				tabs.Add (tabButton);
		}

		public void UnRegistryTabButton (TabButton tabButton)
		{
			if (tabs.Contains (tabButton))
			{
				tabs.Remove (tabButton);
				tabButton.Active = tabButton.Equals (lastSelcted);
			}
		}

	}
}

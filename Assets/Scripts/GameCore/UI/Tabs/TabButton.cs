﻿using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace GameCore
{
	public class TabButton : MonoBehaviour, IPointerClickHandler
	{
		[SerializeField] ToggleMode toggleMode;
		[SerializeField] TabsButtonsController controller;
		[SerializeField] GameObject OnActiveObject;
		[SerializeField] GameObject OnDontActiveObject;

		[SerializeField] UnityEvent onSelect;

		[SerializeField] UnityEvent onDeSelect;
		private void OnEnable ()
		{
			if (controller)
				controller.RegistryTabButton (this);
		}
		private void OnDisable ()
		{
			if (controller)
				controller.UnRegistryTabButton (this);
		}

		public bool Active
		{
			get { return OnActiveObject.activeInHierarchy; }
			set
			{
				if (toggleMode == ToggleMode.Switch)
					OnDontActiveObject.SetActive (!value);
				OnActiveObject.SetActive (value);
			}
		}

		public void OnPointerClick (PointerEventData eventData)
		{
			if (controller.IsSelected (this)) return;
			controller.Swith (this);
		}
		public void DeSelect ()
		{
			Active = false;
			onDeSelect.Invoke ();
		}
		public void Select ()
		{
			Active = true;
			onSelect.Invoke ();
		}
	}

	public enum ToggleMode
	{
		Switch,
		Activate
	}
}

﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.SceneManagement;
namespace GameCore
{

	public class StartInitialize : MonoBehaviour
	{
		public bool consentValue { get { return PlayerPrefs.GetInt ("consentValue", 0) == 1; } set { PlayerPrefs.SetInt ("consentValue", value ? 1 : 0); } }

		[SerializeField] GameObject GDPR_PanelInfo;
		[SerializeField] BoolVariable tutorialComplited;
		public NotificationManager notificationManager { get { return NotificationManager.instance; } }
		void Start ()
		{
			if (!tutorialComplited.Value)
			{
				LoadTutorial ();
				return;
			}

			GDPR_PanelInfo.SetActive (!consentValue);

			if (consentValue)
				Init ();
		}

		public void Init ()
		{
			var initalizableData = Resources.LoadAll<ScriptableObject> ("Data")
				.ToList ()
				.FindAll (so => so is IStartInitializationData)
				.Select (so => so as IStartInitializationData);

			foreach (var data in initalizableData)
				data.Init (consentValue);

			LoadGame ();
		}

		void LoadGame ()
		{
			SceneManager.LoadScene ("Menu");
		}
		void LoadTutorial ()
		{
			SceneManager.LoadScene ("Tutorial");
		}
	}
}

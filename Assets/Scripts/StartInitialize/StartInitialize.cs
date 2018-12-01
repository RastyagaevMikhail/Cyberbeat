using AppodealAds.Unity.Api;

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
namespace GameCore
{

	public class StartInitialize : MonoBehaviour
	{
		public bool consentValue { get { return PlayerPrefs.GetInt ("consentValue", 0) == 1; } set { PlayerPrefs.SetInt ("consentValue", value ? 1 : 0); } }

		[SerializeField] GameObject GDPR_PanelInfo;
		public NotificationManager notificationManager { get { return NotificationManager.instance; } }
		void Start ()
		{
			GDPR_PanelInfo.SetActive (!consentValue);

			InitAds ();
			notificationManager.Init();

		}

		public void InitAds ()
		{
			string appKey = "3c672f0ffdbf98b0bac46b6d2009945882ca20f1e7a2dc3a"; //DevSkim: ignore DS173237 
			Appodeal.initialize (appKey, Appodeal.INTERSTITIAL | Appodeal.REWARDED_VIDEO, consentValue);
			Invoke ("LoadGame", 1f);
		}

		void LoadGame ()
		{
			SceneManager.LoadScene ("Loading");
		}
	}
}

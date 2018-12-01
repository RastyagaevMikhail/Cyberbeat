using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace GameCore
{
	public class AdsController : MonoBehaviour, IRewardedVideoAdListener
	{
		private static AdsController _instance = null;
		public static AdsController instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = GameObject.FindObjectOfType<AdsController> ();
					if (_instance == null) _instance = new GameObject ("AdsController").AddComponent<AdsController> ();
				}
				return _instance;
			}
		}
		public bool isLoadedRewardVideo
		{
			get
			{
#if UNITY_EDITOR
				return true;
#else
				return Appodeal.isLoaded (Appodeal.REWARDED_VIDEO);
#endif
			}
		}
		public bool isLoadedInterstitial
		{
			get
			{
#if UNITY_EDITOR
				return true;
#else
				return Appodeal.isLoaded (Appodeal.INTERSTITIAL);
#endif
			}
		}
		public bool internetNotReachable
		{
			get
			{
#if UNITY_EDITOR
				return false;
#else
				return Application.internetReachability == NetworkReachability.NotReachable;
#endif    
			}
		}
		private void Start ()
		{
			Appodeal.setRewardedVideoCallbacks (this);
		}
		Action<double, string> _onVideShown;
		public Action OnRewardedVideoLoaded;

		public void ShowRewardVideo (Action<double, string> OnVideoShown = null)
		{
			_onVideShown = OnVideoShown;
#if UNITY_EDITOR
			onRewardedVideoFinished (0, "");
#else
			Appodeal.show (Appodeal.REWARDED_VIDEO);
#endif
		}

		public void onRewardedVideoClosed (bool finished)
		{
			// Debug.LogFormat ("onRewardedVideoClosed finished= {0}", finished);
		}

		public void onRewardedVideoExpired ()
		{
			// Debug.Log ("onRewardedVideoExpired");
		}

		public void onRewardedVideoFailedToLoad ()
		{
			// Debug.Log ("onRewardedVideoFailedToLoad");
		}

		public void onRewardedVideoFinished (double amount, string name)
		{
			// Debug.LogFormat ("onRewardedVideoFinished = {0} : {1} ", amount, name);
			// Debug.LogFormat ("_onVideShown = {0}", _onVideShown);
			if (_onVideShown != null) _onVideShown (amount, name);
		}

		public void onRewardedVideoLoaded (bool precache)
		{
			if (OnRewardedVideoLoaded != null) OnRewardedVideoLoaded ();
			// Debug.LogFormat ("onRewardedVideoLoaded prechache = {0}", precache);

		}
		public void onRewardedVideoShown ()
		{
			// Debug.Log ("onRewardedVideoShown");
			// Debug.LogFormat ("_onVideShown = {0}", _onVideShown);
		}
	}
}

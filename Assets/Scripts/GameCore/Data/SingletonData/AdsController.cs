using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;

using Sirenix.OdinInspector;

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace GameCore
{
	public partial class AdsController : SingletonData<AdsController>, IRewardedVideoAdListener, IStartInitializationData
	{
#if UNITY_EDITOR
		[UnityEditor.MenuItem ("Game/Data/ADS")] public static void Select () { UnityEditor.Selection.activeObject = instance; }
		public override void ResetDefault () { }
		public override void InitOnCreate () { }
#endif

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

		[Multiline]
		[SerializeField] 
		string appKey;

		[SerializeField]
		[EnumToggleButtons]
		
		AdType adType = AdType.INTERSTITIAL | AdType.REWARDED_VIDEO;
		public void Init (bool consentValue)
		{
			Appodeal.initialize (appKey, (int) adType, consentValue);
			Appodeal.setRewardedVideoCallbacks (this);
		}
		Action<double, string> _onVideShown;
		[NonSerialized] public Action OnRewardedVideoLoaded;

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
			[System.Flags]
        private enum AdType
		{
			INTERSTITIAL = Appodeal.INTERSTITIAL,
			REWARDED_VIDEO = Appodeal.REWARDED_VIDEO,
			BANNER = Appodeal.BANNER,
			BANNER_BOTTOM = Appodeal.BANNER_BOTTOM,
			BANNER_HORIZONTAL_CENTER = Appodeal.BANNER_HORIZONTAL_CENTER,
			BANNER_HORIZONTAL_LEFT = Appodeal.BANNER_HORIZONTAL_LEFT,
			BANNER_HORIZONTAL_RIGHT = Appodeal.BANNER_HORIZONTAL_RIGHT,
			BANNER_HORIZONTAL_SMART = Appodeal.BANNER_HORIZONTAL_SMART,
			BANNER_TOP = Appodeal.BANNER_TOP,
			BANNER_VIEW = Appodeal.BANNER_VIEW,
			MREC = Appodeal.MREC,
			NON_SKIPPABLE_VIDEO = Appodeal.NON_SKIPPABLE_VIDEO,
		}
	}
	
}

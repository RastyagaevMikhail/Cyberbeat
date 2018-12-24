using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;

using Sirenix.OdinInspector;

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace GameCore
{
	public partial class AdsController : SingletonData<AdsController>, IRewardedVideoAdListener, IInterstitialAdListener, IStartInitializationData
	{
#if UNITY_EDITOR
		[UnityEditor.MenuItem ("Game/Data/ADS")] public static void Select () { UnityEditor.Selection.activeObject = instance; }
		public override void ResetDefault () { }
		public override void InitOnCreate ()
		{
			CreateNoAdsVariable ();
		}

		[Button]
		private void CreateNoAdsVariable ()
		{
			const string NoAdsVariablePath = "Assets/Data/Variables/AdsController/NoAds.asset";
			noAds = Tools.GetAssetAtPath<BoolVariable> (NoAdsVariablePath);
			if (noAds == null)
			{
				noAds = ScriptableObject.CreateInstance<BoolVariable> ();
				noAds.CreateAsset (NoAdsVariablePath);
				noAds.isSavable = true;
			}
		}
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

		public void ActivateNoAds ()
		{
			NoAds = true;
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

		public Action OnIntrastitialShown;

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
		[SerializeField] BoolVariable noAds;
		private bool NoAds { get { return noAds.Value; } set { noAds.Value = value; } }

		public void ShowRewardVideo (GameEventRewardVideo OnVideoShown)
		{
			Debug.Log ("ShowRewardVideo");
			Debug.LogFormat (OnVideoShown, "OnVideoShown = {0}", OnVideoShown);
			ShowRewardVideo (OnVideoShown.Raise);
		}
		public void ShowRewardVideo (RewardVideoUnityEvent OnVideoShown)
		{

			ShowRewardVideo (OnVideoShown.Invoke);
		}
		public void ShowRewardVideo (GameEvent OnVideoShown)
		{
			ShowRewardVideo ((a, n) => OnVideoShown.Raise ());
		}
		public void ShowRewardVideo (Action<double, string> OnVideoShown = null)
		{

			_onVideShown = OnVideoShown;
			Debug.Log ("_onVideShown");
#if UNITY_EDITOR
			onRewardedVideoFinished (0, "");
#else
			Appodeal.show (Appodeal.REWARDED_VIDEO);
#endif
		}

		public void ShowIntrastitial ()
		{
			ShowIntrastitial ("default", null);
		}

		public void ShowIntrastitial (GameEvent GE)
		{
			ShowIntrastitial ("default", GE.Raise);
		}
		public void ShowIntrastitial (string playsment = "default")
		{
			ShowIntrastitial (playsment, null);
		}
		public void ShowIntrastitial (string playsment = "default", Action onIntrastitialShown = null)
		{
			OnIntrastitialShown = onIntrastitialShown;
#if UNITY_EDITOR
			// Appodeal.showTestScreen ();
			onInterstitialShown ();
#else
			Appodeal.show (Appodeal.INTERSTITIAL, playsment);
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
			Debug.LogFormat ("onRewardedVideoFinished = {0} : {1} ", amount, name);
			Debug.LogFormat ("_onVideShown = {0}", _onVideShown);
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

		public void onInterstitialLoaded (bool isPrecache)
		{

		}

		public void onInterstitialFailedToLoad () { }

		public void onInterstitialShown ()
		{
			if (OnIntrastitialShown != null)
			{
				OnIntrastitialShown ();
			}
		}

		public void onInterstitialClosed () { }

		public void onInterstitialClicked () { }
		public void onInterstitialExpired () { }

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

	[Serializable] public class RewardVideoUnityEvent : UnityEvent<double, string> { }

}

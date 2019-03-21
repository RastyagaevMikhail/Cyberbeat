using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Monetization;
namespace GameCore
{
    [CreateAssetMenu (fileName = "IsReadyCondition", menuName = "GameCore/Condition/UnityAds/PlacmentIsReady")]
	public class UnityAdsPlacementIsReadyCondition : ACondition
	{
        [SerializeField] string placemnetId = "INTERSTITIAL";
		public override bool Value => Monetization.IsReady(placemnetId);
	}
}

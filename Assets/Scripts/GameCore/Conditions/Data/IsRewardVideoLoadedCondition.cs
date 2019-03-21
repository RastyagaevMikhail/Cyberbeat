using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace GameCore
{
    [CreateAssetMenu (menuName = "GameCore/Condition/IsRewardVideoLoaded")]
    public class IsRewardVideoLoadedCondition : ACondition
    {
        [SerializeField] AdsController adsController;
        public override bool Value => adsController.IsLoaded_REWARDED_VIDEO;
    }
}

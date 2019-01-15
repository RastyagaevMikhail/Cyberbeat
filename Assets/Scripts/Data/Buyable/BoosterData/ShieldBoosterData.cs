using UnityEngine;
namespace CyberBeat
{
    [CreateAssetMenu (fileName = "Shield", menuName = "CyberBeat/BoosterData/Shield")]
    public class ShieldBoosterData : BoosterData
    {
        public override bool Apply (ColorBrick brick, bool equalColor)
        {
            OnApply.Invoke(this);
            // Debug.Log("Apply.ShieldBoosterData");
            return true;
        }


    }
}

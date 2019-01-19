using UnityEngine;
namespace CyberBeat
{
    [CreateAssetMenu (fileName = "Life", menuName = "CyberBeat/BoosterData/Life")]
    public class LifeBoosterData : BoosterData
    {
        public override bool Apply (ColorBrick brick, bool equalColor)
        {
            OnApply.Invoke (this);
            // Debug.Log ("Apply.LifeBoosterData");

            if (!equalColor)
                DeActivate ();
                
            return true;
        }

    }
}

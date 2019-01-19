using UnityEngine;
namespace CyberBeat
{
    [CreateAssetMenu (fileName = "Shield", menuName = "CyberBeat/BoosterData/Shield")]
    public class ShieldBoosterData : BoosterData
    {
        public override void Apply (ColorBrick brick)
        {
            // Debug.Log("Apply.ShieldBoosterData");
            brick.Death();
        }
    }
}

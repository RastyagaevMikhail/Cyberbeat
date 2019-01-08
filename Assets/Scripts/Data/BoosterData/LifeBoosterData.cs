using UnityEngine;
namespace CyberBeat
{
    [CreateAssetMenu (fileName = "Life", menuName = "CyberBeat/BoosterData/Life")]
    public class LifeBoosterData : BoosterData
    {
        public override void Apply (ColorBrick brick)
        {
            Debug.Log ("Apply.LifeBoosterData");
            brick.Death ();
            DeActivate ();
        }
    }
}

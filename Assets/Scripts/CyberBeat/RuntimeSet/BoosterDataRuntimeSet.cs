using GameCore;

using UnityEngine;
using UnityEngine.Events;
namespace CyberBeat
{
[CreateAssetMenu(
    fileName = "BoosterDataRuntimeSet.asset",
    menuName = "CyberBeat/RuntimeSet/BoosterData")]
    public class BoosterDataRuntimeSet : RuntimeSet<BoosterData> 
    {
        [SerializeField] UnityEventBoosterData onAddComplete;
        protected override UnityEvent<BoosterData> OnAddComplete
        {
            get
            {
                return onRemoveComplete;
            }
        }
        [SerializeField] UnityEventBoosterData onRemoveComplete;
        protected override UnityEvent<BoosterData> OnRemoveComplete
        {
            get
            {
                return onRemoveComplete;
            }
        }
     }
}

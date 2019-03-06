using GameCore;

using UnityEngine;
using UnityEngine.Events;
namespace CyberBeat
{
[CreateAssetMenu(
    fileName = "ColorInfoRuntimeSetRuntimeSet.asset",
    menuName = "CyberBeat/RuntimeSet/ColorInfoRuntimeSet")]
    public class ColorInfoRuntimeSetRuntimeSet : RuntimeSet<ColorInfoRuntimeSet> 
    {
        [SerializeField] UnityEventColorInfoRuntimeSet onAddComplete;
        protected override UnityEvent<ColorInfoRuntimeSet> OnAddComplete
        {
            get
            {
                return onAddComplete;
            }
        }
        [SerializeField] UnityEventColorInfoRuntimeSet onRemoveComplete;
        protected override UnityEvent<ColorInfoRuntimeSet> OnRemoveComplete
        {
            get
            {
                return onRemoveComplete;
            }
        }
     }
}

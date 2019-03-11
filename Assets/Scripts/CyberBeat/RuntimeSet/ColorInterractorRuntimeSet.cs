using GameCore;

using UnityEngine;
using UnityEngine.Events;
namespace CyberBeat
{
[CreateAssetMenu(
    fileName = "ColorInterractorRuntimeSet.asset",
    menuName = "CyberBeat/RuntimeSet/ColorInterractor")]
    public class ColorInterractorRuntimeSet : RuntimeSet<ColorInterractor> 
    {
        [SerializeField] UnityEventColorInterractor onAddComplete;
        protected override UnityEvent<ColorInterractor> OnAddComplete
        {
            get
            {
                return onAddComplete;
            }
        }
        [SerializeField] UnityEventColorInterractor onRemoveComplete;
        protected override UnityEvent<ColorInterractor> OnRemoveComplete
        {
            get
            {
                return onRemoveComplete;
            }
        }
     }
}

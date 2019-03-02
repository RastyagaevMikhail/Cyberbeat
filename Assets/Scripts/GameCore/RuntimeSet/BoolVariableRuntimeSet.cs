using GameCore;

using UnityEngine;
using UnityEngine.Events;
namespace GameCore
{
[CreateAssetMenu(
    fileName = "BoolVariableRuntimeSet.asset",
    menuName = "GameCore/RuntimeSet/BoolVariable")]
    public class BoolVariableRuntimeSet : RuntimeSet<BoolVariable> 
    {
        [SerializeField] UnityEventBoolVariable onAddComplete;
        protected override UnityEvent<BoolVariable> OnAddComplete
        {
            get
            {
                return onAddComplete;
            }
        }
        [SerializeField] UnityEventBoolVariable onRemoveComplete;
        protected override UnityEvent<BoolVariable> OnRemoveComplete
        {
            get
            {
                return onRemoveComplete;
            }
        }
     }
}

using GameCore;

using UnityEngine;
using UnityEngine.Events;
namespace GameCore
{
[CreateAssetMenu(
    fileName = "TransformRuntimeSet.asset",
    menuName = "GameCore/RuntimeSet/Transform")]
    public class TransformRuntimeSet : RuntimeSet<Transform> 
    {
        [SerializeField] UnityEventTransform onAddComplete;
        protected override UnityEvent<Transform> OnAddComplete
        {
            get
            {
                return onRemoveComplete;
            }
        }
        [SerializeField] UnityEventTransform onRemoveComplete;
        protected override UnityEvent<Transform> OnRemoveComplete
        {
            get
            {
                return onRemoveComplete;
            }
        }
     }
}

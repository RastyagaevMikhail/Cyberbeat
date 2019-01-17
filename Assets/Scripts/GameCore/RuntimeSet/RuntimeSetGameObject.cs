using GameCore;

using UnityEngine;
using UnityEngine.Events;
namespace GameCore
{
[CreateAssetMenu(
    fileName = "GameObjectRuntimeSet.asset",
    menuName = "GameCore/RuntimeSet/GameObject")]
    public class GameObjectRuntimeSet : RuntimeSet<GameObject> 
    {
        [SerializeField] UnityEventGameObject onAddComplete;
        protected override UnityEvent<GameObject> OnAddComplete
        {
            get
            {
                return onRemoveComplete;
            }
        }
        [SerializeField] UnityEventGameObject onRemoveComplete;
        protected override UnityEvent<GameObject> OnRemoveComplete
        {
            get
            {
                return onRemoveComplete;
            }
        }
     }
}

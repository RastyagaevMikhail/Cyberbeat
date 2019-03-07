using GameCore;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.Events;
namespace GameCore
{
    [CreateAssetMenu (menuName = "GameCore/Selectors/Vector2ActionSelector")]
    public class Vector2ActionSelector : AEnumDataSelectorScriptableObject<Vector2, UnityEvent>
    {
        public List<Vector2UnityEventTypeData> datas;
        public override List<TypeData<Vector2, UnityEvent>> Datas
        {
            get
            {
                return datas.Cast<TypeData<Vector2, UnityEvent>> ().ToList ();
            }
        }

        [System.Serializable] public class Vector2UnityEventTypeData : TypeData<Vector2, UnityEvent> { }
        public void Invoke (Vector2 key)
        {
            UnityEvent action;
            Selector.TryGetValue (key, out action);
            if (action != null) action.Invoke ();
        }
    }
}

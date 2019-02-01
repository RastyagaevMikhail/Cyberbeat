using UnityEngine;

namespace GameCore
{
    public abstract class ACondition : ScriptableObject
    {
        public abstract bool Value { get; }
    }
}

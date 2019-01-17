using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;
namespace GameCore
{
    public abstract class GetSetVariableValue<TSavableVariable, TObject> : MonoBehaviour
    where TSavableVariable : SavableVariable<TObject>
    {
        public abstract TSavableVariable Variable { get; }
        public abstract UnityEvent<TObject> Set { get; }
    }
}

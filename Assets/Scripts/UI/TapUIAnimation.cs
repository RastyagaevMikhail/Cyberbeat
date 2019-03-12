using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
    public class TapUIAnimation : MonoBehaviour
    {
        [SerializeField] InputControlTypeVariable variable;
        [SerializeField] InputControlTypeSelector selector;
        [SerializeField] UnityEventInputControlType OnControlChanged;
        public void OnColotrolChange (string nameInputControlType)
        {
            InputControlType inputControlType = selector[nameInputControlType];
            if (inputControlType.Equals (variable.Value)) return;
            variable.Value = inputControlType;
            OnControlChanged.Invoke (inputControlType);
        }

    }
}

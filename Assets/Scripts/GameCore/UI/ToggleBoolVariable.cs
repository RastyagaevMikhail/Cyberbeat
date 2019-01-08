using GameCore;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace CyberBeat
{
    [RequireComponent (typeof (Toggle))]
    public class ToggleBoolVariable : MonoBehaviour
    {
        [SerializeField] BoolVariable variable;
        private Toggle _toggle = null;
        public Toggle toggle { get { if (_toggle == null) _toggle = GetComponent<Toggle> (); return _toggle; } }

        private void OnEnable ()
        {
            toggle.isOn = variable.Value;
        }

        private void Awake ()
        {
            toggle.onValueChanged.AddListener (variable.SetValue);
        }
    }
}

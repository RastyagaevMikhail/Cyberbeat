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
        public void Init (BoolVariable boolVariable)
        {
            variable = boolVariable;
            OnValidate ();
            OnEnable();
            Awake();
        }
        private Toggle _toggle = null;
        public Toggle toggle { get { if (_toggle == null) _toggle = GetComponent<Toggle> (); return _toggle; } }

        private void OnValidate ()
        {
            if (!variable) return;

            name = $"{variable.name}Toggle";
            GetComponentInChildren<Text> ().text = variable.name;
        }
        private void OnEnable ()
        {
            if (variable)
                toggle.isOn = variable.Value;
        }

        private void Awake ()
        {
            if (variable)
                toggle.onValueChanged.AddListener (variable.SetValue);
        }
    }
}

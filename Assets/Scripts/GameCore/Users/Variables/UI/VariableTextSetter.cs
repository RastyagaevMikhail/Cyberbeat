using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

namespace GameCore
{
    public abstract class VariableTextSetter<T, K> : MonoBehaviour where T : SavableVariable<K>
    {

        [SerializeField] protected T variable;
        public void SetVariavle (T newVariable)
        {
            variable = newVariable;
        }
        private TextMeshProUGUI _textTMPro = null;
        public TextMeshProUGUI textTMPro
        {
            get
            {
                if (this == null) return null;
                if (_textTMPro == null) _textTMPro = GetComponent<TextMeshProUGUI> ();
                return _textTMPro;
            }
        }
        private Text _textuigui = null;
        public Text textuigui
        {
            get
            {
                if (this == null) return null;
                if (_textuigui == null) _textuigui = GetComponent<Text> ();
                return _textuigui;
            }
        }
        public string text
        {
            get { return textTMPro ? textTMPro.text : textuigui ? textuigui.text : ""; }
            set
            {
                if (textTMPro) textTMPro.text = value;
                if (textuigui) textuigui.text = value;
            }
        }

        [SerializeField] protected string stringFormat = "{0}";
        protected virtual void OnEnable ()
        {
            variable.OnValueChanged += OnValueChanged;
            OnValueChanged (variable.Value);
        }
        private void OnDisable ()
        {
            variable.OnValueChanged -= OnValueChanged;
        }
        protected virtual void OnValueChanged (K obj)
        {
            text = string.Format (stringFormat, variable.Value);
        }

#if UNITY_EDITOR
        private void Update ()
        {
            if (!Application.isPlaying)
                text = string.Format (stringFormat, variable.Value);
        }
#endif

    }
}

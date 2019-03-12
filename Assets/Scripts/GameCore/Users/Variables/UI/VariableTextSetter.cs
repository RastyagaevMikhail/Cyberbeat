using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

namespace GameCore
{
    [ExecuteInEditMode]
    public abstract class VariableTextSetter<TVariable, TValue> : MonoBehaviour where TVariable : SavableVariable<TValue>
    {

        [SerializeField] protected TVariable variable;
        public void SetVariavle (TVariable newVariable)
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
        public virtual string text
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
        protected virtual void OnValueChanged (TValue obj)
        {
            text = string.Format (stringFormat, obj);
        }

        [SerializeField] bool updateInEditor;
#if UNITY_EDITOR
        private void Update ()
        {
            if (!Application.isPlaying && updateInEditor)
                OnValueChanged (variable.Value);
        }
#endif

    }
}

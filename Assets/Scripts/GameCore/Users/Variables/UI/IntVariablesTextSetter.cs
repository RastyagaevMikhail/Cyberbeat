using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.UI;
// #if TMPro
using Text = TMPro.TextMeshProUGUI;
// #endif
namespace GameCore
{
    public class IntVariablesTextSetter : MonoBehaviour
    {
        private Text _textComponent = null;
        public Text textComponent { get { if (_textComponent == null) _textComponent = GetComponent<Text> (); return _textComponent; } }

        [SerializeField] List<IntVariable> variables;

        [SerializeField] string format = "{0}/{1}";

        protected virtual void OnEnable ()
        {
            foreach (var variable in variables)
            {
                variable.OnValueChanged += OnValueChanged;
                OnValueChanged (variable.Value);
            }
        }

        private void OnValueChanged (int value)
        {
            textComponent.text = string.Format (
                format,
                variables
                .Select (variable => variable.Value)
                .Cast<object> ()
                .ToArray ()
            );
        }

        private void OnDisable ()
        {
            foreach (var variable in variables)
                variable.OnValueChanged -= OnValueChanged;
        }
#if UNITY_EDITOR
        private void Update ()
        {
            if (!Application.isPlaying)
                foreach (var variable in variables)
                    OnValueChanged (variable.Value);
        }
#endif
    }
}

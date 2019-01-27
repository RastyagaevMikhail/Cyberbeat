using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using Text = TMPro.TextMeshProUGUI;
namespace GameCore
{
    public class IntVariablesTextSetter : MonoBehaviour
    {
        private Text _textComponent = null;
        public Text textComponent { get { if (_textComponent == null) _textComponent = GetComponent<Text> (); return _textComponent; } }
        string text { set => textComponent.text = value; }

        [SerializeField] List<IntVariable> variables;
        [SerializeField] string Format = "{0}/{1}";

        private void OnEnable ()
        {
            variables.ForEach (variable => variable.OnValueChanged += OnValueChanged);
        }

        private void OnValueChanged (int obj)
        {
            text = string.Format (Format, variables.Select (valriable => valriable.Value).ToArray ());
        }
    }
}

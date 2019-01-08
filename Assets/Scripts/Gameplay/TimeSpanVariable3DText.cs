using GameCore;

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
    public class TimeSpanVariable3DText : VariableTextSetter<TimeSpanVariable, TimeSpan>
    {
        private SimpleHelvetica _textComponent3D = null;
        public SimpleHelvetica textComponent3D { get { if (_textComponent3D == null) _textComponent3D = GetComponent<SimpleHelvetica> (); return _textComponent3D; } }

        public override string text
        {
            get
            {
                return textComponent3D.Text;
            }
            set
            {
                textComponent3D.Text = value;
                textComponent3D.GenerateText ();
            }
        }
        protected override void OnValueChanged (TimeSpan timeSpan)
        {
            text = string.Format (stringFormat, new DateTime ((long) Mathf.Abs ((float) timeSpan.Ticks)));
        }

    }
}

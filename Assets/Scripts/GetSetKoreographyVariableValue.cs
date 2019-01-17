using GameCore;

using SonicBloom.Koreo;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace CyberBeat
{
    public class GetSetKoreographyVariableValue : GetSetVariableValue<KoreographyVariable, Koreography>
    {
        [SerializeField] KoreographyVariable variable;

        public override KoreographyVariable Variable { get { return variable; } }

        [SerializeField] UnityEventKoreography set;
        public override UnityEvent<Koreography> Set { get { return set; } }

        public void InvokeSetValue ()
        {
            Set.Invoke (Variable.Value);
        }
    }
}

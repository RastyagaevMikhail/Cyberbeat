using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace GameCore
{
    public class OfflineTimer : MonoBehaviour
    {
        [SerializeField] DateTimeVariable dateTimeVariable;
        [SerializeField] UnityEventTimeSpan onDiffOflineTime;

        public void SaveNow ()
        {
            dateTimeVariable.Value = DateTime.Now;
            dateTimeVariable.SaveValue ();
            Debug.LogFormat ("dateTimeVariable.Value = {0}", dateTimeVariable.Value);
        }

        public void SendDiff ()
        {
            dateTimeVariable.LoadValue ();
            var Diff = DateTime.Now.Subtract (dateTimeVariable.Value);
            Debug.Log (Diff);
            onDiffOflineTime.Invoke (Diff);
        }
    }
}

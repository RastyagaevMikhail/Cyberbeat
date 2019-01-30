using GameCore;

using SonicBloom.Koreo;

using System;
using System.Linq;

using UnityEngine;

namespace CyberBeat
{
    public abstract class APayloadData : IPayloadData
    {
        public string RandomString => strings.GetRandom ();
        public string StringValue => strings.FirstOrDefault ();
        public string[] Strings { get => strings; }

        [SerializeField] string[] strings;
        public virtual void Init (IPayload payload)
        {
            TextPayload textPayload = (payload as TextPayload);
            if (textPayload == null) return;

            strings = textPayload.TextVal.Split (",".ToCharArray (), StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < strings.Length; i++)
            {
                strings[i] = strings[i].Replace (" ", string.Empty);
            }
        }
    }
}

using GameCore;

using SonicBloom.Koreo;

using System.Linq;

using UnityEngine;

namespace CyberBeat
{
    public abstract class APayloadData : IPayloadData
    {
        public int RandomInt =>
            Ints.GetRandom ();

        public string RandomString =>
            strings.GetRandom ();

        public int IntValue =>
            Ints.FirstOrDefault ();

        public string StringValue =>
            strings.FirstOrDefault ();

        public string[] Strings { get => strings; }
        public int[] Ints { get => ints; }

        [SerializeField] string[] strings;
        [SerializeField] int[] ints;
        public virtual void Init (IPayload payload)
        {
            TextPayload textPayload = (payload as TextPayload);
            if (textPayload == null) return;
            string payloadValue = textPayload.TextVal;
            string[] splited = payloadValue.Split (',');
            strings = splited;
            try
            {
                ints = splited.Select (s => int.Parse (s)).ToArray ();
            }
            catch (System.Exception)
            {
                try
                {
                    ints = new int[] { int.Parse (payloadValue) };
                }
                catch (System.Exception)
                {
                    Debug.Log ($"Is not int".warn ());
                }
            }

        }
    }
}

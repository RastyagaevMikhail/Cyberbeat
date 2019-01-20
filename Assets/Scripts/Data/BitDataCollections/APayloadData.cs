using GameCore;

using SonicBloom.Koreo;

using System.Linq;

using UnityEngine;

namespace CyberBeat
{
    public abstract class APayloadData : IPayloadData
    {
        public int RandomInt =>
            ints.GetRandom ();

        public string RandomString =>
            strings.GetRandom ();

        public int IntValue =>
            ints.FirstOrDefault ();

        public string StringValue =>
            strings.FirstOrDefault ();

        [SerializeField] string[] strings;
        [SerializeField] int[] ints;
        public virtual void Init (IPayload payload)
        {
            string payloadValue = (payload as TextPayload).TextVal;
            string[] splited = payloadValue.Split (',');
            strings = splited;
            try
            {
                ints = splited.Select (s => int.Parse (s)).ToArray ();
            }
            catch (System.Exception)
            {
                ints = new int[] { int.Parse (payloadValue) };
            }

        }
    }
}

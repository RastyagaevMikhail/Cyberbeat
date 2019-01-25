using GameCore;

using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
	[RequireComponent (typeof (GameEventListenerIBitData))]
	public class SpeedParticles : MonoBehaviour
	{
		[SerializeField] TransformVariable parentVariable;
		Transform Parent { get { return parentVariable.ValueFast; } }
		Dictionary<string, ParticleSystem> hash = new Dictionary<string, ParticleSystem> ();
		[SerializeField] ParticleSystemSelector selector;
		private void OnEnable ()
		{
			foreach (var key in selector.Keys)
			{
				ParticleSystem value = Instantiate (selector[key], Parent);
				hash.Add (key, value);
				value.gameObject.SetActive (false);
			}
		}
		public void OnBit (IBitData bitData)
		{
			string key = bitData.StringValue;

			ParticleSystem part = null;

			hash.TryGetValue (key, out part);

			if (part == null)
				part = Instantiate (selector[key], Parent);

			part.gameObject.SetActive (true);

			part.Play ();

			this.DelayAction (bitData.Duration, () =>
			{
				if (!hash.ContainsKey (key)) hash.Add (key, part);
				part.gameObject.SetActive (false);
			});
		}
	}
}

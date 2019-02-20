using System.Collections;
using System.Collections.Generic;

using Timers;

using UnityEngine;
namespace CyberBeat
{
    public class AcelerationTimeController : MonoBehaviour
    {
        [SerializeField] ParticleSystem particles;
        public void OnBit (IBitData bitData)
        {
            particles.gameObject.SetActive (true);
            particles.Play (true);
            TimersManager.SetTimer (this, bitData.Duration, 1, () =>
            {
                particles.Stop ();
                particles.gameObject.SetActive (false);
            });
        }
    }
}

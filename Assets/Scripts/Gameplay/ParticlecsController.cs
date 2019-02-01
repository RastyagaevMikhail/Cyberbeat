using GameCore;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
    [RequireComponent (typeof (GameEventListenerColor))]
    public class ParticlecsController : MonoBehaviour
    {
        [SerializeField] PoolVariable pool;
        [SerializeField] IntVariable ScoresPerBeat;
        [SerializeField] TransformVariable PlayerTransform;

        public void OnColorTaked (Color color)
        {
            
        }
    }
}

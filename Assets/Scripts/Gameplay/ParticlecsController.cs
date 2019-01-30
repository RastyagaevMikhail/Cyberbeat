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
            var scoreText = pool.Pop<ScoreText> ("ScoreText", PlayerTransform.parent);
            scoreText.Init (ScoresPerBeat.Value, color);
        }
    }
}

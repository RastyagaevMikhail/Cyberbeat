using GameCore;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
    [RequireComponent (typeof (GameEventColor))]
    public class ParticlecsController : MonoBehaviour
    {
        public Pool pool { get { return Pool.instance; } }

        [SerializeField] IntVariable ScoresPerBeat;
        [SerializeField] TransformVariable PlayerTransform;
        public void OnColorTaked (Color color)
        {
            var scoreText = pool.Pop ("ScoreText", PlayerTransform.parent).Get<ScoreText> ();
            scoreText.Init (ScoresPerBeat.Value, color);
        }
    }
}

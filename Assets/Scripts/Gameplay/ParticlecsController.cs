using GameCore;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
    public class ParticlecsController : MonoBehaviour
    {
        public Pool pool { get { return Pool.instance; } }
        public IntVariable ScoresPerBeat;
        public void OnColorInterractorDeath (ColorInterractor interractor)
        {
            var scoreText = pool.Pop ("ScoreText", Player.instance.transform.parent).Get<ScoreText> ();
            scoreText.Init (ScoresPerBeat.Value, interractor.matSwitch.CurrentColor);
        }
    }
}

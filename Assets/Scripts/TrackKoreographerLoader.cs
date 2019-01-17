using SonicBloom.Koreo;
using SonicBloom.Koreo.Players;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
    [RequireComponent (typeof (SimpleMusicPlayer))]
    public class TrackKoreographerLoader : MonoBehaviour
    {
        private SimpleMusicPlayer _player = null;
        public SimpleMusicPlayer player { get{if(_player == null) _player = GetComponent<SimpleMusicPlayer>(); return _player;} }
        [SerializeField] TrackVariable trackVariable;

        public void LoadKoreography ()
        {
            player.LoadSong (trackVariable.Value.Koreography);
        }

    }
}

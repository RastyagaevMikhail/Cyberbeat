using GameCore;

using Sirenix.OdinInspector;

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
    [CreateAssetMenu (fileName = "AudioPlayer", menuName = "CyberBeat/Data/AudioPlayer")]
    public class GameAudioPlayer : ScriptableObject
    {
        [SerializeField] AudioClip[] clicks;
        [SerializeField] AudioClip[] playerDeaths;
        [SerializeField] AudioClip mianMenuTrack;
        [ShowInInspector][NonSerialized] int currentMainTheme = 0;
        [SerializeField] AudioClip notEnouthMoney;
        public void PlayClik ()
        {
            SoundManager.PlayUISound (clicks.GetRandom ());
        }
        public void PlayMainTheme ()
        {
            if (currentMainTheme == 0)
            {
                if (mianMenuTrack)
                    currentMainTheme = SoundManager.PlayMusic (mianMenuTrack, 1, true, false);
            }
            else
            {
                SoundManager.GetMusicAudio (currentMainTheme).Play ();
            }
        }

        public void PauseMainTheme ()
        {
            SoundManager.GetAudio (currentMainTheme).Pause ();
        }

        public void PlayNotMoney ()
        {
            SoundManager.PlayUISound (notEnouthMoney);
        }

        public void PlayPlayerDeath ()
        {
            SoundManager.PlaySound (playerDeaths.GetRandom ());
        }
    }
}

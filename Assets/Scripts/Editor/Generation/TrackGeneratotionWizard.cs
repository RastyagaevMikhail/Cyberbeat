using System.Collections;
using System.Collections.Generic;

using UnityEditor;

using UnityEngine;

using Tools = GameCore.Tools;
namespace CyberBeat
{
    public class TrackGeneratotionWizard : ScriptableWizard
    {
        [SerializeField] AudioClip clip;
        [MenuItem ("Game/Generation/Track")]
        private static void MenuEntryCall ()
        {
            DisplayWizard<TrackGeneratotionWizard> ("Title", "Generate");
        }

        private void OnWizardCreate ()
        {
            string nameTrack = clip.name;
            var track = Tools.ValidateSO<Track> ($"Assets/Resources/Data/Tracks/{nameTrack}.asset");

            track.music = new MusicInfo ();
            track.music.Validate (nameTrack);
            track.music.clip = clip;

            track.shopInfo = new ShopInfo ();
            track.shopInfo.Validate (nameTrack);

            track.progressInfo = new ProgressInfo ();
            track.progressInfo.Validate (nameTrack);

            track.ValidateKoreography ();

            track.ValidateLayerBits ();

            track.SaveME ();

            Selection.activeObject = track;

        }
    }
}

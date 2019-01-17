using GameCore;
using Tools = GameCore.Tools;

using SonicBloom.Koreo;

using System.Collections.Generic;

using UnityEditor;

using UnityEngine;

namespace CyberBeat.Generation
{
    public class TimeEventGenerationWizard : ScriptableWizard
    {
        [SerializeField] List<Track> tracks;

        [MenuItem ("Game/Generation/TimeEvent")]
        private static void MenuEntryCall ()
        {
            DisplayWizard<TimeEventGenerationWizard> ("TimeEvent Generation Wizard", "Generate");
        }

        private void OnWizardCreate ()
        {

            foreach (var track in tracks)
                foreach (var layer in Enums.LayerTypes)
                {
                    TimePointsData timePointsData = CreateInstance<TimePointsData>();
                    var events = track[layer];
                    TimeOfEventsData timeOfEventsData = CreateInstance<TimeOfEventsData> ();
                    timeOfEventsData.Init (track.Koreography.SampleRate, events,timePointsData);


                    Tools.CreateAsset (timeOfEventsData, "Assets/Data/TimeEvents/{0}/{1}.asset".AsFormat (track.name, layer));
                    Tools.CreateAsset (timePointsData, "Assets/Data/TimePoints/{0}/{1}.asset".AsFormat (track.name, layer));
                }
        }
    }
}

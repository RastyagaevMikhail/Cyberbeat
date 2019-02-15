using GameCore;

using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;

using SonicBloom.Koreo;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEditor;

using UnityEngine;
using Tools = GameCore.Tools;
namespace CyberBeat
{
    public class TracksHelperWindow : OdinEditorWindow
    {
        [MenuItem ("Game/Windows/Tracks Helper")]
        public static void Open ()
        {
            GetWindow<TracksHelperWindow> ().Show ();
        }
        public TracksCollection data { get { return TracksCollection.instance; } }

        [Button, PropertyOrder (int.MaxValue - 1)]
        public void AddAlltracks ()
        {
            tracks = Resources.LoadAll<Track> ("Data/Tracks");
        }

        [SerializeField, PropertyOrder (int.MaxValue)] Track[] tracks;

        [SerializeField] LayerType layer;
        [Button ("CalculateConstatn[Bit]")]
        public void CalculateConstatn ()
        {
            foreach (var track in tracks)
            {
                ProgressInfo progressInfo = track.progressInfo;
                progressInfo.Max = 0;
                Debug.LogFormat ("track = {0}", track);
                Debug.LogFormat ("track.LayerBitsSelector = {0}", track.LayerBitsSelector);
                Debug.LogFormat ("track.LayerBitsSelector[layer] = {0}", track.LayerBitsSelector[layer]);
                Debug.LogFormat ("track.LayerBitsSelector[layer].Bits = {0}", track.LayerBitsSelector[layer].Bits);
                foreach (var bitInfo in track.LayerBitsSelector[layer].Bits) //LayerType.Bit
                {
                    List<string> presetList = bitInfo.Strings.ToList ();
                    bool isContainConstant = presetList
                        .TrueForAll (p => data.Presets[p]
                            .Find (material =>
                            {
                                if (material)
                                    return material.name == "ConstantBeat" || material.name == "Switcher";
                                return false;
                            }));

                    if (isContainConstant) progressInfo.Max++;
                }
                progressInfo.Save ();
                track.progressInfo = progressInfo;
                track.Save ();
            }
        }

        [Button]
        public void ValidateKoreography ()
        {
            foreach (var track in tracks)
            {
                var Koreography = track.Koreography = Tools.ValidateSO<Koreography> ($"Assets/Data/Koreography/{track.name}/{track.name}_Koreography.asset");
                Debug.LogFormat ("Koreography = {0}", Koreography);
                EditorUtility.SetDirty (Koreography);
                for (int i = 0; i < Koreography.GetNumTempoSections (); i++)
                    Koreography.RemoveTempoSectionAtIndex (i);
                Koreography.InsertTempoSectionAtIndex (0).SectionName = "Default Selection";
                Koreography.InsertTempoSectionAtIndex (0).SectionName = "Zero Selection";
                Koreography.SourceClip = track.music.clip;
                Koreography.Save ();
                track.Save ();
            }
            ValidateKoreographyTracksLayers ();
        }

        [Button]
        public void ValidateKoreographyTracksLayers ()
        {
            foreach (var track in tracks)
            {
                var koreography = track.Koreography;

                foreach (var koreographyTrack in koreography.Tracks)
                    koreography.RemoveTrack (koreographyTrack);

                foreach (var layer in Enums.instance.LayerTypes)
                {
                    string trackName = track.name;
                    string layerName = layer.name;
                    var trackLayer =
                        Tools.ValidateSO<KoreographyTrack> ($"Assets/Data/Koreography/{trackName}/Tracks/{layerName}_{trackName}.asset");
                    EditorUtility.SetDirty (trackLayer);
                    trackLayer.EventID = layerName;
                    // if (Koreography.CanAddTrack (trackLayer))
                    koreography.AddTrack (trackLayer);
                }
                koreography.Save ();
                track.Save ();
            }
        }

        [Button ("Validate LayerBits")]
        public void ValidateLayerBits ()
        {
            foreach (Track track in tracks)
            {

                track.LayerBitsSelector = Tools.ValidateSO<LayerTypeTrackBitsCollectionSelector> ($"Assets/Data/Selectors/Tracks/{track.name}_LayerBitsSelector.asset");
                track.LayerBitsSelector.datas = new List<LayerTypeTrackBitsCollectionSelector.LayerTypeTrackBitsCollectionTypeData> ();
                foreach (var layer in Enums.instance.LayerTypes)
                {
                    var dataBits = Tools.ValidateSO<TrackBitsCollection> ($"Assets/Data/LayerTypeCollection/{layer.name}/{track.name}_{layer.name}.asset");
                    dataBits.Init (track.GetAllEventsByType (layer));

                    track.LayerBitsSelector.datas.Add (new LayerTypeTrackBitsCollectionSelector.LayerTypeTrackBitsCollectionTypeData ()
                    {
                        type = layer,
                            data = dataBits
                    });
                }
                track.LayerBitsSelector.Save ();
                track.Save ();
            }
        }

        [Button ("Convert To Name")]
        void ConvertToName ()
        {
            // LayerType layer = LayerType.Effect;
            foreach (var track in tracks)
            {
                EditorUtility.SetDirty (track.GetTrack (layer));
                foreach (var e in track[layer])
                {
                    string pld = e.GetTextValue ();
                    var payload = new TextPayload ();
                    payload.TextVal = pld.Replace (',', '_');
                    e.Payload = payload;
                }
            }
        }
    }
}

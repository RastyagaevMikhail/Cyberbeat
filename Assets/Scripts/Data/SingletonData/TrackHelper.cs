using System.Text.RegularExpressions;

namespace CyberBeat
{
    using GameCore;

    using SonicBloom.Koreo;

    using System.Collections.Generic;
    using System.Linq;

    using UnityEngine;
    [CreateAssetMenu (fileName = "TrackHelper", menuName = "CyberBeat/TrackHelper")]
    public class TrackHelper : ScriptableObject
    {
        [SerializeField] TracksCollection data;
        [SerializeField] Enums enums;
        [SerializeField] StringMaterialsSelector Presets;

        [SerializeField] bool debug;
        public Enums Enums => enums;
#if UNITY_EDITOR
        private void OnValidate ()
        {
            if (!data)
            data = Resources.Load<TracksCollection> ("Data/TracksCollection");
            if (!enums)
            enums = Resources.Load<Enums> ("Data/Enums");
            if (!Presets)
            Presets = Tools.GetAssetAtPath<StringMaterialsSelector> ("Assets/Data/Selectors/Preset/Presets.asset");

        }
#endif

        public void UpdateCollections (LayerTypeTrackBitsCollectionSelector layerBitsSelector) { data.UpdateCollections (layerBitsSelector); }
        public int CalculateConstant (Track track, List<TrackBit> bits)
        {
            int Max = 0;
            foreach (var bitInfo in bits)
            {
                List<string> presetList = bitInfo.Strings.ToList ();
                bool isContainConstant = presetList
                    .Select (s => s.Regex (@"\d"))
                    .ToList ()
                    .TrueForAll (p => Presets[p]
                        .Find (material =>
                        {
                            if (material)
                                return material.name == "ConstantBeat";
                            return false;
                        }));

                if (isContainConstant) Max++;
            }
            return Max;
        }
        public LayerTypeTrackBitsCollectionSelector ValidateLayerBitsSelector (Track track, LayerTypeTrackBitsCollectionSelector selector)
        {
#if UNITY_EDITOR
            selector = Tools.ValidateSO<LayerTypeTrackBitsCollectionSelector> ($"Assets/Data/Selectors/Tracks/{track.name}_LayerBitsSelector.asset");
            selector.datas = new List<LayerTypeTrackBitsCollectionSelector.LayerTypeTrackBitsCollectionTypeData> ();

            foreach (var layer in enums.GetValues<LayerType> ())
            {
                var dataBits = Tools.ValidateSO<TrackBitsCollection> ($"Assets/Data/LayerTypeCollection/{layer.name}/{track.name}_{layer.name}.asset");
                dataBits.Init (track.GetAllEventsByType (layer));
                dataBits.Save ();
                selector.datas.Add (new LayerTypeTrackBitsCollectionSelector.LayerTypeTrackBitsCollectionTypeData ()
                {
                type = layer,
                data = dataBits
                });
            }
            selector.Save ();
#endif
            return selector;
        }

        public Koreography ValidateKoreography (Track track)
        {
            Koreography koreography = null;
#if UNITY_EDITOR
            koreography = Tools.ValidateSO<Koreography> ($"Assets/Data/Koreography/{track.name}/{track.name}_Koreography.asset");

            for (int i = 0; i < koreography.GetNumTempoSections (); i++)
                koreography.RemoveTempoSectionAtIndex (0);

            koreography.InsertTempoSectionAtIndex (0).SectionName = "Zero Selection";
            koreography.SourceClip = track.music.clip;
            koreography.Save ();
#endif
            return koreography;
        }
        public void ValidateKoreographyTrackLayer (Koreography koreography)
        {
#if UNITY_EDITOR
            foreach (var koreographyTrack in koreography.Tracks)
                koreography.RemoveTrack (koreographyTrack);

            string trackName = koreography.name.Replace ("_Koreography", "");
            foreach (var layer in enums.GetValues<LayerType> ())
            {
                string layerName = layer.name;
                var trackLayer =
                Tools.ValidateSO<KoreographyTrack> ($"Assets/Data/Koreography/{trackName}/Tracks/{layerName}_{trackName}.asset");
                UnityEditor.EditorUtility.SetDirty (trackLayer);
                trackLayer.EventID = layerName;

                koreography.AddTrack (trackLayer);
            }
            koreography.Save ();
#endif
        }
    }

}

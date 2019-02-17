namespace CyberBeat
{
    using GameCore;

    using SonicBloom.Koreo;

    using System.Collections.Generic;
    using System.Linq;

    using UnityEngine;

    public class TrackHelper : SingletonData<TrackHelper>
    {
#if UNITY_EDITOR
        public override void InitOnCreate () { }
        public override void ResetDefault () { }
        public TracksCollection data { get { return TracksCollection.instance; } }
        public Enums enums { get { return Enums.instance; } }
        public void UpdateCollections (LayerTypeTrackBitsCollectionSelector layerBitsSelector) { data.UpdateCollections (layerBitsSelector); }
        public int CalculateConstant (Track track, List<IBitData> bits)
        {
            int Max = 0;
            foreach (var bitInfo in bits)
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

                if (isContainConstant) Max++;
            }
            return Max;
        }
        public void ValidateLayerBitsSelector (Track track, LayerTypeTrackBitsCollectionSelector selector)
        {
            selector = Tools.ValidateSO<LayerTypeTrackBitsCollectionSelector> ($"Assets/Data/Selectors/Tracks/{track.name}_LayerBitsSelector.asset");
            selector.datas = new List<LayerTypeTrackBitsCollectionSelector.LayerTypeTrackBitsCollectionTypeData> ();
            foreach (var layer in enums.LayerTypes)
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
            track.Save ();
        }

        public Koreography ValidateKoreography (Track track)
        {
            var koreography = Tools.ValidateSO<Koreography> ($"Assets/Data/Koreography/{track.name}/{track.name}_Koreography.asset");

            for (int i = 0; i < koreography.GetNumTempoSections (); i++)
                koreography.RemoveTempoSectionAtIndex (0);

            koreography.InsertTempoSectionAtIndex (0).SectionName = "Zero Selection";
            koreography.SourceClip = track.music.clip;
            koreography.Save ();
            track.Save ();
            return koreography;
        }
        public void ValidateKoreographyTrackLayer (Koreography koreography)
        {

            foreach (var koreographyTrack in koreography.Tracks)
                koreography.RemoveTrack (koreographyTrack);

            string trackName = koreography.name.Replace ("_Koreography", "");
            foreach (var layer in enums.LayerTypes)
            {
                string layerName = layer.name;
                var trackLayer =
                Tools.ValidateSO<KoreographyTrack> ($"Assets/Data/Koreography/{trackName}/Tracks/{layerName}_{trackName}.asset");
                UnityEditor.EditorUtility.SetDirty (trackLayer);
                trackLayer.EventID = layerName;
                // if (Koreography.CanAddTrack (trackLayer))
                koreography.AddTrack (trackLayer);
            }
            koreography.Save ();
        }
    }
#endif

}

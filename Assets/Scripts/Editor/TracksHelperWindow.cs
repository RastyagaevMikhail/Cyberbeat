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
    public partial class TracksHelperWindow : OdinEditorWindow
    {
        [MenuItem ("Game/Windows/Tracks Helper")]
        public static void Open ()
        {
            TracksHelperWindow tracksHelperWindow = GetWindow<TracksHelperWindow>();
            tracksHelperWindow.AddAlltracks();
            tracksHelperWindow.Show ();
        }
        public TrackHelper data { get { return TrackHelper.instance; } }

        [PropertySpace]
        [Button, PropertyOrder (int.MaxValue - 1)]
        public void AddAlltracks ()
        {
            tracks = Resources.LoadAll<Track> ("Data/Tracks");
        }

        [SerializeField, PropertyOrder (int.MaxValue)] Track[] tracks;
        [Button] public void UpdateCollections (LayerTypeTrackBitsCollectionSelector layerBitsSelector) { data.UpdateCollections (layerBitsSelector); }

        [Button] public void ValidateLayerTypes ()
        {
            Enums.instance.ValidateLayerType ();
            Selection.activeObject = Enums.instance;
        }

        [PropertyOrder (50), PropertySpace]
        [TitleGroup ("From Layer")]
        [SerializeField] LayerType layer;
        [PropertyOrder (51)]
        [Button ("Calculate Constant On Track [Bit]")]
        public void CalculateConstantOn (Track track)
        {
            track.CalculateConstantOn(layer);
        }

        [PropertyOrder (52)]
        [Button ("Calculate Constant On All Tracks [Bit]")]
        public void CalculateConstantOnAllTracks ()
        {
            foreach (Track track in tracks)
                CalculateConstantOn (track);
        }

        [PropertyOrder (53)]
        [Button ("BitItemData Generattion")]
        public void TrackBitItemDataGenerattion (TimeControllerType user, bool validateIsOver)
        {
            string layerName = layer.name;

            TrackBitItemData trackBitItemData = CreateInstance<TrackBitItemData> ();
            trackBitItemData.Variable = Tools.ValidateSO<ABitDataCollectionVariable> ($"Assets/Data/Variables/ABitDataCollection/Current{layerName}Collection.asset");
            trackBitItemData.CreateAsset ($"Assets/Data/ATimeUpdatable/TrackBitItemData/{user}/{layerName}.asset");

            Tools.ValidateSO<GameEventIBitData> ($"Assets/Data/Events/IBitData/On{layerName}{user}.asset");
            if (validateIsOver)
                Tools.ValidateSO<GameEventIBitData> ($"Assets/Data/Events/IBitData/On{layerName}{user}IsOver.asset");
        }

        [TitleGroup ("Foldouts")]
        [FoldoutGroup ("Koreography")]
        [Button (ButtonSizes.Large)]
        public void ValidateKoreography (Track track)
        {
            track.ValidateKoreography ();
        }

        [FoldoutGroup ("Koreography")]
        [Button (ButtonSizes.Large)]
        public void ValidateKoreography ()
        {
            foreach (var track in tracks)
                ValidateKoreography (track);
        }

        [FoldoutGroup ("Koreography Tracks")]
        [Button (ButtonSizes.Large)]
        public void ValidateKoreographyTrackLayer (Track track)
        {
            track.ValidateKoreographyTrackLayer ();
        }

        [FoldoutGroup ("Koreography Tracks")]
        [Button (ButtonSizes.Large)]
        public void ValidateKoreographyTracksLayers ()
        {
            foreach (var track in tracks)
                ValidateKoreographyTrackLayer (track);
        }

        [FoldoutGroup ("Track LayerBits Selector")]
        [Button ("Validate LayerBitsSelector", ButtonSizes.Large)]
        public void ValidateLayerBitsSelector (Track track)
        {
            track.ValidateLayerBitsSelector ();
        }

        [FoldoutGroup ("Track LayerBits Selector")]
        [Button ("Validate All LayerBits Selectors", ButtonSizes.Large)]
        public void ValidateAsllLayerBitsSelectors ()
        {
            foreach (Track track in tracks)
                ValidateLayerBitsSelector (track);
        }

    }
}

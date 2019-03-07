﻿using GameCore;

using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;

using SonicBloom.Koreo;

using System;
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
            TracksHelperWindow tracksHelperWindow = GetWindow<TracksHelperWindow> ();
            tracksHelperWindow.Init ();
            tracksHelperWindow.Show ();
        }

        [SerializeField] TrackHelper data;
        [SerializeField] LayerTypeABitDataCollectionVariableSelector collctionsSelector;
        Enums enums => data.Enums;
        void Init ()
        {
            data = Resources.Load<TrackHelper> ("Data/TrackHelper");
            tracks = Resources.LoadAll<Track> ("Data/Tracks");

            collctionsSelector =
                Tools.GetAssetAtPath<LayerTypeABitDataCollectionVariableSelector>
                ("Assets/Data/Variables/ABitDataCollection/LayerTypeABitDataCollectionVariableSelector.asset");
        }

        [TitleGroup ("From Layer")]
        [SerializeField] LayerType layer;
        #region Valiadtors 
        [FoldoutGroup ("Valiadtors")]
        [Button ("LayerType", ButtonSizes.Large)]
        public void ValidateLayerTypes ()
        {
            enums.Validate ();
            collctionsSelector.Validate (enums);
            Selection.activeObject = enums;
        }

        [HorizontalGroup ("Valiadtors/CalculateConstantOnAllTracks")]
        [Button]
        public void CalculateConstantOn (Track track)
        {
            track.CalculateConstantOn (layer);
        }

        [HorizontalGroup ("Valiadtors/CalculateConstantOnAllTracks")]
        [Button ("All [Bit]")]
        public void CalculateConstantOnAllTracks ()
        {
            foreach (Track track in tracks)
                CalculateConstantOn (track);
        }

        [HorizontalGroup ("Valiadtors/UpdateCollections")]
        [Button]
        public void UpdateCollections (LayerTypeTrackBitsCollectionSelector layerBitsSelector)
        {
            data.UpdateCollections (layerBitsSelector);
        }

        [HorizontalGroup ("Valiadtors/UpdateCollections")]
        [Button ("All")]
        public void UpdateALLCollections ()
        {
            LayerTypeTrackBitsCollectionSelector[] selectors = Tools.GetAtPath<LayerTypeTrackBitsCollectionSelector> ("Assets/Data/Selectors/Tracks");
            Debug.Log (selectors.Log ());
            foreach (var layerBitsSelector in selectors)
                data.UpdateCollections (layerBitsSelector);
        }

        [HorizontalGroup ("Valiadtors/Koreography")]
        [Button]
        public void ValidateKoreography (Track track)
        {
            track.ValidateKoreography ();
        }

        [HorizontalGroup ("Valiadtors/Koreography")]
        [Button ("All")]
        public void ValidateKoreography ()
        {
            foreach (var track in tracks)
                ValidateKoreography (track);
        }

        [HorizontalGroup ("Valiadtors/Koreography Tracks")]
        [Button]
        public void ValidateKoreographyTrackLayer (Track track)
        {
            track.ValidateKoreographyTrackLayer ();
        }

        [HorizontalGroup ("Valiadtors/Koreography Tracks")]
        [Button ("All")]
        public void ValidateKoreographyTracksLayers ()
        {
            foreach (var track in tracks)
                ValidateKoreographyTrackLayer (track);
        }

        [HorizontalGroup ("Valiadtors/Track LayerBits Selector")]
        [Button]
        public void ValidateLayerBitsSelector (Track track)
        {
            track.ValidateLayerBitsSelector ();
        }

        [HorizontalGroup ("Valiadtors/Track LayerBits Selector")]
        [Button ("All")]
        public void ValidateAsllLayerBitsSelectors ()
        {
            foreach (Track track in tracks)
                ValidateLayerBitsSelector (track);
        }

        #endregion

        #region Generators 
        [PropertySpace]
        [FoldoutGroup ("Generators")]
        [Button]
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

        [Button] public void GenerateNewLayerType (string nameLayer, bool validate = true)
        {
            var layer = Tools.ValidateSO<LayerType> ($"Assets/Data/Enums/LayerType/{nameLayer}.asset");

            enums.TryAddEnumScriptable<LayerType> (layer);
            enums.Save ();

            if (validate)
            {
                ValidateKoreographyTracksLayers ();
                ValidateAsllLayerBitsSelectors ();
            }

        }

        [Button] public void GenerateNewEnum (Type type, string nameValue)
        {
            var value = Tools.ValidateSO ($"Assets/Data/Enums/LayerType/{nameValue}.asset", type);

            enums.TryAddEnumScriptable (type, (EnumScriptable) value);
            enums.Save ();
        }

        #endregion

        #region Tracks 

        [ListDrawerSettings (
            Expanded = true,
            IsReadOnly = true,
            HideAddButton = true,
            HideRemoveButton = true,
            ShowPaging = false)]
        [Space]
        [PropertyOrder (int.MaxValue)]
        [SerializeField]
        Track[] tracks;

        #endregion

    }
}

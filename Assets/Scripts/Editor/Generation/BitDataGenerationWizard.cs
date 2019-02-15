using System.Collections;
using System.Collections.Generic;

using UnityEditor;

using UnityEngine;
using Tools = GameCore.Tools;
namespace CyberBeat.Generation
{

    public class BitDataGenerationWizard : ScriptableWizard
    {
        [SerializeField] LayerType layer;
        [MenuItem ("Game/Generation/BitData")]
        private static void MenuEntryCall ()
        {
            DisplayWizard<BitDataGenerationWizard> ("Generate BitData", "Generate");
        }

        private void OnWizardCreate ()
        {
            string layerName = layer.ToString();
            // string layerName = layer.name;
            var CurrentLayerBitItemData = Tools.ValidateSO<TrackBitItemData> ($"Assets/Data/ATimeUpdatable/Current{layerName}ItemData.asset");
            var LayerDataCollectionVariable = Tools.ValidateSO<ABitDataCollectionVariable> ($"Assets/Data/Variables/ABitDataCollection/Current{layerName}Collection.asset");
            var LayerGameEvent = Tools.ValidateSO<GameEventIBitData> ($"Assets/Data/Events/IBitData/On{layerName}.asset");
        }
    }
}

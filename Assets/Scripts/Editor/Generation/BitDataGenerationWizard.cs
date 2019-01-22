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
            var CurrentLayerBitItemData = Tools.ValidateSO<TrackBitItemData> ($"Assets/Data/ATimeUpdatable/Current{layer}ItemData.asset");
            var LayerDataCollectionVariable = Tools.ValidateSO<ABitDataCollectionVariable> ($"Assets/Data/Variables/ABitDataCollection/Current{layer}Collection.asset");
            var LayerGameEvent = Tools.ValidateSO<GameEventIBitData> ($"Assets/Data/Events/IBitData/On{layer}.asset");
        }
    }
}

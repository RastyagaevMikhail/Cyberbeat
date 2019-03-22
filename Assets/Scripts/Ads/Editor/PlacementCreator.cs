using System.Collections;
using System.Collections.Generic;

using UnityEditor;

using UnityEngine;
namespace GameCore.Editor
{
    public class PlacementCreator : ScriptableWizard
    {

        [MenuItem ("Game/Windows/Placement Creator")]
        private static void MenuEntryCall ()
        {
            DisplayWizard<PlacementCreator> ("Placement Creator", "Parce by csv", "Create Placement");
        }

        [SerializeField] TextAsset csv;
        [SerializeField] string placementName;
        [SerializeField] string placementDecsription;
        private void OnWizardCreate ()
        {
            var lines = csv.text.Split ('\n');
            foreach (var line in lines)
            {
                var values = line.Split (';');
                var name = values[0];
                var desc = values[1];
                ValidatePlacment (name, desc);
            }
        }

        private void ValidatePlacment (string name, string desc)
        {
            Placement placement = GameCore.Tools.ValidateSO<Placement> ($"Assets/Data/Enums/Placement/{name}.asset");
            Debug.LogFormat ("placement = {0}", placement);
            placement.Description = desc;
            placement.Save ();
        }

        private void OnWizardOtherButton ()
        {
            ValidatePlacment (placementName, placementDecsription);
        }
    }
}

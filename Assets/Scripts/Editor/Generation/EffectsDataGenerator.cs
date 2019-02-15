using GameCore;

using System.Collections;

using UnityEditor;

using UnityEngine;
using Tools = GameCore.Tools;

namespace CyberBeat
{
    public class EffectsDataGenerator : ScriptableWizard
    {
        [SerializeField] Material originalMaterial;
        [SerializeField] MeshRenderer originalPrefab;

        [MenuItem ("Game/Generation/EffectsData")]
        private static void MenuEntryCall ()
        {
            DisplayWizard<EffectsDataGenerator> ("EffectsDataGenerator", "Generate Data", "Generate Materials");
        }

        private void OnWizardCreate () => GenerateData ();
        private void OnWizardOtherButton () => GenerateMaterials ();
        private void GenerateData ()
        {
            var materials = Tools.GetAtPath<Material> ("Assets/Materials/Effects");
            foreach (var material in materials)
            {
                EffectDataPreset preset = CreateInstance<EffectDataPreset> ();
                string namePrefab = material.name;
                preset.Init (namePrefab);
                preset.CreateAsset ($"Assets/Data/MetaData/Effects/{namePrefab}.asset");
                var intance = Instantiate (originalPrefab);
                intance.name = namePrefab;
                intance.sharedMaterial = material;
            }
        }
        private void GenerateMaterials ()
        {
            var textures = Tools.GetAtPath<Texture> ("Assets/Textures/Effects");
            Debug.Log (textures);
            foreach (var texture in textures)
            {
                Material material = Object.Instantiate (originalMaterial);
                material.SetTexture ("_MainTex", texture);

                string materialName = texture.name;
                material.name = materialName;
                Tools.CreateAsset (material, $"Assets/Materials/Effects/{materialName}.mat");
            }
        }
    }
}

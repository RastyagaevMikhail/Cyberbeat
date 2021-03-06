﻿using GameCore;
using GameCore.Utils;

using Sirenix.OdinInspector;

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using UnityEngine;
namespace CyberBeat
{
    public class SkinsDataCollection : SingletonData<SkinsDataCollection>
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem ("Game/Data/Skins")] public static void Select () { UnityEditor.Selection.activeObject = instance; }
        public override void ResetDefault ()
        {
            foreach (var skins in skinsSelector.Values)
                foreach (var skin in skins)
                    skin.ResetDefault ();
            foreach (var type in AllTypes)
                if (skinIndexsSelector.ContainsKey (type))
                    skinIndexsSelector[type].Value = 0;
        }

        [ContextMenu ("InitOnCreate")]
        public override void InitOnCreate ()
        {
            AllTypes = Tools.GetAtPath<SkinType> ("Assets/Data/Enums/Skins").ToList ();
            foreach (var skinType in AllTypes)
            {
                skinsSelector.Add (skinType, new List<SkinItem> ());
                //? createIndexes
                skinIndexsSelector[skinType] = ScriptableObject.CreateInstance<IntVariable> ();
                IntVariable indexVariable = skinIndexsSelector[skinType];
                indexVariable.name = "{0}SkinIndex".AsFormat (skinType.name);
                Tools.CreateAsset (indexVariable, "Assets/Data/Skins/Indexes/{0}.asset".AsFormat (indexVariable.name));
                indexVariable.SetSavable (true);
            }

        }

        [SerializeField] List<SkinsValiadtor> SkinsFolders;
        [ContextMenu ("CreateNewSkins")]
        void CreateNewSkins ()
        {
            foreach (var validator in SkinsFolders)
            {
                validator.AddNewSkins ();
            }
        }

        [System.Serializable]
        public class SkinsValiadtor
        {

            public SkinsDataCollection data { get { return SkinsDataCollection.instance; } }

            static List<string> namesSkisTypes = null;
            static List<string> NamesSkisTypes { get { if (namesSkisTypes == null || namesSkisTypes.Count == 0) namesSkisTypes = ReflectiveEnumerator.GetNames<SkinItem> (); return namesSkisTypes; } }
            static string[] namesOfSearchedPrefabTypes = new string[] { "Object", "Texture" };
            [ContextMenuItem ("Add New Skins", "AddNewSkins")]
            [SerializeField] SkinType type;

            [SerializeField] string searchedPrefabType = namesOfSearchedPrefabTypes[0];
            [SerializeField] string SkinItemTypeName = NamesSkisTypes[0];
            [SerializeField] string prefabsPath = "Assets/Prefabs/Skins/{0}"; /// {0} - SkinType name
            [SerializeField] string iconsPath = "Assets/Sprites/UI/Skins/{0}"; /// {0} - SkinType name
            [SerializeField] string pathFromSave = "Assets/Data/Skins/{0}/{1}.asset"; /// {0} - SkinType name {1} - SkintItem(Prefab) name

            public List<SkinItem> LoadSkinItems ()
            {
                var prefabs = Tools.GetAtPath (prefabsPath.AsFormat (type.name), searchedPrefabType);
                var icons = Tools.GetAtPath<Sprite> (iconsPath.AsFormat (type.name));
                List<SkinItem> items = prefabs.Select ((p, i) =>
                {
                    SkinItem skinItem = ScriptableObject.CreateInstance (SkinItemTypeName) as SkinItem;
                    skinItem.InitOnCreate (icons.Length == 0 ? null : icons[i], p, i * 100, type);
                    Tools.CreateAsset (skinItem, pathFromSave.AsFormat (type.name, p.name));
                    return skinItem;
                }).ToList ();

                return items;
            }

            [Button]
            public void AddNewSkins ()
            {
                data.skinsSelector.Add (type, LoadSkinItems ());
                data.skinsSelector.Save ();
            }
        }
#else
        public override void ResetDefault () { }
#endif
        [SerializeField] List<SkinType> AllTypes;
        public SkinsEnumDataSelector skinsSelector;
        public SkinIndexSelector skinIndexsSelector;

#if UNITY_EDITOR
        [Button] public void RandomizePrices ()
        {
            foreach (var skinLists in skinsSelector.Values)
            {
                foreach (var skin in skinLists)
                {
                    if (skin.Price == 0) continue;
                    skin.Price = Random.Range (750, 1250);
                    skin.VideoCount = Random.Range (7, 15);
                    skin.Save ();
                }
            }
        }
#endif

    }

}

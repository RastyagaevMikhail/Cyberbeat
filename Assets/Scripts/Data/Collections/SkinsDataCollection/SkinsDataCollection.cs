using GameCore;

using Sirenix.OdinInspector;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace CyberBeat
{
    public class SkinsDataCollection : DataCollections<SkinsDataCollection, SkinItem>
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem ("Game/Data/Skins")] public static void Select () { UnityEditor.Selection.activeObject = instance; }
        public override void ResetDefault ()
        {
            // foreach (var item in SkinsFolders)
            //     item.AddNewSkins();
            foreach (var type in AllTypes)
                skinIndexs[type].Value = 0;
        }
        public override void InitOnCreate ()
        {
            AllTypes = Tools.GetAtPath<SkinType> ("Assets/Data/Enums/Skins").ToList ();
            foreach (var skinType in AllTypes)
            {
                skins.Add (skinType, new List<SkinItem> ());
                //? createIndexes
                skinIndexs[skinType] = ScriptableObject.CreateInstance<IntVariable> ();
                IntVariable indexVariable = skinIndexs[skinType];
                indexVariable.name = "{0}SkinIndex".AsFormat (skinType.name);
                Tools.CreateAsset (indexVariable, "Assets/Data/Skins/Indexes/{0}.asset".AsFormat (indexVariable.name));
                indexVariable.isSavable = true;
            }

        }

        [Title ("{0} - SkinType name;  {1} - SkintItem(Prefab) name")][SerializeField] List<SkinsValiadtor> SkinsFolders;
        [System.Serializable]
        public class SkinsValiadtor
        {

            public SkinsDataCollection data { get { return SkinsDataCollection.instance; } }

            [SerializeField] SkinType type;
            [SerializeField] string searchedPrefabType = "Object";
            [SerializeField] string prefabsPath = "Assets/Prefabs/Skins/{0}"; /// {0} - SkinType name
            [SerializeField] string iconsPath = "Assets/Images/Sprites/Skins/{0}"; /// {0} - SkinType name
            [SerializeField] string pathFromSave = "Assets/Data/Skins/{0}/{1}.asset"; /// {0} - SkinType name {1} - SkintItem(Prefab) name

            public List<SkinItem> LoadSkinItems ()
            {
                var prefabs = Tools.GetAtPath (prefabsPath.AsFormat (type.name), searchedPrefabType);
                var icons = Tools.GetAtPath<Sprite> (iconsPath.AsFormat (type.name));
                List<SkinItem> items = prefabs.Select ((p, i) =>
                {
                    SkinItem skinItem = ScriptableObject.CreateInstance<SkinItem> ();
                    skinItem.InitOnCreate (icons[i], p, i * 100);
                    Tools.CreateAsset (skinItem, pathFromSave.AsFormat (type.name, p.name));
                    return skinItem;
                }).ToList ();

                return items;
            }

            [Button]
            public void AddNewSkins ()
            {
                data.skins[type] = LoadSkinItems ();
            }
        }
#endif
        public List<SkinType> AllTypes;
        public Dictionary<SkinType, List<SkinItem>> skins = new Dictionary<SkinType, List<SkinItem>> ();
        public Dictionary<SkinType, IntVariable> skinIndexs = new Dictionary<SkinType, IntVariable> ();

        [ShowInInspector] int skinIndex { get { return skinIndexs[SkinType].Value; } set { skinIndexs[SkinType].Value = value; } }
        public int SkinIndex { get { return skinIndex.GetAsClamped (0, Items.Count - 1); } set { skinIndex = value; } }
        public SkinType SkinType;
        public List<SkinItem> Items { get { return skins[SkinType].ToList (); } }

        [ShowInInspector] public SkinItem currrentSkinItem { get { return Items[SkinIndex]; } }

        public System.Action<int> OnSkinsChanged { get { return skinIndexs[SkinType].OnValueChanged; } set { skinIndexs[SkinType].OnValueChanged = value; } }
    }

}

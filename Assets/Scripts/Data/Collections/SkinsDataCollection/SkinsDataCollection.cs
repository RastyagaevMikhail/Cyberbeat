using GameCore;
using GameCore.Utils;

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using UnityEngine;
namespace CyberBeat
{
    public class SkinsDataCollection : DataCollections<SkinsDataCollection, SkinItem>
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
                indexVariable.isSavable = true;
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

            public void AddNewSkins ()
            {
                data.skinsSelector.Add (type, LoadSkinItems ());
                data.skinsSelector.Save();
            }
        }
#endif
        public List<SkinType> AllTypes;
        public SkinsEnumDataSelector skinsSelector;
        public SkinIndexSelector skinIndexsSelector;
        [SerializeField] SkinType RoadType;
        public Material RoadMaterial;
        public bool isRoadType (SkinType skinType) { return RoadType.Equals (skinType); }
        public int RoadSkinTypeIndex { get { return skinIndexsSelector[RoadType].Value; } set { skinIndexsSelector[RoadType].Value = value; } }
        public List<SkinItem> RoadSkins { get { return skinsSelector[RoadType]; } }
        int skinIndex
        {
            get
            {
                if (!skinIndexsSelector.ContainsKey (SkinType)) return 0;
                return skinIndexsSelector[SkinType].Value;
            }
            set
            {
                if (SkinType == RoadType) Debug.LogFormat ("skinIndex = {0}", value);
                if (skinIndexsSelector.ContainsKey (SkinType)) skinIndexsSelector[SkinType].Value = value;
            }
        }
        public int SkinIndex { get { return skinIndex.GetAsClamped (0, Items.Count - 1); } set { skinIndex = value; } }

        [SerializeField] UnityObjectVariable CurrentSkinTypeObjecVariable;
        public SkinType SkinType { get { return CurrentSkinTypeObjecVariable.As<SkinType> (); } }
        public List<SkinItem> Items { get { return skinsSelector[SkinType].ToList (); } }
        public SkinItem currrentSkinItem { get { return Items[SkinIndex]; } }
        public System.Action<int> OnSkinsChanged { get { return skinIndexsSelector[SkinType].OnValueChanged; } set { skinIndexsSelector[SkinType].OnValueChanged = value; } }
    }

}

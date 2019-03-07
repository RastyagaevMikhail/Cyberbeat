using GameCore;

using Sirenix.OdinInspector;

using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace CyberBeat
{
    [CreateAssetMenu (fileName = "Enums", menuName = "CyberBeat/Data/Enums")]
    public class Enums : ScriptableObject
    {
#if UNITY_EDITOR
        [Button] public void Validate ()
        {
            Type EnumScriptableType = typeof (EnumScriptable);
            var names = EnumScriptableType.Assembly
                .GetTypes ()
                .ToList ()
                .FindAll (t => t.IsSubclassOf (EnumScriptableType))
                .Select (t => Tuple.Create (t.Name, t.FullName));
            Debug.Log (names.Log ());
            enumsHash = new List<TypeEnumsList> ();
            foreach (var nameType in names)
            {
                var path = validatePathTemplate.Replace ("$EnumName$", nameType.Item1);
                List<EnumScriptable> scriptables = Tools.GetAtPath<EnumScriptable> (path).ToList ();

                enumsHash.Add (new TypeEnumsList (nameType.Item2, scriptables));
            }
            this.Save ();
        }

        [SerializeField] string validatePathTemplate = "Assets/Data/Enums/$EnumName$";

#endif      
        [SerializeField] List<TypeEnumsList> enumsHash;
        private Dictionary<Type, List<EnumScriptable>> _dict;
        private Dictionary<Type, List<EnumScriptable>> dict => _dict ?? (_dict = InitDict ());

        private Dictionary<Type, List<EnumScriptable>> InitDict ()
        {
            return enumsHash.ToDictionary (tel => Type.GetType (tel.Type), tel => tel.EnumValues);
        }
        private void OnEnable ()
        {
            InitDict ();
        }
        public List<T> GetValues<T> () where T : EnumScriptable
        {
            return GetValues (typeof (T)).Cast<T> ().ToList ();
        }
        public List<EnumScriptable> GetValues (Type type)
        {
            List<EnumScriptable> list = null;
            dict.TryGetValue (type, out list);
            return list;
        }

        public void TryAddEnumScriptable<T> (EnumScriptable enumScriptable) where T : EnumScriptable
        {
            TryAddEnumScriptable (typeof (T), enumScriptable);
        }
        public void TryAddEnumScriptable (Type type, EnumScriptable enumScriptable)
        {
            List<EnumScriptable> scriptables = null;

            dict.TryGetValue (type, out scriptables);
            if (scriptables == null) return;

            if (!scriptables.Contains (enumScriptable))
            scriptables.Add (enumScriptable);
        }

    }

    [Serializable]
    public class TypeEnumsList
    {
        [SerializeField] string type;
        [ListDrawerSettings (
            Expanded = true,
            IsReadOnly = true,
            HideAddButton = true,
            HideRemoveButton = true,
            ShowPaging = false)]
        [SerializeField] List<EnumScriptable> enumValues;

        public TypeEnumsList () { }
        public TypeEnumsList (string name, List<EnumScriptable> scriptables)
        {
            type = name;
            enumValues = scriptables;
        }

        public string Type => type;
        public List<EnumScriptable> EnumValues => enumValues;
    }
}

using Sirenix.OdinInspector;

using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace GameCore
{
    public class SaveData : ScriptableObject, IResetable
    {

        [Button]
        public void ValidateSaves ()
        {
#if UNITY_EDITOR
            Saves = Tools
                .GetAtPath<ASavableVariable> ("Assets").ToList ()
                .FindAll (asv => asv.IsSavable).ToList ();
            this.Save ();
#endif
        }
        public void ResetDefault ()
        {
            ValidateSaves ();
        }

        [ListDrawerSettings (
            Expanded = true,
            IsReadOnly = true,
            HideAddButton = true,
            HideRemoveButton = true,
            ShowPaging = false)]
        [PropertyOrder (int.MaxValue)]
        [SerializeField] List<ASavableVariable> Saves;

        Saver _saver = null;
        Saver saver
        {
            get
            {
                if (_saver == null)
                {
                    _saver = GameObject.FindObjectOfType<Saver> ();
                    if (_saver == null)
                    {
                        _saver = new GameObject ("Saver").AddComponent<Saver> ();
                        DontDestroyOnLoad (_saver.gameObject);
                    }
                }
                return _saver;
            }
        }

        public void CheckSaver ()
        {
            if (_saver) return;
            _saver = saver;
        }
        public void SaveAll ()
        {
            foreach (var save in Saves)
            {
                save.SaveValue ();
            }
        }
#if UNITY_EDITOR

        public void ResetLoaded ()
        {
            foreach (var save in Saves)
            {
                save.ResetLoaded ();
            }
        }
#endif
    }

}

using GameCore;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
    public abstract class SkinChnager : MonoBehaviour
    {
        [SerializeField] SkinIndexSelector skinIndexsSelector;
        [SerializeField] SkinsEnumDataSelector skinsSelector;
#if UNITY_EDITOR
        private void OnValidate ()
        {
            skinIndexsSelector = Tools.ValidateSO<SkinIndexSelector> ("Assets/Data/Selectors/Skins/SkinIndexSelector.asset");
            skinsSelector = Tools.ValidateSO<SkinsEnumDataSelector> ("Assets/Data/Selectors/Skins/SkinsSelector.asset");
        }
#endif

        [SerializeField] protected SkinType type;

        void Start ()
        {
            ApplySkin (skinsSelector[type][skinIndexsSelector[type].Value]);
        }

        protected abstract void ApplySkin (SkinItem skin);
    }
}

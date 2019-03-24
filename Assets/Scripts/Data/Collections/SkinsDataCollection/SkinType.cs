using GameCore;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{

   [CreateAssetMenu (fileName = "SkinType", menuName = "CyberBeat/Variable/Enum/SkinType", order = 0)]
   public class SkinType : EnumScriptable
   {
      public Material OnSeleted;
      public Material OnOpend;
      public Material OnClosed;
      [SerializeField] SkinsEnumDataSelector skinsSelector;
#if UNITY_EDITOR
      private void OnValidate ()
      {
         skinsSelector = Tools.ValidateSO<SkinsEnumDataSelector> ("Assets/Data/Selectors/Skins/SkinsSelector.asset");
      }
#endif
      public void Apply (Object obj)
      {
         foreach (var skin in skinsSelector[this])
            skin.Apply (obj);
      }
   }
}

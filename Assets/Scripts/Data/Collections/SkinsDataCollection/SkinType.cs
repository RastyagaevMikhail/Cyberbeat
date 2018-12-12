using GameCore;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{

   [CreateAssetMenu (fileName = "SkinType", menuName = "Variables/CyberBeat/Enum/SkinType", order = 0)]
   public class SkinType : EnumScriptable
   {
      public Material OnSeleted;
      public Material OnOpend;
      public Material OnClosed;
      public SkinsDataCollection skinsData { get { return SkinsDataCollection.instance; } }
      public void Apply (Object obj)
      {
         foreach (var skin in skinsData.skins[this])
            skin.Apply (obj);
      }
   }
}

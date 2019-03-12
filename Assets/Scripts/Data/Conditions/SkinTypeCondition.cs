using GameCore;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
    [CreateAssetMenu (fileName = "SkinTypeCondition", menuName = "CyberBeat/Condition/SkinType")]
    public class SkinTypeCondition : ACondition
    {
        [SerializeField] SkinType skinType;
        [SerializeField] SkinTypeVariable skinTypeVariable;
        public override bool Value => skinType.Equals (skinTypeVariable.Value);
    }
}

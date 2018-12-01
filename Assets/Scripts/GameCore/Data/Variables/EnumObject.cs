using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GameCore
{
    public class EnumObject : MonoBehaviour
    {

        public EnumScriptable type;

        public void SaveMyTypeTo(UnityObjectVariable unityObject)
		{
			if(!unityObject) return;
			unityObject.Value = type;
		}
    }
}
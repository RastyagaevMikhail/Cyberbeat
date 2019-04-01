using Sirenix.OdinInspector;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class EffectsPrefabgenerator : MonoBehaviour
{
    [SerializeField] GameObject orig;
    [SerializeField] Transform source;
    [Button] public void Generate ()
    {
        for (int i = 0; i < source.childCount; i++)
        {
            var mf = source.GetChild(i).GetComponent<MeshFilter>();
			GameObject go = Instantiate(orig);
            go.name = mf.name;
			go.GetComponent<MeshFilter>().sharedMesh = mf.sharedMesh; 
        }
    }
}

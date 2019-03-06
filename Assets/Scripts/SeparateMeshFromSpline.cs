using FluffyUnderware.Curvy.Generator;
using FluffyUnderware.Curvy.Generator.Modules;

using Sirenix.OdinInspector;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
    public class SeparateMeshFromSpline : MonoBehaviour
    {
        [SerializeField] GameObject createMeshGO;
        private void OnValidate ()
        {
            createMeshGO = GetComponentInChildren<CreateMesh> ().gameObject;
        }

        [Button] public void Separate ()
        {
            foreach (var cgMeshRes in createMeshGO.GetComponentsInChildren<CGMeshResource> ())
            {
                DestroyImmediate (cgMeshRes);
            };
            DestroyImmediate (createMeshGO.GetComponent<CreateMesh> ());
            var generator = createMeshGO.transform.parent.gameObject;
            createMeshGO.transform.SetParent (transform);
            DestroyImmediate (generator);

        }
    }
}

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
    public class MeshSkinChnager : SkinChnager
    {
        private MeshFilter _mf = null;
        public MeshFilter mf { get { if (_mf == null) _mf = GetComponent<MeshFilter> (); return _mf; } }
        private SkinnedMeshRenderer _smr = null;
        public SkinnedMeshRenderer smr { get { if (_smr == null) _smr = GetComponent<SkinnedMeshRenderer> (); return _smr; } }
        public Mesh mesh { set { if (mf) mf.sharedMesh = value; if (smr) smr.sharedMesh = value; } }
        protected override void ApplySkin (SkinItem skin)
        {
            GameObject PrefabGO = skin.Prefab as GameObject;
            var mf = PrefabGO.GetComponent<MeshFilter> ();
            var smr = PrefabGO.GetComponent<SkinnedMeshRenderer> ();

            if (mf) mesh = mf.sharedMesh;
            if (smr) mesh = smr.sharedMesh;
        }
    }
}

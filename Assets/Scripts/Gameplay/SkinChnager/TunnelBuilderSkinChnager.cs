using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace CyberBeat
{
    [RequireComponent (typeof (SkinItemTunnelBuilderSelector))]
    public class TunnelBuilderSkinChnager : SkinChnager
    {
        private SkinItemTunnelBuilderSelector _selector = null;
        public SkinItemTunnelBuilderSelector selector { get { if (_selector == null) _selector = GetComponent<SkinItemTunnelBuilderSelector> (); return _selector; } }
        protected override void ApplySkin (SkinItem skin)
        {
            selector[skin].SetActive (true);
        }
    }
}

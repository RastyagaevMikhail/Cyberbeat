using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace CyberBeat
{

    [CreateAssetMenu (fileName = "EffectSkinData", menuName = "CyberBeat/EffectSkinData", order = 0)]
    public class EffectSkinData : ScriptableObject
    {
        public Texture texture;
        public List<State> states;
        [ContextMenu ("SortSates")]
        void SortSates ()
        {
            states = states.OrderBy (s => (int) Enum.Parse (typeof (Shapes), s.Name)).ToList ();
        }
        Dictionary<Shapes, EffectSkinData.State> _states = null;
        public Dictionary<Shapes, EffectSkinData.State> States
        {
            get
            {
                return _states ??
                    (_states = states
                        .ToDictionary (s => Enums.shapes
                            .Find (Sh => Sh.ToString () == s.Name)
                        )
                    );
            }
        }

        [Serializable]
        public class State
        {
            public string Name;
            public Vector2 Tilling;

            public void IntitMaterial (Material mat)
            {
                mat.SetVector ("Tilling", Tilling);
            }
        }

    }

}

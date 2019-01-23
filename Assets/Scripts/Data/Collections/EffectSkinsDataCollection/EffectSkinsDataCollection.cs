using GameCore;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace CyberBeat
{
    public class EffectSkinsDataCollection : DataCollections<EffectSkinsDataCollection, EffectSkinData>
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem ("Game/Data/Collections/Effect Skins")] public static void Select () { UnityEditor.Selection.activeObject = instance; }

        [ContextMenu ("GenerateSkinsData")]
        void GenerateSkinsData ()
        {
            var textures = Tools.GetAtPath<Texture> ("Assets/Textures/Effects").ToList ();
            Debug.LogFormat ("textures.Count = {0}", textures.Count);
            // "Assets/Data/Skins/Effects"
            textures.Select (t =>
                    new { skin = CreateInstance<EffectSkinData> (), texture = t }).ToList ()
                .ForEach (esft =>
                {
                    Debug.LogFormat ("{0}:{1}", esft.skin, esft.texture);
                    esft.skin.texture = esft.texture;
                    esft.skin.CreateAsset ("Assets/Data/Skins/Effects/{0}.asset".AsFormat (esft.texture.name));
                });

            Objects = Tools.GetAtPath<EffectSkinData> ("Assets/Data/Skins/Effects").ToList ();
            this.Save ();
        }
#endif
        private void OnEnable ()
        {
            dict = Objects.ToDictionary (obj => obj.name);
        }
        Dictionary<string, EffectSkinData> dict = null;
        Dictionary<string, EffectSkinData> Dict { get { return dict??(dict = Objects.ToDictionary (obj => obj.name)); } }
        public EffectSkinData this [string nameSkin]
        {
            get
            {
                EffectSkinData result = null;
                Dict.TryGetValue (nameSkin, out result);
                return result;
            }
        }
    }
}

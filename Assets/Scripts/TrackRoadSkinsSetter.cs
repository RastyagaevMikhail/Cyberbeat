using GameCore;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace CyberBeat
{
    public class TrackRoadSkinsSetter : MonoBehaviour
    {
        [SerializeField] List<Renderer> rends;
        [SerializeField] TextureProperty _textureProp;
        [SerializeField] ColorProperty _colorProp;
        [SerializeField] IntVariable index;

#if UNITY_EDITOR

        private void OnValidate ()
        {
            rends = GetComponentsInChildren<Renderer> ().ToList ();
            _textureProp = Tools.ValidateSO<TextureProperty> ("Assets/Data/PropertyBlockInfo/RoadSkin.asset");
            _colorProp = Tools.ValidateSO<ColorProperty> ("Assets/Data/PropertyBlockInfo/ColorRoad.asset");
            index = Tools.ValidateSO<IntVariable> ("Assets/Data/Skins/Indexes/RoadSkinIndex.asset");
        }
#endif
        private void Awake ()
        {
            Texture currentSkin = Resources.Load<Texture> ($"Skins/Road/{index.Value+1}");
            foreach (var rend in rends)
            {
                _textureProp.OnValidte ();
                _textureProp.Init (rend);
                _textureProp.property = currentSkin;
            }
        }
        public Color color
        {
            set
            {
                foreach (var rend in rends)
                {
                    _colorProp.OnValidte ();
                    _colorProp.Init (rend);
                    _colorProp.property = value;
                }
            }
        }
    }
}

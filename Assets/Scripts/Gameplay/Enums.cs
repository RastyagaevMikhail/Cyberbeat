using GameCore;

using Sirenix.OdinInspector;

using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace CyberBeat
{
    public class Enums : SingletonData<Enums>
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem ("Game/Data/Enums")] public static void Select () { UnityEditor.Selection.activeObject = instance; }
        public override void ResetDefault () { }

        [Button]
        public override void InitOnCreate ()
        {
            layerTypes = new string[]
                {
                    "Bit",
                    "Effect",
                    "Speed",
                    "Camera",
                    "Shake"
                }
                .Select (s =>
                {
                    LayerType layer = CreateInstance<LayerType> ();
                    Tools.CreateAsset<LayerType> (layer, $"Assets/Data/Enums/LayerType/{s}.asset");
                    return layer;
                }).ToList ();

            inputTypes = new string[]
                {
                    "Tap",
                    "Swipe"
                }
                .Select (s =>
                {
                    InputType layer = CreateInstance<InputType> ();
                    Tools.CreateAsset<InputType> (layer, $"Assets/Data/Enums/InputType/{s}.asset");
                    return layer;
                }).ToList ();
        }

        public void ValidateLayerType ()
        {
            layerTypes = Tools.GetAtPath<LayerType> ("Assets/Data/Enums/LayerType").ToList ();
            instance.Save ();
        }

        public void ValidateInputType ()
        {
            inputTypes = Tools.GetAtPath<InputType> ("Assets/Data/Enums/InputType").ToList ();
            instance.Save ();
        }
#else
        public override void ResetDefault () { }
#endif
        [SerializeField][InlineButton ("ValidateLayerType", "Validate")] List<LayerType> layerTypes;
        public List<LayerType> LayerTypes => layerTypes;
        [SerializeField][InlineButton ("ValidateInputType", "Validate")] List<InputType> inputTypes;
        public List<InputType> InputTypes => inputTypes;

    }
}

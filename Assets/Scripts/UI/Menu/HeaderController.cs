    using DG.Tweening;

    using GameCore;

    using Sirenix.OdinInspector;

    using System.Collections.Generic;
    using System.Collections;

    using TMPro;

    using UnityEngine.UI;
    using UnityEngine;
    namespace CyberBeat
    {
        public class HeaderController : SerializedMonoBehaviour
        {

            private static HeaderController _instance = null;
            public static HeaderController instance { get { if (_instance == null) _instance = GameObject.FindObjectOfType<HeaderController> (); return _instance; } }

            [SerializeField] TextMeshProUGUI NotesText;

            //For any Text On ByuButton
            public void OnCantBuyAnimation (Graphic targetGraphic)
            {
                CantByColorTweenAnimation (NotesText);
                CantByColorTweenAnimation (targetGraphic);
            }
            //For me
            void CantByColorTweenAnimation (Graphic targetGraphic)
            {
                targetGraphic.DOKill (true);
                var startColor = targetGraphic.color;
                targetGraphic
                    .DOColor (Color.red, 0.1f)
                    .SetLoops (3, LoopType.Yoyo)
                    .OnComplete (() => targetGraphic.color = startColor);
            }

            [SerializeField] Gradient gradient;
            [SerializeField] Color result;
            [Range (0f, 1f)]
            [OnValueChanged ("Evaluate")]
            // [InlineButton ("Evaluate")]
            [SerializeField] float percent = 0.5f;

            public void Evaluate ()
            {
                result = gradient.Evaluate (percent);
            }
        }
    }

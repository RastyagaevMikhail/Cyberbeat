    using DG.Tweening;

    using GameCore;

    using System.Collections.Generic;
    using System.Collections;

    using TMPro;

    using UnityEngine.UI;
    using UnityEngine;
using UnityEngine.Events;

namespace CyberBeat
    {
        public class HeaderController : MonoBehaviour
        {
            [SerializeField] TextMeshProUGUI NotesText;
            [SerializeField] UnityEvent OnCantBuyEvent;

            //For any Text On ByuButton
            public void OnCantBuyAnimation (Graphic targetGraphic)
            {
                 OnCantBuyEvent.Invoke();
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
        }
    }

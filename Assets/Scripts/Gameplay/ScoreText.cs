using DG.Tweening;

using GameCore;

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using Text = TMPro.TextMeshPro;

namespace CyberBeat
{
    public class ScoreText : TransformObject
    {
        [SerializeField] Ease EaseFade = Ease.InQuint;
        [SerializeField] Ease EaseMove = Ease.OutQuint;

        [SerializeField] Text textComponent;
        [SerializeField] Rigidbody rb;

        public Color color { get { return textComponent.color; } set { textComponent.color = value; } }
        float alpha { set { color = new Color (color.r, color.g, color.b, value); } }

        [SerializeField] IntVariable ScorePerBeat;
        int countScore => ScorePerBeat.ValueFast;
        [SerializeField] float Duration = 0.5f;
        [SerializeField] float upDistance = 1f;
        [SerializeField] float startAlpha = 1f;
        Vector3[] path;
        float startLocalY;
        Tween moveTween = null;
        Tween fadeTween = null;
        protected override void Awake ()
        {
            startLocalY = yLocal;
            path = new Vector3[] { Vector3.up * (startLocalY + upDistance) };
            
            Reset ();
        }
        public void OnColorTaked (Color color)
        {
            this.color = color;
            alpha = startAlpha;

            textComponent.text = "+" + countScore;
            if (moveTween != null || fadeTween != null) Reset ();
            moveTween = rb.DOLocalPath (path, Duration).SetEase (EaseMove);
            // transform.DOLocalMoveY (yLocal + upDistance, Duration).SetEase (EaseMove);
            fadeTween = textComponent.DOFade (0, Duration).SetEase (EaseFade).OnComplete (Reset);
        }
        private void Reset ()
        {
            yLocal = startLocalY;
            alpha = 0f;
            textComponent.text = string.Empty;

            if (moveTween != null)
            {
                moveTween.Kill (true);
                moveTween = null;
            }

            if (fadeTween != null)
            {
                fadeTween.Kill (true);
                fadeTween = null;
            }
        }
    }
}

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
        private Text _textComponent = null;
        public Text textComponent { get { if (_textComponent == null) _textComponent = GetComponent<Text> (); return _textComponent; } }
        private SpawnedObject _spwnObj = null;
        public SpawnedObject spwnObj { get { if (_spwnObj == null) _spwnObj = GetComponent<SpawnedObject> (); return _spwnObj; } }
        public Color color { get { return textComponent.color; } set { textComponent.color = value; } }
        float alpha { set { color = new Color (color.r, color.g, color.b, value); } }

        [SerializeField] float Duration = 0.5f;
        [SerializeField] float upDistance = 1f;
        [SerializeField] float startAlpha = 1f;

        public void Init (int countScore, Color color)
        {
            this.color = color;
            alpha = startAlpha;

            textComponent.text = "+" + countScore;
            transform.DOLocalMoveY (yLocal + upDistance, Duration).SetEase (EaseMove);
            textComponent.DOFade (0, Duration).SetEase (EaseFade).OnComplete (Reset);

        }

        private void Reset ()
        {
            spwnObj.PushToPool ();
            // color = Color.white;
        }
    }
}

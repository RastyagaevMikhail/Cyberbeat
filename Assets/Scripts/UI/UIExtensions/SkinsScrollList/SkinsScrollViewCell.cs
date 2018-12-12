using GameCore;

using Sirenix.OdinInspector;

using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

namespace CyberBeat
{
    public class SkinsScrollViewCell : FancyScrollViewCell<SkinsScrollData, SkinsScrollContext>
    {
        SkinsDataCollection skinsData { get { return SkinsDataCollection.instance; } }

        private Animator _animator = null;
        public Animator animator { get { if (_animator == null) _animator = GetComponent<Animator> (); return _animator; } }
        private Image _Icon = null;
        public Image Icon { get { if (_Icon == null) _Icon = GetComponent<Image> (); return _Icon; } }
        private CanvasGroup _canvasGroup = null;
        public CanvasGroup canvasGroup { get { if (_canvasGroup == null) _canvasGroup = GetComponent<CanvasGroup> (); return _canvasGroup; } }

        [SerializeField] GameObject Selection;
        [SerializeField] GameObject DeSelection;

        readonly int scrollTriggerHash = Animator.StringToHash ("Scroll");
        SkinsScrollContext context;

        public override void SetContext (SkinsScrollContext context)
        {
            this.context = context;
        }

        public override void UpdatePosition (float position)
        {
            animator.Play (scrollTriggerHash, -1, position);
            animator.speed = 0;
        }

        public void OnPressedCell ()
        {
            if (context != null)
            {
                context.OnPressedCell (this);
            }
        }
        public override void UpdateContent (SkinsScrollData data)
        {
            Icon.sprite = data.Icon;
            ValidateValues ();
            // UpdatePosition (0);
        }

        [SerializeField] Color SelectedColor;
        [SerializeField] Color UnSelectedColor;
        [Button]
        private void ValidateValues ()
        {

            if (context == null) return;

            bool isSelected = context.SelectedIndex == DataIndex;
            Selection.SetActive (isSelected);
            DeSelection.SetActive (!isSelected);

            canvasGroup.alpha = isSelected ? 1f : 0.5f;
            Icon.color = isSelected ? SelectedColor : UnSelectedColor;

        }
    }
}

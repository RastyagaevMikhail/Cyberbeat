using GameCore;

using UnityEngine;
using UnityEngine.UI;

namespace CyberBeat
{
    public class SkinsScrollViewCell : MonoBehaviour
    {
        [Header ("Links")]
        [SerializeField] Image Icon;
        [SerializeField] CanvasGroup canvasGroup;
        [SerializeField] GameObject Selection;
        [SerializeField] GameObject DeSelection;
        [SerializeField] Color SelectedColor;
        [SerializeField] Color UnSelectedColor;
        [SerializeField] UnityEventInt onHighlighted;
        private int index;
        public void Init (Sprite skin, int _index, bool isSelectedCell)
        {
            Icon.sprite = skin;
            index = _index;
            ValidateValues (isSelectedCell);
        }
        public void OnPressedCell ()
        {
            onHighlighted.Invoke(index);
            ValidateValues (true);
        }
        public void OnSkinItemSelected (int _index)
        {
            ValidateValues (_index == index);
        }     
        private void ValidateValues (bool isSelected = false)
        {
            Selection.SetActive (isSelected);
            DeSelection.SetActive (!isSelected);

            canvasGroup.alpha = isSelected ? 1f : 0.5f;
            Icon.color = isSelected ? SelectedColor : UnSelectedColor;

        }
    }
}

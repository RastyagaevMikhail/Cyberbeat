using GameCore;

using UnityEngine;
namespace CyberBeat
{
    public class SkinsColorChanger : MonoBehaviour
    {
        [SerializeField] SkinType skinType;
        [SerializeField] SkinsEnumDataSelector skinsSelector;
        [SerializeField] ColorVariableSelector addColorSelector;
        [SerializeField] ColorVariableSelector stateColorSelector;
        [SerializeField] UnityEventColor addColorChanged;
        [SerializeField] UnityEventColor stateColorChanged;
        [SerializeField] bool debug;
        public SkinType CurrentSkinType
        {
            set
            {
                skinType = value;
                SendColorByIndex (true, selectedIndex);
            }
        }
        int selectedIndex;

        public void OnSkinSelected (int index)
        {
            selectedIndex = index;
            SendColorByIndex (true, index);

        }
        public void OnSkinHightlighted (int index)
        {
            SendColorByIndex (selectedIndex == index, index);
        }

        public string getColorName (bool selected, bool Bougth)
        {
            string result = "Closed";
            if (selected)
                result = Bougth ? "Selected" : "Closed";
            else
                result = Bougth ? "Highlighted" : "Closed";
            return result;
        }
        public void SendColorByIndex (bool selected, int index)
        {
            var skins = skinsSelector[skinType];
            if (!index.InRange (0, skins.Count)) return;
            var skin = skins[index];
            string key = getColorName (selected, skin.Bougth);
            SendColor (key);
        }

        private void SendColor (string key)
        {
            Color addColor;
            if (addColorSelector.TryGetValue (key, out addColor))
                addColorChanged.Invoke (addColor);

            Color stateColor;
            if (stateColorSelector.TryGetValue (key, out stateColor))
                stateColorChanged.Invoke (stateColor);
            if (debug)
            {
                Debug.Log (key);
                Debug.Log ("Add : " + addColor.Log ());
                Debug.Log ("State : " + stateColor.Log ());
            }
        }
    }
}

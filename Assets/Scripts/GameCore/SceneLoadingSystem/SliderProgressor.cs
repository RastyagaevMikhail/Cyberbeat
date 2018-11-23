using UnityEngine;
using UnityEngine.UI;

namespace GameCore
{
    public class SliderProgressor : MonoBehaviour, ILoadingProgressor
    {
        private Slider _slider = null;
        public Slider slider { get { if (_slider == null) _slider = GetComponent<Slider> (); return _slider; } }
        public float value { get { return slider.value; } set { slider.value = value; } }
    }
}

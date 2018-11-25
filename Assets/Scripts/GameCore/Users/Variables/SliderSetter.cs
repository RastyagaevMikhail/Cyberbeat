using UnityEngine;
using UnityEngine.UI;

namespace GameCore
{
    [ExecuteInEditMode]
    public class SliderSetter : MonoBehaviour
    {
        public Slider Slider;
        public FloatVariable Variable;

        private void Update ()
        {
            if (Slider != null && Variable != null)
                Slider.value = Variable.Value;
        }
    }
}

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
    public class InputControllerComponent : MonoBehaviour
    {
        [SerializeField] InputControlType CurrentInputType;

        [SerializeField] InputControlTypeAInputControllerSelector InputControllerSelector;
        private void Awake ()
        {
            InputControllerSelector.Values.ForEach (aic => aic.Awake ());
        }

        public void SetControl (InputControlType controlTypeToSwitch)
        {
            CurrentInputType = controlTypeToSwitch;
        }
        public void MoveRight ()
        {
            InputControllerSelector[CurrentInputType].MoveRight ();
        }
        public void MoveLeft ()
        {
            InputControllerSelector[CurrentInputType].MoveLeft ();
        }
    }
}

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
    public class InputControllerComponent : MonoBehaviour
    {
        [SerializeField] InputControlType CurrentInputType;
        public InputSettings inputSettings;
        [SerializeField] InputController inputController;
        private void Awake ()
        {
            inputSettings.SwipeDuration = 0.2f;
            inputController.Init (transform, inputSettings);
        }
        public void SetControl (InputControlType controlTypeToSwitch)
        {
            CurrentInputType = controlTypeToSwitch;
        }
        public void MoveRight ()
        {
            inputController[CurrentInputType].MoveRight ();
        }
        public void MoveLeft ()
        {
            inputController[CurrentInputType].MoveLeft ();
        }
    }
}

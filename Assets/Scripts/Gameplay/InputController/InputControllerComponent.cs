using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
    public class InputControllerComponent : MonoBehaviour
    {
        [SerializeField] InputControlType CurrentInputType;
        
        [SerializeField] InputControlTypeAInputControllerSelector InputControllerSelector;
       
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

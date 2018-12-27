using System;
using System.Collections.Generic;
using System.Linq;

namespace CyberBeat
{
    [Serializable]
    public class InputController
    {
        public List<TypeController> controllers;

        public AInputController this [InputControlType type]
        {
            get
            {
                TypeController typeController = controllers.Find (ctrl => ctrl.type == type);
                if (typeController != null)
                    return typeController.controller;
                return null;
            }
        }
    }

    [Serializable]
    public class TypeController
    {
        public InputControlType type;
        public AInputController controller;
    }
}

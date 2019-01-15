using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace CyberBeat
{
    [Serializable]
    public class InputController
    {
        [SerializeField] List<TypeController> controllers;
        Dictionary<InputControlType, AInputController> _dictControllers = null;
        Dictionary<InputControlType, AInputController> dictControllers
        {
            get { return _dictControllers ?? (_dictControllers = controllers.ToDictionary (c => c.type, c => c.controller)); }
        }
        public AInputController this [InputControlType type]
        {
            get
            {
                AInputController controller = null;
                dictControllers.TryGetValue (type, out controller);
                return controller;
            }
        }

        public void Init (Transform target, InputSettings inputSettings)
        {
            foreach (var item in controllers)
                item.controller.Init (target, inputSettings);
        }
    }

    [Serializable]
    public class TypeController
    {
        public InputControlType type;
        public AInputController controller;
    }
}

using GameCore;

using System;
using System.Collections.Generic;
using System.Linq;
namespace CyberBeat
{
    public static class Enums
    {
        private static List<LayerType> layerTypes;
        public static List<LayerType> LayerTypes
        {
            get
            {
                if (layerTypes == null) layerTypes = Enum.GetValues (typeof (LayerType)).Cast<LayerType> ().ToList ();
                return layerTypes;
            }

        }
         private static List<Shapes> _shapes;
        public static List<Shapes> shapes
        {
            get
            {
                if (_shapes == null) _shapes = Enum.GetValues (typeof (Shapes)).Cast<Shapes> ().ToList ();
                return _shapes;
            }

        }

        private static List<InputType> inputTypes;
        public static List<InputType> InputTypes
        {
            get
            {
                if (inputTypes == null) inputTypes = Enum.GetValues (typeof (InputType)).Cast<InputType> ().ToList ();
                return inputTypes;
            }

        }
    }
}


using System;

using UnityEngine;
namespace GameCore
{
    public class ColorReference : VariableReference<ColorVariable, Color>
    {
        public void SetValue (Color color)
        {
            if (UseConstant) ConstantValue = color;
            else Variable.Value = color;
        }
    }
}

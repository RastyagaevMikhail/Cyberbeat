﻿using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace GameCore
{
    public static class DebugStringExtensions
    {
        public static DebugLogSettings settings { get { return DebugLogSettings.instance; } }
        /// <summary>
        /// MonoBehaviour Text in  Color
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string mb (this string str)
        {
            return str.ToColor (settings.MonoBehaviourColor);
        }
        /// <summary>
        /// ScriptableObject Text in  Color
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string so (this string str)
        {
            return str.ToColor (settings.ScriptableObjectColor);
        }
        /// <summary>
        /// Action Text in Color
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string a (this string str)
        {
            return str.ToColor (settings.ActionColor);
        }
        public static string err (this string str)
        {
            return str.ToColor (settings.ErrorColor);
        }
        public static string warn (this string str)
        {
            return str.ToColor (settings.WarningColor);
        }
    }
}

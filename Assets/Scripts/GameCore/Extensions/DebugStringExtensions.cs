using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.Events;
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

        public static string Log (this UnityEventBase EventBase, object arg = null)
        {
            string log = "null";
            log = EventBase.GetType ().Name.a () + $":{(arg != null ? arg.ToString().red():string.Empty)}\n";
            int EventCount = EventBase.GetPersistentEventCount ();
            IEnumerable<string> methodsNames = Enumerable.Range (0, EventCount).Select (i => EventBase.GetPersistentMethodName (i));
            IEnumerable<string> targetsNames = Enumerable.Range (0, EventCount).Select (i => EventBase.GetPersistentTarget (i).name);
            IEnumerable<string> typeNames = Enumerable.Range (0, EventCount).Select (i => EventBase.GetPersistentTarget (i).GetType ().Name);
            var methodsInfo = targetsNames.Select ((t, i) => $"{t.black()}: {typeNames.ElementAt(i).mb()}.{methodsNames.ElementAt(i).a()} \n");
            log += string.Concat (methodsInfo);
            return log;
        }
        public static string Log<T> (this IEnumerable<T> collection) => Log (collection, i => i);
        public static string Log<T, TResult> (this IEnumerable<T> collection, System.Func<T, TResult> selector, string Format = "{0}", string separator = "\n")
        {
            string result = "";
            result = collection != null ? collection.Count ().ToString () : "Null";
            result += " items {0}\n------\n".AsFormat (typeof (T).Name);

            foreach (var item in collection.Select (selector))
            {
                var parameters = new object[] { item };
                result += string.Format (Format + separator, parameters);
            }
            return result;
        }
        public static string Log (this Color color, bool useAlpha = false)
        {
            return color.ToString (useAlpha).ToColor (color);
        }
        public static string Log (this MonoBehaviour monoBehaviour, bool useType = false)
        {
            var Components = monoBehaviour.gameObject.GetComponents<Component> ();
            return $"{(useType ? monoBehaviour.ToString().mb() : monoBehaviour.GetType().Name.mb())}[{System.Array.IndexOf(Components,monoBehaviour)}]";
        }
        public static string Log (this ScriptableObject scriptableObject, bool useType = false)
        {
            return $"{(useType ? scriptableObject.ToString().so() : scriptableObject.GetType().Name.mb())}";
        }
        public static string Log (this System.Action method)
        {
            return $"{(method!= null?method.ToString().a():"null".err())}";
        }
        public static string Log (this System.Action<double, string> method)
        {
            return $"{(method!= null?method.ToString().a():"null".err())}";
        }
    }
}

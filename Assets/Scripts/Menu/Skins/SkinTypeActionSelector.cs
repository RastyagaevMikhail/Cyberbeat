using GameCore;

using System;

namespace CyberBeat
{
    public class SkinTypeActionSelector : EnumActionSelector<SkinType>
    {
        public void InvokeAll ()
        {
            foreach (var action in actions)
                action.action.Invoke ();
        }
    }
}

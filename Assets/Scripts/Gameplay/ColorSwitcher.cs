using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace CyberBeat
{
    public class ColorSwitcher : ColorInterractor
    {
        public override void OnPlayerContact ()
        {
            if (player == null) return;

            Death ();
            
            player.SetColor (matSwitch.CurrentColor);
        }

    }
}

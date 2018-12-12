using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace CyberBeat
{
    public class ColorSwitcher : ColorInterractor
    {
        public override void OnPlayerContact (GameObject go)
        {
            base.OnPlayerContact (go);
            if (player == null) return;

            GameData.instance.OnDestroyedBrick ();

            Death ();
            
            player.SetColor (matSwitch.CurrentColor);
        }

    }
}

using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace CyberBeat
{
    public class ColorSwitcher : ColorInterractor
    {

        public override void OnSpawn ()
        {

        }
        Player player { get { return Player.instance; } }

        public override void OnPlayerContact ()
        {

            // if (matSwitch.Constant)
            // {
            GameData.instance.OnDestroyedBrick ();
            // }

            player.SetColor (matSwitch.CurrentColor);
            Death ();
        }

    }
}

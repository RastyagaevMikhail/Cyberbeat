using System;
using System.Collections;
using System.Collections.Generic;

using DG.Tweening;

using UnityEngine;
using UnityEngine.Events;


namespace CyberBeat
{
    public class ColorBrick : ColorInterractor
    {
        Player player { get { return Player.instance; } }

        public override void OnPlayerContact ()
        {
            bool isEqualsColors = player.matSwitch.ChechColor (matSwitch.CurrentColor);
            if (isEqualsColors)
            {
                GameData.instance.OnDestroyedBrick ();
                Death ();
            }
            else
            {
                player.Death ();
            }
        }
    }
}

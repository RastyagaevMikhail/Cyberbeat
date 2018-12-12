using DG.Tweening;

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace CyberBeat
{
    public class ColorBrick : ColorInterractor
    {
        public override void OnPlayerContact (GameObject go)
        {
         base.OnPlayerContact (go);
            if (player == null) return;
            bool isEqualsColors = player.ChechColor (matSwitch.CurrentColor);
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

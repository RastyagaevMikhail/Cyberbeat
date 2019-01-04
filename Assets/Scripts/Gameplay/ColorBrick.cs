using UnityEngine;

namespace CyberBeat
{
    public class ColorBrick : ColorInterractor
    {
        public override void OnPlayerContact ()
        {
            if (player == null)
            {
                return;
            }
            Death ();

            bool isEqualsColors = player.ChechColor (matSwitch.CurrentColor);
            if (!isEqualsColors)
            {
                player.Death ();
            }
        }
        public override void OnDeSpawn ()
        {
            base.OnDeSpawn();
            localScale = Vector3.one;
        }
    }
}

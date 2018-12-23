
using UnityEngine;

namespace CyberBeat
{
    public class ColorBrick : ColorInterractor
    {
        protected Animator _animator = null;
        public virtual Animator animator { get { if (_animator == null) { _animator = GetComponent<Animator>(); } return _animator; } }
        public override void OnPlayerContact(GameObject go)
        {
            base.OnPlayerContact(go);
            if (player == null)
            {
                return;
            }

            bool isEqualsColors = player.ChechColor(matSwitch.CurrentColor);
            GameData.instance.OnDestroyedBrick();
            animator.Play("Scale");
            Death();
            if (isEqualsColors)
            {
            }
            else
            {
                player.Death();

            }
        }
        public void OnDeSpawn()
        {
            localScale = Vector3.one;
        }
    }
}

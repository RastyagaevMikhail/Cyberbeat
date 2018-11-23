using System;

namespace CyberBeat
{
    public class AuthorsContext
    {
        public int SelectedIndex { get; internal set; }

        internal void OnPressedCell(AuthorsViewCell authorsViewCell)
        {
            throw new NotImplementedException();
        }
    }
}
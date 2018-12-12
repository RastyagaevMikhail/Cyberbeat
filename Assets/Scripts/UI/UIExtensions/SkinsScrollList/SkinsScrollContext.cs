namespace CyberBeat
{
    public class SkinsScrollContext
    {
        public System.Action<SkinsScrollViewCell> OnPressedCell;
        public SkinsDataCollection skinsData { get { return SkinsDataCollection.instance; } }
        public int SelectedIndex ;
    }
}

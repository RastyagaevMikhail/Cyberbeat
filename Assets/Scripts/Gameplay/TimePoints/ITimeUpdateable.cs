using System.Collections.Generic;
namespace CyberBeat
{
    public interface ITimeUpdateable
    {
        void Start ();
        void UpdateInTime (float time);
        bool TimesIsOver { get; }
        IEnumerable<ITimeItem> TimeItems { get; }
    }
}

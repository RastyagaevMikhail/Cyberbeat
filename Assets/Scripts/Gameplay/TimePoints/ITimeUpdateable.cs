using System.Collections.Generic;
namespace CyberBeat
{
    public interface ITimeUpdateable
    {
        bool Start ();
        void UpdateInTime (float time);
        bool TimesIsOver { get; }
        IEnumerable<ITimeItem> TimeItems { get; }
    }
}

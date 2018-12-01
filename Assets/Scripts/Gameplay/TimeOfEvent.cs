using SonicBloom.Koreo;
using GameCore;
namespace CyberBeat
{
    [System.Serializable]
    public class TimeOfEvent
    {
        public TimeOfEvent (KoreographyEvent e, float SampleRate)
        {
            Start = (float) e.StartSample / SampleRate;
            End = (float) e.EndSample / SampleRate;
        }
        public float Start;
        public float End;
        public bool InRange (float time)
        {
            return time.InRange(Start,End);
        }
        public override string ToString ()
        {
            return string.Format ("{0:00.0000} - {1:00.0000}", Start, End);
        }
    }
}

using GameCore;

using SonicBloom.Koreo;
namespace CyberBeat
{
    [System.Serializable]
    public class TimeOfEvent
    {
        public TimeOfEvent (KoreographyEvent e, float SampleRate)
        {
            Start = (float) e.StartSample / SampleRate;
            End = (float) e.EndSample / SampleRate;
            payload = e.GetTextValue ();
        }
        public bool InRange (float time)
        {
            return time.InRange (Start, End);
        }
        public override string ToString ()
        {
            return string.Format ("{0:00.0000} - {1:00.0000} {2}", Start, End, payload);
        }
        public string payload;
        public float Start;
        public float End;
        float time { get { return (End - Start).Abs (); } }
        string timeStr { get { return "{0:00.0000}:".AsFormat (time); } }

        float OneShotTime { get { return Start; } }
        bool OneIsShotEvent { get { return Start == End; } }
        public static bool operator == (TimeOfEvent left, TimeOfEvent right)
        {
            return !object.ReferenceEquals (left, null) &&
                !object.ReferenceEquals (right, null) &&
                left.Start == right.Start &&
                left.End == right.End;
        }

        public static bool operator != (TimeOfEvent left, TimeOfEvent right)
        {
            return !(left == right);
        }
    }
}

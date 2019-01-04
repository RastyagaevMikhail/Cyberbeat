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

        public override bool Equals (object obj)
        {
            var other = obj as TimeOfEvent;
            if (other == null)
            {
                return false;
            }

            return other.Start == Start && other.End == End;
        }

        public override int GetHashCode ()
        {
            return Start.GetHashCode () + End.GetHashCode ();
        }

    }
}

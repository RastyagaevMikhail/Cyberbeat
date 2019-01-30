using Microsoft.VisualBasic;
using SonicBloom.Koreo;
namespace CyberBeat
{
    public interface IBitData  : ITimeItem
    {
        void Init (KoreographyEvent koreographyEvent);
        float StartTime { get; }
        float EndTime { get; }
        float Duration { get; }

        IPayloadData PayloadData { get; }
        string RandomString { get; }
        string StringValue { get; }
        string[] Strings { get; }
    }
}

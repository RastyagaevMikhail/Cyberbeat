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
        int RandomInt { get; }
        int[] Ints { get; }
        string RandomString { get; }
        int IntValue { get; }
        string StringValue { get; }
        string[] Strings { get; }
    }
}

using SonicBloom.Koreo;
namespace CyberBeat
{
    public interface IBitData  : ITimeItem
    {
        void Init (KoreographyEvent koreographyEvent);
        float BitTime { get; }

        IPayloadData PayloadData { get; }
        int RandomInt { get; }
        string RandomString { get; }
        int IntValue { get; }
        string StringValue { get; }
    }
}

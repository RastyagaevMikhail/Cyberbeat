using SonicBloom.Koreo;
namespace CyberBeat
{
    public interface IPayloadData
    {
        void Init (IPayload payload);
        int RandomInt { get; }
        string RandomString { get; }
        int IntValue { get; }
        string StringValue { get; }
        int[] Ints { get; }
        string[] Strings { get; }

    }
}

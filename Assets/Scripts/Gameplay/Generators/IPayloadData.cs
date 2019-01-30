using SonicBloom.Koreo;
namespace CyberBeat
{
    public interface IPayloadData
    {
        void Init (IPayload payload);
        string RandomString { get; }
        string StringValue { get; }
        string[] Strings { get; }

    }
}

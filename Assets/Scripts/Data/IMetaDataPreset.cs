namespace CyberBeat
{
    public interface IMetaDataPreset<TMetaData> 
        where TMetaData : GameCore.IMetaData
        {
            TMetaData Data { get; }
        }

    public interface IMetaDataPreset
    {
        object Data { get; }
    }
}

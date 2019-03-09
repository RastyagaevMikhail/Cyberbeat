using GameCore;

using SonicBloom.Koreo;
namespace CyberBeat
{

    public static class KoreoTools
    {
        public static void OpenKoreographyEditor (Koreography koreography, KoreographyTrack koreographyTrack)
        {
#if UNITY_EDITOR
            var methodInfo = Tools.EditorAssembliesHelper.GetMethodInCalss ("CyberBeat.KoreoEditorTools", "OpenEditorByDifficulty");

            methodInfo.Invoke (null, new object[] { koreography, koreographyTrack });
#endif
        }

        public static float GetDurationTime (this KoreographyEvent _event, int sampleRate)
        {
            return (float) (_event.EndSample - _event.StartSample) / (float) sampleRate;
        }
    }
}

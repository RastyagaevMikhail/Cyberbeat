using GameCore;

using SonicBloom.Koreo;
namespace CyberBeat
{

    public static class KoreoTools
    {
        public static void OpenKoreographyEditor(Koreography koreography, KoreographyTrack koreographyTrack)
        {
#if UNITY_EDITOR
            var methodInfo = Tools.EditorAssembliesHelper.GetMethodInCalss("CyberBeat.KoreoEditorTools", "OpenEditorByDifficulty");

            methodInfo.Invoke(null, new object[] { koreography, koreographyTrack });
#endif
        }

    }
}

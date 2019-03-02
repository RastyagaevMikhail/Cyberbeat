#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("4/fNvqfgmhqd+XMxFcBxB+Bp6IbsigjbsxXVOpsiO/j+U2m3VsUQZq+ZXDqpBFVUxTajPBbw7IC31m3X9BRh4IZTltVKxJGJz/LHHMIcV4qFC0iqTxyFgfGO64a+/OFbLWS0YMC5bCiFKDha50BmuTJbDEjALKS/aJq595dIIXISFAQlcRy/lr7DYl4V96H6zbMnTzfjCTiNpZ7YT/ahxOA6fe7a03aP6xznywa1wFoyNaONGoemqF0iuYXz9zkPaZ6ciAMX0ymPLXqg2O06i4jgAMRliAABMFrS0v9Nzu3/wsnG5UmHSTjCzs7Oys/MTc7Az/9NzsXNTc7Oz1q60XGQCLg+FL/XKPFUeT0uyMHl1c1gVBtEsThnKy15OEVRss3Mzs/O");
        private static int[] order = new int[] { 4,2,10,7,9,12,12,13,13,13,11,13,13,13,14 };
        private static int key = 207;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif

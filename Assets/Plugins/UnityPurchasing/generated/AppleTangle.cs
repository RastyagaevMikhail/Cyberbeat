#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class AppleTangle
    {
        private static byte[] data = System.Convert.FromBase64String("BU/9fglPcXl8KmJwfn6Ae3t8fX4m2Hp2A2g/KW5hC6zI9FxEONyqEMpF0otwcX/tdM5eaVELqkNypB1pOgFgMxQv6T72uwsddG/8PvhM9f5yeXZV+Tf5iHJ+fnp6f3z9fn5/I/AM/h+5ZCR2UO3Nhzs3jx9H4WqKcOJCjFQ2V2W3gbHKxnGmIWOptEILFxANFgsGTmlPa3l8Knt8bHI+D9ejAV1KtVqqpnCpFKvdW1xuiN7TdyFP/X5ueXwqYl97/X53T/1+e0+/HEwIiEV4UymUpXBecaXFDGYwyrZmDYoicaoAIOSNWnzFKvAyInKO/2tUrxY46wl2gYsU8lE/2Yg4MgBpT2t5fCp7fGxyPg8PExpfLRAQCwgIUR4PDxMaURwQElAeDw8TGhwe/X5/eXZV+Tf5iBwben5P/o1PVXlZT1t5fCp7dGxiPg8PExpfPBoNC2DupGE4L5R6kiEG+1KUSd0oMyqTExpfNhEcUU5ZT1t5fCp7dGxiPg9fEBlfCxcaXwsXGhFfHg8PExYcHhYZFhweCxYQEV8+CgsXEA0WCwZODxMaXzwaDQsWGRYcHgsWEBFfPgp5fCpicXtpe2tUrxY46wl2gYsU8janCeBMaxreCOu2Un18fn9+3P1+DxMaXy0QEAtfPD5PYWhyT0lPS01QT/68eXdUeX56enh9fU/+yWX+zOrhBXPbOPQkq2lITLS7cDKxaxauen98/X5wf0/9fnV9/X5+f5vu1nYtGhMWHhEcGl8QEV8LFxYMXxwaDaZJAL74KqbY5sZNPYSnqg7hAd4tXzw+T/1+XU9yeXZV+Tf5iHJ+fn4LFhkWHB4LGl8dBl8eEQZfDx4NCxtKXGo0aiZizOuIiePhsC/FvicvyGTC7D1bbVW4cGLJMuMhHLc0/2hJ5jNSB8iS8+SjjAjkjQmtCE8wvk/9e8RP/Xzc33x9fn19fn1Pcnl2Vfk3+Yhyfn56en9PHU50T3Z5fCpTXxwaDQsWGRYcHgsaXw8QExYcBniTAkb89CxfrEe7zsDlMHUUgFSDQlkYX/VMFYhy/bChlNxQhiwVJBsY8HfLX4i001NfEA/JQH5P88g8sMGLDOSRrRtwtAYwS6fdQYYHgBS3AD7X54autRnjWxRur9zEm2RVvGDU3A7tOCwqvtBQPsyHhJwPspncM3dUeX56enh9fmlhFwsLDwxFUFAIW52UrsgPoHA6nli1jhIHkpjKaGhMSSVPHU50T3Z5fCp7eWx9KixObF8eERtfHBoNCxYZFhweCxYQEV8P9Gb2oYY0E4p41F1PfZdnQYcvdqxRP9mIODIAdyFPYHl8KmJce2dPaU9ueXwqe3VsdT4PDxMaXzYRHFFOBl8eDAwKEhoMXx4cHBoPCx4RHBp5T3B5fCpibH5+gHt6T3x+foBPYhEbXxwQERsWCxYQEQxfEBlfCgwazk8nkyV7TfMXzPBioRoMgBghGsN7eWx9KixObE9ueXwqe3VsdT4PD0pNTktPTEklaHJMSk9NT0ZNTktPHRMaXwwLHhEbHg0bXwsaDRIMXx5g+vz6ZOZCOEiN1uQ/8VOrzu9tpw0eHAsWHBpfDAseCxoSGhELDFFPL9X1qqWbg692eEjPCgpe");
        private static int[] order = new int[] { 28,52,23,25,47,55,22,55,54,33,57,19,32,32,36,34,57,34,25,41,41,47,30,27,30,37,37,36,38,58,32,39,41,59,48,38,52,44,51,56,47,50,48,50,51,53,54,53,57,56,59,53,58,59,59,58,58,57,58,59,60 };
        private static int key = 127;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif

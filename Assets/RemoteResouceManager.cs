    using System.Collections.Generic;
    using System.Collections;
    using System.IO;
    using System.Xml.Serialization;
    using System;

    using UnityEngine.Networking;
    using UnityEngine;

    [CreateAssetMenu]
    public class RemoteResouceManager : ScriptableObject
    {
        private static RemoteResouceManager instance = null;
        public static RemoteResouceManager Instance
        {
            get
            {
                if (instance == null)
                    instance = Resources.Load<RemoteResouceManager> ("RemoteResouceManager");
                if (instance == null)
                    instance = CreateInstance<RemoteResouceManager> ();
#if UNITY_EDITOR
                UnityEditor.AssetDatabase.CreateAsset (instance, "Assets/Resources/RemoteResouceManager.asset");
#endif
                return instance;
            }
        }

        [SerializeField] string Uri = @"http://michaelershov.com/Mila_Sketch_AR/videos_mila.xml";
        public List<ArVideo> Videos;
        private float progressNormalized;
        public static float ProgressNormalized { get { return Instance.progressNormalized; } }
        public event Action<float> OnProgressNormalizedChange;
        public static readonly string CachePath = Application.persistentDataPath + @"/";

        private bool loadInChache (int Version)
        {
            bool sucsses = false;
            int currentCountFiles = Directory.GetFiles (RemoteResouceManager.CachePath).Length;

            if (currentCountFiles == 0)
            {
                sucsses = false;
            }
            else
            {
                Videos = new List<ArVideo> ();
                for (int i = 0, j = 0; i < currentCountFiles; i++, j++)
                {
                    Videos.Add (new ArVideo (Version, i + 1, i + j + 1, i + j + 2));
                }
                sucsses = true;
            }

            return sucsses;
        }
        public static bool LoadInChache (int Version)
        {
            return Instance.loadInChache (Version);
        }

        private ArVideo getByUrl (string url)
        {
            foreach (var arvideo in Videos)
            {
                if (arvideo.Video.Url == url)
                {
                    return arvideo;
                }
            }
            return null;
        }
        public static ArVideo GetByUrl (string url)
        {
            return Instance.getByUrl (url);
        }

        public static void LoadAll (System.Action onLoad, System.Action<string> onError)
        {
            Instance.loadAll (onLoad, onError);
        }
        private void loadAll (System.Action onLoad, System.Action<string> onError)
        {
            //TODO: Videos.Xml
            //Example: http://site.ru/videos.xml
            UnityWebRequest www = UnityWebRequest.Get (Uri);
            var request = www.SendWebRequest ();
            request.completed += (operation) =>
            {
                if (www.isHttpError || www.isNetworkError)
                {
                    if (onError != null)
                    {
                        onError.Invoke (www.error);
                    }
                    return;
                }

                XmlSerializer serializer = new XmlSerializer (typeof (List<ArVideo>));
                Videos = (List<ArVideo>) serializer.Deserialize (new MemoryStream (www.downloadHandler.data));

                if (Videos != null)
                {
                    Queue<ArVideo> videos = new Queue<ArVideo> (Videos);

                    Action<ArVideo> onLoadVideo = null;

                    onLoadVideo = (ar) =>
                    {
                        progressNormalized += 1f / Videos.Count;
                        if (OnProgressNormalizedChange != null)
                        {
                            OnProgressNormalizedChange.Invoke (progressNormalized);
                        }

                        if (videos.Count > 0)
                        {
                            var v = videos.Dequeue ();
                            v.OnLoad += onLoadVideo;
                            v.Load ();
                        }
                        else
                        {
                            if (onLoad != null)
                            {
                                onLoad.Invoke ();
                            }
                        }
                    };

                    ArVideo video = videos.Dequeue ();
                    video.OnLoad += onLoadVideo;
                    video.Load ();

                }
            };
        }

    }

    [System.Serializable]
    public class ArVideo
    {
        public int Id;
        public int Version;
        public RemoteResouce Video;
        public RemoteResouce Audio;
        public bool IsLoad
        {
            get
            {
                return Video.IsLoad && Audio.IsLoad;
            }
        }

        public event Action<ArVideo> OnLoad;
        public ArVideo (int defaultVersion, int ID, int IDVVideo, int IDAudio)
        {
            Version = defaultVersion;
            Id = ID;
            Video = new RemoteResouce (Version, IDVVideo);
            Audio = new RemoteResouce (Version, IDAudio);
        }
        public ArVideo ()
        {
            Video = new RemoteResouce ();
            Audio = new RemoteResouce ();
        }
        public void Load ()
        {
            Action<RemoteResouce> loadHandler = (ar) =>
            {
                if (IsLoad)
                {
                    if (OnLoad != null)
                    {
                        OnLoad (this);
                    }
                }
            };

            Video.OnLoad += loadHandler;
            Audio.OnLoad += loadHandler;

            Video.Load (Version);
            Audio.Load (Version);
        }
    }

    [System.Serializable]
    public class RemoteResouce
    {
        public int Id;
        public string Url;
        public string LocalPath;

        public RemoteResouce () { }

        public RemoteResouce (int version, int id)
        {
            Id = id;
            Load (version);
        }

        public bool IsLoad { get; set; }

        public event Action<RemoteResouce> OnLoad;

        public void Load (int version)
        {
            foreach (var file in Directory.GetFiles (RemoteResouceManager.CachePath))
            {
                if (file.EndsWith (GetFileName (version)))
                {
                    if (file.Contains (GetFileName (version)))
                    {
                        Debug.Log ("Load from cache" + Url);
                        IsLoad = true;
                        LocalPath = RemoteResouceManager.CachePath + GetFileName (version);
                        if (OnLoad != null)
                        {
                            OnLoad (this);
                        }
                        return;
                    }
                    else
                    {
                        File.Delete (file);
                    }
                }
            }

            Debug.Log ("Load from server" + Url);

            UnityWebRequest www = UnityWebRequest.Get (Url);
            var request = www.SendWebRequest ();
            if (www.isHttpError || www.isNetworkError)
                return;
            request.completed += (operation) =>
            {

                using (var cacheFile = File.Open (RemoteResouceManager.CachePath + GetFileName (version), FileMode.Create))
                {
                    cacheFile.Write (www.downloadHandler.data, 0, www.downloadHandler.data.Length);
                }

                IsLoad = true;
                LocalPath = RemoteResouceManager.CachePath + GetFileName (version);
                if (OnLoad != null)
                {
                    OnLoad (this);
                }
            };
        }

        private string GetFileName (int version)
        {
            return string.Intern ("resource_" + Id + "_" + version + Path.GetExtension (Url));
        }
    }

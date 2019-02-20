using EZCameraShake;

using GameCore;

using Sirenix.OdinInspector;

using System;

using Timers;

using UnityEngine;
namespace CyberBeat
{
    [System.Serializable]
    public class ShakeData : GameCore.IMetaData
    {
        [Header ("Интенсивность встряхивания")]
        [SerializeField] float magnitude;
        [Header ("Резкость встряхивания.")]
        [Tooltip ("Более низкие значения более плавные, более высокие значения более резкие")]
        [SerializeField] float roughness;

        [BoxGroup ("Time")]
        [Header ("Вычитать время ?")]
        [SerializeField] bool substructTimes;
        [BoxGroup ("Time")]
        [Header ("Время разгона тряски")]
        [Tooltip ("Как долго разгоняется тряска, в секундах")]
        [SerializeField] float fadeInTime;
        [BoxGroup ("Time")]
        [Header ("Время затухания тряски")]
        [Tooltip ("Как долго исчезает тряска, через несколько секунд")]
        [SerializeField] float fadeOutTime;
        [Header ("Сила перемещения")]
        [Tooltip ("Насколько это встряска влияет на положение.")]
        [SerializeField] Vector3 posInfluence;
        [Header ("Сила вращения")]
        [Tooltip ("Насколько это встряска влияет на вращение.")]
        [SerializeField] Vector3 rotInfluence;
        [Header ("Время, которое вычисляется автоматически")]
        [SerializeField]
        float timeDuaration;
        private CameraShakeInstance shakeInstance;

        public float TimeDuaration { get => timeDuaration; set => timeDuaration = value; }
        public float FadeInTime { get => fadeInTime; }
        public float FadeOutTime { get => fadeOutTime; }

        public void ShakeOnce (CameraShaker shaker)
        {
            shakeInstance = shaker.ShakeOnce (magnitude, roughness, FadeInTime, 0f, posInfluence, rotInfluence);

            float timeDelay = substructTimes ? (timeDuaration - fadeInTime - fadeOutTime) : timeDuaration;
            shaker.DelayAction (timeDelay, FadeOutInShakerInstance);
        }
        public void FadeOutInShakerInstance ()
        {
            if (shakeInstance == null) return;
            shakeInstance.StartFadeOut (fadeOutTime);
        }
        public override string ToString ()
        {
            return $"timeDuaration:{timeDuaration}" +
                $"magnitude:{magnitude}\n" +
                $"roughness:{roughness}\n" +
                $"substructTimes:{substructTimes}\n" +
                $"fadeInTime:{FadeInTime}\n" +
                $"fadeOutTime:{FadeOutTime}\n" +
                $"posInfluence:{posInfluence}\n" +
                $"rotInfluence:{rotInfluence}\n";
        }
    }
}

using EZCameraShake;

using Sirenix.OdinInspector;

using System;

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
        [Header ("Использовать процент")]
        [Tooltip ("Fade In Time = Time Duration * Precent;\nFade Out Time = Time Duration * (1f- Percent);")]
        [SerializeField] bool usePercent;
        [BoxGroup ("Time")]
        [HideIf ("usePercent")]
        [Header ("Время разгона тряски")]
        [Tooltip ("Как долго разгоняется тряска, в секундах")]
        [SerializeField] float fadeInTime;
        [BoxGroup ("Time")]
        [HideIf ("usePercent")]
        [Header ("Время затухания тряски")]
        [Tooltip ("Как долго исчезает тряска, через несколько секунд")]
        [SerializeField] float fadeOutTime;

        [BoxGroup ("Time")]
        [ShowIf ("usePercent")]
        [Header ("Процент для времени")]
        [Tooltip ("Fade In Time = Time Duration * Precent;\nFade Out Time = Time Duration * (1f- Percent);")]
        [Range (0, 1f)]
        [SerializeField] float percent = 0.5f;
        [Header ("Сила перемещения")]
        [Tooltip ("Насколько это встряска влияет на положение.")]
        [SerializeField] Vector3 posInfluence;
        [Header ("Сила вращения")]
        [Tooltip ("Насколько это встряска влияет на вращение.")]
        [SerializeField] Vector3 rotInfluence;
        [Header ("Время, которое вычисляется автоматически")]
        [SerializeField]
        float timeDuaration;
        public float TimeDuaration { get => timeDuaration; set => timeDuaration = value; }
        public float FadeInTime { get => usePercent & timeDuaration > 0f? timeDuaration * percent : fadeInTime; }
        public float FadeOutTime { get => usePercent & timeDuaration > 0f ? timeDuaration * (1f - percent) : fadeOutTime; }

        public void ShakeOnce (CameraShaker shaker)
        {
            shaker.ShakeOnce (magnitude, roughness, FadeInTime, FadeOutTime, posInfluence, rotInfluence);
            Vibration.Vibrate (timeDuaration);
        }
        public override string ToString ()
        {
            return $"magnitude:{magnitude}\n" +
                $"roughness:{roughness}\n" +
                $"fadeInTime:{FadeInTime}\n" +
                $"fadeOutTime:{FadeOutTime}\n" +
                $"posInfluence:{posInfluence}\n" +
                $"rotInfluence:{rotInfluence}\n";
        }
    }
}

using Cinemachine;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace HoofGame.Cameras
{
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public class CinemachineShake : MonoBehaviour
    {
        private CinemachineBasicMultiChannelPerlin _channelPerlin;

        private void Awake()
        {
            _channelPerlin = GetComponent<CinemachineVirtualCamera>()
                .GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }

        public async void Shake(float intensity, float duration)
        {
            _channelPerlin.m_AmplitudeGain = intensity;
            await UniTask.Delay(TimeSpan.FromSeconds(duration));
            _channelPerlin.m_AmplitudeGain = 0f;
        }
    }
}

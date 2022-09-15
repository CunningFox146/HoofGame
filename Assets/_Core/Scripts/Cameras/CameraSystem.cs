using System;
using System.Collections.Generic;
using UnityEngine;

namespace HoofGame.Cameras
{
    public class CameraSystem : MonoBehaviour
    {
        [SerializeField] private List<CameraInfo> _cameras;

        private void Awake()
        {
            SetCamera(CameraType.Start);
        }

        public void SetCamera(CameraType cameraType)
        {
            foreach (var cameraInfo in _cameras)
            {
                cameraInfo.camera.SetActive(cameraInfo.cameraType == cameraType);
            }
        }

        public void StartEndCamera()
        {
            //Rotate camera
            SetCamera(CameraType.End);
        }

        [Serializable]
        public struct CameraInfo
        {
            public CameraType cameraType;
            public GameObject camera;
        }
    }

    [Serializable]
    public enum CameraType
    {
        Start,
        Gameplay,
        End
    }
}

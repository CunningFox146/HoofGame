using System;
using UnityEngine;

namespace HoofGame.Horse
{
    [RequireComponent(typeof(MeshRenderer))]
    public class Hoof : MonoBehaviour
    {
        [SerializeField] private Vector2 _cleanSize;

        private MeshRenderer _meshRenderer;
        private Material _material;
        private Texture2D _mask;

        private int _dirtyPixelsCount;
        private int _pixelsCount;
        private Color32[] _savedPixels;

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _material = _meshRenderer.material;
        }

        private void Start()
        {
            CreateTexture();
        }

        public void Clean(Vector2 textureCoord)
        {
            Debug.Log(textureCoord);
            int pixelX = (int)(textureCoord.x * _mask.width);
            int pixelY = (int)(textureCoord.y * _mask.height);

            for (int x = Mathf.FloorToInt(-_cleanSize.x * 0.5f); x <= Mathf.FloorToInt(_cleanSize.x * 0.5f); x++)
            {
                for (int y = Mathf.FloorToInt(-_cleanSize.y * 0.5f); y <= Mathf.FloorToInt(_cleanSize.y * 0.5f); y++)
                {
                    Color pixelDirtMask = _mask.GetPixel(pixelX + x, pixelY + y);
                    if (pixelDirtMask != Color.black)
                    {
                        _dirtyPixelsCount--;
                        _mask.SetPixel(pixelX + x,
                            pixelY + y,
                            Color.black);
                    }
                }
            }

            _mask.Apply();
        }
        public void SaveState()
        {
            _savedPixels = _mask.GetPixels32(0);
        }

        public void RollbackState()
        {
            _mask.SetPixels32(_savedPixels);
            _mask.Apply();
        }

        private void CreateTexture()
        {
            var mainText = _material.GetTexture("_MainTex");
            _mask = new Texture2D(mainText.width, mainText.height);
            for (int y = 0; y < _mask.height; y++)
            {
                for (int x = 0; x < _mask.width; x++)
                {
                    _mask.SetPixel(x, y, Color.white);
                }
            }
            _mask.Apply();
            _material.SetTexture("_Mask", _mask);
            _pixelsCount = _dirtyPixelsCount = _mask.height * _mask.width;
        }

    }
}

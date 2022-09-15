using System.Linq;
using UnityEngine;

namespace HoofGame.Horse
{
    public class Hoof : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private Vector2 _cleanSize;

        private Texture2D _mask;
        private int _dirtyPixelsCount;
        private int _pixelsCount;
        private Color32[] _savedPixels;

        public float CleanPercent => Mathf.Max(0f, _dirtyPixelsCount / (float)_pixelsCount);

        private void Start()
        {
            CreateTexture();
        }

        public void Clean(Vector2 textureCoord)
        {
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
            _savedPixels = _mask?.GetPixels32(0);
        }

        public void RollbackState()
        {
            _mask.SetPixels32(_savedPixels);
            _mask.Apply();
        }

        private void CreateTexture()
        {
            var material = _meshRenderer.material;
            var mainText = material.GetTexture("_MainTex");
            var alphaTex = (Texture2D)material.GetTexture("_AlphaMask");

            _mask = new Texture2D(mainText.width, mainText.height);
            for (int y = 0; y < _mask.height; y++)
            {
                for (int x = 0; x < _mask.width; x++)
                {
                    _mask.SetPixel(x, y, Color.white);
                }
            }

            _mask.Apply();
            material.SetTexture("_Mask", _mask);

            _pixelsCount = _dirtyPixelsCount = alphaTex.GetPixels().Count(e => e == Color.white);
        }

    }
}

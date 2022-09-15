using HoofGame.Infrastructure;
using HoofGame.InputActions;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(MeshRenderer))]
public class Test : MonoBehaviour
{
    [SerializeField] private Vector2 _cleanSize;

    private MeshRenderer _meshRenderer;
    private Material _material;
    private Texture2D _mask;

    private GameplayInputActions _inputActions;
    private bool _clicked;
    private int _dirtyPixelsCount;
    private int _pixelsCount;

    private Vector2 ClickPos => _inputActions.Hoove.Position.ReadValue<Vector2>();

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _material = _meshRenderer.material;
    }

    private void Start()
    {
        _inputActions = GameplaySystemsContainer.Instance.GameplayInputActions;

        _inputActions.Hoove.Enable();
        _inputActions.Hoove.Click.performed += OnClickHandler;
        _inputActions.Hoove.Click.canceled += OnClickCanceledHandler;

        CreateTexture();
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

    private void OnClickCanceledHandler(InputAction.CallbackContext obj)
    {
        _clicked = false;
    }

    private void OnClickHandler(InputAction.CallbackContext obj)
    {
        _clicked = true;
    }

    private void Update()
    {
        Debug.Log(_dirtyPixelsCount / (float)_pixelsCount);
        if (!_clicked) return;
        var ray = Camera.main.ScreenPointToRay(ClickPos);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector2 textureCoord = hit.textureCoord;

            int pixelX = (int)(textureCoord.x * _mask.width);
            int pixelY = (int)(textureCoord.y * _mask.height);

            for (int x = 0; x < _cleanSize.x; x++)
            {
                for (int y = 0; y < _cleanSize.y; y++)
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
    }

}

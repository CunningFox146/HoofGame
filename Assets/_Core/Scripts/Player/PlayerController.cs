using HoofGame.Horse;
using HoofGame.Infrastructure;
using HoofGame.InputActions;
using HoofGame.Util;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace HoofGame.Player
{
    [RequireComponent(typeof(Camera))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Transform _knife;
        [SerializeField] private float _minDistance = 10f;
        [SerializeField] private Hoof _hoof;

        private Camera _camera;
        private GameplayInputActions _inputActions;
        private bool _clicked;

        private Vector2 _startPos;
        private Vector2 ClickPos => _inputActions.Hoove.Position.ReadValue<Vector2>();

        private void Start()
        {
            _camera = GetComponent<Camera>();
            InitInputActions();
        }

        private void Update()
        {
            if (!_clicked) return;
            var currentPos = new Vector2(_startPos.x, Mathf.Max(_startPos.y, ClickPos.y));
            var ray = _camera.ScreenPointToRay(currentPos);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, 1 << (int)Layers.Hoof))
            {
                _knife.position = hit.point;
                _hoof.Clean(hit.textureCoord);
            }
        }

        private void InitInputActions()
        {
            _inputActions = GameplaySystemsContainer.Instance.GameplayInputActions;

            _inputActions.Hoove.Enable();
            _inputActions.Hoove.Click.performed += OnClickHandler;
            _inputActions.Hoove.Click.canceled += OnClickCanceledHandler;
        }

        private void OnClickCanceledHandler(InputAction.CallbackContext _)
        {
            _knife.gameObject.SetActive(false);
            _clicked = false;
            if (Vector2.Distance(_startPos, ClickPos) < _minDistance)
            {
                _hoof.RollbackState();
            }
        }
        private void OnClickHandler(InputAction.CallbackContext _)
        {
            _knife.gameObject.SetActive(true);
            _startPos = ClickPos;
            _clicked = true;
            _hoof.SaveState();
        }
    }
}

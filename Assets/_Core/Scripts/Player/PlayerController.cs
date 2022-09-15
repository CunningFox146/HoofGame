using HoofGame.Horse;
using HoofGame.Infrastructure;
using HoofGame.InputActions;
using HoofGame.Util;
using UnityEngine;
using UnityEngine.InputSystem;

namespace HoofGame.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Transform _knife;
        [SerializeField] private float _minDistance = 10f;
        [SerializeField] private float _maxSpeed = 10f;

        private Hoof _hoof;

        private Camera _camera;
        private GameplayInputActions _inputActions;
        private bool _clicked;

        private Vector2 _startPos;
        private Vector2 _lastPos;
        private Vector2 ClickPos => _inputActions.Hoove.Position.ReadValue<Vector2>();

        private void Start()
        {
            _hoof = GameplaySystemsContainer.Instance.Hoof;
            _camera = Camera.main;
            InitInputActions();
        }

        private void Update()
        {
            if (!_clicked) return;
            var currentPos = new Vector2(_startPos.x, Mathf.Max(_startPos.y, ClickPos.y));
            if (Vector2.Distance(_lastPos, currentPos) >= _maxSpeed)
            {
                StopCleaning();
                _hoof.RollbackState();
                return;
            }

            var ray = _camera.ScreenPointToRay(currentPos);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, 1 << (int)Layers.Hoof))
            {
                _knife.position = hit.point;
                _hoof.Clean(hit.textureCoord);
            }
            _lastPos = currentPos;
        }

        private void InitInputActions()
        {
            _inputActions = GameplaySystemsContainer.Instance.GameplayInputActions;

            _inputActions.Hoove.Click.performed += OnClickHandler;
            _inputActions.Hoove.Click.canceled += OnClickCanceledHandler;
        }

        private void StopCleaning()
        {
            _knife.gameObject.SetActive(false);
            _clicked = false;
        }

        private void OnClickCanceledHandler(InputAction.CallbackContext _)
        {
            if (!_clicked) return;
            StopCleaning();
            if (Vector2.Distance(_startPos, ClickPos) < _minDistance)
            {
                _hoof.RollbackState();
                return;
            }
            _hoof.UpdateDirtPerncent();
        }

        private void OnClickHandler(InputAction.CallbackContext _)
        {
            if (_clicked) return;
            _knife.gameObject.SetActive(true);
            _startPos = ClickPos;
            _lastPos = _startPos;
            _clicked = true;
            _hoof.SaveState();
        }
    }
}

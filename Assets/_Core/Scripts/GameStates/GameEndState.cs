using HoofGame.Cameras;
using HoofGame.Horse;
using HoofGame.Infrastructure;
using HoofGame.InputActions;
using UnityEngine.InputSystem;

namespace HoofGame.GameStates
{
    public class GameEndState : IGameState
    {
        private HorseAnimation _horse;
        private Hoof _hoof;
        private CameraSystem _cameraSystem;
        private SceneSystem _sceneSystem;
        private GameplayInputActions _inputActions;

        public GameEndState()
        {
            var container = GameplaySystemsContainer.Instance;
            _horse = container.Horse;
            _hoof = container.Hoof;
            _cameraSystem = container.CameraSystem;
            _inputActions = container.GameplayInputActions;
            _sceneSystem = container.SceneSystem;
        }

        public void OnEnter()
        {
            _horse.StartWin();
            _hoof.gameObject.SetActive(false);
            _cameraSystem.SetCamera(CameraType.End);
            _inputActions.Hoove.Disable();
            _inputActions.Start.Enable();
            _inputActions.Start.Click.performed += OnClickHandler;
        }

        public void OnExit()
        {
            _inputActions.Start.Click.performed -= OnClickHandler;
        }

        private void OnClickHandler(InputAction.CallbackContext obj)
        {
            _sceneSystem.Reload();
        }
    }
}

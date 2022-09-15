using HoofGame.Infrastructure;
using HoofGame.InputActions;
using UnityEngine.InputSystem;

namespace HoofGame.GameStates
{
    public class GameStartState : IGameState
    {
        private GameplayInputActions _inputActions;

        public GameStartState()
        {
            _inputActions = GameplaySystemsContainer.Instance.GameplayInputActions;
        }

        public void OnEnter()
        {
            _inputActions.Start.Click.performed += OnClickPerformed;
            _inputActions.Start.Enable();
        }
        public void OnExit()
        {
            _inputActions.Start.Click.performed -= OnClickPerformed;
            _inputActions.Start.Disable();
            _inputActions.Hoove.Enable();
        }

        private void OnClickPerformed(InputAction.CallbackContext _)
        {
            GameState.CurrentState = new GameplayState();
        }
    }
}

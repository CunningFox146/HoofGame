using HoofGame.Infrastructure;

namespace HoofGame.GameStates
{
    public class GameEndState : IGameState
    {
        public void OnEnter()
        {
            GameplaySystemsContainer.Instance.CameraSystem.StartEndCamera();
        }

        public void OnExit()
        {

        }
    }
}
